if (typeof (cms) == "undefined") {
    var cms = {};
}

cms.menu = {
    //general options and settings
    opts: {
        selectedItem: null,
        parentItem: 'htxtParentID',
        currentItem: 'htxtCurrentID',
        userRights: 'htxtUserRights',
        editMode: 'htxtEditMode',
        divButtons: 'divMenuButtons',
        divControls: 'divMenuControls',
        btnEditItm: 'btnEditItm',
        btnAddItm: 'btnAddSubItm',
        btnDeleteItm: 'btnDeleteMenuItem',
        rawMenuData: 'htxtMenuData',
        menuData: null
    },

    //select tree node
    selectNode: function (obj) {
        var me = this,
            divButtons = $('div[id$="' + me.opts.divButtons + '"]')[0],
            btnEditItm = $('[id$="' + me.opts.btnEditItm + '"]')[0],
            btnAddItm = $('[id$="' + me.opts.btnAddItm + '"]')[0],
            btnDeleteItm = $('[id$="' + me.opts.btnDeleteItm + '"]')[0],
            divControls = $('div[id$="' + me.opts.divControls + '"]')[0],
            currentItem = $('[id$="' + me.opts.currentItem + '"]')[0],
            parentItem = $('[id$="' + me.opts.parentItem + '"]')[0],
            userRights = $('[id$="'+ me.opts.userRights +'"]')[0];

        $(divControls).show();

        if (obj.menu_id == 0) {
            //hide edit menu item option
            $('input[id$="radEditItm"]').hide();
            $('label[for$="radEditItm"]').hide();

            //pre-select "add child menu item"
            $('input[id$="radAddItm"]').attr('checked', 'checked');

            $(btnDeleteItm).hide();
            me.opts.selectedItem = obj;
        }
        else {
            //hide edit menu item option
            $('input[id$="radEditItm"]').show();
            $('label[for$="radEditItm"]').show();

            //pre-select "add child menu item"
            $('input[id$="radEditItm"]').attr('checked', 'checked');

            $(btnDeleteItm).show();
        }

        currentItem.value = obj.menu_id;
        parentItem.value = obj.parent_id;
        userRights.value = obj.user_rights;

        $.each(me.opts.menuData, function (i, o) {
            if (o.menu_id == obj.menu_id) {
                me.opts.selectedItem = o;
            }
        });

        if (obj.menu_id == 0) {
            me.addChildItem();
        }
        else {
            me.editItem();
        }
    },

    //edit menu item
    editItem: function () {
        var me = this,
            divControls = $('div[id$="'+ me.opts.divControls +'"]')[0];

        $('[id$="' + me.opts.editMode + '"]').val('2');
        me.fillMenuControls();
    },

    //confirmation: delete menu item
    confirmDeleteMenuItem: function () {
        var me = this,
            msg = "Are you sure you want to delete this menu iem?";
        return confirm(msg);
    },

    //add child item
    addChildItem: function () {
        var me = this,
            divControls = $('div[id$="' + me.opts.divControls + '"]')[0];

        me.clearMenuControls();
        $('[id$="' + me.opts.editMode + '"]').val('1');
    },

    //fill menu controls
    fillMenuControls: function () {
        var me = this,
            o = me.opts.selectedItem;

        //menu title
        $('[id$="txtMenuTitle"]').val(o.menu_title);

        //target page
        $('option', $('select[id$="cboTargetPage"]')).each(function () {
            this.selected = (this.value == o.page_id);
        });

        //target portal
        $('input[name$="rblTargetPortal"]').each(function () {
            this.checked = (this.value == o.target_portal_id);
        });

        /*
        if (o.target_portal_id == "2") {
            $('[id$="trUserRights"]').show();
        }
        else {
            $('[id$="trUserRights"]').hide();
        }
        */

        //sort order 
        $('[id$="txtSortOrder"]').val(o.sort_order);

        //user rights
        var oUR = parseInt(o.user_rights);
        $('input[name$="chklstUserRights"]').each(function () {
            var mUR = parseInt(this.value);
            this.checked = (mUR & oUR);
        });
    },

    //clear menu controls
    clearMenuControls: function () {
        var me = this,
            o = me.opts.menuData;

        //menu title
        $('[id$="txtMenuTitle"]').val('');

        //target page
        $('option', $('select[id$="cboTargetPage"]')).each(function () {
            this.selected = (this.value == "0");
        });

        //target portal
        $('input[name$="rblTargetPortal"]').each(function () {
            this.checked = (this.value == "1");
        });

        //sort order 
        $('[id$="txtSortOrder"]').val('');

        //user rights
        $('input[name$="chklstUserRights"]').each(function () {
            this.checked = false;
        });
        //$('[id$="trUserRights"]').hide();
        $('input[id$="' + me.opts.userRights + '"]').val('0');
    },

    //hide/show user rights options
    hideUserRights: function (obj) {
        var me = this;
        /*
        if (obj.value == "2") {
            $('[id$="trUserRights"]').show();
        }
        else {
            $('[id$="trUserRights"]').hide();
            $('input[id$="' + me.opts.userRights + '"]').val('0');
        }
        */
    },

    //bind onclick event to "hideUserRights"
    bindHideUserRights: function () {
        var me = this;
        $('input[name$="rblTargetPortal"]').each(function () {
            $(this).bind({
                click: function () {
                    me.hideUserRights(this);
                }
            });
        });
    },

    //create menu data object
    getMenuDataObj: function () {
        var me = this,
            rawData = $('input[id$="' + me.opts.rawMenuData + '"]')[0];
        me.opts.menuData = eval(rawData.value);
    },

    //auto-update user rights values on selection
    bindUserRightsUpdate: function () {
        var me = this;
        $('input[name$="chklstUserRights"]').each(function () {
            $(this).bind({
                click: function () {
                    me.userRightsUpdate();
                }
            });
        });
    },

    userRightsUpdate: function () {
        var me = this,
            ur = 0,
            userRights = $('[id$="'+ me.opts.userRights +'"]')[0];

        $('input[name$="chklstUserRights"]:checked').each(function () {
            ur = ur + parseInt(this.value);
        });

        $(userRights).val(ur);
    },

    //initialization functions
    init: function () {
        var me = this;
        $(document).ready(function () {
            me.getMenuDataObj();
            me.bindHideUserRights();
            me.bindUserRightsUpdate();
        });
        
    }
}