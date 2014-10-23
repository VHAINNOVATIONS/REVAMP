var sysreq = {
    opts: {
        updateLinkID: 'lnkUpdateBrowser',
        browserDDID: 'selBrowser',
        browsersURL: [
            null,
            'http://windows.microsoft.com/en-us/internet-explorer/download-ie',
            'http://www.google.com/chrome',
            'http://www.mozilla.org/en-US/firefox/new/',
            'http://www.apple.com/safari'
        ]
    },

    updateBrowser: function (obj)
    {
        var me = this,
            lnk = $('a[id$="' + me.opts.updateLinkID + '"]')[0],
            sel = obj;

        if (sel.selectedIndex < 1) {
            $(lnk).attr('href', '#').attr('onclick', 'return false;');
            return false;
        }
        else {
            var m_val = parseInt($(sel).val()),
                m_href = me.opts.browsersURL[m_val];
            $(lnk).attr('href', m_href).attr('onclick','return true;');
            return true;
        }
    }
};

$(document).ready(function ()
{
    var menuEl = Ext.get('simple-horizontal-menu') || null;

    if (menuEl)
    {
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

    //adjust buttons style
    Ext.select("input[type=submit], input[type=button]", true).each(function(el)
    {
        el.setStyle({
            padding: '2px 6px'
        });
    });
	
    //$('div[id$="fixedMenuBar"]').show();
    /*
    position:absolute; left: -5000px; top:102px;
    */
    var _mpos = $('[id$="divHeader"]').position();

    $('div[id$="fixedMenuBar"]').css({
        position: 'absolute',
        left: _mpos.left + 'px',
        top: '102px'
    });

});

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

//Text input masks

//date mask mm/dd/yyyy
//to call the function: onkeypress="return maskDate(event, this.value, this);"
function maskDate(e, str, textbox) {

    // #  #  /  #  #  /  #  #  #  #
    // 0  1  2  3  4  5  6  7  8  9

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[2] = '/';
    delim[5] = '/';

    var len = str.length;

    if (charCode >= 48 && charCode <= 57) {
        if (len == 0 && parseInt(String.fromCharCode(charCode)) > 1) {
            textbox.value = '0';
        }

        //check date -- optional
        if (arguments[2] && typeof (arguments[2]) == 'boolean') {
            if (len == 5) {
                m_month = parseInt(str.substring(0, 2), 10);
                m_day = parseInt(str.substring(3, 5), 10);

                if (m_day > 30 && (m_month == 2 || m_month == 4 || m_month == 6 || m_month == 9 || m_month == 11)) {
                    textbox.value = str.substring(0, 3) + '0' + str.substring(3, 4) + '/' + str.substring(4);
                    return true;
                }
            }
        }


        if (delim[len]) {
            textbox.value = str + delim[len];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }
    return false;
}

function maskZipCode(e, str, textbox) {

    // #  #  #  #  #  -  #  #  #  #  
    // 0  1  2  3  4  5  6  7  8  9

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[5] = '-';

    var len = str.length;

    if (charCode >= 48 && charCode <= 57) {

        if (delim[len]) {
            textbox.value = str + delim[len];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }
    return false;
}

//phone mask (###)###-####
//to call the function: onkeypress="return maskPhone(event, this.value, this);"
function maskPhone(e, str, textbox) {

    // (  #  #  #  )  #  #  #  -  #   #   #   #
    // 0  1  2  3  4  5  6  7  8  9  10  11  12    

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[0] = '(';
    delim[4] = ')';
    delim[8] = '-';


    if (charCode >= 48 && charCode <= 57) {
        if (delim[str.length]) {
            textbox.value = str + delim[str.length];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }

    return false;
}

//FMP/SSN mask ##/######### -- for Patient Demographics Screen
//to call the function: onkeypress="return maskFMPSSN(event, this.value, this);"
function maskFMPSSN(e, str, textbox) {

    // #  #  /  #  #  #  #  #  #  #   #   #
    // 0  1  2  3  4  5  6  7  8  9  10  11  

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[2] = '/';

    if (charCode >= 48 && charCode <= 57) {
        if (delim[str.length]) {
            textbox.value = str + delim[str.length];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }

    return false;
}

function maskFMPSSN2(e, str, textbox) {

    // #  #  /  #  #  #  -  #  #  -   #  #   #   #
    // 0  1  2  3  4  5  6  7  8  9  10  11  12  13  

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[2] = '/';
    delim[6] = '-';
    delim[9] = '-';

    if (charCode >= 48 && charCode <= 57) {
        if (delim[str.length]) {
            textbox.value = str + delim[str.length];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }

    return false;
}

function maskFMPSSN3(e, str, textbox) {

    // #  #  #  -  #  #  -  #  #  #  #
    // 0  1  2  3  4  5  6  7  8  9  10 

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[3] = '-';
    delim[6] = '-';

    if (charCode >= 48 && charCode <= 57) {
        if (delim[str.length]) {
            textbox.value = str + delim[str.length];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }

    return false;
}

//date mask mm/yyyy
//to call the function: onkeypress="return maskMonthYear(event, this.value, this);"
function maskMonthYear(e, str, textbox) {

    // #  #  /  #  #  #  #  #
    // 0  1  2  3  4  5  6  7

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[2] = '/';

    var len = str.length;

    if (charCode >= 48 && charCode <= 57) {
        if (len == 0 && parseInt(String.fromCharCode(charCode)) > 1) {
            textbox.value = '0';
        }

        if (delim[len]) {
            textbox.value = str + delim[len];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }
    return false;
}

//date mask mm/dd
//to call the function: onkeypress="return maskMonthDay(event, this.value, this);"
function maskMonthDay(e, str, textbox) {

    // #  #  /  #  #  
    // 0  1  2  3  4  

    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    var delim = new Array();
    delim[2] = '/';

    var len = str.length;

    if (charCode >= 48 && charCode <= 57) {
        if (len == 0 && parseInt(String.fromCharCode(charCode)) > 1) {
            textbox.value = '0';
        }

        if (delim[len]) {
            textbox.value = str + delim[len];
        }
        return true;
    }

    if (charCode == 8 || charCode == 9) {
        return true;
    }
    return false;
}

//accept only numbers, dash, tab and spacebar
function onlyNumbers(e) {
    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    if (charCode >= 48 && charCode <= 57) {
        return true;
    }

    if (charCode == 8 || charCode == 9 || charCode == 45) {
        return true;
    }

    return false;
}

//maintain scrollbar position
var scrollPos = {

    //opts
    _opts: {
        divID: 'div-page-contents',
        txtYID: 'htxtScrollY',
        txtXID: 'htxtScrollX'
    },

    //get scroll position
    getScrollPos: function () {
        var me = this;
        $('[id$="' + me._opts.txtYID + '"]').val($('[id$="' + me._opts.divID + '"]').get(0).scrollTop);
        $('[id$="' + me._opts.txtXID + '"]').val($('[id$="' + me._opts.divID + '"]').get(0).scrollLeft);
        return true;
    },

    //set scroll position
    setScrollPos: function () {
        var me = this,
            txtYscroll = $('[id$="' + me._opts.txtYID + '"]')[0],
            txtXscroll = $('[id$="' + me._opts.txtXID + '"]')[0];

        //set Y position
        if (txtYscroll) {
            if (txtYscroll.value != '') {
                $('[id$="' + me._opts.divID + '"]').get(0).scrollTop = txtYscroll.value;
            }
        }

        //set X position
        if (txtXscroll) {
            if (txtXscroll.value != '') {
                $('[id$="' + me._opts.divID + '"]').get(0).scrollLeft = txtXscroll.value;
            }
        }
    },

    init: function () {
        $(document).ready(function () {
            setTimeout(function () {
                scrollPos.setScrollPos();
            }, 300);
        });
    }

};

var clearStatusDiv = function (delaySecs) {
    Sys.onReady(function () {
        var clDiv = setTimeout('$(\'div[id$="divStatus"]\')[0].innerHTML = \'\'', delaySecs * 1000);
    });
};
