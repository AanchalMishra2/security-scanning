Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmResetPassword
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
            BindGrid()

        End If
    End Sub
    Public Sub BindGrid()
        Dim varParam(1, 1) As String
        Dim DRProject As SqlDataReader
        Dim DRUser As SqlDataReader
        Dim objDB As New clsDB
        Try
            varParam(0, 0) = "@strRole"
            varParam(0, 1) = Session("User_Role")
            varParam(1, 0) = "@intUserId"
            varParam(1, 1) = Session("User_Id")
            DRUser = objDB.ExecProc_getDataReder("D_SP_GET_USER_LIST", varParam)
            If DRUser.HasRows Then
                ddl_user.DataSource = DRUser
                ddl_user.DataTextField = "D_USER_NAME"
                ddl_user.DataValueField = "D_USER_ID"
                ddl_user.DataBind()
                DRUser.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub btn_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        Try
            Dim Params As New ArrayList
            Dim strPassword As String
            Dim objDB As New clsDB
            strPassword = "password" 'strName & strDate 
            Params.Add(ddl_user.SelectedValue) 'P1
            Params.Add(strPassword) 'P2
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_RESET_PASSWORD", Params)
            Dim sAlert As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            Page.RegisterStartupScript("NoAnswer", sAlert)
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
