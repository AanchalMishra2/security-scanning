Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
Partial Class Forms_frmFundAllotmentNew
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
        lblwarning.Visible = False
        pnlimport.Visible = False
        If Page.IsPostBack = False Then
            'Bindmodeofpay()
            Insert_log()
            BindCustomer()

        End If
    End Sub
    Private Sub Bindmodeofpay()
        ddlmodeofpay.Items.Clear()
        ddlmodeofpay.Items.Add(New ListItem("--SELECT--", 0))
        ddlmodeofpay.Items.Add(New ListItem("PETRO CARDS", 1))
        ddlmodeofpay.Items.Add(New ListItem("CASH", 2))
        ddlmodeofpay.Items.Add(New ListItem("DD", 3))
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"

            custParam(0, 1) = CInt(Session("USER_ID"))
            ddlcircle.Items.Clear()
            ddlmodeofpay.Items.Clear()
            ddlvendor.Items.Clear()
            ddlpetrocardno.Items.Clear()
            ddldealer.Items.Clear()
            ddlcircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlmodeofpay.Items.Add(New ListItem("--SELECT--", 0))
            ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            ddldealer.Items.Add(New ListItem("--SELECT--", 0))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)

            If DRCustomer.HasRows Then
                ddlcustomer.DataSource = DRCustomer
                ddlcustomer.DataTextField = "D_CUSTOMER_NAME"
                ddlcustomer.DataValueField = "D_CUSTOMER_ID"
                ddlcustomer.DataBind()
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

    Protected Sub ddlcustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcustomer.SelectedIndexChanged
        Try
            ddlmodeofpay.Items.Clear()
            Bindmodeofpay()
            ddlvendor.Enabled = True
            ddlpetrocardno.Enabled = True
            ddldealer.Enabled = True
            ddlvendor.Items.Clear()
            ddlpetrocardno.Items.Clear()
            ddldealer.Items.Clear()
            ddlvendor.Items.Add(New ListItem("-SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("-SELECT--", 0))
            ddldealer.Items.Add(New ListItem("-SELECT--", 0))
            BindCircle(ddlcustomer.SelectedValue)
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
            ddlcircle.Items.Clear()
            If DRCircleName.HasRows Then
                ' dgSite.Visible = True
                ddlcircle.DataSource = DRCircleName
                ddlcircle.DataTextField = "D_CIRCLE_NAME"
                ddlcircle.DataValueField = "D_CIRCLE_ID"
                ddlcircle.DataBind()
                DRCircleName.Close()
            Else
                ' lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlcircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcircle.SelectedIndexChanged
        Try
            Bindmodeofpay()
            ddlvendor.Enabled = True
            ddlpetrocardno.Enabled = True
            ddldealer.Enabled = True
            ddlvendor.Items.Clear()
            ddlpetrocardno.Items.Clear()
            ddldealer.Items.Clear()
            ddlvendor.Items.Add(New ListItem("-SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("-SELECT--", 0))
            ddldealer.Items.Add(New ListItem("-SELECT--", 0))
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlmodeofpay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmodeofpay.SelectedIndexChanged
        Try
            reqdealer.Enabled = True
            reqpetrocardno.Enabled = True
            reqvendor.Enabled = True
            If ddlmodeofpay.SelectedValue = 1 Then ' petrocard
                ddlvendor.Enabled = True
                ddlpetrocardno.Enabled = True
                BindVendor()
                ddlvendor.SelectedValue = 0
                ddlpetrocardno.Items.Clear()
                ddldealer.Items.Clear()
                ddlpetrocardno.Items.Add(New ListItem("-SELECT--", 0))
                ddldealer.Items.Add(New ListItem("-SELECT--", 0))
                ddldealer.Enabled = False
                reqdealer.Enabled = False

            End If
            If ddlmodeofpay.SelectedValue = 3 Then 'dd
                ddlpetrocardno.Items.Clear()
                ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
                ddlvendor.Items.Clear()
                ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
                ddlpetrocardno.Enabled = False
                ddlvendor.Enabled = False
                BindDealer()
                ddldealer.Enabled = True
                reqpetrocardno.Enabled = False
                reqvendor.Enabled = False
                'BindPetrocard()
                'ddlPetrocard.SelectedValue = 0

                'ddlvendor.Enabled = False
                ' ddldealer.Enabled = True
                'Else
                '    ddldealer.Enabled = False
            End If
            If ddlmodeofpay.SelectedValue = 2 Then 'cash
                ddlpetrocardno.Items.Clear()
                ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
                ddlvendor.Items.Clear()
                ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
                ddldealer.Items.Clear()
                ddldealer.Items.Add(New ListItem("--SELECT--", 0))
                ddlvendor.Enabled = False
                ddlpetrocardno.Enabled = False
                ddldealer.Enabled = False
                reqpetrocardno.Enabled = False
                reqdealer.Enabled = False
                reqvendor.Enabled = False
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindVendor()
        Try
            Dim DRVendor As SqlDataReader
            'Dim circleParam(0, 1) As String
            'circleParam(0, 0) = "@intCircleId"
            'circleParam(0, 1) = ddlCircle.SelectedValue
            'DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_VENDOR_FOR_DROPDOWN", circleParam)
            DRVendor = objDB.getReader_Sproc_NoParam("D_SP_GET_VENDOR_FOR_DROPDOWN")
            ddlvendor.Items.Clear()
            If DRVendor.HasRows Then
                ddlvendor.DataSource = DRVendor
                ddlvendor.DataTextField = "D_VENDOR_NAME"
                ddlvendor.DataValueField = "D_VENDOR_ID"
                ddlvendor.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindDealer()
        Try
            Dim drDealer As SqlDataReader
            Dim varDealer(0, 1) As String
            varDealer(0, 0) = "@intCircleId"
            varDealer(0, 1) = ddlcircle.SelectedValue
            drDealer = objDB.ExecProc_getDataReder("D_SP_GET_PETROL_PUMP_FOR_DROPDOWN", varDealer)
            If drDealer.HasRows Then
                ddldealer.DataSource = drDealer
                ddldealer.DataTextField = "D_PUMP_NAME"
                ddldealer.DataValueField = "D_PUMP_ID"
                ddldealer.DataBind()
                drDealer.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlvendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlvendor.SelectedIndexChanged
        Try
            ddlpetrocardno.Items.Clear()
            ddldealer.Items.Clear()
            BindPetrocard()
            ddldealer.Items.Add(New ListItem("-SELECT--", 0))
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindPetrocard()
        Try
            Dim drPetroCard As SqlDataReader
            Dim varPetro(2, 1) As String
            varPetro(0, 0) = "@intcustomerId"
            varPetro(0, 1) = ddlcustomer.SelectedValue
            varPetro(1, 0) = "@intCircleID"
            varPetro(1, 1) = ddlcircle.SelectedValue
            varPetro(2, 0) = "@intVendorID"
            varPetro(2, 1) = ddlvendor.SelectedValue
            'varPetro(0, 0) = "@intCircleId"
            'varPetro(0, 1) = ddlCircle.SelectedValue
            'varPetro(1, 0) = "@intVendorId"
            'varPetro(1, 1) = ddlVendor.SelectedValue

            drPetroCard = objDB.ExecProc_getDataReder("D_SP_GET_PETRO_CARDS_FOR_DROPDOWN", varPetro)
            If drPetroCard.HasRows Then
                ddlpetrocardno.DataSource = drPetroCard
                ddlpetrocardno.DataTextField = "D_CARD_NO"
                ddlpetrocardno.DataValueField = "D_C_NO"
                ddlpetrocardno.DataBind()
                drPetroCard.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try

    End Sub

    Protected Sub ddlpetrocardno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlpetrocardno.SelectedIndexChanged
        Try
            ddldealer.Items.Clear()
            BindDealer()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try

    End Sub

    Protected Sub lnktemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnktemplate.Click
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/FUNDING.CSV")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/FUNDING.CSV"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            ' fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            lblwarning.Text = Nothing
            lblwarning.Visible = False
            Dim Params As New ArrayList
            If Page.IsValid Then
                Params.Add("Insert") 'P0
                Params.Add(ddlcircle.SelectedValue) 'P1
                Params.Add(txtdate.Text.Trim) 'P2
                Params.Add(ddlmodeofpay.SelectedItem.Text) 'P3
                If ddlmodeofpay.SelectedItem.Text.ToUpper <> "PETRO CARDS" Then
                    Params.Add(0) 'P4
                Else
                    If ddlvendor.SelectedValue = 0 Then
                        lblwarning.Visible = True
                        lblwarning.Text = "Select Vendor "
                        Exit Sub
                    Else

                        Params.Add(ddlvendor.SelectedValue) 'P4
                    End If

                End If


                If ddlmodeofpay.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                    If ddlpetrocardno.SelectedValue = 0 Then
                        lblwarning.Visible = True
                        lblwarning.Text = "Select Petrocard Number "
                        Exit Sub
                    Else
                        Params.Add(ddlpetrocardno.SelectedValue) 'P5
                        Params.Add(ddlpetrocardno.SelectedItem.Text) 'P6
                    End If


                Else
                    Params.Add("") 'P5
                    Params.Add(0) 'P6
                End If
                'If ddlmodeofpay.SelectedItem.Text.ToUpper = "CASH" Then
                '    Params.Add(0) 'P7
                '    Params.Add("") 'P8
                'End If
                'If ddlmodeofpay.SelectedItem.Text.ToUpper = "DD" Then
                '    If ddldealer.SelectedValue = 0 Then
                '        lblwarning.Visible = True
                '        lblwarning.Text = "Select Dealer "
                '        Exit Sub
                '    Else
                '        Params.Add(ddldealer.SelectedItem.Text) 'P8
                '    End If

                'End If
                'If ddlmodeofpay.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                '    If ddldealer.SelectedValue = 0 Then
                '        lblwarning.Visible = True
                '        lblwarning.Text = "Select Dealer "
                '        Exit Sub
                '    Else
                '        Params.Add(ddldealer.SelectedItem.Text) 'P8
                '    End If
                'End If
                If ddlmodeofpay.SelectedItem.Text.ToUpper = "CASH" Then
                    Params.Add(0) 'P
                    Params.Add("") 'P8
                End If
                If ddlmodeofpay.SelectedItem.Text.ToUpper = "DD" Then

                    ' Params.Add(ddldealer.SelectedValue) 'P7

                    If ddldealer.SelectedValue = 0 Then
                        lblwarning.Visible = True
                        lblwarning.Text = "Select Dealer "
                        Exit Sub
                    Else
                        Params.Add(ddldealer.SelectedValue) 'P7
                        Params.Add(ddldealer.SelectedItem.Text) 'P8
                    End If

                Else
                    If ddlmodeofpay.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                        If ddldealer.SelectedValue = 0 Then
                            lblwarning.Visible = True
                            lblwarning.Text = "Select Dealer "
                            Exit Sub
                        Else
                            Params.Add(ddldealer.SelectedValue) 'P7
                            Params.Add(ddldealer.SelectedItem.Text) 'P8
                        End If
                    End If

                End If

                Params.Add(CDbl(txtfundallotment.Text.Trim)) 'P9
                Params.Add(CInt(Session("USER_ID"))) 'P10
                Params.Add(ddlcustomer.SelectedValue) 'P11
                objDB = New clsDB
                Dim strOUT As String
                strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_FUND_ALLOTMENT", Params)
                ' Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
                'Page.RegisterStartupScript("NoAnswer", sAlert2)
                lblwarning.Visible = True
                lblwarning.Text = strOUT
                ClearControls()
            End If

        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        lblwarning.Text = Nothing
        lblwarning.Visible = False
        ClearControls()
    End Sub
    Private Sub ClearControls()
        txtdate.Text = Nothing
        txtfundallotment.Text = Nothing
        'lblwarning.Text = Nothing
        'lblwarning.Visible = False
        BindCustomer()
    End Sub

    Protected Sub import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles import.Click
        Dim FilePath As String
        Dim FileName As String

        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.upload.PostedFile.FileName)
            If ext = "" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                'Page.RegisterStartupScript("Nodata", sAlert)
                lblwarning.Visible = True
                lblwarning.Text = "Please Select File"
            ElseIf ext.ToLower <> ".csv" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select .csv File')</script>"
                'Page.RegisterStartupScript("Nodata", sAlert)
                lblwarning.Visible = True
                lblwarning.Text = "Please Select .csv File"

            Else
                FilePath = Left(System.IO.Path.GetFullPath(upload.PostedFile.FileName), System.IO.Path.GetFullPath(upload.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(upload.PostedFile.FileName)
                If upload.PostedFile.FileName.ToString = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    'Page.RegisterStartupScript("Nodata", sAlert)
                    lblwarning.Visible = True
                    lblwarning.Text = "Please Select File"
                    Exit Sub
                Else
                    upload.PostedFile.SaveAs(Server.MapPath("../DieselData/" & FileName))
                End If
                FilePath = Server.MapPath("../DieselData")
                Dim ExcelConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FilePath & ";Extended Properties=Text;")
                Dim ExcelCommand As OleDbCommand = New OleDbCommand("SELECT * FROM " & FileName, ExcelConnection)
                Dim ExcelAdapter As OleDbDataAdapter = New OleDbDataAdapter(ExcelCommand)
                ExcelConnection.Open()
                objDSSiteExcel = New DataSet
                ExcelAdapter.Fill(objDSSiteExcel)
                ExcelConnection.Close()

                If (objDSSiteExcel.Tables(0).Rows.Count) = 0 Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('No Records found in excel sheet')</script>"
                    'Page.RegisterStartupScript("Nodata", sAlert)
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

                        Dim datea As String() = (dtRow.Item(1).ToString.Trim).Split(" ")
                        i = i + 1
                        Dim Params As New ArrayList
                        Params.Add(dtRow.Item(0).ToString.Trim) 'P0
                        Params.Add(datea(0)) 'P1
                        Params.Add(dtRow.Item(2).ToString.Trim) 'P2
                        Params.Add(dtRow.Item(3).ToString.Trim) 'P3
                        Params.Add(dtRow.Item(4).ToString.Trim) 'P4
                        Params.Add(dtRow.Item(5).ToString.Trim) 'P5
                        Params.Add(CInt(Session("USER_ID"))) 'P6
                        Params.Add(dtRow.Item(6).ToString.Trim) 'P7

                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_FUNDING_DATA", Params)
                        FillError(i.ToString, "RECORD", strOUT)
                    Next
                    objDSSiteExcel.Dispose()
                    objDB = Nothing
                End If
                If (dt.Rows.Count) > 0 Then
                    dgRecords.Visible = True
                    dgRecords.DataSource = dt
                    dgRecords.DataBind()
                Else
                    dgRecords.Visible = True
                End If
            End If
            ' acc_1.SelectedIndex = "-1"
            pnlimport.Visible = True
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub FillError(ByVal RowNo As String, ByVal ColumnName As String, ByVal ErrorDesc As String)
        Try
            dr = dt.NewRow
            dr("Row No") = RowNo
            dr("Column Name") = ColumnName
            dr("Error") = ErrorDesc
            dt.Rows.Add(dr)
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
End Class
