using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.SessionState;

/// <summary>
/// Summary description for CMDWSUtils
/// </summary>
public class CMDWSUtils
{
    public CMDWSUtils()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// get patient ids
    /// </summary>
    /// <param name="strConnectionString"></param>
    /// <param name="bAudit"></param>
    /// <param name="baseMstr"></param>
    /// <param name="data"></param>
    /// <param name="strPatientID"></param>
    /// <param name="strMDWSPatientID"></param>
    /// <param name="strProviderID"></param>
    /// <returns></returns>
    public REVAMP.TIU.CStatus GetPatientIDs(string strConnectionString,
                                            bool bAudit,
                                            BaseMaster baseMstr,
                                            REVAMP.TIU.CData data,
                                            string strPatientID,
                                            out string strMDWSPatientID,
                                            out string strProviderID)
    {
        strMDWSPatientID = String.Empty;
        strProviderID = String.Empty;

        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get the MDWS patient ids
        DataSet dsPat;
        REVAMP.TIU.CPatientData pd = new REVAMP.TIU.CPatientData(data);
        status = pd.GetPatientIDDS(strConnectionString,
                                   bAudit,
                                   baseMstr.FXUserID,
                                   baseMstr.SelectedPatientID,
                                   out dsPat);
        if (status.Status)
        {
            strMDWSPatientID = REVAMP.TIU.CDataUtils.GetDSStringValue(dsPat, "MDWS_PATIENT_ID");
            strProviderID = REVAMP.TIU.CDataUtils.GetDSStringValue(dsPat, "PROVIDER_ID");
        }

        return status;
    }

    /// <summary>
    /// checks if we are logged in and logs in if we are not
    /// </summary>
    /// <param name="data"></param>
    /// <param name="lMDWSUserID"></param>
    /// <returns></returns>
    public REVAMP.TIU.CStatus MDWSLogin(REVAMP.TIU.CData data,
                                        BaseMaster BaseMstr,
                                        string strConnectionString,
                                        bool bAudit,
                                        out long lMDWSUserID)
    {
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //if the user is logged in and not times out then 
        //no reason to login again.
        lMDWSUserID = 0;
        REVAMP.TIU.CAppUser appUser = new REVAMP.TIU.CAppUser(data);
        status = appUser.CheckMDWSConnection(out lMDWSUserID);
        if (!status.Status)
        {
            status = appUser.MDWSLogin(strConnectionString,
                                       bAudit,
                                       BaseMstr.FXUserID,
                                       BaseMstr.Key,
                                       BaseMstr.Session,
                                       out lMDWSUserID);
        }

        return status;
    }

    /// <summary>
    /// load note titles ddl
    /// </summary>
    /// <param name="strNoteTitleLabel"></param>
    /// <param name="master"></param>
    /// <param name="ddlNoteTitle"></param>
    /// <returns></returns>
    public bool LoadNoteTitlesDDL(string strNoteTitleLabel,
                               BaseMaster master,
                               DropDownList ddlNoteTitle)
    {
        ddlNoteTitle.Items.Clear();

        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //check connection login if we are not connected
        long lMDWSUserID = 0;
        status = mdwsUtils.MDWSLogin(data,
                                     master,
                                     strConnectionString,
                                     bAudit,
                                     out lMDWSUserID);
        if (!status.Status)
        {
            return false;
        }

        REVAMP.TIU.CNoteTitleData ntd = new REVAMP.TIU.CNoteTitleData(data);
        DataSet dsNoteTitles = null;
        status = ntd.GetNoteTitleDS(strConnectionString,
                                     bAudit,
                                     master.FXUserID,
                                     master.Session,
                                     out dsNoteTitles);

        status = REVAMP.TIU.CDropDownList.RenderDataSet(dsNoteTitles,
                                                         ddlNoteTitle,
                                                         "NOTE_TITLE_LABEL",
                                                         "NOTE_TITLE_LABEL");
        ddlNoteTitle.SelectedIndex = -1;

        REVAMP.TIU.CDropDownList.SelectItemByValue(ddlNoteTitle, strNoteTitleLabel);

        return true;
    }

