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
using DataAccess;
using Ext.Net;

public partial class MasterPage : BaseMaster
{
    public string strMenuItems;
    public string strMilisecondsSessionExpire;
    public string strSessionTimeout;

    //common objects
    protected CUser usr = new CUser();
    protected CDataUtils utils = new CDataUtils();
    protected CTreatment treatment = new CTreatment();

    protected void Page_Load(object sender, EventArgs e)
    {
        //doesn't cache the page so users cannot back into sensitive information after logging off
        //Response.AddHeader("X-UA-Compatible", "IE=9");
        //Response.Cache.SetAllowResponseInBrowserHistory(false);
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        //Response.Cache.SetNoStore();
        //Response.AddHeader("Pragma", "no-cache");
        
        //loads version
        lblVersion.Text = ConfigurationManager.AppSettings["Version"];

        //set session time remaining
        strMilisecondsSessionExpire = SessionTimeRemaining();
        strSessionTimeout = SessionTimeRemaining();
       
        // register Ext.NET library icons
        ResourceManager1.RegisterIcon(Icon.Information);

        if (!IsPostBack) {
            //get site
            if (Session["SiteID"] == null)
            {
                CSystemSettings SysSettings = new CSystemSettings();
                DataSet dsSite = SysSettings.GetSiteDS(this);
                string strSiteID = utils.GetStringValueFromDS(dsSite, "SITE_ID");
                Session["SiteID"] = strSiteID;
            }
        }


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

            }
        }
        #endregion

        #region ShowUsernameAndPatientDemographics
        if (this.IsLoggedIn())
        {
            //Name Of User Currently logged on.
            string strLoginDateTime = String.Empty;
            if (Session["LOGINDATETIME"] == null)
            {
                strLoginDateTime += DateTime.Now.ToShortDateString();
                strLoginDateTime += " ";
                strLoginDateTime += DateTime.Now.ToShortTimeString();
                Session["LOGINDATETIME"] = strLoginDateTime;
            }

            string strUserLoggedOn = String.Empty;
            if (Session["USERLOGGEDON"] == null)
            {
                strUserLoggedOn += "<img alt=\"Account Activity\" src=\"Images/information.png\" style=\"cursor: pointer; vertical-align: middle; margin-right: 3px;\" onclick=\"showAccDetails();\" />";
                strUserLoggedOn += UserLoggedOn();
                strUserLoggedOn += " - ";
                strUserLoggedOn += Session["LOGINDATETIME"].ToString();
                
                lblUserLoggedOn.Text = strUserLoggedOn;
                Session["USERLOGGEDON"] = strUserLoggedOn;
            }
            else
            {
                lblUserLoggedOn.Text = Session["USERLOGGEDON"].ToString();
            }

            //draw the patient info bar at the top
            //patDemoInfoBar.InnerHtml = "";

        }
        #endregion

        string strPage = this.GetPageName().ToLower();
        
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
        btnEmailNew.Visible = this.HasNewMessage();

    }

    //build menus and toolbar
    protected void BuildMenu() {
        CAppMenu menu = new CAppMenu(this);
        strMenuItems = menu.RenderMenuHTML();
    }
    
    protected string UserLoggedOn()
    {
        string strUserName = String.Empty;
        CDataUtils utils = new CDataUtils();
        CUser user = new CUser();
        DataSet dsUser = user.GetLoginUserDS(this, this.FXUserID);
        strUserName = utils.GetDSStringValue(dsUser, "NAME");

        return strUserName;
    }

    protected void btnKeepAlive_OnClick(object sender, EventArgs e) 
    {
        string strValue = "";
        if (this.GetSessionValue("FX_USER_ID", out strValue))
        {
            Session["SESSION_INITIATED"] = DateTime.Now;
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
        
        /*
        long lSessionTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SessionTimeout"]) - 60000;
        
        if (this.IsLoggedIn() && Session["SESSION_INITIATED"] != null)
        {
            DateTime dsSessionInitiated = (DateTime)Session["SESSION_INITIATED"];
            TimeSpan tsRemainingTime = DateTime.Now.Subtract(dsSessionInitiated);
            long lTimeRemaining = lSessionTimeout - (long)tsRemainingTime.TotalMilliseconds;

            if (lTimeRemaining < 31000) 
            {
                lTimeRemaining = 31000;
            }
            
            strSessionTimeout = lSessionTimeout.ToString();
            return lTimeRemaining.ToString();
        }
        else 
        {
            strSessionTimeout = lSessionTimeout.ToString();
            return String.Empty;
        }
         */
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
                //divLoginInfo.Visible = true;

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

                //litLoginInfo.Text = strAccDetails;

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

    protected void btnSearch_OnClick(object sender, EventArgs e) {
        if (txtSearchKeyword.Text.Trim().Length > 0) {
            string strSearch = Server.UrlEncode(txtSearchKeyword.Text);
            Response.Redirect("Search.aspx?q=" + strSearch);
        }
    }
}