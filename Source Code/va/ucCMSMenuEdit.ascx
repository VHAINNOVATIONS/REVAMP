<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCMSMenuEdit.ascx.cs" Inherits="ucCMSMenuEdit" %>
<%@ Import Namespace="System.Data" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<div class="PageTitle" style="margin-bottom: 10px;">
    CMS Menu Editor
</div>
<div align="left" id="divStatus" runat="server" style="margin-bottom: 0; margin-left: 10px; padding: 0; font-weight: bold;"></div>
<div>
    <div id="divMenuTree" runat="server" style="float: left; width: 350px; margin-right: 10px;"></div>
    <div id="divMenuControls" style="float: left; display: none; width: 550px; border-left: 1px solid #e1e1e1; padding-left: 10px;">
        <div id="divMenuButtons" style="padding: 5px; background-color: #e1e1e1;">

            <input type="radio" style="margin-right: 4px;" id="radEditItm" name="radMenuItemsControls" value="0" onclick="cms.menu.editItem();" />
            <label for="radEditItm" style="margin-right: 10px;">Edit Menu Item</label>

            <input type="radio" style="margin-right: 4px;" id="radAddItm" name="radMenuItemsControls" value="1" onclick="cms.menu.addChildItem();" />
            <label for="radAddItm" style="margin-right: 10px;">Add Child Menu Item</label>
            <asp:Button ID="btnDeleteMenuItem" runat="server" Text="Delete Menu Item" CssClass="button-yellow" OnClick="btnDeleteMenuItem_OnClick" />
        </div>
        <div style="margin-top: 10px;">
            <table border="0" style="width: 100%">
                <tbody>
                    <tr>
                        <td style="width: 100px; text-align: right; padding-right: 5px;">
                            <span class="label">Title</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMenuTitle" runat="server" Width="95%" MaxLength="255"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right; padding-right: 5px;">
                            <span class="label">Target Page</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="cboTargetPage" runat="server" Width="95%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right; padding-right: 5px;">
                            <span class="label">Target Portal</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:RadioButtonList ID="rblTargetPortal" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-list">
                                <asp:ListItem Text="Patient" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Provider" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Both" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right; padding-right: 5px;">
                            <span class="label">Sort Order</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSortOrder" runat="server" MaxLength="4" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="divSpacer" style="height: 10px;"></div>
                        </td>
                    </tr>
                    <tr id="trUserRights" style="display: none;">
                        <td style="width: 100px; text-align: right; padding-right: 5px; vertical-align: top;">
                            <span class="label">User Rights</span>
                        </td>
                        <td style="text-align: left;">
                            <table border="0">
                                <tbody>
                                    <asp:Repeater ID="repUserRights" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="checkbox-list">
                                                    <input type="checkbox" id='chklstUserRights_<%#DataBinder.Eval(Container.DataItem, "RIGHT_DEC") %>'
                                                        name="chklstUserRights"
                                                        value='<%#DataBinder.Eval(Container.DataItem, "RIGHT_DEC") %>' />
                                                    <label for='chklstUserRights_<%#DataBinder.Eval(Container.DataItem, "RIGHT_DEC") %>'><%#DataBinder.Eval(Container.DataItem, "RIGHT_DESC") %></label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div style="clear: both"></div>
</div>

<asp:HiddenField ID="htxtCurrentID" runat="server" />
<asp:HiddenField ID="htxtParentID" runat="server" />
<asp:HiddenField ID="htxtUserRights" runat="server" />
<asp:HiddenField ID="htxtEditMode" runat="server" />
<asp:HiddenField ID="htxtMenuData" runat="server" />
