<%@ Page Title="REVAMP Practitioner - Login Page" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPageLogin.master" %>
<%@ Register TagPrefix="ucLogin" TagName="ucLogin" Src="~/ucLogin.ascx" %>
<asp:Content ID="cHeader" ContentPlaceHolderID="cpHeader" runat="server">
    <style>
        fieldset.requirements {
            BORDER-RIGHT: #e85922 0px solid;
            PADDING-RIGHT: 10px;
            BORDER-TOP: #e85922 4px solid;
            PADDING-LEFT: 10px;
            BACKGROUND: #eeeeee;
            PADDING-BOTTOM: 10px;
            MARGIN: 10px 10px 30px;
            BORDER-LEFT: #e85922 0px solid;
            PADDING-TOP: 10px;
            BORDER-BOTTOM: #e85922 1px solid;
        }

            fieldset.requirements legend {
                font-size: 11px;
                font-weight: bold;
            }

            fieldset.requirements div, fieldset.requirements p {
                font-size: 11px;
            }

            fieldset.requirements p {
                margin-bottom: 15px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    
    <% if(bShowAlert){ %>

    <script type="text/javascript">
        var msg = "This system is intended to be used by [authorized VA network users] for viewing and retrieving information only except as otherwise explicitly authorized. VA information resides on and transmits through computer systems and networks funded by VA; all use is considered to be understanding and acceptance that there is no reasonable expectation of privacy for any data or transmissions on Government Intranet or Extranet (non-public) networks or systems. All transactions that occur on this system and all data transmitted through this system are subject to review and action including (but not limited to) monitoring, recording, retrieving, copying, auditing, inspecting, investigating, restricting access, blocking, tracking, disclosing to authorized personnel, or any other authorized actions by all authorized VA and law enforcement personnel. All use of this system constitutes understanding and unconditional acceptance of these terms.  Unauthorized attempts or acts to either (1) access, upload, change, or delete information on this system, (2) modify this system, (3) deny access to this system, or (4) accrue resources for unauthorized use on this system are strictly prohibited. Such attempts or acts are subject to action that may result in criminal, civil, or administrative penalties.";

        alert(msg);
    </script>

    <% } %>

    <div style="width: auto;">
        <table border="0" class="login-tbl" style="width: 850px;">
            <tr>
                <td style="width: 240px;">
                    <div style="padding: 5px;">
                        <img alt="revamp logo" src="Images/revamp_logo_lg.png" />
                    </div>
                </td>
                <td style="width: 10px;"></td>
                <td style="width: 600px; background-color: #3e657c;">
                    <div style="padding: 5px; text-align: right;">
                        <img alt="va logo" src="Images/va_logo_lg.png" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div style="padding: 5px; height: 360px; background-color: #bdccd4;">
                        <div class="lbl-round-right">LOGIN</div>
                        <asp:UpdatePanel ID="upLoginForm" runat="server">
                            <ContentTemplate>
                                <div class="login-form" style="margin: 0 15px 0 0;">
                                    <ucLogin:ucLogin ID="ucLogin" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td style="width: 10px;"></td>
                <td style="vertical-align: top;">
                    <div style="height: 370px;">
                        <img alt="revamp main banner" src="Images/main_banner.jpg" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div style="padding: 5px; height: 20px; background-color: #9f0022;">
                    </div>
                </td>
                <td style="width: 10px;"></td>
                <td style="vertical-align: top;">
                    <div style="padding: 5px; height: 20px; background-color: #3e657c;">
                    </div>
                </td>
            </tr>
        </table>
        <div>
            <fieldset class="requirements">
                <legend>System Requirements</legend>
                <div>
                    <p>
                        This website is designed to work with Internet Explorer 9 or greater, Chrome 21.0.1180 or greater, Firefox 3.5 or greater, Safari 5 or greater.
                    </p>
                    <p>
                        If you want to update your computer browser, select one from the dropdown below and then click the "Update" button. A new window will appear with more details on how to download and install the browser on your computer. 
                    </p>

                    <select id="selBrowser" onchange="sysreq.updateBrowser(this);">
                        <option value="0">--Select browser-- </option>
                        <option value="1">Internet Explorer</option>
                        <option value="2">Chrome</option>
                        <option value="3">Firefox</option>
                        <option value="4">Safari</option>
                    </select>&nbsp;
                    <a id="lnkUpdateBrowser" href="#" target="_blank" class="button-yellow" onclick="return false;">Update</a>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

