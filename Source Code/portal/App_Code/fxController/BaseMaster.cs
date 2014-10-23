using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using DataAccess;
//
//reference: http://msdn.microsoft.com/en-us/library/c8y19k6h.aspx
//
//This is our base master page, all other master pages will derive from this
//master page. This page will handle db connection and cleanup etc...
//
//By defining the BaseMasterPage class as abstract, it can't be 
//created directly and can only serve as the base for another class.
//
//sequence of events for reference...
//Master page controls Init event
//Content controls Init event
//
//Master page Init event
//Content page Init event
//
//Content page Load event
//Master page Load event
//
//Content page PreRender event
//Master page PreRender event
//
//Master page controls PreRender event
//Content controls PreRender event
//
public abstract class BaseMaster : System.Web.UI.MasterPage
{

    //04/16/2012 - Security Updates///////////////////////////

    /// <summary>
    /// ASP .NET session id
    /// </summary>
    public string ASPSessionID
    {
        get
        {
            return Context.Session.SessionID;
        }
    }

    /// <summary>
    /// time out
    /// </summary>
    int m_nTimeOut = 15;
    public int Timeout
    {
        set
        {
            m_nTimeOut = value;
        }
        get
        {
            return m_nTimeOut;
        }
    }

    /// <summary>
    /// get/set session
    /// </summary>
    //DB session id  member, this gets set when the user logs in
    public string DBSessionID
    {
        get
        {
            string strDBSessionID = "";
            if (Session["DBSessionID"] == null)
            {
                return strDBSessionID;
            }

            strDBSessionID = Session["DBSessionID"].ToString();
            return strDBSessionID;
        }
        set
        {
            Session["DBSessionID"] = value;
        }
    }
   
    //////////////////////////////////////////////////////////





    //data connection member
    private CDataConnection m_DBConnection;

    //app specific controller not part of framework...
    public AppMaster APPMaster;
    
    /// <summary>
    /// get the database connection
    /// </summary>
    public CDataConnection DBConn
    {
        get { return m_DBConnection; }
    }

    public string Key { get; private set; }

    /// <summary>
    /// get/set status comment info, it will append... this allows us to keep
    /// a running list of errors if needed
    /// </summary>
    private string m_strStatusComment;
    public string StatusComment
    {
        get { return m_strStatusComment; }
        set {
                if (m_strStatusComment != "")
                {
                    string strEnding = "";
                    if (m_strStatusComment.Length >= 6)
                    {
                        strEnding = m_strStatusComment.Substring(m_strStatusComment.Length - 6);
                    }
                    if (strEnding != "<br />")
                    {
                        Session["StatusComment"] += "<br />";
                        m_strStatusComment += "<br />";
                    }
                }
               
                Session["StatusComment"] += value;
                m_strStatusComment += value;

                //if the status contains an oracle error
                //then show a more user-friendly error instead
                //example: ORA-00942: table or view does not exist
                //search for "ORA-" 
                if (m_strStatusComment.ToUpper().IndexOf("ORA-") != -1)
                {
                    //todo: logg the current status in an error table

                    m_strStatusComment = "An error occured while processing, please contact your system administrator.";
                    m_strStatusComment += "<br />";
                    Session["StatusComment"] = m_strStatusComment;
                }
               
            }
    }

    /// <summary>
    /// clear the status, called by the masterpage after we display status info
    /// </summary>
    public void ClearStatus()
    {
        Session["StatusComment"] = "";
        Session["StatusCode"] = 0;

        m_strStatusComment = "";
        m_lStatusCode = 0;

    }

    /// <summary>
    /// get/set status code info
    /// </summary>
    private long m_lStatusCode;
    public long StatusCode
    {
        get { return m_lStatusCode; }
        set {
                //do not clear it if its already set to 1, this way
                //errors will overrule success for display
                if (m_lStatusCode < 1) //Changed because we have error status codes that are greater than 1.
                {
                    Session["StatusCode"] = value;
                    m_lStatusCode = value;
                }
            }
    }

