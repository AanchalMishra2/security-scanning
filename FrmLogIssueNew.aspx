<%@ Page Language="VB" MasterPageFile="~/TestMaster.master" AutoEventWireup="false" CodeFile="FrmLogIssueNew.aspx.vb" Inherits="Forms_FrmLogIssueNew" title="Issue Tarcker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"   %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../css/stylesheet.css" rel="stylesheet" type="text/css" />
  <link rel="stylesheet" type="text/css" href="../style.css" />
	<link rel="stylesheet" type="text/css" href="../subModal.css" />
	<script type="text/javascript" src="../common.js"></script>
	<script type="text/javascript" src="../subModal.js"></script>
    <style type="text/css">
        .style13
        {
            text-align: left;
            width: 80%;
        }
        </style>

    <script language="vbscript">
			Sub exportbutton_onclick 
			Dim sHTML, oXL, oBook
			sHTML = document.all.item("reportContent").outerhtml
			Set oXL = CreateObject("Excel.Application")
			Set oBook = oXL.Workbooks.Add
			oBook.HTMLProject.HTMLProjectItems("Sheet1").Text = sHTML
			oBook.HTMLProject.RefreshDocument
			oXL.Visible = true
			oXL.UserControl = true
			End Sub
    </script>
    
  


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="strip"><h1>Open Issue List</h1></div>
   <div class="clear"></div>
    
    <table style="width:100%" class="style1">
               
        <tr>
            <td "width:20%" class="style9">
              <asp:Button ID="btnAddNew"  runat="server" Text="Add New" CssClass="button" />
            
                         </td>
            <td "width:20%" class="style6">
                &nbsp;</td>
            <td "width:20%" class="style7">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td "width:20%" class="style5" colspan="3">
            <asp:UpdatePanel ID="UpGVCust" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table width="100%"> <tr><td align="center"><asp:Label ID="lblWarningMessage" runat="server" Visible="true" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana" /> </td></tr> </table> 
            
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="100%">
                                <RowStyle BackColor="#F7F7DE" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   
                             <asp:TemplateField HeaderText="Ticket No ">
                            <ItemTemplate>

                            <asp:HyperLink ID="h1" NavigateUrl='<%#"frmIssueOpenList.aspx?id=" & Eval("D_Ticket_No") %>'
                            Text='<%# Eval("D_Ticket_No") %>' runat="server"></asp:HyperLink>

                            
                            </ItemTemplate>
                            
                </asp:TemplateField>
                 <asp:BoundField DataField="D_ISSUE_LOGGED_DATE" HeaderText="Issue Logged Date" 
                                        SortExpression="D_ISSUE_LOGGED_DATE" />
                <asp:BoundField DataField="D_CAPTION" HeaderText="Caption" 
                                        SortExpression="D_CAPTION" />
                                        
                                         <asp:BoundField DataField="D_ISSUE_STATUS" HeaderText="Issue Status" 
                                        SortExpression="D_ISSUE_STATUS" />
                    <asp:BoundField DataField="D_EXPECTED_RESOLVED_DATE" HeaderText="EXPECTED RESOLVED DATE" 
                                        SortExpression="D_EXPECTED_RESOLVED_DATE" /> 
                                  
                    <asp:BoundField DataField="ACTION_TAKEN" HeaderText="ACTION TAKEN " 
                                        SortExpression="ACTION_TAKEN" />               
                                        
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lbl_RecordNotFound" runat="server" Font-Size="Larger" 
                                                    ForeColor="maroon" Text="No Record Found"></asp:Label>
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
<%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display:none;" CssClass ="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Delete customer</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure, you want to delete the customer?
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button  id="ButtonDeleleOkay" runat="server" Text="Ok" CssClass="submit" />
                <asp:Button id="ButtonDeleteCancel" Text ="Cancel" runat="server" CssClass="submit" />
            </div>
        </div> 
    </asp:Panel>
 <asp:Panel ID="pnlPopup" runat="server" Width="700px"   style="display:none;"     >
     <asp:UpdatePanel ID="upPnlcustomerDetail" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none;" CssClass="submit" />
        <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" 
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
            <div class="popup_Container">
            <div class="popup_Titlebar" id="Div2">
                <div class="TitlebarLeft">
                    Manage Customer</div>
                
            </div>
            <div class="popup_Body">
            <table>
            
             <tr>
                 <td "width:20%" class="style12">
                     Customer Name </td>
            <td "width:20%">
                :</td>
            <td "width:20%" class="style13">
                <asp:TextBox ID="txtCustomerName"  runat="server" CssClass="textboxS"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCustomerName" CssClass="ValidationMessage" 
                    Display="Dynamic" ErrorMessage="Customer Name" SetFocusOnError="True" 
                    ValidationGroup="CustomerMstr">*</asp:RequiredFieldValidator>
                 </td>
        </tr>
       
        
        <tr>
            <td "width:20%" class="style12">
                Active
            </td>
            <td "width:20%">
                :</td>
            <td "width:20%" class="style13">
                <asp:CheckBox ID="chkIsactive" runat="server" Checked="True" />
            </td>
        </tr>
        <tr>
            <td "width:20%" class="style12">
                &nbsp;</td>
            <td "width:20%">
                &nbsp;</td>
            <td "width:20%" class="style13">
                &nbsp;
                <asp:Button ID="btnSubmit" runat="server" CssClass="submit" Text="Submit" 
                    ValidationGroup="CustomerMstr" />
                &nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" CssClass="submit" Text="Close" />
            </td>
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
            
      <asp:Panel ID="pnl_popup" runat="server" Width="900px" style="display:none ;" >
       <asp:UpdatePanel ID="upPnlcustomerDetail1" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
       <asp:Button ID="btnShowPopup1" runat="server" style="display:none;" CssClass="submit" />
       
       <cc1:ModalPopupExtender ID="mdlPopup1" runat="server" TargetControlID="btnShowPopup1" 
            PopupControlID="pnl_popup" CancelControlID="Button1" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div3">
                <div class="TitlebarLeft">
                    Log Issue</div>
                
            </div>
            <div class="popup_Body">
            <table id="Table1" width="100%"  runat="server" >
 
 <tr>
 <td colspan="6" align="center" >
 <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="#C00000" 
                Font-Names="Verdana"></asp:Label>
 </td>
 </tr>
 <tr>
            <td align="left" width="15%">
            <asp:Label ID="Label7" runat="server" Text="Caption"></asp:Label>
            </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="23%">
            <asp:TextBox ID="txt_caption" runat="server" Width="160px" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="transissue" ControlToValidate="txt_caption"  ErrorMessage="Please Enetr Caption">*</asp:RequiredFieldValidator>
            </td> 
            <td align="left" width="17%">
            <asp:Label ID="Label8" runat="server" Text="Circle Name"></asp:Label>
            </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="45%">
             <asp:TextBox ID="txt_circle" ReadOnly="true" runat="server" Width="160px" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="req_circle" runat="server" ValidationGroup="transissue" ControlToValidate="txt_circle"  ErrorMessage="Please Select Circle">*</asp:RequiredFieldValidator>
         
            </td> 
 
 </tr>
  <tr>
            <td align="left" width="15%">
            <asp:Label ID="Label2" runat="server" Text="Customer Name"></asp:Label>
            </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="23%">
            <asp:TextBox ID="txt_customer" ReadOnly="true" runat="server" Width="160px"></asp:TextBox>
              </td> 
            <td align="left" width="17%">
            <asp:Label ID="Label3" runat="server" Text="Department Name"></asp:Label>
            </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="45%">
            <asp:TextBox ID="txt_department" ReadOnly="true" runat="server" Width="160px"  ></asp:TextBox>
            </td> 
 
 </tr>
 
  <tr>
            <td align="left" width="15%">
            <asp:Label ID="Label4" runat="server" Text="Issue Category"></asp:Label>
            </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="23%">
               <asp:DropDownList ID="ddn_category" runat="server" Width="170Px" AutoPostBack="True"></asp:DropDownList>
           <asp:RequiredFieldValidator ID="reqcategory" runat="server" InitialValue="0" ControlToValidate="ddn_category" ErrorMessage="Please selectIssue Category" ValidationGroup="transissue">*</asp:RequiredFieldValidator>
          </td> 
            <td align="left" width="17%">
            <asp:Label ID="Label5" runat="server" Text="Issue Sub Category Name"></asp:Label>
                                </td> 
            <td width="1%" align="center">:
            </td> 
            <td align="left" width="45%">
            <asp:DropDownList ID="ddn_subcategoryname" runat="server" Width="170px">
            <asp:ListItem Text="Select"></asp:ListItem>
            </asp:DropDownList>
             </td> 
 
 </tr>
 

  <tr>
            <td align="left" width="15%">
                <asp:Label ID="Label6" runat="server" Text="Issue Details"></asp:Label>
            </td> 
            <td width="1%" align="center">&nbsp;:</td> 
            <td align="left" width="23%" colspan="3">
           
           <asp:TextBox ID="txt_issuedetails" Width="500px"  runat="server" 
                    TextMode="MultiLine" Font-Names="Verdana" Height="60px" ></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="reqissuedetails" ValidationGroup="transissue" runat="server" ControlToValidate="txt_issuedetails" ErrorMessage="Please Mention Issue Deails">*</asp:RequiredFieldValidator>
            
             
             </td> 

 
 </tr>

 <tr>
            <td colspan="6" align="center"><asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="BtnSubmit" ValidationGroup="transissue"/>
            &nbsp; <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="BtnSubmit" />
            &nbsp; <asp:Button ID="Button1" runat="server" CssClass="submit" Text="Close" />
           
                <asp:HiddenField ID="hdn_sno" runat="server" Visible="false" />
            </td>

            </tr>
 
  <tr>
            <td align="right" width="15%">
                &nbsp;</td> 
            <td width="1%" align="center">&nbsp;</td> 
            <td align="left" width="23%">
            
           </td> 
            <td align="right" width="10%">
            
            </td> 
            <td width="1%" align="center">
            </td> 
            <td align="left" width="38%">
          
            </td> 
 
 </tr>
 </table>
            </div>
         </div>
        </ContentTemplate>
       </asp:UpdatePanel>
      
      </asp:Panel>  

            
            
</asp:Content>
