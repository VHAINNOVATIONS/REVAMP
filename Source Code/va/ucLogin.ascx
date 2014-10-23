<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLogin.ascx.cs" Inherits="ucLogin" %>
<asp:UpdatePanel ID="upLogin" runat="server">
    <ContentTemplate>
        <div runat="server" id="divLogin">
            <asp:Panel BorderStyle="None" DefaultButton="btnLogin" ID="Panel1" runat="server">
                <asp:Label ID="lblUserName" CssClass="lbl-login" Text="username" AssociatedControlID="txtU" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtU" EnableViewState="true" runat="server"></asp:TextBox><br />
                <asp:Label ID="lblPassword" CssClass="lbl-login" Text="password" AssociatedControlID="txtP" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnLogin" CssClass="button-yellow" runat="server" Text="Login" OnClick="btnLogin_Click" UseSubmitBehavior="false" />
            </asp:Panel>

        </div>
        <div runat="server" id="divChangePassword">
            <asp:Label ID="lblUserNameChg" CssClass="lbl-login" Text="Username" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtUN" ReadOnly="false" EnableViewState="true" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblPasswordChg" CssClass="lbl-login" Text="Current Password" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtOldP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="lblPasswordNew" CssClass="lbl-login" Text="New Password" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtNewP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="lblPasswordNewVerify" CssClass="lbl-login" Text="Verify Password" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtVNewP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox><br />
            <br />
            <asp:Button ID="btnNewPasswd" CssClass="button-yellow" runat="server" Text="Change Password" OnClick="btnLogin_Click" UseSubmitBehavior="false" />
        </div>

        <div id="divLoginStatus" runat="server"></div>
        <div id="PopupPostLogin" runat="server"></div>

    </ContentTemplate>
</asp:UpdatePanel>
