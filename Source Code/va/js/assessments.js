/////////////////////////////////////////////////////////
//todo: move these later to a js file
/////////////////////////////////////////////////////////
imgPortalRadioOn = new Image();
imgPortalRadioOn.src = "images_portal/radiobutton_on.jpg";
imgPortalRadioOff = new Image();
imgPortalRadioOff.src = "images_portal/radiobutton_off.jpg";

imgPortalCheckOn = new Image();
imgPortalCheckOn.src = "images_portal/CheckBoxOn_2.jpg";
imgPortalCheckOff = new Image();
imgPortalCheckOff.src = "images_portal/CheckBoxOff_2.jpg";

//checks max length of an object
function imposeMaxLength(Object, MaxLen)
{
    return (Object.value.length <= MaxLen);
}

//get the value changed in the text area
function portalTextAreaChanged(strRID, Object)
{
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        document.getElementById(valID).value = Object.value;
    }

    //reset type99 checkbox
    resetType99();

}

//get the value changed in the text area, 
function portalListDelete(strRID)
{
    //id of list and the list itself
    var lstID = 'portal_list_' + strRID;
    var lst = document.getElementById(lstID);

    //value id
    var valID = 'curr_value_' + strRID;
    if (document.getElementById(valID) != null)
    {
        //get the selected item and delete it
        for (i = 0; i < lst.length; i++)
        {
            if (lst.options[i].selected)
            {
                lst.remove(i);
            }
        }

        //loop over the items and build a comma delimeted string
        //this is how the exisiting SUAT app does it...
        var strValue = '';
        for (i = 0; i < lst.length; i++)
        {
            strValue += lst.options[i].value;
            if (i < lst.length - 1)
            {
                strValue += ",";
            }
        }

        document.getElementById(valID).value = strValue;
        //todo:alert(document.getElementById(valID).value);
    }

    //reset type99 checkbox
    resetType99();

}

//get the value changed in the text area
function portalListAdd(strRID)
{
    //id of list and the list itself
    var lstID = 'portal_list_' + strRID;
    var lst = document.getElementById(lstID);

    //value id
    var valID = 'curr_value_' + strRID;
    if (document.getElementById(valID) != null)
    {
        //loop and move data from edit boxes to list box
        for (i = 0; i < 5; i++)
        {
            var txtID = 'portal_listtxt';
            txtID += i + 1;
            txtID += '_';
            txtID += strRID;
            var txt = document.getElementById(txtID);
            if (txt != null)
            {
                if (txt.value != '')
                {
                    if (lst != null)
                    {
                        lst.options[lst.length] = new Option(txt.value, txt.value);
                    }

                    txt.value = '';
                }
            }
        }

        //loop over the items and build a comma delimeted string
        //this is how the exisiting SUAT app does it...
        var strValue = '';
        for (i = 0; i < lst.length; i++)
        {
            strValue += lst.options[i].value;
            if (i < lst.length - 1)
            {
                strValue += ",";
            }
        }

        document.getElementById(valID).value = strValue;
        //todo:alert(document.getElementById(valID).value);
    }

    //reset type99 checkbox
    resetType99();

}


//get the value changed in the text
function portalTextChanged(strRID, Object)
{
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        document.getElementById(valID).value = Object.value;
    }

    //reset type99 checkbox
    resetType99();
}


//clears readio buttons and reselects the one selected...
function portalSelectRadio(strRID)
{
    var allowControl = true;

    var valID = "curr_value_" + strRID;
    var rdoID = "portal_rdo_" + strRID;

    var objRadio = document.getElementById(rdoID);
    var objValue = document.getElementById(valID);

    var arrRadios = [];
    var arrValues = [];

    var groupClassName;

    if (objRadio.getAttribute('disabled') == 'disabled')
    {
        allowControl = false;
    }

    if (allowControl)
    {
        if (objRadio)
        {
            groupClassName = objRadio.className;
            if (groupClassName)
            {
                var arrGroupObj = document.getElementsByClassName(groupClassName);
                if (arrGroupObj)
                {
                    for (var a = 0; a < arrGroupObj.length; a++)
                    {
                        if (arrGroupObj[a].tagName == "INPUT")
                        {
                            arrValues.push(arrGroupObj[a]);
                        }
                        else
                        {
                            arrRadios.push(arrGroupObj[a]);
                        }
                    }
                    //reset radios
                    for (var b = 0; b < arrRadios.length; b++)
                    {
                        arrRadios[b].src = imgPortalRadioOff.src;
                    }
                    //reset values
                    for (var c = 0; c < arrValues.length; c++)
                    {
                        arrValues[c].value = 0;
                    }
                }
            }
        }

        //turn on the radio clicked and set it value
        objRadio.src = imgPortalRadioOn.src;
        objValue.value = 1;

        //reset type99 checkbox
        resetType99();
    }

}

//clears check boxes and reselects the one selected...
function portalSelectCheck(strRID)
{
    var allowControl = true;

    var valID = "curr_value_" + strRID;
    var chkID = "portal_chk_" + strRID;

    var objCheck = document.getElementById(chkID);

    if (objCheck.getAttribute('disabled') == 'disabled')
    {
        allowControl = false;
    }

    if (allowControl)
    {
        //turn on the radio we clicked and set its value                  
        if (document.getElementById(chkID) != null)
        {
            if (document.getElementById(chkID).src == imgPortalCheckOn.src)
            {
                document.getElementById(chkID).src = imgPortalCheckOff.src;
                if (document.getElementById(valID) != null)
                {
                    document.getElementById(valID).value = 0;
                }
            }
            else
            {
                document.getElementById(chkID).src = imgPortalCheckOn.src;
                if (document.getElementById(valID) != null)
                {
                    document.getElementById(valID).value = 1;
                }
            }
        }

        //reset type99 checkbox
        resetType99();
    }
}

