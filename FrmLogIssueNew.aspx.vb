Imports System.Data
Imports System.Data.SqlClient

Partial Class Forms_FrmLogIssueNew
    Inherits System.Web.UI.Page
    Dim objDB As New clsDB

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Issue List"
        If IsNothing(Session("USER_ID")) = True Or Session("USER_ID") = "" Then
            Session.Abandon()
            'Response.Redirect("frmLogin.aspx")
            Response.Write("<script>window.parent.location.href='../frmLogin.aspx'</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub
    Protected Sub BindGrid()
        Dim dsSupplier As New DataSet
        Dim strSort As String = ""
        Dim param(0, 1) As String
        param(0, 0) = "@intUserId"
        param(0, 1) = Session("USER_ID") ' ConvertDate(txtDocDt.Text.Trim)
        'dsSupplier = objDB.ExecProc_getDataSet("[D_SP_DETAILOPEM_ISSUE_LIST_ONLY_OPEN]", param)
        dsSupplier = objDB.ExecProc_getDataSet("[D_SP_DETAIL_OPEN_ISSUE_LIST]", param)
        GridView1.DataSource = dsSupplier
        GridView1.DataBind()
    End Sub
    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        ShowModalDataInsert()
    End Sub
    Protected Sub ShowModalDataInsert()
        Try
            BindDDL()
            upPnlcustomerDetail1.Update()
            mdlPopup1.Show()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindDDL()
        BindCategory()
        Binddetails()
    End Sub
    Public Sub Binddetails()
        Dim dt As New DataTable
        Dim strSort As String = ""
        Dim param(0, 1) As String
        param(0, 0) = "@intUserId"
        param(0, 1) = Session("USER_ID")

        dt = objDB.ExecProc_getDataTable("[D_SP_GET_DEATILS_FROM_USER_MSTR_FOR_TRANSCATION]", param)

        If dt.Rows.Count > 0 Then
            txt_circle.Text = dt.Rows(0)("D_CIRCLE_NAME").ToString()
            txt_customer.Text = dt.Rows(0)("D_CUSTOMER_NAME").ToString()
            txt_department.Text = dt.Rows(0)("D_DEPT_NAME").ToString()
        End If

    End Sub
    Public Sub BindCategory()
        Dim arrParam(0, 1) As String
        Dim drcategory As SqlDataReader

        arrParam(0, 0) = "@strType"
        arrParam(0, 1) = ""
        drcategory = objDB.ExecProc_getDataReder("[D_SP_GET_CATEGORY_FOR_SUBCATEGORY]", arrParam)

        If drcategory.HasRows Then
            ddn_category.DataSource = drcategory
            ddn_category.DataTextField = "D_CATEGORY_NAME"
            ddn_category.DataValueField = "D_CATEGORY_ID"
            ddn_category.DataBind()
            drcategory.Close()
        End If

    End Sub
    Public Sub BindSubCategory()

        Dim arrParam(0, 1) As String
        Dim drcategory As SqlDataReader
        arrParam(0, 0) = "@intCatID"
        arrParam(0, 1) = ddn_category.SelectedValue
        drcategory = objDB.ExecProc_getDataReder("[D_SP_GET_SUBCATEGORY_ON_CATEGORY_FOR_TRANSCATION]", arrParam)
        If drcategory.HasRows Then
            ddn_subcategoryname.DataSource = drcategory
            ddn_subcategoryname.DataTextField = "D_SUBCATEGORY_NAME"
            ddn_subcategoryname.DataValueField = "D_SUBCATEGORY_ID"
            ddn_subcategoryname.DataBind()
            drcategory.Close()
        End If
    End Sub
    Protected Sub fillsubcategory(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddn_category.SelectedIndexChanged

        mdlPopup1.Show()
        BindSubCategory()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        InsertDetails()
        upPnlcustomerDetail1.Update()
        mdlPopup1.Show()
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        upPnlcustomerDetail1.Update()
        mdlPopup1.Show()

        txt_caption.Text = ""
       
        txt_issuedetails.Text = ""
        lbl_msg.Visible = False
        lbl_msg.Text = ""
    End Sub
    Protected Sub InsertDetails()
        Try
            Dim varParam As New ArrayList
            Dim dsSupplier As New DataSet
            Dim strSort As String = ""

            Dim param(0, 1) As String
            param(0, 0) = "@intUserId"
            param(0, 1) = Session("USER_ID")

            Dim DS_NO As Integer = 0

            Dim strOUT As String = ""
            Dim USERID As Integer
            dsSupplier = objDB.ExecProc_getDataSet("[D_SP_GET_DEATILS_FROM_USER_MSTR_FOR_TRANSCATION]", param)
            Dim dt As New DataTable
            dt = dsSupplier.Tables(0)

            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1

                varParam.Clear()
                varParam.Add("INSERT") 'P0
                'varParam.Add(DS_NO) 'p1
                'varParam.Add("XYZ") 'P2
                varParam.Add(txt_caption.Text) 'p3
                varParam.Add(dt.Rows(i)("D_USER_ID").ToString()) '@P4 D_Issue_Logged_by
                varParam.Add(dt.Rows(i)("D_CIRCLE_ID").ToString()) '@P5 D_CIRCLE_ID
                varParam.Add(dt.Rows(i)("D_CUSTOMER_ID").ToString()) '@P6 D_CUSTOMER_ID
                varParam.Add(dt.Rows(i)("D_DEPT_ID").ToString())   '@P7D_DEPT_ID
                varParam.Add(ddn_category.SelectedValue)  '@P8D_CATEGORY_ID

                varParam.Add(ddn_subcategoryname.SelectedValue)  '@P9 D_SUBCATEGORY_ID

                varParam.Add(txt_issuedetails.Text.Trim) 'P10D_ISSUE_DEATILS
                varParam.Add("Open") '--P10 ISSUE STATUS
                strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_LOGGED_ISSUE", varParam)
                lbl_msg.Visible = True
                lbl_msg.Text = strOUT
                If strOUT = "Record Inserted Successfully!" Then
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class
