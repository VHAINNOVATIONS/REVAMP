using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
//using websuatreports.reports;
using DataAccess;
using Ext.Net;

public partial class MasterPage : BaseMaster
{
    public bool IsRecordOpen;
    public bool patConsents;
    public bool HasPatientTransfers = false;   
    public string strInitialVisitID;
    public string strMenuItems;
    public string strToolbarItems;
    public string strMilisecondsSessionExpire;
    public string strSessionTimeout;

    //common objects
    protected CUser usr = new CUser();
    protected CDataUtils utils = new CDataUtils();
    protected CTreatment treatment = new CTreatment();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsLoggedIn()) 
        {
            Response.Redirect("Default.aspx", true);
        }
        
        //set session time remaining
        strMilisecondsSessionExpire = SessionTimeRemaining();
        strSessionTimeout = SessionTimeRemaining();
       
        // register Ext.NET library icons
        ResourceManager1.RegisterIcon(Icon.Information);

        //pass the master to the popup
        ucPatLookup.BaseMstr = this;
        ucLogin.BaseMstr = this;
        ucVerticalMenu.BaseMstr = this;
        ucEncounterType.BaseMstr = this;
                
        if (!this.IsLoggedIn())
        {
            //put us back in login mode
            ucLogin.SetMode(1);

            this.ClosePatient();
        }

        //------------------------------------------------------------------------------
        // Get "Last updated" info
        string strLastUpdated = this.GetVSStringValue("LAST_UPDATED");
        if (!String.IsNullOrEmpty(strLastUpdated))
        {
            divLastModified.Visible = true;
            divLastModified.InnerText = strLastUpdated;
        }

        //remove PATIENTNAME session variable if
        //not logged in and patient_id is empty
        if (!this.IsLoggedIn() || String.IsNullOrEmpty(this.SelectedPatientID))
        {
            Session["PATIENTNAME"] = null;
        }

        //------------------------------------------------------------------------------
        // NOT POSTBACK
        //------------------------------------------------------------------------------
        #region not_postback
        if (!IsPostBack)
        {
            //get system settings
            DataSet dsSys = new DataSet();
           
            if(Session["SYSSETTINGS"] == null)
            {
                CSystemSettings sys = new CSystemSettings();
                Session["SYSSETTINGS"] = sys.GetSystemSettingsDS(this);
            }
            dsSys = (DataSet)Session["SYSSETTINGS"];

            //get site
            if(Session["SiteID"] == null)
            {
                CSystemSettings SysSettings = new CSystemSettings();
                DataSet dsSite = SysSettings.GetSiteDS(this);
                string strSiteID = utils.GetStringValueFromDS(dsSite, "SITE_ID");
                Session["SiteID"] = strSiteID;
            }
        }
        #endregion

        //------------------------------------------------------------------------------
        // IS POSTBACK
        //------------------------------------------------------------------------------
        #region isPostBack
        if (IsPostBack)
        {
            //get the postback control
            string strPostBackControl = Request.Params["__EVENTTARGET"];
            if (strPostBackControl != null)
            {

                #region PatientLookup
                //did we do a patient lookup?
                if (strPostBackControl.Equals("PATIENT_LOOKUP"))
                {

                    //Clears previously looked up patient id, treatment id
                    this.SelectedPatientID = "";
                    this.SelectedTreatmentID = -1;
                    this.SelectedEncounterID = "";
                    this.SelectedProblemID = -1;

                    this.ClosePatient();


                    //get the patient id
                    string[] strArg = Request.Form["__EVENTARGUMENT"].Split('|');

                    //pass the patient id to the base, this will cache
                    //it in the db fx_session_value table
                    this.SelectedPatientID = strArg[0];

                    //check if it is an event lookup
                    if (strArg.Length > 1) 
                    {
                        if (strArg[1].ToLower().Equals("event")) 
                        {
                            Session["EVENT_LOOKUP"] = true;
                        }
                    }

                    //set the current treatment id, gets list of records with newest first
                    DataSet t_recs = treatment.GetRecordList(this,
                                                              this.SelectedPatientID,
                                                              2); //OPEN CASES - Revamp only uses this

                    if (t_recs != null && t_recs.Tables[0].Rows.Count > 0)
                    {
                        this.SelectedTreatmentID = Convert.ToInt32(t_recs.Tables[0].Rows[0]["treatment_id"].ToString());
                    }

                    //------------------------------------------------------------------------------
                    //GET INITIAL VISIT ID
                    //GET PATIENT NAME FOR THE DEMOGRAPHICS BLURB
                    if (this.IsLoggedIn() && !String.IsNullOrEmpty(this.SelectedPatientID))//must be logged in too... 
                    {
                        if (Session["InitialVisit"] == null)
                        {
                            CEncounter patInitVisit = new CEncounter();
                            CDataUtils dUtils = new CDataUtils();
                            DataSet dsInitVisit = patInitVisit.GetInitialVisitDS(this, this.SelectedPatientID, this.SelectedTreatmentID);
                            Session["InitialVisit"] = dUtils.GetStringValueFromDS(dsInitVisit, "encounter_id");
                        }
                        
                        if (Session["PATIENTNAME"] == null)
                        {
                            CPatient cpat = new CPatient();
                            Session["PATIENTNAME"] = cpat.GetPatientName(this);
                        }

                        //GET SELECTED PATIENT'S DEMOGRAPHICS
                        CPatient pat = new CPatient();
                        CDataUtils utils = new CDataUtils();
                        DataSet clientDemographics = new DataSet();

                        Session["PAT_DEMOGRAPHICS_DS"] = pat.GetPatientDemographicsDS(this);
                        clientDemographics = (DataSet)Session["PAT_DEMOGRAPHICS_DS"];

                        foreach (DataTable patTable in clientDemographics.Tables)
                        {
                            foreach (DataRow patRow in patTable.Rows)
                            {
                                this.APPMaster.PatientHasOpenCase = false;
                                if (!patRow.IsNull("OPENCASE_COUNT"))
                                {
                                    if (Convert.ToInt32(patRow["OPENCASE_COUNT"]) > 0)
                                    {
                                        this.APPMaster.PatientHasOpenCase = true;
                                    }
                                }
                            }
                        }
                    }
                    //--------------------------------------------------------------------------------------

                    //VERIFY STATUS OF THE PATIENT RECORD
                    CPatientLock plock = new CPatientLock(this);

                    string strLockProviderName = String.Empty;
                    string strLockProviderEmail = String.Empty;
                    
                    this.IsPatientLocked = plock.IsPatientLocked(this.SelectedPatientID, out strLockProviderName, out strLockProviderEmail);
                    Session["PAT_LOCK_PROVIDER"] = strLockProviderName;
                    Session["PAT_LOCK_EMAIL"] = strLockProviderEmail;

                    //REDIRECT USER ------------------------------------------------------------------------
                    long lSoapNoteUR = (long)SUATUserRight.NoteSubjectiveUR
                        + (long)SUATUserRight.NoteObjectiveUR
                        + (long)SUATUserRight.NoteAssessmentUR
                        + (long)SUATUserRight.NotePlanUR;


                    if (this.APPMaster.HasUserRight(lSoapNoteUR))
                    {
                        Response.Redirect("pat_summary.aspx", true);
                    }
                    else if (this.APPMaster.HasUserRight((long)SUATUserRight.ProcessNewPatientsUR))
                    {
                        Response.Redirect("pat_demographics.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("revamp.aspx", true);
                    }
                }
                #endregion

                #region OtherLookups
                //did we do a user lookup?
                if (strPostBackControl.Equals("USER_LOOKUP"))
                {
                    //get the uidpwd
                    string strArg = Request.Form["__EVENTARGUMENT"];
                    //pass the patient id to the base, this will cache
                    //it in the db fx_session_value table
                    this.SelectedProviderID = strArg;

                    Response.Redirect("user_admin.aspx", true);
                }

                //did we do a Portal Patient Lookup?
                if (strPostBackControl.Equals("PORTAL_PATIENT_LOOKUP"))
                {
                
                    //get the uidpwd
                    string strArg = Request.Form["__EVENTARGUMENT"];
                    //pass the patient id to the base, this will cache
                    //it in the db fx_session_value table
                    this.SelectedPatientID = strArg;

                    Response.Redirect("pat_portal_account.aspx", true);
                }

                //-- 2/22/2011 close currently looked up patient
                if (strPostBackControl.Equals("CLOSE_PATIENT"))
                {
                    this.SelectedPatientID = "";
                    this.SelectedTreatmentID = -1;
                    this.LookupSearchCase = -1;

                    Response.Redirect("revamp.aspx", true);
                }
                #endregion
            }
        }
        #endregion

        #region ShowUsernameAndPatientDemographics
        if (this.IsLoggedIn())
        {
            //Name Of User Currently logged on.
            string strUserLoggedOn = String.Empty;
            if (Session["USERLOGGEDON"] == null)
            {
                strUserLoggedOn += "<img alt=\"Account Activity\" src=\"Images/information.png\" style=\"cursor: pointer; vertical-align: middle; margin-right: 3px;\" onclick=\"showAccDetails();\" />";
                strUserLoggedOn += UserLoggedOn();
                strUserLoggedOn += " - ";
                strUserLoggedOn += DateTime.Now.ToShortDateString();
                strUserLoggedOn += " ";
                strUserLoggedOn += DateTime.Now.ToShortTimeString();
                lblUserLoggedOn.Text = strUserLoggedOn;
                Session["USERLOGGEDON"] = strUserLoggedOn;
            }
            else
            {
                lblUserLoggedOn.Text = Session["USERLOGGEDON"].ToString();
            }

            //draw the patient info bar at the top
            patDemoInfoBar.InnerHtml = "";

            //GET PATIENT NAME for the demographics blurb
            //Render Left Vertical Menu for selected patient
            if (this.IsLoggedIn() && !String.IsNullOrEmpty(this.SelectedPatientID))//must be logged in too... 
            {
                if (Session["PATIENTNAME"] == null)
                {
                    CPatient cpat = new CPatient();
                    Session["PATIENTNAME"] = cpat.GetPatientName(this);
                }
                string[] strPatInfo = (string[])Session["PATIENTNAME"];

                patDemoInfoBar.InnerHtml = strPatInfo[0];
                                                
                //render vertical menu
                ucVerticalMenu.RenderVerticalMenu();
            }
            else
            {
                this.ClosePatient();
            }
        }
        #endregion

        //load the patient treatment/encounter info
        //if we are in group note, hide it...

        bool bSkipSelectedPatientCheck = false;
        string strPage = "";
        strPage = this.GetPageName().ToLower();
        if (strPage.IndexOf("pat_portal_account.aspx") > -1)
        {
            pnlTxTree.Visible = false;
            bSkipSelectedPatientCheck = true;
        }
        else if (strPage.IndexOf("cms_menu_edit.aspx") > -1)
        {
            pnlTxTree.Visible = false;
            bSkipSelectedPatientCheck = true;

            if (IsPostBack) {
                if (this.OnMasterSAVE()) {
                    BuildMenu();
                }
            }
        }
        else if (strPage.IndexOf("cms_page_edit.aspx") > -1)
        {
            pnlTxTree.Visible = false;
            bSkipSelectedPatientCheck = true;
        }
        else
        {
            pnlTxTree.Visible = true;
            patDemoInfoBar.Visible = true;
            bSkipSelectedPatientCheck = false;
        }

        // ----------------
        if (!bSkipSelectedPatientCheck)
        {
            if (!String.IsNullOrEmpty(this.SelectedPatientID))
            {
                pnlTxTree.Visible = true;
                pnlDemoInfoBar.Visible = true;
            }
            else
            {
                pnlTxTree.Visible = false;
                pnlDemoInfoBar.Visible = false;
            } 
        }

        if (!this.IsLoggedIn())
        {
            pnlLogoff.Visible = false;
        }
        else
        {
            pnlLogoff.Visible = true;
        }

        //get account activity details
        GetAccountDetails();

        // Build the Menu & Toolbar HTML string
        BuildMenu();

        //check if user has new messages
        btnEmailNew.Attributes.CssStyle.Add("vertical-align", "middle");
        btnEmailNew.Attributes.CssStyle.Add("margin-right", "8px");
        btnEmailNew.Attributes.CssStyle.Add("cursor", "pointer");
        btnEmailNew.Visible = this.HasNewMessage();
        btnEmailNew.Attributes.Add("onclick","winrpt.showReport('messagescenter',['null'],{maximizable:false, width:($(window).width() - 50), height:($(window).height() - 50)});");

    }

    //build menus and toolbar
    protected void BuildMenu() {
        CAppMenu menu = new CAppMenu(this);
        strMenuItems = menu.RenderMenuHTML();
        strToolbarItems = menu.RenderToolbarHTML();
    }

    //does the patient consent to treatment?
    public bool PatientConsents()
    {
        if (Session["PATIENT_CONSENTS"] != null)
        {
            if (Session["PATIENT_CONSENTS"].ToString() == "1")
            {
                return true;
            }
        }
        return false;
    }
    
    //checks if encounter has counselor and patient assessments
    protected bool HasAssessments(DataSet ds, string strEncounterID)
    {
        int iCount = 0;
        DataRow[] arrDR = ds.Tables[0].Select("encounter_id = '" + strEncounterID + "'");
        if (arrDR.GetLength(0) > 0)
        {
            foreach (DataRow dr in arrDR)
            {
                if (Convert.ToInt32(dr["INTAKE_TYPE"]) == (long)IntakeType.PATIENT)
                {
                    ++iCount;
                }
                else if (Convert.ToInt32(dr["INTAKE_TYPE"]) == (long)IntakeType.COUNSELOR)
                {
                    ++iCount;
                }
            }
        }
        if (iCount > 0)
        {
            return true;
        }
        return false;
    }

    protected string UserLoggedOn()
    {
        string strUserName = "";
        CDataUtils utils = new CDataUtils();


        if (this.APPMaster.UserType == (long)(SUATUserType.PATIENT))
        {
            CPatient pat = new CPatient();

            //attempt to grab the user's profile
            strUserName = pat.GetPatientUserName(this);
        }
        else
        {
            CUserAdmin RevampUser = new CUserAdmin();
            DataSet dsRevampUser = new DataSet();
            //attempt to grab the user's profile
            dsRevampUser = RevampUser.GetSuatUserNameDS(this);

            //load SUAT User Name Name Field
            if (dsRevampUser != null)
            {
                foreach (DataTable table0 in dsRevampUser.Tables)
                {
                    foreach (DataRow row in table0.Rows)
                    {
                        if (!row.IsNull("NAME"))
                        {
                            strUserName = row["NAME"].ToString();
                        }

                        if (!row.IsNull("GRAPH_PREF"))
                        {
                            this.GraphicOption = Convert.ToInt32(row["GRAPH_PREF"]);
                        }

                        if (!row.IsNull("DIMS_ID"))
                        {
                            this.APPMaster.UserDMISID = row["DIMS_ID"].ToString();
                        }
                    }
                }
            }
        }
        return strUserName;
    }

    protected void btnKeepAlive_OnClick(object sender, EventArgs e) 
    {
        string strValue = "";
        if (this.GetSessionValue("FX_USER_ID", out strValue))
        {
            Session["SESSION_INITIATED"] = DateTime.Now;
            
            //refresh patient's record lock
            if (!String.IsNullOrEmpty(this.SelectedPatientID))
            {
                CPatientLock plock = new CPatientLock(this);
                plock.RefreshPatientLock(this.SelectedPatientID); 
            }
        }
        else
        {
            this.LogOff();
        }
    }

    protected void btnFeedbackOK_OnClick(object sender, EventArgs e) 
    {
        winSysFeedback.Hide();
    }

    protected string SessionTimeRemaining() 
    {
        long lSessionTimeout = (Session.Timeout - 1) * 60 * 1000;
        return lSessionTimeout.ToString();
    }

    protected void GetAccountDetails() 
    {
        #region account_details
        if (this.IsLoggedIn() && Session["ACC_DETAILS"] == null)
        {
            CUser user = new CUser();
            DataSet dsUser = user.GetLoginUserDS(this, this.FXUserID);
            if (dsUser != null)
            {
                CDataUtils utils = new CDataUtils();
                string strAccDetails = String.Empty;

                DateTime dtLastLogin = utils.GetDSDateTimeValue(dsUser, "date_last_login");
                string strLastLogin = utils.GetDateTimeAsString(dtLastLogin);
                string strLastLoginIP = utils.GetDSStringValue(dsUser, "last_login_ip");

                DateTime dtFLastLogin = utils.GetDSDateTimeValue(dsUser, "last_flogin_date");
                string strFLastLogin = utils.GetDateTimeAsString(dtFLastLogin);
                string strFLastLoginIP = utils.GetDSStringValue(dsUser, "last_flogin_ip");

                long lFAttempts = utils.GetDSLongValue(dsUser, "flogin_attempts");
                string strFAttempts = Convert.ToString(lFAttempts);

                strAccDetails += "Unsuccessful Logon Attempts Since Last Successful Logon: " + strFAttempts;
                strAccDetails += "<br>";
                if (!String.IsNullOrEmpty(strFLastLoginIP))
                {
                    strAccDetails += "Last Unsuccessful Logon:";
                    strAccDetails += " " + strFLastLogin;
                    strAccDetails += " - IP Address: " + strFLastLoginIP;
                }
                else
                {
                    strAccDetails += "No Unsuccessful Logons";
                }

                strAccDetails += "<br>";
                strAccDetails += "Last Successful Logon:";
                strAccDetails += " " + strLastLogin;
                strAccDetails += " - IP Address: " + strLastLoginIP;
                                
                Session["ACC_DETAILS"] = strAccDetails;
            }

            if (this.APPMaster.PasswordExpires > 0 && this.APPMaster.PasswordExpires <= 10)
            {
                this.StatusCode = 1;
                this.StatusComment = "Your account password will expire in " + Convert.ToString(this.APPMaster.PasswordExpires) + " days!";
            }
        }
        #endregion
    }

    protected bool HasNewMessage() {
        bool result = false;
        CMessages msg = new CMessages(this);
        DataSet dsNewMsg = msg.GetUnreadMessagesDS();
        if (dsNewMsg != null) {
            result = (dsNewMsg.Tables[0].Rows.Count > 0);
        }
        return result;
    }
}