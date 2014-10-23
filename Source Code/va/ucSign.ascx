<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSign.ascx.cs" Inherits="ucSign" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<ext:Window ID="winSignNote" runat="server" IDMode="Static" Hidden="true" Modal="false"
    Collapsible="true" Maximizable="false" Width="350" Height="185" Icon="ApplicationAdd"
    Title="Sign" BodyStyle="background-color: #f1f1f1;">
    <Content>
        <asp:UpdatePanel ID="upSign" runat="server">
            <ContentTemplate>
                <div style="padding: 5px; margin: 10px; background-color: #fff;">
                    <div style="padding-bottom: 10px; margin: 0 auto;">
                        <table>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    User Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProvUsername" runat="server"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    Password:
                                </td>
                                <td>
                                    <asp:TextBox TextMode="Password" ID="txtUPassword" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%if(false){ //now if a provider signs the note it gets closed (if they have Close a Note rights) %> 
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkClosed" runat="server" />&nbsp;<asp:Label ID="lblClosed" runat="server"
                                        Text="">Close Note</asp:Label>
                                </td>
                            </tr>
                            <%} %>
                        </table>
                        <p style="text-align: center;">
                            Enter your username/password and press "Sign" button to sign this document.
                        </p>
                    </div>
                    <div style="text-align: center;">
                        <asp:Button name="btnSignSOAPP" ID="btnSignSOAPP" OnClick="btnSignSOAPP_Click" runat="server"
                            Text="Sign" />
                        &nbsp;
                        <input type="button" class="button" id="btnCancel" value="Cancel" onclick="Ext.onReady(function(){soap.closeSign();});" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </Content>
</ext:Window>
<input type="hidden" id="htxtLogAddendum" name="htxtLogAddendum" runat="server" enableviewstate="true" />

