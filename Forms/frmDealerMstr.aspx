<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmDealerMstr.aspx.vb" Inherits="Forms_frmCustomerMstr" title="Energy Tracker"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .style5
        {
        }
        .style6
        {
            width: 28px;
        }
        .style7
        {
            width: 334px;
        }
        .style1
    {
        width: 100%;
        border-style: solid;
        border-width: 1px;
    }
        .style9
        {
        width: 136px;
    }
        .style12
        {
            width: 134px;
        }
        .style13
        {
            width: 304px;
        }
        .style14
        {
            width: 232px;
        }
        .style15
        {
            width: 167px;
        }
        </style> 
       
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="strip"><h1>Manage Dealer</h1><div style="text-align:left; ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton  ID="LinkButton1" Text="Download Template" runat="server"></asp:LinkButton></div></div>
   <div class="clear"></div>
    <table style="width:100%" class="style1">
               
        <tr>
            <td "width:20%" class="style9">
              <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="button" />
                         </td>
            <td "width:20%" class="style6">
                &nbsp;</td>
            <td "width:20%" align="right">
        <asp:FileUpload ID="fileinput" runat="server"   />
            <asp:RequiredFieldValidator  ID="RequiredFieldValidator9" runat="server" 
                ControlToValidate="fileinput" Display="Dynamic" ValidationGroup="upload" ErrorMessage="Select a file to upload"  
                 SetFocusOnError="True">*</asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator9"  runat="server"></cc1:ValidatorCalloutExtender>
               <asp:Button ID="BtnImport" CssClass="input_btn input_text_x" ValidationGroup="upload"  runat="server" Text="Import"  />
               <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
               <cc1:TextBoxWatermarkExtender WatermarkCssClass="watermarked" ID="tbwesearch" runat="server" TargetControlID="txtsearch" WatermarkText="Search Dealer" ></cc1:TextBoxWatermarkExtender>  
               <asp:RequiredFieldValidator ControlToValidate="txtsearch" ID="reqsearch" runat="server" ValidationGroup="search" ErrorMessage="Enter Search Text" >*</asp:RequiredFieldValidator>
               <cc1:ValidatorCalloutExtender ID="vcesearch" PopupPosition="BottomLeft" TargetControlID="reqsearch"  runat="server"></cc1:ValidatorCalloutExtender>
                <asp:Button CssClass="input_btn input_text_x" ValidationGroup="search"  ID="btnsearchdealer" runat="server" 
                    Text="Search" Width="87px" />
             </td>
        </tr>
        
        <tr>
            <td "width:20%" class="style5" colspan="3">
            <asp:UpdatePanel ID="UpGVCust" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> 
            
                <asp:GridView ID="GVCustomer" runat="server" CssClass="tablestyle" 
                 AllowPaging="true" AllowSorting="true"  Width="100%"
                GridLines="None" DataKeyNames="D_PUMP_ID"  PageSize="6" 
                AutoGenerateColumns="false" >
                <RowStyle CssClass="rowstyle"  />
              <%--  <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
              <asp:TemplateField  HeaderText="Edit " >
                   <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                           
                       <asp:LinkButton Visible="false" ID="lnkdelete" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"  ></asp:LinkButton>
                    <asp:HiddenField ID="hfCustomerID" runat="server" Value='<%# Bind("D_PUMP_ID") %>' />
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
                 <asp:BoundField DataField="D_CUSTOMER_NAME" HeaderText="Customer Name" SortExpression="D_CUSTOMER_NAME"  />
                 <asp:BoundField DataField="D_PUMP_NAME" HeaderText="Dealer Name" SortExpression="D_PUMP_NAME"   />
                 <asp:BoundField DataField="D_LOCATION" HeaderText="Location Name" SortExpression="D_LOCATION"   />
                 <asp:BoundField Visible="false"  DataField="D_CIRCLE_ID" HeaderText="Circle Id" SortExpression="D_CIRCLE_ID" />
                 <asp:BoundField Visible="false"  DataField="D_CUSTOMER_ID" HeaderText="Customer Id" SortExpression="D_Customer_ID" />
                 <asp:BoundField DataField="D_ACTIVE" HeaderText="Active" SortExpression="D_ACTIVE" />
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
     <asp:HiddenField ID="hdf_CustomerId" runat="server" Visible="False" />
