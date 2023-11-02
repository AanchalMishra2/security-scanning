﻿Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Partial Class Forms_frmSummaryFAFURPT
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
            varParam(2, 0) = "@intUserId"
            varParam(2, 1) = CInt(Session("USER_ID"))
            varParam(3, 0) = "@strFromDate"
            varParam(3, 1) = Request.QueryString("f")
            varParam(4, 0) = "@strToDate"
            varParam(4, 1) = Request.QueryString("t")
            
            Dim reportDataSource As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource()
            Dim viewer As ReportViewer = New ReportViewer()
            viewer.Reset()
            viewer.ShowToolBar = True
            Dim datasource As ReportDataSource = New ReportDataSource()

            'If Request.QueryString("Mode") = "D" Then
            viewer.Width = 951 'set height and width to rpt
            viewer.Height = 480
            ' giving path to report
            viewer.LocalReport.ReportPath = Server.MapPath("../Reports/ConsolidatedRPT.rdlc")
            dsCustomer = Nothing
            dsCustomer = objDB.ExecProc_getDataSet("D_SP_FUNDALLOTMENT_FUNDUTILIZATION_REPORT", varParam)
            reportDataSource.Name = "DataSet1_D_SP_FUNDALLOTMENT_FUNDUTILIZATION_REPORT"
            reportDataSource.Value = dsCustomer.Tables(0)
            viewer.LocalReport.DataSources.Add(reportDataSource)
            pnlrpt.Controls.Add(viewer)
            '///////////////////////////
            'ElseIf Request.QueryString("Mode") = "S" Then
            'viewer.Width = 951 'set height and width to rpt
            'viewer.Height = 480
            '' giving path to report
            'viewer.LocalReport.ReportPath = Server.MapPath("../Reports/VendorSummaryReport.rdlc")
            'dsCustomer = Nothing
            'dsCustomer = objDB.ExecProc_getDataSet("D_SP_VENDORSIDE_PURCHASE_SUMMARY_REPORT_NEW", varParam)
            'reportDataSource.Name = "DataSet1_D_SP_VENDORSIDE_PURCHASE_SUMMARY_REPORT_NEW"
            'reportDataSource.Value = dsCustomer.Tables(0)
            'viewer.LocalReport.DataSources.Add(reportDataSource)
            'pnlrpt.Controls.Add(viewer)
            'End If

            Dim rg_params As ReportParameter() = New ReportParameter(0) {}
            rg_params(0) = New ReportParameter("frmdate", Request.QueryString("f"))
            viewer.LocalReport.SetParameters(rg_params)

            Dim rg_params1 As ReportParameter() = New ReportParameter(0) {}
            rg_params1(0) = New ReportParameter("todate", Request.QueryString("t"))
            viewer.LocalReport.SetParameters(rg_params1)

            'ReportViewer1.LocalReport.DataSources.Clear()
            ' ReportViewer1.LocalReport.DataSources.Add(datasource)
            If dsCustomer.Tables(0).Rows.Count > 0 Then
                pnlrpt.Visible = True
            Else
                pnlwarning.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
