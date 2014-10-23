/************************************
      This set of functions is 
      not longer used
*************************************/


function selectTriChoice(grpname, index)
{
    var collection = document.getElementsByName(grpname);
    if (collection != null)
    {
        for (i=0;i<collection.length;i++)
        {
            if (collection[i].checked)
            {
                collection[i].checked = false;
                ClearRadio(collection[i].id);
            }
        }
        
        if (collection[index] != null)
        {
            collection[index].checked = true;
        }
    }
    onRadioAbove();
}


function onRadioAbove()
{
    var collection = document.getElementsByName('chkNoneofAbove');
    if (collection != null)
    {
        for (i=0;i<collection.length;i++)
        {
            collection[i].checked = false;
            ClearRadio(collection[i].id);
        }
    }
}

function selectNoneofAbove(grpname, index)
{
	var collection = document.getElementsByName(grpname);
	if (collection != null)
	{
		if (collection[index].checked)
		{
			collection[index].checked = false;
		}
		else
		{
			collection[index].checked = true;
		}
		onCheckNoneOfAbove();
	}
}

function onCheckNoneOfAbove()
{
	var collection = document.getElementsByName('grpRadio');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].checked = false;
            ClearRadio(collection[i].id);
		}
	}

	collection = document.getElementsByName('grpCheckbox');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].checked = false;
	        ClearCheck(collection[i].id);
        }
	}
			
	collection = document.getElementsByName('grpText');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].value = '';
		}
	}
	
	collection = document.getElementsByName('grpcmbHeight');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
	    {
			collection[i].value = '';
		}
	}

	collection = document.getElementsByName('txtHeightValue');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].value = '';
		}
	}
	
	collection = document.getElementsByName('txtWeightValue');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].value = '';
		}
	}
	
	collection = document.getElementsByName('cmbResponse');
	if (collection != null)
	{
	    for (i=0;i<collection.length;i++)
		{
			collection[i].value = 0;
		}
	}
	
	collection = document.getElementsByName('cmbDay');
	if (collection != null)
	{
	    for (i=0;i<collection.length;i++)
		{
			collection[i].value = 0;
    	}
	}

	collection = document.getElementsByName('cmbMonth');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].value = 0;
		}
	}

	collection = document.getElementsByName('cmbYear');
	if (collection != null)
	{
		for (i=0;i<collection.length;i++)
		{
			collection[i].value = 0;
		}
	}
}

function imposeMaxLength(Object, MaxLen)
{
    return (Object.value.length <= MaxLen);
}

function kmlbs(oForm)
{
	 var kg = oForm.txtWeightValue.value;
	 kg = Number(kg);
	 var lbs = kg * 2.2046226;
	 lbs = roundit(lbs,0);
	 oForm.txtWeightValue.value = lbs;
	 return true;
}

function lbskm(oForm)
{
    var lbs = oForm.txtWeightValue.value;
    lbs = Number(lbs);
    var kg = lbs * 0.45359237;
    kg = roundit(kg,0);
    oForm.txtWeightValue.value = kg;
    return true;
}

function roundit(nValue, nPlace)
{
    var multiplier = Math.pow(10, nPlace);
    var nRounded = Math.round(nValue * multiplier) / multiplier;
    return nRounded;
}

function cminch(oForm)
{
    var cm = oForm.txtHeightValue.value;
    cm = Number(cm);
    var inch = cm * 0.3937;
    inch = roundit(inch,0);
    oForm.txtHeightValue.value = inch;
    return true;
}

function inchcm(oForm)
{								
    var inch = oForm.txtHeightValue.value;
    inch = Number(inch);
    var cm = inch * 2.54;cm = roundit(cm,0);
    oForm.txtHeightValue.value = cm;
    return true;
}

var bAltLang = false;

function showInterest(strID, strImageSrc)
{
    document.getElementById(strID).src = strImageSrc;
}
function showDisinterest(strID, strImageSrc)
{
    document.getElementById(strID).src = strImageSrc;
}
function WaitForPageLoad()
{
	/*
	scrollTo(0,0);
	showdiv('divStatus');
	*/
}
function hidediv(id)
{
    if (document.getElementById)
    {
        document.getElementById(id).style.display = 'none';
    }
    else
    {
        if (document.layers)
        {
	        document.id.display = 'none';
        }
        else
        {
	        document.all.id.style.display = 'none';
        }
    }
};

