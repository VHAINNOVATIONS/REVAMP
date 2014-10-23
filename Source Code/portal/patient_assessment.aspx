<%@ Page Title="REVAMP Portal - Questionaires" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="patient_assessment.aspx.cs" Inherits="patient_assessment" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    <style>
        div.module-group {
			margin: 5px 5px 5px 200px;
		}
		
		div.module-group h1, h2, h3, h4 {
			color: #003399;
		}

        div.module-group h2 {
			font-size: 16px;
            margin-bottom: 12px;
		}
		
		div.module-group table tr td:first-child {
			width: 20px;
            padding-right: 10px;
		}
		
		div.module-group tr.pending:hover {
			background-color: #FFFF99;
		}

        div.module-group table
        {
	        font-family: Sans-Serif;
	        font-size: 13px;
	        background: #fff;
	        border-collapse: collapse;
	        text-align: left;
        }
        
        div.module-group table th
        {
	        font-size: 14px;
	        font-weight: normal;
	        color: #039;
	        padding: 10px 8px;
	        border-bottom: 2px solid #6678b1;
        }
        
        div.module-group table td
        {
	        border-bottom: 1px solid #ccc;
	        color: #404040;
	        padding: 6px 8px;
        }

        div.module-group table td a
        {
	        color: blue;
	        text-decoration: underline;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lbl1" runat="server"></asp:Label>
    <div class="PageTitle" style="margin-bottom: 20px;">
        Questionnaires
    </div>
    <asp:Panel ID="pnlInstructions" runat="server">
        <div style="width: 95%; margin: 10px auto; padding: 5px; border: 1px solid #e1e1e1; background-color: #f2f0d0; line-height: 155%;">
             <p>Please complete the questionnaires listed below.  Click on each questionnaire to open it. Once you have completed a questionnaire, a green check mark will appear next to it. You will need to complete all of the questionnaires before you can proceed further. If you need to stop before completing all of the questionnaires, you can come back later and complete the rest. However, before stopping, you must complete whatever particular questionnaire you are working on. Your responses will be NOT be saved if you stop in the middle of a questionnaire.</p>
            <p>
                Once you have submitted a questionnaire you cannot change your responses.
            </p>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upUpdatePatInfo" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlPatDetails" runat="server">
                <div style="margin-left: 200px; margin-bottom: 20px;">
                    <div>
                        <div style="float: left;">
                            <table class="mGrid" style="width: 450px;">
                                <thead>
                                    <tr>
                                        <th style="width: 50%;">Height</th>
                                        <th>Weight</th>
                                        <th style="width: 60px;">&nbsp</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="trReadInfo" runat="server">
                                        <td>
                                            <asp:Label ID="lblPatHeight" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPatWeight" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="imgbtnShowEditRow" runat="server" OnClick="ShowEditRow" ImageUrl="~/Images/pencil.png" />
                                        </td>
                                    </tr>
                                    <tr id="trEditInfo" runat="server">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="cboFeet" runat="server">
                                                            <asp:ListItem Value="-1" Text="-please select-"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; feet
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="cboInches" runat="server">
                                                            <asp:ListItem Value="-1" Text="-please select-"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; inches
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="htxtHeightFeet" runat="server" />
                                            <asp:HiddenField ID="htxtHeightInches" runat="server" />
                                            <asp:HiddenField ID="htxtWeightPounds" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWeight" runat="server" Columns="4" MaxLength="3"></asp:TextBox>&nbsp;pounds
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="btnUpdateInfo" runat="server" ImageUrl="~/Images/disk.png" OnClick="btnUpdateInfo_Click" />&nbsp;
                            <asp:ImageButton ID="btnCancelUpdate" runat="server" ImageUrl="~/Images/cross.png" OnClick="CancelEditInfo" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div style="float: left; padding-top: 6px;">
                            <img alt="Edit weight and height" title="Edit weight and height" style="margin-left:5px; float: left; width: 20px; height: 20px; cursor: pointer;" src="images/help-icon.png" onclick="$('#hlpMessages').toggle();" />
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <div id="hlpMessages" style="display: none; width: 95%; margin: 10px auto; padding: 5px; border: 1px solid #e1e1e1; background-color: #f2f0d0; line-height: 155%;">
        <p>Please select your height from the drop down height fields and enter your current weight in the weight field.  Please click on the floppy disk <img alt="floppy save" src="Images/disk.png"> on the right of the weight to save this information.</p>
        <p>
            Once the information has been saved, you can click on the pencil <img alt="pencil edit" src="Images/pencil.png"> that will appear on the right of the weight to modify the height/weight.  Click on the floppy disk <img alt="floppy save" src="Images/disk.png"> on the right to save the changes or click on the <img alt="X" src="Images/cross.png"> to cancel any changes. 
        </p>
        <div style="text-align: center;">
            <a href="#" style="color: blue; text-decoration: underline;" onclick="$('#hlpMessages').hide();">[X] Close</a>
        </div>
    </div>

    <div id="divModuleGroups" runat="server"></div>
</asp:Content>

<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScripts">
    <script>
        var hasWightHeight = <%= HasWeightHeight.ToString().ToLower() %>,
            chkWeightHeight = function () {
            $('a', $('div[id$="divModuleGroups"]')).each(function (a, b) {
                var reMID = /mid3002/gi,
                    m_href = $(b).attr('href'),
                    msg = 'The MAP Sleep Symptom-Frequency Questionaire cannot be filled out until you have entered your height and weight at the top of the page.\n\nPlease enter and save your height and weight.';

                if (reMID.test(m_href)) {
                    $(b).bind({
                        click: function () {
                            if(!hasWightHeight){
                                alert(msg);
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    });
                }
            });
        };

        $(document).ready(function () {
            setTimeout(function () {
                chkWeightHeight();
            }, 1);
        });

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {

            $(document).ready(function () {
                setTimeout(function () {
                    chkWeightHeight();
                }, 1);
            });

            $('div.ajax-loader').hide();
            $('input[type="button"], input[type="submit"]').css({ padding: 'px 6px' });

            restartSessionTimeout();
        }

    </script>
</asp:Content>