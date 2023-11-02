<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmPetroCardMstr.aspx.vb" Inherits="Forms_frmPetroCardMstr1" title="Energy Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Manage Petro Crads</h1></div>
   <div class="clear"></div>
 <table style="width:100%;" >
        <tr>
            <td  >
              <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="button" />
                         </td>
            <td  colspan="2"></td>
        </tr>
        <tr>
            <td  colspan="3">
           <asp:UpdatePanel ID="UpGVCircle" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div style="text-align:center;"><asp:Panel ID="pnlwarning" runat="server"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" />  </asp:Panel></div>
       <%--     <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> --%>
            <asp:GridView ID="GVpetrocards" runat="server" CssClass="tablestyle" 
                 AllowPaging="true" AllowSorting="true"  Width="100%" PageSize="6"
                GridLines="None" DataKeyNames="D_C_NO"  
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
                   <asp:TemplateField HeaderText="S.No">
                     <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
               </asp:TemplateField>   
              <asp:TemplateField  HeaderText="Edit">
                  
                   <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                           
                       <asp:LinkButton ID="lnkdelete" Visible="false" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"  ></asp:LinkButton>
                    <asp:HiddenField ID="hfCircleID" runat="server" Value='<%# Bind("D_C_NO") %>' />
                       <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                        runat="server" TargetControlID="lnkDelete" PopupControlID="DivDeleteConfirmation"
                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                       </cc1:ModalPopupExtender>
                       <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                        TargetControlID="lnkDelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                       </cc1:ConfirmButtonExtender>
                   </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="D_CARD_NO" HeaderText="Card NO" SortExpression="D_CARD_NO"  />
                 <asp:BoundField DataField="D_CARD_NAME" HeaderText="Card Name" SortExpression="D_CARD_NAME"  />
                 <asp:BoundField DataField="D_VENDOR_NAME" HeaderText="Vendor Name" SortExpression="D_VENDOR_NAME"  />
                 <asp:BoundField DataField="D_VALIDITY_DATE" HeaderText="Validity Date" SortExpression="D_VALIDITY_DATE"  />
                 <asp:BoundField DataField="D_FILL_AMOUNT" HeaderText="Filled Amount" SortExpression="D_FILL_AMOUNT"  />
                 <asp:BoundField DataField="D_CARD_LIMIT" HeaderText="Card Limit" SortExpression="D_CARD_LIMIT"  />
                 <asp:BoundField Visible="false"  DataField="D_C_NO" HeaderText="D C No" SortExpression="D_C_NO" />
                 <asp:TemplateField>
                 <FooterTemplate>
                  <asp:Button ID="Button1" runat="server" Text="Button" />
                  </FooterTemplate>
                 </asp:TemplateField>
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
        <tr>
            <td  class="style9">
                 <asp:HiddenField ID="hdf_CircleId" runat="server" Visible="False" /></td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                &nbsp;</td>
        </tr>
     
    </table>

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
 <asp:Panel ID="pnlPopup" runat="server"   Width="600px" style="display:none ;"    >
     <asp:UpdatePanel ID="upPnlCircleDetail" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowPopup" runat="server"  style="display:none;" CssClass="submit" />
        <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
            <div class="popup_Container">
            <div class="popup_Titlebar" id="Div2">
                <div class="TitlebarLeft">
                    Manage Petro Cards </div>
                
            </div>
            <div >
          <table>
          <tr> <td colspan="4"></td> </tr>
   <tr>
   <td><asp:Label ID="lblcustomer" runat="server" Text="Customer :"></asp:Label></td>
   <td><asp:DropDownList ID="ddlcustomer"  TabIndex="1" runat="server" Height="20px" 
           Width="150px" AutoPostBack="True"></asp:DropDownList>
   <asp:RequiredFieldValidator TabIndex="1" ID="req_customer" InitialValue="0" ControlToValidate="ddlcustomer" ValidationGroup="customer" ErrorMessage="Select Customer" runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_customer" runat="server" TargetControlID="req_customer"></cc1:ValidatorCalloutExtender> 
   </td>
   <td><asp:Label ID="lblcircle" runat="server" Text="Circle :"></asp:Label></td>
   <td><asp:DropDownList ID="ddlcircle" TabIndex="2" runat="server" Height="20px" 
           Width="150px" AutoPostBack="false"></asp:DropDownList>
   <asp:RequiredFieldValidator TabIndex="2" InitialValue="0" ID="req_circle" ControlToValidate="ddlcircle" ValidationGroup="customer" ErrorMessage="Select Circle" runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_circle" runat="server" TargetControlID="req_circle"></cc1:ValidatorCalloutExtender> 
   </td>
   </tr>
     <tr>
   <td><asp:Label ID="lblcard" runat="server" Text="Card Number :"></asp:Label></td>
   <td><asp:TextBox ID="txtCardNo" TabIndex="3" runat="server"></asp:TextBox>
   <asp:RequiredFieldValidator TabIndex="3" ID="req_cradno" ControlToValidate="txtCardNo" ValidationGroup="customer" ErrorMessage="Enter Card Number" runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_card_no" runat="server" TargetControlID="req_cradno"></cc1:ValidatorCalloutExtender> 
   </td>
   <td><asp:Label ID="lbl_nameOn_card" runat="server" Text="Name On Card :"></asp:Label></td>
   <td><asp:TextBox ID="txtnameOnCard" TabIndex="4" runat="server"></asp:TextBox>
   <asp:RequiredFieldValidator TabIndex="4" ID="req_nameoncard" ControlToValidate="txtnameOnCard" ValidationGroup="customer" ErrorMessage="Enter Name On Card " runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_nameoncard" runat="server" TargetControlID="req_nameoncard"></cc1:ValidatorCalloutExtender> 
   </td>
   </tr>
     <tr>
   <td><asp:Label ID="lbl_vendor" runat="server" Text="Vendor Name :"></asp:Label></td>
   <td><asp:DropDownList ID="ddlVendor" TabIndex="5" runat="server" Height="20px" Width="150px" ></asp:DropDownList>
   <asp:RequiredFieldValidator InitialValue="0" TabIndex="5" ID="req_vendor" ControlToValidate="ddlVendor" ValidationGroup="customer" ErrorMessage="Select Vendor" runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_vendor" runat="server" TargetControlID="req_vendor"></cc1:ValidatorCalloutExtender> 
   </td>
   <td><asp:Label ID="lbl_validitydt" runat="server" Text="Validity Date :"></asp:Label></td>
   <td><asp:TextBox ID="txtValidityDate" TabIndex="6" runat="server"></asp:TextBox>
   <cc1:CalendarExtender ID="ajx_calV" Format="dd/MM/yyyy" runat ="server" TargetControlID ="txtValidityDate"></cc1:CalendarExtender>
   <asp:RequiredFieldValidator TabIndex="6" ID="req_validityDt" ControlToValidate="txtValidityDate" ValidationGroup="customer" ErrorMessage="Select Validity Date " runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_validity" runat="server" TargetControlID="req_validityDt"></cc1:ValidatorCalloutExtender> 
   <asp:RegularExpressionValidator ID="revValidityDate" ValidationGroup="customer" TabIndex="6" ControlToValidate="txtValidityDate" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" runat="server" ErrorMessage="Check Date Format (DD/MM/YYYY)">*</asp:RegularExpressionValidator>
   <cc1:ValidatorCalloutExtender ID="vceValidityDate"  PopupPosition ="BottomLeft"  runat="server" TargetControlID="revValidityDate"></cc1:ValidatorCalloutExtender>
   </td>
   </tr>
    <tr>
   <td><asp:Label ID="lbl_fillamount" runat="server" Text="Filled Amount :"></asp:Label></td>
   <td><asp:TextBox ID="txtFilledAmount" TabIndex="7" runat="server"></asp:TextBox>
   <asp:RequiredFieldValidator TabIndex="7" ID="req_filledAmount" ControlToValidate="txtFilledAmount" ValidationGroup="customer" ErrorMessage="Enter Filled Amount" runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_filledAmount" runat="server" TargetControlID="req_filledAmount"></cc1:ValidatorCalloutExtender> 
   <asp:RegularExpressionValidator TabIndex="7" ValidationGroup="customer" ID="req_filledAmount1" ErrorMessage="Enter Valid Data" ValidationExpression="^[0-9]+(\.[0-9]+)?$" ControlToValidate="txtFilledAmount" runat="server">*</asp:RegularExpressionValidator>
   <cc1:ValidatorCalloutExtender ID="vce_filledAmount1" runat="server" TargetControlID="req_filledAmount1"></cc1:ValidatorCalloutExtender> 
   </td>
   <td><asp:Label ID="lbl_petrocardLimit" runat="server" Text="Petro Card Limit :"></asp:Label></td>
   <td><asp:TextBox ID="txtPetrocardLimit" TabIndex="8" runat="server"></asp:TextBox>
   <asp:RequiredFieldValidator TabIndex="8" ID="req_limit" ControlToValidate="txtPetrocardLimit" ValidationGroup="customer" ErrorMessage="Enter Name On Card " runat="server">*</asp:RequiredFieldValidator>
   <cc1:ValidatorCalloutExtender ID="vce_limit" runat="server" TargetControlID="req_limit"></cc1:ValidatorCalloutExtender> 
   <asp:RegularExpressionValidator TabIndex="8" ValidationGroup="customer" ID="regEx_security" ErrorMessage="Enter Valid Data" ValidationExpression="^[0-9]+(\.[0-9]+)?$" ControlToValidate="txtPetrocardLimit" runat="server">*</asp:RegularExpressionValidator>
   <cc1:ValidatorCalloutExtender ID="vce_limit1" runat="server" TargetControlID="regEx_security"></cc1:ValidatorCalloutExtender> 
   </td>
   </tr>
   <tr>
   <td><asp:Label ID="lbl_active" Text="Active :" runat="server"></asp:Label></td>
   <td ><asp:CheckBox ID="chkIsactive" runat="server"   TextAlign="Left" Checked="True" /> </td>
    </tr>
    <tr>
    <td colspan="6" align="center">
    <asp:button CssClass="input_btn input_text_x" Visible="false" id="btnNew" runat="server"  Text="New" Font-Bold="True"
							tabIndex="10"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button CssClass="input_btn input_text_x" id="btnSubmit" ValidationGroup="customer" runat="server"   Text="Submit"
							Font-Bold="True" tabIndex="11"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button CssClass="input_btn input_text_x" id="btnClose" runat="server"  Text="Close"
							Font-Bold="True" tabIndex="12"></asp:button>
    </td>
    </tr>
   </table>
            </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
</asp:Content>