//javascript to show a div tag
function showdiv(id)
{
  if (document.getElementById)
  {
	    document.getElementById(id).style.display = 'block';
  }
  else
  {
	  if (document.layers)
      {
		    document.id.display = 'block';
	  }
	  else
      {
	       document.all.id.style.display = 'block';
	  }
  }
}

function ToggleCheck(CheckID)
{
	var imgID = "chk_img_" + CheckID;
	var chkValueID = "chk_value_" + CheckID; 
	var chkValue = document.getElementById(chkValueID);
	var img = document.getElementById(imgID);
	if (chkValue != null) 
	{
		if(chkValue.value == 1){
		 	img.src = imgCheckOff.src;
		   	chkValue.value = 0;
		}
		else{
			img.src = imgCheckOn.src;
		   	chkValue.value = 1;
		}
	}
} 


function trim(s) 
{
    while (s.substring(0,1) == ' ') 
	{
		s = s.substring(1, s.length);
	}
	while (s.substring(s.length-1, s.length) == ' ') 
	{
		s = s.substring(0, s.length-1);
	}
	return s;
}

var childwin = null
var childopen = false;
function linkClick(strReportASP, strReport, strEncounter)
{
	var intMyWidth;
	var intMyHeight;
	
	var strReportName = strReportASP + "?report=" + strReport + "&encounter=" + strEncounter;
	//gets top and left positions based on user's resolution so window is centered.
	
	if (childopen)
	{
		childwin.close();
		childwin = null;
	}
		
	childwin = window.open(strReportName,"childwin","menubar=no,toolbar=no,location=no,status=no,title=no,resizable=yes");
	childopen = true;
	childwin.focus();
}

//end the questionnaire
function EndQuestionnaire()
{
	//resizeBy(0,20000);
	location.replace('app_module_finish.asp');
}

parent.parent.scrollTo(0,0);
	
ns4 = (navigator.appName=='Netscape')? true:false;  // old netscape won't recognize this
ie4 = ((navigator.appName=='Microsoft Internet Explorer') ||
	   (navigator.appName=='Netscape'))? true:false;  // ie and new netscape will catch this
			   
var bGaveDeleteWarning = false; 

function onclick_Table()
{
	if( !bGaveDeleteWarning )
	{
	    if(bAltLang)
	    {
    		alert("Las respuestas siguientes serán borradas SI CAMBIA SU RESPUESTA A ESTA PREGUNTA, y sólo las podrá guardar si visita este sitio otra vez.");
		}
		else
	    {
		    alert("Changing your response to this question will cause responses to subsequent questions to be deleted and only saved if visited again.");
		}
		
		bGaveDeleteWarning = true;
	}		
}
		
