/// <reference path="jquery-1.4.2.min.js" />

// Functions to handle Prev/Next buttons
function OnClickNav(strDirection)
{
    //onclick_Move(strDirection);
    intake().onclick_Move(strDirection);
    __doPostBack(strDirection, '');
}

function OnClickSave(strDirection)
{
    onclick_Save();
    __doPostBack('save', '');
} 	


/*********************************************
*                                            *
*       JS TO HANDLE RESPONSES               *
*       SELECTION ON THE QUESTIONNAIRE       *
*                                            *
**********************************************/

var intake = function(args)
{
    //general definitions
    var _options = {
        singletonName: 'intake',
        argType: null, //null: null, 1:HTML element, 2:Object
        mOBJ: null,
        performValidations: false
    },
    _results = false;

    //determine type of argument passed on
    if (args != undefined)
    {
        if (typeof args == "object")
        {
            //is it an HTML element?
            if (typeof (args.nodeType) != "undefined")
            {
                if (args.nodeType == 1)
                {
                    _options.argType = 1;
                    _options.mOBJ = args;
                }
            }
            else
            {
                //is a javascript object
                _options.argType = 2;
            }
        }
        else
        {
            //only HTML elements and javascript's objects are allowed
            _options.argType = null;
        }
    }

    //merge argumets with the defaults collection
    if (_options.argType == 2)
    {
        $.extend(true, _options, args);
        if (typeof (_options.obj) != "undefined")
        {
            _options.mOBJ = _options.obj;
        }
    }

    //private -----------------

    var _getCollection = function(name)
    {
        return $('[name="' + name + '"]');
    },

	_getIndexFromID = function(strID)
	{
	    var index = strID.substring(strID.lastIndexOf('_') + 1).replace(/\D/gi, '');
	    return index;
	},

	_maxLength = function(len)
	{
	    if (typeof (len) != "undefined")
	    {
	        if (!isNaN(len) || len != null)
	        {
	            if (_options.argType == 1 || typeof (_options.mOBJ) != "undefined")
	            {
	                _options.mOBJ.maxLength = len;
	            }
	        }
	    }
	},

	_addOnBlurEvt = function(fn)
	{
	    if (fn != undefined)
	    {
	        if (!_options.mOBJ.getAttribute('onblur'))
	        {
	            _options.mOBJ.setAttribute('onblur', fn);
	        }
	    }
	},

	_addOnKeyUpEvt = function(fn)
	{
	    if (fn != undefined)
	    {
	        if (!_options.mOBJ.getAttribute('onkeyup'))
	        {
	            _options.mOBJ.setAttribute('onkeyup', fn);
	        }
	    }
	},

	_evalMask = function(patt)
	{

	    //return true if there is no mask pattern to be applied
	    if (typeof (patt) == "undefined" || patt == null || patt == 'null' || $.trim(patt) == '')
	    {
	        return true;
	    }

	    var obj = _options.mOBJ,
			    evtobj = window.event ? event : e,
			    charCode = evtobj.which || evtobj.charCode || evtobj.keyCode;

	    //--------------------------------
	    // Get mask's chars positions from pattern
	    var arrPatt = patt.split(''),
		        charPos = new Array();
	    for (var a = 0; a < arrPatt.length; a++)
	    {

	        if (arrPatt[a] != '#')
	        {
	            charPos[a] = arrPatt[a];
	        }
	    }

	    //--------------------------------

	    if (charCode >= 48 && charCode <= 57)
	    {
	        _placeMaskChars(charPos, obj)
	        return true;
	    }

	    if (charCode == 8 || charCode == 9)
	    {
	        return true;
	    }

	    return false;
	},

	_placeMaskChars = function(charPos, obj)
	{
	    if (charPos[obj.value.length])
	    {
	        obj.value = obj.value + charPos[obj.value.length];
	        _placeMaskChars(charPos, obj);
	    }
	},

	_mask = function(pattern)
	{
	    _results = _evalMask(pattern);
	},

	_testAlert = function()
	{
	    alert('Intellica Patient Portal');
	};

    //public ------------------

    this.testAlert = function()
    {
        _testAlert();
    };

    // ------------------------------------------------
    // methods for the different display types
    // ------------------------------------------------

    // _RADIO  ------------------

    var _clearRADIO = function(radGroup, radId)
    {
        var radCollection = $('input[type="radio"][name="' + radGroup + '"]');
        var imgRadCollection = $('img[name="' + radGroup + '"]');

        //clear all radios
        $.each(radCollection, function(i, ele)
        {
            $(ele).removeAttr('checked');
        });

        //clar all img radios
        $.each(imgRadCollection, function(i, ele)
        {
            ele.src = imgRadioOff.src;
        });
    },

	_clearAllRADIO = function()
	{
	    var radCollection = $('input[type="radio"][id^="RADIO_"]'),
		    imgRadCollection = $('img[id^="rdo_img_"]');

	    $.each(radCollection, function(i, o)
	    {
	        $(o).removeAttr('checked');
	    });

	    $.each(imgRadCollection, function(i, o)
	    {
	        o.src = imgRadioOff.src;
	    });
	},

	_clearRadOnTxtOther = function(sender)
	{
	    var sIndex = sender.id.replace(/\D/gi, ''), //response index of text box
	        radCollection = $('input[type="radio"][id^="RADIO_"]'),
		    imgRadCollection = $('img[id^="rdo_img_"]');

	    $.each(radCollection, function(i, o)
	    {
	        var rIndex = o.id.replace(/\D/gi, ''); //response index of radio button

	        //clear the radio if the text box is after the radio group
	        if (sIndex > rIndex)
	        {
	            $(o).removeAttr('checked');
	        }
	    });

	    $.each(imgRadCollection, function(i, o)
	    {
	        var rIndex = o.id.replace(/\D/gi, ''); //response index of radio button

	        //clear the radio if the text box is after the radio group
	        if (sIndex > rIndex)
	        {
	            o.src = imgRadioOff.src;
	        }
	    });
	},

    _toggleRadio = function(radGroup, radId)
    {

        var rad = $('input[id$="' + radId + '"]')[0];
        imgRad = $('img[id$="rdo_img_' + radId + '"]')[0];

        if (rad.checked)
        {
            $(rad).removeAttr('checked');
            imgRad.src = imgRadioOff.src;
        }
        else
        {
            $(rad).attr('checked', 'checked');
            imgRad.src = imgRadioOn.src;
        }
    };

    this.toggleRadio = function(radGroup, radId)
    {
        _resetNoneOfAbove();
        _clearRADIO(radGroup, radId);
        _toggleRadio(radGroup, radId);
        _clearTxtOnRadToggle(radId);
    }

    // _CHECK  ------------------

    var _clearAllCheckbox = function()
    {
        var chkCollection = $('input[type="checkbox"][name="grpCheckbox"]'),
		    imgChkCollection = $('img[name="grpCheckbox_ctrl"]');

        $.each(chkCollection, function(i, o)
        {
            $(o).removeAttr('checked');
        });

        $.each(imgChkCollection, function(i, o)
        {
            o.src = imgCheckOff.src;
        });
    };

    this.toggleCheckbox = function(chkId)
    {
        _resetNoneOfAbove();

        var chk = $('input[type="checkbox"][id="' + chkId + '"]')[0],
		    imgChk = $('img[id="chk_img_' + chkId + '"]')[0];

        if (chk.checked)
        {
            $(chk).removeAttr('checked');
            imgChk.src = imgCheckOff.src;
        }
        else
        {
            $(chk).attr('checked', 'checked');
            imgChk.src = imgCheckOn.src;
        }

    };

    // _TEXT  ------------------

    this.setTextBox = function(args)
    {
        _addOnBlurEvt(_options.singletonName + '().build_TEXT_Response();');
        _addOnKeyUpEvt(_options.singletonName + '().build_TEXT_Response();');

        if (typeof (args) == "object")
        {
            $.each(args, function(k, v)
            {
                if (k != "obj")
                {

                    var fn = '_' + k;
                    eval(fn + '("' + v + '")');
                }
                else
                {
                    _options.mOBJ = v;
                }
            });
        }

        if (_results)
        {
            _resetNoneOfAbove();
            _clearRadOnTxtOther(_options.mOBJ);
        }

        return _results;
    };

    this.build_TEXT_Response = function()
    {
        var strValue = '',
		    grpText = _getCollection('grpText_ctrl');
        $.each(grpText, function(i, o)
        {
            strValue = o.id.replace(/\D/gi, '') + ':' + o.value.replace(',', '^');
            $('input[name="grpText"][id="grpText_' + o.id.replace(/\D/gi, '') + '"]').val(strValue);
        });
    };

    var _clearTEXT = function()
    {
        var grpText = _getCollection('grpText_ctrl');
        $.each(grpText, function(index, object)
        {
            object.value = '';
        });

        this.build_TEXT_Response();
    },

    _clearTxtOnRadToggle = function(radID)
    {
        var sIndex = radID.replace(/\D/gi, ''), //response index of radio

            grpText = _getCollection('grpText_ctrl');
        $.each(grpText, function(index, object)
        {
            var tIndex = object.id.replace(/\D/gi, ''); //response index of textbox

            //clear textbox if radio index is less than texbox index
            if (sIndex < tIndex)
            {
                object.value = '';
            }
        });

        this.build_TEXT_Response();
    };
	
	// _SUBSTANCE QUANTITY ------------
	
	this.setTextBoxSubs = function(args)
    {		
		var _me = this;
		
		if (typeof (args) == "object")
        {
            $.each(args, function(k, v)
            {
                if (k != "obj")
                {

                    var fn = '_' + k;
                    eval(fn + '("' + v + '")');
                }
                else
                {
                    _options.mOBJ = v;
                }
            });
        }

        if (_results)
        {
            _resetNoneOfAbove();
            _clearRadOnTxtOther(_options.mOBJ);
        }

        return _results;
    };
	
	this.enableComboUnits = function()
	{
		var _me = this;
		
		if(_options.mOBJ)
		{
			var cbo = $('select[id$="grpSubstanceQtyUnit_'+ _options.mOBJ.id.replace(/\D/gi,'') +'"]')[0];
			if(cbo)
			{
				if(_options.mOBJ.value.length > 0)
				{
					$(cbo).removeAttr('disabled');
					_me.selectedUnit(cbo);
				}
				else
				{
					cbo.selectedIndex = -1;
					$(cbo).attr('disabled','disabled');
					_me.clearSubstanceQty(cbo);
				}
			}
		}
		
		return this;
	};
	
	this.selectedUnit = function(obj)
	{
		var _me = this;
		
		if(obj)
		{
			if(obj.selectedIndex > -1 && obj.value != "-1")
			{
				_me.build_SubsQty_Response(obj);
			}
			else
			{
				_me.clearSubstanceQty(obj);
			}
		}
		
		return this;
	};
	
	this.build_SubsQty_Response = function(obj)
	{
		var strValue = '',
		    strHowMuch = $('input[id$="grpSubstanceQty_ctrl_'+ obj.id.replace(/\D/gi,'') +'"]').val(),
			strUnit = $('option:selected', $(obj)).text();
			
			strValue = obj.id.replace(/\D/gi,'') + ':' + strHowMuch + ' ' + strUnit;
        
		$('input[name="grpSubstanceQty"][id="grpSubstanceQty_'+ obj.id.replace(/\D/gi,'') +'"]').val(strValue);
	};
	
	this.clearSubstanceQty = function(obj)
	{
		var _me = this,
		    strValue = '';
			
		$('input[name="grpSubstanceQty"][id="grpSubstanceQty_'+ obj.id.replace(/\D/gi,'') +'"]').val(strValue);
	};
	
	var _clearALLSubstanceQty = function()
	{
		var comboQty = _getCollection('grpSubstanceQtyUnit'),
			txtHowMuch = _getCollection('grpSubstanceQty_ctrl'),
			txtResponse = _getCollection('grpSubstanceQty');
		
		$.each(comboQty, function(i, o){
			o.selectedIndex = -1;
		});
		
		$.each(txtHowMuch, function(i, o){
			o.value = '';
		});
		
		$.each(txtResponse, function(i, o){
			o.value = '';
		});
	};
	
	
	
    // _COMB  -------------------------

    var _clearAllCombo = function()
    {
        var colCombo = $('select[name="cmbResponse"]');

        $.each(colCombo, function(i, o)
        {
            o.selectedIndex = -1;
        });

        _setCombo();
    },

	_setCombo = function()
	{
	    var cmbResponse = $('select[name="cmbResponse"]');

	    $.each(cmbResponse, function(i, o)
	    {
	        var htxtCmb = $('input[name="grpcmbResponse"][id="grpcmbResponse_' + o.id.replace(/\D/gi, '') + '"]')[0];
	        htxtCmb.value = htxtCmb.id.replace(/\D/gi, '') + ':' + o.value;
	    });
	};

    this.setCombo = function()
    {
        _resetNoneOfAbove();
        _setCombo();
        return this;
    };

    // _MONTHDAY  ------------------

    var _clearAllMonthDay = function()
    {
        var cmbDay = $('select[name="cmbDay"]'),
		    cmbMonth = $('select[name="cmbMonth"]');

        $.each(cmbDay, function(i, o)
        {
            o.selectedIndex = -1;
        });

        $.each(cmbMonth, function(i, o)
        {
            o.selectedIndex = -1;
        });

        _setMonthDay();
    },

	_setMonthDay = function()
	{
	    var grpMonthDay = $('input[name="grpMonthDay"]');

	    $.each(grpMonthDay, function(i, o)
	    {
	        var cmbMonth = $('select[name="cmbMonth"][id="cmbMonth_' + o.id.replace(/\D/gi, '') + '"]')[0],
				cmbDay = $('select[name="cmbDay"][id="cmbDay_' + o.id.replace(/\D/gi, '') + '"]')[0],
				strMonth = '',
				strDay = '';

	        if (cmbMonth.selectedIndex > -1)
	        {
	            if (cmbMonth.value > 0)
	            {
	                strMonth = '00' + cmbMonth.value;
	                strMonth = strMonth.substring(strMonth.length - 2) + '/';
	            }
	        }

	        if (cmbDay.selectedIndex > -1)
	        {
	            if (cmbDay.value > 0)
	            {
	                strDay = cmbDay.value;
	            }
	        }

	        o.value = o.id.replace(/\D/gi, '') + ':' + strMonth + strDay;
	    });
	};

    this.setMonthDay = function()
    {
        _resetNoneOfAbove();
        _setMonthDay();
        return this;
    };

    // _MONTHYEAR  ------------------

    var _clearAllMonthYear = function()
    {
        var cmbMonth = $('select[name="cmbMonth"]'),
		    cmbYear = $('select[name="cmbYear"]');

        $.each(cmbMonth, function(i, o)
        {
            o.selectedIndex = -1;
        });

        $.each(cmbYear, function(i, o)
        {
            o.selectedIndex = -1;
        });

        _setMonthYear();
    },

	_setMonthYear = function()
	{
	    var grpMonthYear = $('input[name="grpMonthYear"]');

	    $.each(grpMonthYear, function(i, o)
	    {
	        var cmbMonth = $('select[name="cmbMonth"][id="cmbMonth_' + o.id.replace(/\D/gi, '') + '"]')[0],
				cmbYear = $('select[name="cmbYear"][id="cmbYear_' + o.id.replace(/\D/gi, '') + '"]')[0],
				strMonth = '',
				strYear = '';

	        if (cmbMonth.selectedIndex > -1)
	        {
	            if (cmbMonth.value > 0)
	            {
	                strMonth = '00' + cmbMonth.value;
	                strMonth = strMonth.substring(strMonth.length - 2) + '/';
	            }
	        }

	        if (cmbYear.selectedIndex > -1)
	        {
	            if (cmbYear.value > 0)
	            {
	                strYear = cmbYear.value;
	            }
	        }

	        o.value = o.id.replace(/\D/gi, '') + ':' + strMonth + strYear;
	    });
	};

    this.setMonthYear = function()
    {
        _resetNoneOfAbove();
        _setMonthYear();
        return this;
    };

    // _NONEOFABOVE  ------------------

    var _resetNoneOfAbove = function()
    {
        var colNOA = _getCollection('chkNoneofAbove');
        $.each(colNOA, function(i, o)
        {
            $(o).removeAttr('checked');
            $('img[id="chk_img_' + o.id + '"]')[0].src = imgCheckOff.src;
        });
    },

	_clearAllAbove = function()
	{
	    _clearAllRADIO();
	    _clearAllCheckbox();
	    _clearTEXT();
	    _clearAllCombo();
	    _clearAllMonthDay();
	    _clearAllMonthYear();
		_clearALLSubstanceQty();
	},

	_toggleNoneOfAbove = function(chkId)
	{
	    var chkNOA = $('input[type="checkbox"][id="' + chkId + '"]')[0],
			imgChkNOA = $('img[id="chk_img_' + chkId + '"]')[0];

	    if (chkNOA.checked)
	    {
	        $(chkNOA).removeAttr('checked');
	        imgChkNOA.src = imgCheckOff.src;
	    }
	    else
	    {
	        $(chkNOA).attr('checked', 'checked');
	        imgChkNOA.src = imgCheckOn.src;
	        _clearAllAbove();
	    }
	};

    this.toggleNoneOfAbove = function(chkId)
    {
        _toggleNoneOfAbove(chkId);
        return this;
    };

    // _COMBOHEIGHT  ------------------

    /* 	right now there are no responses
    using this display type */

    // _DATEPICKER  ------------------

    /* 	right now there are no responses
    using this display type */

    // _TITLE  ------------------

    /* 	this display type does not require js code */

    // ---------------------------------------------
    // BUTTONS ACTIONS
    // ---------------------------------------------

    // Move ----------------------
    this.onclick_Move = function(dir)
    {
        if (dir.toLowerCase() == "next")
        {
            this.build_TEXT_Response();
            _setCombo();
            _setMonthDay();
            _setMonthYear();
        }
        document.forms[0].txtDirection.value = dir;
    };

    // Finish --------------------


    //always return a reference to itself 
    //for use the method chaining
    return this;
}



