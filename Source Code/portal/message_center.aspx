<%@ Page Title="REVAMP Portal - Message Center" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="message_center.aspx.cs" ValidateRequest="false" Inherits="message_center" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="ucMsg" TagName="ucMsg" Src="~/ucMessagesRead.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    
    <link href="js/messages/textboxlist/css/TextboxList.css" rel="stylesheet" />
    <link href="js/messages/textboxlist/css/TextboxListAutocomplete.css" rel="stylesheet" />
    <style>

        #hlpMessages p {
           margin-bottom: 10px;
        }

        #hlpMessages ul {
           list-style: disc;
           margin-left: 45px;
        }
        
        #divMsgBody {
            font-family: 'Times New Roman', serif;
        }

        #divMsgBody p {
            margin-bottom: 8px;
        }

        tr.unread td {
            font-weight: bold;
            color: #377bce;
        }

        .selectedMsg {
            background-color: #E6EEF2;
        }

        .chklist input[type="checkbox"] {
            margin-right: 8px;
        }

        span.message-recipients {
            font-family: monospace;
            color: #377bce;
        }

        .form_tags { margin-bottom: 10px; }

		/* Setting widget width example */
		.textboxlist { width: 482px; 
                       height: 100px;
                       overflow: auto;
		}

		/* Preloader for autocomplete */
		.textboxlist-loading { background: url('images/spinner.gif') no-repeat 380px center; }

		/* Autocomplete results styling */
		.form_friends .textboxlist-autocomplete-result { overflow: hidden; zoom: 1; }
		.form_friends .textboxlist-autocomplete-result img { float: left; padding-right: 10px; }

		.note { color: #666; font-size: 90%; }
		#footer { margin: 50px; text-align: center; }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div class="PageTitle" style="margin-bottom:10px; float:left;">Messages Center</div>
        <img alt="How to use the Message Center" title="How to use the Message Center" style="margin-left:15px; float: left; width: 20px; height: 20px; cursor: pointer;" src="images/help-icon.png" onclick="$('#hlpMessages').toggle();" />
        <div style="clear: both;"></div>
        </div>
    
    <div id="hlpMessages" style="display: none; width: 95%; margin: 10px auto; padding: 5px; border: 1px solid #e1e1e1; background-color: #f2f0d0; line-height: 155%;">
        <p>This page allows you to send and receive messages. To compose a message, click the &#8220;<b>Compose</b>&#8221; 
            box on the left side of the screen. In the new window that appears, click &#8220;<b>Select Provider(s)</b>&#8221; 
            and check the people to whom you want to send a message. The name of your sleep specialist is on your My Profile page. 
            Once you have typed your message, click the <b>Send</b> button.</p>

        <ul>
            <li>In <b>Inbox</b> tab will show you messages that you have received. You will be sent messages reminding you 
                of scheduled events and actions that are needed as your evaluation proceeds.</li>

            <li>The <b>Sent</b> tab will show you messages that you have sent.</li>

            <li>The <b>Deleted</b> tab will show you messages that you have deleted.</li>
        </ul>
        <div style="text-align:center;">
            <a href="#" style="color: blue; text-decoration: underline;" onclick="$('#hlpMessages').hide();">[X] Close</a>
        </div>
    </div>

    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server">
        <ContentTemplate>
            <ucMsg:ucMsg ID="ucMsg" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script src="js/messages/textboxlist/js/GrowingInput.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.Autocomplete.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.Autocomplete.Binary.js"></script>
    <script src="js/messages/messages.js"></script>

    <script>
        messages.init();

        $(document).ready(function () {
            setTimeout(function () {
                if (typeof (adjustHeight) != "undefined") {
                    adjustHeight();
                }
                else {
                    var adjustHeight = function () {
                        var opts = {
                            _containerID: 'divContentsWrapper',
                            _overflow: true,
                            _offset: 20
                        };

                        if (typeof (arguments[0] != "undefined")) {
                            if (typeof (arguments[0] == "object")) {
                                $.extend(opts, arguments[0]);
                            }
                        }

                        var ele = $('[id$="' + opts._containerID + '"]')[0],
                            pos = $(ele).position(),
                            wHeight = $(window).height(),

                            newHeight = parseInt(wHeight - (pos.top + opts._offset));

                        if (opts._overflow) {
                            $(ele).css({ height: newHeight + 'px', overflow: 'auto' });
                        }
                        else {
                            $(ele).css({ height: newHeight + 'px' });
                        }
                    };
                    adjustHeight();
                }
            }, 1);
        });

        // Prevent the backspace key from navigating back.
        $(document).bind('keydown', function (event) {
            var doPrevent = false;
            if (event.keyCode === 8) {
                var d = event.srcElement || event.target;
                if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD'))
                     || d.tagName.toUpperCase() === 'TEXTAREA' || d.tagName.toUpperCase() === 'UL' || d.tagName.toUpperCase() === 'LI') {
                    doPrevent = d.readOnly || d.disabled;
                }
                else {
                    doPrevent = true;
                }
            }

            if (doPrevent) {
                event.preventDefault();
            }
        });


        var tblist;
        $(function () {
            // Standard initialization
            tblist = new $.TextboxList('#form_tags_input', {
                unique: false,
                startEditableBit: true,
                endEditableBit: true,
                inBetweenEditableBits: false,
                hideEditableBits: true
            });
            tblist.addEvent('bitBoxRemove', function (bit) {
                var bitValue = bit.value[1];
                $('input[type="checkbox"][name*="chklstPatients"], input[type="checkbox"][name*="chklstProviders"]').each(function (index, element) {
                    var lbl = $('label[for="' + element.id + '"]').text();
                    if (lbl == bitValue) {
                        element.checked = false;
                    }
                });
            });
        });

        function GetKeyCode(event) {
            var keyCode = ('which' in event) ? event.which : event.keyCode;
            return (keyCode == 8 || keyCode == 37 || keyCode == 39);
        }

        $(document).ready(function (e) {
            //--->
            $('input[type="checkbox"][name*="chklstPatients"], input[type="checkbox"][name*="chklstProviders"]').each(function (index, element) {
                $(element).bind({
                    click: function () {
                        messages.recipients.addRecipient(this);
                    }
                });
            });

            //--->
            $('input.textboxlist-bit-editable-input').each(function (index, element) {
                $(element).bind({
                    keydown: function (event) {
                        return GetKeyCode(event);
                    }
                });
            });
        });

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {
            $('div.ajax-loader').hide();
            messages.init();

            //wp.adjustMain();
            //autoAdjustMainDiv();

            adjustHeight();

            $(function () {
                // Standard initialization
                tblist = new $.TextboxList('#form_tags_input', {
                    unique: false,
                    startEditableBit: true,
                    endEditableBit: true,
                    inBetweenEditableBits: false,
                    hideEditableBits: true
                });
                tblist.addEvent('bitBoxRemove', function (bit) {
                    var bitValue = bit.value[1];
                    $('input[type="checkbox"][name*="chklstPatients"], input[type="checkbox"][name*="chklstProviders"]').each(function (index, element) {
                        var lbl = $('label[for="' + element.id + '"]').text();
                        if (lbl == bitValue) {
                            element.checked = false;
                        }
                    });
                });
            });

            $(document).ready(function (e) {
                //--->
                $('input[type="checkbox"][name*="chklstPatients"], input[type="checkbox"][name*="chklstProviders"]').each(function (index, element) {
                    $(element).bind({
                        click: function () {
                            messages.recipients.addRecipient(this);
                        }
                    });
                });

                //--->
                $('input.textboxlist-bit-editable-input').each(function (index, element) {
                    $(element).bind({
                        keydown: function (event) {
                            return GetKeyCode(event);
                        }
                    });
                });
            });

            restartSessionTimeout();
        }

        function autoAdjustMainDiv() {
            /*
            origWidth = $('input[id$="htxtMainDivWidth"]').val();
            wp.adjustMain();

            if ($('div[id$="mainContents"]').css('width') != origWidth) {
                $('div[id$="mainContents"]').css({
                    width: origWidth
                });
                $('input[id$="htxtMainDivWidth"]').val(origWidth);
            }
            $('input[type="button"], input[type="submit"]').css({
                padding: '2px 6px'
            });
            */
            return;
        }

    </script>
</asp:Content>