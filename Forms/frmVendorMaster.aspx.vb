Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmVendorMaster
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
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
    Protected Sub BindGrid()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        'dsSupplier = objDB.getDataSet_SProc("D_SP_GET_CIRCLE_FOR_GRID", param)
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_VENDOR_FOR_GRID")
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)

        GVCircle.DataSource = dt
        GVCircle.DataBind()
    End Sub
    Protected Sub BindGrid_sort()
        SetSortOrder()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        'Dim param(0, 1) As String
        'param(0, 0) = "@intCircleId"
        'param(0, 1) = 0 'Session("UserID")
        dsSupplier = objDB.getDataSet_SProc("D_SP_GET_VENDOR_FOR_GRID")
        strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim dt As New DataTable
        dt = dsSupplier.Tables(0)
        dt.DefaultView.Sort = strSort
        GVCircle.DataSource = dt.DefaultView
        GVCircle.DataBind()
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

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        ShowModalDataInsert()
    End Sub
    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            txtVendorName.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ClearControl()

        txtVendorName.Text = ""
        chkIsactive.Checked = True
        hdf_CircleId.Value = Nothing
        btnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False
    End Sub
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClearControl()
    End Sub
    Protected Sub ShowModalData(ByVal id As Integer)
        Try
            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@IntVendorID"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("[D_SP_GET_VENDOR_FOR_GRID_EDIT]", varParam)
            If dt.Rows.Count > 0 Then
                txtVendorName.Text = dt.Rows(0)("D_VENDOR_NAME").ToString()
                'txtCircleCode.Text = dt.Rows(0)("D_CIRCLE_CODE").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlCircleDetail.Update()
            mdlPopup.Show()
            txtVendorName.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GVCircle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVCircle.PageIndexChanging
        GVCircle.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlCircleDetail.Update()
        SaveCircle()
        Me.UpGVCircle.Update()
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
                varParam.Add(0) 'p1
            Else
                varParam.Add("UPDATE") 'P0
                varParam.Add(CInt(hdf_CircleId.Value.ToString())) 'p1
            End If
            varParam.Add(txtVendorName.Text.Trim) 'P2 
            ' varParam.Add(txtCircleCode.Text.Trim) 'P3 
            ' USERID = CInt(Session("USER_ID"))

            ' varParam.Add(USERID) 'P4
            If chkIsactive.Checked = True Then
                CircleActive = "Y"
            Else
                CircleActive = "N"
            End If
            varParam.Add(CircleActive) 'P4
            strOUT = objDB.ExecProc_getStatus("[D_SP_VENDOR]", varParam)
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GVCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVCircle.SelectedIndexChanged
        Try

        
            Dim CircleId As String
            CircleId = GVCircle.SelectedValue.ToString()

            hdf_CircleId.Value = CircleId
            Dim intCircleId As Integer
            intCircleId = CInt(CircleId)
            'BindControls(intCircleId)
            ShowModalData(intCircleId)
            btnSubmit.Text = "Update"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GVCircle_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVCircle.Sorting
        Dim exp As String = e.SortExpression
        ViewState("SortExpression") = exp
        BindGrid_sort()
    End Sub
End Class
