<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTemplateManagement.ascx.cs"
    Inherits="ucTemplateManagement" %>
<%@ Import Namespace="System.Data" %>

<asp:Label ID="lblDebug" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>

<span style="display: block; margin-left: 12px; text-align: left;" class="PageTitle">Template Management</span>

<div style="margin: 12px; padding: 10px; border: 1px solid #8692c4; text-align: left;">
    <span style="display: block; font-size: 13px; font-weight: bold; margin-bottom: 10px;">Template Groups</span>
    <%if (bAllowUpdate)
      { %>
    <input type="button" value="Add Group" onclick="management.template.group.AddTempGroup();" />
    <% } %>

    <table id="tblTemplateGroups" border="0" cellpadding="4" cellspacing="0" class="mGrid" style="width: 100%;">
        <thead>
            <tr>
                <th>Name</th>
                <%if (bAllowUpdate)
                  { %>
                <th style="width: 60px;">&nbsp;</th>
                <%} %>
            </tr>
        </thead>
        <tbody>
            <tr id='trtempgroup_0' tempgroup='0' style="display: none; background-color: #c8dce0;">
                <td>
                    <asp:TextBox ID="txtTempGroupNameAdd" runat="server" Width="99%"></asp:TextBox>
                </td>
                <td>
                    <div style="text-align: center;">
                        <asp:ImageButton ID="imgBtnInsertTempGroup" runat="server" ImageUrl="~/Images/disk.png" CssClass="edit-row" OnClick="InsertTempGroup" />&nbsp;
                        <img alt="Cancel Add Template Group" src="Images/cross.png" class="cancel-edit" style="cursor: pointer;" onclick="management.template.group.cancelAddTempGroup();" />
                    </div>
                </td>
            </tr>
            <asp:Repeater ID="repTemplateGroups" runat="server" OnItemDataBound="repTemplateGroups_OnItemDataBound">
                <ItemTemplate>
                    <tr id="trTempGroup" runat="server">
                        <td id="tdTempGroupName" runat="server">
                            <div id="divGroupLabel" runat="server" class="grid-read" style="display: block; font-weight: bold;">
                                <%#DataBinder.Eval(Container.DataItem, "GROUP_NAME") %>
                            </div>
                            <div class="grid-edit">
                                <asp:HiddenField ID="htxtGroupName" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "GROUP_NAME") %>' />
                                <asp:TextBox ID="txtGroupName" runat="server" Width="95%" Text='<%#DataBinder.Eval(Container.DataItem, "GROUP_NAME") %>'></asp:TextBox>
                            </div>
                        </td>
                        <td id="tdTempGroupEdit" runat="server">
                            <div class="grid-read" style="text-align: center;">
                                <img alt="Edit Template Group" src="Images/pencil.png" class="edit-row" style="cursor: pointer;"
                                    onclick="management.template.editRow(this);" />
                                <asp:ImageButton ID="btnDeleteTempGroup" runat="server" ImageUrl="~/Images/delete.png" OnClick="btnDeleteTempGroup_OnClick" />
                            </div>
                            <div class="grid-edit" style="text-align: center;">
                                <asp:ImageButton ID="btnUpdateTempGroup" runat="server" ImageUrl="~/Images/disk.png" OnClick="btnUpdateTempGroup_OnClick" />
                                <img alt="Cancel Edit Template Group" src="Images/cross.png" class="cancel-edit" style="cursor: pointer;" onclick="management.template.cancelEditRow(this);" />
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div style="display: none;">
        <asp:Button ID="btnSelectGroup" runat="server" />
    </div>

</div>

<asp:HiddenField ID="htxtTemplateGroupID" runat="server" />

