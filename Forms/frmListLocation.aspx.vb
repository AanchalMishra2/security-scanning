Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Partial Class Forms_frmListLocation
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            Response.Redirect("../frmLogin.aspx")
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lblWarningMessage.Text = ""
        'pnlimport.Visible = False
        If Not Page.IsPostBack Then
            Insert_log()
            BindCircle()
        End If
    End Sub
    Public Sub BindCircle()
        Try
            Dim DRCircle As SqlDataReader
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intUserId"
            circleParam(0, 1) = CInt(Session("USER_ID"))
            DRCircle = objDB.ExecProc_getDataReder("D_SP_GET_CIRCLE_FOR_REPORT", circleParam)
            If DRCircle.HasRows Then
                ' dgSite.Visible = True
                ddlcircle.DataSource = DRCircle
                ddlcircle.DataTextField = "D_CIRCLE_NAME"
                ddlcircle.DataValueField = "D_CIRCLE_ID"
                ddlcircle.DataBind()
                DRCircle.Close()
            Else
                'lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnview.Click
        Dim varParam(1, 1) As String
        Dim intRowCount As Integer
        Try

            varParam(0, 0) = "@intCircleId"
            varParam(0, 1) = ddlcircle.SelectedValue
            varParam(1, 0) = "@intUserId"
            varParam(1, 1) = CInt(Session("USER_ID"))
            Dim dsUserMstr As New DataSet
            'dsUserMstr = objDB.ExecProc_getDataSet("D_SP_LIST_LOCATION", varParam)
            'intRowCount = dsUserMstr.Tables(0).Rows.Count
            'If (intRowCount <= 0) Then
            '    lblmsg.Text = "No Records Found"
            '    lblmsg.Visible = True
            '    lbldata.Visible = False
            'Else
            '    BuildString(dsUserMstr)
            'End If
            Dim reportDataSource As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource()
            Dim viewer As ReportViewer = New ReportViewer()
            viewer.Reset()
            viewer.ShowRefreshButton = False
            viewer.ShowToolBar = True
            Dim datasource As ReportDataSource = New ReportDataSource()
            viewer.Width = 800 'set height and width to rpt
            viewer.Height = 350
            ' giving path to report
            viewer.LocalReport.ReportPath = Server.MapPath("../Reports/ListLocation.rdlc")
            dsUserMstr = Nothing
            dsUserMstr = objDB.ExecProc_getDataSet("D_SP_LIST_LOCATION", varParam)
            reportDataSource.Name = "DataSet1_D_SP_LIST_LOCATION"
            reportDataSource.Value = dsUserMstr.Tables(0)
            viewer.LocalReport.DataSources.Add(reportDataSource)
            pnlssrs.Controls.Add(viewer)
            pnlssrs.Visible = True
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BuildString(ByVal ds As DataSet)
        lbldata.Visible = True
        Dim str As New System.Text.StringBuilder
        Dim i, j, k As Integer
        str.Append("<div><table border='1' width='900px'><tr>")
        For k = 0 To ds.Tables(0).Columns.Count - 1
            str.Append("<td align='center' style='width:140px;'><b>" & ds.Tables(0).Columns(k).Caption() & "</b></td>")
        Next
        str.Append("</tr><td colspan=" & ds.Tables(0).Columns.Count & "><div style='overflow:scroll;height:400px;width:900px'><table border='1' width='900px'>")

        For i = 0 To ds.Tables(0).Rows.Count - 1
            str.Append("<tr>")
            For j = 0 To ds.Tables(0).Columns.Count - 1
                str.Append("<td align='center' style='width:140px;'>" & Convert.ToString(ds.Tables(0).Rows(i)(j)) & "</td>")
            Next
            str.Append("</tr>")
        Next
        str.Append("</table></div></td></table></div>")
        lbldata.Text = str.ToString
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

    Protected Sub ddlcircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcircle.SelectedIndexChanged
        pnlssrs.Visible = False
    End Sub
End Class
