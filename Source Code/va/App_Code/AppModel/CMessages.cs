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
/// Summary description for CMessages
/// </summary>
public class CMessages
{
    protected BaseMaster m_BaseMstr;

    public CMessages(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    public DataSet GetUserMessagesDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetUserMessagesRS",
                                          plist,
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

    public DataSet GetUnreadMessagesDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetUnreadMessagesRS",
                                          plist,
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

    public DataSet GetDeletedMessagesDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetDeletedMessagesRS",
                                          plist,
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

    public DataSet GetSentMessagesDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetSentMessagesRS",
                                          plist,
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

    public DataSet GetProviderPatientsDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetProviderPatientsRS",
                                          plist,
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

    public DataSet GetAllProviderDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetAllProviderRS",
                                          plist,
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

    public DataSet GetRecipientsListDS(long lMessageID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMessageID", lMessageID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_MESSAGES.GetRecipientsListRS",
                                          plist,
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
    
    public bool SendMessage(string strRecipients,
                            string strSubject,
                            string strBody,
                            out long lMessageID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        lMessageID = -1;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vRecipients", strRecipients);
        plist.AddInputParameter("pi_vSubject", strSubject);
        plist.AddInputParameterCLOB("pi_clobBody", strBody);
        plist.AddOutputParameter("po_nMessageID", lMessageID);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_MESSAGES.SendMessage",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            CDataParameter paramValue = plist.GetItemByName("po_nMessageID");
            lMessageID = paramValue.LongParameterValue;
            return true;
        }
        return false;
    }

    public bool MarkAsRead(long lMessageID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMessageID", lMessageID);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_MESSAGES.MarkAsRead",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    public bool MarkAsDeleted(long lMessageID, long lIsSentMsg)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMessageID", lMessageID);
        plist.AddInputParameter("pi_nIsSentMsg", lIsSentMsg);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_MESSAGES.MarkAsDeleted",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

}