    //use this check to see if the user clicked the 
    //apps main "Save" button...
    public bool OnMasterSAVE()
    {
        //get the postback control
        string strPostBackControl = Request.Params["__EVENTTARGET"];
        if (strPostBackControl != null)
        {
            //did we do a patient lookup?
            if (strPostBackControl.IndexOf("btnMasterSave") > -1)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// get/set user id
    /// </summary>
    //user id member - set when we login
    private long m_lFXUserID;
    public long FXUserID
    {
        get {
                return m_lFXUserID; 
            }
        set { m_lFXUserID = value; }
    }

    //are we still logged in...
    public bool IsLoggedIn()
    {
        if (m_lFXUserID < 1)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// get client ip, may be router but better than nothing
    /// </summary>
    public string ClientIP
    {
        get { return Context.Request.ServerVariables["REMOTE_ADDR"]; }
    }

    //sets a string viewstate value
    public void SetVSValue(string strKey, string strValue)
    {
        ViewState[strKey] = strValue;
    }

    //sets a bool viewstate value
    public void SetVSValue(string strKey, bool bValue)
    {
        ViewState[strKey] = bValue;
    }

    //sets a long viewstate value
    public void SetVSValue(string strKey, long lValue)
    {
        ViewState[strKey] = Convert.ToString(lValue);
    }

    //gets a string value from viewstate
    public string GetVSStringValue(string strKey)
    {
        string strValue = "";
        if (ViewState[strKey] != null)
        {
            strValue = Convert.ToString(ViewState[strKey]);
        }

        return strValue;
    }

    //gets a bool value from view state
    public bool GetVSBoolValue(string strKey)
    {
        bool bValue = false;
        if (ViewState[strKey] != null)
        {
            bValue = Convert.ToBoolean(ViewState[strKey]);
        }

        return bValue;
    }

    //gets a long value from viewstate
    public long GetVSLongValue(string strKey)
    {
        long lValue = -1;
        if (ViewState[strKey] != null)
        {
            lValue = Convert.ToInt32(ViewState[strKey]);
        }

        return lValue;
    }

    //closes the patient
    public void ClosePatient()
    {
        this.SelectedPatientID = "";
        this.SelectedTreatmentID = -1;
        this.LookupSearchCase = -1;
        this.SelectedEncounterID = "";
        this.SelectedProblemID = -1;
        this.PatientTxStep = 0;
        this.NotificationTxStep = 0;
        this.PatientGender = "";
        this.PatientAge = 0;
        this.PatientDOB = DateTime.Now;
        this.PatientHeight = 0;
        this.PatientWeight = 0;
        
        //remove session variables associated to the patient
        Session["PATIENTNAME"] = null;
		Session["PAT_DEMOGRAPHICS_DS"] = null;
        Session["PATIENT_STEP"] = null;
        Session["NOTIFICATION_STEP"] = null;
    }

    //this is the currently looked up patient id...
    public string SelectedPatientID
    {
        get
        {
            CSec sec = new CSec();
            string strValue = "";
            //more efficient to just use a session var
            //no db hit this way
            //GetSessionValue("SELECTED_PATIENT_ID", out strValue);
            if(Session["SELECTED_PATIENT_ID"] != null)
            {
                strValue = Session["SELECTED_PATIENT_ID"].ToString();
            }
            return sec.dec(strValue, "");
        }
        //set { SetSessionValue("SELECTED_PATIENT_ID", Convert.ToString(value)); }
        set 
        {
            CSec sec = new CSec();
            Session["SELECTED_PATIENT_ID"] =  sec.Enc(Convert.ToString(value), ""); 
        }
    }

    public bool IsPatientLocked
    {
        get
        {
            bool bLocked = false;
            if (Session["PATIENT_LOCKED_BM"] != null)
            {
                bLocked = Convert.ToBoolean(Session["PATIENT_LOCKED_BM"]);
            }
            return bLocked;
        }
        set
        {
            Session["PATIENT_LOCKED_BM"] = value;
        }
    }

    // 03/11/2011 - Selected treatment id
    public long SelectedTreatmentID
    {
        get
        {
            string strValue = "-1";
            //GetSessionValue("SELECTED_TREATMENT_ID", out strValue);
            if (Session["SELECTED_TREATMENT_ID"] != null)
            {
                strValue = Session["SELECTED_TREATMENT_ID"].ToString();
            }

            if (strValue.Length > 0)
            {
                return Convert.ToInt32(strValue);
            }
            else
            {
                return -1;
            }
        }
        
        // set { SetSessionValue("SELECTED_TREATMENT_ID", Convert.ToString(value)); }
        set { Session["SELECTED_TREATMENT_ID"] = Convert.ToString(value); }
    }

    // 2012-12-23 09:02PM **************************************************
    public long PatientHeight
    {
        get
        {
            long value = 0;
            //more efficient to just use a session var
            //no db hit this way
            //
            if (Session["PATIENT_HEIGHT"] != null)
            {
                value = Convert.ToInt32(Session["PATIENT_HEIGHT"]);
            }

            return value;
        }
        set { Session["PATIENT_HEIGHT"] = value; }
    }
    
    public DateTime PatientDOB
    {
        get
        {
            DateTime dtDOB = DateTime.Now;
            //more efficient to just use a session var
            //no db hit this way
            //
            if (Session["PATIENT_DOB"] != null)
            {
                dtDOB = Convert.ToDateTime(Session["PATIENT_DOB"]);
            }

            return dtDOB;
        }
        set { Session["PATIENT_DOB"] = value; }
    }

    public long PatientAge
    {
        get
        {
            long value = 0;
            //more efficient to just use a session var
            //no db hit this way
            //
            if (Session["PATIENT_AGE"] != null)
            {
                value = Convert.ToInt32(Session["PATIENT_AGE"]);
            }

            return value;
        }
        set { Session["PATIENT_AGE"] = value; }
    }

    public string PatientGender
    {
        get
        {
            string value = "";
            //more efficient to just use a session var
            //no db hit this way
            //
            if (Session["PATIENT_GENDER"] != null)
            {
                value = Session["PATIENT_GENDER"].ToString();
            }

            return value;
        }
        set { Session["PATIENT_GENDER"] = value; }
    }

    public long PatientWeight
    {
        get
        {
            long value = 0;
            //more efficient to just use a session var
            //no db hit this way
            //
            if (Session["PATIENT_WEIGHT"] != null)
            {
                value = Convert.ToInt32(Session["PATIENT_WEIGHT"]);
            }

            return value;
        }
        set { Session["PATIENT_WEIGHT"] = value; }
    }

    // *********************************************************************

    //this is the currently looked up patient id...
    public string SelectedEncounterID
    {
        get
        {   string strValue = "";
            //more efficient to just use a session var
            //no db hit this way
            //GetSessionValue("SELECTED_ENCOUNTER_ID", out strValue);
            //
            if(Session["SELECTED_ENCOUNTER_ID"] != null)
            {
                strValue = Session["SELECTED_ENCOUNTER_ID"].ToString();
            }
            
            return strValue;
        }
        //set { SetSessionValue("SELECTED_ENCOUNTER_ID", Convert.ToString(value)); }
        set { Session["SELECTED_ENCOUNTER_ID"] =  Convert.ToString(value); }
    }

    //this is the currently selected level of care
    public string SelectedLevelofCareID
    {
        get
        {
            string strValue = "";
            if (Session["SELECTED_LEVELOFCARE_ID"] != null)
            {
                strValue = Session["SELECTED_LEVELOFCARE_ID"].ToString();
            }

            return strValue;
        }
        set { Session["SELECTED_LEVELOFCARE_ID"] = Convert.ToString(value); }
    }
    
    //this is the currently looked up provider id...
    public string SelectedProviderID
    {
        get
        {
            string strValue = "";
            //more efficient to just use a session var
            //no db hit this way
            //GetSessionValue("SELECTED_PROVIDER_ID", out strValue);
            if(Session["SELECTED_PROVIDER_ID"] != null)
            {
                strValue = Session["SELECTED_PROVIDER_ID"].ToString();
            }
            return strValue;
        }
        //set { SetSessionValue("SELECTED_PROVIDER_ID", Convert.ToString(value)); }
        set { Session["SELECTED_PROVIDER_ID"] =  Convert.ToString(value); }
   }

    //Patient Treatment Step
    public long PatientTxStep
    {
        get
        {
            long value = 0;
            //more efficient to just use a session var
            //no db hit this way
            if (Session["PATIENT_STEP"] != null)
            {
                value = Convert.ToInt32(Session["PATIENT_STEP"]);
            }

            return value;
        }
        set { Session["PATIENT_STEP"] = value; }
    }

    //Notification Treatment Step
    public long NotificationTxStep
    {
        get
        {
            long value = 0;
            //more efficient to just use a session var
            //no db hit this way
            if (Session["NOTIFICATION_STEP"] != null)
            {
                value = Convert.ToInt32(Session["NOTIFICATION_STEP"]);
            }

            return value;
        }
        set { Session["NOTIFICATION_STEP"] = value; }
    }

    /*
     *****************************************************************
     *  MODULE ID SUPPORT FOR PATIENT PORTAL
     * ***************************************************************
     */

    /// get/set module id
    private int m_lModuleID;
    public int ModuleID
    {
        get
        {
            return m_lModuleID;
        }
        set
        {
            m_lModuleID = value;
        }
    }

    /// get/set page id
    private int m_lPageID;
    public int PageID
    {
        get { return m_lPageID; }
        set { m_lPageID = value; }
    }

    /// get/set random data segment
    private string m_strRDS;
    public string RDS
    {
        get { return m_strRDS; }
        set { m_strRDS = value; }
    }

    //------------------------------------------------------------------------------
    private int m_nSessionLanguage;
    public int SessionLanguage
    {
        get { return m_nSessionLanguage; }
        set { Session["VS_ALTLANG"] = value; m_nSessionLanguage = value; }
    }

    //------------------------------------------------------------------------------
    public string EncounterID
    {
        get
        {
            string strValue = "";
            GetSessionValue("ENCOUNTER_ID", out strValue);
            return strValue;
        }
        set
        {
            SetSessionValue("ENCOUNTER_ID", Convert.ToString(value));
        }

    }

    // 03/11/2011 - this is the type of lookup:: 1- all cases, 2- open cases, 3- closed cases
    public long LookupSearchCase
    {
        get
        {
            string strValue = "-1";
            GetSessionValue("LOOKUP_SEARCH_CASE", out strValue);
            if (strValue.Length > 0)
            {
                return Convert.ToInt32(strValue);
            }
            else
            {
                return -1;
            }
        }
        set { SetSessionValue("LOOKUP_SEARCH_CASE", Convert.ToString(value)); }
    }

    // 11/17/2011 - Selected problem id
    public long SelectedProblemID
    {
        get
        {
            string strValue = "-1";
            //GetSessionValue("SELECTED_PROBLEM_ID", out strValue);
            if (Session["SELECTED_PROBLEM_ID"] != null)
            {
                strValue = Session["SELECTED_PROBLEM_ID"].ToString();
            }

            if (strValue.Length > 0)
            {
                return Convert.ToInt32(strValue);
            }
            else
            {
                return -1;
            }
        }

        // set { SetSessionValue("SELECTED_PROBLEM_ID", Convert.ToString(value)); }
        set { Session["SELECTED_PROBLEM_ID"] = Convert.ToString(value); }
    }
    
    //this is the currently selected Branch Of Service
    public long BranchOfService
    {
        get
        {
            string strValue = "-1";
            GetSessionValue("BRANCH_OF_SERVICE", out strValue);
            if (strValue.Length > 0)
            {
                return Convert.ToInt32(strValue);
            }
            else
            {
                return -1;
            }
        }
        set { SetSessionValue("BRANCH_OF_SERVICE", Convert.ToString(value)); }
    }

    public bool HasMilitaryDetails
    {
        get
        {
            bool value = false;
            value = Convert.ToBoolean(Session["HAS_MILITARY_DETAILS"]);
            return value;
        }
        set
        {
            Session["HAS_MILITARY_DETAILS"] = value;
        }
    }

    public bool HasPatientSupervisorInput
    {
        get
        {
            bool value = false;
            value = Convert.ToBoolean(Session["HAS_PATIENT_SUPERVISOR_INPUT"]);
            return value;
        }
        set
        {
            Session["HAS_PATIENT_SUPERVISOR_INPUT"] = value;
        }
    }

    public bool HasPatientInsurance
    {
        get
        {
            bool value = false;
            value = Convert.ToBoolean(Session["HAS_PATIENT_INSURANCE"]);
            return value;
        }
        set
        {
            Session["HAS_PATIENT_INSURANCE"] = value;
        }
    }

    //this puts us into a developer mode so 
    //that CAC stuff is bypassed
    //etc....
    public bool DEV_MODE = false;

    /// <summary>
    /// constructor
    /// </summary>
    public BaseMaster()
    {
        //this puts us into a developer mode so 
        //that CAC stuff is bypassed
        //etc....

        //TODO: comment out before depoloying!!!
        DEV_MODE = true;

        //create a new dataconnection object
        m_DBConnection = new CDataConnection();

        //clear status
        m_strStatusComment = "";
        m_lStatusCode = -1;
        FXUserID = 0;

        m_lModuleID = 0;
        m_lPageID = 0;
        m_strRDS = "";
    }

    /// <summary>
    /// this is the proper place to do initialization in a master page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        //app specific stuff outside the base controller
        APPMaster = new AppMaster();
        APPMaster.SetBaseMaster(this);

        //Returns a string that can be used in a client 
        //event to cause postback to the server. 
        Page.ClientScript.GetPostBackEventReference(this, String.Empty);

        //set the character set, since all pages derive from basemaster
        //this will set the encoding for all pages...
        Response.ContentEncoding = Encoding.UTF8;

        //init status info and objects
        m_strStatusComment = "";
        m_lStatusCode = -1;//-1 = success no show

        //04/16/2012 - Security Updates
        //set the timeout
        if (Session.Timeout < 15)
        {
            Timeout = 15;
        }
        else
        {
            Timeout = Session.Timeout;
        }
        

        //connect to the data source
        if (!ConnectToDataSource())
        {
            //redirect to an error page
            Response.Redirect("error_database.aspx");
            Response.End();
        }

        //sec helper
        CSec sec = new CSec();

        //auto-login with CAC/cert NO!
        //from the inspection user must click banner
        //so no auto login here
        /*if (!IsPostBack)
        {
            string strPage = GetPageName();
            if (strPage != "fx_logoff.aspx")
            {
                //don't try to login if we clicked the logoff option
                if (Request.QueryString["logoff"] == null)
                {
                    //attempt a cac cert login
                    if (Session["SessionID"] == null)
                    {
                        //auto login with the cert on the CAC...
                        sec.CertLogin(this);
                    }
                }
            }
        }*/

        //get sessionid if set - set user id if session is ok
        //Session["SessionID"] gets set in the database when the user
        //logs in. this is used to cache values in the db and also
        //force timeouts etc....
 /*       if (Session["SessionID"] != null)
        {
            m_strSessionID = Session["SessionID"].ToString();

            //get actual user id
            string strUID = "";
            if (GetSessionValue("FX_USER_ID", out strUID))
            {
                if (strUID != "")
                {
                    m_lFXUserID = Convert.ToInt32(strUID);
                }

                //load the app specific user details
                //needed for the application
                APPMaster.LoadUserDetails();
            }
        }
        else
        {
            //default to ASP.net session if we have not logged in
            m_strSessionID = Context.Session.SessionID;
        }
  */

        //DBSessionID gets set in the database when the user
        //logs in. this is used to cache values in the db and to determine if the 
        //user is logged in
        //
        //reset FXUserID, only gets set in the call below
        FXUserID = 0;
        if (!String.IsNullOrEmpty(DBSessionID))
        {
            //get actual user id from the database session created when the 
            //user logs in
            string strUID = "";
            if (GetSessionValue("FX_USER_ID", out strUID))
            {
                if (strUID != "")
                {
                    FXUserID = Convert.ToInt32(strUID);
                }

                //load the app specific user details
                //needed for the application
                APPMaster.LoadUserDetails();
            }
            else
            {
                //log off if we cannot retrieve a valid session,
                //user timed out
                LogOff();
            }
        }

        //user does not have access to this page
        //so logoff.
        if (!sec.AuditPageAccess(this))
        {
            LogOff();
        }

        long lNewModuleID = -1;

        //keep the module id, page id and random data segment
        if (Request.QueryString["mid"] != null)
        {
            string strModuleID;
            GetSessionValue("CURR_MODULE_ID", out strModuleID);

            if (strModuleID != Request.QueryString["mid"].ToString())
            {
                lNewModuleID = 1;
            }
            else
            {
                lNewModuleID = -1;
            }
            m_lModuleID = Convert.ToInt32(Request.QueryString["mid"].ToString());
            SetSessionValue("CURR_MODULE_ID", Convert.ToString(m_lModuleID));
        }

        if (Request.QueryString["pid"] != null)
        {
            if (lNewModuleID != -1)
            {
                m_lPageID = -1;
            }
            else
            {
                m_lPageID = Convert.ToInt32(Request.QueryString["pid"].ToString());
            }

            SetSessionValue("CURR_PAGE_ID", Convert.ToString(m_lPageID));
        }

        if (Request.QueryString["rds"] != null)
        {
            m_strRDS = Request.QueryString["rds"].ToString();
        }

        if (m_lModuleID < 1)
        {
            string strModuleID = "";
            if (m_lFXUserID > 0)
            {
                GetSessionValue("CURR_MODULE_ID", out strModuleID);
                if (strModuleID != "")
                {
                    m_lModuleID = Convert.ToInt32(strModuleID);
                }
            }
        }

        if (m_lModuleID < 1)
        {
            string strModuleID = "";
            if (m_lFXUserID > 0)
            {
                GetSessionValue("CURR_MODULE_ID", out strModuleID);
                if (strModuleID != "")
                {
                    m_lModuleID = Convert.ToInt32(strModuleID);
                }
            }
        }

        if (m_lPageID < 1)
        {
            string strPageID = "";
            if (m_lFXUserID > 0)
            {
                GetSessionValue("CURR_PAGE_ID", out strPageID);
                if (strPageID != "")
                {
                    m_lPageID = Convert.ToInt32(strPageID);
                }
            }
        }
    }
      
    /// <summary>
    /// caches a session value in the database. 
    /// stored encrypted and more secure then an asp.net session var
    /// </summary>
    /// <param name="strKey"></param>
    /// <param name="strKeyValue"></param>
    /// <returns></returns>
    public bool SetSessionValue( string strKey,
                                 string strKeyValue)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        if (!IsLoggedIn())
        {
            return true;
        }

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddParameter("pi_vSessionID", DBSessionID, ParameterDirection.Input);
        pList.AddParameter("pi_vSessionClientIP", ClientIP, ParameterDirection.Input);
        pList.AddParameter("pi_nUserID", FXUserID, ParameterDirection.Input);
               
        //
        pList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
        pList.AddParameter("pi_vKeyValue", strKeyValue, ParameterDirection.Input);
        //
        //execute the stored procedure
        DBConn.ExecuteOracleSP("PCK_FX_SEC.SetSessionValue",
                                pList,
                                out lStatusCode,
                                out strStatusComment);
        
        // 0 = success if strStatus is populated it will show on the screen
       	// 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// gets a cached session value from the database. 
    /// stored encrypted and more secure then an asp.net session var
    /// </summary>
    /// <param name="strKey"></param>
    /// <param name="strKeyValue"></param>
    /// <returns></returns>
    public bool GetSessionValue(string strKey,
                                out string strKeyValue)
    {
        strKeyValue = "";

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddParameter("pi_vDBSessionID", DBSessionID, ParameterDirection.Input);
        pList.AddParameter("pi_vWebSessionID", ASPSessionID, ParameterDirection.Input);
        pList.AddParameter("pi_vSessionClientIP", ClientIP, ParameterDirection.Input);
        pList.AddParameter("pi_nUserID", FXUserID, ParameterDirection.Input);

        //
        pList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
        pList.AddParameter("po_vKeyValue", strKeyValue, ParameterDirection.Output);
      
        //
        //execute the stored procedure
        DBConn.ExecuteOracleSP("PCK_FX_SEC.GetSessionValue",
                               pList,
                               out lStatusCode,
                               out strStatusComment);

        
        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            CDataParameter paramValue = pList.GetItemByName("po_vKeyValue");
            strKeyValue = paramValue.StringParameterValue;

            return true;
        }

        strKeyValue = "";
        return false;
    }

    /// <summary>
    /// deletes a cached session value in the database.
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public bool DeleteSessionValue(string strKey)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddParameter("pi_vSessionID", DBSessionID, ParameterDirection.Input);
        pList.AddParameter("pi_vSessionClientIP", ClientIP, ParameterDirection.Input);
        pList.AddParameter("pi_nUserID", FXUserID, ParameterDirection.Input);
       
        pList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
        //
        //execute the stored procedure
        DBConn.ExecuteOracleSP("PCK_FX_SEC.DeleteSessionValue",
                               pList,
                               out lStatusCode,
                               out strStatusComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// deletes all cached session values in the database. 
    /// </summary>
    /// <returns></returns>
    public bool DeleteAllSessionValues()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddParameter("pi_vSessionID", DBSessionID, ParameterDirection.Input);
        pList.AddParameter("pi_vSessionClientIP", ClientIP, ParameterDirection.Input);
        pList.AddParameter("pi_nUserID", FXUserID, ParameterDirection.Input);
       
        //execute the stored procedure
        DBConn.ExecuteOracleSP("PCK_FX_SEC.DeleteAllSessionValues",
                               pList,
                               out lStatusCode,
                               out strStatusComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// helper to get the current page name
    /// </summary>
    /// <returns></returns>
    public string GetPageName()
    {
        string strPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(strPath);

        return oInfo.Name.ToLower();
    } 
   
    /// <summary>
    /// good place to close connections etc...
    /// </summary>
    public override void Dispose()
    {
        //close the database connection
        if (m_DBConnection != null) 
        {
            m_DBConnection.Close();
        }

        base.Dispose();
    }

    /// <summary>
    /// connect to datasource
    /// </summary>
    private bool ConnectToDataSource()
    {
        //get the connection string from the web.config file
        //connection string is encrypted in the file using MS recommended procedures
        //
        //cd\
        //cd windows
        //cd microsoft.net
        //cd framework
        //cd v2.0.50727
        //aspnet_regiis -pe "connectionStrings" -app "/PrimeCarePlus" -prov "RsaProtectedConfigurationProvider"
        //
        //look for connection strings in connection strings and app settings
        string strConnectionString = string.Empty;
        try
        {
            //try to get the connection string from the encrypted connectionstrings section
            strConnectionString = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
            Key = ConfigurationManager.ConnectionStrings["Key"].ConnectionString;
        }
        catch (Exception e)
        {
            string strStatus = e.Message;
        }

        bool bAudit = (ConfigurationManager.AppSettings["AUDIT"] == "1") ? true : false;

        //Connect to the database, connection is housed in the master page 
        //so that all pages that use the master have access to it.
        if (!m_DBConnection.Connect(
            strConnectionString,
            (int)DataConnectionType.Oracle,
            bAudit))
        {
            Session["DB_ERROR_CODE"] = string.Empty;
            Session["DB_ERROR"] = string.Empty;

            m_strStatusComment = "Error Connecting to Data Source";
            m_lStatusCode = 1;
            return false;
        }

        return true;
    }

    /// <summary>
    /// called to logoff the user
    /// </summary>
    public void LogOff()
    {
        //clear the patient
        this.ClosePatient();

        //clear FX_USER session var
        Session["FX_USER"] = null;

        //clear account details session var
        Session["ACC_DETAILS"] = null;

        //do any clean up necessary to logoff
        CSec sec = new CSec();
        sec.LogOff(this);

        //is an extra step for timeouts etc...
        if (!String.IsNullOrEmpty(DBSessionID))
        {
            DeleteAllSessionValues();
        }

        //clear the dbsessionid
        DBSessionID = String.Empty;

        //clear the session
        Session.Clear();

        //abandon the session
        Session.Abandon();

        //redirect;
        Response.Redirect("default.aspx");
    }

    public void LogOff(bool bRedirect)
    {
        //clear the patient
        this.ClosePatient();

        //clear FX_USER session var
        Session["FX_USER"] = null;

        //clear account details session var
        Session["ACC_DETAILS"] = null;

        //do any clean up necessary to logoff
        CSec sec = new CSec();
        sec.LogOff(this);

        //is an extra step for timeouts etc...
        if (!String.IsNullOrEmpty(DBSessionID))
        {
            DeleteAllSessionValues();
        }

        //clear the dbsessionid
        DBSessionID = String.Empty;

        //clear the session
        Session.Clear();

        //abandon the session
        Session.Abandon();

        //redirect;
        if (bRedirect)
        {
            Response.Redirect("default.aspx");
        }
    }

    /// <summary>
    /// Get a random number, good for forcing the browser to refresh a page
    /// also used to help generate our session id
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomNumber()
    {
        string strRand = "";
        Random r = new Random();
        strRand = Convert.ToString(r.NextDouble());
        strRand = strRand.Replace(".", "");

        return strRand;
    }

    /// <summary>
    /// Get a random chars, good for forcing the browser to refresh a page
    /// also used to help generate our session id
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomChars()
    {
        string strRand = "";
        Random r = new Random();
        strRand = Convert.ToString(r.NextDouble());
        strRand = strRand.Replace(".", "");

        string strRandChars = "";

        for (int i = 0; i < strRand.Length; i++)
        {
            string strC = "";
            strC = strRand.Substring(i, 1);
            if (strC == "0")
            {
                strRandChars += "a";
            }
            else if (strC == "1")
            {
                strRandChars += "b";
            }
            else if (strC == "2")
            {
                strRandChars += "c";
            }
            else if (strC == "3")
            {
                strRandChars += "d";
            }
            else if (strC == "4")
            {
                strRandChars += "e";
            }
            else if (strC == "5")
            {
                strRandChars += "f";
            }
            else if (strC == "6")
            {
                strRandChars += "g";
            }
            else if (strC == "7")
            {
                strRandChars += "h";
            }
            else if (strC == "8")
            {
                strRandChars += "i";
            }
            else if (strC == "9")
            {
                strRandChars += "j";
            }
            else
            {
                strRandChars += "z";
            }
        }

        return strRandChars;
    }


    // 2011-07-21 D.S.
    // Get "Last Updated" info
    public bool getLastUpdated(DataSet ds)
    {
        if (ds != null)
        {
            DateTime dtLastUpdated;
            DateTime.TryParse("1900-01-01", out dtLastUpdated);
            long lLastUpdatedBy = -1;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("last_updated") && !dr.IsNull("last_updated_by"))
                    {
                        DateTime dtRecUpdated = Convert.ToDateTime(dr["last_updated"]);
                        if (dtRecUpdated > dtLastUpdated)
                        {
                            dtLastUpdated = dtRecUpdated;
                            lLastUpdatedBy = Convert.ToInt32(dr["last_updated_by"]);
                        }
                    }
                }
            }

            if (lLastUpdatedBy > -1)
            {
                string strLastUpdatedBy = "";
                string strLastUpdated = dtLastUpdated.ToString();

                CUser user = new CUser();
                DataSet dsUser = user.GetLoginUserDS(this, lLastUpdatedBy);

                if (dsUser != null)
                {
                    CDataUtils utils = new CDataUtils();
                    strLastUpdatedBy = utils.GetStringValueFromDS(dsUser, "name");
                    string strUpdated = "Last updated on " + strLastUpdated + " by " + strLastUpdatedBy + ".";
                    this.SetVSValue("LAST_UPDATED", strUpdated);
                    return true;
                }
            }
        }
        return false;
    }

