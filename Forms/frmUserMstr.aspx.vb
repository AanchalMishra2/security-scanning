Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Serialization
Partial Class Forms_frmManageUsers
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            Response.Redirect("frmLogin.aspx")
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblWarningMessage.Text = ""
        'pnlimport.Visible = False
        If Not Page.IsPostBack Then
            Insert_log()
            'chkIsactive.Checked = True
            InitializeVariables()
            BindGrid()
        End If
    End Sub
    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "I_USER_ID"
        ViewState("SortOrder") = "ASC"
        SetSortOrder()
    End Sub
    Protected Sub SetSortOrder()
        If ViewState("SortOrder") Is Nothing Then
            ViewState("SortOrder") = "ASC"
        ElseIf ViewState("SortOrder") = "ASC" Then
            ViewState("SortOrder") = "DESC"
        Else
            ViewState("SortOrder") = "ASC"

        End If
    End Sub
    Protected Sub BindGrid()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intUserId"
        'param(0, 1) = Session("USER_ID")  ' 'Session("UserID")
        'dsSupplier = objDB.ExecProc_getDataSet("D_SP_GET_USER", param)
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_USER")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        GVuser.DataSource = dt
        GVuser.DataBind()
    End Sub
    Protected Sub BindGrid_SORT()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intUserId"
        'param(0, 1) = Session("USER_ID")  '
        'dsSupplier = objDB.ExecProc_getDataSet("I_GET_USER_DETAIL", param)
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_USER")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVuser.DataSource = dt.DefaultView
        GVuser.DataBind()
    End Sub
    Protected Sub ClearControl()
        txtEmpId.Text = ""
        txtLoginName.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        hf_userId.Value = Nothing
        txtEmail.Text = ""
        ddlDesination.SelectedIndex = -1
        btnSubmit.Text = "Save"
        ddlRole.SelectedIndex = -1
        chkIsactive.Checked = True
        lblWarningMessage.Visible = False
    End Sub
    Protected Sub SaveUser()
        Try
            Dim varParam As New ArrayList
            Dim usereId As Integer
            Dim UserActive As String
            Dim strOUT As String
            Dim strPass As String
            Dim strEmp As String
            Dim strName As String
            Dim intUserId As Integer = 0
            If hf_userId.Value.ToString() = "" Then
                varParam.Add("INSERT") 'P0
                varParam.Add(intUserId) 'p1
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hf_userId.Value.ToString())) 'p1
            End If
            strEmp = txtEmpId.Text.Substring(0, 3)
            strName = txtFirstName.Text.Substring(0, 3)
            strPass = "password" 'strEmp & strName
            varParam.Add(CInt(txtEmpId.Text.Trim)) 'P2
            varParam.Add(txtLoginName.Text.Trim) 'P3
            varParam.Add(strPass) 'P4 
            varParam.Add(txtFirstName.Text.Trim) 'P5
            varParam.Add(txtLastName.Text.Trim) 'P6
            varParam.Add(ddlDesination.SelectedValue) 'P7
            varParam.Add(ddlRole.SelectedValue) 'P8
            varParam.Add(txtEmail.Text.Trim) 'P9
            If (chkIsactive.Checked = True) Then
                varParam.Add("Y") 'P10
            End If
            If (chkIsactive.Checked = False) Then
                varParam.Add("N") 'P10
            End If
            'strEmp = txtEmpId.Text.Substring(0, 3)
            'strName = txtFirstName.Text.Substring(0, 3)
            'strPass = strEmp & strName
            'varParam.Add(txtEmpId.Text.Trim) 'P2
            'varParam.Add(txtLoginName.Text.Trim) 'p3
            'varParam.Add(strPass) 'p4
            'varParam.Add(txtFirstName.Text)
            'varParam.Add(txtLastName.Text)
            'varParam.Add(ddlDesination.SelectedValue)
            'varParam.Add(ddlRole.SelectedValue)
            'varParam.Add(txtEmail.Text)
            'usereId = Session("USER_ID")  '
            'varParam.Add(usereId) 'P5
            'If chkIsactive.Checked = True Then
            '    UserActive = "Y"
            'Else
            '    UserActive = "N"
            'End If
            'varParam.Add(UserActive) 'P6
            strOUT = objDB.ExecProc_getStatus("D_SP_USER", varParam)
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DeleteUser(ByVal id As Integer)
        Try
            Dim varparam(0, 1) As String
            Dim strOUT As String = ""
            varparam(0, 0) = "@intUserID"
            varparam(0, 1) = id
            strOUT = objDB.ExecProc_getRecordsAffected("I_SP_DELETE_USER_MSTR", varparam)
            lblWarningMessage.Visible = True
            If strOUT = "1" Then
                lblWarningMessage.Text = "Record Deleted Sucussfully !!"
            Else
                lblWarningMessage.Text = "Failed To Delete Record !!"
            End If
            BindGrid()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindControls(ByVal intUserId As Integer)
        Try

        
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@intUserId"
            varParam(0, 1) = intUserId
            dt = objDB.ExecProc_getDataTable("I_SP_GET_USER", varParam)
            If dt.Rows.Count > 0 Then
                txtEmpId.Text = dt.Rows(0)("I_USER_EMP_ID").ToString()
                txtLoginName.Text = dt.Rows(0)("I_LOGIN_NAME").ToString()
                txtFirstName.Text = dt.Rows(0)("I_F_NAME").ToString()
                txtLastName.Text = dt.Rows(0)("I_L_NAME").ToString()
                ddlDesination.SelectedValue = dt.Rows(0)("I_DESIG_ID")
                ddlRole.SelectedValue = dt.Rows(0)("I_ROLE_ID")
                txtEmail.Text = dt.Rows(0)("I_USER_EMAIL").ToString()

                If dt.Rows(0)("I_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        'SaveUser()
        Me.mdlPopup.Hide()

        ' BindGrid()
        Me.upPnlUserDetail.Update()
        SaveUser()
        Me.upGVUser.Update()

        'pnlPopup.Visible = False
    End Sub
    'Public Sub BindProject()
    '    Try


    '        Dim arrParam(0, 1) As String
    '        Dim drProject As SqlDataReader
    '        ' Dim dtTranType As DataTable
    '        arrParam(0, 0) = "@intUserId"
    '        arrParam(0, 1) = 1 'Session("USER_ID")
    '        drProject = objDB.ExecProc_getDataReder("T_SP_GET_PROJECT_FOR_DDL", arrParam)
    '        ' dtTranType = objDB.ExecProc_getDataTable_noParam()

    '        If drProject.HasRows Then
    '            ddlProject.DataSource = drProject
    '            ddlProject.DataTextField = "IA_PROJECT_NAME"
    '            ddlProject.DataValueField = "IA_PROJECT_ID"
    '            ddlProject.DataBind()
    '            drProject.Close()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub BindDesignation()
        Try

        
            Dim arrParam(0, 1) As String
            Dim drDesig As SqlDataReader
            ' Dim dtTranType As DataTable
            'arrParam(0, 0) = "@intUserId"
            'arrParam(0, 1) = Session("USER_ID")  ' 'Session("USER_ID")
            ' drDesig = objDB.ExecProc_getDataReder("D_SP_DESIGNATION_LIST", arrParam)
            drDesig = objDB.getReader_Sproc_NoParam("D_SP_DESIGNATION_LIST")
            ' dtTranType = objDB.ExecProc_getDataTable_noParam()

            If drDesig.HasRows Then
                ddlDesination.DataSource = drDesig
                ddlDesination.DataTextField = "D_DESIG_NAME"
                ddlDesination.DataValueField = "D_DESIG_ID"
                ddlDesination.DataBind()
                drDesig.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindRole()
        Try


            Dim arrParam(0, 1) As String
            Dim drRole As SqlDataReader
            ' Dim dtTranType As DataTable
            'arrParam(0, 0) = "@intUserId"
            'arrParam(0, 1) = Session("USER_ID")
            'drRole = objDB.ExecProc_getDataReder("I_SP_GET_ROLE_FOR_DDL", arrParam)
            drRole = objDB.getReader_Sproc_NoParam("D_SP_ROLE_LIST")

            If drRole.HasRows Then
                ddlRole.DataSource = drRole
                ddlRole.DataTextField = "D_ROLE_NAME"
                ddlRole.DataValueField = "D_ROLE_ID"
                ddlRole.DataBind()
                drRole.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GVuser_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVuser.PageIndexChanging
        GVuser.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVuser_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVuser.RowDataBound
        'Try

        '    'if (e.Row.RowType == DataControlRowType.DataRow)
        '    '    {
        '    '        Expanse expanse = e.Row.DataItem as Expanse;
        '    '        (e.Row.FindControl("lnkEdit") as LinkButton).Attributes.Add("onClick", "ShowEditModal('" + expanse.ID + "');");
        '    '        (e.Row.FindControl("lnkDelete") as LinkButton).CommandArgument = expanse.ID.ToString();
        '    '    }
        '    If e.Row.RowType = DataControlRowType.DataRow Then
        '        CType(e.Row.FindControl("lnkedit"), LinkButton).Attributes.Add("onClick", "ShowEditModal('" & DataBinder.Eval(e.Row.DataItem, "IA_USER_ID") & "');")
        '    End If

        '    'e.Item.Cells(1).Attributes("onclick") = "javascript:return confirm('Are you sure, you want to delete? """ & DataBinder.Eval(e.Item.DataItem, "T_EQUIP_SERIAL_NO") & """?')"
        '    'e.Row.Cells(1).FindControl("lnkedit").Attributes.Add("onClick", "ShowEditModal('" & DataBinder.Eval(e.Row.DataItem, "IA_USER_ID") & "');")
        '    'Dim lnkBtn As LinkButton = CType(GVuser.Rows(e.Row.DataItem).FindControl("lnkedit"), LinkButton)
        '    'lnkBtn.Attributes.Add("onClick", "ShowEditModal('" & DataBinder.Eval(e.Row.DataItem, "IA_USER_ID") & "');")
        '    '       e.Row.Cells(1).Attributes.Add("onClick", "ShowEditModal('" & DataBinder.Eval(e.Row.DataItem, "IA_USER_ID") & "');")
        'Catch ex As Exception

        'End Try

    End Sub

    Protected Sub GVuser_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVuser.RowDeleting
        Dim hf As HiddenField = CType(GVuser.Rows(e.RowIndex).FindControl("hfUserID"), HiddenField)
        Dim UserId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteUser(UserId)
    End Sub

    Protected Sub GVuser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVuser.SelectedIndexChanged
        Dim UserId As String
        UserId = GVuser.SelectedValue.ToString()
        hf_userId.Value = UserId
        Dim intUserId As Integer
        intUserId = CInt(UserId)
        'BindControls(intUserId)
        ShowModalData(intUserId)
        btnSubmit.Text = "Update"
    End Sub

    Protected Sub GVuser_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVuser.Sorting
        Dim exp As String = e.SortExpression
        ViewState("SortExpression") = exp
        BindGrid_SORT()
    End Sub

    
    Protected Sub ShowModalData(ByVal id As Integer)
        Try

            'dvUserDetail.Visible = True

            BindDesignation()
            BindRole()
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@IntUserId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("D_SP_GET_USER_DTLS", varParam)
            If dt.Rows.Count > 0 Then
                'dvUserDetail.DataSource = dt
                'dvUserDetail.DataBind()

                txtEmpId.Text = dt.Rows(0)("D_USER_EMP_ID").ToString()
                txtEmpId.Enabled = False
                txtLoginName.Text = dt.Rows(0)("D_LOGIN_NAME").ToString()
                txtLoginName.Enabled = False
                txtFirstName.Text = dt.Rows(0)("D_F_NAME").ToString()
                txtLastName.Text = dt.Rows(0)("D_L_NAME").ToString()
                ddlDesination.SelectedValue = dt.Rows(0)("D_DESIG_ID")
                ddlRole.SelectedValue = dt.Rows(0)("D_ROLE_ID")
                txtEmail.Text = dt.Rows(0)("D_USER_EMAIL").ToString()

                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If
            End If
            upPnlUserDetail.Update()
            mdlPopup.Show()
            txtEmpId.Focus()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            BindDesignation()
            BindRole()
            upPnlUserDetail.Update()
            mdlPopup.Show()
            txtEmpId.Focus()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click

        btnSubmit.Text = "Save"
        ShowModalDataInsert()

    End Sub



    <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
      Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer) As System.String()



        Dim serializer As New JavaScriptSerializer
        Dim icount As Integer = 100
        Dim sql As String
        ' Dim da As New Data.SqlClient.SqlDataAdapter
        Dim objDB1 As New clsDB
        'Dim dt As Data.DataTable
        'Dim comm As New SqlCommand
        Dim dt As New Data.DataTable
        Dim i As Long
        Try


            '   Dim commandText As String = "SELECT DISTINCT TOP " + count.ToString() + " I_F_NAME +' '+ I_L_NAME  ,I_USER_ID FROM I_USER_MSTR WHERE I_ACTIVE='Y' AND I_F_NAME LIKE '" + key + "%'"
            Using conn As SqlConnection = objDB1.GetConnection()

                sql = "SELECT DISTINCT TOP " + count.ToString() + " I_F_NAME +' '+ I_L_NAME as I_F_NAME  ,I_USER_ID FROM I_USER_MSTR WHERE I_ACTIVE='Y' AND I_F_NAME LIKE  '" + prefixText + "%'"
                Dim comm As New SqlCommand(sql, conn)
                comm.CommandType = CommandType.Text
                ' da.SelectCommand.Parameters.Add("@prefixText", Data.SqlDbType.VarChar, 200).Value = "%" & prefixText & "%"
   Dim da As New SqlDataAdapter(comm)

                da.Fill(dt)

            End Using

            Dim items(dt.Rows.Count - 1) As String

            i = 0
            Dim dr As Data.DataRow
            For Each dr In dt.Rows


                items(i) = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr("I_F_NAME").ToString(), dr("I_USER_ID").ToString())
                i = i + 1
            Next

            Return items
        Catch ex As Exception

        End Try

    End Function


    Private Shared Function GetSqlCommand(ByVal conn As SqlConnection, ByVal key As String, ByVal count As Integer) As SqlCommand
        Dim commandText As String = "SELECT DISTINCT TOP " + count.ToString() + " I_F_NAME +' '+ I_L_NAME  ,I_USER_ID FROM I_USER_MSTR WHERE I_ACTIVE='Y' AND I_F_NAME LIKE '" + key + "%'"
        Dim comm As New SqlCommand(commandText, conn)
        comm.CommandType = CommandType.Text
        Return comm
    End Function


    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        
        Dim tsearch As String = ""


        If tsearch = "" Then
            Call BindGrid()
        Else

            Dim ds1 As New DataSet
            Try
                Dim arrparam1(0, 1) As String
                arrparam1(0, 0) = "@intName"
                arrparam1(0, 1) = hdEmpID.Value



                ds1 = objDB.ExecProc_getDataSet("[I_SP_GET_SEARCH_USER_DETAIL]", arrparam1)
                Dim Dt As New DataTable
                Dt = ds1.Tables(0)

                GVuser.DataSource = Dt
                GVuser.DataBind()

            Catch ex As Exception

            End Try

        End If
    End Sub
    

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If hdEmpID.Value = "" Then
            Call BindGrid()
            Exit Sub
        End If

        Dim ds1 As New DataSet
        Try
            Dim arrparam1(0, 1) As String
            arrparam1(0, 0) = "@intName"
            arrparam1(0, 1) = hdEmpID.Value



            ds1 = objDB.ExecProc_getDataSet("[I_SP_GET_SEARCH_USER_DETAIL]", arrparam1)
            Dim Dt As New DataTable
            Dt = ds1.Tables(0)

            GVuser.DataSource = Dt
            GVuser.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Insert_log()
        Try
            Functions.Insert_Log(Session("User_ID"), Session("User_Login_Name"), "Download Auditor Files", Request.ServerVariables("REMOTE_ADDR").ToString.Trim, Replace(Page.ToString.Trim, "ASP.", " ").Trim, "Y", Session.Item("NEW_GUID"), "INSERT")
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Dim err As Exception = Server.GetLastError
        Dim url As String = "ErrorPage.aspx?page=" & Replace(Page.ToString.Trim, "ASP.", " ") & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        Response.Redirect(url)
    End Sub
End Class
