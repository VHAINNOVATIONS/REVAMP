<%@ Page Title="REVAMP Practitioner - Change Password" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="change_password" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="ucLogin" TagName="ucLogin" Src="~/ucLogin.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table>
 <tr valign="top">
 <td width="220">
    <div class="PageTitle" style="text-align:left;">
        Change Password
    </div>
    <div style="text-align:left;">
        <ucLogin:ucLogin ID="ucLogin" runat="server" />
    </div>
 </td>
 <td width="200">
    <asp:Panel ID="pnlMDWS" runat="server">
    
    <asp:UpdatePanel ID="upMDWS" RenderMode="Block" runat="server">
    <ContentTemplate>
    <div class="PageTitle" style="text-align:left;">
     MDWS Account
    </div>
    <table>
    <tr>
    <td align="left">
        <asp:Label ID="Label1" runat="server" Text="Region" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:DropDownList ID="ddlRegion" runat="server" 
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td align="left">
        <asp:Label ID="Label2" runat="server" Text="Site" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:DropDownList ID="ddlSite" runat="server">
        </asp:DropDownList>
       </td>
    </tr>
    <tr>
    <td align="left">
        <asp:Label ID="lblMDWSUserName" runat="server" Text="User Name" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:TextBox ID="txtMDWSUserName" Style="width: 220px;" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left">
        <asp:Label ID="lblMDWSPWD" runat="server" Text="Password" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:TextBox TextMode="Password" ID="txtMDWSPWD" Style="width: 220px;" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left">
        <br />
        <asp:Button ID="btnSaveMDWS" CssClass="button-yellow" 
        runat="server" Text="Save MDWS Account" 
         UseSubmitBehavior="false" />
         <br /><br />
    </td>
    </tr>
    <tr>
    <td align="left">
        <asp:Label ID="lblNoteTitle" runat="server" Text="Default TIU Note Title" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:DropDownList Width="600px" ID="ddlNoteTitle" runat="server"></asp:DropDownList>
        <br /> 
    </td>
    </tr>
    <tr>
        <td align="left">
        <asp:Label ID="lblClinic" runat="server" Text="Default TIU Clinic" CssClass="lbl-login"></asp:Label>&nbsp;
        <br />
        <asp:DropDownList Width="200" ID="ddlClinic" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td align="left">
        <br />
        <asp:Button ID="btnSaveTIU" CssClass="button-yellow" 
            runat="server" Text="Save Defaults" 
            UseSubmitBehavior="false" />
    </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</td>
</tr>
</table>   
<!--TIU SUPPORT-->
<script type="text/javascript" defer="defer">
   var prm_changepwd = Sys.WebForms.PageRequestManager.getInstance();
   prm_changepwd.add_initializeRequest(InitializeRequest);
   prm_changepwd.add_endRequest(EndRequest);
   function InitializeRequest(sender, args)
   {
       $('div.ajax-loader').show();
   }
   function EndRequest(sender, args)
   {
       $('div.ajax-loader').hide();
   }
</script>
</asp:Content>