//clears controls if checked type99 checkbox
function portalSelectNONECheck(strRID)
{
    if (arrRespCollection)
    {
        var type99Ele = null;
        for (var x = 0; (type99Ele = arrRespCollection[x]) != null; x++)
        {
            if (type99Ele.rid == strRID)
            {
                break;
            }
            else
            {
                type99Ele = null;
            }
        }

        //toggle check box state
        if (type99Ele)
        {
            if (type99Ele.portalItm.src == imgPortalCheckOn.src)
            {
                type99Ele.portalItm.src = imgPortalCheckOff.src;
                type99Ele.currValue.value = '0';
            }
            else
            {
                type99Ele.portalItm.src = imgPortalCheckOn.src;
                type99Ele.currValue.value = '1';

                //clear other controls
                var obj;
                for (var i = 0; (obj = arrRespCollection[i]) != null; i++)
                {
                    if (obj.currType != 99)
                    {
                        clearControl(obj);
                    }
                }
            }
        }
    }
}

//select a combo, update our value
function portalSelectCombo(strRID, Object)
{
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        document.getElementById(valID).value = Object.value;
    }

    //reset type99 checkbox
    resetType99();

}

//select a combo, update our value
function portalBMIChange(strRID, Object1, Object2, Object3)
{
    //Object1 height feet, 
    var valFeetID = "portal_bmi_feet_" + strRID;
    var valFeet = 0;
    if (document.getElementById(valFeetID) != null)
    {
        if (document.getElementById(valFeetID).value != '')
        {
            valFeet = document.getElementById(valFeetID).value;
        }
    }
    else
    {
        valFeet = 0;
    }

    //Object2 height inches, 
    var valInchesID = "portal_bmi_inches_" + strRID;
    var valInches = 0;
    if (document.getElementById(valInchesID) != null)
    {
        if (document.getElementById(valInchesID).value != '')
        {
            valInches = document.getElementById(valInchesID).value;
        }
    }
    else
    {
        valInches = 0;
    }

    //Object3 weight pounds
    var valPoundsID = "portal_bmi_lbs_" + strRID;
    var valPounds = 0;
    if (document.getElementById(valPoundsID) != null)
    {
        if (document.getElementById(valPoundsID).value != '')
        {
            valPounds = document.getElementById(valPoundsID).value;
        }
    }
    else
    {
        valPounds = 0;
    }

    //get total inches
    var feetoinches = (12 * valFeet);
    var totalinches = parseInt(feetoinches) + parseInt(valInches);

    //
    //1 Inch = 0.0254 Meters 
    var totalmeters = parseInt(totalinches) * .0254;

    //
    //1 pound = 0.45359237 kilograms
    var totalpounds = parseInt(valPounds) * .45359237;

    //alert(totalmeters + ' - ' + totalpounds);

    //result
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        //todo: need to convert to metric!
        document.getElementById(valID).value = totalmeters + ',' + totalpounds;
    }

    //reset type99 checkbox
    resetType99();

}

//select a day, month, year, update our value
function portalDateChange(strRID)
{
    var valMonthID = "portal_date_month_" + strRID;
    var valMonth = -1;
    if (document.getElementById(valMonthID) != null)
    {
        if (document.getElementById(valMonthID).value != '')
        {
            valMonth = document.getElementById(valMonthID).value;
        }
        else
        {
            valMonth = -1;
        }
    }
    else
    {
        valMonth = -1;
    }

    var valDayID = "portal_date_day_" + strRID;
    var valDay = -1;
    if (document.getElementById(valDayID) != null)
    {
        if (document.getElementById(valDayID).value != '')
        {
            valDay = document.getElementById(valDayID).value;
        }
        else
        {
            valDay = -1;
        }
    }
    else
    {
        valDay = -1;
    }

    var valYearID = "portal_date_year_" + strRID;
    var valYear = -1;
    if (document.getElementById(valYearID) != null)
    {
        if (document.getElementById(valYearID).value != '')
        {
            valYear = document.getElementById(valYearID).value;
        }
        else
        {
            valYear = -1;
        }
    }
    else
    {
        valYear = -1;
    }


    //result
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        //init date to invalid
        document.getElementById(valID).value = '';

        //todo
        var valHH = "00";
        var valMM = "00";
        var valSS = "00";

        //result
        if (document.getElementById(valID) != null)
        {
            //init date to invalid
            document.getElementById(valID).value = '';

            if (portalCheckDate(valMonth, valDay, valYear, valHH, valMM))
            {
                var strDate = portalGetDateTime(valMonth, valDay, valYear, valHH, valMM);
                //alert(strDate);

                document.getElementById(valID).value = strDate;
            }
        }
    }

    //reset type99 checkbox
    resetType99();

}

