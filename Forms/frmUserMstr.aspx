<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmUserMstr.aspx.vb" Inherits="Forms_frmManageUsers" title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
 <style  type ="text/css">
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

.HellowWorldPopup
{
    min-width:200px;
    min-height:150px;
    background:white;
}
     .style10
     {
     }
     .style13
     {
         width: 130px;
     }
     .style14
     {
         width: 90px;
     }
     .style15
     {
         width: 100px;
     }
 </style> 
<script language="javascript" type="text/javascript">
  function ace1_itemSelected(sender, e)  

    {
      //  alert($get("<%=txtSearch.ClientID %>").value);
    //  var hdCustID = $get('<%= hdEmpID.ClientID %>');
    //  var hdCustID = 3;
    //  alert(hdCustID);
      // hdCustID.value = e.get_value();
        //  var hdCustID = $get("<%=txtSearch.ClientID %>").value; 
        //  alert(hdCustID);
        //__doPostBack("AutoCompleteExtenderDemo", hdCustID);  
       //   alert(var1.value );
       // __doPostBack("btnAddNew", '');
        //  document.All('<%=  Button1.ClientID %>');
        //  alert(btn);
       
         var hdCustID = eval('(' + e.get_value() + ')');       
        var var1 = $get('<%= hdEmpID.ClientID %>');
        var1.value = hdCustID;  
        var btn = $get('<%=Button1.ClientID%>');   
        btn.click();
      
   }

   
   

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="strip"><h1>Manage User</h1></div>
   <div class="clear"></div>
    <asp:Panel runat ="server" ID="p1"  >
    <table>
     <tr>
            <td class="style10">
                
                       <asp:Button ID="btnAddNew" CssClass="button" runat="server" Text="Add New"   />
                
            </td>
            <td align="right" >
            <asp:Label text="Search User Name :" runat="server" ID="lbl_search" Visible="false"></asp:Label></td>
        <td colspan ="2" align ="left"  >
         <asp:TextBox ID="txtSearch" runat="server" 
                TextMode="SingleLine" Width="287px" Visible="False"   ></asp:TextBox>
       <%--  OnTextChanged="txtSearch_TextChanged"--%>
            </td>
        <td><cc1:autocompleteextender ID="AutoCompleteExtenderDemo" runat="server" 
                                       TargetControlID="txtSearch" ServiceMethod="GetCompletionList"     
                                              MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                               CompletionSetCount="10" usecontextkey="true" OnClientItemSelected ="ace1_itemSelected" FirstRowSelected="true">
        </cc1:autocompleteextender>
            
           <asp:HiddenField ID="hdEmpID" runat="server" Visible ="true" Value="0" />  
            
          <asp:Button ID="Button1" runat="server" OnClick ="Button1_Click"   visible="False" 
                BackColor ="White" Height="0px" Width="0px"   />
             &nbsp;</td>
          
          
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="overflow:auto;">
    <table  class="style1">
       <tr>
            <td class="style10" colspan="6">
            
                 <asp:UpdatePanel ID="upGVUser" runat="server" UpdateMode="Conditional">
                 <ContentTemplate>
                  <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /></td></tr> </table> 
             <div>
            <asp:GridView ID="GVuser" runat="server" CssClass="tablestyle"  AllowSorting="true"  
                GridLines="None" DataKeyNames="D_USER_ID"  AllowPaging="true" PageSize="6"
                AutoGenerateColumns="false" Width="100%" >
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
                       
                       <asp:LinkButton Visible="false" ID="lnkdelete" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"  ></asp:LinkButton>
                       
                    <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Bind("D_USER_ID") %>' />
                   
                    <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                        runat="server" TargetControlID="lnkDelete" PopupControlID="DivDeleteConfirmation"
                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                    </cc1:ModalPopupExtender>
                    <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                        TargetControlID="lnkDelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                    </cc1:ConfirmButtonExtender>
                    </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="D_USER_EMP_ID" Visible=false  HeaderText="Emp ID" SortExpression="D_USER_EMP_ID" />
                 <asp:BoundField DataField="D_LOGIN_NAME" Visible=false  HeaderText="Login ID" SortExpression="D_LOGIN_NAME" />
                 <asp:BoundField Visible="false"  DataField="PASSWORD" HeaderText="Password" SortExpression="D_PASSWORD" />
                 <asp:BoundField DataField="D_F_NAME" HeaderText="First Name" SortExpression="D_F_NAME" />
                <asp:BoundField DataField="D_L_NAME" HeaderText="Last Name" SortExpression="D_L_NAME" />
                 <asp:BoundField DataField="D_USER_EMAIL" HeaderText="Email" SortExpression="D_USER_EMAIL" />
                 <asp:BoundField Visible="false"  DataField="D_DESIG_ID" HeaderText="D_DESIG_ID" SortExpression="D_DESIG_ID" />
                  <asp:BoundField DataField="D_DESIG_NAME" HeaderText="Designation" SortExpression="D_DESIG_ID" />
                  <asp:BoundField Visible="false"  DataField="D_ROLE_ID" HeaderText="D_ROLE_ID" SortExpression="D_ROLE_ID" />
                  <asp:BoundField DataField="D_ROLE_NAME" HeaderText="Role" SortExpression="D_ROLE_ID" />
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
     </div>
        </asp:Panel>        
             <%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" CssClass ="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Delete User</div>
                
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the user?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit" />
                <asp:Button id="ButtonDeleteCancel" Text ="Close" runat="server" CssClass="submit" />
            </div>
        </div>
    </asp:Panel>
   
    <asp:Panel ID="pnlPopup" runat="server"   Width="70%" style="display:none;"  >
        <asp:UpdatePanel ID="upPnlUserDetail" runat="server" UpdateMode="Conditional"  >
        <ContentTemplate >
        <asp:Button ID="btnShowPopup" runat="server" style="display:none;" />
            <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <div class="popup_Container" style ="width :100%">
            <div class="popup_Titlebar" id="Div2" style ="width :100%">
                <div class="TitlebarLeft" style ="width :100%">
                    Manage User</div>
                
            </div>
            <div class="popup_Body">
            <table>
            <tr>
            <td class="style15" >
                 Employee ID :</td>
            <td "width:20%" class="style13" >
                <asp:TextBox ID="txtEmpId" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtEmpId" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Enter Employee ID" SetFocusOnError="True" 
                    ValidationGroup="UserMstr">*</asp:RequiredFieldValidator>
            </td>
            <td class="style14" >
                Email :</td>
            <td >
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td >
               <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtEmail" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Enter Email Address" SetFocusOnError="True" 
                    ValidationGroup="UserMstr">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid Email " 
                ControlToValidate="txtEmail" CssClass="ValidationMessage" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="UserMstr" >*</asp:RegularExpressionValidator>
                    </td>
        </tr>
         <tr>
            <td class="style15" >
                First Name :</td>
            <td class="style13" >
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td >
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtFirstName" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Enter First Name" SetFocusOnError="True" 
                    ValidationGroup="UserMstr">*</asp:RequiredFieldValidator></td>
            <td class="style14" >
                Last Name :</td>
            <td >
                <asp:TextBox ID="txtLastName" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td >
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                     ControlToValidate="txtLastName" CssClass="ValidationMessage" Display="Dynamic" 
                     ErrorMessage="Enter Last Name" SetFocusOnError="True" ValidationGroup="UserMstr">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style15" >
                 Login Name :&nbsp; </td>
            <td class="style13" >
                <asp:TextBox ID="txtLoginName" runat="server" CssClass="textboxS"></asp:TextBox>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtLoginName" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Enter Login Name" SetFocusOnError="True" 
                    ValidationGroup="UserMstr">*</asp:RequiredFieldValidator>
            </td>
            <td class="style14" >
                Designation :</td>
            <td >
                <asp:DropDownList ID="ddlDesination" runat="server" CssClass="ddlps" 
                    ValidationGroup="CustomerMstr">
                </asp:DropDownList>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="ddlDesination" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Select Desination" SetFocusOnError="True" 
                    ValidationGroup="UserMstr" InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="style15" >
                Role : </td>
            <td class="style13" >
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="ddlps" 
                    ValidationGroup="CustomerMstr">
                </asp:DropDownList>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="ddlRole" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Select Role" InitialValue="0" SetFocusOnError="True" 
                    ValidationGroup="UserMstr">*</asp:RequiredFieldValidator>
            </td>
            <td class="style14" >
                Active :</td>
            <td >
                <asp:CheckBox ID="chkIsactive" runat="server" Checked="true" />
            </td>
            <td >
                 &nbsp;</td>
        </tr>
       
        <tr>
            <td class="style15" >
                &nbsp;</td> 
              
            <td class="style13" align="right" >
                <asp:Button ID="btnSubmit" CssClass="input_btn input_text_x"  runat="server"  Text="Save" 
                    ValidationGroup="UserMstr" />
            </td>
             <td >
                </td>
            <td class="style14" >
                <asp:Button ID="btnClose" CssClass="input_btn input_text_x"  runat="server"  Text="Close" />
            </td>
            <td >
                <asp:HiddenField ID="hf_userId" runat="server" />
            </td>
            <td >
                &nbsp;</td>
        </tr>
            </table>
            </div> 
              <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
               
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                    TargetControlID="RequiredFieldValidator2">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                    TargetControlID="RequiredFieldValidator4">
                </cc1:ValidatorCalloutExtender>
                                  
             
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" 
                    TargetControlID="RequiredFieldValidator6">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" 
                    TargetControlID="RequiredFieldValidator7">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" 
                    TargetControlID="RequiredFieldValidator10">
                </cc1:ValidatorCalloutExtender>
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                    TargetControlID="RequiredFieldValidator9">
                </cc1:ValidatorCalloutExtender>
                
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" 
                    TargetControlID="RegularExpressionValidator1">
                </cc1:ValidatorCalloutExtender>
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

