Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI
Imports System.Configuration

Public Class clsDB
    Dim objCmd As SqlCommand
    Public intTransID As New Integer
    Public SqlConn As New SqlConnection
    'Public Connectionstring As String = System.Configuration.ConfigurationSettings.AppSettings("ConnString").ToString 
    Public Connectionstring As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnString").ToString
    Public Function GetConnection() As SqlConnection
        Try
            '*******************************************************************
            '* Created By ABHILASH JHARIYA
            '* Date: 18/06/2008
            '* Purpose : Open Sql Connection
            '* Modified date:
            '* Modified By :
            '*******************************************************************
            If SqlConn.State <> ConnectionState.Open Then
                SqlConn.ConnectionString = Connectionstring.Trim
                SqlConn.Open()

            Else

            End If


        Catch ex As SqlException

        End Try
        Return SqlConn
    End Function

    Public Sub CloseConnection()
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Close SQl Connection
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Try
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
                SqlConn.Dispose() 'added tc on 04-09-2006
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Public Function getReader(ByVal qry As String) As SqlDataReader
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Returns DataReader
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim SQLCmd As SqlCommand
        Dim DR As SqlDataReader = Nothing

        Try
            CloseConnection()
            SQLCmd = New SqlCommand(qry, GetConnection)
            DR = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)

        Catch ex As Exception

        Finally
            ' DR.Close()
        End Try
        Return DR
    End Function

    Public Function getdataset(ByVal qry As String) As DataSet
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Returns DataSet
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim SQLCmd As New SqlCommand
        Dim ds As New DataSet
        Try
            
            CloseConnection()
            SQLCmd.Connection = GetConnection()
            SQLCmd.CommandText = qry
            Dim da As New SqlDataAdapter(SQLCmd)
            da.Fill(ds)
            CloseConnection()

        Catch ex As Exception
        Finally
            ' ds.Clear()
        End Try
        Return ds
    End Function
    Public Sub getDataInGrid(ByVal datagrid As DataGrid, ByVal qry As String)
        Dim objDS As New DataSet
        CloseConnection()
        objDS = getdataset(qry)
        datagrid.DataSource() = objDS
        datagrid.DataBind()
        CloseConnection()
    End Sub
    Public Sub getDataInGrid1(ByVal datagrid As DataGrid, ByVal Stored_Proc_name As String)
        Dim objDS As New DataSet
        CloseConnection()
        objDS = getDataSet_SProc(Stored_Proc_name)
        datagrid.DataSource() = objDS
        datagrid.DataBind()
        CloseConnection()
    End Sub
    Public Sub getDataInGrid2(ByVal datagrid As DataGrid, ByVal Stored_Proc_name As String, ByVal varParam(,) As Object)
        Dim objDS As New DataSet
        CloseConnection()
        objDS = ExecProc_getDataSet(Stored_Proc_name, varParam)
        datagrid.DataSource() = objDS
        datagrid.DataBind()
        CloseConnection()
    End Sub


    Public Function getReader_Sproc_NoParam(ByVal Stored_Proc_name As String) As SqlDataReader
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Returns DataReader using stored procedure
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim SQLCmd As SqlCommand
        Dim DR As SqlDataReader = Nothing
        Try
            CloseConnection()
            SQLCmd = New SqlCommand
            SQLCmd.CommandText = Stored_Proc_name
            SQLCmd.CommandType = CommandType.StoredProcedure
            SQLCmd.Connection = GetConnection()
            DR = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            'CloseConnection()

        Catch ex As Exception

        Finally
            ' DR.Close()

        End Try
        Return DR
    End Function

    Public Function getDataSet_SProc(ByVal Stored_Proc_name As String) As DataSet
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Returns DataSet using Stored Procedure
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim SQLCmd As New SqlCommand
        Dim ds As New DataSet
        CloseConnection()
        SQLCmd.CommandText = Stored_Proc_name
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.Connection = GetConnection()
        Dim da As New SqlDataAdapter(SQLCmd)
        da.Fill(ds)
        CloseConnection()
        Return ds
    End Function

    Public Function ExecProc_getRecordsAffected(ByVal strProcName As String, ByVal varParam(,) As Object) As Integer
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Procedure for executing Non Query stored procedure with parameters
        '* Modified date:
        '* Modified By :
        '*******************************************************************

        Try
            Dim intRecordsAffected As Integer
            Dim intI As Integer
            Dim objParam As SqlParameter = Nothing
            Dim CmdSql As New SqlCommand
            CloseConnection()
            With CmdSql
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName

                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
                'for output parameter
                objParam = .Parameters.Add("@SP_OUT", SqlDbType.Int, 1)
                objParam.Direction = ParameterDirection.Output
                intRecordsAffected = .ExecuteNonQuery()
                intRecordsAffected = .Parameters("@SP_OUT").Value
            End With
            CmdSql.Dispose()
            CloseConnection()
            Return intRecordsAffected
        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        End Try

    End Function


    Public Function ExecProc_getStatus(ByVal strProcName As String, ByVal varParam As ArrayList) As String
        Dim iRecordsAffected As Integer = 0
        Dim strOutput As String = Nothing
        Dim intI As Integer = 0
        Dim objParam As SqlParameter = Nothing
        Try

            CloseConnection()
            'SqlConn = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConnString"))
            SqlConn = New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ConnString").ToString())

            objCmd = New SqlCommand
            With objCmd
                .Connection = SqlConn
                .CommandTimeout = 3600
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName
                'if there are no parameters passed then dont add the parameters
                If Not varParam Is Nothing Then

                    For intI = 0 To varParam.Count - 1
                        Try
                            Select Case TypeName(varParam.Item(intI))
                                Case "String"
                                    objParam = .Parameters.Add("@P" & intI, SqlDbType.VarChar, Len(varParam.Item(intI)))
                                Case "Integer"
                                    objParam = .Parameters.Add("@P" & intI, SqlDbType.Int)
                                Case "Date"
                                    objParam = .Parameters.Add("@P" & intI, SqlDbType.DateTime, Len(varParam.Item(intI)))
                                Case "Double"
                                    objParam = .Parameters.Add("@P" & intI, SqlDbType.Float)
                            End Select
                            objParam.Value = varParam.Item(intI)
                            objParam.Direction = ParameterDirection.Input
                        Catch ex As Exception

                        End Try
                    Next

                End If
                'for output parameter
                objParam = .Parameters.Add("@OutputParam", SqlDbType.VarChar, 100)
                objParam.Direction = ParameterDirection.Output
                If .Connection.State = ConnectionState.Closed Then
                    .Connection.Open()
                End If
            End With
            iRecordsAffected = objCmd.ExecuteNonQuery()
            'Return iRecordsAffected
            strOutput = objCmd.Parameters("@OutputParam").Value
            objCmd.Dispose()
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

        Catch ex As Exception
            Return ex.Message.ToString
        Finally
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
            ' strOutput = Nothing
        End Try
        Return strOutput
    End Function

    Public Function ExecProc_getDataSet(ByVal strProcName As String, ByVal varParam(,) As Object) As DataSet
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Procedure for executing stored procedure with parameters and returns DataSet
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim intRecordsAffected As Integer = 0
        Dim intI As Integer
        Dim objParam As SqlParameter = Nothing
        Dim CmdSql As New SqlCommand
        Dim DsDataSet As New DataSet
        Try
          
            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName

                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
            End With
            Dim daUsers As New SqlDataAdapter(CmdSql)
            daUsers.Fill(DsDataSet)
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally
            ' DsDataSet.Clear()
        End Try
        Return DsDataSet
    End Function
    Public Function ExecProc_getDataTable(ByVal strProcName As String, ByVal varParam(,) As Object) As DataTable
        '*******************************************************************
        '* Created By SAJID KHAN
        '* Date: 9/02/2010      
        '* Purpose : Procedure for executing stored procedure with parameters and returns DATATABLE
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim DTDataTable As New DataTable
        Try
            Dim intI As Integer
            Dim objParam As SqlParameter = Nothing
            Dim CmdSql As New SqlCommand

            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName

                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
            End With
            Dim daUsers As New SqlDataAdapter(CmdSql)
            daUsers.Fill(DTDataTable)
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally
            ' DTDataTable.Clear()
        End Try
        Return DTDataTable
    End Function
    Public Function ExecProc_getDataSet_noParam(ByVal strProcName As String) As DataSet
        '*******************************************************************
        '* Created By Sajid khan
        '* Date: 09/02/2010
        '* Purpose : Procedure for executing stored procedure returns DataSet
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim DsDataSet As New DataSet
        Try
            
            Dim CmdSql As New SqlCommand

            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName

            End With
            Dim daUsers As New SqlDataAdapter(CmdSql)
            daUsers.Fill(DsDataSet)
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally
            'DsDataSet.Clear()
        End Try
        Return DsDataSet
    End Function
    Public Function ExecProc_getDataTable_noParam(ByVal strProcName As String) As DataTable
        '*******************************************************************
        '* Created By Sajid khan
        '* Date: 09/02/2010
        '* Purpose : Procedure for executing stored procedure returns DataSet
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim DtDatatable As New DataTable
        Try

            Dim CmdSql As New SqlCommand

            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName

            End With
            Dim daUsers As New SqlDataAdapter(CmdSql)
            daUsers.Fill(DtDatatable)
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally
            ' DtDatatable.Clear()
        End Try
        Return DtDatatable
    End Function


    Public Function ExecProc_getDataReder(ByVal strProcName As String, ByVal varParam(,) As Object) As SqlDataReader
        '*******************************************************************
        '* Created By ABHILASH JHARIYA
        '* Date: 18/06/2008
        '* Purpose : Procedure for executing stored procedure with parameters and returns DataReader
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim intI As Integer = 0
        Dim objParam As SqlParameter = Nothing
        Dim CmdSql As New SqlCommand
        Dim DRDataReader As SqlDataReader = Nothing
        Try
            
            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName
                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
            End With
            DRDataReader = CmdSql.ExecuteReader(CommandBehavior.CloseConnection)
            CmdSql.Dispose()
            'CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally
            'DRDataReader.Close()
        End Try
        Return DRDataReader
    End Function

    Public Function ExecProc_getScalar(ByVal strProcName As String) As Integer
        '*******************************************************************
        '* Created By Sajid Khan
        '* Date: 22/01/2010
        '* Purpose : Procedure for executing scalar procedure 
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim intReturnValue As Integer = 0
        Dim intI As Integer = 0
        'Dim objParam As SqlParameter
        Dim CmdSql As New SqlCommand
        Try
            
            CloseConnection()
            With CmdSql
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName
                'for output parameter
                ' objParam = .Parameters.Add("@SP_OUT", SqlDbType.Int, 1)
                ' objParam.Direction = ParameterDirection.Output
                intReturnValue = .ExecuteScalar()
                ' intReturnValue = .Parameters("@SP_OUT").Value
            End With
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        End Try
        Return intReturnValue
    End Function
    Public Function ExecProc_getScalar_withParam(ByVal strProcName As String, ByVal varParam(,) As Object) As Integer
        '*******************************************************************
        '* Created By Sajid Khan
        '* Date: 28/01/2010
        '* Purpose : Procedure for executing scalar procedure with parameters
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim intReturnValue As Integer = 0
        Dim intI As Integer
        Dim objParam As SqlParameter = Nothing
        Dim CmdSql As New SqlCommand

        Try
        
            
            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName
                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
            End With
            intReturnValue = CmdSql.ExecuteScalar()
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally

        End Try

        Return intReturnValue
    End Function
    Public Function ExecProc_getScalar_string(ByVal strProcName As String, ByVal varParam(,) As Object) As String
        '*******************************************************************
        '* Created By Sajid Khan
        '* Date: 15/04/2010
        '* Purpose : Procedure for executing scalar procedure with parameters return type string values
        '* Modified date:
        '* Modified By :
        '*******************************************************************
        Dim strReturnValue As String = Nothing
        Dim intI As Integer
        Dim objParam As SqlParameter = Nothing
        Dim CmdSql As New SqlCommand

        Try


            With CmdSql
                CloseConnection()
                .Connection = GetConnection()
                .CommandType = CommandType.StoredProcedure
                .CommandText = strProcName
                'if there are no parameters passed then dont add the                parameters()
                If Not varParam Is Nothing Then
                    For intI = 0 To UBound(varParam)
                        Select Case TypeName(varParam(intI, 1))
                            Case "String"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.VarChar, Len(varParam(intI, 1)))
                            Case "Integer"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Int)
                            Case "Date"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.DateTime, Len(varParam(intI, 1)))
                            Case "Double"
                                objParam = .Parameters.Add(varParam(intI, 0), SqlDbType.Float)
                        End Select
                        objParam.Value = varParam(intI, 1)
                        objParam.Direction = ParameterDirection.Input
                    Next
                End If
            End With
            strReturnValue = CmdSql.ExecuteScalar()
            CmdSql.Dispose()
            CloseConnection()

        Catch ex As Exception
            'Throw
            'MsgBox(ex.Message & ex.StackTrace)
        Finally

        End Try

        Return strReturnValue
    End Function
    Public Function GetInTimeFormat(ByVal lngValue As String) As String
        Dim intHour As Long
        Dim intMinute As Long
        Dim intSeconds As Long
        If lngValue = 0 Then
            GetInTimeFormat = "00:00:00"
            Exit Function
        End If
        intHour = Int(lngValue / 3600)
        intMinute = Int((lngValue Mod 3600) / 60)
        intSeconds = Int(lngValue Mod 60)
        GetInTimeFormat = Format(intHour, "00") & ":" & Format(intMinute, "00") & ":" & Format(intSeconds, "00")
    End Function
    Public Function ExecScal(ByVal qry As String) As String
        Dim str As String
        Try
            Dim cmd As New SqlCommand
            cmd = New SqlCommand(qry, GetConnection)

            Str = cmd.ExecuteScalar().ToString()
            CloseConnection()

        Catch ex As Exception
            Return String.Empty
        Finally
            '  str = Nothing
        End Try
        Return str
    End Function

    Public Shared Function GetNumberValidatorScript() As String
        Dim _str As String
        _str = "<script> "
        _str += " function FilterNumeric(e){"
        _str += " var re; "
        _str += "var keychar=window.event? e.keyCode:e.charCode;"
        _str += "if((keychar<=57)&&(keychar>=48))"
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
        Dim _str As String
        _str = "<script> "
        _str += " function FilterText(e){"
        _str += " var re; "
        _str += "var keychar=window.event? e.keyCode:e.charCode;"
        _str += "if(((keychar<=122)&&(keychar>=97))||((keychar<=90)&&(keychar>=65)))"
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
    Protected Sub ShowAlertBox(ByVal Alert As String)

        'Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "alert", "<script>alert('" & Alert & "')</script>")
        
    End Sub
    Public Function get_parameter_of_sp(ByVal spname As String)
        Try
            Dim blInputParameters As New ArrayList
            Dim blOutputParameters As New ArrayList
            Dim myConnection As SqlConnection = GetConnection()
            '    blInputParameters.Items.Clear()
            'blOutputParameters.Items.Clear()
            Dim str As String = GetConnection().ToString
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConnection
            myCommand.CommandText = spname
            myCommand.CommandType = Data.CommandType.StoredProcedure

            SqlCommandBuilder.DeriveParameters(myCommand)

            For Each param As SqlParameter In myCommand.Parameters
                If param.Direction = Data.ParameterDirection.Input Then 'OrElse param.Direction = Data.ParameterDirection.InputOutput Then
                    ' blInputParameters.Items.Add(param.ParameterName & " - " & param.SqlDbType.ToString())
                    blInputParameters.Add(param.ParameterName & " - " & param.SqlDbType.ToString())

                Else
                    blOutputParameters.Add(param.ParameterName & " - " & param.SqlDbType.ToString())
                End If
            Next
            'Dim str As String = blInputParameters.Items.Count & "-" & blOutputParameters.Items.Count
            Return blInputParameters.Count '- blOutputParameters.Count
        Catch ex As Exception

        End Try
    End Function
End Class

