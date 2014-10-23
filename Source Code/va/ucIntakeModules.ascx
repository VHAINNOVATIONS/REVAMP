<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucIntakeModules.ascx.cs"
    Inherits="ucIntakeModules" %>
 <%@ Import Namespace="System.Data" %>   
    
<style type="text/css">
    body
    {
        font-family: Arial, Helvetiva, Verdana, sans-serif;
    }
    
    .group-header tr
    {
        background-color: #435265;
        color: #fff;
        font-weight: bold;
    }
    
    div.group-header
    {
        border: 1px solid #f1f1f1;
    }
    
    .group-header td
    {
        padding: 4px 0;
    }
    
    .group-header > div
    {
        color: #fff;
        font-weight: bold;
    }
    
    .module-items td
    {
        padding: 2px 0;
    }
    .module-items tr:nth-child(even)
    {
        background-color: #e1e1e1;
    }
    .module-items tr:nth-child(odd)
    {
        background-color: #f1f1f1;
    }
    
    .module-items-even
    {
        background-color: #e1e1e1;
    }
    .module-items-odd
    {
        background-color: #f1f1f1;
    }
</style>
<div id="divAssignModulesWrapper" style="width: auto; margin: 0 0 0 10px;">
    <asp:Repeater ID="repGroup" runat="server" OnItemDataBound="repGroup_OnItemDataBound">
        <ItemTemplate>
            <div id="divGroupWrapper" class="group-wrapper" groupid="1">
                <div class="group-header" groupid='<%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_ID") %>'>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td align="center" style="width: 20px; vertical-align: middle;">
                                <input type="checkbox" id='chkSelectGroup' runat="server"
                                class="chk-select-group" />
                            </td>
                            <td>
                                <div style="padding-left: 5px; font-weight: bold; color: #fff;">
                                    <%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_DESCR") %>
                                </div>
                            </td>
                            <td style="width: 24px;" align="center">
                                <img id='imgExpandCollapse_<%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_ID") %>' src="Images/up-arrow.png" alt="Expand/Collapse Group" class="icon-expand-collapse expanded"
                                    groupid='<%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_ID") %>' style="vertical-align: middle;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id='divModuleItems_<%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_ID") %>' class="div-module-items" 
                groupid='<%#DataBinder.Eval(Container.DataItem, "MODULE_GROUP_ID") %>'>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" class="module-items">
                        <asp:Repeater ID="repModules" runat="server" OnItemDataBound="repModules_OnItemDataBound" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("modgroups") %>'>
                            <ItemTemplate>
                                <tr>
                                    <td align="center" style="width: 20px; vertical-align: middle;">
                                        <input type="checkbox" id="chkModule" runat="server" class="chk-select-module" />
                                    </td>
                                    <td>
                                        <div style="padding-left: 5px;">
                                            <asp:Label ID="lblModuleTitle" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    

</div>

<asp:HiddenField ID="htxtIconState" runat="server" />
<asp:HiddenField ID="htxtChkbxState" runat="server" />
<asp:HiddenField ID="htxtDivState" runat="server" />