//select a day, month, year, update our value DATE TIME
function portalDateTimeChange(strRID)
{
    //dtSelDate.Format(_T("%d/%d/%d %s"),
    //m_nMonth, 
    //m_nDay, 
    //m_nYear, 
    //m_strTime);

    var valMonthID = "portal_datetime_month_" + strRID;
    var valMonth = -1;
    if (document.getElementById(valMonthID) != null)
    {
        if (document.getElementById(valMonthID).value != '')
        {
            valMonth = document.getElementById(valMonthID).value;
        }
        else
        {
            valMonth = -1;
        }
    }
    else
    {
        valMonth = -1;
    }

    var valDayID = "portal_datetime_day_" + strRID;
    var valDay = -1;
    if (document.getElementById(valDayID) != null)
    {
        if (document.getElementById(valDayID).value != '')
        {
            valDay = document.getElementById(valDayID).value;
        }
        else
        {
            valDay = -1;
        }
    }
    else
    {
        valDay = -1;
    }

    var valYearID = "portal_datetime_year_" + strRID;
    var valYear = -1;
    if (document.getElementById(valYearID) != null)
    {
        if (document.getElementById(valYearID).value != '')
        {
            valYear = document.getElementById(valYearID).value;
        }
        else
        {
            valYear = -1;
        }
    }
    else
    {
        valYear = -1;
    }

    //todo
    var valHH = document.getElementById('portal_datetime_HH_'+strRID).value;
    var valMM = document.getElementById('portal_datetime_MM_' + strRID).value;
    var valSS = "00";

    //result
    var valID = "curr_value_" + strRID;
    if (document.getElementById(valID) != null)
    {
        //init date to invalid
        document.getElementById(valID).value = '';

        if (portalCheckDate(valMonth, valDay, valYear, valHH, valMM))
        {
            var strDate = portalGetDateTime(valMonth, valDay, valYear, valHH, valMM);
            //alert(strDate);

            document.getElementById(valID).value = strDate;
        }
    }

    //reset type99 checkbox
    resetType99();

}

//general funtion used to piece together a date time string
//DOES NOT CHECK VALUES!!! that is done with portalCheckDate
//before calling this
function portalGetDateTime(valMonth, valDay, valYear, valHH, valMM)
{
    //set the value     
    var strDate = valMonth + '/' + valDay + '/' + valYear;
    strDate += ' ';
    strDate += valHH;

    strDate += ':';

    strDate += valMM;

    strDate += ':';

    //seconds are always zero!
    strDate += '00';

    return strDate;
}

//general function used to check if a date/time is valid
function portalCheckDate(valMonth, valDay, valYear, valHH, valMM)
{
    //check that the vals are numbers
    if (valMonth.length < 1)//could still be entering data
        return false;

    if (valDay.length < 1)//could still be entering data
        return false;

    if (valYear.length < 4)//could still be entering data
        return false;

    if (valMonth == -1 ||
        valDay == -1 ||
        valYear == -1)//could still be entering data
    {
        return false;
    }

    if (valMonth < 1 || valMonth > 12)
    {
        alert('Invalid Month! ...Please check your data entry.');
        return false;
    }
    if (valDay < 1 || valDay > 31)
    {
        alert('Invalid Day! ...Please check your data entry.');
        return false;
    }
    if (valYear < 1900 || valYear > 3000)
    {
        alert('Invalid Year! ...Please check your data entry.');
        return false;
    }

    //make a date and see if its valid
    var secs = (new Date(valYear, (valMonth - 1), valDay)).getTime();
    var CheckDate = new Date();
    CheckDate.setTime(secs);

    // if difference exists then date isn't valid  
    if (CheckDate.getFullYear() != valYear ||
         CheckDate.getMonth() != (valMonth - 1) ||
         CheckDate.getDate() != valDay)
    {
        alert('Invalid Date! ...Please check your data entry.');
        return false;
    }
    return true;
}


// -------------------------------------------------------------------------------
//		05/07/11 DS: Make the TR a selector 
// 		monitors the event.target so the function's call is not duplicated
// -------------------------------------------------------------------------------

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

(function()
{
    //format listboxes width
    //with id like 'portal_list'
    var allSel = document.getElementsByTagName('SELECT');
    var mSel;
    for (var a = 0; (mSel = allSel[a]) != null; a++)
    {
        //modify with for multiline listbox
        if (mSel.id.indexOf('portal_list_') != -1)
        {
            mSel.style.width = '403px';
        }

        //modify widths for DateTime Combos
        if (mSel.id.indexOf('portal_datetime_') != -1)
        {
            mSel.style.width = '45%';
        }

        //modify widths for Date Combos
        if (mSel.id.indexOf('portal_date_') != -1)
        {
            mSel.style.width = '45%';
        }
    }


    //monitor independent radio/check buttons groups
    var responsesDescription = document.getElementById('htxtResponsesDescription');
    if (responsesDescription)
    {
        var arrRespDesc = responsesDescription.value.split('^');
        var strRID = arrRespDesc[0].substring(0, (arrRespDesc[0].length - 1));
        var strRespType = arrRespDesc[1].substring(0, (arrRespDesc[1].length - 1));
        var arrRID = strRID.split('|');
        var arrType = strRespType.split('|');
        var groupNumber = 1;
        var rdoButton;
        var htxtValue;

        for (var a = 0; a < arrType.length; a++)
        {
            if (arrType[a] != '1' && arrType[a] != '2')
            {
                groupNumber++;
            }
            rdoButton = document.getElementById('portal_rdo_' + arrRID[a]);
            chkButton = document.getElementById('portal_chk_' + arrRID[a]);
            htxtValue = document.getElementById('curr_value_' + arrRID[a]);

            if (rdoButton)
            {
                var id = rdoButton.id.replace(/rdo/, 'rdo_link');
                var objLink = document.getElementById(id);

                rdoButton.className = 'group' + groupNumber;
                objLink.className = 'group' + groupNumber;
                htxtValue.className = 'group' + groupNumber;
            }
            if (chkButton)
            {
                var id = chkButton.id.replace(/chk/, 'chk_link');
                var objLink = document.getElementById(id);

                chkButton.className = 'group' + groupNumber;
                objLink.className = 'group' + groupNumber;
                htxtValue.className = 'group' + groupNumber;
            }
        }
    }
})();

