Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions

Partial Class Forms_frmSiteMstr1
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Dim objDSSiteExcel As DataSet
    Dim dt As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
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
        lblWarningMessage.Text = ""
        pnlimport.Visible = False
        If Not Page.IsPostBack Then
            Insert_log()
            InitializeVariables()
            BindGrid()

        End If
    End Sub

    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "D_SITE_NO"
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
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        'dsSupplier = objDB.getDataSet_SProc("D_SP_GET_CIRCLE_FOR_GRID", param)
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_SITE_LIST_GRID")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVSite.DataSource = dt
        GVSite.DataBind()
    End Sub

    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_SITE_LIST_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVSite.DataSource = dt.DefaultView
        GVSite.DataBind()
    End Sub

    Protected Sub ClearControl()

        txtBillingCycle.Text = ""
        txtCustomerNo.Text = ""
        txtDGMake.Text = ""
        txtEBConnectionDT.Text = ""
        txtEBDeposit.Text = ""
        txtMeterNo.Text = ""
        txtNoOperator.Text = ""
        txtSiteId.Text = ""
        txtSiteName.Text = ""
        ddlCluster.Items.Clear()
        ddlCluster.Items.Add(New ListItem("--SELECT--", 0))
        'txtCircleName.Text = ""
        'txtCircleCode.Text = ""
        ' chkIsactive.Checked = True
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCircle()
        Dim Params As New ArrayList
        Try
            If ddlEBconnection.SelectedItem.Text = "NO" Then
                If ddlDGSet.SelectedItem.Text = "N/A" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('Please select a DG Set.') </script>")
                    Exit Sub
                End If
                If txtDGMake.Text = "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('DG Capacity can not be blank.') </script>")
                    Exit Sub
                End If

                If ddlDGSet.SelectedItem.Text = "YES" Then
                    If txtDGMake.Text = "" Then
                        'Page.RegisterStartupScript("NoAnswer", "<script> alert('If DG Set is YES then DG Make and Capacity can not be blank.') </script>")
                        Exit Sub
                    End If
                End If
                If txtMeterNo.Text <> "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('Meter Number must be  blank if EB Connection is NO.') </script>")
                    Exit Sub
                End If
                If txtCustomerNo.Text <> "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('Customer Number must be blank if EB Connection is NO.') </script>")
                    Exit Sub
                End If
                If txtEBDeposit.Text <> "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('EB Deposit must be  blank if EB Connection is NO.') </script>")
                    Exit Sub
                End If
                If txtBillingCycle.Text <> "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('Billing Cycle must be  blank if EB Connection is NO.') </script>")
                    Exit Sub
                End If
                If txtEBConnectionDT.Text <> "" Then
                    ' Page.RegisterStartupScript("NoAnswer", "<script> alert('EB connection Date must be  blank if EB Connection is NO.') </script>")
                    Exit Sub
                End If
            End If



            If hdf_CircleId.Value.ToString() = "" Then
                Params.Add("INSERT") 'P0
                Params.Add(CInt("0")) 'P1
            Else
                Params.Add("UPDATE") 'P0
                Params.Add(CInt(hdf_CircleId.Value.ToString())) 'P1
            End If

            Params.Add(ddlcircle.SelectedValue) 'P2
            Params.Add(txtSiteId.Text) 'P3
            Params.Add(txtSiteName.Text) 'P4
            Params.Add(ddlSiteType.SelectedItem.Text) 'P5
            Params.Add(CInt(txtNoOperator.Text.Trim)) 'p6
            Params.Add(ddlEBconnection.SelectedItem.Text) 'P7
            Params.Add(ddlDGSet.SelectedItem.Text) 'P8
            Params.Add(txtDGMake.Text.Trim) 'P9
            If (chkActive.Checked = True) Then
                Params.Add("Y") 'P10
            End If
            If (chkActive.Checked = False) Then
                Params.Add("N") 'P10
            End If
            If Session("User_Role") = "EXECUTIVE" Then
                Params.Add(CInt(Session("user_id"))) 'P11
            Else
                Params.Add(CInt(Session("user_id"))) 'P11
            End If
            Params.Add(ddlCluster.SelectedItem.ToString.Trim) 'P12
            Params.Add(txtMeterNo.Text.Trim) 'P13
            Params.Add(txtCustomerNo.Text.Trim) 'P14
            If txtEBDeposit.Text.Trim = "" Then
                Params.Add(0)
            Else
                Params.Add(CDbl(txtEBDeposit.Text.Trim)) 'P15
            End If
            Params.Add(txtBillingCycle.Text.Trim) 'P16
            Params.Add(txtEBConnectionDT.Text.Trim) 'P17
            Params.Add(ddlcustomer.SelectedValue) 'P18
            Params.Add(ddlCluster.SelectedValue) 'P19
            Params.Add(ddlSiteType.SelectedValue) 'P20
            Params.Add(ddlEBconnection.SelectedValue) 'P21
            Params.Add(ddlDGSet.SelectedValue) 'P22
            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_SITE", Params)
            Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DeleteCircle(ByVal id As Integer)
        Try
            Dim varparam(0, 1) As String
            Dim strOUT As String = ""
            varparam(0, 0) = "@intCircleId"
            varparam(0, 1) = id
            strOUT = objDB.ExecProc_getRecordsAffected("[D_SP_DELETE_CIRCLE_MSTR]", varparam)

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

    Protected Sub BindControls(ByVal intCircletId As Integer)
        Dim dt As New DataTable
        Dim varParam(0, 1) As String
        varParam(0, 0) = "@intCircleId"
        varParam(0, 1) = intCircletId
        dt = objDB.ExecProc_getDataTable("[D_SP_GET_CIRCLE_FOR_GRID_DTLS]", varParam)
        If dt.Rows.Count > 0 Then
            'txtCircleName.Text = dt.Rows(0)("D_CIRCLE_NAME").ToString()
            ' txtCircleCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
            If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If

        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlCircleDetail.Update()
        SaveCircle()
        Me.UpGVCircle.Update()
    End Sub

    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVSite.PageIndexChanging
        GVSite.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCircle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVSite.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lkbtn As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)
        '    lkbtn.Attributes.Add("onclick", "showConfirm('" & GVCircle.DataKeys(e.Row.RowIndex).Value & "'); return false; )")
        'End If
    End Sub

    Protected Sub GVCircle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVSite.RowDeleting
        Dim hf As HiddenField = CType(GVSite.Rows(e.RowIndex).FindControl("hfCircleID"), HiddenField)
        Dim CircleId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCircle(CircleId)
    End Sub

    Protected Sub GVCircle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVSite.RowEditing

    End Sub
    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVSite.SelectedIndexChanged
        Dim CircleId As String
        CircleId = GVSite.SelectedValue.ToString()

        hdf_CircleId.Value = CircleId
        Dim intCircleId As Integer
        intCircleId = CInt(CircleId)
        'BindControls(intCircleId)
        ShowModalData(intCircleId)
        btnSubmit.Text = "Update"
    End Sub

    Protected Sub ShowModalData(ByVal id As Integer)
        Try
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@intSiteNo"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_SITE_LIST_GRID_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                BindDDL()
                BindCustomer()
                ddlcustomer.SelectedValue = dt.Rows(0)("D_CUSTOMER_ID").ToString()
                BindCircle(dt.Rows(0)("D_CUSTOMER_ID").ToString())
                ddlcircle.SelectedValue = dt.Rows(0)("D_CIRCLE_ID").ToString()
                BindLocation(dt.Rows(0)("D_CIRCLE_ID").ToString())
                ddlCluster.SelectedValue = dt.Rows(0)("D_CLUSTER_ID").ToString()
                txtSiteId.Text = dt.Rows(0)("D_SITE_ID").ToString()
                txtSiteName.Text = dt.Rows(0)("D_SITE_NAME").ToString()
                ddlSiteType.SelectedValue = dt.Rows(0)("D_SITE_TYPE_ID").ToString()
                txtNoOperator.Text = dt.Rows(0)("D_NO_OF_OPERATOR").ToString()
                ddlEBconnection.SelectedValue = dt.Rows(0)("D_EB_CONN_STATUS_ID").ToString()
                ddlDGSet.SelectedValue = dt.Rows(0)("D_DG_SET_ID").ToString()
                txtDGMake.Text = dt.Rows(0)("D_DG_CAPACITY").ToString()
                txtMeterNo.Text = dt.Rows(0)("D_DG_CAPACITY").ToString()
                txtCustomerNo.Text = dt.Rows(0)("D_CUSTOMER_NO").ToString()
                txtBillingCycle.Text = dt.Rows(0)("D_BILLING_CYCLE").ToString()
                txtEBDeposit.Text = dt.Rows(0)("D_EB_DEPOSIT").ToString()
                txtEBConnectionDT.Text = dt.Rows(0)("D_EB_CONNECTION_DATE").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

            End If
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            'txtCircleName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            BindCustomer()
            BindDDL()
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            'txtCircleName.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindCustomer()
        Try
            Dim DRCustomer As SqlDataReader
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlcircle.Items.Clear()
            ddlcircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlCluster.Items.Clear()
            ddlCluster.Items.Add(New ListItem("--SELECT--", 0))
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
                'dgSite.Visible = True
                ddlcircle.DataSource = DRCircleName
                ddlcircle.DataTextField = "D_CIRCLE_NAME"
                ddlcircle.DataValueField = "D_CIRCLE_ID"
                ddlcircle.DataBind()
                DRCircleName.Close()
            Else
                'lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindDDL()
        Try

            '' don't change this part 
            ddlSiteType.Items.Clear()
            ddlEBconnection.Items.Clear()
            ddlDGSet.Items.Clear()
            'add ddl site type
            ddlSiteType.Items.Add(New ListItem("N/A", 0))
            ddlSiteType.Items.Add(New ListItem("GBT", 1))
            ddlSiteType.Items.Add(New ListItem("RTT", 2))
            ddlSiteType.Items.Add(New ListItem("RTP", 3))
            ' add Eb Connection Items
            ddlEBconnection.Items.Add(New ListItem("N/A", 0))
            ddlEBconnection.Items.Add(New ListItem("YES", 1))
            ddlEBconnection.Items.Add(New ListItem("NO", 2))
            ' add DG SET Items
            ddlDGSet.Items.Add(New ListItem("N/A", 0))
            ddlDGSet.Items.Add(New ListItem("YES", 1))
            ddlDGSet.Items.Add(New ListItem("NO", 2))
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
            ddlCluster.Items.Clear()
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            If DRVendor.HasRows Then
                ddlCluster.DataSource = DRVendor
                ddlCluster.DataTextField = "D_LOCATION_NAME"
                ddlCluster.DataValueField = "D_LOCATION_ID"
                ddlCluster.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVSite.Sorting
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

    Protected Sub hdf_CircleId_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hdf_CircleId.ValueChanged

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
        mdlPopup.Show()
        BindCircle(ddlcustomer.SelectedValue.ToString)
    End Sub

    Protected Sub ddlcircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcircle.SelectedIndexChanged
        mdlPopup.Show()
        BindLocation(ddlcircle.SelectedValue.ToString)
    End Sub

    Protected Sub BtnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImport.Click
        Dim FilePath As String
        Dim FileName As String

        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.fileinput.PostedFile.FileName)
            If ext = "" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                'Page.RegisterStartupScript("Nodata", sAlert)
                lblWarningMessage.Text = "Please Select File"
                lblWarningMessage.Visible = True
            ElseIf ext.ToLower <> ".csv" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select .csv File')</script>"
                'Page.RegisterStartupScript("Nodata", sAlert)
                lblWarningMessage.Text = "Please Select .csv File"
                lblWarningMessage.Visible = True
            Else

                FilePath = Left(System.IO.Path.GetFullPath(fileinput.PostedFile.FileName), System.IO.Path.GetFullPath(fileinput.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(fileinput.PostedFile.FileName)
                If fileinput.PostedFile.FileName.ToString = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    'Page.RegisterStartupScript("Nodata", sAlert)
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
                objDSSiteExcel = New DataSet
                ExcelAdapter.Fill(objDSSiteExcel)
                ExcelConnection.Close()

                If (objDSSiteExcel.Tables(0).Rows.Count) = 0 Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('No Records found in excel sheet')</script>"
                    'Page.RegisterStartupScript("Nodata", sAlert)
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
                        Dim p14 As String() = (dtRow.Item(13).ToString.Trim).Split(" ")
                        i = i + 1
                        Dim Params As New ArrayList
                        Params.Add("INSERT") 'P0
                        Params.Add(dtRow.Item(0).ToString.Trim)  'P1
                        Params.Add(dtRow.Item(1).ToString.Trim)  'P2
                        Params.Add(dtRow.Item(2).ToString.Trim)  'P3
                        Params.Add(dtRow.Item(3).ToString.Trim)  'P4
                        Params.Add(dtRow.Item(4).ToString.Trim)  'P5
                        Params.Add(dtRow.Item(5).ToString.Trim)  'P6
                        Params.Add(dtRow.Item(6).ToString.Trim)  'P7
                        Params.Add(dtRow.Item(7).ToString.Trim)  'P8
                        Params.Add(dtRow.Item(8).ToString.Trim)  'P9
                        Params.Add(dtRow.Item(9).ToString.Trim)  'P10
                        Params.Add(dtRow.Item(10).ToString.Trim) 'P11
                        Params.Add(dtRow.Item(11).ToString.Trim) 'P12
                        Params.Add(dtRow.Item(12).ToString.Trim) 'P13
                        Params.Add(p14(0)) 'P14
                        Params.Add(CInt(Session("USER_ID")))
                        Params.Add(dtRow.Item(14).ToString.Trim) 'P16
                        'Params.Add(dtRow.Item(16).ToString.Trim) 'P17
                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_SITE_DATA", Params)
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
    Protected Sub lnktemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnktemplate.Click
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/SITE.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/SITE.csv"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            ' fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsearchdealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearchdealer.Click
        Try
            Dim objClsdb As New clsDB
            Dim DRdealer As DataSet
            Dim dealerParam(0, 1) As String
            dealerParam(0, 0) = "@P1"
            dealerParam(0, 1) = txtsearch.Text
            DRdealer = objClsdb.ExecProc_getDataSet("[D_SP_GET_SITE_FOR_SEARCH_GRID]", dealerParam)
            GVSite.DataSource = DRdealer
            GVSite.DataBind()
            'GVCustomer.AllowSorting = Nothing
            txtsearch.Text = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class
