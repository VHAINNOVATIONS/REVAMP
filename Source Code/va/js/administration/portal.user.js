
if (typeof (portal) == "undefined")
{
    //var admin = {};
    var portal = {};
}

portal.user = {
    _opts: { tblID: 'tblPatList', criteriaID: 'txtPatSearch' },
    _selectedPatID: '',

    //textboxes mappings
    _mapText: {
        name: 'txtName',
        phone: 'txtPhone',
        email: 'txtEmail'
    },

    //combos mappings
    _mapCombo: {
        
    },

    //filter patient list by search criteria
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
                var mTXT = $.trim($(v).children('td').get(0).innerHTML);
                if (mTXT.toLowerCase().indexOf(txtCriteria) > -1)
                {
                    $(v).show();
                }
                else
                {
                    $(v).hide();
                }
            });
        }

        _me.clearPatientData();

        //goto first tab
        $find($('[id$="tcPatPortal"]').attr('id')).set_activeTabIndex(0);

    },

    //reset filter & show all users on the list
    showAll: function()
    {
        var _me = this,
			txt = $('input[id$="' + _me._opts.criteriaID + '"]')[0];

        txt.value = '';
        _me.search();
    },

    select: function(row)
    {
       
        var _me = this,

        //original trID = row.id.replace(/\D/gi, '');
        trID = row.id.replace(/^.../, '');

        //clear selected class from previous selection
        $('tbody tr', $('table[id$="' + _me._opts.tblID + '"]')).each(function(i, r)
        {
            $(r).removeClass('selected');
        });

        //update class for selected row
        $(row).addClass('selected');

        //FILL PATIENT DATA CONTROLS
        _me.fillPatientData(trID);

        //goto first tab
        /*
        Sys.onReady(function(){
        $find($('[id$="tcPatPortal"]').attr('id')).set_activeTabIndex(0);
        });
        */
    },

    selectPat: function (patid) {

        var _me = this;

        //FILL PATIENT DATA CONTROLS
        _me.fillPatientData(patid);
    },

    //add new patient
    addPatient: function()
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
        $.each(['htxtPatientID', 'htxtFxUserID'], function(i, v)
        {
            $('input[id$="' + v + '"]').val('');
        });

        //clear user's data controls
        _me.clearPatientData();
    },

    //build patient data object
    buildPatientDataObject: function()
    {
        var _me = this,
            txtData = $('input[id$="htxtPatientData"]')[0];
        if (txtData.value.length > 2)
        {
            return eval(txtData.value);
        }
        return null;
    },

    //fill user data controls
    fillPatientData: function(patientID)
    {

        var _me = this;

        if (typeof (_patientData) != "undefined")
        {
            var patObj = null;

            //fetch the selected user data object
            $.each(_patientData, function(i, o)
            {
                if (o.patient_id == patientID)
                {
                    patObj = o;
                }
            });

            //clear all user's data controls first
            if (patObj != null)
            {
                _me.clearPatientData();
            }

            //set fx_user_id & patient_id
            $.each({ fx_user_id: 'htxtFxUserID', patient_id: 'htxtPatientID' }, function (k, v) 
            {
                $('input[id$="' + v + '"]').val(patObj[k]);
            });

            //fill user's textboxes
            $.each(_me._mapText, function(k, v)
            {
                $('input[id$="' + v + '"]')[0].value = patObj[k];
            });

            //fill user's dropdowns
            //$.each(_me._mapCombo, function(k, v)
            //{
            //    $('select[id$="' + v + '"]')[0].value = patObj[k];
            //});

           //fill user credentials
            $('input[id$="txtUserId"]').val(patObj.user_name);

            $.each({ is_locked: 'chkbxAccountLocked', is_inactive: 'chkbxAccountInactive' }, function(k, v)
            {
                $('input[id$="' + v + '"]')[0].checked = (patObj[k] == 1);
            });

            if (patObj.user_name.length > 0)
            {
                $.each(['txtPassword', 'txtVerifyPassword'], function(i, v)
                {
                    var pwd = $('input[id$="' + v + '"]')[0];
                    pwd.value = '';
                    pwd.setAttribute('disabled', 'disabled');
                });

                $('input[id$="chkResetPasswd"]')[0].removeAttribute('disabled');
                $('input[id$="chkResetPasswd"]')[0].checked = false;
                
                //------------------------------------Added New - Enable CheckBoxes
                $('input[id$="chkbxAccountLocked"]')[0].removeAttribute('disabled');
                $('input[id$="chkbxAccountInactive"]')[0].removeAttribute('disabled');
                //-------------------------------------
                $('input[id$="txtUserId"]')[0].setAttribute('disabled', 'disabled');
                
            }

            _me._selectedPatID = patObj.patient_id;

            //goto first tab
            //$find($('[id$="tcPatPortal"]').attr('id')).set_activeTabIndex(0);
        }
        return;
        },
     
    // clear patient's data controls
    clearPatientData: function()
    {
        var _me = this;

        //clear patient's textboxes
        $.each(_me._mapText, function(k, v)
        {
            $('input[id$="' + v + '"]')[0].value = '';
        });

        //clear user's combos
        //$.each(_me._mapCombo, function(k, v)
        //{
        //    $('select[id$="' + v + '"]')[0].selectedIndex = -1;
        //});

        $('input[id$="htxtSelUsrType"]').val('');

        //clear fx_user_id & patient_id
        $.each({ fx_user_id: 'htxtFxUserID', patient_id: 'htxtPatientID' }, function(k, v)
        {
            $('input[id$="' + v + '"]').val('');
        });

        //clear patient credentials
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

        _me._selectedPatID = '';
    },

    // restore status
    restoreStatus: function()
    {
        var _me = this,
		    patientID = $('input[id$="htxtPatientID"]').val(),
			fx_user_id = 0

        if (patientID.length > 0)
        {
            if (typeof (_patientData) != "undefined")
            {
                $.each(_patientData, function(i, o)
                {
                    if (patientID == o.patient_id)
                    {
                        fx_user_id = o.fx_user_id;
                    }
                });

                if (fx_user_id > 0)
                {
                    //var patRow = $('[id$="tr_' + fx_user_id + '"]')[0];
                    //if (patRow)
                    //{
                    //    _me.select(patRow);
                    //}
                    _me.fillPatientData(patientID);
                }
            }
        }
    },

    //reset password
    resetPasswd: function(chk)
    {
        if (chk.checked)
        {
            $.each(['txtPassword', 'txtVerifyPassword'], function (i, v)
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

    //init function
    init: function()
    {
        var _me = this;
        $(document).ready(function()
        {
            var patientID = $('input[id$="htxtPatientID"]').val();
            if (patientID.length > 0)
            {
                _me.restoreStatus();
            }

            //audit reset passwd
            var txtUsrID = $('input[id$="txtUserId"]')[0];
            if (txtUsrID.value.length < 1
            || ((patientID.length == 0 || patientID.value == ''))) {
                $('input[id$="chkResetPasswd"]').each(function () {
                    this.checked = false;
                    this.setAttribute('disabled', 'disabled');
                });

                //------------------------------------Added New - Disabled CheckBoxes             
                //$('input[id$="txtName"]')[0].setAttribute('disabled', 'disabled');
                $('input[id$="txtPhone"]')[0].setAttribute('disabled', 'disabled');

                $('input[id$="txtEmail"]')[0].setAttribute('disabled', 'disabled');
                $('input[id$="txtUserId"]')[0].setAttribute('disabled', 'disabled');

                $('input[id$="txtPassword"]')[0].setAttribute('disabled', 'disabled');
                $('input[id$="txtVerifyPassword"]')[0].setAttribute('disabled', 'disabled');

                $('input[id$="chkbxAccountLocked"]')[0].setAttribute('disabled', 'disabled');
                $('input[id$="chkbxAccountInactive"]')[0].setAttribute('disabled', 'disabled');

                //-------------------------------------

            }
            
        });
    }
};