// **************************************************************

// Make response's table row a selector
// on responses of type Radio, Check, NoneOfAbove

function selectFromTR(e, element)
{
    if (!e) var e = window.event;
    var eTarget = (e.target) ? e.target : e.srcElement;
    if (eTarget.tagName == 'A' || eTarget.tagName == 'IMG')
    {
        //do nothing
        /* action will be triggered either from the radio/check
        IMG's onclick or the Response's anchor tag */
    }
    else
    {
        var allTD = element.getElementsByTagName('TD');
        var mTD;
        var mA;
        for (var a = 0; (mTD = allTD[a]) != null; a++)
        {
            var allA = mTD.getElementsByTagName('A');
            for (var b = 0; (mA = allA[b]) != null; b++)
            {
                var evtOnclick = mA.getAttribute('onclick');
                if (evtOnclick)
                {
                    eval(evtOnclick);
                    break;
                }

            }
        }
    }
}


// ---------------------------------------------
//      MODAL CONTROLS
//      (e.g. on-screen keyboard, list...)
// ---------------------------------------------

// Moved as it was from the old assessment.js file

var ModalDialogWindow,
    ModalDialogInterval,
    ModalCallerID = '',
    gDateValue = '',
    ModalReturnValue = '';
    
    ModalDialog = new Object();
    ModalDialog.value = '';
    ModalDialog.eventhandler = '';
    

