<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmUserLoginRpt.aspx.vb" Inherits="Forms_frmUserLoginRpt" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 13px;
        }
        .style2
        {
            width: 453px;
        }
    </style>
    <style type="text/css">
        .gvFixedHeader
        {
            font-weight: bold;
            position: relative;
            background-color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            User Login Details</h1>
    </div>
    <asp:Panel ID="pnl_user" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_User" runat="server" Text="User :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddl_user" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="req_user" ValidationGroup="validate" InitialValue="0"
                        runat="server" ControlToValidate="ddl_user" ErrorMessage="Select User">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_user" TargetControlID="req_user" runat="server">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_frmdt" Text="From Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_frmdt" TabIndex="2" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_frm" Format="dd/MM/yyyy" TargetControlID="txt_frmdt"
                        runat="server">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="req_frmdt" TabIndex="2" ValidationGroup="validate"
                        runat="server" ControlToValidate="txt_frmdt" ErrorMessage="Select From Date">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_frmdt" TargetControlID="req_frmdt" runat="server">
                    </asp:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revfrmdt" TabIndex="2" ValidationGroup="validate"
                        ControlToValidate="txt_frmdt" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <asp:ValidatorCalloutExtender ID="vcefrmdt" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revfrmdt">
                    </asp:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbl_todt" Text="To Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_todt" TabIndex="3" runat="server" Height="22px" Width="163px"></asp:TextBox>
                    <asp:CalendarExtender Format="dd/MM/yyyy" ID="ce_todt" TargetControlID="txt_todt"
                        runat="server">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="req_todt" TabIndex="3" ValidationGroup="validate"
                        runat="server" ControlToValidate="txt_todt" ErrorMessage="Select To Date">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_todt" TargetControlID="req_todt" runat="server">
                    </asp:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtodt" TabIndex="3" ValidationGroup="validate"
                        ControlToValidate="txt_todt" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <asp:ValidatorCalloutExtender ID="vcetodt" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revtodt">
                    </asp:ValidatorCalloutExtender>
                   <asp:CompareValidator ID="cmdate" ControlToValidate="txt_todt" ControlToCompare="txt_frmdt"
                        ErrorMessage="Please Select To Date Greater than From Date" 
                        Operator="GreaterThanEqual" Type="Date" runat="server" SetFocusOnError="true" ValidationGroup="validate">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender ID="vcecmdate"  runat="server"
                        TargetControlID="cmdate">
                    </asp:ValidatorCalloutExtender>
                    
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btn_View" ValidationGroup="validate" Text="View" CssClass="input_btn input_text_x"
                        CausesValidation="true" runat="server" Width="142px" />
                    <asp:Button ID="btn_excel" Visible="false" Text="Excel" CssClass="input_btn input_text_x"
                        runat="server" Width="142px" />
                    <asp:Button ID="btn_reset" Text="Reset" CssClass="input_btn input_text_x" runat="server"
                        Width="142px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_gdv" Visible="false" runat="server" Height="200px" Width="800px"
        ScrollBars="Vertical">
        <asp:GridView ID="gdvUser" runat="server" CssClass="tablestyle" AllowSorting="True"
            GridLines="None" AutoGenerateColumns="False" Width="100%">
            <RowStyle CssClass="rowstyle" />
            <FooterStyle CssClass="PagerStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="gvFixedHeader" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle CssClass="altrowstyle" />
            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hf_UserId" runat="server" Value='<%#Bind("D_USER_ID") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="FULL NAME" HeaderText="FULL NAME" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="D_LOGIN_NAME" HeaderText="LOGIN NAME" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <%-- <asp:BoundField DataField="D_MODULE_NAME" HeaderText="MODULE NAME" 
                            HeaderStyle-HorizontalAlign ="left" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                <asp:BoundField DataField="LOGIN" HeaderText="LOGIN" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="LOGOUT" HeaderText="LOGOUT" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TOTAL TIME" HeaderText="TOTAL TIME" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlimport" Visible="false" runat="server">
        <asp:Accordion ID="acc_1" SelectedIndex="-1" Width="500px" HeaderCssClass="accordionHeader"
            ContentCssClass="accordionContent" runat="server" AutoSize="None" FadeTransitions="true"
            TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
            <Panes>
                <asp:AccordionPane ID="pn1" runat="server">
                    <Header>
                        <b>Login Report</b></Header>
                    <Content>
                        <asp:GridView ID="gdvUser_1" runat="server" CssClass="tablestyle" AllowSorting="True"
                            GridLines="None" AutoGenerateColumns="False" Width="100%">
                            <RowStyle CssClass="rowstyle" />
                            <FooterStyle CssClass="PagerStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="gvFixedHeader" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle CssClass="altrowstyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%-- <%# Container.DataItemIndex + 1 %>--%>
                                        <asp:HiddenField ID="hf_UserId" runat="server" Value='<%#Bind("D_USER_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FULL NAME" HeaderText="FULL NAME" HeaderStyle-HorizontalAlign="left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_LOGIN_NAME" HeaderText="LOGIN NAME" HeaderStyle-HorizontalAlign="left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="D_MODULE_NAME" HeaderText="MODULE NAME" 
                            HeaderStyle-HorizontalAlign ="left" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                                <asp:BoundField DataField="LOGIN" HeaderText="LOGIN" HeaderStyle-HorizontalAlign="left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LOGOUT" HeaderText="LOGOUT" HeaderStyle-HorizontalAlign="left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TOTAL TIME" HeaderText="TOTAL TIME" HeaderStyle-HorizontalAlign="left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </Content>
                </asp:AccordionPane>
            </Panes>
        </asp:Accordion>
    </asp:Panel>
</asp:Content>
