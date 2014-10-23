using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DataAccess;

/// <summary>
/// Summary description for CReferralClinic
/// </summary>
public class CReferralClinic
{
	public CReferralClinic()
	{
    }

    public DataSet GetReferralClinicLookupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_REFERRAL_CLINIC.GetReferralClinicLookUpRS",
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

    public DataSet GetReferralClinicLookupDS(BaseMaster BaseMstr,
                                             string strSearchValue
                                             )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vSearchValue", strSearchValue);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_REFERRAL_CLINIC.GetReferralClinicLookUpRS",
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

    public bool InsertReferralClinic(BaseMaster BaseMstr,
                                        string strReferralDesc,
                                        string strReferralText,
                                        //Not Needed Now string strPersonContacted,
                                        string strProviderName,
                                        string strAddress,
                                        string strCity,
                                        string strStateID,
                                        string strPostalCode,
                                        string strPhone,
                                        string strFax,
                                        //Not Needed Now long   lReleaseSigned,
                                        out long lReferralID
                                      )
    {
        lReferralID = -1;
        
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";
        

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vReferralDesc", strReferralDesc);
        plist.AddInputParameter("pi_vReferralText", strReferralText);
        //Not Needed Now plist.AddInputParameter("pi_vPersonContacted", strPersonContacted);
        plist.AddInputParameter("pi_vProviderName", strProviderName);
        plist.AddInputParameter("pi_vAddress", strAddress);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_nStateID", strStateID);
        plist.AddInputParameter("pi_vPostalCode", strPostalCode);
        plist.AddInputParameter("pi_vPhone", strPhone);
        plist.AddInputParameter("pi_vFax", strFax);
        //Not Needed Now plist.AddInputParameter("pi_nReleaseSigned", lReleaseSigned);
        plist.AddOutputParameter("po_nReferralID", lReferralID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_REFERRAL_CLINIC.InsertReferralClinic",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        CDataParameter parmReferralID = plist.GetItemByName("po_nReferralID");
        lReferralID = parmReferralID.LongParameterValue;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            BaseMstr.StatusComment = "Referral Clinic - Saved. ";
            return true;
        }

        return false;
    }

    public bool UpdateReferralClinic(BaseMaster BaseMstr,
                                        long lReferralID,
                                        string strReferralDesc,
                                        string strReferralText,
                                        //Not Needed Now string strPersonContacted,
                                        string strProviderName,
                                        string strAddress,
                                        string strCity,
                                        string strStateID,
                                        string strPostalCode,
                                        string strPhone,
                                        string strFax
                                        //Not Needed Now long lReleaseSigned
                                     )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nReferralID", lReferralID);
        plist.AddInputParameter("pi_vReferralDesc", strReferralDesc);
        plist.AddInputParameter("pi_vReferralText", strReferralText);
        //Not Needed Now plist.AddInputParameter("pi_vPersonContacted", strPersonContacted);
        plist.AddInputParameter("pi_vProviderName", strProviderName);
        plist.AddInputParameter("pi_vAddress", strAddress);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_nStateID", strStateID);
        plist.AddInputParameter("pi_vPostalCode", strPostalCode);
        plist.AddInputParameter("pi_vPhone", strPhone);
        plist.AddInputParameter("pi_vFax", strFax);
        //Not Needed Now plist.AddInputParameter("pi_nReleaseSigned", lReleaseSigned);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_REFERRAL_CLINIC.UpdateReferralClinic",
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
            BaseMstr.StatusComment = "Referral Clinic - Updated. ";
            return true;
        }

        return false;
    }

    public bool DiscontinueReferralClinic(BaseMaster BaseMstr,
                                        long lReferralID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nReferralID", lReferralID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_REFERRAL_CLINIC.DiscontinueReferralClinic",
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
}
