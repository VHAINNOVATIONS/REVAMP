<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatientEvent.ascx.cs" Inherits="ucPatientEvent" %>
<%@ Import Namespace="System.Data" %>
<asp:UpdatePanel ID="upPatEvt" runat="server">
    <ContentTemplate>
        <table class="mGrid" border="0">
            <thead>
                <tr>
                    <th style="width: 315px;" >Event</th>
                    <th style="width: 125px; text-align: center;">Scheduled Event Date</th>
                    <th style="width: 125px; text-align: center;">Status</th>
                    <th>Comments</th>
                    <th style="width: 30px; text-align: center;">&nbsp;</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repPatientEvents" runat="server" OnItemDataBound="repPatientEvents_OnItemDataBound">
                    <ItemTemplate>
                        <tr id="trPatEvt" runat="server">
                            <td>
                                <span style="font-weight: bold;">
                                    <%# DataBinder.Eval(Container.DataItem, "EVENT") %>
                                </span>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblEvtDate" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEvtDate" runat="server" Width="97%" CssClass="date-picker"></asp:TextBox><br />
                                <span id="spQuest" style="font-size: 10px; color: green;" runat="server" visible="false">Patient started to answers these questionnaires.</span>
                            </td>
                            <td>
                                <div style="text-align: center;">
                                    <asp:CheckBox ID="chkEventStatus" runat="server" />
                                    <asp:Image ID="imgEvtDone" runat="server" ImageUrl="~/Images/tick.png" />
                                    <asp:Image ID="imgEvtNotDone" runat="server" ImageUrl="~/Images/cross.png" />
                                </div>
                                <div style="text-align: center;">
                                    <asp:Label ID="lblStatusDate" runat="server"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEvtComments" runat="server" Width="97%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </td>
                            <td style="text-align: center; width: 30px;">
                                <asp:ImageButton ID="imgBtnUpdt" runat="server" ImageUrl="~/Images/disk.png" OnClick="imgBtnUpdt_OnClick" AlternateText="Save" ToolTip="Save" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
