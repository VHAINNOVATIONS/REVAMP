<%@ Page Title="REVAMP Practitioner - Template Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="template_mgmt.aspx.cs" Inherits="template_mgmt" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register TagPrefix="ucTemplate" TagName="ucTemplate" Src="~/ucTemplateManagement.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="smTemplates" runat="server">
        <Scripts>
            <asp:ScriptReference Path="js/management/management.js" />
            <asp:ScriptReference Path="js/management/management.template.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    
    <% if (!bAllowUpdate)
        { %>
            <div style="display: block; margin: 10px; background-color: #f1f1f1; padding: 4px; text-align:left;">
                <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png" runat="server" />&nbsp;
                You have <b>Read-Only access</b> to this section.
            </div>
        <%} %>
    <asp:UpdatePanel ID="upTemplateManagement" runat="server" >
        <ContentTemplate>
            <ucTemplate:ucTemplate ID="ucTemplate" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- show template tags reference popup --%>
    <ext:Window ID="winTemplateTags" runat="server" IDMode="Static" Hidden="true" Modal="false"
        Collapsible="true" Maximizable="false" Width="450" Height="525" Icon="ApplicationAdd"
        Title="Template Tags" BodyStyle="background-color: #f1f1f1;">
        <Content>
            <div style="margin: 10px; padding: 10px; background-color: #fff; width:auto; height:450px; text-align:left; overflow:auto;">
                <!-- Repeater starts here -->
                <asp:Repeater ID="repTempItemGroups" runat="server">
                    <ItemTemplate>
                        <div style="margin-bottom: 5px; border: 1px solid #e1e1e1; background-color: #f1f1f1;
                            padding: 5px; color: #000; font-weight: bold;">
                            <%#DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>
                        </div>
                        <asp:Repeater ID="repTempItems" runat="server" DataSource='<%#((DataRowView)Container.DataItem).Row.GetChildRows("taggroup") %>'>
                            <ItemTemplate>
                                <div style="padding-left: 15px; margin-bottom: 5px; color:#000;">
                                    [<%#DataBinder.Eval(Container.DataItem, "[\"DESCRIPTION\"]") %>]
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- Repeater ends here -->
            </div>
        </Content>
    </ext:Window>
    
    <script type="text/javascript">

        management.template.init();
        
        function autoAdjustMainDiv()
        {
            $(document).ready(function () {
                setTimeout(function () {
                    origWidth = $('input[id$="htxtMainDivWidth"]').val();
                    wp.adjustMain();

                    if ($('div[id$="mainContents"]').css('width') != origWidth)
                    {
                        $('div[id$="mainContents"]').css({
                            width: origWidth
                        });
                        $('input[id$="htxtMainDivWidth"]').val(origWidth);
                    }
                    $('input[type="button"], input[type="submit"]').css({
                        padding: '2px 6px'
                    });
                }, 1);
            });
        }

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args)
        {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args)
        {
            $('div.ajax-loader').hide();
            management.template.init();
            restartSessionTimeout();
        }
    </script>
        
</asp:Content>
