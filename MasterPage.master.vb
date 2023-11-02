Imports System.Xml
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.SqlClient

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim objDB As New clsDB
    Dim dsMenu As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        If Not Page.IsPostBack Then

            Try
                lblWelcomeMessage.Text = "Welcome  " & Session.Item("User_FName") & " " & Session.Item("User_LName")
                dsMenu = Nothing
                xmlDataSource.Data = Nothing
                Dim params(0, 1) As String

                params(0, 0) = "@intRoleId"
                params(0, 1) = CInt(Session.Item("Role_ID"))
                'dsMenu = objDB.ExecProc_getDataSet("D_SP_GET_ROLE_FORM_DETAIL", params)

                dsMenu = objDB.ExecProc_getDataSet("D_SP_GET_ROLE_FORM_DETAIL", params)
                dsMenu.DataSetName = "Menus"
                dsMenu.Tables(0).TableName = "Menu"
                Dim relation As New DataRelation("ParentChild", dsMenu.Tables("Menu").Columns("D_Form_ID"), dsMenu.Tables("Menu").Columns("D_Form_Parent_ID"), True)

                relation.Nested = True
                dsMenu.Relations.Add(relation)

                xmlDataSource.Data = dsMenu.GetXml()
                If Not Request.Params("Sel") Is Nothing Then
                    Page.Controls.Add(New System.Web.UI.LiteralControl("You selected " & Request.Params("Sel")))
                End If

            Catch ex As Exception

            End Try
        End If


    End Sub

  

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~\frmLogin.aspx")
    End Sub
End Class

