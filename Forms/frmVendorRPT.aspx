<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVendorRPT.aspx.vb" Inherits="Forms_frmVendorRPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Report</title>
    <script src="../JS/validation.js" type="text/javascript"></script>
    <link href="../css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlheader" Width="951px" runat="server">
    <div id="header" >
        <ul>
        <li class="logo"><img src="../Images/issue_tracker_logo.jpg" alt="Energy Tracker" width="264" height="50" title="Issue Tracker" /></li>
        <li class="fr" id="top_nav">
        <ul>
        <li style="height:50px"></li>
       <%-- <li class="top_nav_mid">
         <span class="welcome">Welcome:</span> 
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="userid">Adminstrator</asp:Label>
              <span class="logout">&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server">Logout</asp:LinkButton></span>
           </li>
           --%>
        <li ></li>
        <li class="clear"></li>
        <li class="globalProsev_logo fr"><img src="../Images/globalgroup.gif" alt="Global Group Enterprise" title="Global Group Enterprise" /></li>
        </ul>
        </li>
        
        <li class="clear"></li>
        </ul>
  </div>
    </asp:Panel>
    <asp:Panel BackColor="#FAF8CC" Width="951px" ID="pnlwarning" Visible="false" runat="server" HorizontalAlign="Center">
    <asp:Label ID="lblwarning" runat="server" ForeColor="Red" Text="Sorry No Data Found."></asp:Label>
    </asp:Panel>
   <asp:Panel  Visible="false" ID="pnlrpt" runat="server">
      
   </asp:Panel>
   
    </div>
    </form>
</body>
</html>
