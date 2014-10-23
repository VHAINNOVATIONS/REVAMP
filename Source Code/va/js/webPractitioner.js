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

    updateBrowser: function (obj) {
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
            $(lnk).attr('href', m_href).attr('onclick', 'return true;');
            return true;
        }
    }
};

//DETECT BROWSER
var BrowserDetect = {
    init: function () {
        this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
        this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";
        this.OS = this.searchString(this.dataOS) || "an unknown OS";
    },
    searchString: function (data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return data[i].identity;
            }
            else if (dataProp)
                return data[i].identity;
        }
    },
    searchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index == -1) return;
        return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
    },
    dataBrowser: [
		{
		    string: navigator.userAgent,
		    subString: "Chrome",
		    identity: "Chrome"
		},
		{
		    string: navigator.userAgent,
		    subString: "OmniWeb",
		    versionSearch: "OmniWeb/",
		    identity: "OmniWeb"
		},
		{
		    string: navigator.vendor,
		    subString: "Apple",
		    identity: "Safari",
		    versionSearch: "Version"
		},
		{
		    prop: window.opera,
		    identity: "Opera",
		    versionSearch: "Version"
		},
		{
		    string: navigator.vendor,
		    subString: "iCab",
		    identity: "iCab"
		},
		{
		    string: navigator.vendor,
		    subString: "KDE",
		    identity: "Konqueror"
		},
		{
		    string: navigator.userAgent,
		    subString: "Firefox",
		    identity: "Firefox"
		},
		{
		    string: navigator.vendor,
		    subString: "Camino",
		    identity: "Camino"
		},
		{		// for newer Netscapes (6+)
		    string: navigator.userAgent,
		    subString: "Netscape",
		    identity: "Netscape"
		},
		{
		    string: navigator.userAgent,
		    subString: "MSIE",
		    identity: "Explorer",
		    versionSearch: "MSIE"
		},
		{
		    string: navigator.userAgent,
		    subString: "Gecko",
		    identity: "Mozilla",
		    versionSearch: "rv"
		},
		{ 		// for older Netscapes (4-)
		    string: navigator.userAgent,
		    subString: "Mozilla",
		    identity: "Netscape",
		    versionSearch: "Mozilla"
		}
    ],
    dataOS: [
		{
		    string: navigator.platform,
		    subString: "Win",
		    identity: "Windows"
		},
		{
		    string: navigator.platform,
		    subString: "Mac",
		    identity: "Mac"
		},
		{
		    string: navigator.userAgent,
		    subString: "iPhone",
		    identity: "iPhone/iPod"
		},
		{
		    string: navigator.platform,
		    subString: "Linux",
		    identity: "Linux"
		}
    ]

};
BrowserDetect.init();

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined'
      ? args[number]
      : match
        ;
    });
};

//functions that will be fired only when the DOM is ready

$(document).ready(function () {
    setTimeout(function () {
        wp.adjustMain();
        wp.fixLeftPaneHeight();
        wp.fixMainDiv();
    }, 1);
});

$(window).bind({
    resize: function () {
        wp.adjustMain();
        wp.fixLeftPaneHeight();
        wp.fixMainDiv();
        if (typeof (soap) != "undefined") {
            if (typeof (soap.resizeTxtSOAP) != "undefined") {
                soap.resizeTxtSOAP();
            }
        }

        wp.resizeVerticalMenu();

        $('div[id$="divUsersList"]').css({ height: $('div[id$="div-page-contents"]').height() - 63 });

        /********************************************
        *    ADJUST AGGREGATE REPORTS SCREEN       *
        ********************************************/
        if (window.location.href.indexOf('aggregate_report.aspx') > -1) {
            if (typeof formatScreen != "undefined") {
                formatScreen();
            }
        }
    }
});