    //specify a different VS key
    public bool getLastUpdated(DataSet ds, string strVSKey)
    {
        if (ds != null)
        {
            DateTime dtLastUpdated;
            DateTime.TryParse("1900-01-01", out dtLastUpdated);
            long lLastUpdatedBy = -1;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("last_updated") && !dr.IsNull("last_updated_by"))
                    {
                        DateTime dtRecUpdated = Convert.ToDateTime(dr["last_updated"]);
                        if (dtRecUpdated > dtLastUpdated)
                        {
                            dtLastUpdated = dtRecUpdated;
                            lLastUpdatedBy = Convert.ToInt32(dr["last_updated_by"]);
                        }
                    }
                }
            }

            if (lLastUpdatedBy > -1)
            {
                string strLastUpdatedBy = "";
                string strLastUpdated = dtLastUpdated.ToString();

                CUser user = new CUser();
                DataSet dsUser = user.GetLoginUserDS(this, lLastUpdatedBy);

                if (dsUser != null)
                {
                    CDataUtils utils = new CDataUtils();
                    strLastUpdatedBy = utils.GetStringValueFromDS(dsUser, "name");
                    string strUpdated = "Last updated on " + strLastUpdated + " by " + strLastUpdatedBy + ".";
                    this.SetVSValue(strVSKey, strUpdated);
                    return true;
                }
            }
        }
        return false;
    }
}