var moduleassign =
{
    _opts: {
        imgExpandSrc: 'Images/down-arrow.png',
        imgCollapseSrc: 'Images/up-arrow.png',

        divModuleItemsClass: '.div-module-items',
        iconExpandCollapseClass: '.icon-expand-collapse',
        chkbxClass: '.chk-select-module',
        chkbxGroup: '.chk-select-group',

        slideSpeed: 'fast',

        mainContainerID: 'divAssignModulesWrapper',
        selectedModulesID: 'htxtSelectedModules',
        iconStateID: 'htxtIconState',
        chkbxStateID: 'htxtChkbxState',
        divStateID: 'htxtDivState'
    },

    //format alternating rows
    formatAlternatingRows: function()
    {
        $('.module-items tr:nth-child(even)').removeClass('module-items-odd').addClass('module-items-even');
        $('.module-items tr:nth-child(odd)').removeClass('module-items-even').addClass('module-items-odd');
    },

    //get selected modules string
    getSelectedModules: function()
    {
        var me = this,
            htxt = $('input[id$="' + me._opts.selectedModulesID + '"]')[0],
            divWrapper = $('div[id="' + me._opts.mainContainerID + '"]')[0];
        if (htxt)
        {
            if (htxt.value.length > 0)
            {
                var modSelected = htxt.value.split('^').slice(0, -1),
                    chkbxs = $('input[type="checkbox"]', $(divWrapper));

                //clear all checkboxes first
                $(chkbxs).removeAttr('checked');

                //set checkboxes to selected modules
                $.each(modSelected, function(i, s)
                {
                    $.each(chkbxs, function(n, c)
                    {
                        if (s == c.value)
                        {
                            c.checked = true;
                        }
                    });
                });
            }
        }
    },

    //get components state
    getCmpState: function()
    {
        var me = this,
            arrIconState = [],
            arrDivState = [],
            arrChkbxState = [],
            hIconState = $('input[id$="' + me._opts.iconStateID + '"]')[0],
            hDivState = $('input[id$="' + me._opts.divStateID + '"]')[0],
            hChkbxState = $('input[id$="' + me._opts.chkbxStateID + '"]')[0];

        //get collapse/expand icons state
        $('img' + me._opts.iconExpandCollapseClass).each(function(i, o)
        {
            var obj = {
                id: o.id,
                className: o.getAttribute('class'),
                src: o.src
            };
            arrIconState.push(obj);
        });

        //get modules div state
        var arrDivState = [];
        $('div' + me._opts.divModuleItemsClass).each(function(i, o)
        {
            var obj = {
                id: o.id,
                display: $(o).css('display')
            };
            arrDivState.push(obj);
        });

        //get checkboxes state
        var arrChkbxState = [];
        $('input[type="checkbox"]' + me._opts.chkbxClass).each(function(i, o)
        {
            var obj = {
                id: o.id,
                checked: o.checked,
                value: o.value
            };
            arrChkbxState.push(obj);
        });

        hIconState.value = JSON.stringify(arrIconState);
        hDivState.value = JSON.stringify(arrDivState);
        hChkbxState.value = JSON.stringify(arrChkbxState);
    },

    //get components state
    setCmpState: function()
    {
        var me = this,
            arrIconState = [],
            arrDivState = [],
            arrChkbxState = [],
            hIconState = $('input[id$="' + me._opts.iconStateID + '"]')[0],
            hDivState = $('input[id$="' + me._opts.divStateID + '"]')[0],
            hChkbxState = $('input[id$="' + me._opts.chkbxStateID + '"]')[0];

        //build json object for icon state
        if (hIconState.value.length > 0)
        {
            arrIconState = jQuery.parseJSON(hIconState.value);
        }

        //build json object for div state
        if (hDivState.value.length > 0)
        {
            arrDivState = jQuery.parseJSON(hDivState.value);
        }

        //restore icon state
        $.each(arrIconState, function(i, o)
        {
            var ele = $('[id="' + o.id + '"]')[0];
            if (ele)
            {
                ele.src = o.src;
                ele.setAttribute('class', o.className);
            }
        });

        //restore div state
        $.each(arrDivState, function(i, o)
        {
            var ele = $('[id="' + o.id + '"]')[0];
            if (ele)
            {
                $(ele).css({ display: o.display });
            }
        });

    },

    //set collapse/expand icons listeners
    setCollapseExpand: function()
    {
        var me = this;
        $('img' + me._opts.iconExpandCollapseClass).css({ cursor: 'pointer' }).bind({
            click: function()
            {
                me.checkCollapseExpand(this);
            }
        });
    },

    //set check/uncheck all listener
    setCheckAllModules: function()
    {
        var me = this;

        $('input[type="checkbox"].chk-select-group').bind({
            click: function()
            {
                me.CheckAllModules(this);
            }
        });
    },

    // check/uncheck all modules
    CheckAllModules: function(obj)
    {
        var me = this,
		    mGroup = $(obj).attr('groupid');

        $('input[type="checkbox"]', $('div' + me._opts.divModuleItemsClass + '[groupid="' + mGroup + '"]')).each(function()
        {
            if (!this.disabled) {
                this.checked = obj.checked;
            }
        });
    },

    //uncheck group's header "Check All" if a module item is unchecked
    setUncheckAll: function()
    {
        var me = this;
        $('input[type="checkbox"].chk-select-module').bind({
            click: function()
            {
                me.uncheckHeaderChkbx(this);
                me.getCmpState();
            }
        });
    },

    uncheckHeaderChkbx: function(obj)
    {
        var me = this,
		    mGroup = $(obj).attr('groupid'),
			chkbxHeader = $('input[type="checkbox"][groupid="' + mGroup + '"].chk-select-group');
        if (!obj.checked)
        {
            $(chkbxHeader).removeAttr('checked');
        }
    },

    //check collapsed/expanded state
    checkCollapseExpand: function(obj)
    {
        var me = this;
        if ($(obj).hasClass('collapsed'))
        {
            me.expandModuleGroup(obj);
        }
        else
        {
            me.collapseModuleGroup(obj);
        }
    },

    //collapse module group
    collapseModuleGroup: function(obj)
    {
        var me = this,
		    mGroup = $(obj).attr('groupid');

        $('div' + me._opts.divModuleItemsClass + '[groupid="' + mGroup + '"]').slideUp(me._opts.slideSpeed, function()
        {
            obj.src = me._opts.imgExpandSrc;
            $(obj).removeClass('expanded').addClass('collapsed');

            setTimeout(function()
            {
                me.getCmpState();
            }, 200);
        });
    },

    //expan module group
    expandModuleGroup: function(obj)
    {
        var me = this,
		    mGroup = $(obj).attr('groupid');

        $('div' + me._opts.divModuleItemsClass + '[groupid="' + mGroup + '"]').slideDown(me._opts.slideSpeed, function()
        {
            obj.src = me._opts.imgCollapseSrc;
            $(obj).removeClass('collapsed').addClass('expanded');

            setTimeout(function()
            {
                me.getCmpState();
            }, 200);
        });
    },

    //unbind "onclick" event from the checkboxes if the page is in read-only mode
    unbindOnclick: function(bReadOnly)
    {
        var me = this;
        if (bReadOnly)
        {
            $(me._opts.chkbxClass + ', ' + me._opts.chkbxGroup).unbind('click');
        }
        return;
    },

    init: function()
    {
        var me = this;
        $(function()
        {
            setTimeout(function()
            {
                me.setCollapseExpand();
                me.setCheckAllModules();
                me.setUncheckAll();
                //me.getSelectedModules();
                me.setCmpState();
                me.formatAlternatingRows();
            }, 100);
        });
    }
};