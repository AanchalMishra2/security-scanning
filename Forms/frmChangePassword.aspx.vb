Imports clsDB
Partial Class Forms_frmChangePassword
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
        'lbl_title.Text = "Change Password"
        If Not Page.IsPostBack Then
            Insert_log()
        End If
    End Sub
    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Dim cal As New ChangePassword
        '        cal.change_password(Session("USER_ID"), txt_oldpwd.Text, text_newpwd.Text)
        Try
            Dim Params As New ArrayList
            Params.Add(Session("USER_ID"))
            Params.Add(txt_oldpwd.Text)
            Params.Add(text_newpwd.Text)
            Dim strOUT As String
            Dim objDB As New clsDB
            strOUT = objDB.ExecProc_getStatus("D_SP_CHANGEPASSWORD", Params)
            Dim sAlert As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            '            Page.RegisterStartupScript("NoAnswer", sAlert)
            Exit Sub
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
    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Dim err As Exception = Server.GetLastError
        Dim url As String = "ErrorPage.aspx?page=" & Replace(Page.ToString.Trim, "ASP.", " ") & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        Response.Redirect(url)
    End Sub


End Class
