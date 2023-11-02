<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmFundUtilization.aspx.vb" Inherits="Forms_frmFundUtilization" title="Energy Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Fund Utilized</h1><div style="text-align:left; ">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton  ID="lnktemplate" Text="Download Template" runat="server"></asp:LinkButton></div></div>
   <div class="clear"></div>
<div style="text-align:right;">
<asp:FileUpload ID="fileInput" runat="server" />
<asp:RequiredFieldValidator ID="requpload" ControlToValidate="fileInput" ErrorMessage="Select file to upload" ValidationGroup="upload" runat="server" >*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender PopupPosition="BottomLeft" ID="vceupload" TargetControlID="requpload" runat="server"></cc1:ValidatorCalloutExtender>
<asp:Button ID="import" ValidationGroup="upload" CssClass="input_btn input_text_x" Text="Import" runat="server" />
<hr />
</div>
<asp:Panel ID="pnlfundallotment" runat="server">
<table>
<%--<tr>
<td colspan="4" align="right"> 
 <asp:FileUpload ID="fileinput" runat="server"  />
  <asp:RequiredFieldValidator ID="requpload" runat="server" 
  ControlToValidate="fileinput" Display="Dynamic" ValidationGroup="upload" ErrorMessage="Select a file to upload"  
  SetFocusOnError="True">*</asp:RequiredFieldValidator>
  <cc1:ValidatorCalloutExtender ID="vceupload" PopupPosition="BottomLeft" TargetControlID="requpload"  runat="server"></cc1:ValidatorCalloutExtender>
  <asp:Button ID="BtnImport" CssClass="input_btn input_text_x" ValidationGroup="upload"  runat="server" Text="Import"  />
               
</td>
</tr>--%>
<tr>
<td colspan="4" align="center"><asp:Label ID="lblwarning" Font-Bold="True" ForeColor="#C00000" runat="server" Visible="false"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblcustomer" runat="server" Text="Customer :"></asp:Label></td>
<td><asp:DropDownList ID="ddlcustomer" CssClass="select_option" runat="server" 
        Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqCustomer" runat="server" ControlToValidate="ddlCustomer" ErrorMessage="Select Customer" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcecustomer" runat="server" TargetControlID="reqCustomer"></cc1:ValidatorCalloutExtender>
</td>
<td><asp:Label ID="lblcircle" runat="server" Text="Circle :"></asp:Label></td>
<td><asp:DropDownList  ID="ddlcircle" CssClass="select_option" runat="server" 
        Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqcircle" runat="server" ControlToValidate="ddlcircle" ErrorMessage="Select Circle" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="reqcircle"></cc1:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td><asp:Label ID="lblmodeofpay" runat="server" Text="Mode Of Payment :"></asp:Label></td>
<td><asp:DropDownList ID="ddlmodeofpay" CssClass="select_option" runat="server" 
         Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqmodeofpay" runat="server" ControlToValidate="ddlmodeofpay" ErrorMessage="Select Mode Of Pay" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcemodeofpay" runat="server" TargetControlID="reqmodeofpay"></cc1:ValidatorCalloutExtender>
</td>
<td><asp:Label ID="lbldate" runat="server" Text="Transaction Date :"></asp:Label></td>
<td><asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
<cc1:CalendarExtender ID="ce_frm" Format="dd/MM/yyyy" TargetControlID="txtdate" runat="server"></cc1:CalendarExtender>
<asp:RequiredFieldValidator ID="reqdate"  ValidationGroup="fund"  runat="server" ControlToValidate="txtdate" ErrorMessage="Select Date" >*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcedate" TargetControlID="reqdate" runat="server"></cc1:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="revdate"  ValidationGroup="fund"  ControlToValidate="txtdate" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$" runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
<cc1:ValidatorCalloutExtender ID="vcedate1"  PopupPosition ="BottomLeft"  runat="server" TargetControlID="revdate"></cc1:ValidatorCalloutExtender>
</td>

</tr>
<tr>
<td><asp:Label ID="lblvendor" runat="server" Text="Vendor :"></asp:Label></td>
<td><asp:DropDownList ID="ddlvendor" CssClass="select_option" runat="server" 
         Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqvendor" runat="server" ControlToValidate="ddlvendor" ErrorMessage="Select Vendor" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcevendor" runat="server" TargetControlID="reqvendor"></cc1:ValidatorCalloutExtender>
