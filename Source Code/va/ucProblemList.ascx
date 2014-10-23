<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProblemList.ascx.cs" Inherits="ucProblemList" %>
<style type="text/css">
    .multiview
    {
        display: none;
    }
    .problem-div
    {
        display: none;
    }
    div.axis5panel
    {
        display: block;
        margin-bottom: 5px;
        border: 1px solid #e1e1e1;
        padding: 10px;
    }
    .axis5panel a
    {
        text-decoration: none;
    }
</style>
<div style="text-align: right;">
</div>
<fieldset style="margin: 0 20px; border:1px solid #e1e1e1; background-color:#f1f1f1;">
    <legend style="font-weight:bold; padding:2px 5px;">Problem List</legend>
    <%-- ******************************************************************** --%>
    <%--                            PROBLEM LIST                              --%>
    <%-- ******************************************************************** --%>
    <div style="display: none;">
    <asp:RadioButtonList ID="rblAxes" runat="server" RepeatDirection="Horizontal" CellSpacing="8"
        EnableViewState="true" AutoPostBack="true" OnSelectedIndexChanged="ResetProblemSelections">
        <asp:ListItem Value="1" Text="Axis I"></asp:ListItem>
    </asp:RadioButtonList>
    </div>
    <div id="divProblemTbls" style="margin: 10px;">
        <div id="divAxis1" axisid="1" class="problem-div" runat="server">
            <table id="tblOMProblemsAxis1" class="mGrid" width="100%">
                <thead>
                    <tr>
                        <th style="width: 25px;">
                        </th>
                        <th>
                            ICD9
                        </th>
                        <th>
                            Problem
                        </th>
                        <th>
                            Modifier
                        </th>
                        <th>
                            Comment
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repProblemsA1" runat="server" OnItemDataBound="repProblemsA1_OnItemDataBound">
                        <ItemTemplate>
                            <tr id='trpr_<%#DataBinder.Eval(Container.DataItem, "problem_id") %>'>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnA1Problem" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "diag_code") %>'
                                        OnClick="lnkbtnProblem_OnClick"></asp:LinkButton>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "diagnostic_text") %>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "stat_specifier_text")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "diagnostic_comment") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <%-- ******************************************************************** --%>
    <%--                            ENDS // PROBLEM LIST                      --%>
    <%-- ******************************************************************** --%>
</fieldset>
<asp:HiddenField ID="htxtProblemSummary" runat="server" />