function ModalDialogMaintainFocus()
{
    try
    {
        if (ModalDialogWindow.closed)
        {
            window.clearInterval(ModalDialogInterval);
            eval(ModalDialog.eventhandler);
            return;
        }
        ModalDialogWindow.focus();
    }
    catch (everything) { }
}

function ModalDialogRemoveWatch()
{
    ModalDialog.value = '';
    ModalDialog.eventhandler = '';
}

function ModalKeyBoardShow(id, question, response)
{
    ModalDialogRemoveWatch();
    ModalDialog.eventhandler = 'ModalKeyBoardReturnMethod()';
    ModalCallerID = id;
    var args = 'width=725,height=390,left=125,top=40,toolbar=0,';
    args += 'location=0,status=0,menubar=0,scrollbars=0,resizable=0';
    ModalDialogWindow = window.open("keyboard.aspx?id=" + id + "&q=" + escape(question) + "&r=" + escape(response), "KeyBoard", args);
    ModalDialogWindow.focus();
    ModalDialogInterval = window.setInterval("ModalDialogMaintainFocus()", 5);
}

function ModalKeyBoardReturnMethod()
{
    ModalReturnValue = ModalDialog.value;
    ModalDialogRemoveWatch();

    var editbox = document.getElementById(ModalCallerID);
    editbox.value = ModalReturnValue;
}

