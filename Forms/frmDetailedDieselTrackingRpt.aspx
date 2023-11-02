﻿<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmDetailedDieselTrackingRpt.aspx.vb" Inherits="Forms_frmDetailedDieselTrackingRpt"
    Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">  
     .gvFixedHeader  
     {  
      font-weight:bold;  
      position:relative;  
       background-color:White;  
     }  
  </style> 
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            Detailed Report For Diesel Tracking</h1>
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
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" runat="server" AutoPostBack="false"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCircle" ControlToValidate="ddlCircle" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" TargetControlID="reqCircle" runat="server">
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
            <asp:Button CssClass="input_btn input_text_x" OnClientClick=" Javascript:openWindow()" ValidationGroup="rpt"  ID="btnview" runat="server" Text="View"  />
             <asp:Button CssClass="input_btn input_text_x"   ID="btnreset" runat="server" Text="Reset"  />
             <asp:Button CssClass="input_btn input_text_x" Visible="false"   ID="btnexport" runat="server" Text="Export"  />
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
    function openWindow()
        {
       // debugger;
        
        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
            }

        if (Page_IsValid) {
            // do something
             window.open("frmDieselTrackingRPT.aspx?param=" + document.getElementById('<%=ddlCustomer.ClientID%>').value + "&c=" +document.getElementById('<%=ddlCircle.ClientID%>').value +"&f=" + document.getElementById('<%=txt_frmdt.ClientID%>').value+"&t=" + document.getElementById('<%=txt_todt.ClientID%>').value +"");
            //alert('Page is valid!');                
                }
        else {
            // do something else
            //alert('Page is not valid!');
        }
         }

      //  alert( document.getElementById('<%=ddlCustomer.ClientID%>').value);
    
    </script>
</asp:Content>
