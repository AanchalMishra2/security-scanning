<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmListSubPetroCard.aspx.vb" Inherits="Forms_frmListSubPetroCard" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            List Sub Petro Card</h1>
    </div>
    <div class="clear">
    </div>

    <asp:Panel ID="pnlcontrols" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlcustomer" CssClass="select_option" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcustomer" ValidationGroup="search" runat="server"
                        ControlToValidate="ddlcustomer" InitialValue="0" ErrorMessage="Select Customer">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecustomer" runat="server" TargetControlID="reqcustomer">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblcircle" Text="Circle :" runat="server"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList CssClass="select_option" ID="ddlcircle" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcircle" ValidationGroup="search" runat="server"
                        ControlToValidate="ddlcircle" InitialValue="0" ErrorMessage="Select Customer">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" runat="server" TargetControlID="reqcustomer">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="Label1" Text="Vendor :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlvendor" CssClass="select_option" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvendor" ValidationGroup="search" runat="server"
                        ControlToValidate="ddlvendor" InitialValue="0" ErrorMessage="Select Customer">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="reqvendor">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button CssClass="input_btn input_text_x" ValidationGroup="search" ID="btnview"
                        runat="server" Text="View" />
                    <asp:Button CssClass="input_btn input_text_x" Visible="false"  ID="btnexport" runat="server" Text="Export" />
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Label ID="lblmsg" Visible="false" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <br />
    <asp:Panel ID="pnldata1" runat="server">
        <asp:Label ID="lbldata" runat="server"></asp:Label>
    </asp:Panel>
       <asp:Panel ID="pnlssrs" runat="server" Visible="false">
    <hr />
    </asp:Panel>
</asp:Content>
