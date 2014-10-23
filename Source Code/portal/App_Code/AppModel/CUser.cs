using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;

/// <summary>
/// Summary description for CUser
/// </summary>
public class CUser
{
    /// <summary>
    /// empty constructor
    /// </summary>
	public CUser()
	{
		
	}

    /// <summary>
    /// get a user dataset
    /// </summary>
    /// <param name="BaseMstr"></param>
    /// <param name="strLastName"></param>
    /// <param name="strFirstName"></param>
    /// <returns></returns>
    public DataSet GetLoginUserDS( BaseMaster BaseMstr,
                                   long lFXUserID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vKey", BaseMstr.Key);
        pList.AddInputParameter("pi_nFXUserID", lFXUserID);
       
        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_USER.GetLoginUserRS",
                                          pList,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return ds;
        }

        return null;
    }

    //get details from the user login, these will be cached in the 
    //masterpage...
    public bool GetLoginUserDetails(BaseMaster BaseMstr,
                                     long lFXUserID,
                                     out long lUserType,
                                     out long lUserRights,
                                     out long lUserReadOnly,
                                     out string strDMISID,
                                     out string strProviderID,
                                     out long lPWDExpiresIn)
    {
        lUserType = 0;
        lUserRights = 0;
        lUserReadOnly = 0;
        strDMISID = "";
        strProviderID = "";
        lPWDExpiresIn = 0;

        DataSet ds = GetLoginUserDS(BaseMstr, lFXUserID);
        if (ds != null)
        {
            //loop and set roles on the base master
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("user_type"))
                    {
                        lUserType = Convert.ToInt32(row["user_type"]);
                    }

                    if (!row.IsNull("user_rights"))
                    {
                        lUserRights = Convert.ToInt32(row["user_rights"]);
                    }

                    if (!row.IsNull("read_only"))
                    {
                        lUserReadOnly = Convert.ToInt32(row["read_only"]);
                    }

                    if (!row.IsNull("dims_id"))
                    {
                        strDMISID = Convert.ToString(row["dims_id"]);
                    }

                    if (!row.IsNull("provider_id"))
                    {
                        strProviderID = Convert.ToString(row["provider_id"]);
                    }

                    if (!row.IsNull("days_till_expiration"))
                    {
                        lPWDExpiresIn = Convert.ToInt32(row["days_till_expiration"]);
                    }
                }
            }

            return true;
        }

        return false;
    }


}