//namespace for wp //WebPractitioner object
var wp = new function () {
    // PRIVATE methods and properties


    // PUBLIC methods and properties

    //maintain scroll position
    this.maintainScroll = function (eleID) {
        var ele = ($('div[id$="' + eleID + '"]'))[0];
        ele.scrollTop = $('input[id$="htxtScrollY"]').val();
    };

    //get scroll position
    this.getScroll = function (eleID) {
        var ele = ($('div[id$="' + eleID + '"]'))[0];
        $('input[id$="htxtScrollY"]').val(ele.scrollTop);
        $('input[id$="htxtScrollX"]').val(ele.scrollLeft);
    };

    //adjust left pane
    this.resizeVerticalMenu = function () {
        var vm = $('div[id$="leftPane"]'),
            vmPos = vm.position();

        if ((BrowserDetect.browser).indexOf("Explorer") > -1) {
            if (typeof VerticalMenu != "undefined") {
                VerticalMenu.setHeight(parseInt($(window).height() - vmPos.top - 106));
            }
        }
    }

    //adjust workspace panel
    this.dimWorkspacePanel = function () {
        var m_pnlWorksapce = Ext.getCmp('pnlWorkspace');
        if (typeof m_pnlWorksapce != "undefined") {
            var pHeight = $(window).height() - $('div[id$="fixedMenuBar"]').outerHeight() - 3,
                    pWidth = $(window).width(),
                    mPos = $('div[id$="div-page-contents"]').position();

            m_pnlWorksapce.setHeight(pHeight);
            m_pnlWorksapce.setWidth(pWidth);

            $('div[id$="div-page-contents"]').css({ height: (pHeight - 45) + 'px' });

            m_pnlWorksapce.doLayout();
        }
    }


    //adjust mainContents div
    this.adjustMain = function () {
        var lp_width = 0;
        var mc_padding = 0;
        var mc_width = 0;
        if ($G('leftPane')) {
            lp_width += parseInt($('div[id$="leftPane"]').outerWidth());
        }

        mc_width = $(window).width() - lp_width - 5;

        if (mc_width > 0) {

            $('input[id$="htxtMainDivWidth"]').val(mc_width + 'px');
        }

        $('input[type="submit"], input[type="button"]').css({ padding: '2px 6px' });
        setTimeout("wp.dimWorkspacePanel();", 100);
    };

    this.fixLeftPaneHeight = function () {
        return;
    };

    this.fixMainDiv = function () {
        var h = 32 + 5;
        var h2 = 32 + 5;
        if ($G('topToolbar')) {
            h = h + 42;
            h2 = h2 + 42;
        }
        if ($G('div-demo-logoff-container')) {
            h = h + 37;
        }
        if ($G('divLastModified')) {
            h = h + $('div[id$="divLastModified"]').innerHeight() + 8;
        }

        $('div[id$="divTopSpacer"]').css({ height: h2 + 'px' });
    };

    // clear/reset selections on checkbox lists
    this.resetChkList = function (opts) {
        for (var n in opts) {
            $('input[' + n + '$="' + opts[n] + '"]').each(function () {
                this.checked = false;
            });
        }
    }

}();


/*
 ----------------------------------------------------
    Relocated from Master Page
    -Used in Lookup popups-
 ----------------------------------------------------
*/
function OnGVLinkClick(id, colheader, propname, propval, rowid) {
    if (id == 'gvPatients') {
        __doPostBack("PATIENT_LOOKUP", rowid);
    } else if (id == 'gvUsers') {
        __doPostBack("USER_LOOKUP", rowid);
    } else if (id == 'gvPortalUsers') {
        __doPostBack("PORTAL_PATIENT_LOOKUP", rowid);
    } else if (id == 'gvReferralClinics') {
        __doPostBack("REFERRAL_CLINIC_MGMT", rowid);
    }
}

//get radio button selected value
function getSelRblValue(rblName) {
    //rblSearchCase: 1) all cases 2) open cases 3) closed cases

    var rbList = new Array(); //radio buttons collection
    var colImput = document.getElementsByTagName('input'); //input elements collection
    if (colImput) {
        for (var a = 0; a < colImput.length; a++) {
            var m_input = colImput[a].name;
            if (m_input.indexOf(rblName) != -1) {
                rbList.push(colImput[a]);
            }
        }
        if (rbList.length > 0) {
            for (b = 0; b < rbList.length; b++) {
                if (rbList[b].checked) {
                    return rbList[b].value;
                }
            }
        }
        return "2"; //defaulted to 2 (open case)
    }
}

