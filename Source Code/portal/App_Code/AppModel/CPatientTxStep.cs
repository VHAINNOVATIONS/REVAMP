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
/// Summary description for CPatientTxStep
/// </summary>
public class CPatientTxStep
{
    private BaseMaster m_BaseMstr;

    public CPatientTxStep(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    public bool InsertPatientStep(long lStep)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        
        /*
            pi_vPatientID in varchar2,
            pi_nStep      in number,
         */

        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nStep", lStep);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_TX_STEP.InsertPatientStep",
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
            this.SetBaseMstrPatientStep();
            return true;
        }

        return false;
    }

    public bool UpdatePatientSteps()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call

        /*
            pi_vPatientID in varchar2,
         */

        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_TX_STEP.UpdatePatientSteps",
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
            this.SetBaseMstrPatientStep();
            return true;
        }

        return false;
    }

    public bool DeletePatientStep(long lStep)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call

        /*
            pi_vPatientID in varchar2,
            pi_nStep      in number,
         */

        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nStep", lStep);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_TX_STEP.DeletePatientStep",
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
            this.SetBaseMstrPatientStep();
            return true;
        }

        return false;
    }

    public DataSet GetPatientStepDS()
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
                                          "PCK_PATIENT_TX_STEP.GetPatientStepRS",
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

    public void SetBaseMstrPatientStep() 
    {
        CDataUtils utils = new CDataUtils();
        long lStep = -1,
             lNotificationStep = -1;
        DataSet ds = this.GetPatientStepDS();
        lStep = utils.GetLongValueFromDS(ds, "STEP");
        lNotificationStep = utils.GetLongValueFromDS(ds, "notification_step");

        if (lStep > -1) 
        {
            m_BaseMstr.PatientTxStep = lStep;
        }

        if (lNotificationStep > -1)
        {
            m_BaseMstr.NotificationTxStep = lNotificationStep;
        }
    }

    public bool UpdateNotificationSteps(long lNotificationStep)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call

        /*
            pi_vPatientID in varchar2,
         */

        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nNotificationStep", lNotificationStep);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT_TX_STEP.UpdateNotificationStep",
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
            this.SetBaseMstrPatientStep();
            return true;
        }

        return false;
    }

}