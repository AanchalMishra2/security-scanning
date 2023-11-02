<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmDieselTracking.aspx.vb" Inherits="Forms_frmDieselTracking" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style1
        {
            width: 152px;
        }
        .style2
        {
            height: 28px;
            width: 152px;
        }
        .style3
        {
            height: 21px;
            width: 152px;
        }
        .style4
        {
            height: 27px;
            width: 152px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            Diesel Tracking</h1>
        <div style="text-align: left;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnktemplate" Text="Download Template" runat="server"></asp:LinkButton></div>
    </div>
    <div class="clear">
    </div>
    <div style="text-align: right;">
        <asp:FileUpload ID="fileInput" runat="server" />
        <asp:RequiredFieldValidator ID="requpload" ControlToValidate="fileInput" ErrorMessage="Select file to upload"
            ValidationGroup="upload" runat="server">*</asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender PopupPosition="BottomLeft" ID="vceupload" TargetControlID="requpload"
            runat="server">
        </cc1:ValidatorCalloutExtender>
        <asp:Button ID="import" ValidationGroup="upload" CssClass="input_btn input_text_x"
            Text="Import" runat="server" />
        <hr />
    </div>
    <asp:Panel ID="pnlDieselTrackin" runat="server">
        <table>
        <tr>
        <td colspan="4" align="center">
            <asp:Label ID="lblwarning" Visible="False" 
                runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCustomer" TabIndex="1" 
                        runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCustomer" runat="server" ControlToValidate="ddlCustomer"
                        ErrorMessage="Select Customer" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecustomer" runat="server" TargetControlID="reqCustomer">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblcircle" Text="Circle :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" TabIndex="2" 
                        runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcircle" runat="server" ControlToValidate="ddlCircle"
                        ErrorMessage="Select Circle" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" runat="server" TargetControlID="reqcircle">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcluster" Text="Cluster :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlLocation" TabIndex="3" 
                        runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcluster" runat="server" ControlToValidate="ddlLocation"
                        ErrorMessage="Select Cluster" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecluster" runat="server" TargetControlID="reqcluster">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblsiteid" Text="Site ID :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlSiteId" TabIndex="4" 
                        runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqsiteid" runat="server" ControlToValidate="ddlSiteId"
                        ErrorMessage="Select Site Id" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcesiteid" runat="server" TargetControlID="reqsiteid">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblsitename" Text="Site Name :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSiteName" TabIndex="5" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblmodeofpayment" Text="Mode Of Payment :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlModeOfPayment" runat="server" 
                        AutoPostBack="True" TabIndex="6"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqmodeofpay" runat="server" ControlToValidate="ddlModeOfPayment"
                        ErrorMessage="Select Mode Of Payment" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcemodeofpay" runat="server" TargetControlID="reqmodeofpay">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblvendor" runat="server" Text="Vendor :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlVendor" TabIndex="7" 
                        runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvendor" runat="server" ControlToValidate="ddlVendor"
                        ErrorMessage="Select Vendor" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcevendor" runat="server" TargetControlID="reqvendor">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblsubpetrocard" Text="Sub Petro Card Number :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlSubPetroCard" TabIndex="8" runat="server"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqSubPetroCard" runat="server" ControlToValidate="ddlSubPetroCard"
                        ErrorMessage="Select Sub PetroCard" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceSubPetroCard" runat="server" TargetControlID="reqSubPetroCard">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblnamedealer" runat="server" Text="Name of Dealer :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlNamePP" runat="server" TabIndex="9"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqNamePP" runat="server" ControlToValidate="ddlNamePP"
                        ErrorMessage="Select Dealer Name" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceNamePP" runat="server" TargetControlID="reqNamePP">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblbillno" Text="Bill Number :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBillNumber" TabIndex="10" runat="server" Width="160px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="reqbillno" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtBillNumber" ErrorMessage="Enetr Bill Number">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillno" TargetControlID="reqbillno" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldateofbill" Text="Date Of Bill :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBillDate" TabIndex="11" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="ce_billdate" Format="dd/MM/yyyy" TargetControlID="txtBillDate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqdate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtBillDate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedate" TargetControlID="reqdate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="diesel" ControlToValidate="txtBillDate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedate1" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revdate">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbldieselqty" Text="Diesel Quantity :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdiselQty" TabIndex="12" runat="server"  Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbloilqty" runat="server" Text="Oil Quantity :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOilQty" runat="server" TabIndex="13" Width="160px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lbldistilledqty" Text="Distilled Quantity :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdistlQty" TabIndex="14" runat="server" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcollentqty" Text="Collent Quantity :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCollentQty" TabIndex="15" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblcottonqty" Text="Cotton Waste Quantity :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCotWestQty" TabIndex="16" runat="server" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblrate" Text="Rate :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRate" TabIndex="17" runat="server" AutoPostBack="True" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqrate" runat="server" ControlToValidate="txtRate"
                        ErrorMessage="Enter Rate" ValidationGroup="diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerate" runat="server" TargetControlID="reqrate">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvrate" ValidationGroup="diesel" ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                        ControlToValidate="txtRate" ErrorMessage="Enter Valid Data" runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerate1" runat="server" TargetControlID="rxvrate">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltotalamt" Text="Total Amount :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalAmt" TabIndex="18" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblhrm" Text="HMR :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHMR" runat="server" TabIndex="19" Width="160px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblgtldate" Text="Date Submitted To GTL :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGTLDate" TabIndex="20" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="ceGTLdate" Format="dd/MM/yyyy" TargetControlID="txtGTLDate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqgtldate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtGTLDate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcegtldate" TargetControlID="reqgtldate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revgtldate" ValidationGroup="diesel" ControlToValidate="txtGTLDate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcegtldate1" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revgtldate">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_petrocardno" Text="PetroCard Number :" runat="server" Visible="False"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList CssClass="select_option" ID="ddlPetrocards" TabIndex="21" 
                        runat="server" Width="165px" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnsave" Text="Save" ValidationGroup="diesel" CssClass="input_btn input_text_x"
                        runat="server" /><asp:Button ID="btnreset" Text="Reset" CssClass="input_btn input_text_x"
                            runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlimport" Visible="false" runat="server">
        <cc1:Accordion ID="acc_1" SelectedIndex="-1" Width="500px" HeaderCssClass="accordionHeader"
            ContentCssClass="accordionContent" runat="server" AutoSize="None" FadeTransitions="true"
            TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
            <Panes>
                <cc1:AccordionPane ID="pn1" runat="server">
                    <Header>
                        <b>Import Status</b></Header>
                    <Content>
                        <asp:GridView ID="dgRecords" runat="server">
                        </asp:GridView>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
    </asp:Panel>
</asp:Content>
