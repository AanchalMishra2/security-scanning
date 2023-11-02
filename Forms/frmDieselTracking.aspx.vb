Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Math
Partial Class Forms_frmDieselTracking
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
        lblwarning.Text = ""
        pnlimport.Visible = False
        If Page.IsPostBack = False Then
            Insert_log()
            'BindCircle()
            BindCustomer()
            BindDDL()
        End If
    End Sub
    Private Sub BindDDL()
        Try
            ddlModeOfPayment.Items.Clear()
            ddlVendor.Enabled = False
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False
            ddlModeOfPayment.Items.Add(New ListItem("--SELECT--", 0))
            ddlModeOfPayment.Items.Add(New ListItem("DD", 1))
            ddlModeOfPayment.Items.Add(New ListItem("CASH", 2))
            ddlModeOfPayment.Items.Add(New ListItem("PETRO CARDS", 3))
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindCustomer()
        Try
            Dim custParam(0, 1) As String
            custParam(0, 0) = "@intUserId"
            custParam(0, 1) = CInt(Session("USER_ID"))
            DRCustomer = objDB.ExecProc_getDataReder("D_SP_GET_CUSTOMER", custParam)
            ddlCircle.Items.Clear()
            ddlLocation.Items.Clear()
            ddlSiteId.Items.Clear()
            ddlVendor.Items.Clear()
            ddlPetrocards.Items.Clear()
            ddlSubPetroCard.Items.Clear()
            ddlNamePP.Items.Clear()
            ddlCircle.Items.Add(New ListItem("--SELECT--", 0))
            ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlPetrocards.Items.Add(New ListItem("--SELECT--", 0))
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
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

    Protected Sub lnktemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnktemplate.Click
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/octet-stream"
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & System.IO.Path.GetFileName(Server.MapPath("../Templates/DIESEL_TRACKING.csv")))
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.WriteFile(Server.MapPath("../Templates/DIESEL_TRACKING.csv"))
            HttpContext.Current.Response.End()
        Catch ex As Exception
            ' fun.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Dim err As Exception = Server.GetLastError
        Dim url As String = "ErrorPage.aspx?page=" & Replace(Page.ToString.Trim, "ASP.", " ") & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        Response.Redirect(url)
    End Sub

    Protected Sub ddlCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            ddlVendor.Enabled = False
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False
            ddlLocation.Items.Clear()
            ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            ddlModeOfPayment.Items.Clear()
            ddlModeOfPayment.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Clear()
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlSubPetroCard.Items.Clear()
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            ddlNamePP.Items.Clear()
            ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            'ddlModeOfPayment.Items.Clear()
            'BindDDL()
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
            'ddlLocation.Items.Clear()
            'ddlSiteId.Items.Clear()
            'ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            'ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            'txtSiteName.Text = ""
            'ddlVendor.Items.Clear()
            'ddlSubPetroCard.Items.Clear()
            'ddlVendor.Items.Add("--SELECT--")
            'ddlSubPetroCard.Items.Add("--SELECT--")
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
            ddlVendor.Enabled = False
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False

            'ddlLocation.Items.Clear()
            'ddlLocation.Items.Add(New ListItem("--SELECT--", 0))
            ddlSiteId.Items.Clear()
            ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            ddlModeOfPayment.Items.Clear()
            ddlModeOfPayment.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Clear()
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlSubPetroCard.Items.Clear()
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            ddlNamePP.Items.Clear()
            ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            BindLocation(ddlCircle.SelectedValue)

            'txtSiteName.Text = ""
            'ddlVendor.Items.Clear()
            'ddlSubPetroCard.Items.Clear()
            'ddlModeOfPayment.Items.Clear()
            'ddlVendor.Items.Add("--SELECT--")
            'ddlSubPetroCard.Items.Add("--SELECT--")
            'BindDDL()
            'BindLocation(ddlCircle.SelectedValue)
            'ddlLocation.SelectedValue = 0
            'BindVendor()
            'ddlVendor.SelectedValue = 0
            'BindPetroCards()
            'ddlPetrocards.SelectedValue = 0
            ''BindSubPetroCards()
            ''ddlSubPetroCard.SelectedValue = 0

            'BindPump()
            'ddlNamePP.SelectedValue = 0
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindPetroCards()
      Try
            Dim drPetroCard As SqlDataReader
            Dim arrParam(2, 1) As String
            arrParam(0, 0) = "@intcustomerId"
            arrParam(0, 1) = ddlCustomer.SelectedValue
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = ddlCircle.SelectedValue
            arrParam(2, 0) = "@intVendorID"
            arrParam(2, 1) = ddlVendor.SelectedValue
            drPetroCard = objDB.ExecProc_getDataReder("D_SP_GET_PETRO_CARDS_FOR_DROPDOWN", arrParam)
            If drPetroCard.HasRows Then
                ddlPetrocards.DataSource = drPetroCard
                ddlPetrocards.DataTextField = "D_CARD_NO"
                ddlPetrocards.DataValueField = "D_C_NO"
                ddlPetrocards.DataBind()
                drPetroCard.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindPump()
        Try
            Dim arrParam(2, 1) As String
            Dim DRPump As SqlDataReader
            arrParam(0, 0) = "@intCustomerId"
            arrParam(0, 1) = ddlCustomer.SelectedValue
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = ddlCircle.SelectedValue
            arrParam(2, 0) = "@strlocation"
            arrParam(2, 1) = ddlLocation.SelectedItem.Text
            DRPump = objDB.ExecProc_getDataReder("D_SP_GET_PETROL_PUMP_FOR_DIESEL", arrParam)
            ddlNamePP.Items.Clear()
            If DRPump.HasRows Then
                ddlNamePP.DataSource = DRPump
                ddlNamePP.DataTextField = "D_PUMP_NAME"
                ddlNamePP.DataValueField = "D_PUMP_ID"
                ddlNamePP.DataBind()
                DRPump.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindLocation(ByVal circle As String)
        Try
            Dim DRVendor As SqlDataReader
            Dim circleParam(0, 1) As String
            circleParam(0, 0) = "@intCircleId"
            circleParam(0, 1) = circle
            DRVendor = objDB.ExecProc_getDataReder("D_SP_GET_LOCATION_FOR_DROPDOWN", circleParam)
            ddlLocation.Items.Clear()
            'ddlSiteId.Items.Clear()
            'ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            'txtSiteName.Text = ""
            If DRVendor.HasRows Then
                ddlLocation.DataSource = DRVendor
                ddlLocation.DataTextField = "D_LOCATION_NAME"
                ddlLocation.DataValueField = "D_LOCATION_ID"
                ddlLocation.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Public Sub BindVendor()
        Try
            Dim DRVendor As SqlDataReader
            DRVendor = objDB.getReader_Sproc_NoParam("D_SP_GET_VENDOR_FOR_DROPDOWN")
            If DRVendor.HasRows Then
                ddlVendor.DataSource = DRVendor
                ddlVendor.DataTextField = "D_VENDOR_NAME"
                ddlVendor.DataValueField = "D_VENDOR_ID"
                ddlVendor.DataBind()
                DRVendor.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Try
            ddlVendor.Enabled = False
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False
            'txtSiteName.Text = ""

            'ddlSiteId.Items.Clear()
            'ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            ddlModeOfPayment.Items.Clear()
            ddlModeOfPayment.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Clear()
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlSubPetroCard.Items.Clear()
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            ddlNamePP.Items.Clear()
            ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            BindSite(ddlCustomer.SelectedValue, ddlCircle.SelectedValue, ddlLocation.SelectedItem.ToString)
            'ddlVendor.Items.Clear()
            'ddlSubPetroCard.Items.Clear()
            'ddlVendor.Items.Add("--SELECT--")
            'ddlSubPetroCard.Items.Add("--SELECT--")
            'ddlModeOfPayment.Items.Clear()
            'BindDDL()
            'BindPump()
            'ddlLocation.Enabled = True
            'ddlNamePP.Enabled = True
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSite(ByVal custid As String, ByVal circleid As String, ByVal cluster As String)
        Try
            Dim arrParam(2, 1) As String
            Dim DRSite As SqlDataReader
            arrParam(0, 0) = "@intCustomerID"
            arrParam(0, 1) = custid
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = circleid
            arrParam(2, 0) = "@cluster"
            arrParam(2, 1) = cluster
            DRSite = objDB.ExecProc_getDataReder("D_SP_GET_SITEID", arrParam)
            If DRSite.HasRows Then
                ddlSiteId.DataSource = DRSite
                ddlSiteId.DataTextField = "D_SITE_ID"
                ddlSiteId.DataValueField = "D_SITE_NO"
                ddlSiteId.DataBind()
                DRSite.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlModeOfPayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModeOfPayment.SelectedIndexChanged
        Try
            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                ddlNamePP.Items.Clear()
                ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
                ddlVendor.Items.Clear()
                ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
                ddlSubPetroCard.Items.Clear()
                ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
                ddlVendor.Enabled = True
                reqvendor.Enabled = True
                ddlSubPetroCard.Enabled = True
                reqSubPetroCard.Enabled = True
                ddlNamePP.Enabled = False
                reqNamePP.Enabled = False
                'ddlLocation.Enabled = True
                'BindPump()
                'ddlNamePP.SelectedValue = 0
                'ddlNamePP.Enabled = False
                BindVendor()
                'ddlVendor.SelectedValue = 0
                ' BindSubPetroCards()
                ' ddlSubPetroCard.SelectedValue = 0
                'BindPetroCards()
                'ddlPetrocards.SelectedValue = 0
                'ddlVendor.Enabled = True
                'ddlPetrocards.Enabled = False
                'ddlSubPetroCard.Enabled = True
            End If
            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "DD" Then
                ddlNamePP.Items.Clear()
                ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
                ddlVendor.Items.Clear()
                ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
                ddlSubPetroCard.Items.Clear()
                ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
                'BindLocation()
                'ddlLocation.SelectedValue = 0
                ddlNamePP.Enabled = True
                reqNamePP.Enabled = True
                'ddlLocation.Enabled = True
                BindPump()
                'ddlNamePP.SelectedValue = 0
                ' ddlNamePP.Enabled = True
                'BindVendor()
                ' ddlVendor.SelectedValue = 0
                ' BindPetroCards()
                ' ddlPetrocards.SelectedValue = 0
                ' BindSubPetroCards()
                ' ddlSubPetroCard.SelectedValue = 0
                ddlVendor.Enabled = False
                reqvendor.Enabled = False
                ' ddlPetrocards.Enabled = False
                ddlSubPetroCard.Enabled = False
                reqSubPetroCard.Enabled = False
            End If
            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "CASH" Then
                ddlNamePP.Items.Clear()
                ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
                ddlVendor.Items.Clear()
                ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
                ddlSubPetroCard.Items.Clear()
                ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
                ' BindLocation()
                ' ddlLocation.SelectedValue = 0
                'ddlLocation.Enabled = True
                'BindPump()
                'ddlNamePP.SelectedValue = 0
                'ddlNamePP.Enabled = False
                'BindVendor()
                'ddlVendor.SelectedValue = 0
                'BindPetroCards()
                'ddlPetrocards.SelectedValue = 0
                ddlVendor.Enabled = False
                reqvendor.Enabled = False
                ddlNamePP.Enabled = False
                reqNamePP.Enabled = False
                ddlSubPetroCard.Enabled = False
                reqSubPetroCard.Enabled = False

            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlVendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVendor.SelectedIndexChanged
        Try
            ddlSubPetroCard.Items.Clear()
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            BindSubPetroCards()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub BindSubPetroCards()
        Try
            Dim arrParam(2, 1) As String
            Dim DRPetroCard As SqlDataReader
            arrParam(0, 0) = "@intCustomerId"
            arrParam(0, 1) = ddlCustomer.SelectedValue
            arrParam(1, 0) = "@intCircleID"
            arrParam(1, 1) = ddlCircle.SelectedValue
            arrParam(2, 0) = "@intVendorID"
            arrParam(2, 1) = ddlVendor.SelectedValue
            DRPetroCard = objDB.ExecProc_getDataReder("D_SP_GET_SUBPETRO_CARDS_FOR_DROPDOWN", arrParam)
            ddlSubPetroCard.Items.Clear()
            If DRPetroCard.HasRows Then
                ddlSubPetroCard.DataSource = DRPetroCard
                ddlSubPetroCard.DataTextField = "D_S_PETROCARD_NO"
                ddlSubPetroCard.DataValueField = "D_S_PETRO_SR_NO"
                ddlSubPetroCard.DataBind()
                DRPetroCard.Close()
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub ddlSiteId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSiteId.SelectedIndexChanged
        Try

            ddlVendor.Enabled = False
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False

            'ddlSiteId.Items.Clear()
            'ddlSiteId.Items.Add(New ListItem("--SELECT--", 0))
            txtSiteName.Text = ""
            ddlModeOfPayment.Items.Clear()
            ddlModeOfPayment.Items.Add(New ListItem("--SELECT--", 0))
            ddlVendor.Items.Clear()
            ddlVendor.Items.Add(New ListItem("--SELECT--", 0))
            ddlSubPetroCard.Items.Clear()
            ddlSubPetroCard.Items.Add(New ListItem("--SELECT--", 0))
            ddlNamePP.Items.Clear()
            ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            'BindLocation(ddlCircle.SelectedValue)
            'ddlNamePP.Items.Clear()
            'ddlNamePP.Items.Add(New ListItem("--SELECT--", 0))
            'txtSiteName.Text = ""
            Dim varParam1(1, 1) As String
            Dim dsSite As New DataSet
            varParam1(0, 0) = "@intCircleID"
            varParam1(0, 1) = ddlCircle.SelectedValue
            varParam1(1, 0) = "@strSiteID"
            varParam1(1, 1) = ddlSiteId.SelectedItem.Text
            dsSite = objDB.ExecProc_getDataSet("D_SP_GET_SITENAME", varParam1)
            ddlModeOfPayment.Items.Clear()
            BindDDL()
            If dsSite.Tables(0).Rows.Count > 0 Then
                txtSiteName.Text = CStr(dsSite.Tables(0).Rows(0)(0))
                'txtClusterName.Text = dsSite.Tables(0).Rows(0)(2)
            Else
                txtSiteName.Text = ""

            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim Params As New ArrayList

        If txtdiselQty.Text <> "" Then
            If floatvalidate(txtdiselQty.Text, "Diesel Quantity") = True Then
                If txtdistlQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Diesel Quntity is there then Distilled Quantity must be blank.') </script>")
                    lblwarning.Visible = True
                    lblwarning.Text = "If Diesel Quntity is there then Distilled Quantity must be blank."
                    Exit Sub
                End If
                If txtCotWestQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Diesel Quntity is there then Cotton Waste Quantity must be blank.') </script>")
                    lblwarning.Visible = True
                    lblwarning.Text = "If Diesel Quntity is there then Cotton Waste Quantity must be blank."
                    Exit Sub
                End If
                If txtOilQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Diesel Quntity is there then Oil Quantity must be blank.') </script>")
                    lblwarning.Visible = True
                    lblwarning.Text = "If Diesel Quntity is there then Oil Quantity must be blank."
                    Exit Sub
                End If
                If txtCollentQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Diesel Quntity is there then Collent Quantity must be blank.') </script>")
                    lblwarning.Visible = True
                    lblwarning.Text = "If Diesel Quntity is there then Collent Quantity must be blank."
                    Exit Sub
                End If
            Else
                lblwarning.Visible = True
                lblwarning.Text = "Diesel Qty Must be Numeric"
                Exit Sub
            End If

        End If
        If txtOilQty.Text <> "" Then
            If floatvalidate(txtOilQty.Text, "Oil Quantity") = True Then
                If txtdiselQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Oil Quantity is there then Diesel Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Oil Quantity is there then Diesel Quantity must be blank."
                    Exit Sub
                End If
                If txtdistlQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Oil Quantity is there then Distilled Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Oil Quantity is there then Distilled Quantity must be blank."
                    Exit Sub
                End If
                If txtCotWestQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Oil Quantity is there then Cotton Waste Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Oil Quantity is there then Cotton Waste Quantity must be blank."
                    Exit Sub
                End If
                If txtCollentQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Oil Quantity is there then Collent Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Oil Quantity is there then Collent Quantity must be blank."
                    Exit Sub
                End If
            Else
                lblwarning.Visible = True
                lblwarning.Text = "Oil Qty Must be Numeric"
                Exit Sub
            End If
        End If

        If txtdistlQty.Text <> "" Then
            If floatvalidate(txtdistlQty.Text, "Distilled Quantity") = True Then
                If txtdiselQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Distilled Quntity is there then Diesel Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Distilled Quntity is there then Diesel Quantity must be blank."
                    Exit Sub
                End If
                If txtCotWestQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Distilled Quntity is there then Cotton Waste Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Distilled Quntity is there then Cotton Waste Quantity must be blank."
                    Exit Sub
                End If
                If txtOilQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Distilled Quntity is there then Oil Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Distilled Quntity is there then Oil Quantity must be blank."
                    Exit Sub
                End If
                If txtCollentQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Distilled Quntity is there then Collent Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Distilled Quntity is there then Collent Quantity must be blank."
                    Exit Sub
                End If
            Else
                lblwarning.Visible = True
                lblwarning.Text = "Distilled Qty Must be Numeric"
                Exit Sub
            End If

        End If
        If txtCollentQty.Text <> "" Then
            If floatvalidate(txtCollentQty.Text, "Collent Quantity") = True Then
                If txtdiselQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Collent Quantity is there then Diesel Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Collent Quantity is there then Diesel Quantity must be blank."
                    Exit Sub
                End If
                If txtdistlQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Collent Quantity is there then Distilled Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Collent Quantity is there then Distilled Quantity must be blank."
                    Exit Sub
                End If
                If txtCotWestQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Collent Quantity is there then Cotton Waste Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Collent Quantity is there then Cotton Waste Quantity must be blank."
                    Exit Sub
                End If
                If txtOilQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Collent Quantity is there then oil Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Collent Quantity is there then oil Quantity must be blank."
                    Exit Sub
                End If
            Else
                lblwarning.Visible = True
                lblwarning.Text = "Collent Qty Must be Numeric"
                Exit Sub
            End If
        End If

        If txtCotWestQty.Text <> "" Then
            If floatvalidate(txtCotWestQty.Text, "Cotton West Quantity") = True Then
                If txtdiselQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Cotton Waste Quantity is there then Diesel Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Cotton Waste Quantity is there then Diesel Quantity must be blank."
                    Exit Sub
                End If
                If txtdistlQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Cotton Waste Quantity is there then Distilled Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Cotton Waste Quantity is there then Distilled Quantity must be blank."
                    Exit Sub
                End If
                If txtOilQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If  Cotton Waste Quantity is there then Oil Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If  Cotton Waste Quantity is there then Oil Quantity must be blank."
                    Exit Sub
                End If
                If txtCollentQty.Text <> "" Then
                    'Page.RegisterStartupScript("NoAnswer", "<script> alert('If Cotton Waste Quantity is there then Collent Quantity must be blank.') </script>")
                    lblwarning.Visible = False
                    lblwarning.Text = "If Cotton Waste Quantity is there then Collent Quantity must be blank."
                    Exit Sub
                End If
            Else
                lblwarning.Visible = True
                lblwarning.Text = "Collent Qty Must be Numeric"
                Exit Sub
            End If
        End If
        If txtdiselQty.Text = "" And txtOilQty.Text = "" And txtdistlQty.Text = "" And txtCollentQty.Text = "" And txtCotWestQty.Text = "" Then
            'Page.RegisterStartupScript("NoAnswer", "<script> alert('Select any One Quantity field') </script>")
            lblwarning.Visible = True
            lblwarning.Text = "Select any One Quantity field"
            Exit Sub
        End If
        If txtdiselQty.Text <> "" Then

            txtTotalAmt.Text = txtdiselQty.Text * txtRate.Text
        End If
        If txtdistlQty.Text <> "" Then
            txtTotalAmt.Text = txtdistlQty.Text * txtRate.Text
        End If
        If txtOilQty.Text <> "" Then
            txtTotalAmt.Text = txtOilQty.Text * txtRate.Text
        End If
        If txtCollentQty.Text <> "" Then
            txtTotalAmt.Text = txtCollentQty.Text * txtRate.Text
        End If
        If txtCotWestQty.Text <> "" Then
            txtTotalAmt.Text = txtCotWestQty.Text * txtRate.Text
        End If

        Try
            Params.Add("Insert") 'P0
            Params.Add(ddlCircle.SelectedValue) 'P1

            Params.Add(txtSiteName.Text.Trim) 'P2
            Params.Add(ddlSiteId.SelectedItem.Text) 'P3
            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "DD" Then
                Params.Add(ddlNamePP.SelectedValue) 'P4
            Else
                Params.Add(0) 'P4
            End If
            Params.Add(ddlLocation.SelectedItem.ToString.Trim) 'P5
            Params.Add(txtBillNumber.Text.Trim) 'P6
            Params.Add(txtBillDate.Text.Trim) 'P7
            If txtdiselQty.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtdiselQty.Text.Trim)) 'P8
            End If

            If txtOilQty.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtOilQty.Text.Trim)) 'P9
            End If

            If txtdistlQty.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtdistlQty.Text.Trim)) 'P10
            End If

            If txtCollentQty.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtCollentQty.Text.Trim)) 'P11
            End If

            If txtCotWestQty.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtCotWestQty.Text.Trim)) 'P12
            End If

            'Params.Add(CDbl(txtRate.Text.Trim)) 'P13
            If txtRate.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtRate.Text.Trim)) 'P13
            End If
            'Params.Add(CDbl(txtTotalAmt.Text.Trim)) 'P14
            If txtTotalAmt.Text.Trim = "" Then
                Params.Add(CDbl(0))
            Else
                Params.Add(CDbl(txtTotalAmt.Text.Trim)) 'P14
            End If
            Params.Add(txtHMR.Text.Trim) 'P15
            Params.Add(txtGTLDate.Text.Trim) 'P16
            Params.Add(ddlModeOfPayment.SelectedItem.Text) 'P17

            If ddlVendor.SelectedItem.Text = "BPCL" Then
                Params.Add(ddlSubPetroCard.SelectedItem.Text) 'P18
            Else
                ' Params.Add(ddlPetrocards.SelectedItem.Text) 'P18
                If ddlSubPetroCard.SelectedItem.Text = "--SELECT--" Then
                    Params.Add("0")
                Else
                    Params.Add(ddlSubPetroCard.SelectedItem.Text) 'p18
                End If
            End If

            Params.Add("") 'P19
            Params.Add("") 'P20
            Params.Add("") 'P21
            Params.Add("") 'P22
            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "PETRO CARDS" Then
                Params.Add(ddlVendor.SelectedValue)   'P23
            Else
                Params.Add(0)   'P23
            End If

            If ddlModeOfPayment.SelectedItem.Text.ToUpper = "DD" Then
                Params.Add(ddlLocation.SelectedValue)   'P24
            Else
                Params.Add(0)   'P24
            End If
            Params.Add(CInt(Session("USER_ID")))   'P25
            Params.Add(ddlCustomer.SelectedValue) 'P26

            objDB = New clsDB
            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_INSERT_DIESEL_TRACKING", Params)
            ' Dim sAlert2 As String = "<SCRIPT language='Javascript'>alert('" & strOUT & "')</script>"
            'Page.RegisterStartupScript("NoAnswer", sAlert2)
            lblwarning.Visible = True
            lblwarning.Text = strOUT.ToString
            ClearFields()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Function floatvalidate(ByVal str As String, ByVal msg As String) As Boolean
        Try
            'this will validate float and int valuess
            ' [0-9]+(\.[0-9]+)?$
            '"^([.]|[0-9])[0-9]*[.]*[0-9]+$"
            If Regex.IsMatch(str, "^[0-9]+(\.[0-9]+)?$") Then
                Return True
            Else
                ' Dim strmsg As String = msg & " must be Float."
                'show_alert(strmsg)
                Return False
            End If
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Function

    Protected Sub import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles import.Click
        Dim FilePath As String
        Dim FileName As String

        Try
            Dim ext As String = System.IO.Path.GetExtension(Me.fileInput.PostedFile.FileName)
            If ext = "" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                lblwarning.Text = "Please Select File"
                lblwarning.Visible = True
                'Page.RegisterStartupScript("Nodata", sAlert)
            ElseIf ext.ToLower <> ".csv" Then
                Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select .csv File')</script>"
                ' Page.RegisterStartupScript("Nodata", sAlert)
                lblwarning.Text = "Please Select .csv File"
                lblwarning.Visible = True
            Else
                FilePath = Left(System.IO.Path.GetFullPath(fileInput.PostedFile.FileName), System.IO.Path.GetFullPath(fileInput.PostedFile.FileName).LastIndexOf("\"))
                FileName = System.IO.Path.GetFileName(fileInput.PostedFile.FileName)
                If fileInput.PostedFile.FileName = "" Then
                    Dim sAlert As String = "<SCRIPT language='Javascript'>alert('Please Select File')</script>"
                    '    Page.RegisterStartupScript("Nodata", sAlert)
                    lblwarning.Text = "Please Select File"
                    lblwarning.Visible = True
                    Exit Sub
                Else
                    fileInput.PostedFile.SaveAs(Server.MapPath("../DieselData/" & FileName))
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
                    lblwarning.Text = "No Records found in excel sheet"
                    lblwarning.Visible = True
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
                        Dim billdate As String() = (dtRow.Item(7).ToString.Trim).Split(" ")
                        Dim subtogil As String() = (dtRow.Item(16).ToString.Trim).Split(" ")
                        i = i + 1
                        Dim Params As New ArrayList
                        Params.Add(dtRow.Item(0).ToString.Trim) 'P0
                        Params.Add(dtRow.Item(1).ToString.Trim) 'P1
                        Params.Add(dtRow.Item(2).ToString.Trim) 'P2
                        Params.Add(dtRow.Item(3).ToString.Trim) 'P3
                        Params.Add(dtRow.Item(4).ToString.Trim) 'P4
                        Params.Add(dtRow.Item(5).ToString.Trim) 'P5
                        Params.Add(dtRow.Item(6).ToString.Trim) 'P6
                        Params.Add(billdate(0)) 'P7
                        Params.Add(dtRow.Item(8).ToString.Trim) 'P8
                        Params.Add(dtRow.Item(9).ToString.Trim) 'P9
                        Params.Add(dtRow.Item(10).ToString.Trim) 'P10
                        Params.Add(dtRow.Item(11).ToString.Trim) 'P11
                        Params.Add(dtRow.Item(12).ToString.Trim) 'P12
                        Params.Add(dtRow.Item(13).ToString.Trim) 'P13
                        Params.Add(dtRow.Item(14).ToString.Trim) 'P14
                        Params.Add(dtRow.Item(15).ToString.Trim) 'P15
                        Params.Add(subtogil(0)) 'P16
                        Params.Add(dtRow.Item(17).ToString.Trim) 'P17
                        Params.Add(dtRow.Item(18).ToString.Trim) 'P18
                        Params.Add(dtRow.Item(19).ToString.Trim) 'P19
                        Params.Add(CInt(Session("USER_ID")))   'P20
                        Params.Add(dtRow.Item(20).ToString.Trim) 'P21

                        Dim strOUT As String
                        strOUT = objDB.ExecProc_getStatus("D_SP_IMPORT_DIESEL_DATA", Params)
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

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Try
            ClearFields()
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
    Private Sub ClearFields()
        'BindCircle()
        Try
            ddlNamePP.Enabled = False
            ddlSubPetroCard.Enabled = False
            ddlVendor.Enabled = False
            BindCustomer()
            txtBillNumber.Text = ""
            txtBillDate.Text = ""
            txtdiselQty.Text = ""
            txtOilQty.Text = ""
            txtSiteName.Text = ""
            txtCollentQty.Text = ""
            txtRate.Text = ""
            txtHMR.Text = ""
            ddlModeOfPayment.SelectedIndex = 0
            txtdistlQty.Text = ""
            txtCotWestQty.Text = ""
            txtTotalAmt.Text = ""
            txtGTLDate.Text = ""
            ddlPetrocards.SelectedIndex = 0
        Catch ex As Exception
            Functions.catch_Exception(ex, Page.ToString.Trim)
        End Try
    End Sub
End Class
