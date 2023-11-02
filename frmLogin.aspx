<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLogin.aspx.vb" Inherits="Test" Title="Energy Tracker Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/stylesheet.css" rel="stylesheet" type="text/css" />
      <%-- this is used to avoid go back when any one log out and try to go bak to man page--%>
<%--<script type="text/javascript" language="javascript">
javascript:window.history.forward(0);
</script>--%>
<script type="text/javascript">
        window.history.forward(0)
        </script>
</head>
<body oncontextmenu="return false;">
   
   
    <form id="form2" runat="server">
    <cc1:ToolkitScriptManager  ID="scrip1" runat="server"></cc1:ToolkitScriptManager>
    <div id="wrapper">
	<div id="header">
        <ul>
        <li class="logo"><img src="images/issue_tracker_logo.jpg" alt="Issue Tracker" width="264" height="50" title="Energy Tracker" /></li>
        <li class="globalProser_logo"><img src="images/global_grouplogo_new.jpg" alt="Global Group"  /></li>
        <li class="clear"></li>
        </ul>
    </div>
    <div id="container">
        <div class="login_box">
        	<dl>
            <dd class="login_box_hearder">Energy Tracker Login</dd>
            <dd class="clear"></dd>
            <dd>
             <asp:Label ID="lblError" runat="server" Text="Label" Visible="False" CssClass="login_error"></asp:Label>
             </dd>
            <dd class="login_area">
            	
                <dl>
                   
                	
                    <dd class="login_lable">User Name</dd>
                    <dd class="">
                   <asp:TextBox ID="txtName" runat="server" CssClass="login_input" TabIndex="1"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="req1" runat="server" ErrorMessage="Please Enter User Name" ValidationGroup="login" ControlToValidate="txtName">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                    TargetControlID="req1">
                </cc1:ValidatorCalloutExtender>
                
                   
                   </dd>
                	<dd class="login_lable">Password</dd>
                    <dd class="">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="login_input" 
                            TextMode="Password" TabIndex="2"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Password" ValidationGroup="login" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                  <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
                  
                    </dd>
                    <dd class="fr">
                    <asp:Button ID="btn_login" runat="server" Text="Login" ValidationGroup="login"  CssClass="login_btn" />
                     <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="login_btn" />
                    </dd>
                  
                </dl>
            
            	
            </dd>
            </dl>
            <div class="clear"></div>
      <%--  !----copyright text--------%>
        <div class="disclamair">Copyright © 2010 GPSL Limited. All Rights Reserved. </div>
        </div>
    </div>

<div class="clear"></div>
</div>
    </form>
</body>
    
</html>
