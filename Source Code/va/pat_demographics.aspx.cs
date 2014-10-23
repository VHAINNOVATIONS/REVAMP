using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class pat_demographics : System.Web.UI.Page
{
    public enum DEMOView : long
    {
        PATIENT = 0,
        MILITARY_DETAILS = 1,
        SPONSOR = 2,
        CONTACT = 3,
        INSURANCE = 4,
        PORTAL_ACCOUNT = 5
    }
    
    #region Constants
    const string cnActiveDuty = "20";
    
    //Patient Demographics
    const int cnFirstName                   = 101;
    const int cnMiddleName                  = 102;
    const int cnLastName                    = 103;
    const int cnFMPSSN                      = 104;
    const int cnFMPSSNConfirm               = 105;
    const int cnEDIPN                       = 106;
    const int cnAddress1                    = 107;
    const int cnAddress2                    = 108;
    const int cnCity                        = 109;
    const int cnPostalCode                  = 110;
    const int cnDateOfBirth                 = 111;
    const int cnWorkPhone                   = 112;
    const int cnHomePhone                   = 113;
    const int cnPatEmail                    = 114;

    //Patient Sponsor
    const int cnPatSponsorName              = 201;
    const int cnPatSponsorWorkPhone         = 202;
    const int cnPatSponsorHomePhone         = 203;
    const int cnPatSponsorStreetAddress     = 204;
    const int cnPatSponsorCity              = 205;
    const int cnPatSponsorPostalCode        = 206;
    const int cnPatSponsorRelationship      = 207;

    //Patient Emergency Contact
    const int cnPatEmergencyName            = 301;
    const int cnPatEmergencyWorkPhone       = 302;
    const int cnPatEmergencyHomePhone       = 303;
    const int cnPatEmergencyStreetAddress   = 304;
    const int cnPatEmergencyCity            = 305;
    const int cnPatEmergencyPostalCode      = 306;
    const int cnPatEmergencyRelationship    = 307;

    //Patient Military Details
    const int cnPatMilDetailsFMPSSN         = 401;

    //FMPSSN SIZE
    const int cnFMPSSN_Size                 = 11;

    #endregion
    string m_strFMPSSN = "";
    string m_strFMPSSNConfirm = "";
    string m_strDOB = "-1";
    string m_strGender = "";
    string m_strNewPatientID = "";
    string m_strNewEncounterID = "";

    bool m_bNewPatient = true;

    protected CSec usrsec = new CSec();
    public long lPatInfoRightMode;
    public bool bRecLocked;

    protected void Page_Init(object sender, EventArgs e) 
    {
        //get mastersave control
        Button btnMasterSave = (Button)Master.FindControl("btnMasterSave");
        AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
        trigger.ControlID = btnMasterSave.ID;
        upWrapperUpdatePanel.Triggers.Add(trigger);

        UpdatePanel upPatDemo = (UpdatePanel)Master.FindControl("upPatientDemo");
        upPatDemo.Triggers.Add(trigger);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lPatInfoRightMode = usrsec.GetRightMode(Master, (long)SUATUserRight.ProcessNewPatientsUR);

        //pass the master to the user control
        ucPatientPortalAccount.BaseMstr = Master;
        ucPAPDevice.BaseMstr = Master;

        if (Master.IsPatientLocked) 
        {
            lPatInfoRightMode = (long)RightMode.ReadOnly;
        }

        //disable the cpap machine tab until the patient is created (patient_id assigned)
        if (String.IsNullOrEmpty(Master.SelectedPatientID))
        {
            lnkCPAPMachine.Enabled = false;
        }
        else
        {
            lnkCPAPMachine.Enabled = true;
        }

        SetJSAttributes();

        //-----------------------------------------------------------------------
        //      NOT POSTBACK
        //-----------------------------------------------------------------------
        #region NotPostback
        if (!IsPostBack)
        {
            rblHomePhoneMsg.ClearSelection();
            
            //Load combos
            loadAllCombos();
            LoadEthnicityRaceLists();

            CCPAPResults cpap = new CCPAPResults(Master);
            htxtDevicePatient.Value = cpap.GetDevicePatient();
            
            bool bNewEntry = Convert.ToBoolean(Request.QueryString["newpatient"]);
           
            if (bNewEntry)
            {
                Master.ClosePatient();
            }

            if (Master.SelectedPatientID == "")
            {
                Master.SetVSValue("IsNewPatient", true);
                addNewPatient();
            }
            else
            {
                Master.SetVSValue("IsNewPatient", false);
                loadPatient();

                loadEmergencyContactInput();
                ucPatientPortalAccount.loadPatientPortalAccount();
            }

            //set hidden field's value to the current tab index
            htxtCurrTab.Value = tabContDemographics.ActiveTabIndex.ToString();
        }
        #endregion
        
        //if the user pushed the master save button
        //then save the form
        if (IsPostBack)
        {
            if (Master.OnMasterSAVE())
            {
                Session["PATIENTNAME"] = null;
                
                Save();

                if (Master.StatusCode <= 0)
                {
                    Master.SetVSValue("VS_PATIENT_TREATMENTS", "");
                    upWrapperUpdatePanel.Update();

                    if (Master.GetVSBoolValue("ReloadPage"))
                    {
                        ViewState["ReloadPage"] = null;
                        Response.Redirect("pat_demographics.aspx");
                    }

                }
                else
                {
                    ShowSysFeedback();
                }
            } 
        }

        //check patient's record status
        bRecLocked = Master.IsPatientLocked;

        CheckUserRightsMode();
    }

    protected void SetJSAttributes()
    {
        txtFMPSSN.Attributes.Add("onkeypress", "return maskFMPSSN3(event, this.value, this);");
        txtFMPSSN_Confirm.Attributes.Add("onkeypress", "return maskFMPSSN3(event, this.value, this, false);");

        txtDateOfBirth.Attributes.Add("onkeypress", "return maskDate(event, this.value, this);");
        txtPostCode.Attributes.Add("onkeypress", "return maskZipCode(event, this.value, this);");
        txtEmergencyPostCode.Attributes.Add("onkeypress", "return maskZipCode(event, this.value, this);");

        txtCelPhone.Attributes.Add("onkeypress", "return maskPhone(event, this.value, this);");
        txtHomePhone.Attributes.Add("onkeypress", "return maskPhone(event, this.value, this);");
    }

    protected void LoadEthnicityRaceLists()
    {
        CPatEthnicityRace per = new CPatEthnicityRace();

        rblEthnicity.DataTextField = "ETHNICITY_NAME";
        rblEthnicity.DataValueField = "ETHNICITY_ID";
        rblEthnicity.DataSource = per.GetEthnicityDS(Master);
        rblEthnicity.DataBind();

        cblRace.DataTextField = "RACE_TITLE";
        cblRace.DataValueField = "RACE_ID";
        cblRace.DataSource = per.GetRaceDS(Master);
        cblRace.DataBind();

        rblSource.DataTextField = "ETH_RACE_SOURCE_NAME";
        rblSource.DataValueField = "ETH_RACE_SOURCE_ID";
        rblSource.DataSource = per.GetEthRaceSourceDS(Master);
        rblSource.DataBind();
    }
    
    protected void loadAllCombos() 
    {
        //LOAD COMBOS --------------------------------------------------------------------

        //demographic State
        CDemographics Demographics = new CDemographics();
        CMilitaryRender MilitaryRender = new CMilitaryRender();

        Demographics.LoadDemStateDropDownList(Master,
                                            cboState,
                                            "");

        //demographic Gender
        Demographics.LoadDemGenderDropDownList(Master,
                                            cboGender,
                                            "");

        CProvider Provider = new CProvider();
        Provider.LoadBaseProviderDDL(Master,
                                     cboProvider,
                                     "",
                                     Master.APPMaster.UserDMISID);

        //load emergency contact
        CDemographics emergState = new CDemographics();
        CDemographics emergRelationship = new CDemographics();
        emergRelationship.LoadDemRelationshipDropDownList(Master,
                                            cboEmergencyRelationship,
                                            "");

        emergState.LoadDemStateDropDownList(Master,
                                            cboEmergencyState,
                                            "");

        //--------------------------------------------------------------------

    }

    protected void addNewPatient()
    {       
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        txtFMPSSN.Text = "";
        txtFMPSSN_Confirm.Text = "";
        txtDateOfBirth.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCity.Text = "";
        txtPostCode.Text = "";
        txtWorkPhone.Text = "";
        txtHomePhone.Text = "";

        cboGender.ClearSelection();
        cboGender.SelectedIndex = -1;

        rblEthnicity.ClearSelection();
        cblRace.ClearSelection();
        rblSource.ClearSelection();

        cboProvider.ClearSelection();
        cboProvider.SelectedIndex = -1;

        cboState.ClearSelection();
        cboState.SelectedIndex = -1;

        rblHomePhoneMsg.SelectedValue = "1";
        rblEmailMessage.SelectedValue = "1";

        tabContDemographics.ActiveTab = lnkPatient;
    }

    protected bool LoadPatEthnicityRaceSource()
    {
        CPatEthnicityRace per = new CPatEthnicityRace();

        DataSet dsEthnicity = per.GetPatientEthnicityDS(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        if (dsEthnicity.Tables[0].Rows.Count > 0)
        {
            rblEthnicity.SelectedValue = dsEthnicity.Tables[0].Rows[0]["ETHNICITY_ID"].ToString(); 
        }

        DataSet dsRace = per.GetPatientRaceDS(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        cblRace.ClearSelection();

        foreach (DataRow dr in dsRace.Tables[0].Rows)
        {
            foreach (ListItem li in cblRace.Items)
            {
                if (dr["RACE_ID"].ToString() == li.Value)
                {
                    li.Selected = true;
                }
            }
        }

        DataSet dsSource = per.GetPatEthRaceSourceDS(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        if (dsSource.Tables[0].Rows.Count > 0)
        {
            rblSource.SelectedValue = dsSource.Tables[0].Rows[0]["ETH_RACE_SOURCE_ID"].ToString(); 
        }

        return true;
    }

    protected void loadPatient()
    {
        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();

        if(Session["PAT_DEMOGRAPHICS_DS"] == null)
        {
            Session["PAT_DEMOGRAPHICS_DS"] = pat.GetPatientDemographicsDS(Master);
        }

        DataSet clientDemographics = (DataSet)Session["PAT_DEMOGRAPHICS_DS"];

        //create a hash with the patient's address data
        //to automatically fill out textboxes in the
        //Sponsor view if the "Same as patient" checkbox is checked
        getPatAddress(clientDemographics);

        CDropDownList DropDownList = new CDropDownList();
        CMilitaryRender MilitaryRender = new CMilitaryRender();
        CDemographics Demographics = new CDemographics();

        //load all of the user's available fields
        if (clientDemographics != null)
        {
            //2012-02-15 DS
            //create a hash of patient demo data in a hidden field
            //so it can be retrieved on specific client side event 
            //without a new roundtrip to the server

            htxtPatDemo.Value = utils.GetJSONString(clientDemographics);

            //clear all preference dropdown
            cboCallPreference.SelectedValue = "0";

            foreach (DataTable patTable in clientDemographics.Tables)
            {
                foreach (DataRow patRow in patTable.Rows)
                {
                    Master.APPMaster.PatientHasOpenCase = false;
                    if (!patRow.IsNull("OPENCASE_COUNT"))
                    {
                        if (Convert.ToInt32(patRow["OPENCASE_COUNT"]) > 0)
                        {
                            Master.APPMaster.PatientHasOpenCase = true;
                        }
                    }

                    // if the column for the first name isn't null
                    // then
                    // assign the string for the column
                    if (!patRow.IsNull("FIRST_NAME"))
                    {
                        txtFirstName.Text = patRow["FIRST_NAME"].ToString();
                    }

                    // middle initial
                    txtMiddleName.Text = "";
                    if (!patRow.IsNull("MI"))
                    {
                        txtMiddleName.Text = patRow["MI"].ToString();
                    }

                    // last name
                    if (!patRow.IsNull("LAST_NAME"))
                    {
                        txtLastName.Text = patRow["LAST_NAME"].ToString();
                    }

                    string strFMPSSN_Concat = "";
                    string strFMPSSN_Confirm_Concat = "";

                    if (!patRow.IsNull("SSN"))
                    {
                        string strSponsorSSN = patRow["SSN"].ToString();
                        strFMPSSN_Concat = strSponsorSSN.Substring(0, 3) + "-" + strSponsorSSN.Substring(3, 2) + "-" + strSponsorSSN.Substring(5);
                    }
                    txtFMPSSN.Text = strFMPSSN_Concat;

                    if (!patRow.IsNull("SSN"))
                    {
                        string strConfirmSSN = patRow["SSN"].ToString();
                        strFMPSSN_Confirm_Concat = strConfirmSSN.Substring(0, 3) + "-" + strConfirmSSN.Substring(3, 2) + "-" + strConfirmSSN.Substring(5);
                    }
                    txtFMPSSN_Confirm.Text = strFMPSSN_Confirm_Concat;

                    if (!patRow.IsNull("GENDER"))
                    {
                        if (patRow["GENDER"].ToString() == "M")
                        {
                            DropDownList.SelectValue(cboGender, "1");
                        }
                        else if (patRow["GENDER"].ToString() == "F")
                        {
                            DropDownList.SelectValue(cboGender, "2");
                        }
                    }

                    if (!patRow.IsNull("DOB"))
                    {
                        //Convert to Short Date - remove TimeStamp
                        DateTime dtBirthDate = Convert.ToDateTime(patRow["DOB"]);
                        dtBirthDate.ToShortDateString();
                        txtDateOfBirth.Text = dtBirthDate.ToString("MM/dd/yyyy");
                    }

                    if (!patRow.IsNull("PROVIDER_ID"))
                    {
                        DropDownList.SelectValue(cboProvider, Convert.ToString(patRow["PROVIDER_ID"]));
                    }

                    if (!patRow.IsNull("ADDRESS1"))
                    {
                        txtAddress1.Text = patRow["ADDRESS1"].ToString();
                    }

                    if (!patRow.IsNull("ADDRESS2"))
                    {
                        txtAddress2.Text = patRow["ADDRESS2"].ToString();
                    }

                    if (!patRow.IsNull("CITY"))
                    {
                        txtCity.Text = patRow["CITY"].ToString();
                    }

                    if (!patRow.IsNull("POSTAL_CODE"))
                    {
                        txtPostCode.Text = patRow["POSTAL_CODE"].ToString();
                    }

                    if (!patRow.IsNull("HOMEPHONE"))
                    {
                        string strHomePhone = Regex.Replace(patRow["HOMEPHONE"].ToString(), @"[\(|\)|\s|\.|\-]", String.Empty);
                        txtHomePhone.Text = Regex.Replace(strHomePhone, @"(\d{3})(\d{3})(\d{4})", "($1)$2-$3");
                    }

                    if (!patRow.IsNull("CELLPHONE"))
                    {
                        string strCellPhone = Regex.Replace(patRow["CELLPHONE"].ToString(), @"[\(|\)|\s|\.|\-]", String.Empty);
                        txtCelPhone.Text = Regex.Replace(strCellPhone, @"(\d{3})(\d{3})(\d{4})", "($1)$2-$3");
                    }

                    if (!patRow.IsNull("WORKPHONE"))
                    {
                        txtWorkPhone.Text = patRow["WORKPHONE"].ToString();
                    }

                    if (!patRow.IsNull("EMAIL"))
                    {
                        txtPatEmail.Text = patRow["EMAIL"].ToString();
                    }

                    if (!patRow.IsNull("STATE_ID"))
                    {
                        DropDownList.SelectValue(cboState, Convert.ToString(patRow["STATE_ID"]));
                    }

                    if (!patRow.IsNull("HOME_PHONE_CALL"))
                    {
                        if (Convert.ToInt32(patRow["HOME_PHONE_CALL"]) == 1)
                        {
                            cboCallPreference.SelectedValue = "1";
                        }
                    }

                    if (!patRow.IsNull("CELL_PHONE_CALL"))
                    {
                        if (Convert.ToInt32(patRow["CELL_PHONE_CALL"]) == 1)
                        {
                            cboCallPreference.SelectedValue = "2";
                        }
                    }

                    if (!patRow.IsNull("WRK_PHONE_CALL"))
                    {
                        if (Convert.ToInt32(patRow["WRK_PHONE_CALL"]) == 1)
                        {
                            cboCallPreference.SelectedValue = "4";
                        }
                    }

                    if (!patRow.IsNull("HOME_PHONE_MSG"))
                    {
                        rblHomePhoneMsg.SelectedValue = patRow["HOME_PHONE_MSG"].ToString();
                    }

                    if (!patRow.IsNull("EMAIL_MSG"))
                    {
                        rblEmailMessage.SelectedValue = patRow["EMAIL_MSG"].ToString();
                    }
                }
            }
        }

        ucPAPDevice.LoadPatientDevice();
        LoadPatEthnicityRaceSource();
    }
    
    protected void loadEmergencyContactInput()
    {
        //demographic Emergency Contact State
        CDropDownList emergStateList = new CDropDownList();
        CDropDownList emergRelationshipList = new CDropDownList();

        CPatient pat = new CPatient();
        DataSet patEmergContact = pat.GetPatientEmergencyContactDS(Master);

        //load all available fields
        if (patEmergContact != null)
        {
            foreach (DataTable patTable in patEmergContact.Tables)
            {
                foreach (DataRow patRow in patTable.Rows)
                {
                    if (!patRow.IsNull("NAME"))
                    {
                        txtEmergencyName.Text = patRow["NAME"].ToString();
                    }
                    if (!patRow.IsNull("RELATIONSHIP_ID"))
                    {
                        emergRelationshipList.SelectValue(cboEmergencyRelationship, Convert.ToString(patRow["RELATIONSHIP_ID"]));
                    }
                    if (!patRow.IsNull("ADDRESS1"))
                    {
                        txtEmergencyAddress1.Text = patRow["ADDRESS1"].ToString();
                    }
                    if (!patRow.IsNull("CITY"))
                    {
                        txtEmergencyCity.Text = patRow["CITY"].ToString();
                    }
                    if (!patRow.IsNull("POSTAL_CODE"))
                    {
                        txtEmergencyPostCode.Text = patRow["POSTAL_CODE"].ToString();
                    }
                    if (!patRow.IsNull("WORKPHONE"))
                    {
                        txtEmergencyWPhone.Text = patRow["WORKPHONE"].ToString();
                    }
                    if (!patRow.IsNull("HOMEPHONE"))
                    {
                        txtEmergencyHPhone.Text = patRow["HOMEPHONE"].ToString();
                    }
                    if (!patRow.IsNull("STATE_ID"))
                    {
                      emergStateList.SelectValue(cboEmergencyState, Convert.ToString(patRow["STATE_ID"]));
                    }
                }
            }

        }
    }

    protected void ConvertPatientDemographics()
    {
        m_strFMPSSN = txtFMPSSN.Text.Replace("-", "");
        m_strFMPSSNConfirm = txtFMPSSN_Confirm.Text.Replace("-", "");

        if (txtDateOfBirth.Text.Trim().Length > 0)
        {
            m_strDOB = txtDateOfBirth.Text.Trim(); 
        }

        if (cboGender.SelectedValue.ToString() == "0")
        {
            m_strGender = "U";
        }
        else if (cboGender.SelectedValue.ToString() == "1")
        {
            m_strGender = "M";
        }
        else if (cboGender.SelectedValue.ToString() == "2")
        {
            m_strGender = "F";
        }
    }

    protected long getPatientFXUserID()
    {
        long Value = 0;

        CSec fxSec = new CSec();

        DataSet SecSet = new DataSet();
        SecSet = fxSec.GetPatientFXUserIdDS(Master);

        //load all of the user's available fields
        if (SecSet != null)
        {
            foreach (DataTable secTable in SecSet.Tables)
            {
                foreach (DataRow secRow in secTable.Rows)
                {
                    if (!secRow.IsNull("FX_USER_ID"))
                    {
                        Value = Convert.ToInt64(secRow["FX_USER_ID"]);
                    }
                }
            }
        }

        return Value;
    }
        
    protected string CheckPatientDemoErrors()
    {
       string strErrMessage = "";
       string strStatus = "";

       CPatient patdemo = new CPatient();

       if (cboGender.SelectedItem.Value == "-1")
       {
           strErrMessage += "Please select a Gender.<br />";
       }

       if (cboProvider.SelectedItem.Value == "-1")
       {
           strErrMessage += "Please select a Sleep Specialist.<br />";
       }

       int iFMPSSNCount = 0;
       iFMPSSNCount = txtFMPSSN.Text.Length;
       if (iFMPSSNCount != cnFMPSSN_Size)
       {
           strErrMessage += "SSN Does not contain enough characters. Please check.<br />";
       }

       int iFMPSSNConfirmCount = 0;
       iFMPSSNConfirmCount = txtFMPSSN_Confirm.Text.Length;
       if (iFMPSSNConfirmCount != cnFMPSSN_Size)
       {
           strErrMessage += "SSN Confirm does not contain enough characters. Please check.<br />";
       }

       if (txtFMPSSN.Text != txtFMPSSN_Confirm.Text)
       {
           strErrMessage += "SSN different than confirmation SSN. Please check.<br />";
       }

       strStatus = ""; 
        
       if (!patdemo.ValidatePatientDemographicRules(Master, cnFirstName, txtFirstName.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnLastName, txtLastName.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnAddress1, txtAddress1.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnAddress2, txtAddress2.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnCity, txtCity.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnPostalCode, txtPostCode.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnWorkPhone, txtWorkPhone.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnHomePhone, txtHomePhone.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnDateOfBirth, txtDateOfBirth.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       strStatus = "";
       if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmail, txtPatEmail.Text, out strStatus))
       {
           strErrMessage += strStatus;
       }

       return strErrMessage;
    }

    protected string CheckPatientEmergencyContactErrors()
    {
        string strErrMessage = "";
        string strStatus = "";

        CPatient patdemo = new CPatient();

        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyName, txtEmergencyName.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyCity, txtEmergencyCity.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyPostalCode, txtEmergencyPostCode.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyWorkPhone, txtEmergencyWPhone.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyHomePhone, txtEmergencyHPhone.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnPatEmergencyRelationship, cboEmergencyRelationship.SelectedItem.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        return strErrMessage;
    }

    //our save called from the master save
    protected bool Save()
    {
        lPatInfoRightMode = usrsec.GetRightMode(Master, (long)SUATUserRight.ProcessNewPatientsUR);
        bRecLocked = Master.IsPatientLocked;
        
        if ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked)//if has Rear/Write User Rights
        {
            m_bNewPatient = Master.GetVSBoolValue("IsNewPatient");

            if (tabContDemographics.ActiveTab == lnkPatient)
            {
                return SaveDemographics();
            }
            else if (tabContDemographics.ActiveTab == lnkContact)
            {
                if (SaveDemographics())
                {
                    // Save Emergency Contact Info
                    if (SavePatientEmergencyContact())
                    {
                        loadEmergencyContactInput();
                    }

                    loadPatient();
                    return true;
                }
                else
                {
                    tabContDemographics.ActiveTab = lnkPatient;
                    return false;
                }
            }
            else if (tabContDemographics.ActiveTab == lnkCPAPMachine)
            {
                if (SaveDemographics())
                {
                    // Save CPAP Device
                    ucPAPDevice.SaveDevice();

                    loadPatient();
                    return true;
                }
                else
                {
                    tabContDemographics.ActiveTab = lnkPatient;
                    return false;
                }
            }
            else if (tabContDemographics.ActiveTab == lnkPatientPortalAccount)
            {
                if (SaveDemographics())
                {
                    // Save Portal Account
                    if (ucPatientPortalAccount.SavePatientPortalAccount())
                    {
                        //ucPatientPortalAccount.loadPatientPortalAccount();
                        ucPatientPortalAccount.showSaveConfirmation();
                    }

                    loadPatient();
                    return true;
                }
                else
                {
                    tabContDemographics.ActiveTab = lnkPatient;
                    return false;
                }
            }
        }
        else
        {
            if (bRecLocked)
            {
                Master.StatusCode = 1;
                Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> <b>Read-Only Access</b>: The patient's record is in use by " + Session["PAT_LOCK_PROVIDER"].ToString() + ".";
            }
            else
            {
                Master.StatusCode = 1;
                Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
            }
        }

        return false;
    }

    // save ethnicity and race for the current patient
    protected bool SaveEthnicityRaceSource()
    {
        CPatEthnicityRace per = new CPatEthnicityRace();

        per.DeletePatientEthnicity(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        if (rblEthnicity.SelectedIndex >= 0)
        {
            per.InsertPatientEthnicity(
                Master,
                Master.SelectedPatientID,
                Convert.ToInt64(rblEthnicity.SelectedValue));

            if (Master.StatusCode != 0)
            {
                return false;
            } 
        }

        per.DeletePatientRace(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        foreach (ListItem li in cblRace.Items)
        {
            if (li.Selected)
            {
                per.InsertPatientRace(
                    Master,
                    Master.SelectedPatientID,
                    Convert.ToInt64(li.Value));

                if (Master.StatusCode != 0)
                {
                    return false;
                }
            }
        } 

        per.DeletePatEthRaceSource(
            Master,
            Master.SelectedPatientID);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        if (rblSource.SelectedIndex >= 0)
        {
            per.InsertPatEthRaceSource(
                Master,
                Master.SelectedPatientID,
                Convert.ToInt64(rblSource.SelectedValue));

            if (Master.StatusCode != 0)
            {
                return false;
            } 
        }

        return true;
    }
            
    //save the first view
    protected bool SaveDemographics()
    {
        Session["PAT_DEMOGRAPHICS_DS"] = null;
        Session["PATIENTNAME"] = null;

        long lHomePhoneMsg = 0;
        long lEmailMsg = 0;

        if (rblHomePhoneMsg.SelectedIndex > -1)
        {
            lHomePhoneMsg = Convert.ToInt32(rblHomePhoneMsg.SelectedValue);
        }

        if (rblEmailMessage.SelectedIndex > -1)
        {
            lEmailMsg = Convert.ToInt32(rblEmailMessage.SelectedValue);
        }

        //check data entry for errors
        string strMessage = CheckPatientDemoErrors();
        if (!string.IsNullOrEmpty(strMessage))
        {
            Master.StatusCode = 1;
            Master.StatusComment = strMessage;
            //ShowSysFeedback();
            return false;
        }

        ConvertPatientDemographics();

        CPatient pat = new CPatient();

        // if new patient then insert else update
        if (m_bNewPatient)
        {
            m_strNewPatientID = Master.APPMaster.GetNewPatientID();
            m_strNewEncounterID = Master.APPMaster.GetNewEncounterID();

            //insert the actual record
            bool bStatus = pat.InsertPatientDemographics(
                Master,
                m_strNewPatientID,
                m_strNewEncounterID,
                txtFirstName.Text,
                txtMiddleName.Text,
                txtLastName.Text,
                m_strFMPSSN,
                m_strFMPSSNConfirm,
                m_strGender,
                m_strDOB,
                cboProvider.SelectedValue,
                txtAddress1.Text,
                txtAddress2.Text,
                txtCity.Text,
                txtPostCode.Text,
                txtHomePhone.Text,
                txtCelPhone.Text,
                txtWorkPhone.Text,
                txtPatEmail.Text,
                cboState.SelectedValue,
                Convert.ToInt32(cboCallPreference.SelectedValue),
                lHomePhoneMsg,
                lEmailMsg);

            if (!bStatus)
            {
                //ShowSysFeedback();
                return false;
            }

            //reset these so the RFR tab will show
            Master.APPMaster.PatientHasOpenCase = true;

            Master.SetVSValue("IsNewPatient", false);
            Master.SetVSValue("ReloadPage", true);

            //set the selected patient id, basically they are
            //"looked up" at this point...
            Master.SelectedPatientID = m_strNewPatientID;

            //Add all patient events
            CPatientEvent evt = new CPatientEvent(Master);
            evt.AddAllEvents();

            //add patient step
            CPatientTxStep patstep = new CPatientTxStep(Master);
            patstep.InsertPatientStep(0);

            //get current (new) treatment id 
            CEncounter enc = new CEncounter();
            long lNewTreatmentID = 1;
            enc.GetCurrentTreatmentID(Master, m_strNewPatientID, out lNewTreatmentID);
            Master.SelectedTreatmentID = lNewTreatmentID;

            //assign initial questionnaires
            CIntake intake = new CIntake();
            intake.AssignInitialAssessments(Master, m_strNewPatientID);

            if (!SaveEthnicityRaceSource())
            {
                //ShowSysFeedback();
                return false;
            }

            return true;
        }
        else
        {
            bool bStatus = pat.UpdatePatientDemographics(
                Master,
                txtFirstName.Text,
                txtMiddleName.Text,
                txtLastName.Text,
                m_strFMPSSN,
                m_strFMPSSNConfirm,
                m_strGender,
                m_strDOB,
                cboProvider.SelectedValue,
                txtAddress1.Text,
                txtAddress2.Text,
                txtCity.Text,
                txtPostCode.Text,
                txtHomePhone.Text,
                txtCelPhone.Text,
                txtWorkPhone.Text,
                txtPatEmail.Text,
                cboState.SelectedValue,
                Convert.ToInt32(cboCallPreference.SelectedValue),
                lHomePhoneMsg,
                lEmailMsg);

            if (!bStatus)
            {
                return false;
            }

            if (!SaveEthnicityRaceSource())
            {
                return false;
            }
        }

        return true;
    }

    protected bool SavePatientEmergencyContact()
    {
        CPatient patEmergChk = new CPatient();
        DataSet patEmergContact = patEmergChk.GetPatientEmergencyContactDS(Master);

        if (patEmergContact.Tables[0].Rows.Count != 0)
        {
            CPatient UpdateEmergContact = new CPatient();
            return UpdateEmergContact.UpdatePatientEmergencyContact(
                Master,
                txtEmergencyName.Text,
                Convert.ToInt32(cboEmergencyRelationship.SelectedValue),
                txtEmergencyAddress1.Text,
                txtEmergencyCity.Text,
                txtEmergencyPostCode.Text,
                txtEmergencyHPhone.Text,
                txtEmergencyWPhone.Text,
                "", //Email ID,
                Convert.ToInt32(cboEmergencyState.SelectedValue));
        }
        else
        {
            CPatient NewEmergContact = new CPatient();
            return NewEmergContact.InsertPatientEmergencyContact(
                Master,
                txtEmergencyName.Text,
                Convert.ToInt32(cboEmergencyRelationship.SelectedValue),
                txtEmergencyAddress1.Text,
                txtEmergencyCity.Text,
                txtEmergencyPostCode.Text,
                txtEmergencyHPhone.Text,
                txtEmergencyWPhone.Text,
                "", //Email ID,
                Convert.ToInt32(cboEmergencyState.SelectedValue));
        }      
    }
       
    protected void getPatAddress(DataSet dsPatDemo)
    {
        //load hidden field with patient address data to be re-used in sponsor screen
        CDataUtils datautils = new CDataUtils();
        htxtPatAddress.Value = datautils.GetJSArray(dsPatDemo, "patient_id,address1,address2,city,postal_code,state_id,homephone,workphone");
    }

    protected void ShowSysFeedback() 
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(upDemographics, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }

    protected void CheckUserRightsMode()
    {
        lPatInfoRightMode = usrsec.GetRightMode(Master, (long)SUATUserRight.ProcessNewPatientsUR);

        if ((lPatInfoRightMode < (long)RightMode.ReadWrite) && !bRecLocked)
        {
            tabContDemographics.OnClientActiveTabChanged = null;
            tabContDemographics.AutoPostBack = false;
        }

        //PATIENT TAB
        txtFirstName.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtMiddleName.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtLastName.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtFMPSSN.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtFMPSSN_Confirm.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cboProvider.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtAddress1.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtAddress2.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtCity.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cboState.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtPostCode.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtDateOfBirth.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cboGender.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);

        // ethnicity/race
        rblEthnicity.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cblRace.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        rblSource.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);

        txtWorkPhone.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtHomePhone.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtPatEmail.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);

        //EMERGENCY CONTACT
        txtEmergencyName.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtEmergencyWPhone.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtEmergencyHPhone.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtEmergencyAddress1.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtEmergencyCity.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cboEmergencyState.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        txtEmergencyPostCode.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
        cboEmergencyRelationship.Enabled = ((lPatInfoRightMode > (long)RightMode.ReadOnly) && !bRecLocked);
    }
}