//GET ALL ASSESSMENT RESPONSE ELEMENTS ------------------------------------------------------

var arrRespCollection = [];
var portalCtrl = [];

(function()
{
    //get assessment container div
    var divCOM;
    var allDIV = document.getElementsByTagName('DIV');
    if (allDIV)
    {
        for (var a = 0; (divCOM = allDIV[a]) != null; a++)
        {
            if (divCOM.id.indexOf('divCOM') != -1)
            {
                break;
            }
            else
            {
                divCOM = false;
            }
        }

        if (divCOM)
        {
            //get current RID's
            var currRID = document.getElementById('curr_rids');
            if (currRID)
            {
                //get all elements with ID "portal_%"
                var x = divCOM.getElementsByTagName('*');
                var xEle;
                for (var i = 0; (xEle = x[i]) != null; i++)
                {
                    var eleID = xEle.id;
                    var pos = parseInt(eleID.lastIndexOf('_') + 1);
                    var eleRID = parseInt(eleID.substring(pos));
                    if ((eleID.indexOf('portal_') != -1) && (eleRID > 0))
                    {
                        portalCtrl[eleRID] = xEle;

                        //get curr_value
                        var currValue = document.getElementById('curr_value_' + eleRID);

                        //get curr_value_type 
                        var currValueType = document.getElementById('curr_value_type_' + eleRID);

                        if (currValue && currValueType)
                        {
                            objCtrl = new Object();
                            objCtrl.id = xEle.id;
                            objCtrl.rid = eleRID;
                            objCtrl.currType = currValueType.value;
                            objCtrl.portalItm = xEle;
                            objCtrl.currValue = currValue;
                            objCtrl.currValueType = currValueType;
                            arrRespCollection.push(objCtrl);
                        }

                    }
                }
            }
        }
    }
})();

function checkType99()
{
    if (arrRespCollection)
    {
        if (arrRespCollection.length > 0)
        {
            var o;
            for (var i = 0; (o = arrRespCollection[i]) != null; i++)
            {
                if (o.currType == "99")
                {
                    return true;
                    break;
                }
            }
        }
        else
        {
            return false;
        }
    }
}

function getType99Obj()
{
    if (arrRespCollection)
    {
        for (var a = 0; a < arrRespCollection.length; a++)
        {
            if (arrRespCollection[a].currType == 99)
            {
                return arrRespCollection[a];
                break;
            }
        }
    }
    return null;
}


//function for clearing other controls
function clearControl(obj)
{
    var x = parseInt(obj.currType);
    switch (x)
    {
        case 1:
            obj.portalItm.src = imgPortalRadioOff.src;
            obj.currValue.value = '0';
            break;

        case 2:
            obj.portalItm.src = imgPortalCheckOff.src;
            obj.currValue.value = '0';
            break;

        case 3:
            obj.portalItm.value = '';
            obj.currValue.value = '';
            break;

        case 4:
            obj.portalItm.selectedIndex = 0;
            obj.currValue.value = '';
            break;

        case 5:
            document.getElementById('portal_date_month_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_date_day_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_date_year_' + obj.rid).value = '';
            obj.currValue.value = '';
            break;

        case 6:
            break;

        case 7:
            break;

        case 8:
            var mList = document.getElementById('portal_list_' + obj.rid);
            if (mList)
            {
                for (var a = 0; a < mList.length; a++)
                {
                    mList.remove(a);
                }
            }
            var txtBox;
            for (var a = 1; a < 6; a++)
            {
                txtBox = document.getElementById('portal_listtxt' + a + '_' + obj.rid);
                if (txtBox)
                {
                    txtBox.value = '';
                }
            }
            obj.currValue.value = '';
            break;

        case 9:
            document.getElementById('portal_datetime_month_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_datetime_day_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_datetime_year_' + obj.rid).value = '';
            document.getElementById('portal_datetime_HH_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_datetime_MM_' + obj.rid).selectedIndex = 0;
            obj.currValue.value = '';
            break;

        case 10:
            document.getElementById('portal_bmi_feet_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_bmi_inches_' + obj.rid).selectedIndex = 0;
            document.getElementById('portal_bmi_lbs_' + obj.rid).value = '';
            obj.currValue.value = '';
            break;

        case 11:
            // n/a
            break;

        case 12:
            break;

        case 13:
            //
            break;

        case 14:
            break;

        case 15:
            break;

        case 99:
            obj.portalItm.src = imgPortalCheckOff.src;
            obj.currValue.value = '0';
            break;
    }
}

//reset the type99 checkbox
function resetType99()
{
    if (checkType99())
    {
        o = getType99Obj();
        clearControl(o);
    }
}

