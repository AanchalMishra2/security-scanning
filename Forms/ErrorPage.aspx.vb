
Partial Class Forms_ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            'Response.Redirect("frmLogin.aspx")
            Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
            Exit Sub
        End If
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        If Not Page.IsPostBack Then
            InsertErrLog(Session("USER_ID"), Request.QueryString("page"), Request.QueryString("err"))
            'DisplayMsg(Session())
            Dim pnl As Panel = New Panel
            Dim lbl As Label = New Label
            Dim lbl1 As Label = New Label
            Dim lbl2 As Label = New Label
            Dim lbl3 As Label = New Label
            lbl.Text = "An Error Has Occured"
            pnl.Controls.Add(New LiteralControl("<h2>"))
            pnl.Controls.Add(lbl)
            pnl.Controls.Add(New LiteralControl("</h2>"))
            lbl3.Text = "An Unexpected error occured on our website. The Website Administrator has been Notified."
            pnl.Controls.Add(lbl3)
            pnl.Controls.Add(New LiteralControl("</br></br>Page Name:</br>"))
            lbl1.Text = Replace(Request.QueryString("page"), "_", ".")
            pnl.Controls.Add(lbl1)
            pnl.Controls.Add(New LiteralControl("</br></br>Error Desc:</br>"))
            lbl2.Text = Request.QueryString("err")
            pnl.Controls.Add(lbl2)
            ' Me.Controls.Add(pnl)
            pnl_err.Controls.Add(pnl)
        End If
    End Sub
    Public Function InsertErrLog(ByVal uid As String, ByVal page As String, ByVal err As String)
        Dim fun As Functions = New Functions
        Functions.InsertErrLog(uid, page, err)
    End Function
End Class
