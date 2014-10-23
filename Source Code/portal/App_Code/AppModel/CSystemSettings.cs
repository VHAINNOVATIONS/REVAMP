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
    //ReVamp Not Needed - Remove
    // Original Method public bool UpdateSystemSettings(BaseMaster BaseMstr,
    //                                 string strMailSMTPHost,
    //                                 string strSenderEmail,
    //                                 long lMailSMTPPort,
    //                                 string strSiteURL,
    //                                 string strNotifyEmail,
    //                                 long lHasMilitaryDetail,
    //                                 long lHasPatientSupervisorInput,
    //                                 long lHasPatientInsurance,
    //                                 long lBranchOfService
    //                                 )

    public bool UpdateSystemSettings(BaseMaster BaseMstr,
                                   string strMailSMTPHost,
                                   string strSenderEmail,
                                   long lMailSMTPPort,
                                   string strSiteURL,
                                   string strNotifyEmail
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

        //ReVamp Not Needed - Remove
        //plist.AddInputParameter("pi_nHasMilitaryDetail", lHasMilitaryDetail);
        //plist.AddInputParameter("pi_nHasPatientSupervisorInput", lHasPatientSupervisorInput);
        //plist.AddInputParameter("pi_nHasPatientInsurance", lHasPatientInsurance);
        //plist.AddInputParameter("pi_BranchOfService", lBranchOfService);

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
