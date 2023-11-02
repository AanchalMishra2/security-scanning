<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmAssignRole.aspx.vb" Inherits="Forms_frmAssignRole" title="Energy Tracker"  %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
    <style type="text/css">
        .style5
        {
            width: 100%;
        }
        .style7
        {
            width: 98px;
            font-weight: bold;
        }
        .style9
        {
            width: 5px;
            font-weight: bold;
        }
        .style10
        {
            width: 303px;
        }
        .style14
        {
            width: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="strip"><h1>Assign Role</h1></div>
   <div class="clear"></div>
 
    <table class="style5">
        <tr>
            <td class="style7">
                Role Name</td>
            <td class="style9">
                :</td>
            <td>
                <asp:DropDownList ID="DDLRoleName" runat="server" AutoPostBack="True" 
                    CssClass="select_option" Width="194px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="DDLRoleName" CssClass="ValidationMessage" Display="Dynamic" 
                    ErrorMessage="Select Role" SetFocusOnError="True" 
                    ValidationGroup="AssignRole" InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <table class="style5">
        <tr>
            <td colspan="3" align="center">
                 <asp:Label ID="lblWarningMessage" runat="server" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" Visible="false" /></td>
           
        </tr>
        <tr>
            <td >
                Available Rights</td>
            <td class="style14"  >
                &nbsp;</td>
            <td>
                <b>Assign Rights</b></td>
        </tr>
        <tr>
            <td >
                <asp:ListBox ID="lstAvailable" runat="server" Height="152px" 
                    SelectionMode="Multiple" Width="306px"></asp:ListBox>
            </td>
            <td class="style14">
                <table class="style5">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server"  
                                Text="Add" CssClass="input_btn"/>
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
            <td align="right" class="style10">
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="input_btn" ValidationGroup="AssignRole"/>
            </td>
            <td class="style14">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="input_btn" />
            </td>
        </tr>
        <tr>
            <td align="right" class="style10">
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td class="style14">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

