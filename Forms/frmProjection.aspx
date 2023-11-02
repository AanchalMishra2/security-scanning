<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmProjection.aspx.vb" Inherits="Forms_frmProjection" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            Monthly Projection</h1>
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
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" runat="server" AutoPostBack="true"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCircle" ControlToValidate="ddlCircle" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" TargetControlID="reqCircle" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="Label3" Text="Cluster :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlcluster" runat="server" AutoPostBack="true"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcluster" ControlToValidate="ddlcluster" InitialValue="-1"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecluster" TargetControlID="reqcluster" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Month :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlmonth" runat="server" AutoPostBack="true"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqmonth" TabIndex="1" ControlToValidate="ddlmonth"
                        InitialValue="0" ErrorMessage="Select Customer" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcemonth" TargetControlID="reqmonth" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="Label2" Text="Year :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlyear" runat="server" AutoPostBack="true"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqyear" ControlToValidate="ddlyear" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceyear" TargetControlID="reqyear" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button CssClass="input_btn input_text_x" ValidationGroup="rpt" ID="btnview"
                        runat="server" Text="View" />
                    <asp:Button CssClass="input_btn input_text_x" ID="btnexport" runat="server" Visible="false"
                        Text="Reset" />
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel  ID="pnlwarning" Visible="false" runat="server"><div style="text-align:center;">
        <asp:Label  ID="lblwarning" runat="server" ForeColor="Red"></asp:Label></div></asp:Panel>
    <asp:Panel ID="pnldata" Visible="false" runat="server">
        <asp:GridView ID="gdvprojection"  GridLines="None" AllowPaging="true"   CssClass="tablestyle" 
            AutoGenerateColumns="false" PageSize="6" runat="server" Width="750px">
        <RowStyle CssClass="rowstyle" />
                            <FooterStyle CssClass="PagerStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="headerstyle" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle CssClass="altrowstyle" />
            <Columns>
                <asp:BoundField  HeaderText="Sr No" DataField="Sr No" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Customer Name" DataField="CUSTOMER NAME" />
                <asp:BoundField HeaderText="Circle Name" DataField="CIRCLE NAME" />
                <asp:BoundField HeaderText="Cluster" DataField="CLUSTER" />
                <asp:BoundField HeaderText="Site Name" DataField="SITE NAME" />
                <asp:BoundField HeaderText="Site Id" DataField="SITE ID" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Diesel Qty In Liter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtDieselunit" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        EB Units
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtebunit" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                <asp:HiddenField ID="d_site_no" Value='<%# Eval("D_SITE_NO") %>' runat="server" />
                <asp:HiddenField ID="D_CIRCLE_ID" Value='<%# Eval("D_CIRCLE_ID") %>' runat="server" />
                <asp:HiddenField ID="D_CUSTOMER_ID" Value='<%# Eval("D_CUSTOMER_ID") %>' runat="server" />
                </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate>
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_RecordNotFound" Text="No Record Found" runat="server" Font-Size="Larger"
                                                ForeColor="maroon"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
        </asp:GridView>
        <div style="text-align:center; width: 868px;">
         <asp:Button CssClass="input_btn input_text_x"  ID="Button1"
                        runat="server" Text="Submit" />
        </div>
    </asp:Panel>
    
    
</asp:Content>
