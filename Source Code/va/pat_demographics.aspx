<%@ Page Title="REVAMP Practitioner - Patient Demographics" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pat_demographics.aspx.cs" Inherits="pat_demographics" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="ucPatientPortalAccount" Src="~/ucPatientPortalaccount.ascx" %>
<%@ Register TagPrefix="uc" TagName="ucPAPDevice" Src="~/ucPAPDevice.ascx" %>
<asp:Content ID="cStyle" ContentPlaceHolderID="cpHeader" runat="server">
    <style type="text/css">
        .radio-list input[type="radio"], .checkbox-list input[type="checkbox"] {
            margin-right: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="ctDemographics" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="smpPatDemographics" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Panel ID="pnlPatDemographics" runat="server" DefaultButton="btnNull">
        <% if (lPatInfoRightMode == (long)RightMode.ReadOnly)
           { %>
        <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
            <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                runat="server" />&nbsp; You have <b>Read-Only access</b> to this section.
        </div>
        <%} %>

        <% if (Master.IsPatientLocked)
           { %>
        <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
            <asp:Image ID="Image2" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                runat="server" />&nbsp; The patient's record is in use by <%= Session["PAT_LOCK_PROVIDER"].ToString() %>. 
                <a style="text-decoration: underline; color: Blue;" href='mailto:<%= Session["PAT_LOCK_EMAIL"].ToString() %>'>[Send Email]</a>
        </div>
        <%} %>

        <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width: 95%; margin: 0 auto;">
                    <asp:UpdatePanel ID="upDemographics" runat="server">
                        <ContentTemplate>
                            <asp:TabContainer ID="tabContDemographics" runat="server" Width="100%">
                                <asp:TabPanel ID="lnkPatient" runat="server" HeaderText="Patient" Font-Size="Large" Font-Bold="true">
                                    <ContentTemplate>
                                        <div class="wrapper">
                                            <div style="margin: 5px 0 15px 0;">
                                                <img alt="required icon" src="Images/asterisk_16x16.png"></img>&nbsp;<span style="color: #ba1919;">Required field.</span>
                                            </div>
                                            <div class="container">
                                                <table border="0" cellpadding="0" cellspacing="0" class="tbl-demographics">
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Initial" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblFMPSSN" runat="server" Text="SSN" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFMPSSN" runat="server" MaxLength="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblFMPSSN_Confirm" runat="server" Text="SSN Confirmation" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFMPSSN_Confirm" runat="server" MaxLength="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblAddress1" runat="server" Text="Address1" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblAddress2" runat="server" Text="Address2" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblCity" runat="server" Text="City" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblState" runat="server" Text="State" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="cboState" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <asp:Label ID="lblPostCode" runat="server" Text="Postal Code" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="height: 5px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label soft-frame">
                                                            <span class="label">Home Phone</span><br />
                                                            <span style="font-size: 10px; color: #808080;">(including area code)</span>
                                                        </td>
                                                        <td class="soft-frame" align="left">
                                                            <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="height: 5px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label soft-frame">
                                                            <span class="label">Cell Phone</span><br />
                                                            <span style="font-size: 10px; color: #808080;">(including area code)</span>
                                                        </td>
                                                        <td class="soft-frame" align="left">
                                                            <asp:TextBox ID="txtCelPhone" runat="server" MaxLength="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="soft-frame">
                                                            <div style="text-align: left; margin-left: 128px;">
                                                                Is it OK to receive text messages?
                                                                <asp:RadioButtonList ID="rblHomePhoneMsg" runat="server" CssClass="radiobutton" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="height: 5px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label soft-frame">
                                                            <span class="label">Work Phone</span><br />
                                                            <span style="font-size: 10px; color: #808080;">(including area code)</span>
                                                        </td>
                                                        <td class="soft-frame" align="left">
                                                            <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="height: 5px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Which phone number would you like us to use to contact you?<br />
                                                            <asp:DropDownList ID="cboCallPreference" runat="server">
                                                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="">Home</asp:ListItem>
                                                                <asp:ListItem Value="2" Text="">Cell</asp:ListItem>
                                                                <asp:ListItem Value="4" Text="">Work</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="container">
                                                <table border="0" cellpadding="0" cellspacing="0" class="tbl-demographics">
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth (MM/DD/YYYY)" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDateOfBirth" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblGender" runat="server" Text="Gender" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="cboGender" runat="server" Width="155px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" class="cell_label">
                                                            <asp:Label ID="lblEthnicity" runat="server"
                                                                CssClass="label"
                                                                Text="Ethnicity">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RadioButtonList ID="rblEthnicity" runat="server" CssClass="radio-list">
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" class="cell_label">
                                                            <asp:Label ID="lblRace" runat="server"
                                                                CssClass="label"
                                                                Text="Race">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBoxList ID="cblRace" runat="server" CssClass="checkbox-list">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" class="cell_label">
                                                            <asp:Label ID="lblSource" runat="server"
                                                                CssClass="label"
                                                                Text="Ethnicity/Race Source">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RadioButtonList ID="rblSource" runat="server" CssClass="radio-list">
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label">
                                                            <img alt="required icon" src="Images/asterisk_8x8.png">&nbsp;
                                                            <asp:Label ID="lblProvider" runat="server" Text="Sleep Specialist" CssClass="label"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="cboProvider" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="height: 5px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cell_label soft-frame">
                                                            <span class="label">Email</span><br />
                                                        </td>
                                                        <td class="soft-frame" align="left">
                                                            <asp:TextBox ID="txtPatEmail" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="soft-frame">
                                                            <div style="text-align: left; margin-left: 128px;">
                                                                OK to receive emails?
                                                                <asp:RadioButtonList ID="rblEmailMessage" runat="server" CssClass="radiobutton" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="wrapper">
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="lnkContact" runat="server" HeaderText="Emergency Contact" Font-Size="Large"
                                    Font-Bold="true">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="3" cellspacing="4">
                                            <tr>
                                                <td align="right" width="135px">
                                                    <asp:Label ID="lblEmergencyName" runat="server" Text="Emergency Contact" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyName" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyWPhone" runat="server" Text="Work Phone" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyWPhone" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyHPhone" runat="server" Text="Cell/Home Phone" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyHPhone" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyAddress" runat="server" Text="Street Address" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyAddress1" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyCity" runat="server" Text="City" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyCity" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyState" runat="server" Text="State" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="cboEmergencyState" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyPostCode" runat="server" Text="Postal Code" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmergencyPostCode" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmergencyRelationship" runat="server" Text="Relationship" CssClass="label"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="cboEmergencyRelationship" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="lnkPatientPortalAccount" runat="server" HeaderText="Portal Account"
                                    Font-Size="Large" Font-Bold="true">
                                    <ContentTemplate>
                                        <div style="text-align: left;">
                                            <uc:ucPatientPortalAccount Visible="true" ID="ucPatientPortalAccount" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="lnkCPAPMachine" runat="server" HeaderText="PAP Machine" Font-Size="Large" Font-Bold="true" Enabled="false">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="htxtDevicePatient" runat="server" />
                                        <div style="text-align: left; padding: 10px;">
                                            <uc:ucPAPDevice ID="ucPAPDevice" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            <input type="hidden" id="htxtPatAddress" runat="server" />
                            <input type="hidden" id="htxtPatDemo" runat="server" enableviewstate="true" />
                            <asp:HiddenField ID="htxtCurrTab" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- 2011-09-03: D.S. Set null button as default --%>
        <div style="display: none;">
            <asp:Button ID="btnNull" CssClass="button" runat="server" UseSubmitBehavior="false"
                OnClientClick="return false;" />
        </div>
    </asp:Panel>


</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="cpScripts" runat="server">
    <script src="js/patient/patient.js" type="text/javascript"></script>
    <script type="text/javascript">
        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        patient.device.init();

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {
            dp();
            patient.device.init();

            autoAdjustMainDiv();
            $('div.ajax-loader').hide();

            restartSessionTimeout();
        }

        //enable the date-picker for all
        //text boxes with the css class 'date-picker'
        function dp() {
            $('.date-picker').datepicker({
                beforeShow: function () {
                    $(document).ready(function () {
                        setTimeout(function () {
                            wp.adjustMain();
                        }, 1);
                    });
                }
            });
        }

        function autoAdjustMainDiv() {

            $(document).ready(function () {
                setTimeout(function () {
                    origWidth = $('input[id$="htxtMainDivWidth"]').val();
                    wp.adjustMain();

                    if ($('div[id$="mainContents"]').css('width') != origWidth) {
                        $('div[id$="mainContents"]').css({
                            width: origWidth
                        });
                        $('input[id$="htxtMainDivWidth"]').val(origWidth);
                    }
                    $('input[type="button"], input[type="submit"]').css({
                        padding: '1px 6px'
                    });
                }, 1);
            });
        }

        dp();

        function onlyNumbers(obj) {
            var val = obj.value.replace(/\D|-/gi, '');
            obj.value = val;
        }
    </script>

</asp:Content>