// ----------------------------------------
//		2011-06-21
//		Manage responses with attribute
//		NO_CLEARS_BELOW="NO_CLEARS_BELO"
// ----------------------------------------

var cbObj = null;
var nnn = 0;

(function()
{
    cbObj = hasAttribute('NO_CLEARS_BELOW');
    if (cbObj)
    {
        disableBelow(cbObj.objNextGroup);
        setClearsBelowOnclick(cbObj.obj, cbObj.objGroup);
    }

})();

function hasAttribute(attrName)
{
    var a = document.getElementsByTagName('*');
    var ctrType = null;
    var cObj;
    var cGrp;
    var reRDO = /portal_rdo_([0-9]+)/gi;
    var reCHK = /portal_chk_([0-9]+)/gi;
    var mValueObj;

    for (var b = 0, ele; (ele = a[b]) != null; b++)
    {
        if (ele.getAttribute(attrName))
        {
            cObj = ele;
            if (ele.className)
            {
                cGrp = ele.className;
                var reGRP = /(group)([0-9]+)/gi;
                if (cGrp.match(reGRP))
                {
                    var gNum = cGrp.replace(/\D/gi, '');
                }
            }
            if (ele.id.match(reRDO))
            {
                ctrType = 1;
            }
            if (ele.id.match(reCHK))
            {
                ctrType = 2;
            }

            mValueObj = document.getElementById(ele.id.replace(/(\D)+/gi, 'curr_value_'));

            break;
        }
    }
    if (cObj)
    {
        return {
            objID: cObj.id,
            obj: cObj,
            objType: ctrType,
            objGroup: cGrp,
            objNextGroup: 'group' + (parseInt(gNum) + 1).toString(),
            objCurrValue: mValueObj
        }
    }
    return null;
}

function disableBelow(grp)
{
    if (cbObj.objCurrValue.value == "" || cbObj.objCurrValue.value == "1")
    {
        var x = document.getElementsByClassName(grp);
        var reCtrl = /portal_(rdo|chk)_([0-9]+)/gi;
        for (var a = 0, obj; (obj = x[a]) != null; a++)
        {
            if (obj.id.match(reCtrl))
            {
                if (!obj.getAttribute('disabled'))
                {
                    obj.setAttribute('disabled', 'disabled');
                }
            }
        }
        clearsBelow(cbObj.objID, cbObj.objNextGroup);
    }

    if (cbObj.objCurrValue.value == "0")
    {
        var x = document.getElementsByClassName(grp);
        for (var a = 0, obj; (obj = x[a]) != null; a++)
        {
            obj.removeAttribute('disabled');
        }
    }
}

function enableBelow(obj, ctrlID, grp)
{
    var id = obj.id.replace(/(\D)+/gi, 'curr_value_');
    var currValue = document.getElementById(id);

    var id2 = ctrlID.replace(/(\D)+/gi, 'curr_value_');
    var currValue2 = document.getElementById(id2);

    if ((parseInt(currValue2.value) < 1 || currValue2.value.length < 1) && parseInt(currValue.value) > 0)
    {
        var x = document.getElementsByClassName(grp);
        var reCtrl = /portal_(rdo|chk)_([0-9]+)/gi;
        for (var a = 0, obj; (obj = x[a]) != null; a++)
        {
            if (obj.id.match(reCtrl))
            {
                obj.removeAttribute('disabled');
            }
        }
    }
}

function clearsBelow(objID, grp)
{
    var id = objID.replace(/(\D)+/gi, 'curr_value_');
    var currValue = document.getElementById(id);
    var reRDO = /portal_rdo_([0-9]+)/gi;
    var reCHK = /portal_chk_([0-9]+)/gi;

    var x = document.getElementsByClassName(grp);
    for (var a = 0, ele; (ele = x[a]) != null; a++)
    {
        if (parseInt(currValue.value) > 0)
        {
            var mID = ele.id.replace(/(\D)+/gi, 'curr_value_');
            var mCurrValue = document.getElementById(mID);
            if (mCurrValue)
            {
                mCurrValue.value = '0';
            }
            ele.setAttribute('disable', 'disable');

            if (ele.id.match(reRDO))
            {
                ele.src = imgPortalRadioOff.src;
            }
            if (ele.id.match(reCHK))
            {
                ele.src = imgPortalCheckOff.src;
            }
        }
    }
}

function setClearsBelowOnclick(ctrl, grp)
{
    var x = document.getElementsByClassName(grp);
    var pattern = /portal_(chk|rdo)_(link_)*?([0-9]+)/i;

    var gNum = grp.replace(/(\D)+/gi, '');
    var grp2 = 'group' + (parseInt(gNum) + 1);

    for (var a = 0, obj; (obj = x[a]) != null; a++)
    {
        if (obj.id.match(pattern))
        {
            if (obj.id != ctrl.id && obj.id != ctrl.id.replace(/(rdo|chk)/i, '$&_link'))
            {
                var oc = obj.getAttribute('onclick');
                obj.setAttribute('onclick', oc + ' enableBelow(this, \'' + ctrl.id + '\', \'' + grp2 + '\');');
            }
            else
            {
                var oc = obj.getAttribute('onclick');
                obj.setAttribute('onclick', oc + ' disableBelow(\'' + grp2 + '\');');
            }
        }
    }
}

