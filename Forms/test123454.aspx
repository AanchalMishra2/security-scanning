<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test123454.aspx.vb" Inherits="Forms_test123454" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="400px" Width="400px">
            <LocalReport ReportPath="Reports\ConsolidatedRPT.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="DataSet1_D_SP_FUNDALLOTMENT_FUNDUTILIZATION_REPORT" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetData" 
            TypeName="DataSet1TableAdapters.D_SP_FUNDALLOTMENT_FUNDUTILIZATION_REPORTTableAdapter">
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
