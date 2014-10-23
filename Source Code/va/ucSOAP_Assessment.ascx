<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSOAP_Assessment.ascx.cs" Inherits="ucSOAP_Assessment" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Import Namespace="System.Data" %>

<style type="text/css">
    .css-criteria
    {
        color: Navy;
        font-size: 12px; 
        font-weight: bold;
    }
    .criteria-question
      {
        border: 1px solid #e1e1e1;
        padding: 6px;
        background-color: #f1f1f1;
        font-weight: bold;
      }
</style>

<div style="margin: 10px 0; padding: 5px;">
    <fieldset style="margin: 0 5px; border: 1px solid #e1e1e1; background-color: #f1f1f1;">
        <legend style="font-weight: bold; padding: 5px; border: 1px solid #e1e1e1; background-color: #fff;
            margin-left: 15px;">Problem List:&nbsp;</legend>
        <%-- ******************************************************************** --%>
        <%--                            PROBLEM LIST                              --%>
        <%-- ******************************************************************** --%>
        <div style="display: none;">
        <asp:RadioButtonList ID="rblDiagAxes" runat="server" RepeatDirection="Horizontal"
            CellSpacing="8" EnableViewState="true" >
            <asp:ListItem Value="1" Text="Axis I" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:UpdatePanel ID="upDiagnosisList" runat="server">
            <ContentTemplate>
                <div id="divDiagTbls" style="margin: 10px;">
                    <div id="divDiagAxis1" axisid="1" class="problem-div" runat="server">
                        <h2 style="margin-bottom: 10px;">
                        <%if (bAllowUpdate && (lROAssessment > (long)RightMode.ReadOnly))
                          { %>
                            &nbsp;<input type="button" class="button" value="Add Diagnosis" onclick="Ext.onReady(function(){soap.assessment.eraseHtxtDiag(); setTimeout('winAxis1.show();',0);});" />
                        <%} %>
                        </h2>
                        <table id="tblDiagAxis1" class="mGrid" style="background-color: #fff; width: 100%;">
                            <thead>
                                <tr>
                                    <th style="width: 25px;">
                                    </th>
                                    <th style="width: 88px;">
                                        ICD9
                                    </th>
                                    <th style="width: 25%;">
                                        Problem
                                    </th>
                                    <th>
                                        Comment
                                    </th>
                                    <%if (bAllowUpdate && (lROAssessment > (long)RightMode.ReadOnly))
                                      { %>
                                    <th style="width: 60px;">
                                        Sort<br />
                                        Order
                                    </th>
                                    <th style="width: 60px;">
                                        Options
                                    </th>
                                    <%} %>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repDiagA1" runat="server" OnItemDataBound="repDiagA1_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr id='trdiag_<%#DataBinder.Eval(Container.DataItem, "problem_id") %>'>
                                            <td>
                                                <div class="grid-read">
                                                    <asp:RadioButton ID="radDiagA1" runat="server" OnCheckedChanged="radDiagA1_OnCheckedChanged"
                                                        AutoPostBack="true" />
                                                </div>
                                                <div class="grid-edit">
                                                </div>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "DIAG_CODE") %>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "DIAGNOSTIC_TEXT") %>
                                            </td>
                                            <td>
                                                <div class="grid-read">
                                                    <%#DataBinder.Eval(Container.DataItem, "DIAGNOSTIC_COMMENT") %>
                                                </div>
                                                <div class="grid-edit">
                                                    <input type="hidden" id="htxtA1CommentOrig" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "DIAGNOSTIC_COMMENT") %>' />
                                                    <asp:TextBox ID="txtA1Comment" runat="server" Width="95%" Text='<%#DataBinder.Eval(Container.DataItem, "DIAGNOSTIC_COMMENT") %>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <%if (bAllowUpdate && (lROAssessment > (long)RightMode.ReadOnly))
                                              { %>
                                            <td style="width: 60px;">
                                                <div class="grid-read">
                                                    <%#DataBinder.Eval(Container.DataItem, "SORT_ORDER") %>
                                                </div>
                                                <div class="grid-edit">
                                                    <input type="hidden" id="htxtA1SortOrderOrig" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "SORT_ORDER") %>' />
                                                    <asp:DropDownList ID="cboA1SortOrder" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grid-read" style="text-align: center;">
                                                    <img alt="Edit Diagnosis" src="Images/pencil.png" style="cursor: pointer;" onclick="soap.editGrid.editThisRow(this);" />&nbsp
                                                    <img src="Images/delete.png" alt="discontinue diagnosis item" 
                                                    style="cursor: pointer;" 
                                                    problemid='<%#DataBinder.Eval(Container.DataItem,"PROBLEM_ID") %>'  
                                                    onclick="soap.assessment.discontinueDiagnosis(this);" />
                                                </div>
                                                <div class="grid-edit" style="text-align: center;">
                                                    <asp:ImageButton ID="btnUpdateA1Diag" runat="server" ImageUrl="~/Images/disk.png"
                                                        OnClick="btnUpdateA1Diag_OnClick" />&nbsp;
                                                    <img alt="Cancel Edit" src="Images/cross.png" style="cursor: pointer;" onclick="soap.editGrid.cancelEditThisRow(this, 0);" />
                                                </div>
                                            </td>
                                            <%} %>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- ******************************************************************** --%>
        <%--                            ENDS // PROBLEM LIST                      --%>
        <%-- ******************************************************************** --%>
    </fieldset>
