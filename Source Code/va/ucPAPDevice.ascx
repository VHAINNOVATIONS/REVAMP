<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPAPDevice.ascx.cs" Inherits="ucPAPDevice" %>
<link href="css/RevampStyleSheet.css" rel="stylesheet" />
<style>
    input[type="radio"], label {
        margin-right: 5px;
    }

    span.label {
        margin-right: 10px;
    }
</style>
<table >
    <tr>
        <td style="vertical-align: top;">
            <fieldset id="fsDevice">
                <legend>Device</legend>
                <div style="padding: 15px;">
                    <table>
                        <tr>
                            <td class="cell_label">
                                <span class="label">Serial Number:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSerialNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label">
                                <span class="label">Type of Unit:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUnitType" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblPAPType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="APAP"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="CPAP"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label">
                                <span class="label">Manufacturer:</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPAPManufacturer" runat="server">
                                    <asp:ListItem Value="1" Text="Philips"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="ResMed"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 8px 0;">
                                <hr />
                                <h3>PAP Pressure Range</h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label">
                                <span class="label">Low Pressure:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLowPressure" runat="server" Columns="4" MaxLength="2"></asp:TextBox>
                                <span style="font-size: 10px; color: #808080;">&nbsp;(4 - 20)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label">
                                <span class="label">High Pressure:</span><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtHighPressure" runat="server" Columns="4" MaxLength="2"></asp:TextBox>
                                <span style="font-size: 10px; color: #808080;">&nbsp;(4 - 20)</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 8px 0;">
                                <hr />
                                <h3>Mask Interface</h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label" style="vertical-align: top;">
                                <span class="label">Mask Type:</span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblMaskType" runat="server">
                                    <asp:ListItem Value="Nasal" Text="Nasal"></asp:ListItem>
                                    <asp:ListItem Value="Full Face" Text="Full Face"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_label" style="vertical-align: top;">
                                <span class="label">Details:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaskDetails" runat="server" TextMode="MultiLine" Rows="6"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </td>
        <td style="padding-left: 20px; vertical-align: top;">
            <fieldset id="fsSleepStudy">
                <legend>HOME SLEEP TEST</legend>
                <div style="padding: 15px;">
                    <table>
                        <tr>
                            <td>
                                <span class="label">Date of Study:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudyDate" runat="server" CssClass="date-picker" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="label">Baseline AHI:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBaselineAHI" runat="server" Columns="6" MaxLength="3"></asp:TextBox>
                                <span style="font-size: 10px; color: #808080;">&nbsp;(0 - 200)</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </td>
    </tr>
</table>
<asp:HiddenField Value="" ID="hfHiddenConfirm" runat="server" />
