
Partial Class Forms_frmMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        'If Session.Item("User_Role") = "USER" Then
        'Response.Write("<script>window.parent.location.href='../frmUserIssueDetail.aspx'</script>")
        ' Response.Redirect("~/Forms/frmUserIssueDetail.aspx")
        'End If
    End Sub
    '    If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
    '        Session.Abandon()
    ''Response.Redirect("frmLogin.aspx")
    '        Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
    '        Exit Sub
    '    End If
    'End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            'Response.Redirect("frmLogin.aspx")
            'Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
            Response.Redirect("../frmLogin.aspx")
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
End Class


