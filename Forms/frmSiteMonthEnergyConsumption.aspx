<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmSiteMonthEnergyConsumption.aspx.vb" Inherits="Forms_frmSiteMonthEnergyConsumption"
    Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 111px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            Monthly Site Energy Consumption</h1>
    </div>
    <div class="clear">
    </div>
    <asp:Panel ID="pnl_controls" runat="server">
        <table>
            <tr>
                <td colspan="10" align="center">
                    <asp:Label ID="lblwarning" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCustomer" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCustomer" TabIndex="1" ControlToValidate="ddlCustomer"
                        InitialValue="0" ErrorMessage="Select Customer" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCustomer" PopupPosition="BottomRight" TargetControlID="reqCustomer" runat="server">
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
                        ErrorMessage="Select Circle" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" PopupPosition="BottomRight" TargetControlID="reqCircle" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lblCluster" Text="Cluster :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlcluster" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcluster" ControlToValidate="ddlcluster" InitialValue="0"
                        ErrorMessage="Select Cluster" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecluster" PopupPosition="BottomRight" TargetControlID="reqcluster" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblsiteId" Text="Site Id :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlSiteId" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqSiteId" ControlToValidate="ddlSiteId" InitialValue="0"
                        ErrorMessage="Select SiteId" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceSiteId" PopupPosition="BottomRight" TargetControlID="reqSiteId" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblSiteame" Text="Site Name :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSiteName" runat="server" ReadOnly="True" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqSiteName" runat="server" ControlToValidate="txtSiteName"
                        Display="Dynamic" ErrorMessage="Site Name" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceSiteName" PopupPosition="BottomRight" TargetControlID="reqSiteName" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lblSiteType" Text="Site Type :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsitetype" runat="server" ReadOnly="True" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqsitetype" runat="server" ControlToValidate="txtsitetype"
                        Display="Dynamic" ErrorMessage="Site Type" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcesitetype" TargetControlID="reqsitetype" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Selection Year :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlselectionyear" runat="server" Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqselectionyear" TabIndex="7" ControlToValidate="ddlselectionyear"
                        InitialValue="0" ErrorMessage="Select Year" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender  ID="vceselectionyear" PopupPosition="BottomRight" TargetControlID="reqselectionyear"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblselectionMonth" Text="Selection Month :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlselectionmonth" runat="server"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqselectionmonth" TabIndex="8" ControlToValidate="ddlselectionmonth"
                        InitialValue="0" ErrorMessage="Select Month" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceselectionmonth" TargetControlID="reqselectionmonth"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lblEBopeningunit" Text="EB Opening Unit :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEbopeningunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqebopeningunit" runat="server" ControlToValidate="txtEbopeningunit"
                        Display="Dynamic" ErrorMessage="EB Opening Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceebopeningunit" TargetControlID="reqebopeningunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvebopeningunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtEbopeningunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceebopeningunit1" TargetControlID="rxvebopeningunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEBclosingunit" Text="EB Closing Unit :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEBclosingunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEBclosingunit" runat="server" ControlToValidate="txtEBclosingunit"
                        Display="Dynamic" ErrorMessage="EB Closing Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceEBclosingunit" TargetControlID="reqEBclosingunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvEBclosingunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtEBclosingunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceEBclosingunit1" TargetControlID="rxvEBclosingunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltotalconsumeunit" Text="Total Consumed Unit :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalconsumeunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotalconsumeunit" runat="server" ControlToValidate="txttotalconsumeunit"
                        Display="Dynamic" ErrorMessage="Total Consumed Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotalconsumeunit" TargetControlID="reqEBclosingunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotalconsumeunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotalconsumeunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotalconsumeunit1" TargetControlID="rxvtotalconsumeunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblebrate" Text="EB Rate :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtebrate" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqebrate" runat="server" ControlToValidate="txtebrate"
                        Display="Dynamic" ErrorMessage="EB Rate" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceebrate" TargetControlID="reqebrate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvebrate" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtebrate" runat="server" ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vceebrate1" TargetControlID="rxvebrate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotalebcost" Text="Total EB Cost :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalebcost" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotalebcost" runat="server" ControlToValidate="txttotalebcost"
                        Display="Dynamic" ErrorMessage="Total EB Cost" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotalebcost" TargetControlID="reqtotalebcost"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotalebcost" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotalebcost" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender  PopupPosition="BottomRight" ID="vcetotalebcost1" TargetControlID="rxvebrate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldgopeningunit" Text="DG Opening Unit :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgopeningunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgopeningunit" runat="server" ControlToValidate="txtdgopeningunit"
                        Display="Dynamic" ErrorMessage="DG Opening Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgopeningunit" TargetControlID="reqdgopeningunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgopeningunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgopeningunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgopeningunit1" TargetControlID="rxvdgopeningunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lbldgclosingunit" Text="DG Closing Unit :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgclosingunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgclosingunit" runat="server" ControlToValidate="txtdgclosingunit"
                        Display="Dynamic" ErrorMessage="DG Closing Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgclosingunit" TargetControlID="reqdgclosingunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgclosingunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgclosingunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgclosingunit1" TargetControlID="rxvdgclosingunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotaldgconsumeunit" Text="Total Diesel Consume Unit :" runat="server"> </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotaldgconsumeunit" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotaldgconsumeunit" runat="server" ControlToValidate="txttotaldgconsumeunit"
                        Display="Dynamic" ErrorMessage="Total Diesel Consume Unit" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgconsumeunit" TargetControlID="reqtotaldgconsumeunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotaldgconsumeunit" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotaldgconsumeunit" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgconsumeunit1" TargetControlID="rxvtotaldgconsumeunit"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldgrunhouropning" Text="DG Run Hour Opening  :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgrunhouropning" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgrunhouropning" runat="server" ControlToValidate="txtdgrunhouropning"
                        Display="Dynamic" ErrorMessage="DG Run Hour Opening" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgrunhouropning" TargetControlID="reqdgrunhouropning"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgrunhouropning" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgrunhouropning" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgrunhouropning1" TargetControlID="rxvdgrunhouropning"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lbldgrunhourclosing" Text="DG Run Hour Closing :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgrunhourclosing" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgrunhourclosing" runat="server" ControlToValidate="txtdgrunhourclosing"
                        Display="Dynamic" ErrorMessage="DG Run Hour Closing" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedgrunhourclosing" TargetControlID="reqdgrunhourclosing"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgrunhourclosing" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgrunhourclosing" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender  PopupPosition="BottomRight" ID="vcedgrunhourclosing1" TargetControlID="rxvdgrunhourclosing"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotaldgrunhour" Text="Total DG Run Hour  :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotaldgrunhour" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotaldgrunhour" runat="server" ControlToValidate="txttotaldgrunhour"
                        Display="Dynamic" ErrorMessage="Total DG Run Hour" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgrunhour" TargetControlID="reqtotaldgrunhour"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotaldgrunhour" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotaldgrunhour" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgrunhour1" TargetControlID="rxvtotaldgrunhour"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldgtankopeningstock" Text="Total DG Tank Opening Stock :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgtankopeningstock" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgtankopeningstock" runat="server" ControlToValidate="txtdgtankopeningstock"
                        Display="Dynamic" ErrorMessage="Total DG Tank Opening Stock" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedgtankopeningstock" TargetControlID="reqdgtankopeningstock"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgtankopeningstock" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgtankopeningstock" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgtankopeningstock1" TargetControlID="rxvdgtankopeningstock"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldgfilledformonth" Text="DG Filled For month :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdgfilledformonth" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdgfilledformonth" runat="server" ControlToValidate="txtdgfilledformonth"
                        Display="Dynamic" ErrorMessage="DG Filled For month" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgfilledformonth" TargetControlID="reqdgfilledformonth"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdgfilledformonth" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdgfilledformonth" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedgfilledformonth1" TargetControlID="rxvdgfilledformonth"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotaltankclosingstock" Text="Total DG Tank Closing Stock :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotaltankclosingstock" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotaltankclosingstock" runat="server" ControlToValidate="txttotaltankclosingstock"
                        Display="Dynamic" ErrorMessage="Total DG Tank Closing Stock" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaltankclosingstock" TargetControlID="reqtotaltankclosingstock"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotaltankclosingstock" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotaltankclosingstock" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaltankclosingstock1" TargetControlID="rxvtotaltankclosingstock"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltotaldgconsumeformonth" Text="Total Diesel Consumed For Month  :"
                        runat="server"> </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotaldgconsumeformonth" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotaldgconsumeformonth" runat="server" ControlToValidate="txttotaldgconsumeformonth"
                        Display="Dynamic" ErrorMessage="Total Diesel Consumed For Month" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgconsumeformonth" TargetControlID="rxvtotaldgconsumeformonth"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotaldgconsumeformonth" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotaldgconsumeformonth" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldgconsumeformonth1" TargetControlID="rxvtotaldgconsumeformonth"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lblrateofdiesel" Text="Rate Of Diesel  :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtrateofdiesel" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqrateofdiesel" runat="server" ControlToValidate="txtrateofdiesel"
                        Display="Dynamic" ErrorMessage="Rate Of Diesel" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcerateofdiesel" TargetControlID="rxvrateofdiesel"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvrateofdiesel" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtrateofdiesel" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcerateofdiesel1" TargetControlID="rxvrateofdiesel"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotaldieselcost" Text="Total Diesel Cost  :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotaldieselcost" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotaldieselcost" runat="server" ControlToValidate="txttotaldieselcost"
                        Display="Dynamic" ErrorMessage="Total Diesel Cost" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldieselcost" TargetControlID="rxvtotaldieselcost"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotaldieselcost" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotaldieselcost" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotaldieselcost1" TargetControlID="rxvtotaldieselcost"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblcphfordg" Text="CPH for DG :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcphfordg" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqcphfordg" runat="server" ControlToValidate="txtcphfordg"
                        Display="Dynamic" ErrorMessage="CPH for DG" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcecphfordg" TargetControlID="rxvcphfordg" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvcphfordg" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtcphfordg" runat="server" ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcecphfordg1" TargetControlID="rxvcphfordg" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblmobiledg" Text="Mobile DG :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlmobiledg" runat="server" 
                        Width="165px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqmobiledg" ControlToValidate="ddlmobiledg" InitialValue="0"
                        ErrorMessage="Select Mobile DG" ValidationGroup="cluster" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender  PopupPosition="BottomRight" ID="vcemobiledg" TargetControlID="reqmobiledg" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldateofdeployment" Text="Date Of Deployment :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdateofdeployment" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender  ID="cedateofdeployment" Format="dd/MM/yyyy" TargetControlID="txtdateofdeployment"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqdateofdeployment" TabIndex="2" ValidationGroup="cluster"
                        runat="server" ControlToValidate="txtdateofdeployment" ErrorMessage="Select Deployment Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedateofdeployment" TargetControlID="reqdateofdeployment"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdateofdeployment" TabIndex="2" ValidationGroup="cluster"
                        ControlToValidate="txtdateofdeployment" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender  ID="vcedateofdeployment1" PopupPosition="BottomRight"
                        runat="server" TargetControlID="rxvdateofdeployment">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldateofdemobilization" Text="Date Of DeMobilization" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdateofdemobilization" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cedateofdemobilization" Format="dd/MM/yyyy" TargetControlID="txtdateofdemobilization"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqdateofdemobilization" TabIndex="2" ValidationGroup="cluster"
                        runat="server" ControlToValidate="txtdateofdemobilization" ErrorMessage="Select DeMobilization Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedateofdemobilization" TargetControlID="reqdateofdemobilization"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdateofdemobilization" TabIndex="2" ValidationGroup="cluster"
                        ControlToValidate="txtdateofdeployment" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedateofdemobilization1" 
                        runat="server" TargetControlID="rxvdateofdemobilization">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltotalmobiledgrunhour" Text="Total Mobile DG Run Hour :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalmobiledgrunhour" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotalmobiledgrunhour" runat="server" ControlToValidate="txttotalmobiledgrunhour"
                        Display="Dynamic" ErrorMessage="Total Mobile DG Run Hour" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetotalmobiledgrunhour" TargetControlID="reqtotalmobiledgrunhour"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotalmobiledgrunhour" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txttotalmobiledgrunhour" runat="server"
                        ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcetotalmobiledgrunhour1" TargetControlID="rxvtotalmobiledgrunhour"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldcmeter1" Text="DC Meter 1 :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdcmeter1" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdcmeter1" runat="server" ControlToValidate="txtdcmeter1"
                        Display="Dynamic" ErrorMessage="DC Meter 1" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter1" TargetControlID="reqdcmeter1" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdcmeter1" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdcmeter1" runat="server" ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter11" TargetControlID="rxvdcmeter1" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldcmeter2" Text="DC Meter 2 :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdcmeter2" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdcmeter2" runat="server" ControlToValidate="txtdcmeter2"
                        Display="Dynamic" ErrorMessage="DC Meter 2" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter2" TargetControlID="reqdcmeter2" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdcmeter2" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdcmeter2" runat="server" ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter21" TargetControlID="rxvdcmeter2" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label Text="DC Meter 3 :" ID="lbldcmeter3" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdcmeter3" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdcmeter3" runat="server" ControlToValidate="txtdcmeter3"
                        Display="Dynamic" ErrorMessage="DC Meter 3" ValidationGroup="cluster">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter3" TargetControlID="reqdcmeter3" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvdcmeter3" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ErrorMessage="Invalid Data" ControlToValidate="txtdcmeter3" runat="server" ValidationGroup="cluster">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender PopupPosition="BottomRight" ID="vcedcmeter31" TargetControlID="rxvdcmeter3" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblremarks" Text="Remarks :" runat="server"></asp:Label>&nbsp;:
                </td>
                <td colspan="6">
                    <asp:TextBox ID="txtremarks" runat="server" Width="165px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button CssClass="input_btn input_text_x" ValidationGroup="cluster" ID="btnsave"
                        runat="server" Text="Save" />
                    <asp:Button CssClass="input_btn input_text_x" ID="btnreset" runat="server" Text="Reset" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
