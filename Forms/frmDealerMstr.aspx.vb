Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Partial Class Forms_frmCustomerMstr
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
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

        lblWarningMessage.Text = ""
        pnlimport.Visible = False
        If Not Page.IsPostBack Then
            Insert_log()
            InitializeVariables()
            BindGrid()

        End If
    End Sub

    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "D_CUSTOMER_ID"
        ViewState("SortOrder") = "ASC"
        SetSortOrder()
    End Sub

    Protected Sub SetSortOrder()
        If ViewState("SortOrder") Is Nothing Then
            ViewState("SortOrder") = "ASC"
        ElseIf ViewState("SortOrder") = "ASC" Then
            ViewState("SortOrder") = "DESC"
        Else
            ViewState("SortOrder") = "ASC"

        End If
    End Sub

    Protected Sub BindGrid()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@IntCustomerId"
        'param(0, 1) = 0 'Session("UserID")
        'dsSupplier = objDB.ExecProc_getDataSet("D_SP_GET_PETROL_PUMP_FOR_GRID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_PETROL_PUMP_FOR_GRID")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVCustomer.DataSource = dt
        GVCustomer.DataBind()
    End Sub

    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@IntCustomerId"
        'param(0, 1) = 0 'Session("UserID")
        'dsSupplier = objDB.ExecProc_getDataSet("D_SP_GET_CUSTOMER_DETAIL", param)
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_PETROL_PUMP_FOR_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVCustomer.DataSource = dt.DefaultView
        GVCustomer.DataBind()
    End Sub

    Protected Sub ClearControl()

        '  txtCustomerName.Text = ""
        txt_dealer.Text = ""
        txt_contact.Text = ""
        txt_security.Text = ""
        chkIsactive.Checked = True
        hdf_CustomerId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCustomer()
        Try
            Dim varParam As New ArrayList
            Dim USERID As Integer
            Dim customerActive As String
            Dim strOUT As String
            Dim intCustomerId As Integer = 0
            If hdf_CustomerId.Value.ToString() = "" Then
                varParam.Add("INSERT") 'P0
                varParam.Add(CInt(0)) 'P1
                'varParam.Add(intCustomerId) 'p1
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hdf_CustomerId.Value.ToString())) 'p1
            End If
            varParam.Add(ddl_circle.SelectedValue) 'P2
            varParam.Add(txt_dealer.Text.Trim) 'P3
            varParam.Add(ddl_cluster.SelectedItem.Text.Trim) 'P4
            varParam.Add(0)  'P5
            varParam.Add(txt_contact.Text.Trim) 'P6
            varParam.Add(CDbl(txt_security.Text.Trim)) 'P7

            If chkIsactive.Checked = True Then
                varParam.Add("Y") 'P8
            End If
            If chkIsactive.Checked = False Then
                varParam.Add("N") 'P8
            End If
            If Session("User_Role") = "EXECUTIVE" Then
                varParam.Add(CInt(Session("user_id"))) 'P9
            Else
                varParam.Add(CInt(Session("user_id"))) 'P9
            End If
            varParam.Add(ddl_customer.SelectedValue) 'P2
            varParam.Add(ddl_cluster.SelectedValue) 'P2

            strOUT = objDB.ExecProc_getStatus("[D_SP_PETROL_PUMP]", varParam)
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DeleteCustomer(ByVal id As Integer)
        Try
            Dim varparam(0, 1) As String
            Dim strOUT As String = ""
            varparam(0, 0) = "@intCustomerId"
            varparam(0, 1) = id
            strOUT = objDB.ExecProc_getRecordsAffected("[D_SP_DELETE_CUSTOMER_MSTR]", varparam)

            BindGrid()
            lblWarningMessage.Visible = True
            If strOUT = "1" Then
                lblWarningMessage.Text = "Record Deleted Sucussfully !!"
            Else
                lblWarningMessage.Text = "Failed To Delete Record !!"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindControls(ByVal intCustomertId As Integer)
        Dim dt As New DataTable
        Dim varParam(0, 1) As String
        varParam(0, 0) = "@intCustomerId"
        varParam(0, 1) = intCustomertId
        dt = objDB.ExecProc_getDataTable("D_SP_GET_CUSTOMER_DETAIL", varParam)
        If dt.Rows.Count > 0 Then
            'txtCustomerName.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()


            If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                chkIsactive.Checked = True
            Else
                chkIsactive.Checked = False
            End If

        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlcustomerDetail.Update()
        SaveCustomer()
        Me.UpGVCust.Update()
    End Sub

    Protected Sub GVCustomer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVCustomer.PageIndexChanging
        GVCustomer.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCustomer_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVCustomer.RowDataBound
    End Sub

    Protected Sub GVCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVCustomer.RowDeleting
        Dim hf As HiddenField = CType(GVCustomer.Rows(e.RowIndex).FindControl("hfCustomerID"), HiddenField)
        Dim CustomerId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCustomer(CustomerId)
    End Sub

    Protected Sub GVCustomer_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVCustomer.RowEditing

    End Sub

    Protected Sub GVCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVCustomer.SelectedIndexChanged
        Dim CustomerId As String
        CustomerId = GVCustomer.SelectedValue.ToString()
        hdf_CustomerId.Value = CustomerId
        Dim intCustomerId As Integer
        intCustomerId = CInt(CustomerId)
        'BindControls(intCustomerId)
        ShowModalData(intCustomerId)
        btnSubmit.Text = "Update"
    End Sub

    Protected Sub ShowModalData(ByVal id As Integer)
        Try
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@intpumpId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_PETROL_PUMP_FOR_GRID_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                BindCustomer()
                ddl_customer.SelectedValue = CStr(dt.Rows(0)("D_CUSTOMER_ID"))
                BindCircle(CStr(dt.Rows(0)("D_CUSTOMER_ID")))
                ddl_circle.SelectedValue = CStr(dt.Rows(0)("D_CIRCLE_ID"))
                txt_dealer.Text = CStr(dt.Rows(0)("D_PUMP_NAME"))
                BindLocation(CStr(dt.Rows(0)("D_CIRCLE_ID")))
                ddl_cluster.SelectedValue = CStr(dt.Rows(0)("D_LOCATION_ID"))
                txt_contact.Text = CStr(dt.Rows(0)("D_CONTACT_NO"))
                txt_security.Text = CStr(dt.Rows(0)("D_DEPOSIT"))
                'txtCustomerName.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()

                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlcustomerDetail.Update()
            mdlPopup.Show()
            'txtCustomerName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            BindCustomer()
            upPnlcustomerDetail.Update()
            mdlPopup.Show()
            ' txtCustomerName.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            Dim DRCustomer As SqlDataReader
            ddl_circle.Items.Clear()
            ddl_cluster.Items.Clear()
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddl_circle.Items.Add(New ListItem("--SELECT--", 0))
            ddl_cluster.Items.Add(New ListItem("--SELECT--", 0))
            If DRCustomer.HasRows Then
                ddl_customer.DataSource = DRCustomer
                ddl_customer.DataTextField = "D_CUSTOMER_NAME"
                ddl_customer.DataValueField = "D_CUSTOMER_ID"
                ddl_customer.DataBind()
                DRCustomer.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub GVCustomer_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVCustomer.Sorting
        Dim exp As String = e.SortExpression
        ViewState("SortExpression") = exp
        BindGrid_sort()
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClearControl()
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        ShowModalDataInsert()
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

    Protected Sub ddl_customer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_customer.SelectedIndexChanged
        Try
            mdlPopup.Show()
            BindCircle(ddl_customer.SelectedValue)
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
            ddl_circle.Items.Clear()
            ddl_cluster.Items.Clear()
            ddl_cluster.Items.Add(New ListItem("--SELECT--",0))
            If DRCircleName.HasRows Then
                ' dgSite.Visible = True
                ddl_circle.DataSource = DRCircleName
                ddl_circle.DataTextField = "D_CIRCLE_NAME"
                ddl_circle.DataValueField = "D_CIRCLE_ID"
                ddl_circle.DataBind()
                DRCircleName.Close()
            Else
                ' lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddl_circle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_circle.SelectedIndexChanged
        Try
            mdlPopup.Show()
            BindLocation()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindLocation()
        Try
            Dim DRVendor As SqlDataReader
            Dim circleParam(0, 1) As String
            ddl_cluster.Items.Clear()
            circleParam(0, 0) = "@intCircleId"
            circleParam(0, 1) = ddl_circle.SelectedValue
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            If DRVendor.HasRows Then
                ddl_cluster.DataSource = DRVendor
                ddl_cluster.DataTextField = "D_LOCATION_NAME"
                ddl_cluster.DataValueField = "D_LOCATION_ID"
                ddl_cluster.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindLocation(ByVal intCircleId As String)
        Try
            Dim DRVendor As SqlDataReader
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intCircleId"
            circleParam(0, 1) = intCircleId
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            If DRVendor.HasRows Then
                ddl_cluster.DataSource = DRVendor
                ddl_cluster.DataTextField = "D_LOCATION_NAME"
                ddl_cluster.DataValueField = "D_LOCATION_ID"
                ddl_cluster.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub BtnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImport.Click
        Dim FilePath As String
        Dim FileName As String
       
        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.fileinput.PostedFile.FileName)
            If ext = "" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                lblWarningMessage.Text = "Please Select File"
                lblWarningMessage.Visible = True
                'Page.RegisterStartupScript("Nodata", sAlert)
            ElseIf ext.ToLower <> ".csv" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select .csv File')</script>"
                ' Page.RegisterStartupScript("Nodata", sAlert)
                lblWarningMessage.Text = "Please Select .csv File"
                lblWarningMessage.Visible = True
            Else
                FilePath = Left(System.IO.Path.GetFullPath(fileinput.PostedFile.FileName), System.IO.Path.GetFullPath(fileinput.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(fileinput.PostedFile.FileName)
                If fileinput.PostedFile.FileName = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    '    Page.RegisterStartupScript("Nodata", sAlert)
                    lblWarningMessage.Text = "Please Select File"
                    lblWarningMessage.Visible = True
                    Exit Sub
                Else
                    fileinput.PostedFile.SaveAs(Server.MapPath("../DieselData/" & FileName))
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
                    lblWarningMessage.Text = "No Records found in excel sheet"
                    lblWarningMessage.Visible = True
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
                        Dim Params As New ArrayList
                        Params.Add(dtRow.Item(0).ToString.Trim) 'P0
                        Params.Add(dtRow.Item(1).ToString.Trim) 'P1
                        Params.Add(dtRow.Item(2).ToString.Trim) 'P2
                        Params.Add(dtRow.Item(3).ToString.Trim) 'P3
                        Params.Add(dtRow.Item(4).ToString.Trim) 'P4
                        Params.Add(dtRow.Item(5).ToString.Trim) 'P5
                        Params.Add(CInt(Session("USER_ID"))) 'P6

                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_DEALER_NAME", Params)
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
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Try
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/DEALER.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/DEALER.csv"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            'fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub btnsearchdealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearchdealer.Click
        Try
            Dim objClsdb As New clsDB
            Dim DRdealer As DataSet
            Dim dealerParam(0, 1) As String
            dealerParam(0, 0) = "@P1"
            dealerParam(0, 1) = txtsearch.Text
            DRdealer = objClsdb.ExecProc_getDataSet("[D_SP_GET_PETROL_PUMP_FOR_SEARCH_GRID]", dealerParam)
            GVCustomer.DataSource = DRdealer
            GVCustomer.DataBind()
            'GVCustomer.AllowSorting = Nothing
            txtsearch.Text = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class
