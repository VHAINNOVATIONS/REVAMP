<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCMSPageEdit.ascx.cs" Inherits="ucCMSPageEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Import Namespace="System.Data" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="PageTitle" style="margin: 10px;">Create/Edit Page Contents</div>
<div style="margin: 10px; padding: 10px; width: 90%;">
    <asp:UpdatePanel ID="upEditPage" runat="server">
        <ContentTemplate>
            <div align="left" id="divStatus" runat="server" style="margin-bottom: 0; margin-left: 10px; padding: 0; font-weight: bold;"></div>
            <div style="border: 1px solid #e1e1e1; padding: 10px; background-color: #e1e1e1;">
                <div style="float: left;">
                    <asp:RadioButtonList ID="rblPageEditMode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radBtnList" EnableViewState="true">
                        <asp:ListItem Text="Create Page" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Edit Page" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div id="divEditContainer" style="float: left; margin-left: 20px;">
                    <div id="divAddPage" runat="server">
                        <input type="button" id="btnAddPage" value="Reset Contents" class="button-yellow" onclick="cms.page.resetPage();" />
                    </div>
                    <div id="divEditPage" style="display: none;" runat="server">
                        <input type="button" id="btnEditPage" value="Select Page" class="button-yellow" onclick="cms.page.editPage();" />
                    </div>
                </div>
                <div style="clear: both;"></div>
            </div>
            <div id="divEditControls" runat="server" style="margin-top: 10px; padding: 10px; border: 1px solid #e1e1e1;">
                <table border="0" style="margin-bottom: 10px; table-layout: auto; width: 100%;">
                    <tbody>
                        <tr>
                            <td style="text-align: right; padding-right: 5px; width: 60px;">
                                <span class="label">Title:</span>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPageTitle" runat="server" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; padding-right: 5px;">
                                <span class="label">Author:</span>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="cboAuthors" runat="server" EnableViewState="true"></asp:DropDownList>&nbsp;
                                <span class="label" style="margin-right: 5px;">Status:</span>
                                <asp:DropDownList ID="cboPageStatus" runat="server" EnableViewState="true">
                                    <asp:ListItem Text="Un-Published" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Published" Value="1"></asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                <span id="spDeletePage" style="display: none;" runat="server">
                                    <asp:Button ID="btnDeletePage" runat="server" Text="Delete Page" CssClass="button-yellow" OnClick="DeletePage" />
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <asp:TextBox ID="txtPageContents" runat="server" TextMode="MultiLine" Rows="25" Columns="80" Width="100%" CssClass="tinymce"></asp:TextBox>

            </div>

            <asp:HiddenField ID="htxtSelectedPageID" runat="server" />

            <!-- Select CMS Page -->
            <ext:Window ID="winSelectPage" runat="server" Title="Select Page" Icon="MagnifierZoomIn"
                AutoHeight="true" Width="630px" BodyStyle="background-color: #fff;" Padding="5"
                Collapsible="false" Modal="true" Hidden="true" IDMode="Static">
                <Content>
                    <div style="border: 1px solid #e1e1e1; padding: 5px; background-color: #e1e1e1; margin-bottom: 10px;">
                        <span class="label">Author: </span>&nbsp;<asp:DropDownList ID="cboAuthorsPopup" runat="server"></asp:DropDownList>&nbsp;
                <span class="label">Status: </span>&nbsp;
                <asp:DropDownList ID="cboStatusPopup" runat="server">
                    <asp:ListItem Text="-View All-" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Un-Published" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Published" Value="1"></asp:ListItem>
                </asp:DropDownList>
                    </div>
                    <div style="height: 350px; overflow: auto;">
                        <table id="tblStaticPagesList" border="0" class="mGrid" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>Author</th>
                                    <th style="width: 50px;">Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repPagesList" runat="server" OnItemDataBound="repPagesList_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr pageid='<%#DataBinder.Eval(Container.DataItem, "PAGE_ID") %>' authorid='<%#DataBinder.Eval(Container.DataItem, "AUTHOR_ID") %>' statusid='<%#DataBinder.Eval(Container.DataItem, "STATUS") %>'>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "TITLE") %>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "AUTHOR") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Image ID="imgStatus" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <asp:HiddenField ID="htxtPreSelPageID" runat="server" />
                    </div>
                    <asp:UpdatePanel ID="upSelPage" runat="server">
                        <ContentTemplate>
                            <div style="margin-top: 10px; text-align: center;">
                                <asp:Button ID="btnSelectPage" runat="server" Text="Select" CssClass="button-yellow" OnClick="SelectPage" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </ext:Window>

            <!-- Select CMS Template -->
            <ext:Window ID="winSelectTemplate" runat="server" Title="Select Template" Icon="Layout"
                AutoHeight="true" Width="630px" BodyStyle="background-color: #fff;" Padding="5"
                Collapsible="false" Modal="true" Hidden="true" IDMode="Static">
                <Content>
                    <asp:UpdatePanel ID="upTemplates" runat="server">
                        <ContentTemplate>
                            <div style="padding: 6px;">
                                <div style="border: 1px solid #e1e1e1; padding: 5px; background-color: #e1e1e1; margin-bottom: 10px;">
                                    <span class="label">Template: </span>&nbsp;<asp:DropDownList ID="cboTemplates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTemplates_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div style="padding: 10px; border: solid 1px #e1e1e1;">
                                    <div class="cms-templates-contents" style="height: 450px; overflow: auto;">
                                        <asp:Literal ID="litTemplateContents" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin:10px 0; text-align: center;">
                                    <asp:Button ID="btnSelTemplate" runat="server" OnClick="btnSelTemplate_OnClick" CssClass="button-yellow" Text="Select" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </ext:Window>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<script>
    $(document).ready(function () {
        setTimeout(function () {
            $('.master-save, [id$="lnkMasterSave"]').attr('onclick', 'return cms.page.setMasterSave();');
        }, 100);
    });
</script>
