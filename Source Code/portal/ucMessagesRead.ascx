<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMessagesRead.ascx.cs" Inherits="ucMessagesRead" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Import Namespace="System.Data" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div style="margin-bottom: 8px;">
    <input type="button" value="Compose" onclick="messages.composeMessage();" />
</div>

<asp:UpdatePanel ID="upWrapper" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:UpdatePanel ID="upMesaggesView" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div id="divMessagesWrapper" style="width: 95%">
                    <div style="float: left; width: 48%; margin-right: 1%;">
                        <asp:UpdatePanel ID="upSelectMsg" runat="server" OnLoad="ReadMessage">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteMsg" />
                                <asp:AsyncPostBackTrigger ControlID="btnRToAll" />
                                <asp:AsyncPostBackTrigger ControlID="btnReply" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:TabContainer ID="tcMessages" runat="server" Width="98%">

                                    <asp:TabPanel ID="tpInbox" runat="server" HeaderText="Inbox">
                                        <ContentTemplate>
                                            <div style="padding: 4px; width: auto;">
                                                <table id="tblMsgList" border="0" class="mGrid" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 30px;">&nbsp;</th>
                                                            <th>From</th>
                                                            <th style="width: 180px;">Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repMsgList" runat="server" OnItemDataBound="repMsgList_OnItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="trMsg" runat="server" style="cursor: pointer;"
                                                                    onclick='messages.readMsg(this);'>
                                                                    <td style="vertical-align: middle; text-align: center;">
                                                                        <asp:Image ID="imgMsgStatus" runat="server" Visible="false" />
                                                                    </td>
                                                                    <td style="padding: 3px 3px 3px 10px; vertical-align: middle;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "SENDER_NAME") %><br />
                                                                        <span style="font-size: 10px; color: #808080;"><%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, "SUBJECT").ToString()) %></span>
                                                                    </td>
                                                                    <td style="padding: 3px; vertical-align: middle;">
                                                                        <span style="font-size: 10px;"><%# DataBinder.Eval(Container.DataItem, "DATE_SENT") %></span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpSent" runat="server" HeaderText="Sent">
                                        <ContentTemplate>
                                            <div style="padding: 4px; width: auto;">
                                                <table id="tblSentMsg" border="0" class="mGrid" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                            <th>To</th>
                                                            <th style="width: 180px;">Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repSentMsg" runat="server">
                                                            <ItemTemplate>
                                                                <tr id="trMsg" runat="server" style="cursor: pointer;" messageid='<%#DataBinder.Eval(Container.DataItem, "MESSAGE_ID") %>'
                                                                    onclick='messages.readMsg(this);'>
                                                                    <td style="padding: 3px 3px 3px 10px; vertical-align: middle;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "RECIPIENTS") %><br />
                                                                        <span style="font-size: 10px; color: #808080;"><%#DataBinder.Eval(Container.DataItem, "SUBJECT") %></span>
                                                                    </td>
                                                                    <td style="padding: 3px; vertical-align: middle;">
                                                                        <span style="font-size: 10px;"><%# DataBinder.Eval(Container.DataItem, "DATE_SENT") %></span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpDeleted" runat="server" HeaderText="Deleted">
                                        <ContentTemplate>
                                            <div style="padding: 4px; width: auto;">
                                                <table id="tblDeletedMsg" border="0" class="mGrid" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                            <th>From</th>
                                                            <th style="width: 180px;">Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repDeletedMsg" runat="server">
                                                            <ItemTemplate>
                                                                <tr id="trMsg" runat="server" style="cursor: pointer;" messageid='<%#DataBinder.Eval(Container.DataItem, "MESSAGE_ID") %>'
                                                                    onclick='messages.readMsg(this);'>
                                                                    <td style="padding: 3px 3px 3px 10px; vertical-align: middle;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "SENDER_NAME") %><br />
                                                                        <span style="font-size: 10px; color: #808080;"><%#DataBinder.Eval(Container.DataItem, "SUBJECT") %></span>
                                                                    </td>
                                                                    <td style="padding: 3px; vertical-align: middle;">
                                                                        <span style="font-size: 10px;"><%# DataBinder.Eval(Container.DataItem, "DATE_SENT") %></span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                </asp:TabContainer>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="float: left; width: 48%; padding: 4px;">
                        <div id="divMsgContents" runat="server" visible="false">
                            <div id="divMsgHeader" style="padding: 4px; background-color: #cae3f2; border-bottom: 1px solid #3b58b5; margin-bottom: 10px;">
                                <div>
                                    <div style="float: left; width: 70%;">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="text-align: right; padding-right: 4px;">
                                                        <span class="label">To:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lblTo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; padding-right: 4px;">
                                                        <span class="label">From:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; padding-right: 4px;">
                                                        <span class="label">Subject:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div style="float: right; width: 27%">
                                        <asp:UpdatePanel ID="upMsgActions" runat="server">
                                            <ContentTemplate>
                                                <table border="0" style="width: 100%;">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="btnReply" runat="server" Text="Reply" CssClass="button-yellow" OnClick="btnReply_OnClick" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="btnRToAll" runat="server" Text="Reply to All" CssClass="button-yellow" OnClick="btnRToAll_OnClick" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="btnDeleteMsg" runat="server" Text="Delete" CssClass="button-yellow" OnClick="btnDeleteMsg_OnClick" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                            </div>
                            <div id="divMsgBody" style="padding: 4px;">
                                <asp:TextBox ID="txtMsgBodyContents" runat="server" TextMode="MultiLine" Width="100%" Rows="30" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <asp:HiddenField ID="htxtSelectedMsg" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>