// ------------------------------------------------------
//		2011-06-22
//		Manage responses with attribute
//		ENABLE_TEXT_ONCHECK="ENABLE_TEXT_ONCHECK"
// ------------------------------------------------------

(function()
{
    var hasTextAttribute = checkTextAttribute();
    if (hasTextAttribute)
    {
        disableTextBox();
    }
})();


function checkTextAttribute()
{
    //var pattern = /portal_(chk|rdo)_(link_)?\d/i;
    var pattern = /portal_(chk|rdo)_([0-9]+)/gi;
    var cObj = false;
    var hasTextAttribute = false;

    var x = document.getElementsByTagName('*');
    for (var a = 0, ele; (ele = x[a]) != null; a++)
    {
        if (ele.id.match(pattern))
        {
            if (ele.getAttribute('ENABLE_TEXT_ONCHECK'))
            {
                hasTextAttribute = true;
                cObj = ele;
                break;
            }
        }
    }
    if (cObj)
    {
        var id = cObj.id.replace(/(rdo|chk)/i, '$&_link');
        var mLink = document.getElementById(id);

        var oc = cObj.getAttribute('onclick');
        cObj.setAttribute('onclick', oc + 'setTxtOnClick(this);');
        mLink.setAttribute('onclick', oc + 'setTxtOnClick(this);');

        enableTextRadioCheck(cObj);
    }

    return hasTextAttribute;
}

function enableTextRadioCheck(obj)
{
    var mClass = obj.className;
    
    var reRadio = /portal_rdo_(.*)?/gi;
    var genRadios = [];
    var x = document.getElementsByTagName('*');
    if (x)
    {
        for (var i = 0, ele; (ele = x[i]) != null; i++)
        {
            if(ele.id)
            {
                if (reRadio.test(ele.id))
                {
                    genRadios.push(ele);
                } 
            } 
        }
    }
    
    for (var i = 0, rad; (rad = genRadios[i]) != null; i++)
    {
        if(rad.className == mClass)
        {
            if (rad.id != obj.id)
            {
                var oc = rad.getAttribute('onclick');
                rad.setAttribute('onclick', oc + 'disableTextBox();');
            }
        }
    }
}

function setTxtOnClick(cObj)
{
    var pattern = /([0-9]+)/gi;
    var id = 'curr_value_' + cObj.id.substring(cObj.id.lastIndexOf('_') + 1);
    var currValue = document.getElementById(id);
    if (parseInt(currValue.value) > 0)
    {
        enableTextBox();
    }
    else
    {
        disableTextBox();
    }
}

function disableTextBox()
{
    var pattern = /portal_txt_([0-9]+)/gi;
    var txtField = new Array();
    var x = document.getElementsByTagName('input');
    for (var a = 0, txt; (txt = x[a]) != null; a++)
    {
        if (txt.id.match(pattern))
        {
            txtField.push(txt);
        }
    }

    for (var a = 0, txt; (txt = txtField[a]) != null; a++)
    {
        var id = 'curr_value_' + txt.id.substring(txt.id.lastIndexOf('_') + 1);
        var currValue = document.getElementById(id);

        txt.value = '';
        currValue.value = '';
        txt.setAttribute('disabled', 'disabled');
    }
}

function enableTextBox()
{
    var pattern = /portal_txt_([0-9]+)/gi;
    var txtField = new Array();
    var x = document.getElementsByTagName('input');
    for (var a = 0, txt; (txt = x[a]) != null; a++)
    {
        if (txt.id.match(pattern))
        {
            txtField.push(txt);
        }
    }

    for (var a = 0, txt; (txt = txtField[a]) != null; a++)
    {
        txt.removeAttribute('disabled');
        txt.focus();
    }
}

/* 
---------------------------------------------------------------
PRIMARY / CONTRIBUTIN SELECTION FUNCTIONS
2011-06-28 - D.S.
---------------------------------------------------------------
*/

// **************************************************************
//  Other misconduct check
// **************************************************************

var otherMisconduct = null;
var ctrlMisconduct = null;
var txtMisconcduct = null;

// get other misconduct curr value 
(function()
{
    var rid;
    var a = document.getElementsByTagName('*');
    if (a)
    {
        for (var i = 0, ele; (ele = a[i]) != null; i++)
        {
            if (ele.getAttribute('PRI_OTHER'))
            {
                rid = ele.id.replace(/\D/gi, '');
                otherMisconduct = rid;
            }
        }
    }
    var mcVal = $G('curr_value_' + rid);
    if (mcVal)
    {
        ctrlMisconduct = mcVal;
    }
})();

// get other misconduct text box 
(function()
{
    var rid;
    var a = document.getElementsByTagName('*');
    if (a)
    {
        for (var i = 0, ele; (ele = a[i]) != null; i++)
        {
            if (ele.getAttribute('CHK_PRI_OTHER'))
            {
                txtMisconcduct = ele;
            }
        }
    }
})();

//enable/disable txtMisconduct
function setOtherMisconduct()
{
    if (ctrlMisconduct && txtMisconcduct)
    {
        txtMisconcduct.style.width = '320px';
        
        var prim = $G('portal_rdo_' + otherMisconduct + '_PRIMARY');
        var cont = $G('portal_rdo_' + otherMisconduct + '_CONTRIBUTING');

        if (prim && cont)
        {
            if (prim.src == imgPortalRadioOff.src && cont.src == imgPortalRadioOff.src)
            {
                txtMisconcduct.value = '';
                txtMisconcduct.disabled = true;
            }
            else
            {
                txtMisconcduct.disabled = false;
            }
        }
    }
}

