﻿<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmCircleMstr.aspx.vb" Inherits="Forms_frmCircleMstr" title="Energy Tracker"  %>
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
        width: 140px;
    }
        .style11
        {
            width: 417px;
        }
        .style12
        {
            width: 134px;
        }
        .style13
        {
            width: 23px;
        }
        </style> 
       
  
</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="strip"><h1>Manage Circle</h1></div>
   <div class="clear"></div>
    <table style="width:100%" class="style1">
              
        <tr>
            <td  class="style9">
              <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="button" />
                         </td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td  class="style5" colspan="3">
            <asp:UpdatePanel ID="UpGVCircle" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> 
            
             
            
                <asp:GridView ID="GVCircle" runat="server" CssClass="tablestyle" 
                 AllowPaging="true" AllowSorting="true"  Width="100%" PageSize="6"
                GridLines="None" DataKeyNames="D_CIRCLE_ID"  
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
               
              <asp:TemplateField  HeaderText="Edit">
                   <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandName="Select" CausesValidation="false" ></asp:LinkButton>
                           
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
                 <asp:BoundField DataField="D_CIRCLE_CODE" HeaderText="Circle Code" SortExpression="D_CIRCLE_CODE"  />
                 
                 <asp:BoundField Visible="false"  DataField="D_CIRCLE_ID" HeaderText="Circle Id" SortExpression="D_CIRCLE_ID" />
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
            <td  class="style9">
                &nbsp;</td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td  class="style9">
                &nbsp;</td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td  class="style9">
                &nbsp;</td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                <asp:HiddenField ID="hdf_CircleId" runat="server" Visible="False" />
             </td>
        </tr>
        <tr>
            <td  class="style9">
                &nbsp;</td>
            <td  class="style6">
                &nbsp;</td>
            <td  class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td  class="style9">
                &nbsp;</td>
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
                    Manage Circle</div>
                
            </div>
            <div class="popup_Body">
            <table>
            
             <tr>
                 <td  class="style12">
                     Circle Name :</td>
            <td  class="style11">
                <asp:TextBox ID="txtCircleName" runat="server" CssClass="textboxS"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCircleName" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Circle Name" SetFocusOnError="True" 
                    ValidationGroup="CircleMstr">*</asp:RequiredFieldValidator>
            </td>
            <td class="style13" >
                &nbsp;</td>
        </tr>
          <tr>
                 <td  class="style12">
                     Circle Code :</td>
            <td  class="style11">
                <asp:TextBox ID="txtCircleCode" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td class="style13" >
                &nbsp;</td>
        </tr>
        
       
        
        <tr>
            <td  class="style12">
                Active :
            </td>
            <td  class="style11">
                <asp:CheckBox ID="chkIsactive" runat="server" Checked="true" />
            </td>
            <td class="style13" >
                &nbsp;</td>
        </tr>
        <tr>
            <td  class="style12">
                &nbsp;</td>
            <td  class="style11">
                &nbsp;
                <asp:Button ID="btnSubmit" CssClass="input_btn input_text_x"  runat="server"  Text="Submit" 
                    ValidationGroup="CircleMstr" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose" CssClass="input_btn input_text_x"  runat="server" Text="Close" />
            </td>
            <td class="style13" >
                &nbsp;</td>
        </tr>
            </table>
        
                 
                   <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
            </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
</asp:Content>

