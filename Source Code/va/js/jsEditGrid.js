// ******************************************************
//                      EDIT GRID
// ******************************************************

function c_editGrid()
{
    var self = this;

    //set of pre-configured settings
    var opts = {
        tableID: null,
        readCls: 'grid-read',
        editCls: 'grid-edit',
        origCls: 'grid-orig',
        editClsA: 'grid-edit-a',
        editClsB: 'grid-edit-b',
        editFields: [], //['txtEditID', 'htxtOrigID']
        editCombos: [], //['comboID', 'htxtOrigID']
        editCheckboxes: [],
        editIcons: {
            edit: '.icon-edit',
            discontinue: '.icon-delete',
            update: '.icon-update',
            cancel: '.icon-cancel',
            cancelAdd: '.cancel-add'
        },
        instanceName: null,
        msgEditConflict: function()
        {
            var v_msgEditConflict = 'Another record is being edited. \n';
            v_msgEditConflict += 'Do you want to revert changes and proceed ';
            v_msgEditConflict += 'to edit the selected row?';
            return v_msgEditConflict;
        },
        msgDeleteAlert: function()
        {
            var v_msgDeleteAlert = 'You are about to delete this record.\n';
            v_msgDeleteAlert += 'Do you want to continue?';
            return v_msgDeleteAlert;
        },
        tempHtxt: null,
        updtEvtTarget: null,
        deleteEvtTarget: null,
        dicEvtTarget: null,
        discWindow: null,
        discTxtField: null,
        skipDiscontinueReason: false
    };
    if (arguments[0] && typeof arguments[0] == "object")
    {
        $.extend(opts, arguments[0]);
    }

    //get the table object
    var $tbl = $('table[id$="' + opts.tableID + '"]');

    //get the 'ADD' table extension
    var $tblAdd = $('table[id$="' + opts.tableID + '_Add"]');

    //is a grid's item being edited
    this.bItemEdit;
    this.trEditRow;
    this.tblAddNew;

    //display edit text field & toggle edit icons
    this.editRow = function(ele)
    {
        if (self.bItemEdit)
        {
            var answer = confirm(opts.msgEditConflict());
            if (answer)
            {
                var cancelEle = $(opts.editIcons.cancel, self.trEditRow);
                self.cancelEdit(cancelEle);

                //hide 'Add New' table
                if (self.tblAddNew)
                {
                    self.cancelAdd();
                }

                fn_editRow(ele);
            }
        } else
        {
            fn_editRow(ele);
        }
    };
    var fn_editRow = function(ele)
    {
        var $mtr = $(ele).parent().parent().parent('tr');
        $('.' + opts.editCls + ', .' + opts.editClsB, $mtr).show();
        $('.' + opts.readCls + ', .' + opts.editClsA, $mtr).hide();
        self.trEditRow = $mtr;
        self.bItemEdit = true;
    };
    //cancel edit & restore original values
    this.cancelEdit = function(ele)
    {
        var $mtr = $(ele).parent().parent().parent('tr');
        //console.log($mtr);
        $.each(opts.editFields, function(i, v)
        {
            var ctrlEdit = $('[id$="' + v[0] + '"]', $mtr).get(0);
            var ctrlOrig = $('[id$="' + v[1] + '"]', $mtr).get(0);
            ctrlEdit.value = ctrlOrig.value;
        });

        $.each(opts.editCheckboxes, function(i, v)
        {
            var ctrlEdit = $('[id$="' + v[0] + '"]', $mtr).get(0);
            var ctrlOrig = $('[id$="' + v[1] + '"]', $mtr).get(0);
            ctrlEdit.checked = (parseInt(ctrlOrig.value) != 0);
        });

        //clear error labels
        $('span', $mtr).html('');
        $mtr.removeClass('notvalidrow');

        $.each(opts.editCombos, function(i, v)
        {
            var $sel = $('select[id$="' + v[0] + '"]', $mtr);
            $('option', $sel).each(function()
            {
                if (this.value == $('input[id$="' + v[1] + '"]').val())
                {
                    this.selected = true;
                }
                else
                {
                    this.selected = false;
                }
            });
        });

        $('.' + opts.editCls + ', .' + opts.editClsB, $mtr).hide();
        $('.' + opts.readCls + ', .' + opts.editClsA, $mtr).show();

        self.bItemEdit = null;
        self.trEditRow = null;
    };
    //update row
    this.updateRow = function(ele)
    {
        var $mtr = $(ele).parent().parent().parent('tr');
        var trID = ($mtr.attr('id')).replace(/\D/gi, '');

        //console.log('table row id: %s', trID);
        var strArg = trID + '|';
        $.each(opts.editFields, function(i, v)
        {
            var obj = $('[id$="' + v[0] + '"]', $mtr).get(0);
            if (obj.tagName == "SELECT")
            {
                var mValue;
                $('option', $(obj)).each(function()
                {
                    if (this.selected)
                    {
                        mValue = this.value;
                        strArg += mValue + '|';
                    }
                });
            }
            else
            {
                var inp = $('input[name$="' + v[0] + '"]', $mtr).get(0);
                strArg += inp.value + '|';
            }
        });

        strArg = strArg.substr(0, strArg.length - 1);

        var input = document.createElement("input");
        input.setAttribute("type", "hidden");
        input.setAttribute("name", opts.tempHtxt);
        input.setAttribute("id", opts.tempHtxt);
        input.setAttribute("value", strArg);
        document.forms[0].appendChild(input);

        __doPostBack(opts.updtEvtTarget, opts.tempHtxt);
        //alert($('#' + opts.tempHtxt).val());

    };
    //delete row
    this.deleteRow = function(ele)
    {
        if (self.bItemEdit)
        {
            var answer = confirm(opts.msgEditConflict());
            if (answer)
            {
                var cancelEle = $(opts.editIcons.cancel, self.trEditRow);
                self.cancelEdit(cancelEle);
                this.fn_deleteRow(ele);
            }
        } else
        {
            self.fn_deleteRow(ele);
        }
    };
    this.fn_deleteRow = function(ele)
    {

        //reset discontinue textbox contents
        $('textarea[id$="' + opts.discTxtField + '"]').val('');

        var $mtr = $(ele).parent().parent().parent('tr');
        var trID = ($mtr.attr('id')).replace(/\D/gi, '');

        //create a temporary hidden field to store row id
        var tmpHtxt = document.getElementById(opts.tempHtxt);
        if (tmpHtxt != null)
        {
            tmpHtxt.value = trID;
        }
        else
        {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", opts.tempHtxt);
            input.setAttribute("id", opts.tempHtxt);
            input.setAttribute("value", trID);
            document.forms[0].appendChild(input);
        }

        if (opts.skipDiscontinueReason)
        {
            __doPostBack(opts.dicEvtTarget, opts.tempHtxt);
        }
        else
        {
            //display Discontinue popup
            Ext.onReady(function()
            {
                self.discWindow.show();
            });
        }

    };

    //cancel 'ADD'
    this.cancelAdd = function(ele)
    {
        self.clearAddControls();
        $tblAdd.hide();
        self.bItemEdit = false;
        self.tblAddNew = false;
        $tblAdd.removeClass('notvalidadd');
    };

    this.clearAddControls = function()
    {
        $('input[type="text"], textarea', $tblAdd).val('');
        $('select', $tblAdd).each(function()
        {
            this.selectedIndex = -1;
        });

        $('input[type="checkbox"]', $tblAdd).each(function()
        {
            this.checked = false;
        });
    };

    //set icon's onclick events
    this.setEditIcons = function()
    {
        $(function()
        {
            $(opts.editIcons.edit, $tbl).css({
                cursor: 'pointer'
            }).bind({
                click: function()
                {
                    self.editRow($(this));
                }
            });
            $(opts.editIcons.discontinue, $tbl).css({
                cursor: 'pointer'
            }).bind({
                click: function()
                {
                    self.deleteRow($(this));
                }
            });
            $(opts.editIcons.update, $tbl).css({
                cursor: 'pointer'
            }).bind({
                click: function()
                {
                    self.updateRow($(this));
                }
            });
            $(opts.editIcons.cancel, $tbl).css({
                cursor: 'pointer'
            }).bind({
                click: function()
                {
                    self.cancelEdit($(this));
                }
            });
            $(opts.editIcons.cancelAdd, $tblAdd).css({
                cursor: 'pointer'
            }).bind({
                click: function()
                {
                    self.cancelAdd($(this));
                }
            });
        });
    };

    this.showNonValidRow = function(args)
    {
        //get table element
        var tbl = $('table[id$="' + opts.tableID + '"]').get(0);

        //get table rows
        var tblrows = $('tbody tr', $(tbl));

        //get row by key,value pair
        $.each(tblrows, function(i, v)
        {
            if ($(v).attr(args.key) == args.value)
            {
                $('.grid-edit, .grid-edit-b', $(v)).show();
                $('.grid-read, .grid-edit-a', $(v)).hide();
                $(v).addClass('notvalidrow');
                self.trEditRow = $(v);
                self.bItemEdit = true;
            }
        });
    };

    //show Add rows
    this.addRows = function()
    {
        if (self.bItemEdit)
        {
            var answer = confirm(opts.msgEditConflict());
            if (answer)
            {
                var cancelEle = $(opts.editIcons.cancel, self.trEditRow);
                self.cancelEdit(cancelEle);
                self.fn_tblAdd();
            }
        } else
        {
            self.fn_tblAdd();
        }
    };

    this.fn_tblAdd = function()
    {
        self.bItemEdit = true;
        self.tblAddNew = true;
        self.clearAddControls();
        $('tbody tr span', $tblAdd).html('');
        $tblAdd.show();
    };

    this.nonValidAdd = function()
    {
        $tblAdd.addClass('notvalidadd');
    };

    this.init = function()
    {
        this.setEditIcons();
        var rowNV = $('.notvalidrow', $tbl).length;
        if (rowNV > 0)
        {
            self.bItemEdit = true;
            self.trEditRow = $('.notvalidrow', $tbl);
        }
        if ($tblAdd.hasClass('notvalidadd'))
        {
            self.bItemEdit = true;
            self.tblAddNew = true;
        }
    };
}

// ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


// ******************************************************
//                      HELPERS
// ******************************************************