setOtherMisconduct();


// Creates and Populates PRIMARY/CONTRIBUTING elements
// on page load and not every time the selection function is called
// When the function is called, it retrieves the element from the array by Index

var rdoPC = new Array();
rdoPC["PRIMARY"] = new Array();
rdoPC["CONTRIBUTING"] = new Array();
rdoPC["currValue"] = new Array();
rdoPC["MISCONDUCT"] = new Array();
rdoPC["MISCONDUCT"]["ATTRIBUTE"] = "CLEAR_ABOVE_ONCHECK";
rdoPC["MISCONDUCT"]["ID"] = null;

cRIDs = document.getElementById('curr_rids');
var currRids;
if (cRIDs)
{
    currRids = cRIDs.value;
    currRids = currRids.substring(0, currRids.length - 1);
    if (txtMisconcduct)
    {
        currRids = currRids.substring(0, currRids.lastIndexOf('|'));
    }
    currRids = currRids.split('|');
}
else
{
    currRids = [];
}

//get all primary/contributing elements
(function()
{
    var reP = /portal_rdo_([0-9]+)_PRIMARY/i; //primary
    var reC = /portal_rdo_([0-9]+)_CONTRIBUTING/i; //contributing
    var reV = /curr_value_([0-9]+)/i; //current_value

    var x = document.getElementsByTagName('*');
    for (var a = 0, obj; (obj = x[a]) != null; a++)
    {
        if (obj.name)
        {
            if (obj.name.match(reP))
            {
                var mIndex = parseInt(obj.name.replace(/\D/gi, ''));
                rdoPC["PRIMARY"][mIndex] = obj;
            }
            if (obj.name.match(reC))
            {
                var mIndex = parseInt(obj.name.replace(/\D/gi, ''));
                rdoPC["CONTRIBUTING"][mIndex] = obj;
            }
            if (obj.name.match(reV))
            {
                var mIndex = parseInt(obj.name.replace(/\D/gi, ''));
                rdoPC["currValue"][mIndex] = obj;
            }
            if (obj.getAttribute(rdoPC["MISCONDUCT"]["ATTRIBUTE"]))
            {
                var mID = parseInt(obj.name.replace(/\D/gi, ''));
                rdoPC["MISCONDUCT"]["ID"] = mID;
            }
        }
    }
})();

//Clear 'No misconduct' if other item is checked
function checkMisconduct(mIndex)
{
    if (mIndex != null)
    {
        if (rdoPC["currValue"][mIndex].value.length > 0)
        {
            if (parseInt(rdoPC["currValue"][mIndex].value) != 1)
            {
                rdoPC["currValue"][mIndex].value = '0';
                rdoPC["CONTRIBUTING"][mIndex].src = imgPortalRadioOff.src;
            }
        }
    }
}

//Clears all CONTRIBUTING when checking 'No misconduct'
function clearContributing()
{
    for (var a = 0, b; (b = currRids[a]) != undefined; a++)
    {
        var cIdx = parseInt(b);
        rdoPC["CONTRIBUTING"][cIdx].src = imgPortalRadioOff.src;
        if (rdoPC["currValue"][cIdx].value != '1')
        {
            rdoPC["currValue"][cIdx].value = '0';
        }
    }
}

//handles selecting primary/contributing
function portalSelectPrimaryContrib(strRID, strPorC)
{
    var mIndex = parseInt(strRID);
    var rdoThis = rdoPC[strPorC][mIndex];
    var rdoOther = rdoPC[(strPorC == 'PRIMARY') ? 'CONTRIBUTING' : 'PRIMARY'][mIndex];
    var currValue = rdoPC["currValue"][mIndex];

    if (rdoThis.src == imgPortalRadioOn.src) //from ON -> OFF
    {
        rdoThis.src = imgPortalRadioOff.src;
        rdoOther.src = imgPortalRadioOff.src;
        currValue.value = '0';
    }
    else //from OFF -> ON
    {
        if (strPorC == "PRIMARY")
        {
            for (var a = 0, b; (b = currRids[a]) != undefined; a++)
            {
                if (rdoPC['currValue'][b].value == "1")
                {
                    alert("You have already selected a PRIMARY reason!");
                    return;
                }
            }
            if (mIndex == rdoPC["MISCONDUCT"]["ID"])
            {
                clearContributing(); //Clears all CONTRIBUTING; 'No misconduct' is now PRIMARY
            }
            rdoThis.src = imgPortalRadioOn.src;
            rdoOther.src = imgPortalRadioOff.src;
            currValue.value = '1';
        }
        else //if selecting on CONTRIBUTING column
        {
            if (rdoPC["MISCONDUCT"]["ID"] != null) //if we have CLEAR_ABOVE_ONCHECK Attribute
            {
                var mcIndex = rdoPC["MISCONDUCT"]["ID"];
                if (mIndex != mcIndex) //if clicked RDO is not the Attribute owner
                {
                    if (rdoPC["currValue"][mcIndex].value != "1") //if "No misconduct" is not PRIMARY
                    {
                        checkMisconduct(mcIndex);
                        rdoThis.src = imgPortalRadioOn.src;
                        rdoOther.src = imgPortalRadioOff.src;
                        currValue.value = '2';
                    }
                    else
                    {
                        return false; //Prevents CONTRIBUTING selections when 'No misconduct' is PRIMARY
                    }
                }
                else //clicked RDO is the Attribute owner
                {
                    clearContributing();
                    rdoThis.src = imgPortalRadioOn.src;
                    rdoOther.src = imgPortalRadioOff.src;
                    currValue.value = '2';
                }
            }
            else //we don't have CLEAR_ABOVE_ONCHECK Attribute
            {
                rdoThis.src = imgPortalRadioOn.src;
                rdoOther.src = imgPortalRadioOff.src;
                currValue.value = '2';
            }
        }
    }

    resetType99();
    setOtherMisconduct();
    
}


