<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEventManagement.ascx.cs" Inherits="ucEventManagement" %>
<%@ Import Namespace="System.Data" %>

<div style="margin-bottom: 15px; padding: 8px; border: 1px solid #e1e1e1;">
    <asp:UpdatePanel ID="upEvtMgnt" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td style="padding-right: 10px;">
                        <span style="color: #808080; font-weight: bold;">View Events Due</span>&nbsp;
                        <asp:DropDownList ID="cboEvtDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboEvtDate_OnSelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="View All"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Overdue Events"></asp:ListItem>
                            <asp:ListItem Value="0" Text="This Week"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Select Date Range"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div id="divDateRange" runat="server">
                            From:&nbsp;<asp:TextBox ID="txtFromDate" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                    To:&nbsp;<asp:TextBox ID="txtToDate" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnUpdateList" runat="server" Text="Update List" OnClick="btnUpdateList_OnClick" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<table class="tbl_evt_headings">
    <thead>
        <tr>
            <td>Patients&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearchPatient" runat="server"></asp:TextBox></td>
            <asp:Repeater ID="repEvtHeaders" runat="server">
                <ItemTemplate>
                    <td>
                        <img alt="event header" src='Images/img_evts/patevt_<%#DataBinder.Eval(Container.DataItem, "EVENT_ID") %>.png' />
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
    </thead>
</table>
<div id="divEvtRows">
    <asp:UpdatePanel ID="upEvtList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tbl_evt_rows">
                <tbody>
                    <asp:Repeater ID="repPatientNames" runat="server" OnItemDataBound="repPatientNames_OnItemDataBound">
                        <ItemTemplate>
                            <tr id='tr_<%#DataBinder.Eval(Container.DataItem, "PATIENT_ID") %>'>
                                <td style="width: 299px;">
                                    <div style="width:100%;">
                                        <div style="width:65%; float:left;">
                                            <a id="lnkPatName" runat="server"><%#DataBinder.Eval(Container.DataItem, "NAME") %></a>
                                        </div>
                                        <div style="width:34%; float:right; font-weight: normal;">
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                        </div>
                                        <div style="clear:both;"></div>
                                    </div>
                                </td>
                                <asp:Repeater ID="repPatEvts" runat="server" OnItemDataBound="repPatEvts_OnItemDataBound" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("events") %>'>
                                    <ItemTemplate>
                                        <td style="text-align: center; width: 36px;">
                                            <asp:Image ID="imgEvtStatus" runat="server" />
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

