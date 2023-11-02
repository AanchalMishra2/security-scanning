<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmSubPetroCardMstr.aspx.vb" Inherits="Forms_frmSubPetroCardMstr" title="Energy Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div class="strip"><h1>Manage Sub Pertocards</h1><div style="text-align:left; ">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton  ID="lnktemplate" Text="Download Template" runat="server"></asp:LinkButton></div></div>
   <div class="clear"></div>
    <table style="width:100%" class="style1">
              
        <tr>
            <td  class="style9">
              <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="button" />
                         </td>
            <td  class="style6">
                &nbsp;</td>
            <td align="right">
            <asp:FileUpload ID="fileinput" runat="server"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ControlToValidate="fileinput" Display="Dynamic" ValidationGroup="upload" ErrorMessage="Select a file to upload"  
                 SetFocusOnError="True">*</asp:RequiredFieldValidator>
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator9"  runat="server"></cc1:ValidatorCalloutExtender>
               <asp:Button ID="BtnImport" CssClass="input_btn input_text_x" ValidationGroup="upload"  runat="server" Text="Import"  />
               
               <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
               <cc1:TextBoxWatermarkExtender WatermarkCssClass="watermarked" ID="tbwesearch" runat="server" TargetControlID="txtsearch" WatermarkText="Search Sub Petrocard No" ></cc1:TextBoxWatermarkExtender>  
               <asp:RequiredFieldValidator ControlToValidate="txtsearch" ID="reqsearch" runat="server" ValidationGroup="search" ErrorMessage="Enter Sub Petrocard No" >*</asp:RequiredFieldValidator>
               <cc1:ValidatorCalloutExtender ID="vcesearch" PopupPosition="BottomLeft" TargetControlID="reqsearch"  runat="server"></cc1:ValidatorCalloutExtender>
                <asp:Button CssClass="input_btn input_text_x" ValidationGroup="search"  ID="btnsearchdealer" runat="server" 
                    Text="Search" Width="87px" />
             </td>
        </tr>
        
        <tr>
            <td  class="style5" colspan="3">
            <asp:UpdatePanel ID="UpGVCircle" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> 
            
             
            
                <asp:GridView ID="GVSubpertrocard" runat="server" CssClass="tablestyle" 
                 AllowPaging="true" AllowSorting="true"  Width="100%" PageSize="6"
                GridLines="None" DataKeyNames="D_S_PETRO_SR_NO"  
                AutoGenerateColumns="false" >
                <RowStyle CssClass="rowstyle"  />
                <%--<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Size="Larger" />--%>
                 <FooterStyle CssClass="PagerStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle CssClass="headerstyle"  />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle CssClass="altrowstyle"  />
                <Columns>
                <asp:TemplateField HeaderText="SNo">
                     <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
               </asp:TemplateField>
               
              <asp:TemplateField Visible="true" >
                   <ItemTemplate>
                        <asp:LinkButton Visible="false" ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                           
                       <asp:LinkButton ID="lnkdelete" Visible="false" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"  ></asp:LinkButton>
                    <asp:HiddenField ID="hfCircleID" runat="server" Value='<%# Bind("D_CIRCLE_ID") %>' />
                       <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                        runat="server" TargetControlID="lnkDelete" PopupControlID="DivDeleteConfirmation"
                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                       </cc1:ModalPopupExtender>
                       <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                        TargetControlID="lnkDelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                       </cc1:ConfirmButtonExtender>
                   </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="D_CIRCLE_NAME" HeaderText="Circle Name" SortExpression="D_CIRCLE_NAME"  />
                 <asp:BoundField DataField="D_VENDOR_NAME" HeaderText="Vendor Name" SortExpression="D_VENDOR_NAME"  />
                 <asp:BoundField DataField="D_CUSTOMER_NAME" HeaderText="Customer Name" SortExpression="D_CUSTOMER_NAME"  />
                 <asp:BoundField DataField="D_PETROCARD_NO" HeaderText="Petrocard Number" SortExpression="D_PETROCARD_NO"  />
                 <asp:BoundField DataField="D_S_PETROCARD_NO" HeaderText="Sub Petrocard Number" SortExpression="D_S_PETROCARD_NO"  />
                 <asp:BoundField DataField="D_ACTIVE" HeaderText="Active" SortExpression="D_ACTIVE" />
                 <asp:BoundField Visible="False" DataField="D_CIRCLE_ID" HeaderText="Circle id" />
			     <asp:BoundField Visible="False" DataField="D_VENDOR_ID" HeaderText="vendor id" />
				 <asp:BoundField Visible="False" DataField="D_S_PETRO_SR_NO" HeaderText="D_S_PETRO_SR_NO" />
				 <asp:BoundField Visible="False" DataField="D_CUSTOMER_ID" HeaderText="Customer Id" />
                </Columns> 
                 <EmptyDataTemplate>
                <table cellpadding="2" cellspacing="2" width="100%"> 
                <tr>
                    <td  align="center">
                        <asp:Label ID="lbl_RecordNotFound" Text="No Record Found" runat="server" Font-Size="Larger" ForeColor="maroon" ></asp:Label>
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
<cc1:Accordion ID="acc_1" SelectedIndex="-1"  Width="500px"  HeaderCssClass="accordionHeader" 
ContentCssClass="accordionContent" runat="server" 
AutoSize="None"
FadeTransitions="true"
TransitionDuration="250"
FramesPerSecond="40"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true">
<Panes>
<cc1:AccordionPane ID="pn1" runat="server">
<Header><b>Import Status</b></Header>
<Content>
<asp:GridView ID="dgRecords"  runat="server"></asp:GridView>
</Content>
</cc1:AccordionPane>

