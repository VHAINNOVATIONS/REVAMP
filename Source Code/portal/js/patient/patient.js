﻿var patient = new Object();

//---------------------------------------------------------
//namespace for DEMOGRAPHICS
patient.demo = {
	
	//alert user to save their work before
	//moving to another tab on the screen
	alertTabChanged: function(sender, args)
	{
		var m_tabCont = $find($('[id$="tabContDemographics"]').get(0).id),
		    m_activeTab = m_tabCont.get_activeTab(),
		    msg = 'Please make sure that you saved you work on this screen before moving to another tab. ';
		    msg += 'If you continue without saving, all your changes will be lost. ';
			msg += 'Do you wish to continue?';
			
		var v_tabConfirm = confirm(msg);
		if(!v_tabConfirm)
		{
			m_tabCont.set_activeTab(m_activeTab);
		}
	}
	
};

//---------------------------------------------------------
//namespace for MILITARY DETAILS
patient.mil = {

    //autofill fmp/ssn if status is active duty
    statusChange: function(obj)
    {
        //get the current instance 
        // -- using "me" variable to prevent conflicts
        // when iterating arrays/objects with the "this" reference
        // inside a jQuery $.each() method  --
        var me = this;
        
        //get hidden field patient with hash of patient's info
        var elePatData = $('[id$="htxtPatDemo"]').get(0);
        
        //get the FMP/SSN textbox in the military details view
        var txtFMP = $('[id$="txtFMP"]').get(0);

        if (elePatData)
        {
            //create an object with the data in the patient's data hidden field
            // --the data in this field is a JSON string--
            var patdata = eval(elePatData.value);
            
            //there is only one element(record) in the object; 
            //that's why manually using the index 0
            var objPat = patdata[0];
            
            //get ssn from the object (by key "ssn")
            var ssn = objPat.ssn;
            
            //format the SSN with the dashes
            var _ssn = ssn.substring(0, 3) + '-' + ssn.substring(3, 5) + '-' + ssn.substring(5);
            
            //get the selected value from the "Status" dropdown
            var selVal = parseInt(obj.value);
            
            //for debug only:
            //console.log('Selected-VAlue: %s', selVal);
            
            //if the selected value is one of the "Active Duty" collection
            if (selVal > 0 && selVal <= 6)
            {
                txtFMP.value = '20/' + _ssn;
            }
            else
            {
                txtFMP.value = '';
            }
        }
    }

};

//---------------------------------------------------------
//COMMON
patient.common = {};