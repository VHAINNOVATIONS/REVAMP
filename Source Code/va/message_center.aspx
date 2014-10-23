<%@ Page Title="REVAMP Practitioner - Message Center" Language="C#" MasterPageFile="~/MasterPageRpt2.master" AutoEventWireup="true" CodeFile="message_center.aspx.cs" Inherits="message_center" %>
<%@ MasterType VirtualPath="~/MasterPageRpt2.master" %>
<%@ Register TagPrefix="ucMsg" TagName="ucMsg" Src="~/ucMessagesRead.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    
    <link href="js/messages/textboxlist/css/TextboxList.css" rel="stylesheet" />
    <link href="js/messages/textboxlist/css/TextboxListAutocomplete.css" rel="stylesheet" />

    <style>
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

    <script src="js/messages/textboxlist/js/GrowingInput.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.Autocomplete.js"></script>
    <script src="js/messages/textboxlist/js/TextboxList.Autocomplete.Binary.js"></script>
    <script src="js/messages/messages.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder001" Runat="Server">
    
    <div style="padding:15px;">
        <ucMsg:ucMsg ID="ucMsg" runat="server" />
    </div>
    
    <script>
        messages.init();

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
        }

    </script>
</asp:Content>