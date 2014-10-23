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
/// Summary description for CPatientEvent
/// </summary>
public class CPatientEvent
{
	private BaseMaster m_BaseMstr;

    public CPatientEvent(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    public DataSet GetPatientEventsDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_PATIENT_EVENTS.GetPatientEventsRS",
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

    public DataSet GetAllPatientEventsDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_PATIENT_EVENTS.GetAllPatientEventsRS",
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


    public bool CompletedEvent(long lEventID) 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nEventID", lEventID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.CompletedEvent",
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

    public bool AddAllEvents()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.AddAllEvents",
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

    public bool UpdateEvent(long lEventID, string strEventDate, long lStatus, string strCommnents)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nEventID", lEventID);
        plist.AddInputParameter("pi_vEventDate", strEventDate);
        plist.AddInputParameter("pi_nStatus", lStatus);
        plist.AddInputParameter("pi_vComments", strCommnents);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.UpdatePatientEvent",
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

    public DataSet GetStatEventsDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_PATIENT_EVENTS.GetStatEventsRS",
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

    //LGL NEW
    public bool AddSpecificEvent(long lEventID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nEventID", lEventID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.AddSpecificEvent",
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

    public bool CompletedSpecificEvent(long lEventID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nEventID", lEventID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.CompletedSpecificEvent",
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

    public bool CheckPAPEvent()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.CheckPAPEvent",
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

    public bool CheckPAPEventALL()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_EVENTS.CheckPAPEventALL",
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