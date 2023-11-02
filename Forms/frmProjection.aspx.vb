Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.Web.UI.Control
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Partial Class Forms_frmProjection
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
        If Page.IsPostBack = False Then
            Insert_log()
            BindCustomer()
            BindMonth()
            Bindyear()
        End If
    End Sub
    Public Sub Bindyear()
        Try
            Dim DRyear As SqlDataReader
            DRyear = objDB.getReader_Sproc_NoParam("SP_GET_YEAR_LIST")
            If DRyear.HasRows Then
                ddlyear.DataSource = DRyear
                ddlyear.DataTextField = "TEXT"
                ddlyear.DataValueField = "ID"
                ddlyear.DataBind()
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
            If DRmonth.HasRows Then
                ddlmonth.DataSource = DRmonth
                ddlmonth.DataTextField = "TEXT"
                ddlmonth.DataValueField = "ID"
                ddlmonth.DataBind()
                DRmonth.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlCircle.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlcluster.Items.Clear()
            ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
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

    Protected Sub ddlCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            pnlwarning.Visible = False
            pnldata.Visible = False
            ddlcluster.Items.Clear()
            ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
            BindMonth()
            Bindyear()
            ddlCircle.Items.Clear()
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
                ' dgSite.Visible = True
                ddlCircle.DataSource = DRCircleName
                ddlCircle.DataTextField = "D_CIRCLE_NAME"
                ddlCircle.DataValueField = "D_CIRCLE_ID"
                ddlCircle.DataBind()
                DRCircleName.Close()
            Else
                ' lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    'Protected Sub btnexport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexport.Click
    '    ddlCustomer.Items.Clear()
    '    BindCustomer()
    '    ddlmonth.Items.Clear()
    '    BindMonth()
    '    ddlyear.Items.Clear()
    '    Bindyear()
    'End Sub

    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            pnlwarning.Visible = False
            pnldata.Visible = True
            BindDataGrid()

        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindDataGrid()

        Dim objDB As New clsDB
        Dim dsReport As DataSet
        Dim intRowCount As Integer
        Dim varParam(5, 1) As String

        varParam(0, 0) = "@intCustomer"
        varParam(0, 1) = ddlCustomer.SelectedValue
        varParam(1, 0) = "@intCircle"
        varParam(1, 1) = ddlCircle.SelectedValue
        varParam(2, 0) = "@cluster"
        varParam(2, 1) = ddlcluster.SelectedItem.ToString
        varParam(3, 0) = "@IntUserId"
        varParam(3, 1) = CInt(Session("USER_ID"))
        varParam(4, 0) = "@month"
        varParam(4, 1) = ddlmonth.SelectedValue
        varParam(5, 0) = "@year"
        varParam(5, 1) = ddlyear.SelectedItem.ToString
        dsReport = objDB.ExecProc_getDataSet("[D_SP_GET_PROJECTION_DATA]", varParam)
        intRowCount = dsReport.Tables(0).Rows.Count
        If intRowCount > 0 Then
            'dgUser.Visible = True
            'Button1.Visible = True
            gdvprojection.DataSource = dsReport
            gdvprojection.DataBind()

        Else
            pnlwarning.Visible = True
            lblwarning.Text = "No Records Found."
            pnldata.Visible = False
            'resetcontrols()
        End If
    End Sub

    Protected Sub ddlCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCircle.SelectedIndexChanged
        pnldata.Visible = False
        ddlcluster.Items.Clear()
        ddlcluster.Items.Add(New ListItem("--SELECT--", 0))
        BindMonth()
        Bindyear()
        BindLocation(ddlCircle.SelectedValue)
    End Sub

    Public Sub BindLocation(ByVal circle As String)
        Try
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intCircleId"
            circleParam(0, 1) = circle
            Dim drcluster As SqlDataReader
            ddlcluster.Items.Clear()
            'ddlSiteId.Items.Clear()
            'ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            'txtSiteName.Text = ""
            'txtsitetype.Text = ""

            drcluster = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN_WITH_ALL", circleParam)
            ddlcluster.Items.Clear()
            If drcluster.HasRows Then
                ddlcluster.DataSource = drcluster
                ddlcluster.DataTextField = "D_LOCATION_NAME"
                ddlcluster.DataValueField = "D_LOCATION_ID"
                ddlcluster.DataBind()
                drcluster.Close()
            Else

            End If

        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cnt As Integer = 0
        Dim gvrow As GridViewRow
        For Each gvrow In gdvprojection.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl("chk"), CheckBox)
            If CheckBox1.Checked Then

                Dim str As String = gdvprojection.Rows(cnt).Cells(0).Text.ToString ' sr no 
                Dim customer As String = gdvprojection.Rows(cnt).Cells(2).Text.ToString 'item.Cells(2).Text ' customer 
                Dim circle As String = gdvprojection.Rows(cnt).Cells(3).Text.ToString 'item.Cells(3).Text ' circle  
                Dim cluster As String = gdvprojection.Rows(cnt).Cells(4).Text.ToString 'item.Cells(4).Text ' cluster \
                Dim sitename As String = gdvprojection.Rows(cnt).Cells(5).Text.ToString 'item.Cells(5).Text ' site name
                Dim siteid As String = gdvprojection.Rows(cnt).Cells(6).Text.ToString 'item.Cells(6).Text ' site id
                Dim diesel As TextBox = DirectCast(gvrow.FindControl("txtDieselunit"), TextBox)
                Dim ebunit As TextBox = DirectCast(gvrow.FindControl("txtebunit"), TextBox)
                Dim siteno As HiddenField = DirectCast(gvrow.FindControl("D_SITE_NO"), HiddenField) 'item.Cells(9).Text ' site no
                Dim circle_id As HiddenField = DirectCast(gvrow.FindControl("D_CIRCLE_ID"), HiddenField) 'item.Cells(10).Text ' cicrcle id
                Dim customer_id As HiddenField = DirectCast(gvrow.FindControl("D_CUSTOMER_ID"), HiddenField) 'item.Cells(11).Text ' customer id
                If compulsoryvalidation(diesel.Text, "Diesel units at " & str & " row") = False Or floatvalidate(diesel.Text, "Diesel Quantity at " & str & " row") = False Then
                    lblwarning.Text = ""
                    pnlwarning.Visible = True
                    lblwarning.Text = "Diesel unit is  compalsory and Numeric"
                    Exit Sub
                End If
                If compulsoryvalidation(ebunit.Text, "EB units  at " & str & " row") = False Or floatvalidate(ebunit.Text, "EB units  at " & str & " row") = False Then
                    lblwarning.Text = ""
                    pnlwarning.Visible = True
                    lblwarning.Text = "EB unit is compalsory and Numeric"
                    Exit Sub
                End If
                inserdata(str, customer, circle, cluster, sitename, siteid, diesel.Text, ebunit.Text, siteno.Value, circle_id.Value, customer_id.Value, ddlmonth.SelectedValue.ToString, ddlyear.SelectedItem.ToString)
                cnt = cnt + 1
            End If
        Next
        If cnt = 0 Then
            lblwarning.Text = ""
            pnlwarning.Visible = True
            lblwarning.Text = "Check At least one Record."
            'show_alert("Check At least one Record.")
            '        Else
            'lblwarning.Text = ""
            'pnlwarning.Visible = True
            'lblwarning.Text = "EB unit compalsory and Numeric"
            'show_alert("Records Inserted Successfully.")
        Else
            BindDataGrid()
        End If
    End Sub
    Private Sub inserdata(ByVal str As String, ByVal customer As String, ByVal circle As String, ByVal cluster As String, ByVal sitename As String, ByVal siteid As String, ByVal diesel As String, ByVal ebunit As String, ByVal siteno As String, ByVal circle_id As String, ByVal customer_id As String, ByVal month As String, ByVal year As String)
        Try
            Dim Params As New ArrayList
            Params.Add(customer) 'CUSTOMER
            Params.Add(circle) 'CIRCLE
            Params.Add(cluster) 'CLUSTER
            Params.Add(sitename) 'SITE NAME
            Params.Add(siteid) 'D_SITE_ID
            Params.Add(diesel) 'DIESEL UNIT
            Params.Add(ebunit) 'EB UNIT
            Params.Add(siteno) 'SITE NO
            Params.Add(circle_id) 'CIRCLE ID
            Params.Add(customer_id) 'CUSTOMER ID
            Params.Add(CInt(Session("USER_ID"))) 'CUSTOMER ID
            Params.Add(month)   'month id
            Params.Add(year)   'year id
            'dt = New DataTable
            'dc = New DataColumn("Row No", GetType(System.String))
            'dt.Columns.Add(dc)
            'dc = New DataColumn("Column Name", GetType(System.String))
            'dt.Columns.Add(dc)
            'dc = New DataColumn("Error", GetType(System.String))
            'dt.Columns.Add(dc)

            '            Dim dtRow As DataRow
            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_PROJECTION_MSTR_INSERT", Params)
            Dim sAlert As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            lblwarning.Text = ""
            pnlwarning.Visible = True
            lblwarning.Text = strOUT

            'FillError(str.ToString, "RECORD", strOUT)
            'If (dt.Rows.Count) > 0 Then
            '    dgRecords.Visible = True
            '    dgRecords.DataSource = dt
            '    dgRecords.DataBind()
            'Else
            '    dgRecords.Visible = True
            'End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try

    End Sub

    Private Function floatvalidate(ByVal str As String, ByVal msg As String) As Boolean
        Try
            If Regex.IsMatch(str, "^[0-9]+(\.[0-9]+)?$") Or str = "" Then
                Return True
            Else
                ' Dim strmsg As String = msg & " must be Numeric."
                'show_alert(strmsg)
                Return False
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Function
    Private Sub dguser_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles gdvprojection.PageIndexChanged
        Try
            gdvprojection.PageIndex = e.NewPageIndex
            objDB = New clsDB
            'objDB.getDataInGrid1(dgUser, "D_SP_GET_PETROL_PUMP_FOR_GRID")
            BindDataGrid()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Function compulsoryvalidation(ByVal str As String, ByVal msg As String) As Boolean
        ' this will check required field validation
        Try
            If str = "" Then
                'Dim strmsg As String = msg & " is Mandatory."
                'show_alert(strmsg)
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Function
    Protected Sub ddlcluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcluster.SelectedIndexChanged
        BindMonth()
        Bindyear()
        lblwarning.Text = ""
        pnlwarning.Visible = False
        pnldata.Visible = False
    End Sub

    Protected Sub ddlmonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmonth.SelectedIndexChanged
        lblwarning.Text = ""
        pnlwarning.Visible = False
        pnldata.Visible = False
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlyear.SelectedIndexChanged
        lblwarning.Text = ""
        pnlwarning.Visible = False
        pnldata.Visible = False
    End Sub
End Class
