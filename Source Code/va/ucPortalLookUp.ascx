<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPortalLookUp.ascx.cs" Inherits="ucPortalLookUp" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Import Namespace="System.Data" %>
<span id="sTitle" style="text-align: left; padding-left: 10px; margin-bottom: 5px; float:left;" class="PageTitle">
    Patient Portal Lookup</span>
<div id="dPatPortal" style="text-align: left; padding: 0px 10px 10px 10px; width: 95%; clear: both;">
    
    <asp:UpdatePanel ID="upPatPortal" runat="server">
        <ContentTemplate>
            <div align="left" id="divStatus" runat="server" style="margin-bottom: 15px; margin-left:330px; padding:5px; font-weight:bold;"></div>
            <table id="tblPatientPortal" width="100%" border="0">
                <tr>
                    <!-- users list -->
                    <td width="310px" align="center" valign="top">
                                <!-- ********************************************************************* -->
                                <!-- ********************************************************************* -->
                                <!-- ********************************************************************* -->
                                <ext:GridPanel ID="gpPatPortalAccLookup" runat="server" StripeRows="true" TrackMouseOver="true" Height="500" >
                                    <Store>
                                        <ext:Store ID="storePatAccount" runat="server">
                                            <Reader>
                                                <ext:JsonReader>
                                                    <Fields>
                                                        <ext:RecordField Name="fx_user_id" />
                                                        <ext:RecordField Name="patient_id" />
                                                        <ext:RecordField Name="last_name" />
                                                        <ext:RecordField Name="first_name" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="cmPatAccounts" runat="server">
                                        <Columns>
                                            <ext:Column Header="Last Name" DataIndex="last_name" Width="140" />
                                            <ext:Column Header="First Name" DataIndex="first_name" Width="140" />
                                        </Columns>
                                    </ColumnModel>
                                    <LoadMask ShowMask="true" />
                                    <Plugins>
                                        <ext:GridFilters runat="server" ID="gfPatAccounts" Local="true">
                                            <Filters>
                                                <ext:StringFilter DataIndex="last_name" />
                                                <ext:StringFilter DataIndex="first_name" />
                                            </Filters>
                                        </ext:GridFilters>
                                    </Plugins>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rsmPatAccount" runat="server" SingleSelect="true">
                                            <Listeners>
                                                <RowSelect Handler="portal.user.selectPat(#{gpPatPortalAccLookup}.selModel.getSelected().data.patient_id)" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                </ext:GridPanel>

                                <!-- ********************************************************************* -->
                                <!-- ********************************************************************* -->
                                <!-- ********************************************************************* -->
                    </td>
                    <!-- users form -->
                    <td width="*" valign="top" style="padding-left: 20px;">
                        <asp:TabContainer ID="tcPatPortal" runat="server" Width="100%" >
                            <asp:TabPanel ID="tpPatDetails" runat="server" HeaderText="Details">
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
                                                            <asp:TextBox ID="txtName" Style="width: 280px;" runat="server" ReadOnly="true" EnableViewState="true"></asp:TextBox>
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
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <div style="margin-left:10px; width:auto;">
                                                    <fieldset>
                                                        <legend>Portal Account</legend>
                                                        <div style="padding: 12px;">
                                                            <asp:UpdatePanel ID="upPatPortalPass" runat="server">
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
                        </asp:TabContainer>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="htxtUserData" runat="server" />
            <asp:HiddenField ID="htxtPatientData" runat="server" />
            <asp:HiddenField ID="htxtFxUserID" runat="server" />
            <asp:HiddenField ID="htxtPatientID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">
    
        var _patientData = null;
        $(document).ready(function()
        {
            $('div[id$="divPatList"]').css({ height: $('div[id$="div-page-contents"]').height() - 63 });
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
            _patientData = portal.user.buildPatientDataObject();
            portal.user.init();

            $(document).ready(function()
            {
                $('div[id$="divPatList"]').css({ height: $('div[id$="div-page-contents"]').height() - 63 });
            });

            $('div.ajax-loader').hide();

            restartSessionTimeout();
        }

        _patientData = portal.user.buildPatientDataObject();
        portal.user.init();

    </script>
    
</div>
