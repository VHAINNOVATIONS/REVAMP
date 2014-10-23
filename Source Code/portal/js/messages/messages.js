var messages = {
    //general options and settings
    opts: {
        selectedMsgID: 'htxtSelectedMsg',
        tblMsgList: 'tblMsgList',
        tblSentMsg: 'tblSentMsg',
        tblDeletedMsg: 'tblDeletedMsg',
        msgBody: 'txtMsgBody',
        txtSubject: 'txtSubject',
        chklsPatients: 'chklstPatients',
        chklstProviders: 'chklstProviders'
    },

    //read message
    readMsg: function (obj) {
        var me = this,
            selMsg = $('input[id$="' + me.opts.selectedMsgID + '"]')[0],
            id = obj.getAttribute('messageid').replace(/\D/gi, ''),
            rows = $('tbody tr', $('table[id$="' + me.opts.tblMsgList + '"]'));

        $('tbody tr', $('table[id$="' + me.opts.tblSentMsg + '"]')).each(function () {
            rows.push(this);
        });

        $('tbody tr', $('table[id$="' + me.opts.tblDeletedMsg + '"]')).each(function () {
            rows.push(this);
        });

        if (typeof (me.opts.selectPanel) != "undefined") {

            //clear previous selections
            $(selMsg).val('');
            $.each(rows, function (i, r) {
                $(r).removeClass('selectedMsg');
            });

            //make the actual selection
            $(selMsg).val(id);

            //clear displayed message
            $('textarea[id$="txtMsgBodyContents"]').val('');
            $('textarea[id$="txtMsgBodyContents"]').text('');
            $('div[id$="divMsgContents"]').hide();

            __doPostBack(me.opts.selectPanel, '');
        }
    },

    //compose message
    composeMessage: function () {
        var me = this,
            msgBody = $('textarea[id$="' + me.opts.msgBody + '"]')[0],
            txtSubject = $('[id$="' + me.opts.txtSubject + '"]')[0],
			lbl = $('span[id$="lblRecipients"]')[0],
            chklstProviders = $('input[type="checkbox"][id*="' + me.opts.chklstProviders + '"]'),
            chklstPatients = $('input[type="checkbox"][id*="' + me.opts.chklsPatients + '"]');

        //reset compose msg screen
        $(msgBody).html('');
        $(msgBody).val('');
        $(txtSubject).val('');
        $(lbl).text('');

        //reset recipients list
        $(chklstProviders).removeAttr('checked');
        $(chklstPatients).removeAttr('checked');

        if(typeof(winComposeMsg)!="undefined"){
            winComposeMsg.show();
        }
    },

    //select patient
    selectPatient: function () {
        if (typeof (winSelectPatient) != "undefined") {
            winSelectPatient.show();
        }
    },

    //select provider
    selectProvider: function () {
        if (typeof (winSelectProvider) != "undefined") {
            winSelectProvider.show();
        }
    },

    //close selection popup 
    selectRecipients: function (obj) {
        if (typeof (obj) != "undefined") {
            obj.hide();
        }
    },

    cancelRecipients: function (opts) {
        if (typeof (opts.dialog) != "undefined") {
            opts.dialog.hide();
        }

        $('input[type="checkbox"][name*="' + opts.checkboxes + '"]').each(function () {
            this.checked = false;
        });
    },

    //paint selected message row after postback
    checkSelectedMsg: function () {
        var me = this,
            selMsg = $('input[id$="' + me.opts.selectedMsgID + '"]')[0],
            rows = $('tbody tr', $('table[id$="' + me.opts.tblMsgList + '"]'));

        $('tbody tr', $('table[id$="' + me.opts.tblSentMsg + '"]')).each(function () {
            rows.push(this);
        });

        $('tbody tr', $('table[id$="' + me.opts.tblDeletedMsg + '"]')).each(function () {
            rows.push(this);
        });

        $.each(rows, function (i, r) {
            if ($(r).attr('messageid') == selMsg.value) {
                $(r).addClass('selectedMsg');
            }
        });
    },

    //monitor "selecte all" on recipient's list
    chkSelectAll: function (obj, args) {
        var me = this,
            chkAll = $('input[type="checkbox"][name*="' + args.name + '"]')[0];

        //check status on "ALL" checkbox
        if (args.master) {
            $('input[type="checkbox"][name*="' + args.name + '"]').each(function () {
                this.checked = obj.checked;
            });
        }
        else
        {
            if (!obj.checked) {
                chkAll.checked = false;
            }
        }
    },

    //select provider recipients
    selectProviderRecipients: function () {
        var me = this,
            chklst = $('input[type="checkbox"][name*="chklstProviders"]:checked'),
            lbl = $('span[id$="lblRecipients"]')[0],
            provNames = [],
            strRecipients = '';

        $.each(chklst, function (i, o) {
            var m_name = $('label[for$="' + o.id + '"]').text();
            if (m_name.indexOf("ALL") < 0) {
                provNames.push(m_name);
            }
        });

        $.each(provNames, function (i, n) {
            strRecipients += n + '; ';
        });

        $(lbl).text(strRecipients);

        if (typeof (arguments[0]) == "undefined") {
            if (typeof (winSelectProvider) != "undefined") {
                winSelectProvider.hide();
            }
        }
    },

    //select patient recipients
    selectPatientRecipients: function () {
        //var me = this,
        //    chklst = $('input[type="checkbox"][name*="chklstPatients"]:checked, input[type="checkbox"][name*="chklstProviders"]:checked'),
        //    lbl = $('span[id$="lblRecipients"]')[0],
        //    provNames = [],
        //    strRecipients = '';

        //$.each(chklst, function (i, o) {
        //    var m_name = $('label[for$="' + o.id + '"]').text();
        //    if (m_name.indexOf("ALL") < 0) {
        //        provNames.push(m_name);
        //    }
        //});

        //$.each(provNames, function (i, n) {
        //    strRecipients += n + '; ';
        //});

        //$(lbl).text(strRecipients);

        //if (typeof (arguments[0]) == "undefined") {
        //    if (typeof (winSelectPatient) != "undefined") {
        //        winSelectPatient.hide();
        //    }
        //}
    },


    //initialization functions
    init: function () {
        var me = this;
        $(document).ready(function () {
            setTimeout(function () {

                me.checkSelectedMsg();

                $('input[type="button"], input[type="submit"]').css({padding: '2px 6px'});
            }, 100);
        });
    }
};

// ------------------------------

messages.recipients = {

    addRecipient: function (obj) {
        var me = this,
		    mID = obj.value,
			mText = $('label[for="' + obj.id + '"]').text(),
			reText = new RegExp(mText, 'g');

        if (obj.checked && mText.toLowerCase() != "all") {
            tblist.add(mText);
        }
        else {
            me.restoreList();
        }
    },

    restoreList: function () {
        $('li.textboxlist-bit-box').remove();
        var editable = $('li.textboxlist-bit-editable');
        if (editable.length > 1) {
            for (var a = 0; a < editable.length - 1; a++) {
                $(editable[a]).remove();
            }
        }

        $('input[type="checkbox"][name*="chklstPatients"], input[type="checkbox"][name*="chklstProviders"]').each(function (index, element) {
            var boxText = $('label[for="' + element.id + '"]').text();
            if (element.checked) {
                if (boxText.toLowerCase() != "all") {
                    tblist.add(boxText);
                }
            }
        });
    }
};