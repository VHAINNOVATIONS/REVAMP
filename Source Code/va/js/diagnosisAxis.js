// functions for handle css classes
if (document.getElementsByClassName == undefined) {
    document.getElementsByClassName = function(className) {
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


//toggleNode
function toggleNode() {

    //default options -----------------------

    var settings = {
        id: '',
        classId: '',
        hasChild: '',
        isTopLevel: false,
        scrollValue: 500,
        axisContainer: 'axis_container',
        axis: 1,
        mLevels: ['level0', 'level1', 'level2', 'level3', 'level4', 'level5', 'level6'],
        mainDiv: ['', 'axisi_container', 'axisii_container', 'axisiii_container', 'axisiv_container', 'axisv_container', '', '', '', '', 'axisx_container']
    };

    if (arguments.length > 0) {
        var opts = arguments[0];
    }
    else {
        var opts = {};
    }
    for (attrname in opts) { settings[attrname] = opts[attrname]; }
    opts = settings;

    //default options ends -----------------------

    // other settings
    axis_container  = document.getElementById(opts.axisContainer + '_' + opts.axis);
    targetLevel     = parseInt(document.getElementById(opts.id).getAttribute('level'));
    h_toplevel      = __getStyle('toplevel', 'height');
    selItms         = document.getElementsByClassName('selAxis');
    // other settings --ends

    //hides all divs on selection change
    
    for (var b = parseInt(opts.classId); b < opts.mLevels.length; b++) 
    {
        clearSelected(b);
        results = document.getElementsByClassName(opts.mLevels[b]);
        if (results.length > 0) 
        {
            for (var a = 0; a < results.length; a++) 
            {
                results[a].style.display = 'none';
            }
        }
    }

    //toggle DIV visibility
    var elId = 'ChildOf_' + opts.id;
    mElem = document.getElementById(elId);

    if (mElem.style.display == 'none') {
        if (opts.hasChild == 1) {
            if (!opts.isTopLevel) {
                newWidth = 520 + (250 * (targetLevel));
            }
            else {
                newWidth = 520;
            }
            axis_container.style.width = newWidth + "px";
        }
        mElem.style.display = 'block';
        document.getElementById(opts.mainDiv[opts.axis]).scrollLeft = opts.scrollValue;

        // 05/17/2011 DS: scroll new displayed column to top position
        document.getElementById(opts.mainDiv[opts.axis]).scrollTop = 0;
        highlightSelected(opts.id);
    }
    else {
        if (!opts.isTopLevel) {
            newWidth = newWidth - (250 * (targetLevel));
        }
        else {
            newWidth = 520;
        }
        axis_container.style.width = newWidth + "px";
        mElem.style.display = 'none';
    }
}
//------------------------------------



// highlight selected item
function highlightSelected(id) {
    mObj = document.getElementById(id);
    testObj = document.getElementById(id);
    while (testObj.id != 'li_' + id) {
        testObj = testObj.parentNode;
    }
    testObj.style.backgroundColor = '#708fc9'; //#426db1
    mObj.style.color = '#ffffff';
}

function clearSelected() {
    classId = parseInt(arguments[0]) - 1;
    var a = document.getElementsByClassName('level' + classId);
    for (b = 0; b < a.length; b++) {
        for (c = 0; c < a[b].childNodes.length; c++) {
            d = a[b].childNodes[c];
            for (e = 0; e < d.childNodes.length; e++) {
                d.childNodes[e].style.backgroundColor = '#ffffff';
                f = d.childNodes[e];
                for (g = 0; g < f.childNodes.length; g++ ) {
                    f.childNodes[g].style.color = '#000000';
                }
            }
        }
    }
    lastLnk = document.getElementsByClassName('lastLnk');
    for (n = 0; n < lastLnk.length; n++) {
        lastLnk[n].style.color = '#0000ff';
    }
}

//check specifier
function checkSpecifier(ele) {
    var selected = ele.options[ele.selectedIndex];
    var m = selected.getAttribute("specifier");
    if (m) {
        __doPostBack('specifier', m);
        //alert(m);
    }
}