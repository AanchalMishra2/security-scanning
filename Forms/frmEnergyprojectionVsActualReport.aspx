<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="frmEnergyprojectionVsActualReport.aspx.vb" Inherits="Forms_frmEnergyprojectionVsActualReport" title="Energy Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="strip">
        <h1>
            Energy Projection Vs Actual Projection</h1>
    </div>
    <div class="clear">
    </div>
    <asp:UpdatePanel ID="upp" runat="server">
    <ContentTemplate>
    <asp:Panel ID="pnl_controls" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCustomer" Text="Customer :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCustomer" runat="server" AutoPostBack="True"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCustomer" TabIndex="1" ControlToValidate="ddlCustomer"
                        InitialValue="0" ErrorMessage="Select Customer" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCustomer" TargetControlID="reqCustomer" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblCircle" Text="Circle :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlCircle" runat="server" AutoPostBack="false"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqCircle" ControlToValidate="ddlCircle" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcecircle" TargetControlID="reqCircle" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Month :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlmonth" runat="server" AutoPostBack="false"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqmonth" TabIndex="1" ControlToValidate="ddlmonth"
                        InitialValue="0" ErrorMessage="Select Customer" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcemonth" TargetControlID="reqmonth" runat="server">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="Label2" Text="Year :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList CssClass="select_option" ID="ddlyear" runat="server" AutoPostBack="false"
                        Width="165px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqyear" ControlToValidate="ddlyear" InitialValue="0"
                        ErrorMessage="Select Circle" ValidationGroup="rpt" runat="server">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceyear" TargetControlID="reqyear" runat="server">
                    </cc1:ValidatorCalloutExtender>
                    
                </td>
            </tr>
            
              <tr>
            <td colspan="4" align="center">
            <asp:Button CssClass="input_btn input_text_x" OnClientClick=" Javascript:openWindow('D')" ValidationGroup="rpt"  ID="btnview" runat="server" Text="Detailed"  />
             <asp:Button CssClass="input_btn input_text_x"  OnClientClick=" Javascript:openWindow('S')"  ID="btnreset" runat="server" Text="Summary"  />
             <asp:Button CssClass="input_btn input_text_x" Visible="true"   ID="btnexport" runat="server" Text="Reset"  />
                &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlwarning" runat="server">
    <asp:Label ID="lblwarning" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlRptviewer" runat="server">
    </asp:Panel>
    
    <script type="text/javascript">
    function openWindow(param)
        {
       // debugger;
        
        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
            }

        if (Page_IsValid) {
            // do something
            var year=  document.getElementById('<%=ddlyear.ClientID%>').options[document.getElementById('<%=ddlyear.ClientID%>').selectedIndex].text;
            if (param=="D") {window.open("frmEnergyprojectionVsActualRPT.aspx?param=" + document.getElementById('<%=ddlCustomer.ClientID%>').value + "&c=" +document.getElementById('<%=ddlCircle.ClientID%>').value +"&m="+document.getElementById('<%=ddlCustomer.ClientID%>').value +"&y="+year+"&Mode=D")}
            if (param=="S") {window.open("frmEnergyprojectionVsActualRPT.aspx?param=" + document.getElementById('<%=ddlCustomer.ClientID%>').value + "&c=" +document.getElementById('<%=ddlCircle.ClientID%>').value +"&m="+document.getElementById('<%=ddlCustomer.ClientID%>').value +"&y="+year+"&Mode=S")}
            //alert('Page is valid!');                
                }
        else {
            // do something else
            //alert('Page is not valid!');
        }
         }

      //  alert( document.getElementById('<%=ddlCustomer.ClientID%>').value);
    
    </script>
</asp:Content>

