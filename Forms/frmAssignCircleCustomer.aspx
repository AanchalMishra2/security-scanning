<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmAssignCircleCustomer.aspx.vb" Inherits="Forms_frmAssignCircleCustomer" title="Energy Tracker" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="strip"><h1>Assign Circle To Customer</h1></div>
   <div class="clear"></div>
    <table class="style5">
     <tr>
            <td class="style7">
              <asp:Label ID="lblcustomer" Text="Customer :" runat="server"></asp:Label>
             </td>
            <td>
                <asp:DropDownList ID="ddlcustomer" runat="server" AutoPostBack="True" 
                    CssClass="select_option"   Width="194px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="ddlcustomer" Display="Dynamic" 
                    ErrorMessage="Select Customer" SetFocusOnError="True" 
                    ValidationGroup="AssignCircletocustomer" InitialValue="0">*</asp:RequiredFieldValidator>
                     <cc1:ValidatorCalloutExtender ID="vceCircletocustomer" TargetControlID="RequiredFieldValidator2" runat="server"></cc1:ValidatorCalloutExtender>
                    </td>
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
                Available Circles</td>
            <td class="style12">
                &nbsp;</td>
            <td>
                <b>Assigned Circles</b></td>
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
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="input_btn " ValidationGroup="AssignCircletocustomer"/>
            </td>
            <td class="style12">
                &nbsp;</td>
            <td >
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="input_btn"  />
            </td>
        </tr>
        <tr>
            <td align="right" class="style10">
               
            </td>
            <td class="style12">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