</div>
<div id="divCriteriaDefs" style="margin: 10px 0; padding: 5px;">
    <fieldset style="margin: 0 5px; border: 1px solid #e1e1e1; background-color: #f1f1f1;">
        <legend id="legendCriteria" runat="server" style="font-weight: bold; padding: 5px;
            border: 1px solid #e1e1e1; background-color: #fff; margin-left: 15px;">Criteria</legend>
        <div style="padding: 15px;">
            <%--Disable--%>
            <!--<h2 id="hProbTitle" runat="server" style="margin: 2px;">
            </h2> -->
            <span id="spProbDescription" runat="server" style="display: block; margin-bottom: 10px;
                padding-left: 25px;"></span><span id="spNoCriteria" runat="server" style="display: block;
                    margin-bottom: 10px;" visible="false">No assessment data found for this problem.</span>
        </div>
       <asp:UpdatePanel ID="upCriteria" runat="server">
            <ContentTemplate>
                <table style="background-color: #fff; width: 100%;">
                    <asp:Repeater ID="repCriteria" runat="server" OnItemDataBound="repCriteria_OnItemDataBound">
                        <ItemTemplate>
                            <table border="0">
                                <tr valign="top">
                                    <td style="text-align: center; width: 25px; padding-bottom: 6px;">
                                        <asp:CheckBox ID="chkCriteria" runat="server" OnCheckedChanged="chkCriteria_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </td>
                                    <td style="padding-bottom: 6px;">
                                        <asp:Label ID="lblCriteria" runat="server" AssociatedControlID="chkCriteria" Text='<%#DataBinder.Eval(Container.DataItem, "CRITERIA") %>'
                                            CssClass="css-criteria"></asp:Label>
                                        <div style="padding-left: 3px;">
                                            <table border="0">
                                                <asp:Repeater ID="repCriteriaDef" runat="server" DataSource='<%#((DataRowView)Container.DataItem).Row.GetChildRows("criteria") %>'
                                                    OnItemDataBound="repDefinition_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:CheckBox ID="chkCriteriaDef" runat="server" OnCheckedChanged="chkCriteriaDef_OnCheckedChanged"
                                                                    AutoPostBack="true" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCriteriaDef" AssociatedControlID="chkCriteriaDef" runat="server"
                                                                    Text='<%#DataBinder.Eval(Container.DataItem, "[\"DEFINITION\"]") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </fieldset>
</div>

<asp:HiddenField ID="htxtSelectedActualProblem" runat="server" />
<asp:HiddenField ID="htxtSelectedPotentialProblem" runat="server" />

<!-- *************************************************************************** -->
<!-- ************************** AXIS SELECTION POPUPS ************************** -->
<!-- *************************************************************************** -->

<!-- Axis I Popup -->
<ext:Window ID="winAxis1" runat="server" Title="Select Diagnosis Item" Icon="ApplicationAdd"
	Width="580px" Height="570px" BodyStyle="background-color: #fff;" Padding="5"
	Collapsible="true" Modal="true" Hidden="true" IDMode="Static">
	<Content>
	    <asp:UpdatePanel ID="upSelA1" runat="server">
            <ContentTemplate>
                <div id="axisi_container" style="width: 540px; height: 520px; border: 1px solid #808080;
                    background: #fff; overflow: auto; margin-left: 4px;">
                    <asp:Literal ID="litAxisI" runat="server"></asp:Literal>
                </div>
            </ContentTemplate>
	    </asp:UpdatePanel>
	</Content>
</ext:Window>
<%-- ***************************************************************** --%>
<%--                            DISCONTINUE PROBLEM                    --%>
<%-- ***************************************************************** --%>
<ext:Window ID="winDiscDiagItem" runat="server" Title="Discontinue Problem" Height="170px"
    Width="350px" BodyStyle="background-color: #fff;" Padding="5" Collapsible="True"
    Modal="True" Hidden="True" IDMode="Static">
    <Content>
        <asp:UpdatePanel ID="upDiscProblem" runat="server">
            <ContentTemplate>
                <div style="padding: 10px;">
				<asp:Label ID="lblDiscDiagItemError" runat="server" ></asp:Label><br/>
                    <asp:Label ID="lblDiscProblem" runat="server" AssociatedControlID="txtDiscDiag" Text="Reason for discontinuing: "></asp:Label>
                    <asp:TextBox ID="txtDiscDiag" runat="server" TextMode="MultiLine" Rows="3" Width="95%"></asp:TextBox>
                    <center>
                        <asp:Button ID="btnDiscDiagItem" CssClass="button" runat="server" Text="Discontinue" Enabled="false" OnClick="DiscDiagItem_OnClick" />
                    </center>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </Content>
</ext:Window>