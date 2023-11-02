Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmAssignRole
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            'Response.Redirect("frmLogin.aspx")
            Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Insert_Log()
            FillRolName()
            FillRightList(0)
            FillAssignedRights(0)
          
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (DDLRoleName.SelectedItem.Text = "--SELECT--") Then
                lblWarningMessage.Text = "Please select a Role."
                lblWarningMessage.Visible = True
                'Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please select a Role.')</script>"
                'Page.RegisterStartupScript("NoAnswer", sAlert)
                Exit Sub
            End If
            'ValidateField()
            Dim intI As Integer
            Dim ObjCon As New clsDB
            Dim cmdRolModule As SqlCommand


            cmdRolModule = New SqlCommand
            cmdRolModule.CommandText = "D_SP_DELETE_ROLE_MODULE"
            cmdRolModule.CommandType = CommandType.StoredProcedure
            cmdRolModule.Connection = ObjCon.GetConnection()

            ''' Role Name
            cmdRolModule.Parameters.Add("@intRoleId", SqlDbType.BigInt, 20)
            cmdRolModule.Parameters(0).Value = DDLRoleName.SelectedValue
            cmdRolModule.ExecuteNonQuery()
            ObjCon.CloseConnection()
            For intI = 0 To lstAssign.Items.Count - 1
                cmdRolModule = New SqlCommand
                cmdRolModule.CommandText = "D_SP_INSERT_ROLE_FORM_DTL"
                cmdRolModule.CommandType = CommandType.StoredProcedure
                cmdRolModule.Connection = ObjCon.GetConnection()

                'Role Name
                cmdRolModule.Parameters.Add("@intRoleId", SqlDbType.BigInt, 20)
                cmdRolModule.Parameters(0).Value = DDLRoleName.SelectedValue

                'Form Id
                cmdRolModule.Parameters.Add("@strFormDesc", SqlDbType.VarChar, 100)
                cmdRolModule.Parameters(1).Value = lstAssign.Items(intI).Text
                'MENU ORDER

                'Error Description
                cmdRolModule.Parameters.Add("@ERROR_NO", SqlDbType.BigInt)
                cmdRolModule.Parameters(2).Direction = ParameterDirection.Output
                cmdRolModule.ExecuteNonQuery()
                ObjCon.CloseConnection()
                If cmdRolModule.Parameters("@ERROR_NO").Value = 0 Then
                    lblWarningMessage.Text = "Record Saved Successfully"
                    lblWarningMessage.Visible = True
                    'RegisterClientScriptBlock("null", "<Script Language='JavaScript'>alert('Record Saved Successfully')</Script>")
                ElseIf cmdRolModule.Parameters("@ERROR_NO").Value = 1 Then
                    lblWarningMessage.Text = "Record Already Exist"
                    lblWarningMessage.Visible = True
                    ' RegisterClientScriptBlock("null", "<Script Language='JavaScript'>alert('Record Already Exist')</Script>")
                ElseIf cmdRolModule.Parameters("@ERROR_NO").Value = 2 Then
                    lblWarningMessage.Text = "Record Not Saved "
                    lblWarningMessage.Visible = True
                    'RegisterClientScriptBlock("null", "<Script Language='JavaScript'>alert('Record Not Saved ')</Script>")
                End If

            Next
            lstAssign.Items.Clear()
            FillRolName()
            FillRightList(0)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub FillRolName()
        Try
            Dim DrRolName As SqlDataReader
            Dim objClsdb As New clsDB
            Dim arrParameter(0, 1) As String
            arrParameter(0, 0) = "@RoleType"
            arrParameter(0, 1) = ""
            DrRolName = objClsdb.ExecProc_getDataReder("D_SP_GET_ROLE_NAME", arrParameter)
            DDLRoleName.Items.Clear()
            If DrRolName.HasRows Then
                DDLRoleName.DataSource = DrRolName
                DDLRoleName.DataTextField = "D_ROLE_NAME"
                DDLRoleName.DataValueField = "D_ROLE_ID"
                DDLRoleName.DataBind()
            End If
            objClsdb = Nothing
            DrRolName.Close()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub FillRightList(ByVal RoleId As Integer)
        Try
            Dim DrAvailRights As SqlDataReader
            Dim objClsdb As New clsDB

            Dim cmdRolModule As SqlCommand
            cmdRolModule = New SqlCommand
            cmdRolModule.CommandText = "D_SP_GET_AVAILABLE_RIGHTS"
            cmdRolModule.CommandType = CommandType.StoredProcedure
            cmdRolModule.Connection = objClsdb.GetConnection()
            'Role ID
            cmdRolModule.Parameters.Add("@intRoleId", SqlDbType.Int)
            If RoleId > 0 Then
                cmdRolModule.Parameters(0).Value = RoleId
            Else
                cmdRolModule.Parameters(0).Value = 0
            End If

            DrAvailRights = cmdRolModule.ExecuteReader(CommandBehavior.CloseConnection)

            lstAvailable.Items.Clear()
            If DrAvailRights.HasRows Then
                lstAvailable.DataSource = DrAvailRights
                lstAvailable.DataTextField = "D_FORM_TITLE"
                lstAvailable.DataValueField = "D_FORM_ID"
                lstAvailable.DataBind()
            End If
            objClsdb = Nothing
            DrAvailRights.Close()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub FillAssignedRights(ByVal RoleId As Integer)
        Try
            Dim DrAvailRights As SqlDataReader
            Dim objClsdb As New clsDB

            Dim cmdRolModule As SqlCommand
            cmdRolModule = New SqlCommand
            cmdRolModule.CommandText = "D_SP_GET_ASSIGNED_RIGHTS"
            cmdRolModule.CommandType = CommandType.StoredProcedure
            cmdRolModule.Connection = objClsdb.GetConnection()
            'Role ID
            cmdRolModule.Parameters.Add("@intRoleId", SqlDbType.Int)
            If RoleId > 0 Then
                cmdRolModule.Parameters(0).Value = RoleId
            Else
                cmdRolModule.Parameters(0).Value = 0
            End If

            DrAvailRights = cmdRolModule.ExecuteReader(CommandBehavior.CloseConnection)

            lstAssign.Items.Clear()
            If DrAvailRights.HasRows Then
                lstAssign.DataSource = DrAvailRights
                lstAssign.DataTextField = "D_FORM_TITLE"
                lstAssign.DataValueField = "D_FORM_ID"
                lstAssign.DataBind()
            End If
            objClsdb = Nothing
            DrAvailRights.Close()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub ValidateField()
        Try
            If DDLRoleName.SelectedValue = 0 Then
                Dim sAlert As String = "<Script Language='JavaScript'>alert('Please select Role Name')</Script>"
                Page.RegisterStartupScript("Select Role Name", sAlert)
                Exit Sub
            End If
            If lstAssign.Items.Count = 0 Then
                Dim sAlert As String = "<Script Language='JavaScript'>alert('Select Atleast one Available right')</Script>"
                Page.RegisterStartupScript("NoAnswer", sAlert)
                Exit Sub
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim intListitem As Integer = 0
            Dim intCount As ListItem
            For Each intCount In lstAvailable.Items
                If intCount.Selected Then
                    intListitem = intListitem + 1
                    lstAssign.Items.Add(intCount)
                End If
            Next
            If intListitem = 0 Then
                lblWarningMessage.Text = "Choose at least one for add"
                lblWarningMessage.Visible = True
                'RegisterClientScriptBlock("null", "<Script Language='JavaScript'>alert('Choose at least one for add')</Script>")
                Exit Sub
            End If
            Dim intCount1, intSelected As Integer
            intCount1 = lstAvailable.Items.Count
            For intSelected = 0 To intCount1 - 1
                If intSelected <= lstAvailable.Items.Count - 1 Then
                    If lstAvailable.Items.Item(intSelected).Selected Then
                        lstAvailable.Items.RemoveAt(lstAvailable.SelectedIndex)
                        intSelected -= 1
                    End If
                End If
            Next
            lstAvailable.ClearSelection()
            lstAssign.ClearSelection()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub btnAddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAll.Click
        Try
            Dim intCount, intAll As Integer
            intCount = lstAvailable.Items.Count
            For intAll = 0 To intCount - 1
                lstAssign.Items.Add(lstAvailable.Items.Item(intAll).Text)
            Next
            lstAvailable.Items.Clear()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If lstAssign.Items.Count = 0 Then
                lblWarningMessage.Text = "Select no Available right to remove"
                lblWarningMessage.Visible = True
                'Dim sAlert As String = "<Script Language='JavaScript'>alert('Select no Available right to remove')</Script>"
                'Page.RegisterStartupScript("NoAnswer", sAlert)
            End If
            Dim intCount As ListItem
            For Each intCount In lstAssign.Items
                If intCount.Selected Then
                    lstAvailable.Items.Add(intCount)
                End If
            Next
            Dim intCount1, intSelected As Integer
            intCount1 = lstAssign.Items.Count
            For intSelected = 0 To intCount1 - 1
                If intSelected <= lstAssign.Items.Count - 1 Then
                    If lstAssign.Items.Item(intSelected).Selected Then
                        lstAssign.Items.RemoveAt(lstAssign.SelectedIndex)
                        intSelected -= 1
                    End If
                End If
            Next
            lstAvailable.ClearSelection()
            lstAssign.ClearSelection()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        Try
            Dim intCount As ListItem
            For Each intCount In lstAssign.Items
                lstAvailable.Items.Add(intCount)
            Next
            lstAssign.Items.Clear()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            FillRightList(0)
            FillAssignedRights(0)
            DDLRoleName.SelectedIndex = -1
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Private Sub DDLRoleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDLRoleName.SelectedIndexChanged
        Try
            FillRightList(DDLRoleName.SelectedItem.Value)
            FillAssignedRights(DDLRoleName.SelectedItem.Value)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub Insert_log()
        Try
            Functions.Insert_Log(Session("User_ID"), Session("User_Login_Name"), "Download Auditor Files", Request.ServerVariables("REMOTE_ADDR").ToString.Trim, Replace(Page.ToString.Trim, "ASP.", " ").Trim, "Y", Session.Item("NEW_GUID"), "INSERT")
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Dim err As Exception = Server.GetLastError
        Dim url As String = "ErrorPage.aspx?page=" & Replace(Page.ToString.Trim, "ASP.", " ") & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        Response.Redirect(url)
    End Sub

End Class
