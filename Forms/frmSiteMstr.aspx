<%@ Page Language="VB"  EnableEventValidation="true" MasterPageFile="~/TestMaster.master" AutoEventWireup="false"
    CodeFile="frmSiteMstr.aspx.vb" Inherits="Forms_frmSiteMstr1" Title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="strip">
        <h1>
            Manage Site</h1>
        <div style="text-align: left;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnktemplate" Text="Download Template" runat="server"></asp:LinkButton></div>
    </div>
    <div class="clear">
    </div>
    <table style="width: 100%" class="style1">
        <tr>
            <td class="style9">
                <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="button" />
            </td>
            <td class="style6">
                &nbsp;
            </td>
            <td align="right">
                <asp:FileUpload ID="fileinput" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="fileinput"
                    Display="Dynamic" ValidationGroup="upload" ErrorMessage="Select a file to upload"
                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft"
                    TargetControlID="RequiredFieldValidator9" runat="server">
                </cc1:ValidatorCalloutExtender>
                <asp:Button ID="BtnImport" CssClass="input_btn input_text_x" ValidationGroup="upload"
                    runat="server" Text="Import" />
                <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender WatermarkCssClass="watermarked" ID="tbwesearch" runat="server"
                    TargetControlID="txtsearch" WatermarkText="Search Site Id">
                </cc1:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ControlToValidate="txtsearch" ID="reqsearch" runat="server"
                    ValidationGroup="search" ErrorMessage="Enter Site Id">*</asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vcesearch" PopupPosition="BottomLeft" TargetControlID="reqsearch"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <asp:Button CssClass="input_btn input_text_x" ValidationGroup="search" ID="btnsearchdealer"
                    runat="server" Text="Search" Width="87px" />
        </tr>
        <tr>
            <td class="style5" colspan="3">
                <asp:UpdatePanel ID="UpGVCircle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True"
                                        ForeColor="#C00000" Font-Names="Verdana" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GVSite" runat="server" CssClass="tablestyle" AllowPaging="true"
                            AllowSorting="true" Width="100%" PageSize="6" GridLines="None" DataKeyNames="D_SITE_NO"
                            AutoGenerateColumns="false">
                            <RowStyle CssClass="rowstyle" />
                            <%--<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Size="Larger" />--%>
                            <FooterStyle CssClass="PagerStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="headerstyle" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle CssClass="altrowstyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkdelete" Visible="false" runat="server" Text="Delete" CommandName="Delete"
                                            CausesValidation="false"></asp:LinkButton>
                                        <asp:HiddenField ID="hfCircleID" runat="server" Value='<%# Bind("D_SITE_NO") %>' />
                                        <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                            runat="server" TargetControlID="lnkDelete" PopupControlID="DivDeleteConfirmation"
                                            OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                                        </cc1:ModalPopupExtender>
                                        <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                            TargetControlID="lnkDelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                        </cc1:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="D_SITE_ID" HeaderText="Site Id" SortExpression="D_SITE_ID" />
                                <asp:BoundField DataField="D_SITE_NAME" HeaderText="Site Name" SortExpression="D_SITE_NAME" />
                                <asp:BoundField DataField="D_SITE_TYPE" HeaderText="Site Type" SortExpression="D_SITE_TYPE" />
                                <asp:BoundField DataField="D_NO_OF_OPERATOR" HeaderText="No Of Operator" SortExpression="D_NO_OF_OPERATOR" />
                                <asp:BoundField DataField="D_EB_CONNECTION" HeaderText="EB Connection" SortExpression="D_EB_CONNECTION" />
                                <asp:BoundField DataField="D_DG_CAPACITY" HeaderText="DG Capacity" SortExpression="D_DG_CAPACITY" />
                                <asp:BoundField Visible="false" DataField="D_CIRCLE_ID" HeaderText="Circle Id" SortExpression="D_CIRCLE_ID" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
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
    <asp:HiddenField ID="hdf_CircleId" runat="server" Visible="False" />
    <%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" CssClass="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Delete Circle</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the circle?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit" />
                <asp:Button ID="ButtonDeleteCancel" Text="Cancel" runat="server" CssClass="submit" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlPopup" runat="server" Width="850px" Style="display: none;">
        <asp:UpdatePanel ID="upPnlCircleDetail" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShowPopup" runat="server" Style="display: none;" CssClass="submit" />
                <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
                    PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
                </cc1:ModalPopupExtender>
                <div class="popup_Container">
                    <div class="popup_Titlebar" id="Div2">
                        <div class="TitlebarLeft">
                            Manage Site</div>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcustomer" runat="server" Text="Customer :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcustomer" TabIndex="1" runat="server" Height="20px" Width="150px"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="1" ID="req_customer" ControlToValidate="ddlcustomer"
                                        InitialValue="0" ValidationGroup="site" ErrorMessage="Select Customer" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vce_customer" runat="server" TargetControlID="req_customer">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblcircle" runat="server" Text="Circle :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcircle" TabIndex="2" runat="server" Height="20px" Width="150px"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="2" ID="req_circle" ControlToValidate="ddlcircle"
                                        InitialValue="0" ValidationGroup="site" ErrorMessage="Select Circle" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vce_circle" runat="server" TargetControlID="req_circle">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblSiteId" runat="server" Text="Site ID :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiteId" TabIndex="3" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="3" ID="req_siteId" ControlToValidate="txtSiteId"
                                        ValidationGroup="site" ErrorMessage="Enter Site ID" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender PopupPosition="BottomLeft" ID="vce_siteId" runat="server"
                                        TargetControlID="req_siteId">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_SiteName" runat="server" Text="Site Name :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiteName" TabIndex="4" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="4" ID="req_SiteName" ControlToValidate="txtSiteName"
                                        ValidationGroup="site" ErrorMessage="Enter Site Name" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vce_sitename" runat="server" TargetControlID="req_SiteName">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblcluster" runat="server" Text="Cluster :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCluster" TabIndex="5" runat="server" Height="20px" Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="5" ID="reqCluster" InitialValue="0" ControlToValidate="ddlCluster"
                                        ValidationGroup="site" ErrorMessage="Select Cluster" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceCluster" runat="server" TargetControlID="reqCluster">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblSiteType" runat="server" Text="Site Type :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSiteType" TabIndex="6" runat="server" Height="20px" Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="6" ID="reqSiteType" InitialValue="0" ControlToValidate="ddlSiteType"
                                        ValidationGroup="site" ErrorMessage="Select Site Type" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender PopupPosition="BottomLeft" ID="vceSiteType" runat="server"
                                        TargetControlID="reqSiteType">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOperator" runat="server" Text="No Of Operator :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOperator" TabIndex="7" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="7" ID="reqOperator" ControlToValidate="txtNoOperator"
                                        ValidationGroup="site" ErrorMessage="Enter No Of Operator" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceOperator" runat="server" TargetControlID="reqOperator">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RevOperator" TabIndex="7" ControlToValidate="txtNoOperator"
                                        ValidationExpression="^\d(\d)?(\d)?$" runat="server" ErrorMessage="Enter Valid Data">*</asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceOperator1" runat="server" TargetControlID="RevOperator">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblEBConnection" runat="server" Text="EB Connection :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEBconnection" TabIndex="8" runat="server" Height="20px"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="8" ID="reqEBConnection" InitialValue="0" ControlToValidate="ddlEBconnection"
                                        ValidationGroup="site" ErrorMessage="Select EB Connection" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEBConnection" runat="server" TargetControlID="reqEBConnection">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblDGSet" runat="server" Text="DG Set :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDGSet" TabIndex="9" runat="server" Height="20px" Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator TabIndex="9" ID="reqDGSet" InitialValue="0" ControlToValidate="ddlDGSet"
                                        ValidationGroup="site" ErrorMessage="Select DG Set" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceDGSet" runat="server" PopupPosition="BottomLeft"
                                        TargetControlID="reqDGSet">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDGMake" runat="server" Text="DG Make & Capacity :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDGMake" TabIndex="10" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="10" ID="reqDGMake" ControlToValidate="txtDGMake"
                                        ValidationGroup="site" ErrorMessage="Enter DG Make & Capacity" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceDgMake" runat="server" TargetControlID="reqDGMake">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revDGMake" TabIndex="10" ControlToValidate="txtDGMake"
                                        ValidationExpression="^\d(\d)?(\d)?$" runat="server" ErrorMessage="Enter Valid Data">*</asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceDGMake1" runat="server" TargetControlID="revDGMake">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblMeterNo" runat="server" Text="Meter No :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMeterNo" TabIndex="11" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="11" ID="reqMeterNo" ControlToValidate="txtMeterNo"
                                        ValidationGroup="site" ErrorMessage="Enter Meter No" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceMeterNo" runat="server" TargetControlID="reqMeterNo">
                                    </cc1:ValidatorCalloutExtender>
                                    <%-- <asp:RegularExpressionValidator ID="revMeterNo" TabIndex="10" ControlToValidate="txtMeterNo" ValidationExpression="^\d(\d)?(\d)?$" runat="server" ErrorMessage="Enter Valid Data">*</asp:RegularExpressionValidator>
   <cc1:ValidatorCalloutExtender ID="vceMeterNo1" runat="server" TargetControlID="revMeterNo"></cc1:ValidatorCalloutExtender> --%>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerNo" runat="server" Text="Customer No :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerNo" TabIndex="12" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="12" ID="reqCustomerNo" ControlToValidate="txtCustomerNo"
                                        ValidationGroup="site" ErrorMessage="Enter Customer No" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender PopupPosition="BottomLeft" ID="vceCustomerNo" runat="server"
                                        TargetControlID="reqCustomerNo">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEBDeposit" runat="server" Text="EB Deposit :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEBDeposit" TabIndex="13" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="13" ID="reqEBDiposit" ControlToValidate="txtEBDeposit"
                                        ValidationGroup="site" ErrorMessage="Enter EB Diposite" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEBDiposit" runat="server" TargetControlID="reqEBDiposit">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revEBDeposit" TabIndex="13" ControlToValidate="txtEBDeposit"
                                        ValidationExpression="^[0-9]+(\.[0-9]+)?$" runat="server" ErrorMessage="Enter Valid Data">*</asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEBDeposit1" runat="server" TargetControlID="revEBDeposit">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblBillingCycle" runat="server" Text="Billing Cycle :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBillingCycle" TabIndex="14" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator TabIndex="14" ID="reqBillingCycle" ControlToValidate="txtBillingCycle"
                                        ValidationGroup="site" ErrorMessage="Enter Billing Cycle" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceBillingCycle" runat="server" TargetControlID="reqBillingCycle">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblEBConnectionDT" runat="server" Text="EB Connection Date :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEBConnectionDT" TabIndex="15" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceEbConnectionDT" runat="server" TargetControlID="txtEBConnectionDT">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator TabIndex="15" ID="reqEBConnectionDT" ControlToValidate="txtEBConnectionDT"
                                        ValidationGroup="site" ErrorMessage="Select Eb Connection Date" runat="server">*</asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEBConnectionDT" PopupPosition="BottomLeft" runat="server"
                                        TargetControlID="reqEBConnectionDT">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revEBConnectionDT" TabIndex="15" ControlToValidate="txtEBConnectionDT"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                        runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEBConnectionDT1" PopupPosition="BottomLeft"
                                        runat="server" TargetControlID="revEBConnectionDT">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_active" Text="Active :" runat="server"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="chkActive" runat="server" TextAlign="Left" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnSubmit" CssClass="input_btn input_text_x" runat="server" Text="Submit"
                                        ValidationGroup="site" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" CssClass="input_btn input_text_x" runat="server" Text="Close" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
