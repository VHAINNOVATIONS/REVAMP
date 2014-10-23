<%@ Page Title="REVAMP Practitioner - System Settings" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="system_settings.aspx.cs" Inherits="system_settings" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="ctSystemSettings" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="sTitle" style="padding-left:26px;float:left" class="PageTitle">System Settings </span>
    <br />
    <% if (!bAllowUpdate)
{ %>
	<div style="display: block; margin: 10px; background-color: #f1f1f1; padding: 4px; text-align:left;">
		<asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png" runat="server" />&nbsp;
		You have <b>Read-Only access</b> to this section.
	</div>
<%} %>
    <div id="dSystemSettings" style="text-align: left; padding: 10px 10px 10px 10px;
        width: 95%; margin: 10px auto; border: 1px solid #8692c4; clear: both;">
        <table cellspacing="5" cellpadding="2">
            <tr>
                <td>
                    <asp:Label ID="lblSiteURL" runat="server" Text="Web Site URL" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWebSiteUrl" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMailHost" runat="server" Text="Mail SMTP Host" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMailSMTPHost" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMailSMTPPort" runat="server" Text="Mail SMTP Port" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMailSMTPPort" Width="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSenderEmailAddress" runat="server" Text="Sender Email Address"
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSenderEmailAddress" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNotifyEmailAddress" runat="server" Text="Notify Email Address"
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNotifyEmailAddress" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTextingHost" runat="server" Text="Text Message Host"
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTextingHost" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTextingPort" runat="server" Text="Text Message Host Port" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTextingPort" Width="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTextingUser" runat="server" Text="Text Message Username"
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTextingUser" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTextingPswd" runat="server" Text="Text Message Password "
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTextingPswd" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOraWinDir" runat="server" Text="Oracle Windows Directory"
                        CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOraWinDir" Width="400" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
