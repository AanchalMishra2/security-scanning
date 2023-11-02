Imports Microsoft.VisualBasic

Public Class Functions
    Public Shared Sub LoadSiteTypes(ByVal ddlSitetype As DropDownList, ByVal intProjectId As String, Optional ByVal flAll As Boolean = False)
        Try
            ddlSitetype.Items.Clear()
            If CInt(intProjectId) = 1 Then
                If flAll Then
                    ddlSitetype.Items.Add(New ListItem("ALL", "ALL"))
                End If
                ddlSitetype.Items.Add(New ListItem("GBT", "GBT"))
                ddlSitetype.Items.Add(New ListItem("RTT", "RTT"))
                ddlSitetype.Items.Add(New ListItem("RTP", "RTP"))
            Else
                If flAll Then
                    ddlSitetype.Items.Add(New ListItem("ALL", "ALL"))
                End If
                ddlSitetype.Items.Add(New ListItem("GBT INDOOR", "GI"))
                ddlSitetype.Items.Add(New ListItem("GBT OUTDOOR", "GO"))
                ddlSitetype.Items.Add(New ListItem("RTT INDOOR", "RTTID"))
                ddlSitetype.Items.Add(New ListItem("RTT OUTDOOR", "RTTOD"))
                ddlSitetype.Items.Add(New ListItem("RTP INDOOR", "PI"))
                ddlSitetype.Items.Add(New ListItem("RTP OUTDOOR", "PO"))
            End If
            ddlSitetype.Items.Insert(0, "--SELECT--")
        Catch ex As Exception
            catch_Exception(ex, "sql err")
        End Try
    End Sub
    Public Shared Sub Insert_Log(ByVal UserID As Integer, ByVal LoginID As String, ByVal LogDesc As String, ByVal IPAddress As String, ByVal ModuleName As String, ByVal strStatus As String, ByVal strGUID As String, ByVal strMODE As String)
        Try
            Dim objClSDB As New clsDB

            Dim intAffected As Integer

            Dim ArrParameters(7, 1) As String

            ArrParameters(0, 0) = "@D_User_Id"

            ArrParameters(0, 1) = UserID

            ArrParameters(1, 0) = "@D_Login_Name"

            ArrParameters(1, 1) = LoginID

            ArrParameters(2, 0) = "@D_LOG_Description"

            ArrParameters(2, 1) = LogDesc

            ArrParameters(3, 0) = "@IP_Address"

            ArrParameters(3, 1) = IPAddress

            ArrParameters(4, 0) = "@D_MODULE_NAME"

            ArrParameters(4, 1) = ModuleName

            ArrParameters(5, 0) = "@Status"

            ArrParameters(5, 1) = strStatus

            ArrParameters(6, 0) = "@MODE"

            ArrParameters(6, 1) = strMODE

            ArrParameters(7, 0) = "@GUID"

            ArrParameters(7, 1) = strGUID

            objClSDB.ExecProc_getRecordsAffected("D_SP_INSERT_USER_LOG", ArrParameters)

            objClSDB = Nothing
        Catch ex As Exception
            catch_Exception(ex, "sql err")
        End Try

    End Sub
    Public Shared Sub InsertErrLog(ByVal uid As String, ByVal page As String, ByVal ex As String)

        Dim objClSDB As New clsDB
        Dim intAffected As Integer
        Dim ArrParameters(2, 1) As String
        ArrParameters(0, 0) = "@User_Id"
        ArrParameters(0, 1) = uid
        ArrParameters(1, 0) = "@page"
        ArrParameters(1, 1) = page
        ArrParameters(2, 0) = "@ex"
        ArrParameters(2, 1) = ex
        Try
            objClSDB.ExecProc_getRecordsAffected("D_SP_INSERT_ERR_LOG", ArrParameters)
        Catch EXT As Exception
            catch_Exception(EXT, "sql err")
        End Try
        objClSDB = Nothing
    End Sub
    Public Shared Sub catch_Exception(ByVal ex As Exception, ByVal page As String)
        Dim err As Exception = ex
        Dim url As String = "ErrorPage.aspx?page=" & page & "&err=" & err.Message.ToString & "\" & Replace(err.StackTrace, System.Environment.NewLine, "").ToString
        System.Web.HttpContext.Current.Response.Redirect(url)
    End Sub
   
End Class