</Panes>
</cc1:Accordion>
</asp:Panel>
    
     <asp:HiddenField ID="hdf_CircleId" runat="server" Visible="False" />
<%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" CssClass ="popupConfirmation">
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
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit" />
                <asp:Button id="ButtonDeleteCancel" Text ="Cancel" runat="server" CssClass="submit" />
            </div>
        </div> 
    </asp:Panel>
 <asp:Panel ID="pnlPopup" runat="server"   Width="500px" style="display:none ;"    >
     <asp:UpdatePanel ID="upPnlCircleDetail" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowPopup" runat="server"  style="display:none;" CssClass="submit" />
        <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
            <div class="popup_Container">
            <div class="popup_Titlebar" id="Div2">
                <div class="TitlebarLeft">
                    Manage Sub Petrocards</div>
                
            </div>
            <div class="popup_Body">
            <table>
            <tr>
             <td><asp:Label ID="lblCustomer" Text="Customer :" runat="server"></asp:Label></td>
            <td><asp:DropDownList Width="150px" Height="20px" ID="ddlCustomer" TabIndex="1" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqCustomer" InitialValue="0" runat="server" 
                    ControlToValidate="ddlCustomer" TabIndex="1"  ErrorMessage="Select Customer" 
                    ValidationGroup="petrocards">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecustomer" TargetControlID="reqCustomer" runat="server"></cc1:ValidatorCalloutExtender>
            </td>
            </tr>
             <tr>
             <td><asp:Label ID="lblcircle" Text="Circle :" runat="server"></asp:Label></td>
            <td><asp:DropDownList Width="150px" Height="20px" ID="ddlcircle" TabIndex="2" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqcircle" runat="server" 
                    ControlToValidate="ddlCustomer" TabIndex="2" InitialValue="0"  ErrorMessage="Select Circle"
                    ValidationGroup="petrocards">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" TargetControlID="reqcircle" runat="server"></cc1:ValidatorCalloutExtender>
            </td>
            </tr>
            <tr>
             <td><asp:Label ID="lblvendor" Text="Vendor :" runat="server"></asp:Label></td>
            <td><asp:DropDownList Width="150px" Height="20px" ID="ddlvendor" TabIndex="3" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqvendor" runat="server" 
                    ControlToValidate="ddlCustomer" TabIndex="3" InitialValue="0"  ErrorMessage="Select Vendor"  
                    ValidationGroup="petrocards">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcevendor" TargetControlID="reqvendor" runat="server"></cc1:ValidatorCalloutExtender>
            </td>
            </tr>
            <tr>
             <td><asp:Label ID="lblpetrocardno" Text="Petrocard No :" runat="server"></asp:Label></td>
            <td><asp:DropDownList Width="150px" Height="20px" ID="ddlpetrocardno" TabIndex="4" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqddlpetrocardno" runat="server" 
                    ControlToValidate="ddlpetrocardno" TabIndex="4"  InitialValue="0" ErrorMessage="Select Petrocard No" 
                   ValidationGroup="petrocards">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceddlpetrocardno" TargetControlID="reqddlpetrocardno" runat="server"></cc1:ValidatorCalloutExtender>
            </td>
            </tr>
            <tr><td><asp:Label ID="lblsubpetrocard" Text="Sub Petrocard :" runat="server"></asp:Label></td>
            <td><asp:TextBox Width="150px" TabIndex="5" Height="20px" ID="txtsubpetrocard" runat="server" ></asp:TextBox>
             <asp:RequiredFieldValidator TabIndex="5" ID="reqsubpetrocard" runat="server" 
                    ControlToValidate="txtsubpetrocard"  ErrorMessage="Enter Sub Petrocard No" 
                    ValidationGroup="petrocards">*</asp:RequiredFieldValidator>
              <cc1:ValidatorCalloutExtender ID="vcesubpetrocard" TargetControlID="reqsubpetrocard" runat="server"></cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblActive" Text="Active :" runat="server"></asp:Label></td>
            <td><asp:CheckBox ID="chkIsactive" runat="server" Checked="true" /></td>
        </tr>
        <tr>
          <td colspan="2" align="center">
               <asp:Button ID="btnSubmit" CssClass="input_btn input_text_x"  runat="server"  Text="Submit" 
                    ValidationGroup="petrocards" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose" CssClass="input_btn input_text_x"  runat="server" Text="Close" />
            </td>
           
        </tr>
            </table>
        
                 
                <%--   <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>--%>
            </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>

</asp:Content>

