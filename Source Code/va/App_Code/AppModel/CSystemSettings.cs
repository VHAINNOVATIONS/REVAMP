using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccess;

/// <summary>
/// Summary description for CSystemSettings
/// </summary>
public class CSystemSettings
{
	public CSystemSettings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //get a dataset of system settings
    public DataSet GetSystemSettingsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("", BaseMstr.SelectedPatientID
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_SYSTEM_SETTINGS.GetSystemSettingsRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    //update system settings...
    public bool UpdateSystemSettings(BaseMaster BaseMstr,
                                   string strMailSMTPHost,
                                   string strSenderEmail,
                                   long lMailSMTPPort,
                                   string strSiteURL,
                                   string strNotifyEmail,
                                   //New Texting Fields
                                   string strTextingHost,
                                   long   lTextingPort,
                                   string strTextingUser,
                                   string strTextingPswd,
                                   string strOraWinDir
                                   )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vMailSMTPHost", strMailSMTPHost);
        plist.AddInputParameter("pi_vSenderEmailAddress", strSenderEmail);
        plist.AddInputParameter("pi_nMailSMTPPort", lMailSMTPPort);
        plist.AddInputParameter("pi_vSiteURL", strSiteURL);
        plist.AddInputParameter("pi_vNotifyEmailAddress", strNotifyEmail);
        plist.AddInputParameter("pi_vTextingHost", strTextingHost);
        plist.AddInputParameter("pi_nTextingPort", lTextingPort);
        plist.AddInputParameter("pi_vTextingUser", strTextingUser);
        plist.AddInputParameter("pi_vTextingPswd", strTextingPswd);
        plist.AddInputParameter("pi_vOraWinDir", strOraWinDir);
        
        BaseMstr.DBConn.ExecuteOracleSP("PCK_SYSTEM_SETTINGS.UpdateSystemSettings",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }


    public DataSet GetSiteDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("", BaseMstr.SelectedPatientID
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_SYSTEM_SETTINGS.GetSiteRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

}
