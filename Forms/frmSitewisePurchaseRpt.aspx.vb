Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.Web.UI.Control
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Partial Class Forms_frmSitewisePurchaseRpt
    Inherits System.Web.UI.Page
    Public objDB As New clsDB
    Dim DRCircle As SqlDataReader
    Dim objDSSiteExcel As DataSet
    Dim dt As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
    Dim DRCustomer As SqlDataReader
    Public fun As New Functions
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
        If Page.IsPostBack = False Then
            Insert_log()
            BindCustomer()
        End If
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlCircle.Items.Clear()
            ddlsiteid.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlsiteid.Items.Add(New ListItem("--SELECT--", 0))
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

    Protected Sub ddlCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            ddlsiteid.Items.Clear()
            ddlsiteid.Items.Add(New ListItem("--SELECT--", 0))
            ddlCircle.Items.Clear()
            BindCircle(ddlCustomer.SelectedValue)
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
            ddlCircle.Items.Clear()
            If DRCircleName.HasRows Then
                ' dgSite.Visible = True
                ddlCircle.DataSource = DRCircleName
                ddlCircle.DataTextField = "D_CIRCLE_NAME"
                ddlCircle.DataValueField = "D_CIRCLE_ID"
                ddlCircle.DataBind()
                DRCircleName.Close()
            Else
                ' lblMessage.Visible = True
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCircle.SelectedIndexChanged
        Try
            ddlsiteid.Items.Clear()
            ddlsiteid.Items.Add(New ListItem("--SELECT--", 0))
            BindSite()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSite()
        Try
            Dim arrParam(1, 1) As String
            Dim DRSite As SqlDataReader
            arrParam(0, 0) = "@intCustomerID"
            arrParam(0, 1) = ddlCustomer.SelectedValue
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = ddlCircle.SelectedValue


            DRSite = objDB.ExecProc_getDataReder("D_SP_GET_SITEID_1", arrParam)
            If DRSite.HasRows Then
                ddlsiteid.DataSource = DRSite
                ddlsiteid.DataTextField = "D_SITE_ID"
                ddlsiteid.DataValueField = "D_SITE_NO"
                ddlsiteid.DataBind()
                DRSite.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        txt_frmdt.Text = ""
        txt_todt.Text = ""
        ddlCustomer.Items.Clear()
        BindCustomer()
    End Sub
End Class
