<%@ Page Title="REVAMP Portal - Portal Login" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/MasterPageLogin.master" %>
<%@ Register TagPrefix="ucLogin" TagName="ucLogin" Src="~/ucLogin.ascx" %>

<asp:Content ID="cntHeader" ContentPlaceHolderID="cpHeader" runat="server">
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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <% if(bShowAlert){ %>

    <script type="text/javascript">
        var msg = "This system is intended to be used by [authorized VA network users] for viewing and retrieving information only except as otherwise explicitly authorized. VA information resides on and transmits through computer systems and networks funded by VA; all use is considered to be understanding and acceptance that there is no reasonable expectation of privacy for any data or transmissions on Government Intranet or Extranet (non-public) networks or systems. All transactions that occur on this system and all data transmitted through this system are subject to review and action including (but not limited to) monitoring, recording, retrieving, copying, auditing, inspecting, investigating, restricting access, blocking, tracking, disclosing to authorized personnel, or any other authorized actions by all authorized VA and law enforcement personnel. All use of this system constitutes understanding and unconditional acceptance of these terms.  Unauthorized attempts or acts to either (1) access, upload, change, or delete information on this system, (2) modify this system, (3) deny access to this system, or (4) accrue resources for unauthorized use on this system are strictly prohibited. Such attempts or acts are subject to action that may result in criminal, civil, or administrative penalties.";

        alert(msg);
    </script>

    <% } %>

    <div style="height: 30px;">
        &nbsp;</div>
    <table style="width:100%;">
        <tbody>
            <tr>
                <td style="padding: 4px; vertical-align: top;">
                    <div style =" text-align:center;">
                        <span style="font-weight: bold; color: #9f0022; display:block; margin-bottom: 10px;">
                            Welcome to REVAMP, the Remote Veteran Apnea Management Portal!
                        </span>
                    </div>
                    <div style="width:100%;">
                        <div style="float:left; width:47%; margin-right: 10px; padding-right:10px; border-right:1px solid #e1e1e1; text-align: left;">
                            <p style="line-height: 150%;">
                                Remote Veterans Apnea Management Portal (REVAMP) is a new way to help veterans manage obstructive sleep apnea, a common breathing disorder. Patients with obstructive sleep apnea will stop breathing while they sleep and this can contribute to a variety of medical problems. Once diagnosed and treated, however, patients often feel better and are able to live healthier lives. You are being offered the opportunity to participate in REVAMP because your VA healthcare provider believes you may have obstructive sleep apnea.. By participating in REVAMP, you will receive this evaluation at no cost. 
                            </p>
                        </div>
                        <div style="float:right; width:49%;">
                            <p style="line-height: 150%;">
                                REVAMP will allow you to obtain sleep testing and treatment without having to visit a sleep center.  REVAMP coordinators and sleep specialists will be available to guide and assist you throughout your evaluation and treatment. The following pages contain information about becoming a participant in REVAMP. Continuing with this program is completely voluntary and you may leave the website at any time. 
                            </p>
                            <p style="margin-top: 15px; line-height: 150%;">
                                 To proceed, login to the website by entering 
                                 <br />
                                 your Usename and Password below and click 
                                 <br />
                                 the Login box. If you have forgotten your password, 
                                 <br />
                                 you can click “<a href="reset_password.aspx" style="text-decoration: underline;">Reset Password</a>” for help.
                            </p>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </td>
                <td style="width: 240px; padding: 4px; vertical-align: top;">
                    <div style="margin-right: 5px; padding: 10px; border: 1px solid #9f0022; background: #fff;">
                        <span style="display: block; margin-bottom: 15px; font-family: Arial, Helvetica, sans-serif;
                            color: #9f0022; font-size: 20px;">LOGIN</span>
                        <asp:UpdatePanel ID="upLogin" runat="server">
                            <ContentTemplate>
                                <ucLogin:ucLogin ID="ucLogin" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
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

                                <select id="selBrowser" onchange="sysreq.updateBrowser(this);" >
                                    <option value="0"> --Select browser-- </option>
                                    <option value="1">Internet Explorer</option>
                                    <option value="2">Chrome</option>
                                    <option value="3">Firefox</option>
                                    <option value="4">Safari</option>
                                </select>&nbsp;
                                <a id="lnkUpdateBrowser" href="#" target="_blank" class="button-yellow" onclick="return false;">Update</a>
                            </div>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script>
        $(window).bind({
            load: function () {
                $('input[id$="txtU"]').focus();
            }
        });
    </script>
</asp:Content>
