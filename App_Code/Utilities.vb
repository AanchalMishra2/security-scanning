Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Public Class Utilities
    Public Shared Function ShowMessage(ByVal strMessage As String, ByVal strMessageType As String) As String
        Dim strMes As String = ""

        If strMessageType = "info" Then
            strMes = "<div class='info'>" & strMessage & "</div>"
        ElseIf strMessageType = "success" Then
            strMes = "<div class='success'>" & strMessage & "</div>"
        ElseIf strMessageType = "warning" Then
            strMes = "<div class='warning'>" & strMessage & "</div>"
        ElseIf strMessageType = "error" Then
            strMes = "<div class='error'>" & strMessage & "</div>"
        End If
        Return strMes

    End Function
    Public Shared Sub AddAtribute(ByVal ctl As ArrayList, ByVal ctlKey As String, ByVal ctlValue As String)
        For Each item As Button In ctl
            item.Attributes.Add(ctlKey, ctlValue)
        Next
    End Sub
   

End Class