<%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" CssClass ="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Delete customer</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the customer?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit" />
                <asp:Button id="ButtonDeleteCancel" Text ="Cancel" runat="server" CssClass="submit" />
            </div>
        </div> 
    </asp:Panel>
 <asp:Panel ID="pnlPopup" runat="server" Width="780px"   style="display:none;"     >
     <asp:UpdatePanel ID="upPnlcustomerDetail" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none;" CssClass="submit" />
        <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
            <div class="popup_Container">
            <div class="popup_Titlebar" id="Div2">
                <div class="TitlebarLeft">
                    Manage Dealer</div>
                
            </div>
            <div class="popup_Body">
            <table>
            
             <tr >
                 <td class="style14"><asp:Label ID="lbl_custname" text="Customer Name :" runat="server"></asp:Label> </td>
                 <td class="style15">
                 <asp:DropDownList  ID="ddl_customer" runat="server" TabIndex="1" Height="20px" Width="126px" 
                         AutoPostBack="True"></asp:DropDownList>
                
                 <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_customer" runat="server" 
                     ControlToValidate="ddl_customer" ErrorMessage="Select Customer" 
                     InitialValue="0" >*</asp:RequiredFieldValidator>
                     <cc1:ValidatorCalloutExtender ID="vce_dealer" runat="server" TargetControlID="req_customer" ></cc1:ValidatorCalloutExtender>
                 </td>
                 <td  class="style12"><asp:Label ID="lbl_circle" runat="server" 
                         text="Circle Name :"></asp:Label> </td>
                 <td>
                 <asp:DropDownList ID="ddl_circle" runat="server" TabIndex="2" Height="20px" Width="126px" 
                         AutoPostBack="True">
                 </asp:DropDownList>
                 
                 <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_circle" runat="server" 
                     ControlToValidate="ddl_circle" ErrorMessage="Select Circle" InitialValue="0" 
                     >*</asp:RequiredFieldValidator>
                     <cc1:ValidatorCalloutExtender ID="vce_circle" runat="server" TargetControlID="req_circle" ></cc1:ValidatorCalloutExtender>
                 </td>
            
             </tr>
             <tr>
             <td  class="style14"><asp:Label ID="lbl_dealer" text="Dealer Name :" runat="server"></asp:Label> </td>
               <td  class="style15"><asp:TextBox ID="txt_dealer" TabIndex="3" runat="server" CssClass="textboxS"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_dealer" runat="server" 
                    ControlToValidate="txt_dealer" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Dealer Name" SetFocusOnError="True" 
                    >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_dealer1" runat="server" TargetControlID="req_dealer" ></cc1:ValidatorCalloutExtender>
                 </td>
                 <td  class="style12"><asp:Label ID="lbl_cluster" text="Cluster Name :" runat="server"></asp:Label> </td>
                 <td >
                 <asp:DropDownList ID="ddl_cluster" runat="server" TabIndex="4" Height="20px" Width="123px">
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_cluster" runat="server" 
                     ControlToValidate="ddl_cluster" ErrorMessage="Select Cluster" InitialValue="0" 
                    >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_cluster" runat="server" TargetControlID="req_cluster" ></cc1:ValidatorCalloutExtender>
                </td> 
           </tr>
            <tr>
             <td  class="style14"><asp:Label ID="lbl_contact" text="Contact  No :" runat="server"></asp:Label> </td>
               <td  class="style15"><asp:TextBox ID="txt_contact" TabIndex="5" runat="server" CssClass="textboxS"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_contact" runat="server" 
                    ControlToValidate="txt_contact" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Contact No" SetFocusOnError="True" 
                    >*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_contact" runat="server" TargetControlID="req_contact" ></cc1:ValidatorCalloutExtender>
                 </td>
                 <td  class="style12"><asp:Label ID="lbl_security" text="Security Diposite :" runat="server"></asp:Label> </td>
               <td class="style13"><asp:TextBox ID="txt_security" runat="server" TabIndex="6" CssClass="textboxS"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="dealer_mstr" ID="req_security" runat="server" 
                    ControlToValidate="txt_security" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Security Diposite" SetFocusOnError="True" 
                    >*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="dealer_mstr" ID="regEx_security" ErrorMessage="Enter Valid Data" ValidationExpression="^[0-9]+(\.[0-9]+)?$" ControlToValidate="txt_security" runat="server">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vce_secirity" runat="server" TargetControlID="req_security" ></cc1:ValidatorCalloutExtender>
                    <cc1:ValidatorCalloutExtender ID="vce_secirity1" runat="server" TargetControlID="regEx_security" ></cc1:ValidatorCalloutExtender>
                 </td>
              </tr>
        
        <tr>
            <td  class="style14">
               <%--<asp:Label ID="lbl_active" Text="Active :" runat="server" ></asp:Label>--%>
               <asp:CheckBox ID="chkIsactive" runat="server" Text="Active :" TextAlign="Left" Checked="True" /> 
            </td
            
        </tr>
        <tr>
            <td "width:20%" class="style14">
                &nbsp;</td>
            <td align="right">
            <asp:Button ID="btnSubmit" CssClass="input_btn input_text_x"  runat="server"  Text="Submit" 
                    ValidationGroup="dealer_mstr" />
                &nbsp;</td>
            <td "width:20%" class="style13">
                &nbsp;
                
                &nbsp;&nbsp;
                <asp:Button ID="btnClose" CssClass="input_btn input_text_x"  runat="server"  Text="Close" />
            </td>
        </tr>
            </table>
        
                 
                   <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>--%>
            </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
</asp:Content>

