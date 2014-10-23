// Render dropdown menus
$(document).ready(function () {
    var menuEl = Ext.get('simple-horizontal-menu') || null;

    if (menuEl) {
        new Ext.ux.Menu('simple-horizontal-menu', {
            transitionType: 'slide',
            direction: 'horizontal', // default
            delay: 0.2,              // default
            autoWidth: true,         // default
            transitionDuration: 0.3, // default
            animate: true,           // default
            currentClass: 'current'  // default
        });
    }

    var menuCMS = Ext.get('cms-horizontal-menu') || null;

    if (menuCMS) {
        new Ext.ux.Menu('cms-horizontal-menu', {
            transitionType: 'slide',
            direction: 'horizontal', // default
            delay: 0.2,              // default
            autoWidth: true,         // default
            transitionDuration: 0.3, // default
            animate: true,           // default
            currentClass: 'current'  // default
        });
    }

    //adjust buttons style
    Ext.select("input[type=submit], input[type=button]", true).each(function (el) {
        el.setStyle({
            padding: '2px 6px'
        });
    });

    $('div[id$="fixedMenuBar"]').css({
        position: 'fixed',
        left: '0px',
        top: '0px'
    });
});


var appmenu = {

    //general options and settings
    opts: {
        menuContainerID: 'simple-horizontal-menu',
        topToolbarID: 'topToolbar',
        message: 'You are about to leave the patient\'s record. \nIf you continue, you will need to re-lookup the patient. \nDo you want to continue?'
    },

    //add the alert to the menu items that have the "CLOSES_PATIENT" attribute
    menuConfirClosePatient: function () {
        var me = this,
            itms = $('a[CLOSES_PATIENT="CLOSES_PATIENT"]', $('[id$="'+ me.opts.menuContainerID +'"]')),
            itms2 = $('div[CLOSES_PATIENT="CLOSES_PATIENT"]', $('[id$="' + me.opts.topToolbarID + '"]'));
        $.each(itms, function (i, a) {
            if ($(a).attr('href') != "#") {
                if (selectedPatientID.length > 0) {
                    $(a).bind({
                        click: function () {
                            var answer = confirm(me.opts.message);
                            return answer;
                        }
                    });
                }
            }
            else {
                if (selectedPatientID.length > 0) {
                    var m_onclick = $(a).attr('onclick');
                    $(a).attr('onclick', 'if(appmenu.confirmClosePatient()){' + m_onclick + '}');
                }
            }
        });

        $.each(itms2, function (i, d) {
            if (selectedPatientID.length > 0) {
                var m_onclick = $(d).attr('onclick');
                $(d).attr('onclick', 'if(appmenu.confirmClosePatient()){' + m_onclick + '}');
            }
        });
    },

    //confirm 
    confirmClosePatient: function () {
        var me = this;
        var answer = confirm(me.opts.message);
        return answer;
    },

    //initializing functions
    init: function () {
        var me = this;
        me.menuConfirClosePatient();
    }
};