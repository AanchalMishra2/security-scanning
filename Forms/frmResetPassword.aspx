<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmResetPassword.aspx.vb" Inherits="Forms_frmResetPassword" title="Energy Tracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Reset Password</h1></div>
<asp:panel id="pnl_resetpwd" runat="server">
<table style="width: 297px">
<tr>
<td><asp:Label ID="lbl_userName" Text="User Name :" runat="server"></asp:Label></td>
<td><asp:DropDownList CssClass="select_option" ID="ddl_user" runat="server"></asp:DropDownList></td>
<td> <asp:RequiredFieldValidator ID="req_user" runat="server" ValidationGroup="validate" ControlToValidate="ddl_user" InitialValue="0" ErrorMessage="Select User">*</asp:RequiredFieldValidator>
      <asp:ValidatorCalloutExtender ID="vce_user" TargetControlID="req_user" runat="server">
     </asp:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td colspan="2" align="center"><asp:Button ID="btn_reset" ValidationGroup="validate"  Text="Reset" 
        CssClass="input_btn input_text_x"  runat="server" Width="142px" /></td>
</tr>
</table>

</asp:panel>
</asp:Content>

