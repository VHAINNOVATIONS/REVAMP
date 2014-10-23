using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class pat_profile : System.Web.UI.Page
{
    public enum DEMOView : long
    {
        PATIENT = 0,
        CONTACT = 1,
        PORTAL_ACCOUNT = 2
    }

    #region Constants
    const string cnActiveDuty = "20";

    //Patient Demographics
    const int cnFirstName = 101;
    const int cnMiddleName = 102;
    const int cnLastName = 103;
    const int cnFMPSSN = 104;
    const int cnFMPSSNConfirm = 105;
    const int cnEDIPN = 106;
    const int cnAddress1 = 107;
    const int cnAddress2 = 108;
    const int cnCity = 109;
    const int cnPostalCode = 110;
    const int cnDateOfBirth = 111;
    const int cnWorkPhone = 112;
    const int cnHomePhone = 113;
    const int cnPatEmail = 114;

    //Patient Sponsor
    const int cnPatSponsorName = 201;
    const int cnPatSponsorWorkPhone = 202;
    const int cnPatSponsorHomePhone = 203;
    const int cnPatSponsorStreetAddress = 204;
    const int cnPatSponsorCity = 205;
    const int cnPatSponsorPostalCode = 206;
    const int cnPatSponsorRelationship = 207;

    //Patient Emergency Contact
    const int cnPatEmergencyName = 301;
    const int cnPatEmergencyWorkPhone = 302;
    const int cnPatEmergencyHomePhone = 303;
    const int cnPatEmergencyStreetAddress = 304;
    const int cnPatEmergencyCity = 305;
    const int cnPatEmergencyPostalCode = 306;
    const int cnPatEmergencyRelationship = 307;

    //Patient Military Details
    const int cnPatMilDetailsFMPSSN = 401;

    //FMPSSN SIZE
    const int cnFMPSSN_Size = 11;

    #endregion

    string m_strFMPSSN = "";
    string m_strFMPSSNConfirm = "";
    string m_strDOB = "-1";
    string m_strGender = "";

    public string strSaveBtnID;

    protected CSec usrsec = new CSec();

    protected void Page_Init(object sender, EventArgs e)
    {
        //get mastersave control
        Button btnMasterSave = (Button)Master.FindControl("btnMasterSave");
        AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
        trigger.ControlID = btnMasterSave.ID;
        upWrapperUpdatePanel.Triggers.Add(trigger);
    }

    protected void GetPatientID()
    {
        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();
        DataSet dsPat = pat.GetPatientIDRS(Master, Master.FXUserID);
        string strPatientID = utils.GetDSStringValue(dsPat, "PATIENT_ID");
        Master.SelectedPatientID = strPatientID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Master.SelectedPatientID == "")
        {
            GetPatientID();
        }
        
        //pass the master to the user control
        ucPatientPortalAccount.BaseMstr = Master;

        strSaveBtnID = Master.FindControl("btnMasterSave").ClientID;

        SetJSAttributes();

        //-----------------------------------------------------------------------
        //      NOT POSTBACK
        //-----------------------------------------------------------------------
        #region NotPostback
        if (!IsPostBack)
        {

            //Load combos
            loadAllCombos();
            LoadEthnicityRaceLists();

            if (Master.SelectedPatientID == "")
            {
                return;
            }
            else
            {
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
        if (Master.OnMasterSAVE())
        {
            if (Save())
            {
                CPatientTxStep patstep = new CPatientTxStep(Master);
                CPatientEvent patevt = new CPatientEvent(Master);

                if ((Master.PatientTxStep & (long)PatientStep.SavedProfile) == 0)
                {
                    patevt.CompletedEvent(1);
                }

                upWrapperUpdatePanel.Update();
                if(patstep.InsertPatientStep(1))
                {
                    Response.Redirect("portal_start.aspx");
                }
            }
        }

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

        return true;
    }

    protected void loadPatient()
    {
        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();

        if (Session["PAT_DEMOGRAPHICS_DS"] == null)
        {
            Session["PAT_DEMOGRAPHICS_DS"] = pat.GetPatientDemographicsDS(Master);
        }

        DataSet clientDemographics = (DataSet)Session["PAT_DEMOGRAPHICS_DS"];

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

            cboCallPreference.ClearSelection();

            foreach (DataTable patTable in clientDemographics.Tables)
            {
                foreach (DataRow patRow in patTable.Rows)
                {
                    //03/09/11 - CR did this patient complete a reason for referral
                    //for the most recent treatment.
                    Master.APPMaster.PatientHasReasonForReferral = false;
                    if (!patRow.IsNull("REFERRAL_COUNT"))
                    {
                        if (Convert.ToInt32(patRow["REFERRAL_COUNT"]) > 0)
                        {
                            Master.APPMaster.PatientHasReasonForReferral = true;
                        }
                    }

                    //referral_count
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
                        htxtProvider.Value = Convert.ToString(patRow["PROVIDER_ID"]);
                    }

                    if (!patRow.IsNull("PROVIDER_NAME"))
                    {
                        lblSleepSpecialist.Text = Convert.ToString(patRow["PROVIDER_NAME"]);
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

        LoadPatEthnicityRaceSource();
    }

    protected void loadEmergencyContactInput()
    {
        //demographic Emergency Contact State
        CDemographics emergState = new CDemographics();
        CDemographics emergRelationship = new CDemographics();
        //emergRelationship.LoadDemRelationshipDropDownList(Master,
        //                                    cboEmergencyRelationship,
        //                                    "");

        //emergState.LoadDemStateDropDownList(Master,
        //                                    cboEmergencyState,
        //                                    "");

        CDropDownList emergStateList = new CDropDownList();
        CDropDownList emergRelationshipList = new CDropDownList();

        CPatient pat = new CPatient();
        DataSet patEmergContact = new DataSet();

        patEmergContact = pat.GetPatientEmergencyContactDS(Master);

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
        m_strFMPSSN = "";
        m_strFMPSSNConfirm = "";

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

    protected long GetPatientTreatmentLookup()
    {
        long lvalue = 0;

        CPatient patTreatIDChk = new CPatient();
        DataSet patTreatmentIDChk = new DataSet();

        patTreatmentIDChk = patTreatIDChk.GetPatientTreatmentIdDS(Master);

        //load all available fields
        if (patTreatIDChk != null)
        {
            foreach (DataTable patTreatmentTable in patTreatmentIDChk.Tables)
            {
                foreach (DataRow patTreatmentRow in patTreatmentTable.Rows)
                {
                    if (!patTreatmentRow.IsNull("TREATMENT_ID"))
                    {
                        lvalue = Convert.ToInt64(patTreatmentRow["TREATMENT_ID"].ToString());
                    }
                }
            }
        }
        return lvalue;
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

        //if (cblRace.SelectedItem == null)
        //{
        //    strErrMessage += "Please select a Race.<br />";
        //}

        //if (cboGender.SelectedItem.Value == "-1")
        //{
        //    strErrMessage += "Please select a Gender.<br />";
        //}

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

        //strStatus = ""; 
        //if (!patdemo.ValidatePatientDemographicRules(Master, cnMiddleName, txtMiddleName.Text))
        //{
        //    strErrMessage += strStatus;
        //}

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
        if (!patdemo.ValidatePatientDemographicRules(Master, cnFMPSSN, txtFMPSSN.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!patdemo.ValidatePatientDemographicRules(Master, cnFMPSSNConfirm, txtFMPSSN_Confirm.Text, out strStatus))
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
        bool bSaved = false;

        if (tabContDemographics.ActiveTab == lnkPatient) //Patient Demographics
        {
            if (SaveDemographics())
            {
                loadPatient();
                Session["USERLOGGEDON"] = null;
            }
        }
        else 
        {
            if (SaveDemographics())
            {
                loadPatient();
                Session["USERLOGGEDON"] = null;

                if (tabContDemographics.ActiveTab == lnkContact) //Emergency Contact
                {
                    if (SavePatientEmergencyContact())
                    {
                        //loadEmergencyContactInput();
                    }
                }

                if (tabContDemographics.ActiveTab == lnkPatientPortalAccount) //Patient Portal Account
                {
                    if (ucPatientPortalAccount.SavePatientPortalAccount())
                    {
                        //ucPatientPortalAccount.loadPatientPortalAccount();
                        ucPatientPortalAccount.showSaveConfirmation();
                    }
                }
            }
        }

        bSaved = (Master.StatusCode <= 0);

        //Display error feedback message
        ShowSysFeedback();

        return bSaved;
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

        per.InsertPatEthRaceSource(
            Master,
            Master.SelectedPatientID,
            1);

        if (Master.StatusCode != 0)
        {
            return false;
        }

        return true;
    }

    //save the first view
    protected bool SaveDemographics()
    {
        Session["PAT_DEMOGRAPHICS_DS"] = null;
        Session["PATIENTNAME"] = null;

        // if not the demographics tab
        if (tabContDemographics.ActiveTab != lnkPatient
            && Convert.ToInt16(htxtCurrTab.Value) != (int)DEMOView.PATIENT)
        {
            return false;
        }

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
            ShowSysFeedback();
            return false;
        }

        ConvertPatientDemographics();

        CPatient pat = new CPatient();

        bool bStatus = pat.UpdatePatientDemographics(Master,
                                                    txtFirstName.Text,
                                                    txtMiddleName.Text,
                                                    txtLastName.Text,
                                                    m_strFMPSSN,
                                                    m_strFMPSSNConfirm,
                                                    m_strGender,
                                                    m_strDOB,
                                                    htxtProvider.Value,
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
            ShowSysFeedback();
            return false;
        }

        if (!SaveEthnicityRaceSource())
        {
            ShowSysFeedback();
            return false;
        }


        return true;
    }

    protected bool SavePatientEmergencyContact()
    {

        string strMessage = "";
        strMessage = CheckPatientEmergencyContactErrors();
        if (strMessage != "")
        {
            Master.StatusCode = 1;
            Master.StatusComment = strMessage;

            //Display error feedback message
            ShowSysFeedback();

            return false;
        }

        CPatient patEmergChk = new CPatient();
        DataSet patEmergContact = new DataSet();

        patEmergContact = patEmergChk.GetPatientEmergencyContactDS(Master);

        if (patEmergContact.Tables[0].Rows.Count != 0)
        {
            CPatient UpdateEmergContact = new CPatient();
            bool bUpdtEmergencyContact = UpdateEmergContact.UpdatePatientEmergencyContact(Master,
                                                            txtEmergencyName.Text,
                                                            Convert.ToInt32(cboEmergencyRelationship.SelectedValue),
                                                            txtEmergencyAddress1.Text,
                                                            txtEmergencyCity.Text,
                                                            txtEmergencyPostCode.Text,
                                                            txtEmergencyHPhone.Text,
                                                            txtEmergencyWPhone.Text,
                                                            "", //Email ID,
                                                            Convert.ToInt32(cboEmergencyState.SelectedValue)
                                                            );

            //Display error feedback message
            ShowSysFeedback();

            return bUpdtEmergencyContact;
        }
        else
        {
            CPatient NewEmergContact = new CPatient();

            bool bInsertEmergencyContact = NewEmergContact.InsertPatientEmergencyContact(Master,
                                                        txtEmergencyName.Text,
                                                        Convert.ToInt32(cboEmergencyRelationship.SelectedValue),
                                                        txtEmergencyAddress1.Text,
                                                        txtEmergencyCity.Text,
                                                        txtEmergencyPostCode.Text,
                                                        txtEmergencyHPhone.Text,
                                                        txtEmergencyWPhone.Text,
                                                        "", //Email ID,
                                                        Convert.ToInt32(cboEmergencyState.SelectedValue)
                                                        );

            //Display error feedback message
            ShowSysFeedback();

            return bInsertEmergencyContact;
        }
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

}