// ----------------------------------------------------
//    Add some methods to String prototype
// ----------------------------------------------------

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}

String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}

String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}

// Globals
var maintainScroll = true;
var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;


// need this to force Crystal Reports
// toolbar's width to fit in the popup
var crtoolbar = [];
var crClassName = 'crtoolbar'; //toolbar css class name
var crWidth = '773px';

function getCRtoolbars(crClass) {
    var reClass = new RegExp('^(' + crClass + ')$', 'gi');
    var x = document.getElementsByTagName('*');
    if (x) {
        for (var i in x) {
            if (x[i].className) {
                if (reClass.test(x[i].className)) {
                    crtoolbar.push(x[i]);
                }
            }
        }
    }
}

function adjustCRtoolbar() {
    getCRtoolbars(crClassName);
    for (var i = 0; i < crtoolbar.length; i++) {
        crtoolbar[i].style.width = crWidth;
    }
}


// ----------------------------------

if (!window.getComputedStyle) {
    window.getComputedStyle = function (el, pseudo) {
        this.el = el;
        this.getPropertyValue = function (prop) {
            var re = /(\-([a-z]){1})/g;
            if (prop == 'float') prop = 'styleFloat';
            if (re.test(prop)) {
                prop = prop.replace(re, function () {
                    return arguments[2].toUpperCase();
                });
            }
            return el.currentStyle[prop] ? el.currentStyle[prop] : null;
        }
        return this;
    }
}


// 02/15/2011 - Add "document.getElementByClassName(class)" function
if (document.getElementsByClassName == undefined) {
    document.getElementsByClassName = function (className) {
        var hasClassName = new RegExp("(?:^|\\s)" + className + "(?:$|\\s)");
        var allElements = document.getElementsByTagName("*");
        var results = [];

        var element;
        for (var i = 0; (element = allElements[i]) != null; i++) {
            var elementClass = element.className;
            if (elementClass && elementClass.indexOf(className) != -1 && hasClassName.test(elementClass))
                results.push(element);
        }

        return results;
    }
}

function hasClass(ele, cls) {
    return ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}
function addClass(ele, cls) {
    if (!this.hasClass(ele, cls)) ele.className += " " + cls;
}
function removeClass(ele, cls) {
    if (hasClass(ele, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        ele.className = ele.className.replace(reg, ' ');
    }
}
//

function __getStyle(el, styleProp) {
    //var x = getObject(el);
    var x = document.getElementById(el);
    if (x) {
        if (x.currentStyle) {
            var y = x.currentStyle[styleProp];
        }
        else if (window.getComputedStyle) {
            var y = window.getComputedStyle(x, null).getPropertyValue(styleProp);
        }

        if (isNaN(parseInt(y))) {
            if (styleProp.toLowerCase() == 'width') {
                y = x.offsetWidth;
                return parseInt(y);
            }
            else if (styleProp.toLowerCase() == 'height') {
                y = x.offsetHeight;
                parseInt(y);
            }
        }
        else {
            return parseInt(y);
        }
    }
    return 0;
}


var txt = "";
function getFocus(eleId) {
    $('[id$="'+ eleId  +'"]').focus();
}


/* --- Centers welcome screen on statup --- */
function adjustMainContentsDiv() {
    var navPane = $('[id$="leftPane"]')[0],
        dW = $('[id$="mainContents"]')[0];
    if (navPane) {
        dW.style.width = 'auto';
    }
    else {
        dW.style.width = '100%';
        dW.style.margin = '0 auto';
        dW.style.textAlign = 'center';
    }
}

function toggleModView(m_id, blanket) {
    if (blanket) {
        blanket_size();;
        toggle('blanket');
    }

    fixEmptyAnchors(m_id)
    window_pos(m_id);
    toggle(m_id);
}

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

function checkMonthInput(e, str, textbox) {
    var evt = e || window.event;
    var charCode = evt.which || evt.keyCode;

    if (str.length == 2) {
        var m = parseInt(str);
        if (m > 12) {
            alert('Invalid month input.');
            textbox.value = '';
        }
    }
}

/*
*  -----------------------------------------------------------
*                  GET ELEMENT'S OFFSET VALUES
*  -----------------------------------------------------------
*/
function getOffset(el) {
    var _x = 0;
    var _y = 0;
    while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
        _x += el.offsetLeft - el.scrollLeft;
        _y += el.offsetTop - el.scrollTop;
        el = el.parentNode;
    }
    return { top: _y, left: _x };
}

