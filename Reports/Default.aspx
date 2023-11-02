<%@ Page Language="VB" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="400px" Width="822px" >
            <LocalReport ReportPath="Reports\Report.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="DataSet1_D_SP_DETAILED_DIESEL_TRACKING_REPORT" />
                </DataSources>
            </LocalReport>
         
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            TypeName="DataSet1TableAdapters.D_SP_DETAILED_DIESEL_TRACKING_REPORTTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="intCustomerId" Type="Int64" />
                <asp:Parameter Name="intCircleId" Type="Int64" />
                <asp:Parameter Name="strFDate" Type="String" />
                <asp:Parameter Name="strTDate" Type="String" />
                <asp:Parameter Name="intUserId" Type="Int64" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