    /// <summary>
    /// load clinics DDL
    /// </summary>
    /// <returns></returns>
    public bool LoadClinicsDDL(long lNoteClinicID,
                               BaseMaster master,
                               DropDownList ddlClinic)
    {
        ddlClinic.Items.Clear();

        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //check connection login if we are not connected
        long lMDWSUserID = 0;
        status = MDWSLogin(data,
                           master,
                           strConnectionString,
                           bAudit,
                           out lMDWSUserID);
        if (!status.Status)
        {
            return false;
        }

        REVAMP.TIU.CClinicData ntd = new REVAMP.TIU.CClinicData(data);
        DataSet dsClinics = null;
        status = ntd.GetClinicDS(strConnectionString,
                                  bAudit,
                                  master.FXUserID,
                                  master.Session,
                                  out dsClinics);

        status = REVAMP.TIU.CDropDownList.RenderDataSet(dsClinics,
                                                         ddlClinic,
                                                         "CLINIC_LABEL",
                                                         "CLINIC_ID");
        ddlClinic.SelectedIndex = -1;
        REVAMP.TIU.CDropDownList.SelectItemByValue(ddlClinic, lNoteClinicID);

        return true;
    }

    /// <summary>
    /// get data info
    /// </summary>
    public REVAMP.TIU.CStatus GetDataInfo(BaseMaster BaseMstr,
                                           HttpSessionState WebSession,
                                           out REVAMP.TIU.CData data,
                                           out string strConnectionString,
                                           out bool bAudit)
    {
        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //conn string
        strConnectionString = String.Empty;

        //audit
        bAudit = false;

        //need a data class
        data = new REVAMP.TIU.CData(null,
                                    BaseMstr.ClientIP,
                                    BaseMstr.FXUserID,
                                    BaseMstr.ASPSessionID,
                                    WebSession,
                                    true);

        //get the conenction string
        strConnectionString = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;

        //get the audit
        bAudit = false;
        string strAudit = "";
        if (System.Configuration.ConfigurationManager.AppSettings["AUDIT"] != null)
        {
            strAudit = System.Configuration.ConfigurationManager.AppSettings["AUDIT"].ToString();
            if (strAudit == "1")
            {
                bAudit = true;
            }
        }

        return status;
    }

    /// <summary>
    /// load regions dll
    /// </summary>
    public bool LoadRegionsDDL(long lRegionID,
                                BaseMaster master,
                                DropDownList ddlRegion)
    {
        ddlRegion.Items.Clear();
        DataSet dsRegion = null;

        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);


        REVAMP.TIU.CSiteData siteData = new REVAMP.TIU.CSiteData(data);

        //get the regions
        status = siteData.GetRegionDS(strConnectionString,
                                      bAudit,
                                      master.FXUserID,
                                      out dsRegion);

        REVAMP.TIU.CDropDownList.RenderDataSet(dsRegion,
                                               ddlRegion,
                                               "REGION_NAME",
                                               "REGION_ID");

        ddlRegion.SelectedIndex = -1;
        REVAMP.TIU.CDropDownList.SelectItemByValue(ddlRegion, lRegionID);