function onclick_Move(dir)
{
	var col1;
	col1 = document.getElementsByName('previous');
	if (col1 != null)
	{
		for(i=0; i<col1.length; i++)
		{
			col1[i].disabled = true;
		}
    }

    var col2;
	col2 = document.getElementsByName('Next');
	if (col2 != null)
	{
		/*for(z=0; i<col2.length; z++)
		{
			col2[z].disabled = true;
		}*/
		
		for(z=0; z<col2.length; z++)
		{
			col2[z].disabled = true;
		}
	}

	//document.forms[0].btnPrevious.disabled = true;
	var str = new String('');
	collection = document.getElementsByName('grpText');
	if (collection != null){
		for(i=0; i<collection.length; i++)
		{	
		    // make sure there isn't any commas in the text boxes.  
			// change them to carets and swap the back later.
			if (ie4) collection[i].style.visibility = 'hidden'
			else collection[i].style.visibility = 'hide'
			str = collection[i].value;
			str = str.replace(',','^');
			collection[i].value = collection.item(i).id + ':' + str;
		}
	}
	collection = null
	collection = document.getElementsByName('txtHeightValue');
	if (collection != null){
		for(i=0; i<collection.length; i++)
		{	
		    // make sure there isn't any commas in the text boxes.  
			// change them to carets and swap the back later.
			if (ie4) collection[i].style.visibility = 'hidden'
			else collection[i].style.visibility = 'hide'
			str = collection[i].value;
			str = str.replace(',','^');
			collection[i].value = collection.item(i).id + ':' + str;
		}
	}
	collection = null
	collection = document.getElementsByName('txtWeightValue');
	if (collection != null){
		for(i=0;i<collection.length;i++)
		{	
		    // make sure there isn't any commas in the text boxes.  
			// change them to carets and swap the back later.
			if (ie4) collection[i].style.visibility = 'hidden'
			else collection[i].style.visibility = 'hide'
			str = collection[i].value;
			str = str.replace(',','^');
			collection[i].value = collection.item(i).id + ':' + str;
		}
	}
	
	// COMMENT #1
	//This code handles the MONTH / YEAR bar. Places the text into the hidden
	//field of the bar after the next key has been pressed -
	collection = null
	collection = document.getElementsByName('grpMonthYear');
	collMonth = document.getElementsByName('cmbMonth');
	collYear = document.getElementsByName('cmbYear');
	if (collection != null){
		for(i=0;i<collection.length;i++)
		{	
			str = "";
			strMonth = "";
			strYear = "";
			if ((collMonth != null) &&
				(collYear  != null))
			{
				if ((collMonth.length > 0) && (collMonth[i].value > 0))
				{
					if (collMonth[i].value < 10)
						strMonth = "0" + collMonth[i].value + "/";
					else
						strMonth = collMonth[i].value + "/";
						strDay = "01" + "/";
				}
			
				if ((collYear.length > 0) && (collYear[i].value > 0))
				{
					strYear = collYear[i].value;
				}
			}
		collection[i].value = collection.item(i).id + ':' + strMonth + strYear;
		}
	}
	
	// COMMENT #2
	//This code handles the MONTH / DAY bar. Places the text into the hidden
	//field of the bar after the next key has been pressed -
	collection = null
	collection = document.getElementsByName('grpMonthDay');
	collMonth = document.getElementsByName('cmbMonth');
	collDay = document.getElementsByName('cmbDay');
	
	if (collection != null){
		for(i=0;i<collection.length;i++)
		{	
			str = "";
			strMonth = "";
			strDay = "";
			if ((collMonth != null) && (collDay  != null))
			{
				if ((collMonth.length > 0) && (collMonth[i].value > 0))
				{
					//if (collMonth[i].value < 10)
					//	strMonth = "0" + collMonth[i].value + "/";
					//else
						strMonth = collMonth[i].value + "/";
					//	strDay = "01" + "/";
				}
			
				if ((collDay.length > 0) && (collDay[i].value > 0))
				{
					strDay = collDay[i].value;
				}
			}
			
		collection[i].value = collection.item(i).id + ':' + strMonth + strDay;
		}
	}
	
	
	collection = null
	collection = document.getElementsByName('grpDateResponse');
	collDay = document.getElementsByName('cmbDay');
	collMonth = document.getElementsByName('cmbMonth');
	collYear = document.getElementsByName('cmbYear');
	if (collection != null){
		for(i=0;i<collection.length;i++)
		{	
			str = "";
			strDay = "";
			strMonth = "";
			strYear = "";
			if ((collDay != null) &&
				(collMonth != null) &&
				(collYear  != null))
			{
				if ((collMonth.length > 0) && (collMonth[i].value > 0))
				{
					if (collMonth[i].value < 10)
						strMonth = "0" + collMonth[i].value + "/";
					else
						strMonth = collMonth[i].value + "/";
						strDay = "01" + "/";
				}
			
				if ((collDay.length > 0) && (collDay[i].value > 0))
				{
					if (collDay[i].value < 10)
						strDay = "0" + collDay[i].value + "/";
					else
						strDay = collDay[i].value + "/";
					
				}
				if ((collYear.length > 0) && (collYear[i].value > 0))
				{
					strYear = collYear[i].value;
				}
			}
		collection[i].value = collection.item(i).id + ':' + strMonth + strDay + strYear;
		}
	}
	collection = null
	collection = document.getElementsByName('grpcmbResponse');
	collResponse = document.getElementsByName('cmbResponse');
	if (collection != null){
		for(i=0;i<collection.length;i++)
		{	
			collection[i].value = collection.item(i).id + ':' + collResponse.item(i).value;
		}
	}
	document.forms[0].txtDirection.value = dir;
}

