Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmPetroCardMstr1
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Protected Sub Forms_frmPetroCardMstr1_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            Response.Redirect("../frmLogin.aspx")
            Exit Sub
        End If
        If Page.Theme = Nothing Then
            Page.Theme = "Default"
        End If
    End Sub
    Protected Sub Forms_frmPetroCardMstr1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblWarningMessage.Text = ""
        'pnlimport.Visible = False
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
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_PETRO_CARD_LIST_FOR_GIRD")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVpetrocards.DataSource = dt
        GVpetrocards.DataBind()
    End Sub

    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_PETRO_CARD_LIST_FOR_GIRD")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVpetrocards.DataSource = dt.DefaultView
        GVpetrocards.DataBind()
    End Sub

    Protected Sub ClearControl()
        ddlcircle.Items.Clear()
        ddlVendor.Items.Clear()
        txtCardNo.Text = ""
        txtnameOnCard.Text = ""
        txtFilledAmount.Text = ""
        txtValidityDate.Text = ""
        txtPetrocardLimit.Text = ""
        chkIsactive.Checked = True
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCircle()
        Try
            Dim Params As New ArrayList
            If hdf_CircleId.Value.ToString() = "" Then
                Params.Add("INSERT") 'P0
                Params.Add(CInt("0")) 'P
            Else
                Params.Add("UPDATE") 'P0
                Params.Add(CInt(hdf_CircleId.Value.ToString())) 'P1
            End If
            Params.Add(ddlcircle.SelectedValue) 'P2
            Params.Add(txtCardNo.Text) 'P3
            Params.Add(txtnameOnCard.Text) 'P4
            Params.Add(ddlVendor.SelectedValue) 'P5
            Params.Add(txtValidityDate.Text.Trim) 'p6
            If txtFilledAmount.Text = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtFilledAmount.Text)) 'P7
            End If
            Params.Add(txtPetrocardLimit.Text) 'P8
            Params.Add("") 'P9
            Params.Add("") 'P10
            If (chkIsactive.Checked = True) Then
                Params.Add("Y") 'P11
            End If

            If (chkIsactive.Checked = False) Then
                Params.Add("N") 'P11
            End If
            If Session("User_Role") = "EXECUTIVE" Then
                Params.Add(CInt(Session("user_id"))) 'P12
            Else
                Params.Add(CInt(Session("user_id"))) 'P12
            End If
            Params.Add(ddlcustomer.SelectedValue) 'P13
            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_PETROL_CARD", Params)
            Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
            BindGrid()
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
            'txtCircleCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
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

    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVpetrocards.PageIndexChanging
        GVpetrocards.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCircle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVpetrocards.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lkbtn As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)
        '    lkbtn.Attributes.Add("onclick", "showConfirm('" & GVCircle.DataKeys(e.Row.RowIndex).Value & "'); return false; )")
        'End If
    End Sub

    Protected Sub GVCircle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVpetrocards.RowDeleting
        Dim hf As HiddenField = CType(GVpetrocards.Rows(e.RowIndex).FindControl("hfCircleID"), HiddenField)
        Dim CircleId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCircle(CircleId)
    End Sub

    Protected Sub GVCircle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVpetrocards.RowEditing

    End Sub


    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpetrocards.SelectedIndexChanged
        Dim CircleId As String
        CircleId = GVpetrocards.SelectedValue.ToString()

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
            varParam(0, 0) = "@intDCNo"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_PETRO_CARD_LIST_FOR_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                BindCustomer()
                ddlcustomer.SelectedValue = dt.Rows(0)("D_CUSTOMER_ID").ToString()
                BindCircle(dt.Rows(0)("D_CUSTOMER_ID").ToString())
                ddlcircle.SelectedValue = dt.Rows(0)("D_CIRCLE_ID").ToString()
                BindVendor()
                ddlVendor.SelectedValue = dt.Rows(0)("D_VENDOR_ID").ToString()
                txtCardNo.Text = dt.Rows(0)("D_CARD_NO").ToString()
                txtnameOnCard.Text = dt.Rows(0)("D_CARD_NO").ToString()
                txtFilledAmount.Text = dt.Rows(0)("D_FILL_AMOUNT").ToString()
                txtPetrocardLimit.Text = dt.Rows(0)("D_CARD_LIMIT").ToString()
                txtValidityDate.Text = dt.Rows(0)("D_VALIDITY_DATE").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            '  txtCircleName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            BindCustomer()
            BindVendor()
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
            ddlcircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
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
    Public Sub BindVendor()
        Try
            Dim DRvendor As SqlDataReader
            Dim circleParam(1, 1) As String
            DRvendor = objDB.getReader_Sproc_NoParam("D_SP_GET_VENDOR_FOR_DROPDOWN")
            If DRvendor.HasRows Then
                ddlVendor.DataSource = DRvendor
                ddlVendor.DataTextField = "D_VENDOR_NAME"
                ddlVendor.DataValueField = "D_VENDOR_ID"
                ddlVendor.DataBind()
                DRvendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVpetrocards.Sorting
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
        BindCircle(ddlcustomer.SelectedValue)
    End Sub
    Public Sub BindCircle(ByVal custId As Integer)
        Try
            Dim DRCircle As SqlDataReader
            Dim circleParam(1, 1) As String
            circleParam(0, 0) = "@intCustomerd"
            circleParam(0, 1) = custId
            circleParam(1, 0) = "@intUserId"
            circleParam(1, 1) = CInt(Session("USER_ID"))
            DRCircle = objDB.ExecProc_getDataReder("D_SP_GET_CIRCLE_WITH_RESPECT_TO_CUSTOMER_WITHOUT_ALL", circleParam)
            ddlcircle.Items.Clear()
            If DRCircle.HasRows Then
                ddlcircle.DataSource = DRCircle
                ddlcircle.DataTextField = "D_CIRCLE_NAME"
                ddlcircle.DataValueField = "D_CIRCLE_ID"
                ddlcircle.DataBind()
                DRCircle.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
End Class
