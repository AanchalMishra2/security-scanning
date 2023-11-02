Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Partial Class Forms_frmSubPetroCardMstr
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
        ViewState("SortExpression") = "D_S_PETRO_SR_NO"
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
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_SUBPETROCARD_FOR_GRID")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVSubpertrocard.DataSource = dt
        GVSubpertrocard.DataBind()
    End Sub

    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_SUBPETROCARD_FOR_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVSubpertrocard.DataSource = dt.DefaultView
        GVSubpertrocard.DataBind()
    End Sub

    Protected Sub ClearControl()

        ddlcircle.Items.Clear()
        ddlvendor.Items.Clear()
        ddlvendor.Items.Clear()
        ddlpetrocardno.Items.Clear()
        chkIsactive.Checked = True
        txtsubpetrocard.Text = ""
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCircle()
        Try
            Dim varParam As New ArrayList
            Dim strOUT As String
            Dim intCircleId As Integer = 0
            If hdf_CircleId.Value.ToString() = "" Then
                varParam.Add("INSERT") 'P0
                varParam.Add(intCircleId) 'p1
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hdf_CircleId.Value.ToString())) 'p1
            End If
            varParam.Add(ddlcircle.SelectedValue) 'P2
            varParam.Add(ddlvendor.SelectedValue) 'P3
            varParam.Add(ddlpetrocardno.SelectedItem.Text) 'P4
            varParam.Add(txtsubpetrocard.Text) 'P5
            If chkIsactive.Checked = True Then
                varParam.Add("Y") 'P6
            End If
            If chkIsactive.Checked = False Then
                varParam.Add("N") 'P6
            End If
            varParam.Add(ddlCustomer.SelectedValue) 'P7
            varParam.Add(CInt(Session("User_ID"))) 'P8

            strOUT = objDB.ExecProc_getStatus("[D_SP_SUB_PETROCARD]", varParam)
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
            '  txtCircleName.Text = dt.Rows(0)("D_CIRCLE_NAME").ToString()
            ' txtCircleCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
            If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                chkIsactive.Checked = True
            Else
                chkIsactive.Checked = False
            End If

        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlCircleDetail.Update()
        SaveCircle()
        Me.UpGVCircle.Update()
    End Sub

    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVSubpertrocard.PageIndexChanging
        GVSubpertrocard.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCircle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVSubpertrocard.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lkbtn As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)
        '    lkbtn.Attributes.Add("onclick", "showConfirm('" & GVCircle.DataKeys(e.Row.RowIndex).Value & "'); return false; )")
        'End If
    End Sub

    Protected Sub GVCircle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVSubpertrocard.RowDeleting
        Dim hf As HiddenField = CType(GVSubpertrocard.Rows(e.RowIndex).FindControl("hfCircleID"), HiddenField)
        Dim CircleId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCircle(CircleId)
    End Sub

    Protected Sub GVCircle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVSubpertrocard.RowEditing

    End Sub


    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVSubpertrocard.SelectedIndexChanged
        Dim CircleId As String
        CircleId = GVSubpertrocard.SelectedValue.ToString()

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
            varParam(0, 0) = "@intCircleId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_CIRCLE_FOR_GRID_DTLS]", varParam)
            If dt.Rows.Count > 0 Then
                'txtCircleName.Text = dt.Rows(0)("D_CIRCLE_NAME").ToString()
                'txtCircleCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
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
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            'txtCircleName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVSubpertrocard.Sorting
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
    Public Sub BindCustomer()
        Dim DRCustomer As SqlDataReader
        Try

            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlcircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlvendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlpetrocardno.Items.Add(New ListItem("--SELECT--", 0))
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
        mdlPopup.Show()
        BindCircle(ddlCustomer.SelectedValue)
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

    Protected Sub ddlcircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcircle.SelectedIndexChanged
        mdlPopup.Show()
        BindVendor()
    End Sub
    Public Sub BindVendor()
        Try
            Dim circleParam(0, 1) As String
            Dim DRVendor As SqlDataReader
            'circleParam(0, 0) = "@intCircleId"
            'circleParam(0, 1) = ddlCircleName.SelectedValue
            'DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_VENDOR_FOR_SUBPETROCARD", circleParam)
            DRVendor = objDB.getReader_Sproc_NoParam("D_SP_GET_VENDOR_FOR_SUBPETROCARD")
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

    Protected Sub ddlvendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlvendor.SelectedIndexChanged
        mdlPopup.Show()
        BindPetroCards()
    End Sub
    Private Sub BindPetroCards()
        Try
            Dim arrParam(2, 1) As String
            Dim DRPetroCard As SqlDataReader
            arrParam(0, 0) = "@intCircleID"
            arrParam(0, 1) = ddlcircle.SelectedValue
            arrParam(1, 0) = "@intVendorId"
            arrParam(1, 1) = ddlvendor.SelectedValue
            arrParam(2, 0) = "@intCustomerId"
            arrParam(2, 1) = ddlCustomer.SelectedValue
            DRPetroCard = objDB.ExecProc_getDataReder("D_SP_GET_PETRO_CARDS_FOR_SUBPETRO_MSTR", arrParam)
            ddlpetrocardno.Items.Clear()
            If DRPetroCard.HasRows Then
                ddlpetrocardno.DataSource = DRPetroCard
                ddlpetrocardno.DataTextField = "D_CARD_NO"
                ddlpetrocardno.DataValueField = "D_C_NO"
                ddlpetrocardno.DataBind()
                DRPetroCard.Close()
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
                If fileinput.PostedFile.FileName = "" Then
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
                        Params.Add(CInt(Session("USER_ID")))  'P4
                        Params.Add(dtRow.Item(4).ToString.Trim) 'P5

                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_SUB_PETROCARD", Params)
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/SUBPETROCARD.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/SUBPETROCARD.csv"))
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
            DRdealer = objClsdb.ExecProc_getDataSet("[D_SP_GET_SUBPETROCARD_FOR_SEARCH_GRID]", dealerParam)
            GVSubpertrocard.DataSource = DRdealer
            GVSubpertrocard.DataBind()
            'GVSubpertrocard.AllowSorting = Nothing
            txtsearch.Text = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class