function ModalListShow(id, question, response)
{
    ModalDialogRemoveWatch();
    ModalDialog.eventhandler = 'ModalListReturnMethod()';
    ModalCallerID = id;
    var args = 'width=540,height=540,left=125,top=40,toolbar=0,';
    args += 'location=0,status=0,menubar=0,scrollbars=0,resizable=0';
    ModalDialogWindow = window.open("kiosk_list_frame.aspx?id=" + id + "&q=" + escape(question) + "&r=" + escape(response), "KeyBoard", args);
    ModalDialogWindow.focus();
    ModalDialogInterval = window.setInterval("ModalDialogMaintainFocus()", 5);
}

function ModalListReturnMethod()
{
    ModalReturnValue = ModalDialog.value;
    ModalDialogRemoveWatch();
    var listBox = document.getElementById(ModalCallerID);
    listBox.selectedIndex = ModalReturnValue;
}

function ModalCalendarShow(id, question, month, day, year)
{
    ModalDialogRemoveWatch();
    ModalDialog.eventhandler = 'ModalCalendarReturnMethod()';
    ModalCallerID = id;
    if (month > 0 && year > 0)
    {
        gNow.setMonth(month);
        gNow.setDate(day);
        gNow.setYear(year);
    }
    var mm = gNow.getMonth() - 1;
    var yyyy = gNow.getFullYear();
    show_fmtcalendar('ModalReturnValue', mm, yyyy.toString());
    ModalDialogWindow = ggWinCal;
    ModalDialogWindow.focus();
    ModalDialogInterval = window.setInterval("ModalDialogMaintainFocus()", 5);
}

function ModalCalendarReturnMethod()
{
    //ModalReturnValue = DIALOG SETS THE VALUE
    ModalDialogRemoveWatch();
    var strID1 = 'DATE1_' + ModalCallerID;
    var strID2 = 'DATE2_' + ModalCallerID;
    var strID3 = 'DATE3_' + ModalCallerID;
    var dtArray = ModalReturnValue.split("/");
    var mon = dtArray[0];
    var day = dtArray[1];
    var year = dtArray[2];
    mon = trim(mon);
    day = trim(day);
    year = trim(year);
    //day
    var cbo = document.getElementById(strID1);
    cbo.selectedIndex = day;
    cbo = document.getElementById(strID2);
    cbo.selectedIndex = mon;
    //year
    cbo = document.getElementById(strID3);
    for (var i = 0; i < cbo.length; i++)
    {
        if (cbo.options[i].text == year)
        {
            cbo.selectedIndex = i;
            break;
        }
    }
}