<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmAssignCustomerToUser.aspx.vb" Inherits="Forms_frmAssignCustomerToUser" title="Energy Tracker" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Assign Customer To User</h1></div>
   <div class="clear"></div>
    <table class="style5">
           <tr>
            <td class="style7">
              <asp:Label ID="lblUser" Text="User Name :" runat="server"></asp:Label>
             </td>
            <td>
                <asp:DropDownList ID="DDLUserName" runat="server" AutoPostBack="True" 
                    CssClass="select_option"   Width="194px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="DDLUserName" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Select Circle" SetFocusOnError="True" 
                    ValidationGroup="AssignCircle" InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <table class="style5">
        <tr>
            <td colspan="3" align="center">
                 <asp:Label ID="lblWarningMessage" runat="server" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" Visible="false" /></td>
           
        </tr>
        <tr>
            <td class="style11">
                Available Customer</td>
            <td class="style12">
                &nbsp;</td>
            <td>
                <b>Assigned Customer</b></td>
        </tr>
        <tr>
            <td class="style10">
                <asp:ListBox ID="lstAvailable" runat="server" Height="152px" 
                    SelectionMode="Multiple" Width="306px"></asp:ListBox>
            </td>
            <td class="style12" align="center">
                <table class="style5">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server"  
                                Text="Add" CssClass="input_btn" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAddAll" runat="server" Text="Add All" CssClass="input_btn"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnRemove" runat="server" 
                                 Text="Remove" CssClass="input_btn" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnRemoveAll" runat="server" Text="Remove All" CssClass="input_btn input_text_x"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:ListBox ID="lstAssign" runat="server" Height="152px" 
                    SelectionMode="Multiple" Width="306px" ></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style10" align="center">
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="input_btn " ValidationGroup="AssignCircle"/>
            </td>
            <td class="style12">
                &nbsp;</td>
            <td >
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="input_btn"  />
            </td>
        </tr>
        <tr>
            <td align="right" class="style10">
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td class="style12">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

