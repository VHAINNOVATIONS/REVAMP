<%@ Page Title="REVAMP Portal - Change Password" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="change_password" %>
<%@ MasterType VirtualPath="~/MasterPageLogin.master" %>
<%@ Register TagPrefix="ucLogin" TagName="ucLogin" Src="~/ucLogin.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 15px 30px;">
        <asp:UpdatePanel ID="upChangePWD" runat="server">
            <ContentTemplate>
                <asp:Panel Visible="true" BorderStyle="None" DefaultButton="btnChangePWD" ID="pnlChangePWD"
                    runat="server">
                    <div style="margin-bottom: 15px; text-align: left; color: #2e4d7b; font-size: x-large; text-transform: uppercase;">
                        Change Password
                    </div>
                    <table border="0" width="300" cellspacing="5">
                        <tr>
                            <td>User Name:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblUID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Current Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtOldP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>New Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Verify Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVNewP" EnableViewState="true" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSecQuestions" runat="server" Visible="true">
                        <div style="margin: 15px 0px;">
                            <div style="text-align: left; color: #2e4d7b; font-size: 14px; text-transform: uppercase; margin-bottom: 10px;">
                                Select Challenge Questions and Answers
                            </div>
                            <div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <span style="color: #2e4d7b; font-weight: bold;">Question 1</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Question:</td>
                                            <td>
                                                <asp:DropDownList ID="cboQuestion1" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Answer:</td>
                                            <td>
                                                <asp:TextBox ID="txtAnswer1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span style="color: #2e4d7b; font-weight: bold;">Question 2</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Question:</td>
                                            <td>
                                                <asp:DropDownList ID="cboQuestion2" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Answer:</td>
                                            <td>
                                                <asp:TextBox ID="txtAnswer2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button Text="Change Password" ID="btnChangePWD" runat="server" CssClass="button-yellow" OnClick="btnChangePWD_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

        <script>

            var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
            prm_soap.add_initializeRequest(InitializeRequest);
            prm_soap.add_endRequest(EndRequest);


            function InitializeRequest(sender, args) {
                $('div.ajax-loader').show();
            }

            function EndRequest(sender, args) {
                $('div.ajax-loader').hide();

                $('input[type="button"], input[type="submit"]').css({ padding: '2px 6px' });

            }

    </script>


</asp:Content>