<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUserAdmin.ascx.cs" Inherits="ucUserAdmin" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Import Namespace="System.Data" %>
<span id="sTitle" style="text-align: left; padding-left: 10px; margin-bottom: 5px; float:left;" class="PageTitle">
    User Administration</span>
<div id="dUserAdmin" style="text-align: left; padding: 0px 10px 10px 10px; width: 95%; clear: both;">
    
    <asp:UpdatePanel ID="upUserAdmin" runat="server">
        <ContentTemplate>
            <div align="left" id="divStatus" runat="server" style="margin-bottom: 15px; margin-left:330px; padding:5px; font-weight:bold;"></div>
            <table id="tblUserAdmin" width="100%" border="0">
                <tr>
                    <!-- users list -->
                    <td width="310px" align="center" valign="top">
                        <ext:Toolbar ID="tbUserToolbar" runat="server" Width="310" EnableTheming="false">
                            <Items>
                                <ext:Button ID="btnAddUser" runat="server" Icon="Add" Text="Add">
                                    <Listeners>
                                        <Click Handler="admin.user.addUser();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server">
                                </ext:ToolbarSpacer>
                                <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </ext:ToolbarSeparator>
                                <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server">
                                </ext:ToolbarSpacer>
                                <ext:TextField ID="txtUsrSearch" IDMode="Static" runat="server">
                                </ext:TextField>
                                <ext:Button ID="btnSearchUser" IDMode="Static" runat="server" Icon="Magnifier">
                                    <Listeners>
                                        <Click Handler="admin.user.search();" />
                                    </Listeners>
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" IDMode="Static" runat="server" Html="Search User" />
                                    </ToolTips>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="ToolbarSeparator2" />
                                <ext:Button ID="btnShowAll" runat="server" Icon="Group" Text="Show All">
                                    <Listeners>
                                        <Click Handler="admin.user.showAll();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                        <div>
                            <div id="divUsersList" style="overflow: auto; text-align:left;">
                                <table id="tblUsersList" border="0" width="100%" class="mGrid">
                                    <tbody>
                                        <asp:Repeater ID="repUsersList" runat="server">
                                            <ItemTemplate>
                                                <tr id='tr_<%#DataBinder.Eval(Container.DataItem, "FX_USER_ID") %>' class="sel-row"
                                                    onclick="admin.user.select(this);">
                                                    <td align="left">
                                                        <%#DataBinder.Eval(Container.DataItem, "NAME") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </td>
                    <!-- users form -->
                    <td width="*" valign="top" style="padding-left: 20px;">
                        <asp:TabContainer ID="tcUserAdmin" runat="server" Width="100%" >
                            <asp:TabPanel ID="tpUserDetails" runat="server" HeaderText="Details">
                                <ContentTemplate>
                                    <div style="padding: 10px;">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <table border="0">
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblName" runat="server" Text="Name" CssClass="label"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtName" Style="width: 280px;" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="label"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTitle" Style="width: 280px;" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblPhone" runat="server" Text="Phone" CssClass="label"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPhone" Style="width: 280px;" runat="server" MaxLength="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="label"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtEmail" Style="width: 280px;" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblSite" runat="server" Text="Clinic" CssClass="label"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="cboSite" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <div style="margin-left:10px; width:auto;">
                                                    <fieldset>
                                                        <legend>User Account</legend>
                                                        <div style="padding: 12px;">
                                                            <asp:UpdatePanel ID="upUserAdminPass" runat="server">
                                                                <ContentTemplate>
                                                                    <div>
                                                                        <div id="divUsername" runat="server">
                                                                            <table cellspacing="5">
                                                                                <tr>
                                                                                    <td class="cell_label">
                                                                                        <asp:Label ID="lblUserId" runat="server" Text="User Name" CssClass="label"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="chkResetPasswd" runat="server" />&nbsp;<asp:Label ID="lblResetPasswd"
                                                                                            runat="server" AssociatedControlID="chkResetPasswd" Text="Reset Password"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div id="divPassword" runat="server">
                                                                            <table cellspacing="5">
                                                                                <tr>
                                                                                    <td class="cell_label">
                                                                                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="label"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="cell_label">
                                                                                        <asp:Label ID="lblVerifyfPassword" runat="server" Text="Verify Password" CssClass="label"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txtVerifyPassword" TextMode="Password" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <table>
                                                                            <tr>
                                                                                <td class="cell_label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkbxAccountLocked" Text="&nbsp;Locked" runat="server" CssClass="label" />&nbsp;&nbsp;
                                                                                    <asp:CheckBox ID="chkbxAccountInactive" Text="&nbsp;Inactive" runat="server" CssClass="label" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                        
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="tpUserRights" runat="server" HeaderText="Rights">
                                <ContentTemplate>
                                    <div style="padding: 10px;">
                                        <table border="0">
                                            <tr>
                                                <th style="font-weight: bold; color: rgb(7, 76, 131);">
                                                    <u>User Types</u>
                                                </th>
                                                <th style="width: 20px;">
                                                    &nbsp;
                                                </th>
                                                <th style="font-weight: bold; color: rgb(7, 76, 131);">
                                                    <u>User Rights</u>
                                                </th>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblUserType" runat="server">
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table border="0" class="tbl-rights">
                                                        <asp:Repeater ID="repUserRights" runat="server" OnItemDataBound="repUserRights_OnItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <input type="checkbox" id='chkUsrRight' runat="server" name="chkUsrRight" value='<%#DataBinder.Eval(Container.DataItem, "RIGHT_DEC") %>'
                                                                            onclick="admin.user.checkReadOnly(this);" /><asp:Label ID="lblUsrRight" AssociatedControlID="chkUsrRight"
                                                                                runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div id="divMode" runat="server" style="display: none; padding-left: 10px;" visible="true">
                                                                            <input type="checkbox" id='chkReadOnly' runat="server" name="chkReadOnly" value='<%#DataBinder.Eval(Container.DataItem, "RIGHT_DEC") %>' />
                                                                            <label id="lblRO" runat="server"><asp:Image runat="server" ImageUrl="~/Images/lock16x16.png" ID="imgROLock" Height="12px" Width="12px" ToolTip="Read-Only Mode" /></label>
                                                                            <asp:Label ID="lblReadOnly" runat="server" AssociatedControlID="chkReadOnly" Font-Size=".9em"
                                                                                ForeColor="#909090" Text="Read Only" Visible="false" ></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div style="display:block; width:200px; margin-left:8px; font-size:.9em; color: Navy;">
                                                                            <asp:Label ID="lblRightComment" runat="server"></asp:Label> 
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    
                                                    <div style="margin:5px 5px 5px 0; padding:10px; border:1px solid #000; background-color:#f1f1f1;">
                                                        <asp:UpdatePanel ID="upRightsTemplate" runat="server">
                                                            <ContentTemplate>
                                                                <a href="#" onclick="admin.user.fillRightsTemplate();" style="display:block; margin-bottom:5px;">Load User Type template</a>
                                                                <asp:LinkButton ID="lnkSaveTemplate" runat="server" Text="Save as User Type template"
                                                                    OnClick="SaveRightsTemplate"></asp:LinkButton><br />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="htxtUserData" runat="server" />
            <asp:HiddenField ID="htxtFxUserID" runat="server" />
            <asp:HiddenField ID="htxtProviderID" runat="server" />
            <asp:HiddenField ID="htxtSelUsrType" runat="server" />
            <asp:HiddenField ID="htxtRightsTemplate" runat="server" />
            <asp:HiddenField ID="htxtSavedTemplate" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">    
    
        var _userData = null;
        $(document).ready(function()
        {
            $('div[id$="divUsersList"]').css({ height: $('div[id$="div-page-contents"]').height() - 63 });
        });

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args)
        {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args)
        {
            _userData = admin.user.buildUserDataObject();
            admin.user.init();

            $(document).ready(function()
            {
                $('div[id$="divUsersList"]').css({ height: $('div[id$="div-page-contents"]').height() - 63 });
            });

            $('div.ajax-loader').hide();

            restartSessionTimeout();
        }

        _userData = admin.user.buildUserDataObject();
        admin.user.init();

    </script>
    
</div>