<!-- Compose Message Popup -->
<ext:Window ID="winComposeMsg" runat="server" Title="Compose Message" Icon="EmailAdd"
    AutoHeight="true" Width="730px" BodyStyle="background-color: #fff;" Padding="5"
    Collapsible="false" Modal="true" Hidden="true" IDMode="Static">
    <Content>
        <div style="padding: 10px;">
            <div style="margin:5px 0; padding:4px; background-color: #eae4e4;">
                <asp:UpdatePanel ID="upSendMessage" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnSendMsg" runat="server" Text="Send" OnClick="btnSendMsg_OnClick" />&nbsp;
                        <asp:Button ID="btnCancelMsg" runat="server" Text="Cancel" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <table border="0" style="width: 100%;">
                <tbody>
                    <tr>
                        <td style="text-align: right; padding-right: 5px; vertical-align: top;"><span class="label">To:</span></td>
                        <td>
                            <table>
                                        <tr>
                                            <td style="width: 120px; vertical-align: top;">
                                                <%--<input type="button" value="Select Patient(s)" onclick="messages.selectPatient();" /><br/>--%>
                                                <input type="button" value="Select Provider(s)" onclick="messages.selectProvider();" />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <div style="margin-left: 12px; padding: 4px;">
                                                    <%--<asp:Label ID="lblRecipients" runat="server" CssClass="message-recipients"></asp:Label>--%>
                                                    <div class="form_tags">
                                                        <input type="text" name="txtrecipients" value="" id="form_tags_input" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 5px;"><span class="label">Subject:</span></td>
                        <td>
                            <asp:TextBox ID="txtSubject" runat="server" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="margin-top: 10px;">
                <asp:TextBox ID="txtMsgBody" runat="server" TextMode="MultiLine" Width="99%" Rows="20"></asp:TextBox>
            </div>
        </div>
    </Content>
</ext:Window>

<!-- Select Provider Popup -->
<ext:Window ID="winSelectProvider" runat="server" Title="Select Provider" Icon="EmailAdd"
    AutoHeight="true" Width="350px" BodyStyle="background-color: #fff;" Padding="5"
    Collapsible="false" Modal="true" Hidden="true" IDMode="Static">
    <%--<Listeners>
        <BeforeHide Handler="messages.selectProviderRecipients(true); return true;" />
    </Listeners>--%>
    <Content>
        <div style="padding: 10px; height: 350px; overflow: auto;">
            <asp:CheckBoxList ID="chklstProviders" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="chklist"></asp:CheckBoxList>
        </div>
        <div style="margin: 10px 0; text-align: center;">
            <%--<input type="button" value="Select" onclick="messages.selectProviderRecipients();" />--%>
            <input type="button" value="Select" onclick="Ext.onReady(function () { winSelectProvider.hide(); });" />
        </div>
    </Content>
</ext:Window>

<!-- Select Patient Popup -->
<ext:Window ID="winSelectPatient" runat="server" Title="Select Patient" Icon="EmailAdd"
    AutoHeight="true" Width="350px" BodyStyle="background-color: #fff;" Padding="5"
    Collapsible="false" Modal="true" Hidden="true" IDMode="Static">
    <Content>
        <div style="padding: 10px; height:350px; overflow:auto;">
            <asp:CheckBoxList ID="chklstPatients" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="chklist"></asp:CheckBoxList>
        </div>
    </Content>
</ext:Window>

<script>
    $(document).ready(function () {
        setTimeout(function () {
            messages.opts.selectPanel = '<%= upSelectMsg.ClientID %>';
        }, 1);
    });
</script>