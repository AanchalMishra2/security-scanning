<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmEBTracking.aspx.vb" Inherits="Forms_frmEBTracking" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            EB Tracking</h1>
        <div style="text-align: left;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    <asp:Panel ID="pnlEBTrackin" runat="server">
        <table>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblwarning" Visible="False" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCustomer" TabIndex="1" runat="server"
                        AutoPostBack="True" Width="165px">
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
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" TabIndex="2" runat="server"
                        AutoPostBack="True" Width="165px">
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
                    <asp:DropDownList CssClass="select_option" ID="ddlLocation" TabIndex="3" runat="server"
                        AutoPostBack="True" Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqcluster" runat="server" ControlToValidate="ddlLocation"
                        ErrorMessage="Select Cluster" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecluster" runat="server" TargetControlID="reqcluster">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblsiteid" Text="GTL Site ID :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlSiteId" TabIndex="4" runat="server"
                        AutoPostBack="True" Width="165px">
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
                    <asp:Label ID="lblnoofoperator" Text="No Of Operator :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnoofoperator" TabIndex="6" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="lblmeterno" Text="Meter No :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmeterno" TabIndex="9" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblcustomerno" Text="Customer No :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcustomerno" TabIndex="10" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
             <tr>
               <td>
                    <asp:Label ID="lblebconnecteddate" Text="EB Connected Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtebconnecteddate" TabIndex="8" runat="server" Width="160px" Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="ce_ebconnecteddate" Format="dd/MM/yyyy" TargetControlID="txtebconnecteddate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqebconnecteddate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtebconnecteddate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceebconnecteddate" TargetControlID="reqebconnecteddate"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revebconnecteddate" ValidationGroup="diesel"
                        ControlToValidate="txtebconnecteddate" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vceebconnecteddate1" PopupPosition="BottomLeft"
                        runat="server" TargetControlID="revebconnecteddate">
                    </cc1:ValidatorCalloutExtender>
                </td>
                 <td>
                    <asp:Label ID="lbldistrict" Text="District :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdistrict" TabIndex="11" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqdistrict" runat="server" ControlToValidate="txtdistrict"
                        ErrorMessage="Enter District" ValidationGroup="diesel">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedistrict" runat="server" TargetControlID="reqdistrict">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblbillingunitno" Text="Billing Unit No :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbillingunitno" TabIndex="12" runat="server" Width="160px"></asp:TextBox>
                   <%-- ^\d(\d)?(\d)?$--%>
                   <asp:RequiredFieldValidator ID="reqbillingunitno" runat="server" ControlToValidate="txtbillingunitno" ErrorMessage="Enter Bill Unit No" ValidationGroup="Diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillingunitno" runat="server" TargetControlID="reqbillingunitno"></cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revbillingunitno" ValidationGroup ="Diesel" ValidationExpression = "^\d(\d)?(\d)?$" ControlToValidate="txtbillingunitno" ErrorMessage="Enter Valid Data "  runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillingunitno1" runat="server" TargetControlID="revbillingunitno"></cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblonlinebilldate" Text="Online Bill Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtonlinebilldate" TabIndex="13" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="ceonlinebilldate" Format="dd/MM/yyyy" TargetControlID="txtonlinebilldate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqonlinebilldate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtonlinebilldate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceonlinebilldate" TargetControlID="reqonlinebilldate"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revonlinebilldate" ValidationGroup="diesel" ControlToValidate="txtonlinebilldate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vceonlinebilldate1" PopupPosition="BottomLeft"
                        runat="server" TargetControlID="revonlinebilldate">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmonth" Text="Month :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlmonth" TabIndex="14" runat="server"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqmonth" runat="server" ControlToValidate="ddlmonth"
                        ErrorMessage="Select Month" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcemonth" runat="server" TargetControlID="reqmonth">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblbillrecdate" Text="Bill Received Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbillrecdate" TabIndex="15" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cebillrecdate" Format="dd/MM/yyyy" TargetControlID="txtbillrecdate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqbillrecdate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtbillrecdate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillrecdate" TargetControlID="reqbillrecdate"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revbillrecdate" ValidationGroup="diesel" ControlToValidate="txtbillrecdate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillrecdate1" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revbillrecdate">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblfromdate" Text="Bill From Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" TabIndex="16" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cefromdate" Format="dd/MM/yyyy" TargetControlID="txtfromdate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqfromdate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txtfromdate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcefromdate" TargetControlID="reqfromdate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revfromdate" ValidationGroup="diesel" ControlToValidate="txtfromdate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcefromdate1" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revfromdate">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltodate" Text="Bill To Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttodate" TabIndex="17" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cetodate" Format="dd/MM/yyyy" TargetControlID="txttodate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqtodate" ValidationGroup="diesel" runat="server"
                        ControlToValidate="txttodate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetodate" TargetControlID="reqtodate" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtodate" ValidationGroup="diesel" ControlToValidate="txttodate"
                        ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetodate1" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="revtodate">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="cmdate" ControlToValidate="txttodate" ControlToCompare="txtfromdate"
                        ErrorMessage="Please Select To Date Greater than From Date" ValueToCompare="Date"
                        Operator="GreaterThanEqual" runat="server" SetFocusOnError="true" ValidationGroup="diesel">*</asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecmdate" PopupPosition="BottomLeft" runat="server"
                        TargetControlID="cmdate">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcurrentreading" Text="Current Reading :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcurrentreading" TabIndex="18" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqcurrentreading" runat="server" ControlToValidate="txtcurrentreading" ErrorMessage="Enter Current Readning" ValidationGroup="Diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecurrentreading" runat="server" TargetControlID="reqcurrentreading"></cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revcurrentreading" ValidationGroup ="Diesel" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txtcurrentreading" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecurrentreading1" runat="server" TargetControlID="revcurrentreading"></cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbllastreading" Text="Last Reading :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastreading" TabIndex="19" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqlastreading" runat="server" ControlToValidate="txtlastreading" ErrorMessage="Enter Last Readning" ValidationGroup="Diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcelastreading" runat="server" TargetControlID="reqlastreading"></cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revlastreading" ValidationGroup ="Diesel" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txtlastreading" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcelastreading1" runat="server" TargetControlID="revlastreading"></cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltotalunit" Text="Total Units :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalunits" TabIndex="20" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotalunits" runat="server" ControlToValidate="txttotalunits" ErrorMessage="Enter Total Units" ValidationGroup="Diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetotalunits" runat="server" TargetControlID="reqtotalunits"></cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtotalunits" ValidationGroup ="Diesel" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txttotalunits" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetotalunits1" runat="server" TargetControlID="revtotalunits"></cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lbltotalbillamount" Text="Total Bill Amount :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalbillamount" TabIndex="21" runat="server" Width="160px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtotalbillamount" runat="server" ControlToValidate="txttotalbillamount" ErrorMessage="Enter Amount" ValidationGroup="Diesel" >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetotalbillamount" runat="server" TargetControlID="reqtotalbillamount"></cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="rxvtotalbillamount" ValidationGroup ="Diesel" ValidationExpression = "^[0-9]+(\.[0-9]+)?$" ControlToValidate="txttotalbillamount" ErrorMessage="Enter Valid Data"  runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcetotalbillamount1" runat="server" TargetControlID="rxvtotalbillamount"></cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblddcheque" Text="DD/Cheque :" runat="server"></asp:Label>
                </td>
                <td>
                   <%-- <asp:TextBox ID="txtddcheque" TabIndex="22" runat="server" Width="160px"></asp:TextBox>--%>
                   <asp:DropDownList CssClass="select_option" ID="ddldd" TabIndex="14" runat="server"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqdd" runat="server" ControlToValidate="ddldd"
                        ErrorMessage="Select DD/Cheque" ValidationGroup="diesel" InitialValue="0">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcedd" runat="server" TargetControlID="reqdd">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblbillpaymentdate" Text="Bill Payment Date :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbillpaymentdate" TabIndex="23" runat="server" Width="160px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cebillpaymentdate" Format="dd/MM/yyyy" TargetControlID="txtbillpaymentdate"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqbillpaymentdate" ValidationGroup="diesel"
                        runat="server" ControlToValidate="txtbillpaymentdate" ErrorMessage="Select Date">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillpaymentdate" TargetControlID="reqbillpaymentdate"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revbillpaymentdate" ValidationGroup="diesel"
                        ControlToValidate="txtbillpaymentdate" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcebillpaymentdate1" PopupPosition="BottomLeft"
                        runat="server" TargetControlID="revbillpaymentdate">
                    </cc1:ValidatorCalloutExtender>
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