// ---------------------------------------
//  Checkboxes
//  "None" Clears all
// ---------------------------------------

var objClearAll = null;
var clearGroupItems = null;
var objClearAllValue = null;

function getClearAllObj()
{
    var reChk = /portal_chk_([0-9]+)/gi; 
    var x = document.getElementsByTagName('img');
    if (x)
    {
        for (var i = 0; i < x.length; i++)
        {
            if (x[i].getAttribute('CLEAR_ABOVE_ONCHECK'))
            {
                if (reChk.test(x[i].id))
                {
                    objClearAll = x[i];
                    var oc = x[i].getAttribute('onclick');
                    x[i].setAttribute('onclick', oc + ' clearAbove(this);');

                    var cLink = document.getElementById('portal_chk_link_' + x[i].id.replace(/\D/gi, ''));
                    if (cLink)
                    {
                        cLink.setAttribute('onclick', oc + ' clearAbove(objClearAll);');
                    }

                    var oVal = document.getElementById('curr_value_' + x[i].id.replace(/\D/gi, ''));
                    if (oVal)
                    {
                        objClearAllValue = oVal.value;
                    }
                }
            }
        }
    }
}

function clearAbove(obj)
{
    var objValue = document.getElementById('curr_value_' + obj.id.replace(/\D/gi, ''));
    if (objValue && clearGroupItems != null)
    {
        if (objValue.value == '1')
        {
            for (var i = 0; i < clearGroupItems.length; i++ )
            {
                var itmValue = document.getElementById('curr_value_' + clearGroupItems[i].id.replace(/\D/gi, ''));
                if (itmValue)
                {
                    if(clearGroupItems[i].id != obj.id)
                    {
                        itmValue.value = '';
                        clearGroupItems[i].src = imgPortalCheckOff.src;
                    }
                }
            }
        }
    }
}

function getSameGroupObjs(obj)
{
    var objClassName = (obj.className) ? obj.className : null;
    var mId = obj.id.replace(/[0-9]/gi, '');
    var arrGrp = [];
    var arrAll = document.getElementsByTagName('img');
    if (obj.className)
    {
        for (var i = 0; i < arrAll.length; i++)
        {
            if (arrAll[i].id)
            {
                if (arrAll[i].id.indexOf(mId) > -1 && arrAll[i].id.indexOf('_link_') == -1)
                {
                    if (arrAll[i].className && objClassName)
                    {
                        if (arrAll[i].className == objClassName)
                        {
                            arrGrp.push(arrAll[i]);
                        }
                    }
                }
            }
        }
    }
    if (arrGrp.length > 0)
    {
        return arrGrp;
    } else
    {
        return null;
    }
}

function undoClearAbove()
{
    objClearAll.src = imgPortalCheckOff.src;
    objValue = document.getElementById('curr_value_' + objClearAll.id.replace(/\D/gi, ''));
    if (objValue)
    {
        objValue.value = '0';
    }
}


(function()
{
    getClearAllObj();
    if (objClearAll)
    {
        clearGroupItems = getSameGroupObjs(objClearAll);
    }
    if (clearGroupItems != null)
    {
        //set clear none
        for (var i = 0; i < clearGroupItems.length; i++)
        {
            if (clearGroupItems[i].id != objClearAll.id)
            {
                var oc = clearGroupItems[i].getAttribute('onclick');
                clearGroupItems[i].setAttribute('onclick', oc + ' undoClearAbove();');
                var cLink = document.getElementById('portal_chk_link_' + clearGroupItems[i].id.replace(/\D/gi, ''));
                if (cLink)
                {
                    cLink.setAttribute('onclick', oc + ' undoClearAbove();');
                }
            }
        }
    }

})();

// -------------------------------------------------
//  Force some css styling with JS
// -------------------------------------------------
(function()
{
    //adjust assessment styling
    var divResponses = [];
    var bDivResponses = false;
    var divCOM = getObject('divCOM');
    if (divCOM)
    {
        var allDIV = divCOM.getElementsByTagName('div');
        for (var i = 0; i < allDIV.length; i++)
        {
            if (allDIV[i].className)
            {
                if (allDIV[i].className == 'divResponses')
                {
                    divResponses.push(allDIV[i]);
                    bDivResponses = true;
                }
            }
        }
    }

    if (bDivResponses)
    {
        divComWidth = getComputedStyle(divCOM, null).getPropertyValue("width");
        if (!isNaN(parseInt(divComWidth)))
        {
            var nWidth = parseInt(divComWidth) - 8;
            for (var i = 0; i < divResponses.length; i++)
            {
                divResponses[i].style.width = nWidth + 'px';
            }
        }
    }
})();

