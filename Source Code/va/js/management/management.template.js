//MANAGEMENT.TEMPLATE namespace
if (management !== undefined)
{
    management.template = {

        //template table
        tblTemplate: 'tblTemplates',

        //template type radio buttons list
        rblType: 'rblTemplateType',

        //grid controls collection
        gridControls: {
            text: [
				['txtTemplateName', 'htxtTemplateNameOrig'],
				['cboTemplateType', 'htxtTemplateTypeOrig'],
				['txtTemplateText', 'htxtTemplateTextOrig'],

                ['txtGroupName', 'htxtGroupName']
			]
        },

        //row in edit mode
        openRow: null,

        //show template type rows
        showTemplateTypeRow: function()
        {
            var me = this;

            //close any open row in edit mode
            me.closeOpenRow();

            var selectedRadio = $('input[name$="' + me.rblType + '"]:checked').get(0);
            var tblTR = $('tbody tr', $('table[id$="' + me.tblTemplate + '"]'));
            $.each(tblTR, function(i, ele)
            {
                if ($(ele).attr('templatetype') == selectedRadio.value)
                {
                    $(ele).show();
                }
                else
                {
                    $(ele).hide();
                }
            });
        },

        //bind 'onclick' event to radio buttons
        bindRadOnclick: function()
        {
            var me = this;

            $('input[name$="' + me.rblType + '"]').bind({
                click: function()
                {
                    me.showTemplateTypeRow();
                }
            });
        },

        //edit row
        editRow: function(obj)
        {
            var me = this;

            //close any open row in edit mode
            me.closeOpenRow();
            me.cancelAddRow();

            //get the current table row
            var mTR = $(obj).parent().parent().parent('tr');

            //hide read DIV
            $('.grid-read', $(mTR)).hide();

            //show edit DIV
            $('.grid-edit', $(mTR)).show();
            $(mTR).css({ 'background-color': '#F5FFF5' });

            //expose row in edit mode
            me.openRow = mTR;
        },

        //cancel edit row
        cancelEditRow: function(obj)
        {
            var me = this;

            //get the current table row
            var mTR = $(obj).parent().parent().parent('tr');

            if (mTR.id != 'trtemp_0')
            {
                //show read DIV
                $('.grid-read', $(mTR)).show();

                //hide edit DIV
                $('.grid-edit', $(mTR)).hide();
                $(mTR).css({ 'background-color': '' });

                me.restoreEditControls(mTR);
            }
            else
            {
                me.cancelAddRow();
            }

            //no row in edit mode
            me.openRow = null;
        },

        //restore edit controls 
        restoreEditControls: function(trObj)
        {
            var me = this;
            $.each(me.gridControls.text, function(i, ele)
            {
                var ctrlEdit = $('[id$="' + ele[0] + '"]', $(trObj)).get(0);
                var ctrlOrig = $('[id$="' + ele[1] + '"]', $(trObj)).get(0);

                if (typeof ctrlEdit != "undefined" && typeof ctrlOrig != "undefined")
                {
                    ctrlEdit.value = ctrlOrig.value;
                }
            });
        },

        //close any open row in edit mode
        closeOpenRow: function()
        {
            var me = this;

            if (me.openRow != null)
            {
                var cancelBtn = $('.cancel-edit', $(me.openRow)).get(0);
                me.cancelEditRow(cancelBtn);
            }
        },

        //display and init new Template row
        addRow: function()
        {
            var me = this,
                selectedRadio = $('input[name$="' + me.rblType + '"]:checked').get(0),
                tblTR = $('tbody tr', $('table[id$="' + me.tblTemplate + '"]')),
                tempCount = 0;

            $.each(tblTR, function (i, ele) {
                if ($(ele).attr('templatetype') == selectedRadio.value) {
                    tempCount = tempCount + 1;
                }
            });

            if (tempCount < 1) {
                //get the active template type radio button text
                me.setTypeLabel();

                //clear fields
                var addCtrls = ['txtTemplateNameAdd', 'txtTemplateTextAdd'];
                $.each(addCtrls, function(i, v){
                    $('[id$="' + v + '"]').val('');
                });

                //close any open row in edit mode
                me.closeOpenRow();

                //display the add row
                $('tr[id$="trtemp_0"]').css({ 'background-color': '#F5FFF5' }).show();
            }
        },

        //cancel add template
        cancelAddRow: function()
        {
            var me = this;

            //clear fields
            var addCtrls = ['txtTemplateNameAdd', 'txtTemplateTextAdd'];
            $.each(addCtrls, function(i, v)
            {
                $('[id$="' + v + '"]').val('');
            });

            //hide the add row
            $('tr[id$="trtemp_0"]').hide();

            //set openRow to null
            me.openRow = null;
        },

        //show row if data validation fails
        showNonValidRow: function(templateID)
        {
            var me = this,
			mRows = $('tbody tr', $('table[id$="' + me.tblTemplate + '"]')),
			editObj = null;

            $.each(mRows, function(i, row)
            {
                if (row.id == 'trtemp_' + templateID)
                {
                    editObj = $('.edit-row', $(row)).get(0);
                }
            });

            if (editObj != null)
            {
                me.editRow(editObj);
                alert('The template was not saved/updated. \nAll text fields are required.');
            }
        },

        //validate add
        validateAdd: function()
        {
            var addCtrls = ['txtTemplateNameAdd', 'txtTemplateTextAdd'],
			valid = 0;
            $.each(addCtrls, function(i, v)
            {
                var strVal = $('[id$="' + v + '"]').val();
                if (strVal.replace(/\s/gi, '').length > 1)
                {
                    valid += 1;
                }
            });
            if (valid > 1)
            {
                return true;
            }
            else
            {
                alert('The template was not saved/updated. \nAll text fields are required.');
                return false;
            }
        },

        //validate row
        validateRow: function(obj)
        {
            var me = this,
			valid = 0,
			controls = ['txtTemplateName', 'txtTemplateText'],
			m_row = $(obj).parent().parent().parent('tr');
            $.each(controls, function(i, ele)
            {
                var strVal = $('[id$="' + ele[0] + '"]', $(m_row)).val();

                //for_debug
                //console.log('control_id: %s, value: %s', ele[0], strVal);

                if (strVal.replace(/\s/gi, '').length > 1)
                {
                    valid += 1;
                }
            });

            if ($('[id$="cboTemplateType"]').get(0).selectedIndex > 0)
            {
                valid += 1;
            }

            if (valid > 2)
            {
                return true;
            }
            else
            {
                alert('The template was not saved/updated. \nAll text fields are required.');
                return false;
            }
        },

        //set "Add Template" type label
        setTypeLabel: function()
        {
            var me = this,
			selectedRadio = $('input[name$="' + me.rblType + '"]:checked').get(0);
            $('label').each(function()
            {
                var ele = this;
                if (ele.htmlFor != undefined)
                {
                    if (ele.htmlFor == selectedRadio.id)
                    {
                        $('[id$="spTemplateType"]').text($(ele).text());
                    }
                }
            });
        },

        //delete template confirm
        deleteConfirm: function()
        {
            return confirm('You are about to delete this template. \nDo you wish to continue?');
        },

        //INIT
        init: function()
        {
            var me = this;
            me.showTemplateTypeRow();
            me.bindRadOnclick();
            me.setTypeLabel();
        }
    };

    management.template.group = {
        opts: {
            htxtGroupID: 'htxtTemplateGroupID',
            newTempNameID: 'txtTempGroupNameAdd',
            addTrID: 'trtempgroup_0'
        },

        cancelAddTempGroup: function(obj) {
            var me = this,
                tr = $('[id$="'+ me.opts.addTrID +'"]')[0],
                txtName = $('input[type="text"][id$="' + me.opts.newTempNameID + '"]')[0];

            $(txtName).val('');
            $(tr).hide();
        },

        AddTempGroup: function (obj) {
            var me = this,
                tr = $('[id$="' + me.opts.addTrID + '"]')[0],
                txtName = $('input[type="text"][id$="' + me.opts.newTempNameID + '"]')[0];

            $(txtName).val('');
            $(tr).show();
        },

        selectGroup: function (id) {
            var me = this,
                grpID = id,
                htxtGrpID = $('input[id$="' + me.opts.htxtGroupID + '"]')[0];

            htxtGrpID.value = grpID;
            __doPostBack(me.opts.SelectGrpBtn, '');

        }
    };
}

// Notify ScriptManager that this is the end of the script.
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();