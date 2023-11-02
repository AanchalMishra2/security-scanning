Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Math
Partial Class Forms_frmSiteMonthEnergyConsumption
    Inherits System.Web.UI.Page
    Public objDB As New clsDB
    Dim DRCircle As SqlDataReader
    Dim objDSSiteExcel As DataSet
    Dim dt As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
    Dim DRCustomer As SqlDataReader
    Public fun As New Functions
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
        lblwarning.Text = ""
        'pnlimport.Visible = False
        If Page.IsPostBack = False Then
            Insert_log()
            BindCustomer()
            BindMobileDg()
            BindMonth()
            Bindyear()
            'BindDD()
            'BindMonth()
            ' BindCircle()
        End If
    End Sub
    Public Sub Bindyear()
        Try
            Dim DRyear As SqlDataReader
            DRyear = objDB.getReader_Sproc_NoParam("SP_GET_YEAR_LIST")
            ddlselectionyear.Items.Clear()
            If DRyear.HasRows Then
                ddlselectionyear.DataSource = DRyear
                ddlselectionyear.DataTextField = "TEXT"
                ddlselectionyear.DataValueField = "ID"
                ddlselectionyear.DataBind()
                DRyear.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindMonth()
        Try
            Dim DRmonth As SqlDataReader
            DRmonth = objDB.getReader_Sproc_NoParam("SP_GET_MONTH_LIST")
            ddlselectionmonth.Items.Clear()
            If DRmonth.HasRows Then
                ddlselectionmonth.DataSource = DRmonth
                ddlselectionmonth.DataTextField = "TEXT"
                ddlselectionmonth.DataValueField = "ID"
                ddlselectionmonth.DataBind()
                DRmonth.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindMobileDg()
        Try
            ddlmobiledg.Items.Add(New ListItem("--SELECT--", 0))
            ddlmobiledg.Items.Add(New ListItem("YES", 1))
            ddlmobiledg.Items.Add(New ListItem("NO", 2))
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
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Dim err As Exception = Server.GetLastError
        Dim url As String = "ErrorPage.aspx?page=" & Replace(Page.ToString.Trim, "ASP.", " ") & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        Response.Redirect(url)
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlCircle.Items.Clear()
            ddlcluster.Items.Clear()
            ddlSiteId.Items.Clear()
            ' ddlVendor.Items.Clear()
            'ddlPetrocards.Items.Clear()
            ' ddlSubPetroCard.Items.Clear()
            ' ddlNamePP.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            ' ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            'ddlPetrocards.Items.Add(New ListItem("--SELECT--", 0))
            ' ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            'ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            If DRCustomer.HasRows Then
                ddlCustomer.DataSource = DRCustomer
                ddlCustomer.DataTextField = "D_CUSTOMER_NAME"
                ddlCustomer.DataValueField = "D_CUSTOMER_ID"
                ddlCustomer.DataBind()
                DRCustomer.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            ddlCircle.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlcluster.Items.Clear()
            ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtsitetype.Text = ""
            'txtnoofoperator.Text = ""
            'txtebconnecteddate.Text = ""
            'txtmeterno.Text = ""
            'txtcustomerno.Text = ""
            BindCircle(ddlCustomer.SelectedValue)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindCircle(ByVal customer_id As String)
        Try
            Dim objClsdb As New clsDB
            Dim DRCircleName As SqlDataReader
            Dim circleParam(1, 1) As String
            circleParam(0, 0) = "@intCustomerd"
            circleParam(0, 1) = customer_id
            circleParam(1, 0) = "@intUserId"
            circleParam(1, 1) = CInt(Session("USER_ID"))
            DRCircleName = objClsdb.ExecProc_getDataReder("D_SP_GET_CIRCLE_WITH_RESPECT_TO_CUSTOMER_WITHOUT_ALL", circleParam)
            ddlCircle.Items.Clear()

            If DRCircleName.HasRows Then
                'dgSite.Visible = True
                ddlCircle.DataSource = DRCircleName
                ddlCircle.DataTextField = "D_CIRCLE_NAME"
                ddlCircle.DataValueField = "D_CIRCLE_ID"
                ddlCircle.DataBind()
                DRCircleName.Close()
            Else
                'lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCircle.SelectedIndexChanged
        Try

            ddlcluster.Items.Clear()
            ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtsitetype.Text = ""
            BindLocation(ddlCircle.SelectedValue)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindLocation(ByVal circle As String)
        Try
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intCircleId"
            circleParam(0, 1) = circle
            Dim DRVendor As SqlDataReader
            ddlcluster.Items.Clear()
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            If DRVendor.HasRows Then
                ddlcluster.DataSource = DRVendor
                ddlcluster.DataTextField = "D_LOCATION_NAME"
                ddlcluster.DataValueField = "D_LOCATION_ID"
                ddlcluster.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlcluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcluster.SelectedIndexChanged
        Try
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtsitetype.Text = ""
            BindSite(ddlCustomer.SelectedValue, ddlCircle.SelectedValue, ddlcluster.SelectedItem.ToString)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSite(ByVal custid As String, ByVal circleid As String, ByVal cluster As String)
        Try
            Dim arrParam(2, 1) As String
            Dim DRSite As SqlDataReader
            arrParam(0, 0) = "@intCustomerID"
            arrParam(0, 1) = custid
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = circleid
            arrParam(2, 0) = "@cluster"
            arrParam(2, 1) = cluster

            DRSite = objDB.ExecProc_getDataReder("D_SP_GET_SITEID", arrParam)
            ddlSiteId.Items.Clear()
            If DRSite.HasRows Then
                ddlSiteId.DataSource = DRSite
                ddlSiteId.DataTextField = "D_SITE_ID"
                ddlSiteId.DataValueField = "D_SITE_NO"
                ddlSiteId.DataBind()
                DRSite.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlSiteId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSiteId.SelectedIndexChanged
        Try
            txtSiteName.Text = ""
            txtsitetype.Text = ""
            BindSiteDetails(ddlCustomer.SelectedValue, ddlCircle.SelectedValue, ddlSiteId.SelectedValue)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSiteDetails(ByVal custid As String, ByVal circleid As String, ByVal siteid As String)
        Try
            Dim varParam1(2, 1) As String
            Dim dsSite As New DataSet
            varParam1(0, 0) = "@intCustomerID"
            varParam1(0, 1) = custid
            varParam1(1, 0) = "@intCircleID"
            varParam1(1, 1) = circleid
            varParam1(2, 0) = "@strSiteID"
            varParam1(2, 1) = siteid
            dsSite = objDB.ExecProc_getDataSet("D_SP_GET_SITEDETAILS", varParam1)
            If dsSite.Tables(0).Rows.Count > 0 Then
                txtSiteName.Text = dsSite.Tables(0).Rows(0)(1)
                txtsitetype.Text = dsSite.Tables(0).Rows(0)(5)
                txtSiteName.Enabled = False
                txtsitetype.Enabled = False
                ' txtClusterName.Text = dsSite.Tables(0).Rows(0)(2)
            Else
                txtSiteName.Text = ""
                txtsitetype.Text = ""
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        lblwarning.Visible = False
        lblwarning.Text = ""
        If ddlmobiledg.SelectedValue = 1 Then
            If compulsoryvalidation(txtdateofdeployment.Text.ToString, "Date Of Deployment") = False Then
                Exit Sub
            End If
            If compulsoryvalidation(txtdateofdemobilization.Text.ToString, "Date Of De-Mobilization") = False Then
                Exit Sub
            End If
            If compulsoryvalidation(txttotalmobiledgrunhour.Text.ToString, "Total Mobile DG Run Hour") = False Or floatvalidate(txttotalmobiledgrunhour.Text.ToString, "Total Mobile DG Run Hour") = False Then
                Exit Sub
            End If
            If compulsoryvalidation(txtdcmeter1.Text.ToString, "DC Meter 1") = False Or floatvalidate(txtdcmeter1.Text.ToString, "DC Meter 1") = False Then
                Exit Sub
            End If
            If compulsoryvalidation(txtdcmeter2.Text.ToString, "DC Meter 2") = False Or floatvalidate(txtdcmeter2.Text.ToString, "DC Meter 2") = False Then
                Exit Sub
            End If
            If compulsoryvalidation(txtdcmeter3.Text.ToString, "DC Meter 3") = False Or floatvalidate(txtdcmeter3.Text.ToString, "DC Meter 3") = False Then
                Exit Sub
            End If
            insertData()
        End If
    End Sub
    Private Sub insertData()

        Dim Params As New ArrayList
        Try
            Params.Add("") 'D_S_NO()
            Params.Add(ddlCustomer.SelectedValue) 'D_CUSTOMER_ID()
            Params.Add(ddlCircle.SelectedValue) 'D_CIRCLE_ID()
            Params.Add(ddlcluster.SelectedValue) 'D_CLUSTER_ID()
            Params.Add(ddlSiteId.SelectedValue) 'D_SITE_ID()
            Params.Add(txtSiteName.Text.ToString.Trim) 'D_SITE_NAME()
            Params.Add(txtsitetype.Text.ToString.Trim) 'D_SITE_TYPE()
            Params.Add(ddlselectionmonth.SelectedItem.ToString.Trim) 'D_SELECTION_MONTH()
            Params.Add(ddlselectionyear.SelectedItem.ToString.Trim) 'D_SELECTION_YEAR()
            Params.Add(txtEbopeningunit.Text.ToString.Trim) 'D_EB_OPENING_UNIT()
            Params.Add(txtEBclosingunit.Text.ToString.Trim) 'D_EB_CLOSING_UNIT()
            Params.Add(txttotalconsumeunit.Text.ToString.Trim) 'D_TOTAL_CONSUME_UNIT()
            Params.Add(txtebrate.Text.ToString.Trim) 'D_EB_RATE()
            Params.Add(txttotalebcost.Text.ToString.Trim) 'D_EB_TOTAL_COST()
            Params.Add(txtdgopeningunit.Text.ToString.Trim) 'D_DG_OPENING_UNIT()
            Params.Add(txtdgclosingunit.Text.ToString.Trim) 'D_DG_CLOSING_UNIT()
            Params.Add(txttotaldgconsumeunit.Text.ToString.Trim) 'D_TOTAL_DG_CONSUME_UNIT()
            Params.Add(txtdgrunhourclosing.Text.ToString.Trim) 'D_DG_RUN_HOUR_OPENING()
            Params.Add(txtdgrunhourclosing.Text.ToString.Trim) 'D_DG_RUN_HOUR_CLOSING()
            Params.Add("") 'D_TOTAL_RUN_HOUR_CLOSING()
            Params.Add(txttotaldgrunhour.Text.ToString.Trim) 'D_TOTAL_DG_RUN_HOUR()
            Params.Add(txtdgtankopeningstock.Text.ToString.Trim) 'D_DG_TANK_OPENING_STOCK()
            Params.Add(txtdgfilledformonth.Text.ToString.Trim) 'D_DG_FILLED_FOR_MONTH()
            Params.Add(txttotaltankclosingstock.Text.ToString.Trim) 'D_DG_TANK_CLOSING_STOCK()
            Params.Add(txttotaldgconsumeformonth.Text.ToString.Trim) 'D_TOTAL_DG_CONSUME_FOR_MONTH()
            Params.Add(txtrateofdiesel.Text.ToString.Trim) 'D_RATE_OF_DIESEL()
            Params.Add(txttotaldieselcost.Text.ToString.Trim) 'D_TOTAL_DIESEL_COST()
            Params.Add(txtcphfordg.Text.ToString.Trim) 'D_CPH_FOR_DG()
            Params.Add(ddlmobiledg.SelectedItem.ToString.Trim) 'D_MOBILE_DG()
            Params.Add(txtdateofdeployment.Text.ToString.Trim) 'D_DATE_OF_DEPLOYMENT()
            Params.Add(txtdateofdemobilization.Text.ToString.Trim) 'D_DATE_OF_DEMOBILIZATION()
            Params.Add(txttotalmobiledgrunhour.Text.ToString.Trim) 'D_TOTAL_MOBILE_DG_RUN_HOUR()
            Params.Add(txtdcmeter1.Text.ToString.Trim) 'D_DC_METER_1()
            Params.Add(txtdcmeter2.Text.ToString.Trim) 'D_DC_METER_2()
            Params.Add(txtdcmeter3.Text.ToString.Trim) 'D_DC_METER_3()
            Params.Add(txtremarks.Text.ToString.Trim) 'D_REMARKS()
            Params.Add(CInt(Session("USER_ID"))) 'D_UPDATED_BY()
            Params.Add("") 'D_UPDATED_ON()
            Params.Add(CInt(Session("USER_ID"))) 'D_USER_ID()
            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_SITE_MONTH_ENERGY_CONSUMPTION", Params)
            'Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            'Page.RegisterStartupScript("NoAnswer", sAlert2)
            lblwarning.Text = strOUT
            lblwarning.Visible = True
            If strOUT = "Record Inserted Successfully!" Then
                ' btnSave.Enabled = False
                resetcontrols()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub resetcontrols()
        BindCustomer()
        BindMonth()
        Bindyear()
        ddlmobiledg.Items.Clear()
        BindMobileDg()
        txtcphfordg.Text = ""
        txtdateofdemobilization.Text = ""
        txtdateofdeployment.Text = ""
        txtdcmeter1.Text = ""
        txtdcmeter2.Text = ""
        txtdcmeter3.Text = ""
        txtdgclosingunit.Text = ""
        txtdgfilledformonth.Text = ""
        txtdgopeningunit.Text = ""
        txtdgrunhourclosing.Text = ""
        txtdgrunhouropning.Text = ""
        txtdgtankopeningstock.Text = ""
        txtEBclosingunit.Text = ""
        txtEbopeningunit.Text = ""
        txtebrate.Text = ""
        txtrateofdiesel.Text = ""
        txttotalebcost.Text = ""
        txtremarks.Text = ""
        txtSiteName.Text = ""
        txtsitetype.Text = ""
        txttotalconsumeunit.Text = ""
        txttotaldgconsumeformonth.Text = ""
        txttotaldgconsumeunit.Text = ""
        txttotaldgrunhour.Text = ""
        txttotaldieselcost.Text = ""
        txttotalebcost.Text = ""
        txttotalmobiledgrunhour.Text = ""
        txttotaltankclosingstock.Text = ""

    End Sub
    Private Sub show_alert(ByVal msg As String)

        Try
            lblwarning.Text = msg
            lblwarning.Visible = True
            ' Page.RegisterStartupScript("NoAnswer", "<script> alert('" & msg & "') </script>")
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Function floatvalidate(ByVal str As String, ByVal msg As String) As Boolean
        ' Try
        'this will validate float and int valuess
        ' [0-9]+(\.[0-9]+)?$
        '"^([.]|[0-9])[0-9]*[.]*[0-9]+$"
        If Regex.IsMatch(str, "^[0-9]+(\.[0-9]+)?$") Then
            Return True
        Else
            Dim strmsg As String = msg & " must be Float."
            show_alert(strmsg)
            Return False
        End If
        'Catch ex As Exception
        '    fun.catch_Exception(ex, Page.ToString.Trim)
        'End Try
    End Function
    Private Function compulsoryvalidation(ByVal str As String, ByVal msg As String) As Boolean
        ' this will check required field validation
        'Try
        If str = "" Then
            Dim strmsg As String = msg & " is Mandatory."
            show_alert(strmsg)
            Return False
        Else
            Return True
        End If
        'Catch ex As Exception
        '    Functions.catch_Exception(ex, Page.ToString.Trim)
        'End Try
    End Function
    Private Function dropdownvalidation(ByVal str As String, ByVal msg As String) As Boolean
        ' Try
        If str = "0" Then
            Dim strmsg As String = "Please Select a " & msg
            show_alert(strmsg)
            Return False
        Else
            Return True
        End If
        'Catch ex As Exception
        '    ' Functions.catch_Exception(ex, Page.ToString.Trim)
        'End Try
    End Function

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        lblwarning.Text = ""
        lblwarning.Visible = False
        ddlCustomer.Items.Clear()
        BindCustomer()
        ddlselectionmonth.Items.Clear()
        BindMonth()
        ddlselectionyear.Items.Clear()
        Bindyear()
        ddlmobiledg.Items.Clear()
        BindMobileDg()
        txtSiteName.Text = ""
        txtsitetype.Text = ""
        txtEbopeningunit.Text = ""
        txtEBclosingunit.Text = ""
        txtebrate.Text = ""
        txttotalebcost.Text = ""
        txtdgopeningunit.Text = ""
        txtdgclosingunit.Text = ""
        txttotaldgconsumeunit.Text = ""
        txtdgrunhouropning.Text = ""
        txtdgrunhourclosing.Text = ""
        txttotaldgrunhour.Text = ""
        txtdgtankopeningstock.Text = ""
        txtdgfilledformonth.Text = ""
        txttotaltankclosingstock.Text = ""
        txttotaldgconsumeformonth.Text = ""
        txtrateofdiesel.Text = ""
        txttotaldieselcost.Text = ""
        txtcphfordg.Text = ""
        txtdateofdeployment.Text = ""
        txtdateofdemobilization.Text = ""
        txttotalmobiledgrunhour.Text = ""
        txtdcmeter1.Text = ""
        txtdcmeter2.Text = ""
        txtdcmeter3.Text = ""
        txtremarks.Text = ""
        txttotalconsumeunit.Text = ""
    End Sub

    Protected Sub ddlmobiledg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmobiledg.SelectedIndexChanged
        If ddlmobiledg.SelectedValue = 2 Then
            reqdateofdeployment.Enabled = False
            reqdateofdemobilization.Enabled = False
            reqtotalmobiledgrunhour.Enabled = False
            reqdcmeter1.Enabled = False
            reqdcmeter2.Enabled = False
            reqdcmeter3.Enabled = False
        End If
    End Sub
End Class
