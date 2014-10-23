<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatientPortalAccount.ascx.cs"
    Inherits="ucPatientPortalAccount" %>
<asp:UpdatePanel ID="upPortalAccount" runat="server">
    <ContentTemplate>
        <div id="divStatus" runat="server" style="text-align: left; margin-bottom: 15px; padding:5px; font-weight:bold;"></div>
        <div>
            <table border="0" cellpadding="3" cellspacing="4">
                <tr>
                    <td class="cell_label">
                        <asp:Label runat="server" ID="lblAccountStatus" Text="Account Status" CssClass="label"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkbxAccountLocked" Text="&nbsp;Locked" runat="server" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkbxAccountInactive" Text="&nbsp;Inactive" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="cell_label">
                        <asp:Label ID="lblUserId" runat="server" Text="User Name" CssClass="label"></asp:Label>
                    </td>
                    <td align="left">
                        <div id="divUsername" runat="server">
                            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divResetPWDButton" style="text-align: center;" runat="server">
                            <asp:CheckBox ID="chkResetPassword" runat="server" AutoPostBack="true" OnCheckedChanged="chkResetPassword_click" />
                            <asp:Label ID="lblChkResetPassword" runat="server" AssociatedControlID="chkResetPassword"
                                Text="Reset Account's Password"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPassword" runat="server" enableviewstate="true">
            <table border="0" cellpadding="3" cellspacing="4">
                <tr>
                    <td class="cell_label">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="label"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" TextMode="Password" Width="150px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cell_label">
                        <asp:Label ID="lblVerfifyPassword" runat="server" Text="Verify Password" CssClass="label"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtVerifyPassword" TextMode="Password" Width="150px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>