<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmChangePassword.aspx.vb" Inherits="Forms_frmChangePassword" title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Change Password</h1></asp:Label></div>
    <asp:Panel id="pnlChangePwd" runat="server">
 <table style="width: 548px" >
 <tr>
 <td><asp:Label ID="lbl_oldpwd" runat="server" Text="Old Password"></asp:Label> </td>
 <td><asp:TextBox ID="txt_oldpwd" runat="server" TextMode="Password"></asp:TextBox></td>
 <td><asp:RequiredFieldValidator ID="req_oldpwd" ValidationGroup="validate" runat="server" ErrorMessage="Enter Old Password" ControlToValidate="txt_oldpwd">*</asp:RequiredFieldValidator>
     <asp:ValidatorCalloutExtender ID="vce_oldpwd" TargetControlID="req_oldpwd" runat="server">
     </asp:ValidatorCalloutExtender>
 </td>
 </tr>
     
 <tr>
 <td><asp:Label ID="lbl_newpwd" runat="server" Text="New Password"></asp:Label> </td>
 <td><asp:TextBox ID="text_newpwd" runat="server" TextMode="Password"></asp:TextBox></td>
 <td>
 <asp:RequiredFieldValidator ID="req_newpwd" ValidationGroup="validate" runat="server" ErrorMessage="Enter New Password" ControlToValidate="text_newpwd">*</asp:RequiredFieldValidator>
     <asp:ValidatorCalloutExtender ID="vce_newpwd" TargetControlID="req_newpwd" runat="server">
     </asp:ValidatorCalloutExtender>
     <asp:PasswordStrength ID="psas_newpwd" TargetControlID="text_newpwd"  PreferredPasswordLength="6" TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
 PrefixText="Strength:"  runat="server">
     </asp:PasswordStrength>
 </td>
 </tr>
     
 <tr>
 <td><asp:Label ID="lbl_confirmpwd" runat="server" Text="Confirm Password"></asp:Label> </td>
 <td><asp:TextBox ID="txt_confirmpwd" runat="server" TextMode="Password"></asp:TextBox></td>
 <td>
 <asp:RequiredFieldValidator ID="req_conpwd" ValidationGroup="validate" runat="server" ErrorMessage="Re Enter Password" ControlToValidate="txt_confirmpwd">*</asp:RequiredFieldValidator>
     <asp:ValidatorCalloutExtender ID="vce_confirmpwd1" TargetControlID="req_conpwd" runat="server">
     </asp:ValidatorCalloutExtender>
 <asp:CompareValidator ID="comv_confirm" ValidationGroup="validate" ControlToCompare="text_newpwd"  ControlToValidate="txt_confirmpwd" ErrorMessage="Eneter Correct Password" runat="server"></asp:CompareValidator>
  <asp:ValidatorCalloutExtender ID="vce_confirmpwd" TargetControlID="comv_confirm" runat="server">
     </asp:ValidatorCalloutExtender>
 </td>
 </tr>
 <tr>
 <td colspan="2" align="center"><asp:Button ID="btn_update" ValidationGroup="validate" Text="Update" CssClass="input_btn input_text_x"  runat="server" /></td>
 </tr>
</table>
</asp:Panel>
</asp:Content>

