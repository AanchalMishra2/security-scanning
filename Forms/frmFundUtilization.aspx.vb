Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
Partial Class Forms_frmFundUtilization
    Inherits System.Web.UI.Page
    Public objDB As New clsDB
    Dim DRCircle As SqlDataReader
    Dim objDSSiteExcel As DataSet
    Dim dt As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            Response.Redirect("frmLogin.aspx")
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
            'BindItem()
            'BindModeOfPay()
            'BindCircle()
        End If
    End Sub
    Public Sub BindCustomer()
        Try
            Dim DRCustomer As SqlDataReader
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlcircle.Items.Clear()
            ddlvendor.Items.Clear()
            ddlmodeofpay.Items.Clear()
            ddlpetrocardno.Items.Clear()
            ddlsubpetrocard.Items.Clear()
            ddlitemparchased.Items.Clear()
            ddlcircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlmodeofpay.Items.Add(New ListItem("--SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
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
    Public Sub BindItem()
        Try
            ddlitemparchased.Items.Clear()
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("Diesel", 1))
            ddlitemparchased.Items.Add(New ListItem("Oil", 2))
            ddlitemparchased.Items.Add(New ListItem("Distilled Water", 3))
            ddlitemparchased.Items.Add(New ListItem("Coolent", 4))
            ddlitemparchased.Items.Add(New ListItem("Cotton Waste", 5))

        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindModeOfPay()
        Try
            ddlmodeofpay.Items.Clear()
            ddlmodeofpay.Items.Add("--SELECT--")
            ddlmodeofpay.Items.Add("PETRO CARDS")
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
            ddlvendor.Items.Clear()
            ddlpetrocardno.Items.Clear()
            ddlsubpetrocard.Items.Clear()
            ddlitemparchased.Items.Clear()
            ddlmodeofpay.Items.Add(New ListItem("--SELECT--", 0))
            ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
            'BindModeOfPay()
            'ddlvendor.SelectedValue = 0
            'ddlpetrocardno.Items.Clear()
            'ddlsubpetrocard.Items.Clear()
            'ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            'ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            'ddlitemparchased.Items.Clear()
            'BindItem()
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
            ddlpetrocardno.Items.Clear()
            ddlsubpetrocard.Items.Clear()
            ddlitemparchased.Items.Clear()
            ddlvendor.Items.Clear()
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
            ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
            BindModeOfPay()
            'BindVendor()
            'ddlitemparchased.SelectedIndex = 0
            'ddlpetrocardno.Items.Clear()
            'ddlsubpetrocard.Items.Clear()
            'ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            'ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindVendor()
        Try
            Dim DRVendor As SqlDataReader
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

    Protected Sub ddlmodeofpay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmodeofpay.SelectedIndexChanged
        Try
            'ddlvendor.SelectedValue = 0
            ddlpetrocardno.Items.Clear()
            ddlsubpetrocard.Items.Clear()
            ddlitemparchased.Items.Clear()
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
            BindVendor()
            'BindItem()

            'ddlitemparchased.SelectedIndex = 0
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlvendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlvendor.SelectedIndexChanged
        Try
            BindPetrocard()
            ddlpetrocardno.SelectedValue = 0
            ddlsubpetrocard.Items.Clear()
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            'BindSubPetroCards()
            'ddlSubPetroCard.SelectedValue = 0
            ddlitemparchased.SelectedIndex = 0
            If ddlvendor.SelectedItem.Text.ToUpper = "BPCL" Then
                ddlsubpetrocard.Enabled = True
            End If
            If ddlvendor.SelectedItem.Text.ToUpper = "IOCL" Then
                ddlsubpetrocard.Enabled = True
            End If
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
            ddlpetrocardno.Items.Clear()
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
            ddlsubpetrocard.Items.Clear()
            ddlitemparchased.Items.Clear()
            ddlsubpetrocard.Items.Add(New ListItem("--SELECT--", 0))
            ddlitemparchased.Items.Add(New ListItem("--SELECT--", 0))
            'ddlitemparchased.SelectedIndex = 0
            BindSubPetroCards()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSubPetroCards()
        Try
            Dim arrParam(3, 1) As String
            Dim DRPetroCard As SqlDataReader
            arrParam(0, 0) = "@intcustomerId"
            arrParam(0, 1) = ddlcustomer.SelectedValue
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = ddlcircle.SelectedValue
            arrParam(2, 0) = "@intVendorID"
            arrParam(2, 1) = ddlvendor.SelectedValue
            arrParam(3, 0) = "@strPetrocardNo"
            arrParam(3, 1) = ddlpetrocardno.SelectedItem.Text
            ddlsubpetrocard.Items.Clear()
            DRPetroCard = objDB.ExecProc_getDataReder("D_SP_GET_SUBPETRO_CARDS_FOR_DROPDOWN_F_UTILIZATION", arrParam)
            If DRPetroCard.HasRows Then
                ddlsubpetrocard.DataSource = DRPetroCard
                ddlsubpetrocard.DataTextField = "D_S_PETROCARD_NO"
                ddlsubpetrocard.DataValueField = "D_S_PETRO_SR_NO"
                ddlsubpetrocard.DataBind()
                DRPetroCard.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub lnktemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnktemplate.Click
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/UTILIZATION.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/UTILIZATION.csv"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            ' fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim Params As New ArrayList
        Try
            Params.Add("Insert") 'P0
            Params.Add(ddlcircle.SelectedValue) 'P1
            Params.Add(txtdate.Text.Trim) 'P2
            Params.Add(ddlmodeofpay.SelectedItem.Text) 'P3
            Params.Add(ddlvendor.SelectedValue) 'P4
            If ddlmodeofpay.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                Params.Add(ddlpetrocardno.SelectedValue) 'P5
                Params.Add(ddlpetrocardno.SelectedItem.Text) 'P6
            End If
            Params.Add(ddlitemparchased.SelectedItem.Text) 'P7
            Params.Add(CDbl(0)) 'P8
            Params.Add(CDbl(0)) 'P9
            Params.Add(CDbl(txttotalamount.Text.Trim)) 'P10
            Params.Add(CInt(Session("USER_ID"))) 'P11
            If ddlvendor.SelectedItem.Text.ToUpper = "BPCL" Then
                Params.Add(ddlsubpetrocard.SelectedItem.Text) 'P12
            Else
                ''Params.Add("")
                Params.Add(ddlsubpetrocard.SelectedItem.Text) 'P12
            End If
            Params.Add(ddlcustomer.SelectedValue.ToString) 'P13

            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_FUND_UTILIZATION_MSTR", Params)
            'Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            'Page.RegisterStartupScript("NoAnswer", sAlert2)
            lblwarning.Visible = True
            lblwarning.Text = strOUT
            ClearControls()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Try
            lblwarning.Text = Nothing
            lblwarning.Enabled = False
            ClearControls()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub ClearControls()
        Try
            BindCustomer()
            'ddlcustomer.SelectedValue = 0
            'BindCircle()
            'ddlcircle.SelectedValue = 0
            txtdate.Text = Nothing
            'ddlmodeofpay.SelectedIndex = 0
            'BindVendor()
            'ddlvendor.SelectedValue = 0
            'BindPetrocard()
            'ddlpetrocardno.SelectedValue = 0
            'BindSubPetroCards()
            ' ddlSubPetroCard.Enabled = False
            'ddlsubpetrocard.SelectedValue = 0
            'ddlitemparchased.SelectedIndex = 0
            txttotalamount.Text = ""
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlsubpetrocard_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsubpetrocard.SelectedIndexChanged
        Try
            BindItem()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles import.Click
        Dim FilePath As String
        Dim FileName As String

        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.fileInput.PostedFile.FileName)
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
                FilePath = Left(System.IO.Path.GetFullPath(fileInput.PostedFile.FileName), System.IO.Path.GetFullPath(fileInput.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(fileInput.PostedFile.FileName)
                If fileInput.PostedFile.FileName = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    'Page.RegisterStartupScript("Nodata", sAlert)
                    lblwarning.Visible = True
                    lblwarning.Text = "Please Select File"
                    Exit Sub
                Else
                    fileInput.PostedFile.SaveAs(Server.MapPath("../DieselData/" & FileName))
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
                    lblwarning.Visible = True
                    lblwarning.Text = "No Records found in excel sheet"
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
                        Params.Add(dtRow.Item(6).ToString.Trim) 'P6
                        Params.Add(CInt(Session("USER_ID"))) 'P7
                        Params.Add(dtRow.Item(7).ToString.Trim) 'P8

                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_UTILIZATION_DATA", Params)
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
