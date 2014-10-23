<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReferralClinicMgnt.ascx.cs"
    Inherits="ucReferralClinicMgnt" %>
<%@ Import Namespace="System.Data" %>

<span style="display: block; margin-left: 12px; text-align: left;" class="PageTitle">
    Referral Clinic Management </span>
<div style="margin: 12px; padding: 10px; border: 1px solid #8692c4; text-align: left;">
    <!-- Clinics list grid -->
    <div>
    <% if (bAllowUpdate)
       { %>
        <input type="button" class="button" id="btnAddClinic" runat="server" value="Add Clinic" style="padding: 2px 6px;" onclick="management.clinic.newClinic();" />
        <%} %>
        <div>
            <div style="float: left;">
                <table id="tblClinics" class="mGrid">
                    <thead>
                        <tr>
                            <th>
                                Clinic
                            </th>
                            <th style="width: 60px;">
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repReferralClinic" runat="server" OnItemDataBound="repReferralClinic_OnItemDataBound" >
                            <ItemTemplate>
                                <tr id='trclin_<%#DataBinder.Eval(Container.DataItem, "STAT_REFERRAL_ID") %>'>
                                    <td align="left">
                                        <%#DataBinder.Eval(Container.DataItem, "STAT_REFERRAL_DESC") %>
                                    </td>
                                    
                                    <td>
                                        <div class="grid-read">
                                            <center>
                                                <img alt="Edit Clinic" style="cursor: pointer;" class="edit-row" src="Images/pencil.png" onclick="management.clinic.editClinic(this);" />
                                                <% if (bAllowUpdate)
                                                { %>
                                                <asp:ImageButton ID="btnDeleteClinic" runat="server" ImageUrl="~/Images/delete.png" OnCommand="DeleteReferralClinic" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "STAT_REFERRAL_ID") %>' />
                                                <%} %>
                                            </center>
                                        </div>
                                        <div class="grid-edit">
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div id="divEditClinic" style="float: left; margin-left: 10px; margin-top:5px; padding: 10px; border: 1px solid #e1e1e1;
                background-color: #f1f1f1; display: none;">
                <span id="spNewClinic" style="display: none; text-align: left;" class="PageTitle">
                    New Clinic
                </span>
                <table border="0" cellspacing="0" cellpadding="3" width="100%" style="margin: 10px;">
                    <tr id="trClinicName">
                        <td align="right" class="label" style="width: 100px; padding-right: 10px;">
                            Clinic
                        </td>
                        <td align="left">
                            <span id="spClinicName" style="display:none; font-size:10px; color:Red;">Please enter the clinic's name.</span>
                            <asp:TextBox ID="txtClinic" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Comment
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtComment" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Provider Name
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProviderName" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Address
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAddress" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            City
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCity" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            State
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="cboState" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Postal Code
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPostalCode" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Phone
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="label" style="padding-right: 10px;">
                            Fax
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFax" runat="server" Text='' Width="95%" />
                        </td>
                    </tr>
                </table>
                <div id="divEditButtons">
                    <% if (bAllowUpdate)
                       { %>
                    <center>
                        <asp:Button ID="btnClinicSave" CssClass="button" runat="server" Text="Save" OnClick="btnClinicSave_OnClick" Enabled="false" />&nbsp;<input id="btnClinicCancel" runat="server" type="button" class="button" value="Cancel" onclick="management.clinic.cancelEdit(this);" />
                    </center>
                    <%} %>
                </div>
            </div>
            <div style="clear: both;">
            </div>
        </div>
    </div>
</div>

<%-- JSON string of clinics dataset --%>
<asp:HiddenField ID="htxtClinicData" runat="server" />
<asp:HiddenField ID="htxtReferralID" runat="server" />

<script type="text/javascript">
    Sys.onDomReady(function()
    {
        management.clinic.init();
    });
</script>
