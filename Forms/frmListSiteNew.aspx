<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmListSiteNew.aspx.vb" Inherits="Forms_frmListSiteNew" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            List Site</h1>
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
                    <asp:DropDownList CssClass="select_option" AutoPostBack="true" ID="ddlcircle" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcircle" ValidationGroup="search" runat="server"
                        ControlToValidate="ddlcircle" InitialValue="0" ErrorMessage="Select Customer">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" runat="server" TargetControlID="reqcustomer">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Button CssClass="input_btn input_text_x" ValidationGroup="search" ID="btnview"
                        runat="server" Text="View" Width="87px" />
                    <asp:Button CssClass="input_btn input_text_x" Visible="false" ID="btnexport" runat="server" Text="Export"
                        Width="87px" />
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
    <asp:Panel ID="pnldata" Visible="false" runat="server">
        <div>
            <table border="1" width="870px">
                <tr>
                    <td style="width: 140px;">
                        <b>Sr No</b>
                    </td>
                    <td style="width: 200px;">
                        <b>Site Id</b>
                    </td>
                    <td style="width: 190px;">
                        <b>Site Name</b>
                    </td>
                    <td style="width: 140px;">
                        <b>Site Type</b>
                    </td>
                    <td style="width: 140px;">
                        <b>No Of Operator</b>
                    </td>
                    <td style="width: 140px;">
                        <b>EB Connection</b>
                    </td>
                    <td style="width: 140px;">
                        <b>DG Set</b>
                    </td>
                    <td style="width: 140px;">
                        <b>DG Capacity</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <div style="overflow: scroll; height: 200px; width: 870px">
                            <asp:GridView ID="gdvrpt" AutoGenerateColumns="false" runat="server" Width="850px">
                                <Columns>
                                    <asp:BoundField DataField="RNO">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_SITE_ID">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_SITE_NAME">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_SITE_TYPE">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_NO_OF_OPERATOR">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_EB_CONNECTION">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_DG_SET">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_DG_CAPACITY">
                                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlssrs" runat="server" Visible="false">
    <hr />
    </asp:Panel>
</asp:Content>
