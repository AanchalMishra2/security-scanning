Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmCustomerMstr
    Inherits System.Web.UI.Page
    Public objDB As New clsDB
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
        ' pnlimport.Visible = False
        If Not Page.IsPostBack Then
            Insert_log()
            InitializeVariables()
            BindGrid()
        End If
    End Sub
    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "D_CIRCLE_ID"
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
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_CUSTOMER_FOR_GRID")
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
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_CUSTOMER_FOR_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVCustomer.DataSource = dt.DefaultView
        GVCustomer.DataBind()
    End Sub

    Protected Sub ClearControl()

        txtcustomer.Text = ""
        txtcustomerCode.Text = ""
        chkIsactive.Checked = True
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCircle()
        Try
            Dim varParam As New ArrayList
            Dim CircleActive As String
            Dim strOUT As String
            Dim intCircleId As Integer = 0
            If hdf_CircleId.Value.ToString() = "" Then
                varParam.Add("INSERT") 'P0
                varParam.Add(intCircleId) 'p1
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hdf_CircleId.Value.ToString())) 'p1
            End If
            varParam.Add(txtcustomer.Text.Trim) 'P2 
            varParam.Add(txtcustomerCode.Text.Trim) 'P3 
            ' varParam.Add(USERID) 'P4
            If chkIsactive.Checked = True Then
                CircleActive = "Y"
            Else
                CircleActive = "N"
            End If
            varParam.Add(CircleActive) 'P4
            varParam.Add(CInt(Session("USER_ID"))) 'p5
            strOUT = objDB.ExecProc_getStatus("[D_SP_CUSTOMER]", varParam)
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
            txtcustomer.Text = dt.Rows(0)("D_CIRCLE_NAME").ToString()
            txtcustomerCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
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

    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVCustomer.PageIndexChanging
        GVCustomer.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCircle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVCustomer.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lkbtn As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)
        '    lkbtn.Attributes.Add("onclick", "showConfirm('" & GVCircle.DataKeys(e.Row.RowIndex).Value & "'); return false; )")
        'End If
    End Sub

    Protected Sub GVCircle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVCustomer.RowDeleting
        Dim hf As HiddenField = CType(GVCustomer.Rows(e.RowIndex).FindControl("hfCircleID"), HiddenField)
        Dim CircleId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCircle(CircleId)
    End Sub

    Protected Sub GVCircle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVCustomer.RowEditing

    End Sub


    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVCustomer.SelectedIndexChanged
        Dim CircleId As String
        CircleId = GVCustomer.SelectedValue.ToString()

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
            varParam(0, 0) = "@intCustomerID"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_CUSTOMER_FOR_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                txtcustomer.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()
                txtcustomerCode.Text = dt.Rows(0)("D_CUSTOMER_CODE").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            txtcustomer.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()

            upPnlCircleDetail.Update()
            mdlPopup.Show()
            txtcustomer.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVCustomer.Sorting
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
End Class
