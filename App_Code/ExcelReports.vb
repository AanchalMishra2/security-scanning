Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Data
Imports System.Web.Mail
Imports System.Data.OleDb

Public Class ExcelReports


    Public Sub GenerateExcelReport(ByVal FileNameToUse As String, ByRef DataTableToExport As DataTable)
        'This view should not contain the Primary Key Id column, but it should be sorted over that Id Column
        ' This kind of a view can be created in the SQL Server Enterprise Manager

        If DataTableToExport Is Nothing Or DataTableToExport.Rows.Count = 0 Then
            Throw New ArgumentException("There is no Data to export !")
        End If

        'Set the Response Attributes to show a Download Dialog Box
        System.Web.HttpContext.Current.Response.Clear()
        System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & FileNameToUse & ".xls")
        System.Web.HttpContext.Current.Response.Charset = ""
        System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        System.Web.HttpContext.Current.Response.ContentType = "application/vnd.xls"

        Dim stringWriter As New System.IO.StringWriter
        Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

        'Create a new DataTable for customization
        Dim dt As New DataTable


        'Add a custom "Serial No" column at the first position
        Dim dc As New DataColumn("Serial No", System.Type.GetType("System.Int32"))
        dt.Columns.Add(dc)

        dc = Nothing
        'Add all the remaining Columns to the new DataTable
        Dim index As Integer

        For index = 0 To DataTableToExport.Columns.Count - 1 Step 1
            dt.Columns.Add(DataTableToExport.Columns(index).ColumnName, DataTableToExport.Columns(index).DataType)
        Next

        Dim dr As DataRow
        'Add the Records to the new DataTable 
        For index = 0 To DataTableToExport.Rows.Count - 1 Step 1  'RowIndex
            'Create a new row 
            dr = dt.NewRow

            'Set the Serial No value
            'dr("Serial No") = index + 1
            dr(0) = index + 1 'The First ciolumn in the DataTable must be "Serial No"

            'Set the values of remaining columns
            Dim ColumnIndex As Integer
            For ColumnIndex = 0 To DataTableToExport.Columns.Count - 1 Step 1
                dr(ColumnIndex + 1) = DataTableToExport.Rows(index)(ColumnIndex)
            Next
            dt.Rows.Add(dr)
        Next

        'Create a new DataGrid object and bind it to the Customized DataTable for exporting
        Dim dgExport As New DataGrid
        dgExport.DataSource = dt
        dgExport.DataBind()

        'Now make the DataGrid reder its html to the HtmlTextWriter, and then write that string to the response.
        dgExport.RenderControl(htmlWriter)

        htmlWriter.Flush()
        htmlWriter.Close()

        System.Web.HttpContext.Current.Response.Write(stringWriter.ToString())
        'System.Web.HttpContext.Current.Response.End()
    End Sub

    Public Shared Function ImportExcelSheel(ByVal strFilePath As String) As DataSet



        ' Create connection string variable. Modify the "Data Source"
        ' parameter as appropriate for your environment.
        Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
         "Data Source=" + strFilePath + ";" & _
         "Extended Properties=Excel 8.0;"

        ' Create connection object by using the preceding connection string.
        Dim objConn As OleDbConnection = New OleDbConnection(sConnectionString)
        Dim objCmdSelect As OleDbCommand
        Try
            ' Open connection with the database.
            objConn.Open()
            Dim dtExcelSchema As DataTable

            dtExcelSchema = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            'column index 2 is for sheetname
            Dim SheetName As String = dtExcelSchema.Rows(0)(2).ToString()

            ' The code to follow uses a SQL SELECT command to display the data from the worksheet.

            ' Create new OleDbCommand to return data from worksheet.
            objCmdSelect = New OleDbCommand("SELECT * FROM [" & SheetName & "]", objConn)

            ' Create new OleDbDataAdapter that is used to build a DataSet
            ' based on the preceding SQL SELECT statement.
            Dim objAdapter1 As OleDbDataAdapter = New OleDbDataAdapter

            ' Pass the Select command to the adapter.
            objAdapter1.SelectCommand = objCmdSelect

            ' Create new DataSet to hold information from the worksheet.
            Dim objDataset1 As DataSet = New DataSet

            ' Fill the DataSet with the information from the worksheet.
            objAdapter1.Fill(objDataset1, "XLData")

            '' Bind data to DataGrid control.
            'DataGrid1.DataSource = objDataset1.Tables(0).DefaultView
            'DataGrid1.DataBind()
            If objDataset1.Tables(0).Rows.Count > 0 Then
                objAdapter1.Dispose()
                objCmdSelect.Dispose()
                Return objDataset1
            Else
                objAdapter1.Dispose()
                objCmdSelect.Dispose()
                Return Nothing
            End If
            ' Clean up objects.



        Catch ex As Exception
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
                objConn.Dispose() 
            End If
             
            Return Nothing
        Finally
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
                objConn.Dispose()

            End If

        End Try
    End Function

    Public Shared Sub GenerateExcel(ByVal dt As DataTable)
        Dim strTitle As String

        strTitle = "ReportingManager"
        If dt.Rows.Count > 0 Then
            'Set the Response Attributes to show a Download Dialog Box
            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=TRANSFER_PENDING_FOR_" & Now.Date.ToString.Remove(" ", "_") & "" & ".xls")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.xls"

            ''System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=TRANSFER_PENDING_FOR_" & Now.Date.ToString.Remove(" ", "_") & "" & ".xls")
            ''System.Web.HttpContext.Current.Response.ContentType = "application/xls"
            'Response.BinaryWrite(fliename);

            Dim stringWriter As New System.IO.StringWriter()
            Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

            ' ''Create a new DataTable for customization
            ''Dim dt1 As New DataTable()


            ' ''Add a custom "Serial No" column at the first position
            ''Dim dc As New DataColumn("Serial No", System.Type.[GetType]("System.Int32"))
            ''dt1.Columns.Add(dc)

            ''dc = Nothing
            ' ''Add all the remaining Columns to the new DataTable
            ''Dim index As Integer = 0

            ''For index = 0 To dt.Columns.Count - 1
            ''    dt1.Columns.Add(dt.Columns(index).ColumnName, dt.Columns(index).DataType)
            ''Next

            ''Dim dr As DataRow = Nothing
            ' ''Add the Records to the new DataTable 
            ''For index = 0 To dt.Rows.Count - 1
            ''    'RowIndex
            ''    'Create a new row 
            ''    dr = dt1.NewRow()

            ''    'Set the Serial No value
            ''    'dr("Serial No") = index + 1
            ''    dr(0) = index + 1
            ''    'The First ciolumn in the DataTable must be "Serial No"

            ''    'Set the values of remaining columns
            ''    Dim ColumnIndex As Integer = 0
            ''    For ColumnIndex = 0 To dt.Columns.Count - 1
            ''        dr(ColumnIndex + 1) = dt.Rows(index)(ColumnIndex)
            ''    Next
            ''    dt1.Rows.Add(dr)
            ''Next

            'Create a new DataGrid object and bind it to the Customized DataTable for exporting
            Dim dgExport As New DataGrid()
            dgExport.DataSource = dt
            dgExport.DataBind()

            'Now make the DataGrid reder its html to the HtmlTextWriter, and then write that string to the response.
            dgExport.RenderControl(htmlWriter)

            htmlWriter.Flush()
            htmlWriter.Close()

            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString())
            System.Web.HttpContext.Current.Response.End()
        End If

    End Sub

    'Private Sub PrintExcelReport()
    '    'Print the excel Report from the DataGrid in the same format
    '    Response.Clear()
    '    Response.AddHeader("content-disposition", "attachment;filename=FileName.xls")
    '    Response.Charset = ""
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.ContentType = "application/vnd.xls"

    '    Dim stringWriter As New System.IO.StringWriter
    '    Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

    '    Dim dgExport As New DataGrid

    '    If Session("UserGridDataSet") Is Nothing Then
    '        Response.Write("Nothing")
    '        Return
    '    End If

    '    dgExport.DataSource = CType(Session("UserGridDataSet"), System.Data.DataSet)
    '    dgExport.DataBind()

    '    dgExport.RenderControl(htmlWriter)

    '    htmlWriter.Flush()
    '    htmlWriter.Close()

    '    Response.Write(stringWriter.ToString())
    '    Response.End()
    'End Sub
    'Private Sub DownloadExcelReport(ByVal FileNameToUse As String, ByRef DataTableToExport As DataTable)

    '    'This view should not contain the Primary Key Id column, but it should be sorted over that Id Column
    '    ' This kind of a view can be created in the SQL Server Enterprise Manager

    '    If DataTableToExport Is Nothing Or DataTableToExport.Rows.Count = 0 Then
    '        Throw New ArgumentException("There is no Data to export !")
    '    End If

    '    'Set the Response Attributes to show a Download Dialog Box
    '    Response.Clear()
    '    Response.AddHeader("content-disposition", "attachment;filename=" & FileNameToUse & ".xls")
    '    Response.Charset = ""
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.ContentType = "application/vnd.xls"

    '    Dim stringWriter As New System.IO.StringWriter
    '    Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

    '    'Create a new DataTable for customization
    '    Dim dt As New DataTable

    '    'Add a custom "Serial No" column at the first position
    '    Dim dc As New DataColumn("Serial No", System.Type.GetType("System.Int32"))
    '    dt.Columns.Add(dc)

    '    dc = Nothing
    '    'Add all the remaining Columns to the new DataTable
    '    Dim index As Integer

    '    For index = 0 To DataTableToExport.Columns.Count - 1 Step 1
    '        dt.Columns.Add(DataTableToExport.Columns(index).ColumnName, DataTableToExport.Columns(index).DataType)
    '    Next

    '    Dim dr As DataRow
    '    'Add the Records to the new DataTable 
    '    For index = 0 To DataTableToExport.Rows.Count - 1 Step 1  'RowIndex
    '        'Create a new row 
    '        dr = dt.NewRow

    '        'Set the Serial No value
    '        'dr("Serial No") = index + 1
    '        dr(0) = index + 1 'The First ciolumn in the DataTable must be "Serial No"

    '        'Set the values of remaining columns
    '        Dim ColumnIndex As Integer
    '        For ColumnIndex = 0 To DataTableToExport.Columns.Count - 1 Step 1
    '            dr(ColumnIndex + 1) = DataTableToExport.Rows(index)(ColumnIndex)
    '        Next
    '        dt.Rows.Add(dr)
    '    Next

    '    'Create a new DataGrid object and bind it to the Customized DataTable for exporting
    '    Dim dgExport As New DataGrid
    '    dgExport.DataSource = dt
    '    dgExport.DataBind()

    '    'Now make the DataGrid reder its html to the HtmlTextWriter, and then write that string to the response.
    '    dgExport.RenderControl(htmlWriter)

    '    htmlWriter.Flush()
    '    htmlWriter.Close()

    '    Response.Write(stringWriter.ToString())
    '    Response.End()
    'End Sub
    'Public Shared Function SendEmail(ByVal sFrom As String, ByVal sTo As String, ByVal sCC As String, ByVal sBCC As String, ByVal sSubject As String, ByVal sBody As String, ByVal AttachedFiles As String) As Boolean

    '    Dim msgMail As New MailMessage
    '    Dim intI As Integer
    '    Dim intAttachNo As Integer
    '    Try

    '        msgMail.To = sTo
    '        msgMail.From = sFrom ''sFrom
    '        msgMail.Cc = sCC ''sCC
    '        msgMail.Bcc = sBCC ''sBCC
    '        msgMail.Subject = sSubject
    '        msgMail.Body = sBody

    '        'msgMail.BodyFormat = MailFormat.Text
    '        'If IsHTML Then
    '        '    msgMail.BodyFormat = MailFormat.Html
    '        'Else
    '        '    msgMail.BodyFormat = MailFormat.Text
    '        'End If


    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.globalproserv.com")
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25")
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", "10")
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
    '        ''''msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "abhilashj@globalproserv.com") 'set your username here
    '        ''''msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "paSs@123") 'set your password here
    '        ''''''msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "bhartiiwan@globalproserv.com") 'set your username here
    '        ''''''msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "team_kpo") 'set your password here
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "iwankpo@gtllimited.com") 'set your username here
    '        msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "H51HRQ11") 'set your password here

    '        SmtpMail.SmtpServer.Insert(0, System.Configuration.ConfigurationSettings.AppSettings("mailServer").ToString())
    '        SmtpMail.Send(msgMail)

    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
End Class