function onclick_Save()
{
	document.forms[0].btnSave.disabled = true;
	document.forms[0].btnPtReport.disabled = true;
	onclick_Move("save")
}

function ClearRadio(RadioID)
{
    var imgID = "rdo_img_" + RadioID;
    var img = document.getElementById(imgID);
    if(img != null)
    {
        img.src = imgRadioOff.src;
    }
    
    var rdoValueID = "rdo_value_" + RadioID; 
    var rdoValue = document.getElementById(rdoValueID);
    if(rdoValue != null)
    {
        rdoValue.value = 0;
    }
}

function ClearRadioButtons(radGrp, radID)
{
	var radCollection = $('input[type="radio"][name="'+ radGrp +'"]');
	var imgRadCollection = $('img[name="'+ radGrp +'"]');
				
	//clar all radios
	$.each(radCollection, function(i, ele)
	{
		$(ele).removeAttr('checked');
		$('input[id$="rdo_value_'+ radID +'"]').val('0');
	});
				
	//clar all img radios
	$.each(imgRadCollection, function(i, ele)
	{
		ele.src = imgRadioOff.src;
	});
}

		
function ToggleRadio(radID)
{
	var rad = $('input[id$="'+ radID +'"]').get(0);
	var imgRad = $('img[id$="'+ radID +'"]').get(0);
	if(rad.checked)
	{
		$(rad).removeAttr('checked');
		$('input[id$="rdo_value_'+ radID +'"]').val('0');
		imgRad.src = imgRadioOff.src;
	}
	else
	{
		$(rad).attr('checked','checked');
		$('input[id$="rdo_value_'+ radID +'"]').val('1');
		imgRad.src = imgRadioOn.src;
	}
}

function ClearCheckBoxes()
{
    var collection = document.getElementsByName('grpCheckbox');
    if (collection != null)
    {
        for (i = 0; i < collection.length; i++)
        {
            collection[i].checked = false;
            ClearCheck(collection[i].id);
        }
    }
    
    collection = document.getElementsByName('chkNoneofAbove');
    if (collection != null)
    {
	    for(i = 0; i < collection.length; i++)
	    {
		    collection[i].checked = false;
		    ClearCheck(collection[i].id);
	    }
    }
}

function ClearCheck(CheckID)
{
	var chkValueID = "chk_value_" + CheckID; 
    var chkValue = document.getElementById(chkValueID);
    if(chkValue != null)
    {
	    chkValue.value = 0;
    }
    
    var imgID = "chk_img_" + CheckID;
    var img = document.getElementById(imgID);
    if(img != null)
    {
	    img.src = imgCheckOff.src;
    }
}	  

function selectCheck(grpname, index)
{
    var collection = document.getElementsByName(grpname);
    if (collection != null)
    {
	    if (collection[index].checked)
	    {
		    collection[index].checked = false;
	    }
	    else
	    {
		    collection[index].checked = true;
	    }
	    onCheckAbove();
    }

    var obj = document.getElementById('DATE1_1');
    if(obj != null)
    {
        obj.selectedIndex = -1;
    }

    obj = document.getElementById('DATE2_1');
    if(obj != null)
    {
        obj.selectedIndex = -1;
    }

    obj = document.getElementById('DATE3_1');
    if(obj != null)
    {
        obj.selectedIndex = -1;
    }
}

function onCheckAbove()
{
    var collection = document.getElementsByName('chkNoneofAbove');
    if (collection != null)
    {
	    for (i = 0; i < collection.length; i++)
	    {
		    collection[i].checked = false;
    	    ClearCheck(collection[i].id);
	    }
    }
}

function ClearNoneCheckBoxes()
{
	var collection = document.getElementsByName('chkNoneofAbove');
	if (collection != null)
	{
		for (i = 0; i < collection.length; i++)
		{
			collection[i].checked = false;
			ClearCheck(collection[i].id);
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



})();