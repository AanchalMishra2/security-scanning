<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmManageRole.aspx.vb" Inherits="Forms_frmManageRole" title="Energy Tracker" %>

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
 <div class="strip"><h1>Manage Role</h1></div>
   <div class="clear"></div>
    <tr>
        <td >
            <asp:Button ID="BtnAddNew" runat="server" Text="Add New" CssClass="button" /></td>
        <td >
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td>
              </td>
    </tr>
    
    <tr>
        <td  colspan="4">
            <asp:UpdatePanel ID="UpGVRole" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /></td></tr> </table> 
         <asp:GridView ID="GVRole" runat="server" CssClass="tablestyle" 
                 AllowSorting="true"  AllowPaging="true" PageSize="10"
                GridLines="None" DataKeyNames="D_ROLE_ID"  AutoGenerateColumns="false" 
                Width="100%">
                <RowStyle CssClass="rowstyle" />
                <FooterStyle CssClass="PagerStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle CssClass="headerstyle" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle CssClass="altrowstyle" />
                <Columns>
                <asp:TemplateField HeaderText="S.No">
                     <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
               </asp:TemplateField>
              <asp:TemplateField >
                   <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                            /
                       <asp:LinkButton ID="lnkdelete" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false" ></asp:LinkButton>
                    <asp:HiddenField ID="hfRoleID" runat="server" Value='<%# Bind("D_ROLE_ID") %>' />
                     
                       <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                        runat="server" TargetControlID="lnkdelete" PopupControlID="DivDeleteConfirmation"
                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                       </cc1:ModalPopupExtender>
                       <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                        TargetControlID="lnkdelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                       </cc1:ConfirmButtonExtender>
                                           
                   </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="d_ROLE_NAME" HeaderText="Role Name" SortExpression="D_ROLE_NAME" />
                 <asp:BoundField DataField="D_ROLE_DESC" HeaderText="Role Description" SortExpression="D_ROLE_DESC" />
                  <asp:BoundField DataField="D_CREATED_ON" HeaderText="Created On" SortExpression="D_CREATED_ON" />
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

    <asp:Panel ID="pnlPopup" runat="server"   Width="50%" style="display:none ;"   >
        <asp:UpdatePanel ID="upPnlRole" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <asp:Button ID="btnShowPopup" runat="server" style="display:none;" CssClass="submit" />
            <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender> 
            <div class="popup_Container"  style ="width :100%" >
              <div class="popup_Titlebar" id="Div2" style ="width :100%">
                <div class="TitlebarLeft"  style ="width :100%">
                    Manage Role</div>
               
            </div>
            <div class="popup_Body">
            <table>
               
            <tr>
            <td >
                Role Name :</td>
            <td >
                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="textboxS"></asp:TextBox>
             </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtRoleName" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Role Name" 
                    ValidationGroup="RoleMstr" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
       
            </tr>
            <tr>
            <td >
                Role Description :</td>
            <td>
                <asp:TextBox ID="txtRoleDes" runat="server" CssClass="textboxS"></asp:TextBox>
             </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtRoleDes" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Role Description" 
                    ValidationGroup="RoleMstr" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
            </tr>
        <tr>
            <td >
                Is active :</td>
            <td >
                <asp:CheckBox ID="chkIsactive" runat="server" />
            </td>
            <td >
                 <asp:HiddenField ID="hdf_RoleId" runat="server"  /></td>
            </tr>
        <tr>
            <td >
                
            </td>
        <td ><asp:Button ID="BtnSubmit" runat="server" Text="Save" 
                    ValidationGroup="RoleMstr"  CssClass="input_btn input_text_x" />
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="input_btn input_text_x"  />
        </td>
        <td >
         </td>
        
    </tr>
                        
           </table> 
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                </cc1:ValidatorCalloutExtender>
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
                    Delete Role</div>
                
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the Role?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit"  />
                <asp:Button id="ButtonDeleteCancel" Text ="Cancel" runat="server" CssClass="submit" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>

