Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmLocationMstr
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB

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
        ' objDB.getDataInGrid1(dgLocation, "D_SP_GET_LOCATION_FOR_GRID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_LOCATION_FOR_GRID")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVLocation.DataSource = dt
        GVLocation.DataBind()
    End Sub

    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_LOCATION_FOR_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVLocation.DataSource = dt.DefaultView
        GVLocation.DataBind()
    End Sub

    Protected Sub ClearControl()
        ddlCircle.SelectedValue = "0"
        txtClusterName.Text = ""
        chkactive.Checked = True
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub

    Protected Sub SaveCircle()
        Try
            Dim varParam As New ArrayList
            Dim USERID As Integer
            Dim CircleActive As String
            Dim strOUT As String
            Dim intCircleId As Integer = 0
            If hdf_CircleId.Value.ToString() = "" Then
                varParam.Add("INSERT") 'P0
                varParam.Add(intCircleId) 'p1
                varParam.Add(CInt(ddlCircle.SelectedValue))
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hdf_CircleId.Value.ToString())) 'p1
                varParam.Add(CInt(ddlCircle.SelectedValue))
            End If
            varParam.Add(txtClusterName.Text.Trim) 'P2 
            USERID = CInt(Session("USER_ID"))
            If chkactive.Checked = True Then
                CircleActive = "Y"
            Else
                CircleActive = "N"
            End If
            varParam.Add(CircleActive) 'P4
            strOUT = objDB.ExecProc_getStatus("[D_SP_LOCATION]", varParam)
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
        'varParam(0, 0) = "@intUserId"
        'varParam(0, 1) = CInt(Session("USER_ID"))
        dt = objDB.ExecProc_getDataTable("[D_SP_GET_CIRCLE]", varParam)
        If dt.Rows.Count > 0 Then
            ddlCircle.SelectedValue = dt.Rows(0)("D_CIRCLE_NAME").ToString()
            txtClusterName.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
            If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                chkactive.Checked = True
            Else
                chkactive.Checked = False
            End If

        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlCircleDetail.Update()
        SaveCircle()
        Me.UpGVCircle.Update()
    End Sub

    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVLocation.PageIndexChanging
        GVLocation.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCircle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVLocation.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lkbtn As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)
        '    lkbtn.Attributes.Add("onclick", "showConfirm('" & GVCircle.DataKeys(e.Row.RowIndex).Value & "'); return false; )")
        'End If
    End Sub

    Protected Sub GVCircle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVLocation.RowDeleting
        Dim hf As HiddenField = CType(GVLocation.Rows(e.RowIndex).FindControl("hfCircleID"), HiddenField)
        Dim CircleId As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCircle(CircleId)
    End Sub

    Protected Sub GVCircle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVLocation.RowEditing

    End Sub


    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVLocation.SelectedIndexChanged
        Dim CircleId As String
        CircleId = GVLocation.SelectedValue.ToString()

        hdf_CircleId.Value = CircleId
        Dim intCircleId As Integer
        intCircleId = CInt(CircleId)
        'BindControls(intCircleId)
        BindCircle()
        ShowModalData(intCircleId)
        btnSubmit.Text = "Update"
    End Sub

    Protected Sub ShowModalData(ByVal id As Integer)
        Try
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@intlocationId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_LOCATION_FOR_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                ddlCircle.SelectedValue = CStr(dt.Rows(0)("D_CIRCLE_ID").ToString())
                txtClusterName.Text = dt.Rows(0)("D_LOCATION_NAME").ToString()
                If (CStr(dt.Rows(0)("D_ACTIVE")).ToUpper).Trim = "Y" Then
                    chkactive.Checked = True
                Else
                    chkactive.Checked = False
                End If

            End If
            upPnlCircleDetail.Update()
            mdlPopup.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            BindCircle()
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            txtClusterName.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub BindCircle()
        Try
            Dim DRCircle As SqlDataReader
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intUserId"
            circleParam(0, 1) = CInt(Session("USER_ID"))
            DRCircle = objDB.ExecProc_getDataReder("D_SP_GET_CIRCLE", circleParam)
            If DRCircle.HasRows Then
                ddlCircle.DataSource = DRCircle
                ddlCircle.DataTextField = "D_CIRCLE_NAME"
                ddlCircle.DataValueField = "D_CIRCLE_ID"
                ddlCircle.DataBind()
                DRCircle.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVLocation.Sorting
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