        return true;
    }

    /// <summary>
    /// load sites ddl
    /// </summary>
    /// <param name="lRegionID"></param>
    /// <param name="lSiteID"></param>
    /// <param name="master"></param>
    /// <param name="ddlSite"></param>
    /// <returns></returns>
    public bool LoadSitesDDL(long lRegionID,
                             long lSiteID,
                             BaseMaster master,
                             DropDownList ddlSite)
    {
        ddlSite.Items.Clear();

        DataSet dsSite = null;

        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        REVAMP.TIU.CSiteData siteData = new REVAMP.TIU.CSiteData(data);

        //get all sites for this region
        siteData.GetSiteDS(strConnectionString,
                           bAudit,
                           master.FXUserID,
                           lRegionID,
                           out dsSite);

        REVAMP.TIU.CDropDownList.RenderDataSet(dsSite,
                                               ddlSite,
                                               "SITE_NAME",
                                               "SITE_ID");

        ddlSite.SelectedIndex = -1;
        REVAMP.TIU.CDropDownList.SelectItemByValue(ddlSite, lSiteID);

        return true;
    }

    /// <summary>
    /// update mdws defaults
    /// </summary>
    /// <param name="lClinicID"></param>
    /// <param name="strNoteTitleLabel"></param>
    /// <param name="master"></param>
    /// <returns></returns>
    public bool UpdateMDWSDefaults(long lClinicID,
                                   string strNoteTitleLabel,
                                   BaseMaster master)
    {
        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //user data
        REVAMP.TIU.CUserData userData = new REVAMP.TIU.CUserData(data);

        //update the account
        status = userData.UpdateMDWSAccount(strConnectionString,
                                            bAudit,
                                            master.FXUserID,
                                            strNoteTitleLabel,
                                            lClinicID);
        if (!status.Status)
        {
            //error so update status
            master.StatusCode = 1;
            master.StatusComment = "An error occured while saving MDWS TIU Note defaults, Please contact your System Administrator!";
            return false;
        }

        master.StatusCode = 0;
        master.StatusComment = "";

        return status.Status;
    }

    /// <summary>
    /// update the mdws account
    /// </summary>
    /// <param name="lRegionID"></param>
    /// <param name="lSiteID"></param>
    /// <param name="strUserName"></param>
    /// <param name="strPWD"></param>
    /// <param name="master"></param>
    /// <returns></returns>
    public bool UpdateMDWSAccount(long lRegionID,
                                      long lSiteID,
                                      string strUserName,
                                      string strPWD,
                                      BaseMaster master)
    {
        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //user data
        REVAMP.TIU.CUserData userData = new REVAMP.TIU.CUserData(data);

        //try a test login to get the MDWS user id
        long lMDWSUserID = 0;
        REVAMP.TIU.CAppUser appuser = new REVAMP.TIU.CAppUser(data);
        status = appuser.MDWSLogin(strConnectionString,
                                   bAudit,
                                   master.FXUserID,
                                   master.Key,
                                   strUserName,
                                   strPWD,
                                   lRegionID,
                                   lSiteID,
                                   out lMDWSUserID);
        if (!status.Status)
        {
            master.StatusCode = 1;
            master.StatusComment = status.StatusComment;
            if (status.StatusComment.ToUpper() == "FAILED TO LOGIN!")
            {
                master.StatusComment = "Invalid MDWS Credentials, Please check your data entry!";
            }

            return false;
        }

        //update the account
        status = userData.UpdateMDWSAccount(strConnectionString,
                                            bAudit,
                                            master.FXUserID,
                                            master.FXUserID,
                                            master.Key,
                                            strUserName,
                                            strPWD,
                                            lRegionID,
                                            lSiteID,
                                            lMDWSUserID);
        if (!status.Status)
        {
            //error so update status
            master.StatusCode = 1;
            master.StatusComment = "An error occured while saving MDWS credentials, Please contact your System Administrator!";
            return false;
        }

        master.StatusCode = 0;
        master.StatusComment = "";

        return status.Status;
    }

    /// <summary>
    /// get mdws account info
    /// </summary>
    /// <param name="lFXUserID"></param>
    /// <param name="strMDWSUserName"></param>
    /// <param name="strMDWSPWD"></param>
    /// <param name="lRegionID"></param>
    /// <param name="lSiteID"></param>
    /// <param name="strNoteTitleLabel"></param>
    /// <param name="lNoteClinicID"></param>
    /// <param name="master"></param>
    /// <returns></returns>
    public bool GetMDWSAccountInfo(long lFXUserID,
                                    out string strMDWSUserName,
                                    out string strMDWSPWD,
                                    out long lRegionID,
                                    out long lSiteID,
                                    out string strNoteTitleLabel,
                                    out long lNoteClinicID,
                                    BaseMaster master)
    {
        strMDWSUserName = String.Empty;
        strMDWSPWD = String.Empty;
        lRegionID = 0;
        lSiteID = 0;
        strNoteTitleLabel = String.Empty;
        lNoteClinicID = 0;

        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(master,
                              master.Session,
                              out data,
                              out strConnectionString,
                              out bAudit);
        //user data
        REVAMP.TIU.CUserData userData = new REVAMP.TIU.CUserData(data);

        //update the account
        DataSet ds = null;
        status = userData.GetMDWSAccountDS(strConnectionString,
                                            bAudit,
                                            master.FXUserID,
                                            lFXUserID,
                                            master.Key,
                                            out ds);
        if (!status.Status)
        {
            //error so update status
            master.StatusCode = 1;
            master.StatusComment = status.StatusComment;
        }
        else
        {
            strMDWSUserName = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_USER_NAME");
            strMDWSPWD = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_PWD");
            lRegionID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_REGION_ID");
            lSiteID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_SITE_ID");
            strNoteTitleLabel = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_NOTE_TITLE_LABEL");
            lNoteClinicID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_NOTE_CLINIC_ID");
        }

        return status.Status;
    }
}
