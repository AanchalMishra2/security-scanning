﻿<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmVendorReport.aspx.vb" Inherits="Forms_frmVendorReport" title="Energy Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 76px;
        }
        .style3
        {
            width: 203px;
        }
        .style4
        {
            width: 137px;
        }
        .style5
        {
            width: 117px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip">
        <h1>
           Fund Utilized With Vendor Report
           </h1>
    </div>
    <div class="clear">
    </div>

    <asp:Panel ID="pnl_controls" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCustomer" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCustomer" TabIndex="1" ControlToValidate="ddlCustomer"
                        InitialValue="0" ErrorMessage="Select Customer" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCustomer" TargetControlID="reqCustomer" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblCircle" Text="Circle :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCircle" ControlToValidate="ddlCircle" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" TargetControlID="reqCircle" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    
                </td>
                <td>
                    <asp:Label ID="lblSiteId" Text="Vendor : " runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlvendor" runat="server" AutoPostBack="false"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvendor" ControlToValidate="ddlvendor" InitialValue="0"
                        ErrorMessage="Select Vendor" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcevendor" TargetControlID="reqvendor" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    
                </td>
            </tr>
          
            <tr>
                <td>
                    <asp:Label ID="lbl_frmdt" Text="From Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_frmdt" TabIndex="3" Width="160px" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="ce_frm" Format="dd/MM/yyyy" TargetControlID="txt_frmdt"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="req_frmdt"  ValidationGroup="rpt"
                        runat="server" ControlToValidate="txt_frmdt" ErrorMessage="Select From Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_frmdt" TargetControlID="req_frmdt" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revfrmdt"  ValidationGroup="rpt"
                        ControlToValidate="txt_frmdt" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcefrmdt" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revfrmdt">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbl_todt" Text="To Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_todt" TabIndex="4" runat="server" Width="160px" ></asp:TextBox>
                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ce_todt" TargetControlID="txt_todt"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="req_todt" ValidationGroup="rpt"
                        runat="server" ControlToValidate="txt_todt" ErrorMessage="Select To Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_todt" TargetControlID="req_todt" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtodt"  ValidationGroup="rpt"
                        ControlToValidate="txt_todt" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetodt" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revtodt">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="cmdate" ControlToValidate="txt_todt" ControlToCompare="txt_frmdt"
                        ErrorMessage="Please Select To Date Greater than From Date" ValueToCompare="Date"
                        Operator="GreaterThanEqual" runat="server" SetFocusOnError="true" ValidationGroup="rpt">*</asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecmdate"  runat="server"
                        TargetControlID="cmdate">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
              <tr>
            <td colspan="4" align="center">
            <asp:Button CssClass="input_btn input_text_x" OnClientClick=" Javascript:openWindow('D')" ValidationGroup="rpt"  ID="btndetailed" runat="server" Text="Detailed"  />
             <asp:Button CssClass="input_btn input_text_x" OnClientClick=" Javascript:openWindow('S')"   ID="btnsummary" ValidationGroup="rpt" runat="server" Text="Summary"  />
             <asp:Button CssClass="input_btn input_text_x" Visible="true"   ID="btnreset" runat="server" Text="Reset"  />
                &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>


    <asp:Panel ID="pnlwarning" runat="server">
    <asp:Label ID="lblwarning" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlRptviewer" runat="server">
    </asp:Panel>
    
    <script type="text/javascript">
    function openWindow(param)
        {
       // debugger;
        
        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
            }

        if (Page_IsValid) {
                if (param=="D"){window.open("frmVendorRPT.aspx?param=" + document.getElementById('<%=ddlCustomer.ClientID%>').value + "&c=" +document.getElementById('<%=ddlCircle.ClientID%>').value + "&v=" +document.getElementById('<%=ddlvendor.ClientID%>').value + "&f=" + document.getElementById('<%=txt_frmdt.ClientID%>').value+"&t=" + document.getElementById('<%=txt_todt.ClientID%>').value +"&Mode=D");}
                if(param=="S"){window.open("frmVendorRPT.aspx?param=" + document.getElementById('<%=ddlCustomer.ClientID%>').value + "&c=" +document.getElementById('<%=ddlCircle.ClientID%>').value + "&v=" +document.getElementById('<%=ddlvendor.ClientID%>').value + "&f=" + document.getElementById('<%=txt_frmdt.ClientID%>').value+"&t=" + document.getElementById('<%=txt_todt.ClientID%>').value +"&Mode=S");}
                        
                }
        else {}
         }

      //  alert( document.getElementById('<%=ddlCustomer.ClientID%>').value);
    
    </script>
</asp:Content>
