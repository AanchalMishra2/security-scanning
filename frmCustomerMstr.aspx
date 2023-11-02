<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frmCustomerMstr.aspx.vb" Inherits="frmCustomerMstr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
    .style1
    {
        width: 100%;
        border-style: solid;
        border-width: 1px;
    }
     .ModalPopupBG
{
    background-color: #666699;
    filter: alpha(opacity=50);
    opacity: 0.7;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <h2>Manage Customer</h2> <table class="style1">
    
    <tr>
        <td >
            <asp:Button ID="BtnAddNew" runat="server" Text="Add New" CssClass="submit" /></td>
        <td >
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="4">
   </td>
    </tr>
    <tr>
        <td  colspan="4">
        <asp:UpdatePanel ID="UpGVCustomer" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> 
          
            <asp:GridView ID="GVCustomer" runat="server" CssClass="tablestyle"  
                 AllowSorting="True" AllowPaging="True"
                GridLines="None" DataKeyNames="D_Customer_ID"  AutoGenerateColumns="False" 
                Width="100%">
                <RowStyle CssClass="rowstyle" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Size="Larger"/>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle CssClass="headerstyle" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle CssClass="altrowstyle"  />
                <Columns>
                <asp:TemplateField HeaderText="SrNo">
                     <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
               </asp:TemplateField>
              <asp:TemplateField >
                   <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                            /
                       <asp:LinkButton ID="lnkdelete" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false" ></asp:LinkButton>
                    <asp:HiddenField ID="hfCustomerID" runat="server" Value='<%# Bind("D_CUSTOMER_ID") %>' />
                     
                       <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                        runat="server" TargetControlID="lnkdelete" PopupControlID="DivDeleteConfirmation"
                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                       </cc1:ModalPopupExtender>
                       <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                        TargetControlID="lnkdelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                       </cc1:ConfirmButtonExtender>
                     
                   </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="D_CUSTOMER_NAME" HeaderText="Customer Name" 
                        SortExpression="D_CUSTOMER_NAME" />
                  <asp:BoundField DataField="D_CREATED_ON" HeaderText="Created On" SortExpression="D_CREATED_ON" />
                  <asp:BoundField DataField="D_ACTIVE" HeaderText="Active" 
                        SortExpression="D_ACTIVE" />
                    
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
        <td >
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
    <asp:Panel ID="pnlPopup" runat="server"   Width="500px" style="display:none;"   >
        <asp:UpdatePanel ID="upPnlCustomer" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
         <asp:Button ID="btnShowPopup" runat="server" style="display:none; " />
           <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
           </cc1:ModalPopupExtender>
            <div class="popup_Container">
              <div class="popup_Titlebar" id="Div2">
                <div class="TitlebarLeft">
                    Manage Customer</div>
                
            </div>
            <div class="popup_Body">
            <table>
               
            <tr>
            <td >
                Customer Name :</td>
            <td >
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textboxS"></asp:TextBox>
             </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCustomerName" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Customer Name" 
                    ValidationGroup="CustomerMstr" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
       
            </tr>
           
            
        <tr>
            <td >
                Is active :</td>
            <td >
                <asp:CheckBox ID="chkIsactive" runat="server" />
            </td>
            <td >
                &nbsp;</td>
            </tr>
        <tr>
            <td >
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" 
                    ValidationGroup="CustomerMstr" CssClass="submit" />
            </td>
        <td >
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="submit" />
        </td>
        <td >
            <asp:HiddenField ID="hdf_Customerid" runat="server"  /></td>
        
    </tr>
                        
           </table> 
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator3"/>  
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator1"/>  
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2"/>
                
            </div> 
            </div> 
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
<%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" CssClass ="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Delete Customer</div>
                
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the Customer?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok"  CssClass="submit" />
                <asp:Button id="ButtonDeleteCancel" Text ="Cancel" runat="server"  CssClass="submit"/>
            </div>
        </div>
    </asp:Panel>
   
</asp:Content>



