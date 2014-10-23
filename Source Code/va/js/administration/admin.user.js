
if (typeof (admin) == "undefined")
{
    var admin = {};
}

admin.user = {
    _opts: { tblID: 'tblUsersList', criteriaID: 'txtUsrSearch' },
    _selectedUsrID: '',

    //textboxes mappings
    
    _mapText: {
        name: 'txtName',
        title: 'txtTitle',
        phone: 'txtPhone',
        email: 'txtEmail'
    },

    //combos mappings
    _mapCombo: {
        dims_id: 'cboSite'
    },

    //filter users list by search criteria
    search: function(opts)
    {
        var _me = this;
        $.extend(_me._opts, opts);
        var tbl = $('table[id$="' + _me._opts.tblID + '"]')[0],
		    txt = $('input[id$="' + _me._opts.criteriaID + '"]')[0],
		    txtLen = txt.value.replace(/\s/gi, '').length;
        bShowAll = false;

        bShowAll = (txtLen < 1);

        var mTR = $('tbody tr', $(tbl));

        if (bShowAll)
        {
            $.each(mTR, function(i, v)
            {
                $(v).show();
            });
        }
        else
        {
            var txtCriteria = ($.trim(txt.value)).toLowerCase();
            $.each(mTR, function(i, v)
            {
                var mTXT = $.trim($(v).children('td').get(0).innerHTML),
                    mUserID = v.id.replace(/\D/gi, '');

                if (mTXT.toLowerCase().indexOf(txtCriteria) > -1 
                    || mUserID.indexOf(txtCriteria) > -1)
                {
                    $(v).show();
                }
                else
                {
                    $(v).hide();
                }
            });
        }

        _me.clearUserData();

        //goto first tab
        $find($('[id$="tcUserAdmin"]').attr('id')).set_activeTabIndex(0);

    },

    //reset filter & show all users on the list
    showAll: function()
    {
        var _me = this,
			txt = $('input[id$="' + _me._opts.criteriaID + '"]')[0];

        //clear selected class from previous selection
        $('tbody tr', $('table[id$="' + _me._opts.tblID + '"]')).each(function (i, r) {
            $(r).removeClass('selected');
        });

        txt.value = '';
        _me.search();
    },

    select: function(row)
    {
        var _me = this,
		    trID = row.id.replace(/\D/gi, '');

        //clear selected class from previous selection
        $('tbody tr', $('table[id$="' + _me._opts.tblID + '"]')).each(function(i, r)
        {
            $(r).removeClass('selected');
        });

        //update class for selected row
        $(row).addClass('selected');

        //FILL USER DATA CONTROLS
        _me.fillUserData(trID);

        //goto first tab
        /*
        Sys.onReady(function(){
        $find($('[id$="tcUserAdmin"]').attr('id')).set_activeTabIndex(0);
        });
        */
    },

    //add new user
    addUser: function()
    {
        var _me = this;

        //clear search criteria textbox
        _me.showAll();

        //clear selected class from previous selection
        $('tbody tr', $('table[id$="' + _me._opts.tblID + '"]')).each(function(i, r)
        {
            $(r).removeClass('selected');
        });

        //clear hidden fields
        $.each(['htxtProviderID', 'htxtFxUserID'], function(i, v)
        {
            $('input[id$="' + v + '"]').val('');
        });

        //clear user's data controls
        _me.clearUserData();
    },

    //build user data object
    buildUserDataObject: function()
    {
        var _me = this,
            txtData = $('input[id$="htxtUserData"]')[0];
        if (txtData.value.length > 2)
        {
            return eval(txtData.value);
        }
        return null;
    },

    //fill user data controls
    fillUserData: function(userID)
    {
        var _me = this;

        if (typeof (_userData) != "undefined")
        {
            var usrObj = null;

            //fetch the selected user data object
            $.each(_userData, function(i, o)
            {
                if (o.fx_user_id == userID)
                {
                    usrObj = o;
                }
            });

            //clear all user's data controls first
            if (usrObj != null)
            {
                _me.clearUserData();
            }

            //set fx_user_id & provider_id
            $.each({ fx_user_id: 'htxtFxUserID', provider_id: 'htxtProviderID' }, function(k, v)
            {
                $('input[id$="' + v + '"]').val(usrObj[k]);
            });

            //fill user's textboxes
            $.each(_me._mapText, function(k, v)
            {
                $('input[id$="' + v + '"]')[0].value = usrObj[k];
            });

            //fill user's dropdowns
            $.each(_me._mapCombo, function(k, v)
            {
                $('select[id$="' + v + '"]')[0].value = usrObj[k];
            });

            //fill user type radio buttons
            $('input[name$="rblUserType"]').each(function()
            {
                if (this.value == usrObj.user_type)
                {
                    this.checked = true;
                }
            });

            //fill user rights
            var uRight = parseInt(usrObj.user_rights);
            $('input[name$="chkUsrRight"]').each(function(i, c)
            {
                var cRight = parseInt(c.value),
				    chkRO = $('input[name$="chkReadOnly"][value="' + c.value + '"]')[0];

                if (uRight & cRight)
                {
                    c.checked = true;
                    var readOnly = parseInt(usrObj.read_only);
                    if (readOnly & cRight)
                    {
                        chkRO.checked = true;
                    }
                }

                _me.checkReadOnly(c);
            });

            //fill user credentials
            $('input[id$="txtUserId"]').val(usrObj.user_name);

            $.each({ is_locked: 'chkbxAccountLocked', is_inactive: 'chkbxAccountInactive' }, function(k, v)
            {
                $('input[id$="' + v + '"]')[0].checked = (usrObj[k] == 1);
            });

            if (usrObj.user_name.length > 0)
            {
                $.each(['txtPassword', 'txtVerifyPassword'], function(i, v)
                {
                    var pwd = $('input[id$="' + v + '"]')[0];
                    pwd.value = '';
                    pwd.setAttribute('disabled', 'disabled');
                });

                $('input[id$="chkResetPasswd"]')[0].removeAttribute('disabled');
                $('input[id$="chkResetPasswd"]')[0].checked = false;

                $('input[id$="txtUserId"]')[0].setAttribute('disabled', 'disabled');
            }


            // check supervisors
            //if (usrObj.user_type == 2)
            //{
            //    $('#trSupervisors').show();
            //    if (usrObj.supervisor_id.length > 0)
            //    {
            //        $('option', $('[id$="cboSupervisors"]')).each(function()
            //        {
            //            this.selected = (this.value == usrObj.supervisor_id);
            //        });
            //    }
            //}
            //else
            //{
            //    $('#trSupervisors').hide();
            //    $('[id$="cboSupervisors"]')[0].selectedIndex = -1;
            //}

            _me._selectedUsrID = usrObj.provider_id;

            //goto first tab
            //$find($('[id$="tcUserAdmin"]').attr('id')).set_activeTabIndex(0);
        }
        return;
    },

    // clear user's data controls
    clearUserData: function()
    {
        var _me = this;

        //clear user's textboxes
        $.each(_me._mapText, function(k, v)
        {
            $('input[id$="' + v + '"]')[0].value = '';
        });

        //clear user's combos
        $.each(_me._mapCombo, function(k, v)
        {
            $('select[id$="' + v + '"]')[0].selectedIndex = -1;
        });

        //clear user type radio buttons
        $('input[name$="rblUserType"]').each(function()
        {
            this.checked = false;
        });

        $('input[id$="htxtSelUsrType"]').val('');

        //clear user rights
        $('input[name$="chkUsrRight"]').each(function(i, c)
        {
            c.checked = false;
            _me.checkReadOnly(c);
        });

        //clear fx_user_id & provider_id
        $.each({ fx_user_id: 'htxtFxUserID', provider_id: 'htxtProviderID' }, function(k, v)
        {
            $('input[id$="' + v + '"]').val('');
        });

        //clear user credentials
        $.each(['txtPassword', 'txtVerifyPassword', 'txtUserId'], function(i, t)
        {
            var txtPwd = $('input[id$="' + t + '"]')[0];
            txtPwd.value = '';
            txtPwd.removeAttribute('disabled');
        });

        $('input[id$="chkResetPasswd"]').each(function()
        {
            this.checked = false;
            this.setAttribute('disabled', 'disabled');
        });

        //clear account status
        $.each(['chkbxAccountLocked', 'chkbxAccountInactive'], function(i, v)
        {
            $('[id$="' + v + '"]')[0].checked = false;
        });

        _me._selectedUsrID = '';
    },

    // show/hide read-only option
    checkReadOnly: function(obj)
    {
        var tgtDiv = $(obj).parent('td').next('td').children('div')[0];
        if (obj.checked)
        {
            $(tgtDiv).show();
        }
        else
        {
            $('input[type="checkbox"]', $(tgtDiv)).each(function()
            {
                this.checked = false;
            });
            $(tgtDiv).hide();
        }
    },

    // restore status
    restoreStatus: function()
    {
        var _me = this,
		    providerID = $('input[id$="htxtProviderID"]').val(),
			fx_user_id = 0,
			htxtSavedTemplate = $('input[id$="htxtSavedTemplate"]')[0];

        if (providerID.length > 0)
        {
            if (typeof (_userData) != "undefined")
            {
                $.each(_userData, function(i, o)
                {
                    if (providerID == o.provider_id)
                    {
                        fx_user_id = o.fx_user_id;
                    }
                });

                if (fx_user_id > 0)
                {
                    var usrRow = $('[id$="tr_' + fx_user_id + '"]')[0];
                    if (usrRow)
                    {
                        _me.select(usrRow);
                        /*
                        if(htxtSavedTemplate.value == "1"){
                        _me.fillRightsTemplate();
                        htxtSavedTemplate.value = "";
                        }
                        */
                    }
                }
            }
        }
    },

    //reset password
    resetPasswd: function(chk)
    {
        if (chk.checked)
        {
            $.each(['txtPassword', 'txtVerifyPassword'], function(i, v)
            {
                var pwd = $('input[id$="' + v + '"]')[0];
                pwd.value = '';
                pwd.removeAttribute('disabled');
            });
        }
        else
        {
            $.each(['txtPassword', 'txtVerifyPassword'], function(i, v)
            {
                var pwd = $('input[id$="' + v + '"]')[0];
                pwd.value = '';
                pwd.setAttribute('disabled', 'disabled');
            });
        }
    },

    //select Intern's Supervisor
    selectSupervisor: function(obj)
    {
        var _me = this,
		    mTR = $('#trSupervisors').get(0),
			cbo = $('[id$="cboSupervisors"]').get(0);
        if (obj.value == 2)
        {
            $(mTR).show();
        }
        else
        {
            cbo.selectedIndex = -1;
            $(mTR).hide();
        }

        _me.selectedUsrType(obj);
    },

    selectedUsrType: function(obj)
    {
        var _me = this,
		    htxtSelUsrType = $('input[id$="htxtSelUsrType"]')[0];
        htxtSelUsrType.value = obj.value;
    },

    //bind 'onchange' event to supervisors combo
    onchangeSupervisor: function(obj)
    {
        var _me = this;
        if (obj.value == _me._selectedUsrID)
        {
            alert('Please select a different Supervisor for this Intern.');
            obj.selectedIndex = -1;
        }
    },

    //fill user type rights template
    fillRightsTemplate: function()
    {

        var me = this,
	        txtData = $('input[id$="htxtRightsTemplate"]').val(),
	        data = eval(txtData),
	        isSelType = false,
	        selType = 0,
	        usrRights = 0,
	        rightsMode = 0,
			htxtSelUsrType = $('input[id$="htxtSelUsrType"]')[0];

        $('[name$="rblUserType"]').each(function()
        {
            if (this.checked)
            {
                isSelType = true;
                selType = this.value;
            }
        });

        if (data)
        {
            if (isSelType)
            {
                $.each(data, function(i, o)
                {
                    if (o.user_type == selType)
                    {

                        //READ ONLY MODE
                        $('input[name$="chkReadOnly"]').each(function()
                        {
                            var r1 = parseInt(this.value),
	                            r2 = o.read_only;
                            this.checked = ((r1 & r2) > 0);
                        });

                        //USER RIGHTS
                        $('input[name$="chkUsrRight"]').each(function()
                        {
                            var r1 = parseInt(this.value),
	                            r2 = o.user_rights;
                            this.checked = ((r1 & r2) > 0);
                            me.checkReadOnly(this);
                        });

                        //USER TYPES
                        $('input[name$="rblUserType"]').each(function()
                        {
                            this.checked = (this.value == o.user_type);
                        });
                    }
                });
            }
        }
    },

    //init function
    init: function()
    {
        var _me = this;
        $(document).ready(function()
        {
            var providerID = $('input[id$="htxtProviderID"]').val();
            if (providerID.length > 0)
            {
                _me.restoreStatus();
            }

            //bind onchange to supervisor's combo
            $('select[id$="cboSupervisors"]').bind({
                change: function()
                {
                    _me.onchangeSupervisor(this);
                }
            });

            //audit reset passwd
            var txtUsrID = $('input[id$="txtUserId"]')[0];
            if (txtUsrID.value.length < 1 
            || ((providerID.length == 0 || providerID.value == '')))
            {
                $('input[id$="chkResetPasswd"]').each(function()
                {
                    this.checked = false;
                    this.setAttribute('disabled', 'disabled');
                });
            }
        });
    }

};