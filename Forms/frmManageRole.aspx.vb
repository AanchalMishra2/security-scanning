Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Forms_frmManageRole
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            chkIsactive.Checked = True
            InitializeVariables()
            BindGrid()
        End If
    End Sub
    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "D_CREATED_ON"
        ViewState("SortOrder") = "ASC"
        SetSortOrder()
    End Sub
    Protected Sub SetSortOrder()
        If ViewState("SortOrder") Is Nothing Then
            ViewState("SortOrder") = "ASC"
        End If
        If ViewState("SortOrder").ToString() = "ASC" Then
            ViewState("SortOrder") = "DESC"
        Else
            ViewState("SortOrder") = "ASC"
        End If
    End Sub
    Public Sub BindGrid()
        Dim dsCountry As New DataSet
        Dim params(0, 1) As String
        params(0, 0) = "@UserId"
        params(0, 1) = Session("USER_ID")  ' 'CInt(Session.Item("userid"))
        dsCountry = objDB.ExecProc_getDataSet("D_GET_ROLE_DETAIL", params)
        Dim Dt As New DataTable
        Dt = dsCountry.Tables(0)

        GVRole.DataSource = Dt
        GVRole.DataBind()

    End Sub
    Public Sub BindGrid_sort()

        Dim dsCountry As New DataSet
        Dim params(0, 1) As String
        params(0, 0) = "@UserId"
        params(0, 1) = Session("USER_ID")  ''CInt(Session.Item("userid"))
        dsCountry = objDB.ExecProc_getDataSet("D_GET_ROLE_DETAIL", params)
        Dim strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim Dt As New DataTable
        Dt = dsCountry.Tables(0)
        Dt.DefaultView.Sort = strSort
        GVRole.DataSource = Dt.DefaultView
        GVRole.DataBind()

    End Sub
    Public Sub ClearControl()
        txtRoleName.Text = ""
        txtRoleDes.Text = ""
        hdf_RoleId.Value = Nothing
        chkIsactive.Checked = True
        BtnSubmit.Text = "Save"
        lblWarningMessage.Visible = False
    End Sub
    Protected Sub ShowModalData(ByVal id As Integer)
        Try

            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@RoleId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("D_GET_ROLE_DETAIL_EDIT", varParam)
            If dt.Rows.Count > 0 Then
                txtRoleName.Text = dt.Rows(0)("D_ROLE_NAME").ToString()
                txtRoleDes.Text = dt.Rows(0)("D_ROLE_DESC").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlRole.Update()
            mdlPopup.Show()
            txtRoleName.Focus()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub BindControls(ByVal countriyid As Integer)
        Try

            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@IntCountryId"
            varParam(0, 1) = ID
            dt = objDB.ExecProc_getDataTable("D_GET_ROLE_DETAIL", varParam)
            If dt.Rows.Count > 0 Then
                txtRoleName.Text = dt.Rows(0)("D_ROLE_NAME").ToString()
                txtRoleDes.Text = dt.Rows(0)("D_ROLE_DESC").ToString()
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub DeleteRole(ByVal id As Integer)
        Try
            Dim varparam(0, 1) As String
            Dim strOUT As String = ""
            varparam(0, 0) = "@intRoleID"
            varparam(0, 1) = id
            strOUT = objDB.ExecProc_getRecordsAffected("D_SP_DELETE_ROLE_MSTR", varparam)
            lblWarningMessage.Visible = True
            If strOUT = "1" Then
                lblWarningMessage.Text = "Record Deleted Sucussfully !!"
            Else
                lblWarningMessage.Text = "Failed To Delete Record !!"
            End If
            BindGrid()
            Me.UpGVRole.Update()
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub MsgAlert(ByVal strOUT As String)
        Dim sAlert As String = "<SCRIPT language='Javascript'>alert('" + strOUT + "')</script>"
        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "showAlert", sAlert, True)
    End Sub
    Protected Sub ShowModalDataInsert()
        Try
            ClearControl()
            upPnlRole.Update()
            mdlPopup.Show()
            txtRoleName.Focus()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub BtnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click

        ShowModalDataInsert()
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClearControl()
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        
        Me.mdlPopup.Hide()
        ' BindGrid()
        Me.upPnlRole.Update()
        'pnlPopup.Visible = False
        SaveRole()
        Me.UpGVRole.Update()
    End Sub
    Protected Sub SaveRole()
        Try
            Dim Varparam As New ArrayList
            Dim USERID As Integer
            Dim RoleActive As String
            Dim strOUT As String
            Dim intRoleId As Integer = 0
            If hdf_RoleId.Value.ToString() = "" Then
                Varparam.Add("INSERT") 'P0
                Varparam.Add(intRoleId) 'p1
            Else
                Varparam.Add("UPDATE") 'P0
                Varparam.Add(CInt(hdf_RoleId.Value.ToString())) 'p1
            End If
            Varparam.Add(txtRoleName.Text.Trim) 'P2
            Varparam.Add(txtRoleDes.Text.Trim) 'P3
            USERID = Session("USER_ID")  'CInt(Session.Item("userid"))
            Varparam.Add(USERID) 'P4
            If chkIsactive.Checked = True Then
                RoleActive = "Y"
            Else
                RoleActive = "N"
            End If
            Varparam.Add(RoleActive) 'P5
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_ROLE_MSTR", Varparam)
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GVRole_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVRole.PageIndexChanging
        GVRole.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVRole_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVRole.RowDeleting
        Dim hf As HiddenField = CType(GVRole.Rows(e.RowIndex).FindControl("hfRoleID"), HiddenField)
        Dim RID As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteRole(RID)
    End Sub

    Protected Sub GVRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVRole.SelectedIndexChanged
        Dim RoleId As String
        RoleId = GVRole.SelectedValue.ToString()
        hdf_RoleId.Value = RoleId
        Dim intRoleId As Integer
        intRoleId = CInt(RoleId)
        'BindControls(intCountryId)
        ShowModalData(intRoleId)
        BtnSubmit.Text = "Update"
    End Sub

    Protected Sub GVRole_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVRole.Sorting
        Dim exp As String = e.SortExpression
        ViewState("SortExpression") = exp
        BindGrid_sort()
    End Sub

    Protected Sub GVRole_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVRole.RowDataBound
        Dim edit As LinkButton = e.Row.FindControl("lnkedit")
        Dim delete As LinkButton = e.Row.FindControl("lnkdelete")

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(2).Text = "ADMINISTRATOR" Or e.Row.Cells(2).Text = "SYSTEM ADMIN" Then
                edit.Enabled = False
                delete.Enabled = False
            End If
        End If

    End Sub

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
End Class
