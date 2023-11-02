Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Partial Class Reports_DTreports
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            lblwarning.Text = "Session Time out."
            pnlwarning.Visible = True
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dsCustomer As New DataSet
            Dim varParam(4, 1) As String
            varParam(0, 0) = "@intCustomerId"
            varParam(0, 1) = Request.QueryString("param")
            varParam(1, 0) = "@intCircleId"
            varParam(1, 1) = Request.QueryString("c")
            varParam(2, 0) = "@strFDate"
            varParam(2, 1) = Request.QueryString("f")
            varParam(3, 0) = "@strTDate"
            varParam(3, 1) = Request.QueryString("t")
            varParam(4, 0) = "@intUserId"
            varParam(4, 1) = CInt(Session("USER_ID"))
            'dsCustomer = objDB.ExecProc_getDataSet("D_SP_DETAILED_DIESEL_TRACKING_REPORT", varParam)

            Dim reportDataSource As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource()
            Dim viewer As ReportViewer = New ReportViewer()
            viewer.Reset()
            viewer.ShowToolBar = True
            Dim datasource As ReportDataSource = New ReportDataSource()

            viewer.Width = 951 'set height and width to rpt
            viewer.Height = 480
            ' giving path to report
            viewer.LocalReport.ReportPath = Server.MapPath("../Reports/Report.rdlc")
            dsCustomer = Nothing
            dsCustomer = objDB.ExecProc_getDataSet("D_SP_DETAILED_DIESEL_TRACKING_REPORT", varParam)
            reportDataSource.Name = "DataSet1_D_SP_DETAILED_DIESEL_TRACKING_REPORT"
            reportDataSource.Value = dsCustomer.Tables(0)
            viewer.LocalReport.DataSources.Add(reportDataSource)
            pnlrpt.Controls.Add(viewer)


            Dim rg_params As ReportParameter() = New ReportParameter(0) {}
            rg_params(0) = New ReportParameter("frmdate", Request.QueryString("f"))
            viewer.LocalReport.SetParameters(rg_params)

            Dim rg_params1 As ReportParameter() = New ReportParameter(0) {}
            rg_params1(0) = New ReportParameter("todate", Request.QueryString("t"))
            viewer.LocalReport.SetParameters(rg_params1)
            'Dim datasource As ReportDataSource = New ReportDataSource("DataSet1_D_SP_DETAILED_DIESEL_TRACKING_REPORT", dsCustomer.Tables(0))
            '        ReportDataSource datasource = new ReportDataSource("EMSDataSet_D_SP_DETAILED_DIESEL_TRACKING_REPORT", dsCustomer.Tables[0]);
            'Dim rg_params As ReportParameter() = New ReportParameter(0) {}
            'rg_params(0) = New ReportParameter("frmdate", Request.QueryString("f"))
            ''rg_params(1) = New ReportParameter("todate", Request.QueryString("t"))
            'Me.ReportViewer1.LocalReport.SetParameters(rg_params)

            'Dim rg_params1 As ReportParameter() = New ReportParameter(0) {}
            ''rg_params1(0) = New ReportParameter("frmdate", Request.QueryString("f"))
            'rg_params1(0) = New ReportParameter("todate", Request.QueryString("t"))
            'Me.ReportViewer1.LocalReport.SetParameters(rg_params1)

            'ReportViewer1.LocalReport.DataSources.Clear()
            'ReportViewer1.LocalReport.DataSources.Add(datasource)

            If dsCustomer.Tables(0).Rows.Count > 0 Then
                pnlrpt.Visible = True
            Else
                pnlwarning.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