<div id="divTemplates" runat="server" style="margin: 12px; padding: 10px; border: 1px solid #8692c4; text-align: left;">
    <fieldset style="margin: 0 5px; border: 1px solid #e1e1e1; background-color: #f1f1f1;">
        <legend style="font-weight: bold; padding: 5px; border: 1px solid #e1e1e1; background-color: #fff; margin-left: 15px;">Template Type:</legend>
        <div>
            <asp:RadioButtonList ID="rblTemplateType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="8" CellPadding="8"></asp:RadioButtonList>
        </div>
    </fieldset>

    <div style="margin-top: 15px; padding: 10px; background-color: #f1f1f1;">
        <%if (bAllowUpdate)
          { %>
        <input type="button" class="button" value="Add Template" style="margin-bottom: 10px;" onclick="management.template.addRow();" />&nbsp;
                <%} %>
        <input type="button" class="button" value="View Tags Reference" style="margin-bottom: 10px;" onclick="Ext.onReady(function () { winTemplateTags.show(); });" />
        <table id="tblTemplates" border="0" cellpadding="4" cellspacing="0" class="mGrid" style="width: 100%;">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Template</th>
                    <%if (bAllowUpdate)
                      { %>
                    <th style="width: 60px;">&nbsp;</th>
                    <%} %>
                </tr>
            </thead>
            <tbody>
                <tr id='trtemp_0' templatetype='0' style="display: none;">
                    <td>
                        <asp:TextBox ID="txtTemplateNameAdd" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td>
                        <span id="spTemplateType"></span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTemplateTextAdd" runat="server" Width="95%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </td>
                    <td>
                        <div style="text-align: center;">
                            <asp:ImageButton ID="btnTemplateAdd" runat="server" ImageUrl="~/Images/disk.png" CssClass="edit-row" OnClick="InsertTemplate" />
                            <img alt="Cancel Edit Template" src="Images/cross.png" class="cancel-edit" style="cursor: pointer;"
                                onclick="management.template.cancelAddRow(this);" />
                        </div>
                    </td>
                </tr>
                <asp:Repeater ID="repTemplates" runat="server" OnItemDataBound="repTemplates_OnItemDataBound">
                    <ItemTemplate>
                        <tr id='trtemp_<%#DataBinder.Eval(Container.DataItem, "TEMPLATE_ID") %>' templatetype='<%#DataBinder.Eval(Container.DataItem, "TYPE_ID") %>'
                            style="display: none;">
                            <td>
                                <div class="grid-read" style="font-weight: bold;">
                                    <%#DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>
                                </div>
                                <div class="grid-edit">
                                    <asp:HiddenField ID="htxtTemplateNameOrig" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>' />
                                    <asp:TextBox ID="txtTemplateName" runat="server" Width="95%" Text='<%#DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>'></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div class="grid-read">
                                    <%#DataBinder.Eval(Container.DataItem, "TEMPLATE_TYPE") %>
                                </div>
                                <div class="grid-edit">
                                    <asp:HiddenField ID="htxtTemplateTypeOrig" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "TYPE_ID") %>' />
                                    <asp:DropDownList ID="cboTemplateType" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div class="grid-read">
                                    <div style="width: auto; height: 60px; overflow: auto; vertical-align: middle;">
                                        <%#DataBinder.Eval(Container.DataItem, "TEMPLATE") %>
                                    </div>
                                </div>
                                <div class="grid-edit">
                                    <asp:HiddenField ID="htxtTemplateTextOrig" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "TEMPLATE") %>' />
                                    <asp:TextBox ID="txtTemplateText" runat="server" Width="95%" TextMode="MultiLine"
                                        Rows="5" Text='<%#DataBinder.Eval(Container.DataItem, "TEMPLATE") %>'></asp:TextBox>
                                </div>
                            </td>
                            <%if (bAllowUpdate)
                              { %>
                            <td>
                                <div class="grid-read" style="text-align: center;">
                                    <img alt="Edit Template" src="Images/pencil.png" class="edit-row" style="cursor: pointer;"
                                        onclick="management.template.editRow(this);" />
                                    <asp:ImageButton ID="btnDeleteTemplate" runat="server" ImageUrl="~/Images/delete.png"
                                        OnClick="DeleteTemplate" />
                                </div>
                                <div class="grid-edit" style="text-align: center;">
                                    <asp:ImageButton ID="btnUpdateTemplate" runat="server" ImageUrl="~/Images/disk.png" OnClick="UpdateTemplate" />
                                    <img alt="Cancel Edit Template" src="Images/cross.png" class="cancel-edit" style="cursor: pointer;"
                                        onclick="management.template.cancelEditRow(this);" />
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

<script type="text/javascript">
    management.template.group.opts.SelectGrpBtn = '<%= btnSelectGroup.ClientID %>';
</script>
