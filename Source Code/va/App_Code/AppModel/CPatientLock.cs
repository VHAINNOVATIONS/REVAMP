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
/// Summary description for CPatientLock
/// </summary>
public class CPatientLock
{
    protected BaseMaster m_BaseMstr;
    
    public CPatientLock(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
    }

    public bool RefreshPatientLock(string strPatientID)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //Execute Oracle stored procedure
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_LOCK.RefreshPatientLock",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected DataSet GetPatientLock(string strPatientID) 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                           "PCK_PATIENT_LOCK.GetPatientLock",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public bool IsPatientLocked(string strPatientID, out string strProviderName, out string strProviderEmail) 
    {
        strProviderName = String.Empty;
        strProviderEmail = String.Empty;
        DataSet ds = this.GetPatientLock(strPatientID);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                bool bLocked = true;
                foreach (DataTable dt in ds.Tables) 
                {
                    foreach (DataRow dr in dt.Rows) 
                    { 
                        if(dr["PATIENT_ID"].ToString() == strPatientID)
                        {
                            if (Convert.ToInt32(dr["FX_USER_ID"]) == m_BaseMstr.FXUserID)
                            {
                                bLocked = false;
                            }
                            strProviderName = dr["PROVIDER_NAME"].ToString();
                            strProviderEmail = dr["EMAIL"].ToString();
                        }
                    }
                }
                return bLocked;
            }
            else
            {
                //insert lock record
                this.InsertPatientLock(strPatientID);
                return false;
            }
        }
        else
        {
            //insert lock record
            this.InsertPatientLock(strPatientID);
            return false;
        }
    }

    public bool InsertPatientLock(string strPatientID)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //Execute Oracle stored procedure
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_LOCK.InsertPatientLock",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeletePatientLock(string strPatientID)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //Execute Oracle stored procedure
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_LOCK.DeletePatientLock",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
