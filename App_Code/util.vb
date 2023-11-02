Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Namespace DMS.Class


    Public Class util

        Private objDB As New clsDB()
        Private iddlSelectedVal As String


        Public Sub New()
            '
            ' TODO: Add constructor logic here
            ''
        End Sub


        Public Shared Sub tootip(ByVal ddlList As DropDownList)
            For Each _listItem As ListItem In ddlList.Items
                _listItem.Attributes.Add("title", _listItem.Text)
            Next


            ddlList.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title")
        End Sub

        Public Shared Function GetAlphanumeric1() As String
            Dim _str As String = Nothing
            _str = "<script> "
            _str += " function Filtext(e){"
            _str += " var re; "
            _str += "var keychar=window.event? e.keyCode:e.charCode;"
            _str += "if((keychar<=122)&&(keychar>=97) || (keychar<=90)&&(keychar>=65) || (keychar<33)&&(keychar>31)||(keychar<= 57)&&(keychar>=48))"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "if (keychar<32)"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "return false;"
            _str += "}</script>"
            Return (_str)
        End Function
        Public Shared Function GetAlphanumeric() As String
            Dim _str As String = Nothing
            _str = "<script> "
            _str += " function FilterText(e){"
            _str += " var re; "
            _str += "var keychar=window.event? e.keyCode:e.charCode;"
            _str += "if((keychar<=122)&&(keychar>=97) || (keychar<=90)&&(keychar>=65) || (keychar<33)&&(keychar>31)||(keychar<= 57)&&(keychar>=48))"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "if (keychar<32)"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "return false;"
            _str += "}</script>"
            Return (_str)
        End Function

        Public Shared Function GetTextValidatorScript() As String
            Dim _str As String = Nothing
            _str = "<script> "
            _str += " function FilterText(e){"
            _str += " var re; "
            _str += "var keychar=window.event? e.keyCode:e.charCode;"
            _str += "if((keychar<=122)&&(keychar>=97) || (keychar<=90)&&(keychar>=65) || (keychar<33)&&(keychar>31))"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "if (keychar<32)"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "return false;"
            _str += "}</script>"
            Return (_str)
        End Function
        Public Shared Function GetNumericValidatorScript() As String
            Dim _str As String = Nothing
            _str = "<script> "
            _str += " function FilterText(e){"
            _str += " var re; "
            _str += "var keychar=window.event? e.keyCode:e.charCode;"
            _str += "if((keychar<= 57)&&(keychar>=48))"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "if (keychar<32)"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "return false;"
            _str += "}</script>"
            Return (_str)
        End Function

        Public Shared Function GetNumericValidatorScriptWithDot() As String
            Dim _str As String = Nothing
            _str = "<script> "
            _str += " function FilterTextWithDot(e){"
            _str += " var re; "
            _str += "var keychar=window.event? e.keyCode:e.charCode;"
            _str += "if(((keychar<= 57)&&(keychar>=48)) ||((keychar < 47)&&(keychar > 45)) )"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "if (keychar<32)"
            _str += "{"
            _str += "return;"
            _str += "}"
            _str += "return false;"
            _str += "}</script>"
            Return (_str)
        End Function
        Public Sub FillDropDownList(ByVal ddlList As DropDownList, ByVal fieldname As String)

            Dim sql As String = "select f.FieldID,Code,Description,(Description) as cd "
            sql += " from Fields f join fieldmaster fm on f.parentid = fm.fmid "
            sql += "where f.Active=1 and fm.Active=1 and fm.FieldName= '" & fieldname & "' "
            'sql += "order by rowcounter";

            Dim da As New SqlDataAdapter(sql, objDB.GetConnection())

            Dim ds As New DataSet()

            da.Fill(ds, "Fields")

            ddlList.DataValueField = "FieldID"

            ddlList.DataTextField = "cd"
            'iddlSelectedVal = ddlList.DataValueField;
            'ddlList.AutoPostBack = false;

            ddlList.DataSource = ds.Tables("Fields")

            ddlList.DataBind()


            ddlList.Items.Insert(0, New ListItem("---SELECT---", "0"))
        End Sub
        Public Sub FillListBox(ByVal ddlselVal As Integer, ByVal lstList As ListBox, ByVal fieldname As String)
            Dim uStrsql As String
            If ddlselVal = 0 Then
                uStrsql = "select f.FieldID,Code,Description,(Description) as cd "
                uStrsql += " from Fields f join fieldmaster fm on f.parentid = fm.fmid "

                uStrsql += "where f.Active=1  and fm.Active=1 and fm.FieldName= '" & fieldname & "' "
            Else
                'string sql = "select f.FieldID,Code,Description,(Description) as cd ";
                'sql += " from Fields f join fieldmaster fm on f.parentid = fm.fmid ";
                'sql += "where f.Active=1 and fm.Active=1 and fm.FieldName= '" + fieldname + "' ";
                uStrsql = "select f.FieldID,Code,Description,(Description) as cd"
                uStrsql += " from Fields f join fieldmaster fm on f.parentid = fm.fmid"
                uStrsql += " where f.Active=1 and fm.Active=1 and fm.FieldName= '" & fieldname & "'"
                uStrsql += " and F.FieldID not in ( select sd.T_STREAM_ID from T_STREAM_DOMAIN sd where sd.t_domain_id = '" & ddlselVal & "')"
            End If

            Dim da As New SqlDataAdapter(uStrsql, objDB.GetConnection())

            Dim ds As New DataSet()

            da.Fill(ds, "Fields")
            lstList.DataValueField = "FieldID"
            lstList.DataTextField = "cd"
            'ddlList.AutoPostBack = false;

            lstList.DataSource = ds.Tables("Fields")


            'lstList.Items.Insert(0));

            lstList.DataBind()
        End Sub
        Public Sub FillListBox(ByVal lstList As ListBox, ByVal fieldname As String)
            Dim uStrsql As String
            uStrsql = "select f.FieldID,Code,Description,(Description) as cd "
            uStrsql += " from Fields f join fieldmaster fm on f.parentid = fm.fmid "
            uStrsql += "where f.Active=1 and fm.Active=1 and fm.FieldName= '" & fieldname & "' "

            Dim da As New SqlDataAdapter(uStrsql, objDB.GetConnection())

            Dim ds As New DataSet()

            da.Fill(ds, "Fields")
            lstList.DataValueField = "FieldID"
            lstList.DataTextField = "cd"
            'ddlList.AutoPostBack = false;
            lstList.DataSource = ds.Tables("Fields")

            'lstList.Items.Insert(0));

            lstList.DataBind()
        End Sub
        Public Shared Function GetString(ByVal Value As String, ByVal input As String) As String
            Dim returnvalue As String = Nothing
            Select Case Value
                Case ("Begins With")
                    returnvalue = " like '" & input & "%'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Does Not Begin With")
                    returnvalue = " not like '" & input & "%'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select

                Case ("Ends With")
                    returnvalue = " like '%" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Does Not End With")
                    returnvalue = " not like '%" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Contains")
                    returnvalue = " like '%" & input & "%'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select

                Case ("Does Not Contain")
                    returnvalue = " not like '%" & input & "%'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Equals To")
                    returnvalue = " = '" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Exact Match")
                    returnvalue = " = '" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select

                Case ("Less Than")
                    returnvalue = " < '" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Greater Than")
                    returnvalue = " > '" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select

                Case ("In List")
                    returnvalue = "'" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
                Case ("Not Equal To")
                    returnvalue = " <> '" & input & "'"
                    Exit Select
                    ' TODO: might not be correct. Was : Exit Select
            End Select
            Return (returnvalue)
        End Function


    End Class
End Namespace