// ------------------------------------------------------------

function getObject(id) {
    var obj = $('[id$="'+ id +'"]')[0];
    
    if (obj) {
        return obj;
    }
    return null;
}

//alias getObjet() to $G()
window.$G = window.getObject;

//ReVamp New Encounter Confirm ---------------------------------------------
function newEncounterConfirm(ele, strNewType) {

    if (ele.getAttribute('linkhref')) {
        var newencounternote = document.getElementById('newencounternote'),
              //newTypeTitle = document.getElementById('newTypeTitle'),
            newType = document.getElementById('newType'),
                  btnYes = document.getElementById('btnYes'),
                  linkhref = ele.getAttribute('linkhref');



        //--------------------------------------------------------
        //newTypeTitle.innerHTML = strNewType.toUpperCase();
        newType.innerHTML = strNewType;
        btnYes.setAttribute('onclick', 'checkYesButton("' + linkhref + '");');
        btnYes.removeAttribute('disabled');

        if (typeof (winNewEncounter) != "undefined") {
            $('.x-window-header-text', $('#winNewEncounter')).text('NEW ' + strNewType.toUpperCase());
            winNewEncounter.show();
        }

        getFocus('lnkAlertNewEncounter');
    }
}

checkYesButton = function (lnk) {
    if (parseInt($('select[id$="ddlModality"]').val()) < 0) {
        return false;
    }
    else {
        var opt = 'op1=' + $('[id$="ddlModality"]').val(),
            newlnk = lnk.replace('op1=6', opt);

        window.location.href = newlnk;
        return true;
    }
};

//Fill in without postback --------------------------------------------
//build the data array ------------------------------------------------
function buildArray(rawText) {
    if (rawText.length > 3) {
        var pos0 = rawText.indexOf('}', 1);
        var mFieldNames = rawText.substring(1, pos0);
        var fieldnames = mFieldNames.split(',');
        var mData = rawText.substring(pos0 + 1);
        var arr = [];
        var recs = mData.split('^');
        for (var a = 0; a < recs.length; a++) {
            var cols = recs[a].split('|');
            arr[cols[0]] = [];
            for (var b = 1; b < cols.length; b++) {
                arr[cols[0]][fieldnames[b]] = cols[b];
            }
        }
        return arr;
    }
    return null;
}

//parse the mapping object and fill in data
function fillInData(selectedValue, sourceField, mapping) {
    if (typeof (mapping == "object")) {
        //get sourceField
        var sField = getObject(sourceField);
        if (sField) {
            var arrData = buildArray(sField.value);
            if (arrData) {
                //traverse mapping object and fill in data
                for (var i in mapping) {
                    var tField = getObject(eval('mapping.' + i));
                    if (tField) {
                        fillValue = eval('arrData["' + selectedValue + '"]["' + i + '"];');
                        if (fillValue != null) {
                            tField.value = fillValue;
                            //console.log(eval('arrData["' + selectedValue + '"]["' + i + '"];'));
                        }
                    }
                }
            }
        }
        else {
            return false;
        }
    }
}

// function build and return object
// from JSON String passed on
function buildObject(strJSON) {
    try {
        var myObj = eval(strJSON);
        if (typeof (myObj) != "object") {
            throw new Error('String passed on is not a valid JSON string.');
        }
        return myObj;
    }
    catch (err) {
        console.error(err);
    }
}

// find and return an element from a data object
// find by key-value pair
function getElementFromObject(dataSource, k, v) {
    dataObject = buildObject(($G(dataSource)).value);

    if (typeof (dataObject) == "object") {
        try {
            for (var i = 0, obj; (obj = dataObject[i]) != null; i++) {
                if (obj[k] == v) {
                    return obj;
                }
            }
        }
        catch (err) {
            console.error(err);
        }
    }
    return null;
}

