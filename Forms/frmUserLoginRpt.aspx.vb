Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmUserLoginRpt
    Inherits System.Web.UI.Page
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
        'lbl_title.Text = "Reset Password"
        If Not Page.IsPostBack Then
            Insert_log()
            FillRolName()
        End If
    End Sub
    Private Sub FillRolName()
        Try
            Dim DrRolName As SqlDataReader
            Dim objClsdb As New clsDB
            Dim arrParameter(0, 1) As String
            arrParameter(0, 0) = "@RoleType"
            arrParameter(0, 1) = ""
            DrRolName = objClsdb.getReader_Sproc_NoParam("d_sp_get_user_name_for_Tracing")
            ddl_user.Items.Clear()
            If DrRolName.HasRows Then
                ddl_user.DataSource = DrRolName
                ddl_user.DataTextField = "D_USER_NAME"
                ddl_user.DataValueField = "D_USER_ID"
                ddl_user.DataBind()
            End If
            ddl_user.Items.Insert(1, New ListItem("ALL", "99999999"))

            objClsdb = Nothing
            DrRolName.Close()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub btn_View_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_View.Click
        Try
            Dim objDB As New clsDB
            'retirve the employee details
            Dim dsReport As New DataSet
            Dim Param(3, 1) As String
            Param(0, 0) = "@USER_ID"
            Param(0, 1) = ddl_user.SelectedValue
            Param(1, 0) = "@T_DATE"
            Param(1, 1) = txt_todt.Text
            Param(2, 0) = "@F_DATE"
            Param(2, 1) = txt_frmdt.Text
            Param(3, 0) = "@USER_ROLE"
            Param(3, 1) = Session.Item("User_Role")

            dsReport = objDB.ExecProc_getDataSet("D_SP_GET_USER_LOGIN_DETAILS", Param)
            Dim dvReport As DataView
            dvReport = dsReport.Tables(0).DefaultView
            dvReport.Sort = "LOGIN DESC"
            If Not dsReport Is Nothing Then
                If dsReport.Tables(0).Rows.Count > 0 Then
                    gdvUser.DataSource = dvReport
                    gdvUser.DataBind()
                    'lbl_msg.Visible = False
                    pnl_gdv.Visible = True
                Else
                    ' lbl_msg.Visible = True
                    pnl_gdv.Visible = False
                End If
            Else
                gdvUser.DataSource = Nothing
                gdvUser.DataBind()
                ' lbl_msg.Visible = True
                ' rep_user_dtls.Visible = False
            End If

            objDB = Nothing
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub btn_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        ddl_user.Items.Clear()
        FillRolName()
        txt_frmdt.Text = ""
        txt_todt.Text = ""
        gdvUser.DataSource = Nothing
        gdvUser.Visible = False
    End Sub
    Protected Sub btn_excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_excel.Click

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

