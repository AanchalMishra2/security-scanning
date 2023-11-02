Imports System.Data
Imports System.Data.SqlClient
Imports clsDB
Partial Class Test
    Inherits System.Web.UI.Page
    Dim objDB As clsDB

    Protected Sub btn_login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_login.Click


        ' objDB.ExecProc_getDataSet_noParam("")
        Try
            Dim Params As New ArrayList
            Params.Add(txtName.Text) 'P0
            Params.Add(txtPassword.Text) 'P1
            objDB = New clsDB

            Dim strOUT As String
            strOUT = objDB.ExecProc_getStatus("D_SP_CHECKLOGIN", Params)
            If strOUT = "SuccessfulLogin" Then
                Call SaveUserDetails()
                Response.Redirect("~/Forms/frmMain.aspx")
            Else
                lblError.Visible = True
                lblError.Text = strOUT
                
            End If

        Catch ex As Exception

        End Try
    End Sub

  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sAlert As String = "<SCRIPT language='Javascript'>document.forms[0]['txtName'].focus();</script>"
        '' Page.RegisterStartupScript("NoAnswer", sAlert)
        'Response.Write(sAlert)
        '' Page.ClientScript.RegisterStartupScript(Page.[GetType](), "NoAnswer", sAlert, True)
        txtName.Focus()
        Session.Clear()
    End Sub
    Private Sub SaveUserDetails()
        Dim dr As SqlDataReader
        objDB = New clsDB
        Dim qry As String
        qry = "SELECT * FROM D_VW_USER " & _
        " WHERE D_USER_EMP_ID = '" & txtName.Text.Trim & "'"
        'qry = "SELECT * FROM T_VW_USER " & _
        '" WHERE T_LOGIN_NAME = 'Administrator'"
        dr = objDB.getReader(qry)
        If dr.HasRows Then
            dr.Read()
            Session.Item("User_ID") = dr.Item(0).ToString
            Session.Item("User_Emp_ID") = dr.Item(1).ToString
            Session.Item("User_Login_Name") = dr.Item(2).ToString
            Session.Item("User_Password") = dr.Item(3).ToString
            Session.Item("User_FName") = dr.Item(4).ToString
            Session.Item("User_LName") = dr.Item(5).ToString
            Session.Item("User_DesigId") = dr.Item(6).ToString
            Session.Item("Role_ID") = dr.Item(8).ToString
            Session.Item("User_Role") = dr.Item(9).ToString
            Session.Item("User_Email") = dr.Item(10).ToString
            Session.Item("User_Active") = dr.Item(11).ToString

        End If
        objDB.CloseConnection()
    End Sub

    Protected Sub btn_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        txtName.Text = ""
        txtPassword.Text = ""
        lblError.Visible = False
    End Sub
End Class