// filling JSON data into controls
// according to the defined control-key mapping
function fillInJSONData(dataSource, k, v, mapping) {
    dataObject = getElementFromObject(dataSource, k, v);

    if (typeof (mapping) == "object" && typeof (dataObject) == "object") {
        //traverse mapping object and fill in data
        for (var i in mapping) {
            var tField = $G(mapping[i]);
            if (tField) {
                fillValue = dataObject[i]
                if (fillValue != null) {
                    tField.value = fillValue;
                }
            }
        }
    }
    return false;
}


// --------------------------------------
//  make leftPane height equal to
//  working area when it is too short
// --------------------------------------
function adjustLeftPane() {
    var objLeftPane = getObject('leftPane');
    var objMainContents = getObject('mainContents');
    var hOffset = 19;
    if (objLeftPane && objMainContents) {
        lpCompHeight = getComputedStyle(objLeftPane, null).getPropertyValue("height");
        mcCompHeight = getComputedStyle(objMainContents, null).getPropertyValue("height");

        if (isNaN(lpCompHeight)) {
            lpCompHeight = objLeftPane.offsetHeight;
        }

        if (isNaN(mcCompHeight)) {
            mcCompHeight = objMainContents.offsetHeight;
        }

        if (parseInt(lpCompHeight) < (parseInt(mcCompHeight) - hOffset)) {
            objLeftPane.style.height = (parseInt(mcCompHeight) - hOffset) + 'px';
        }
        else {
            objLeftPane.style.height = 'auto';
        }
    }
}

// ---------------------------------------------------------
//  Maintain Scroll Position after postback
// ---------------------------------------------------------

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


function Scroll_GetCoords() {
    var scrollX, scrollY;

    if (document.all) {
        if (!document.documentElement.scrollLeft)
            scrollX = document.body.scrollLeft;
        else
            scrollX = document.documentElement.scrollLeft;

        if (!document.documentElement.scrollTop)
            scrollY = document.body.scrollTop;
        else
            scrollY = document.documentElement.scrollTop;
    }
    else {
        scrollX = window.pageXOffset;
        scrollY = window.pageYOffset;
    }

    var htxtScrollX = getObject('htxtScrollX');
    var htxtScrollY = getObject('htxtScrollY');

    htxtScrollX.value = scrollX;
    htxtScrollY.value = scrollY;
}

function ScrollToPosition() {
    var htxtScrollX = getObject('htxtScrollX');
    var htxtScrollY = getObject('htxtScrollY');

    var x = htxtScrollX.value;
    var y = htxtScrollY.value;

    if (maintainScroll) {
        window.scrollTo(x, y);
    }
}
// ----------------------------------
function adjustTopSpacer() {
    var spacer = document.getElementById('divTopSpacer');
    var topToolbar = document.getElementById('topToolbar');
    if (!topToolbar) {
        if (spacer) {
            spacer.style.height = '34px';
        }
    }
    else {
        if (spacer) {
            spacer.style.height = '77px';
        }
    }
}

// manage cookies to maintain lefPane status across pages
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

// *******************************************
//      Fill sponsor data same as patient
// *******************************************
function fillSponsorData(obj) {
    if (obj.checked) {
        var selValue = obj.value;
        var sourcedata = 'htxtPatAddress';
        var mapping = {
            address1: 'txtSponsorAddress1',
            city: 'txtSponsorCity',
            postal_code: 'txtSponsorPostCode',
            state_id: 'cboSponsorState',
            homephone: 'txtSponsorHPhone',
            workphone: 'txtSponsorWPhone'
        };
        fillInData(selValue, sourcedata, mapping);

        //get and fill name of the sponsor
        var patData = eval($('[id$="htxtPatDemo"]').val());
        var patName = patData[0].first_name;
        if (patData[0].mi.length > 0) {
            patName += ' ' + patData[0].mi;
        }
        patName += ' ' + patData[0].last_name;
        $('[id$="txtSponsorName"]').val(patName);

        //set relationship dropdown
        //selected option to "Self"
        $('select[id$="cboSponsorRelationship"]').val('9');
    }
}