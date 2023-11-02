Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Math
Partial Class Forms_frmEBTracking
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
        pnlimport.Visible = False
        If Page.IsPostBack = False Then
            Insert_log()
            BindCustomer()
            BindDD()
            BindMonth()
            ' BindCircle()
        End If
    End Sub
    Public Sub BindMonth()
        Try
            Dim DRmonth As SqlDataReader
            DRmonth = objDB.getReader_Sproc_NoParam("SP_GET_MONTH_LIST")
            ddlmonth.Items.Clear()
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
    Public Sub BindDD()
        Try
            ddldd.Items.Clear()
            ddldd.Items.Add(New ListItem("--SELECT--", 0))
            ddldd.Items.Add(New ListItem("DD", 1))
            ddldd.Items.Add(New ListItem("Cheque", 2))
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
            ddlLocation.Items.Clear()
            ddlSiteId.Items.Clear()
            ' ddlVendor.Items.Clear()
            'ddlPetrocards.Items.Clear()
            ' ddlSubPetroCard.Items.Clear()
            ' ddlNamePP.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
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
            ddlCircle.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlLocation.Items.Clear()
            ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtnoofoperator.Text = ""
            txtebconnecteddate.Text = ""
            txtmeterno.Text = ""
            txtcustomerno.Text = ""
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
          
            ddlLocation.Items.Clear()
            ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtnoofoperator.Text = ""
            txtebconnecteddate.Text = ""
            txtmeterno.Text = ""
            txtcustomerno.Text = ""
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
            ddlLocation.Items.Clear()
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            If DRVendor.HasRows Then
                ddlLocation.DataSource = DRVendor
                ddlLocation.DataTextField = "D_LOCATION_NAME"
                ddlLocation.DataValueField = "D_LOCATION_ID"
                ddlLocation.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Try
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            txtnoofoperator.Text = ""
            txtebconnecteddate.Text = ""
            txtmeterno.Text = ""
            txtcustomerno.Text = ""
            BindSite(ddlCustomer.SelectedValue, ddlCircle.SelectedValue, ddlLocation.SelectedItem.ToString)
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
            txtnoofoperator.Text = ""
            txtebconnecteddate.Text = ""
            txtmeterno.Text = ""
            txtcustomerno.Text = ""
            BindMonth()
            Dim varParam1(1, 1) As String
            Dim dsSite As New DataSet
            varParam1(0, 0) = "@intCircleID"
            varParam1(0, 1) = ddlCircle.SelectedValue
            varParam1(1, 0) = "@intSiteNo"
            varParam1(1, 1) = ddlSiteId.SelectedValue
            dsSite = objDB.ExecProc_getDataSet("D_SP_GET_SITENAME_EB", varParam1)
            'ddlGtlSiteId.Items.Clear()
            If dsSite.Tables(0).Rows.Count > 0 Then
                txtSiteName.Text = dsSite.Tables(0).Rows(0)(0)
                ' txtCluster.Text = dsSite.Tables(0).Rows(0)(2)
                txtnoofoperator.Text = dsSite.Tables(0).Rows(0)(3)
                txtebconnecteddate.Text = dsSite.Tables(0).Rows(0)(4)
                txtmeterno.Text = dsSite.Tables(0).Rows(0)(5)
                txtcustomerno.Text = dsSite.Tables(0).Rows(0)(6)
            Else
                txtSiteName.Text = ""
                ' txtCluster.Text = dsSite.Tables(0).Rows(0)(2)
                txtnoofoperator.Text = ""
                txtebconnecteddate.Text = ""
                txtmeterno.Text = ""
                txtcustomerno.Text = ""

            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub lnktemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnktemplate.Click
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/EB_TRACKTING.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/EB_TRACKTING.csv"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            ' fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim Params As New ArrayList

        Try
            Params.Add("Insert") 'P0
            Params.Add(ddlCircle.SelectedValue) 'P1
            Params.Add(ddlCustomer.SelectedValue) 'P2
            Params.Add(ddlLocation.SelectedItem.ToString.Trim) 'P3
            Params.Add(txtdistrict.Text.Trim) 'P4
            Params.Add(txtSiteName.Text.Trim) 'P5
            Params.Add(ddlSiteId.SelectedItem.Text.Trim) 'P6
            Params.Add(txtnoofoperator.Text.Trim) 'P7
            If txtebconnecteddate.Text.Trim = "N/A" Then
                Params.Add("")
            Else
                Params.Add(txtebconnecteddate.Text.Trim) 'P8
            End If
            Params.Add(txtmeterno.Text.Trim) 'P9
            Params.Add(txtcustomerno.Text.Trim) 'P10
            Params.Add(txtbillingunitno.Text.Trim) 'P11

            Params.Add(txtonlinebilldate.Text.Trim) 'P12
            Params.Add(ddlmonth.SelectedItem.ToString) 'P13
            Params.Add(txtbillrecdate.Text) 'P14
            Params.Add(txtfromdate.Text) 'P15

            Params.Add(txtcurrentreading.Text) 'P16
            Params.Add(txttodate.Text) 'p17
            Params.Add(txtlastreading.Text) 'p18

            Params.Add(txttotalunits.Text) 'p19
            If txttotalbillamount.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txttotalbillamount.Text.Trim)) 'P20
            End If
            Params.Add(ddldd.SelectedItem.ToString) 'p21
            Params.Add(txtbillpaymentdate.Text) 'p22
            Params.Add(CInt(Session("USER_ID"))) 'p23

            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_EB_TRACKING", Params)
            lblwarning.Visible = True
            lblwarning.Text = strOUT.ToString
            'Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            'Page.RegisterStartupScript("NoAnswer", sAlert2)
            'ClearDate()
            resetControls()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        resetControls()
    End Sub
    Protected Sub resetControls()
        ddlCustomer.Items.Clear()
        BindCustomer()
        ddlmonth.Items.Clear()
        BindMonth()
        ddldd.Items.Clear()
        BindDD()
        txtSiteName.Text = ""
        txtnoofoperator.Text = ""
        txtmeterno.Text = ""
        txtcustomerno.Text = ""
        txtebconnecteddate.Text = ""
        txtdistrict.Text = ""
        txtbillingunitno.Text = ""
        txtonlinebilldate.Text = ""
        txtbillrecdate.Text = ""
        txtfromdate.Text = ""
        txttodate.Text = ""
        txtcurrentreading.Text = ""
        txtlastreading.Text = ""
        txttotalbillamount.Text = ""
        txttotalunits.Text = ""
        txtbillpaymentdate.Text = ""
        pnlimport.Visible = False
    End Sub
    Protected Sub import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles import.Click
        lblwarning.Visible = False
        Dim FilePath As String
        Dim FileName As String

        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.fileInput.PostedFile.FileName)
            If ext = "" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                lblwarning.Text = "Please Select File"
                lblwarning.Visible = True
                'Page.RegisterStartupScript("Nodata", sAlert)
                Exit Sub
            ElseIf ext.ToLower <> ".csv" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select .csv File')</script>"
                ' Page.RegisterStartupScript("Nodata", sAlert)
                lblwarning.Text = "Please Select .csv File"
                lblwarning.Visible = True
                Exit Sub
            Else
                FilePath = Left(System.IO.Path.GetFullPath(fileInput.PostedFile.FileName), System.IO.Path.GetFullPath(fileInput.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(fileInput.PostedFile.FileName)
                If fileInput.PostedFile.FileName = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    '    Page.RegisterStartupScript("Nodata", sAlert)
                    lblwarning.Text = "Please Select File"
                    lblwarning.Visible = True
                    Exit Sub
                Else
                    fileInput.PostedFile.SaveAs(Server.MapPath("../DieselData/" & FileName))
                End If
                FilePath = Server.MapPath("../DieselData")
                Dim ExcelConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FilePath & ";Extended Properties=Text;")
                Dim ExcelCommand As OleDbCommand = New OleDbCommand("SELECT * FROM " & FileName, ExcelConnection)
                Dim ExcelAdapter As OleDbDataAdapter = New OleDbDataAdapter(ExcelCommand)
                ExcelConnection.Open()
                Dim objDSSiteExcel As New DataSet
                ExcelAdapter.Fill(objDSSiteExcel)
                ExcelConnection.Close()

                If (objDSSiteExcel.Tables(0).Rows.Count) = 0 Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('No Records found in excel sheet')</script>"
                    ' Page.RegisterStartupScript("Nodata", sAlert)
                    lblwarning.Text = "No Records found in excel sheet"
                    lblwarning.Visible = True
                    Exit Sub
                Else
                    dt = New DataTable
                    dc = New DataColumn("Row No", GetType(System.String))
                    dt.Columns.Add(dc)
                    dc = New DataColumn("Column Name", GetType(System.String))
                    dt.Columns.Add(dc)
                    dc = New DataColumn("Error", GetType(System.String))
                    dt.Columns.Add(dc)

                    Dim dtRow As DataRow
                    Dim i As Integer
                    i = 1
                    objDB = New clsDB

                    For Each dtRow In objDSSiteExcel.Tables(0).Rows
                        i = i + 1

                        Dim p8 As String() = (dtRow.Item(7).ToString.Trim).Split(" ")
                        Dim p12 As String() = (dtRow.Item(11).ToString.Trim).Split(" ")
                        Dim p14 As String() = (dtRow.Item(13).ToString.Trim).Split(" ")
                        Dim p15 As String() = (dtRow.Item(14).ToString.Trim).Split(" ")
                        Dim p17 As String() = (dtRow.Item(16).ToString.Trim).Split(" ")
                        Dim p22 As String() = (dtRow.Item(21).ToString.Trim).Split(" ")


                        Dim Params As New ArrayList
                        Params.Add("insert") 'P0
                        Params.Add(dtRow.Item(0).ToString.Trim) 'P1
                        Params.Add(dtRow.Item(1).ToString.Trim) 'P2
                        Params.Add(dtRow.Item(2).ToString.Trim) 'P3
                        Params.Add(dtRow.Item(3).ToString.Trim) 'P4
                        Params.Add(dtRow.Item(4).ToString.Trim) 'P5
                        Params.Add(dtRow.Item(5).ToString.Trim) 'P6
                        Params.Add(dtRow.Item(6).ToString.Trim) 'P7
                        Params.Add(p8(0)) 'P8
                        Params.Add(dtRow.Item(8).ToString.Trim) 'P9
                        Params.Add(dtRow.Item(9).ToString.Trim) 'P10
                        Params.Add(dtRow.Item(10).ToString.Trim) 'P11
                        Params.Add(p12(0)) 'P12
                        Params.Add(dtRow.Item(12).ToString.Trim) 'P13
                        Params.Add(p14(0)) 'P14
                        Params.Add(p15(0)) 'P15
                        Params.Add(dtRow.Item(15).ToString.Trim) 'P16
                        Params.Add(p17(0)) 'P17
                        Params.Add(dtRow.Item(17).ToString.Trim) 'P18
                        Params.Add(dtRow.Item(18).ToString.Trim) 'P19
                        Params.Add(dtRow.Item(19).ToString.Trim) 'P20
                        Params.Add(dtRow.Item(20).ToString.Trim) 'p21
                        Params.Add(p22(0)) 'p22
                        Params.Add(CInt(Session("USER_ID"))) 'p23
                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_EB_DATA", Params)
                        FillError(i.ToString, "RECORD", strOUT)
                    Next
                    ' objDSSiteExcel.Dispose()
                    objDB = Nothing
                End If
                Dim str As String = dt.Rows.Count
                If dt.Rows.Count > 0 Then
                    dgRecords.Visible = True
                    dgRecords.DataSource = dt
                    dgRecords.DataBind()
                    pnlimport.Visible = True
                Else
                    dgRecords.Visible = True
                End If
            End If

        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub FillError(ByVal RowNo As String, ByVal ColumnName As String, ByVal ErrorDesc As String)
        Try
            Dim drn As DataRow
            drn = dt.NewRow
            dr = dt.NewRow
            drn("Row No") = RowNo
            drn("Column Name") = ColumnName
            drn("Error") = ErrorDesc
            dt.Rows.Add(drn)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
End Class