</td>
<td><asp:Label ID="lblpetrocardno" runat="server" Text="PetroCard Number :"></asp:Label></td>
<td><asp:DropDownList ID="ddlpetrocardno" CssClass="select_option" runat="server" 
         Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqpetrocardno" runat="server" ControlToValidate="ddlpetrocardno" ErrorMessage="Select Petrocard No" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcepetrocardno" runat="server" TargetControlID="reqpetrocardno"></cc1:ValidatorCalloutExtender>
</td>
</tr>
<%--<tr>
<td><asp:Label ID="lbldealer" runat="server" Text="Dealer :"></asp:Label></td>
<td><asp:DropDownList ID="ddldealer" CssClass="select_option" runat="server" 
         Width="152px"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqdealer" runat="server" ControlToValidate="ddldealer" ErrorMessage="Select Dealer" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcedealer" runat="server" TargetControlID="reqdealer"></cc1:ValidatorCalloutExtender>
</td>
<td><asp:Label ID="Label2" runat="server" Text="Petro Card No :"></asp:Label></td>
<td><asp:TextBox ID="txtfundallotment" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqfundallotment" runat="server" ControlToValidate="txtfundallotment" ErrorMessage="Enter Amount" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcefundallotment" runat="server" TargetControlID="reqfundallotment"></cc1:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="rxvfundallotment" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txtfundallotment" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
<cc1:ValidatorCalloutExtender ID="vcefundallotment1" runat="server" TargetControlID="rxvfundallotment"></cc1:ValidatorCalloutExtender>
</td>
</tr>--%>
<tr>
<td><asp:Label ID="lblsubpetrocards" runat="server" Text="Sub Petrocard Number :"></asp:Label></td>
<td><asp:DropDownList ID="ddlsubpetrocard" CssClass="select_option" runat="server" 
        Width="152px" AutoPostBack="True"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqsubpetrocard" runat="server" ControlToValidate="ddlsubpetrocard" ErrorMessage="Select Sub Petrocard" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcesubpetrocard" runat="server" TargetControlID="reqsubpetrocard"></cc1:ValidatorCalloutExtender>
</td>
<td><asp:Label ID="lblitemparchased" runat="server" Text="Item Purchased :"></asp:Label></td>
<td><asp:DropDownList  ID="ddlitemparchased" CssClass="select_option" runat="server" 
        Width="152px"> </asp:DropDownList>
<asp:RequiredFieldValidator ID="reqitemparchased" runat="server" ControlToValidate="ddlitemparchased" ErrorMessage="Select Item" ValidationGroup="fund" InitialValue="0">*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vceitemparchased" runat="server" TargetControlID="reqitemparchased"></cc1:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td><asp:Label ID="lbltotalamount" runat="server" Text="Total Amount :"></asp:Label></td>
<td><asp:TextBox ID="txttotalamount" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqtotalamount" ValidationGroup ="fund" ControlToValidate="txttotalamount" runat="server" ErrorMessage="Enter Amount" >*</asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="vcetotalamount" runat="server" TargetControlID="reqtotalamount"></cc1:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="rxvtotalamount" ValidationGroup ="fund" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txttotalamount" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
<cc1:ValidatorCalloutExtender ID="vcetotalamount1" runat="server" TargetControlID="rxvtotalamount"></cc1:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td colspan="4" align="center"><asp:Button ID="btnsave" Text="Save" ValidationGroup="fund"  CssClass="input_btn input_text_x"  runat="server" /><asp:Button ID="btnreset" Text="Reset"  CssClass="input_btn input_text_x"  runat="server" /> </td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlimport" Visible="false" runat="server">
<cc1:Accordion ID="acc_1" SelectedIndex="-1"  Width="500px"  HeaderCssClass="accordionHeader" 
ContentCssClass="accordionContent" runat="server" 
AutoSize="None"
FadeTransitions="true"
TransitionDuration="250"
FramesPerSecond="40"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true">
<Panes>
<cc1:AccordionPane ID="pn1" runat="server">
<Header><b>Import Status</b></Header>
<Content>
<asp:GridView ID="dgRecords"  runat="server"></asp:GridView>
</Content>
</cc1:AccordionPane>

</Panes>
</cc1:Accordion>
</asp:Panel>
</asp:Content>

