<%@ Page Title="REVAMP Portal - Reset Password" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reset_password.aspx.cs" Inherits="reset_password" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 15px 30px;">
        <asp:UpdatePanel ID="upChangePWD" runat="server">
            <ContentTemplate>
                <asp:Panel Visible="true" BorderStyle="None" DefaultButton="btnSubmit" ID="pnlResetPWD"
                    runat="server">
                    <div style="text-align: left; color: #2e4d7b; font-size: x-large; text-transform: uppercase;">
                        Reset Password
                    </div>
                    <asp:Panel ID="pnlAccntDetails" runat="server">
                        <div style="text-align: left; color: #2e4d7b; font-size: 14px; text-transform: uppercase; margin: 10px 0;">
                            Please enter your user name
                        </div>
                        <table border="0" width="300" cellspacing="5">
                            <tr>
                                <td>User Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%-- 
                    <tr>
                        <td>Email:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                            --%>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlNewPassword" runat="server" Visible="false">
                        <div style="text-align: left; color: #2e4d7b; font-size: 14px; text-transform: uppercase; margin: 10px 0;">
                            Select the new password for your account
                        </div>
                        <table border="0" width="300" cellspacing="5">
                            <tr>
                                <td>New Password:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Confirm Password:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlSecQuestions" runat="server" Visible="false">
                        <div style="text-align: left; color: #2e4d7b; font-size: 14px; text-transform: uppercase; margin: 10px 0;">
                            Please answer your selected security questions
                        </div>

                        <div id="divAccLocked" runat="server" visible="false" style="margin: 5px 25px 15px 25px; border: 1px solid #e1e1e1; padding: 5px; color:red; font-size: 11px;">
                            <!-- message when account is locked -->
                        </div>

                        <div style="margin: 5px 0px; border: 1px solid #e1e1e1; padding: 5px;">
                            <p style="font-weight: bold;">
                                <asp:Label ID="lblQuestion1" runat="server"></asp:Label>
                            </p>
                            <asp:TextBox ID="txtAnswer1" runat="server"></asp:TextBox>
                        </div>
                        <div style="margin: 5px 0px; border: 1px solid #e1e1e1; padding: 5px;">
                            <p style="font-weight: bold;">
                                <asp:Label ID="lblQuestion2" runat="server"></asp:Label>
                            </p>
                            <asp:TextBox ID="txtAnswer2" runat="server"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button-yellow" OnClick="Submit_Click" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

<asp:Content ID="cpScripts" runat="server" ContentPlaceHolderID="cpScripts">
        <script>

            var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
            prm_soap.add_initializeRequest(InitializeRequest);
            prm_soap.add_endRequest(EndRequest);

            adjustHeight();

            function InitializeRequest(sender, args) {
                $('div.ajax-loader').show();
            }

            function EndRequest(sender, args) {
                $('div.ajax-loader').hide();

                adjustHeight();
                $('input[type="button"], input[type="submit"]').css({ padding: '2px 6px' });
            }

    </script>
</asp:Content>