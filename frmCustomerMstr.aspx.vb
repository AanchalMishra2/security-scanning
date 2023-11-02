Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class frmCustomerMstr
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB

    Protected Sub InitializeVariables()
        ViewState("SortExpression") = "D_CREATED_ON"
        ViewState("SortOrder") = "ASC"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            'Response.Redirect("frmLogin.aspx")
            Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            InitializeVariables()
            BindGrid()
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Me.mdlPopup.Hide()
        Me.upPnlCustomer.Update()
        SaveCustomer()
        Me.UpGVCustomer.Update()
    End Sub
     
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClearControl()
    End Sub

    Protected Sub BtnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click
        ShowModalDataInsert()
    End Sub

    Protected Sub SaveCustomer()
        Try
            Dim Varparam As New ArrayList
            Dim USERID As Integer
            Dim CustomerActive As String
            Dim strOUT As String
            Dim intCustomerId As Integer = 0
            If hdf_Customerid.Value.ToString() = "" Then
                Varparam.Add("INSERT") 'P0
                Varparam.Add(intCustomerId) 'p1
            Else
                Varparam.Add("UPDATE") 'P0
                Varparam.Add(CInt(hdf_Customerid.Value.ToString())) 'p1
            End If
            Varparam.Add(txtCustomerName.Text.Trim) 'P2
            USERID = 1
            Varparam.Add(USERID) 'P4
            If chkIsactive.Checked = True Then
                CustomerActive = "Y"
            Else
                CustomerActive = "N"
            End If
            Varparam.Add(CustomerActive) 'P5
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_CUSTOMER_MSTR", Varparam)
            BindGrid()
            ClearControl()
            lblWarningMessage.Visible = True
            lblWarningMessage.Text = strOUT
        Catch ex As Exception

        End Try
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
        SetSortOrder()
        Dim dsCustomer As New DataSet
        Dim params(0, 1) As String
        params(0, 0) = "@UserId"
        params(0, 1) = 1 'CInt(Session.Item("userid"))
        dsCustomer = objDB.ExecProc_getDataSet("D_SP_GET_CUSTOMER_DETAIL", params)
        'Dim strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim Dt As New DataTable
        Dt = dsCustomer.Tables(0)
        'Dt.DefaultView.Sort = strSort
        GVCustomer.DataSource = Dt
        GVCustomer.DataBind()

    End Sub

    Public Sub BindGrid_Sort()
        SetSortOrder()
        Dim dsCustomer As New DataSet
        Dim params(0, 1) As String
        params(0, 0) = "@UserId"
        params(0, 1) = 1 'CInt(Session.Item("userid"))
        dsCustomer = objDB.ExecProc_getDataSet("D_SP_GET_CUSTOMER_DETAIL", params)
        Dim strSort = ViewState("SortExpression").ToString() & " " & ViewState("SortOrder").ToString()
        Dim Dt As New DataTable
        Dt = dsCustomer.Tables(0)
        Dt.DefaultView.Sort = strSort
        GVCustomer.DataSource = Dt.DefaultView
        GVCustomer.DataBind()

    End Sub 

    Protected Sub GVCustomer_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVCustomer.PageIndexChanged

    End Sub

    Protected Sub GVCustomer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVCustomer.PageIndexChanging
        GVCustomer.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GVCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVCustomer.RowDeleting
        Dim hf As HiddenField = CType(GVCustomer.Rows(e.RowIndex).FindControl("hfCustomerID"), HiddenField)
        Dim CountID As Integer = Convert.ToInt32(hf.Value.ToString())
        DeleteCustomer(CountID)
    End Sub

    Protected Sub GVCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVCustomer.SelectedIndexChanged
        Dim CustomerId As String
        CustomerId = GVCustomer.SelectedValue.ToString()
        hdf_Customerid.Value = CustomerId
        Dim intCustomerId As Integer
        intCustomerId = CInt(CustomerId)
        'BindControls(intCustomerId)
        ShowModalData(CustomerId)
        BtnSubmit.Text = "Update"
    End Sub

    Protected Sub ShowModalData(ByVal id As Integer)
        Try

            Dim dt As New DataTable
            Dim varParam(0, 1) As String
            varParam(0, 0) = "@IntCustomerId"
            varParam(0, 1) = id
            dt = objDB.ExecProc_getDataTable("D_SP_GET_CUSTOMER_DETAIL", varParam)
            If dt.Rows.Count > 0 Then
                txtCustomerName.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()
                
                If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                    chkIsactive.Checked = True
                Else
                    chkIsactive.Checked = False
                End If

            End If
            upPnlCustomer.Update()
            mdlPopup.Show()
            txtCustomerName.Focus()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GVCustomer_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVCustomer.Sorting
        Dim exp As String = e.SortExpression
        ViewState("SortExpression") = exp
        BindGrid_Sort()
    End Sub

    Protected Sub BindControls(ByVal countriyid As Integer)
        Dim dt As New DataTable
        Dim varParam(0, 1) As String
        varParam(0, 0) = "@IntCustomerId"
        varParam(0, 1) = countriyid
        dt = objDB.ExecProc_getDataTable("D_SP_GET_CUSTOMER_DETAIL", varParam)
        If dt.Rows.Count > 0 Then
            txtCustomerName.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()

            If dt.Rows(0)("D_ACTIVE").ToString = "Y" Then
                chkIsactive.Checked = True
            Else
                chkIsactive.Checked = False
            End If

        End If
    End Sub
     
    Public Sub ClearControl()
        txtCustomerName.Text = ""

        hdf_Customerid.Value = Nothing
        chkIsactive.Checked = False
        BtnSubmit.Text = "Submit"
        lblWarningMessage.Visible = False

    End Sub

    Protected Sub DeleteCustomer(ByVal id As Integer)
        Try
            Dim varparam(0, 1) As String
            Dim strOUT As String = ""
            varparam(0, 0) = "@intCID"
            varparam(0, 1) = id
            strOUT = objDB.ExecProc_getRecordsAffected("D_SP_DELETE_CUSTOMER_MSTR", varparam)
            lblWarningMessage.Visible = True
            If strOUT = "1" Then
                lblWarningMessage.Text = "Record Deleted Sucussfully !!"
            Else
                lblWarningMessage.Text = "Failed To Delete Record !!"
            End If

            BindGrid()
            Me.UpGVCustomer.Update()
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
            upPnlCustomer.Update()
            mdlPopup.Show()
            txtCustomerName.Focus()
        Catch ex As Exception

        End Try

    End Sub

End Class
