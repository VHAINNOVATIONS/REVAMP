using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAccess;

/// <summary>
/// Summary description for CSoapp
/// </summary>
public class CSoapp
{
	public CSoapp()
	{
		
	}

    public bool updtSubjective(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strSubjective) 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vSubjective", strSubjective);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtSubjective",
                                        pList, 
                                        out lStatusCode, 
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtObjective(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strObjective)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vObjective", strObjective);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtObjective",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtAssessment(
        BaseMaster BaseMstr, 
        string strEncounterID, 
        long lnTreatmentID, 
        string strAssessment, 
        long lnDLCID, 
        string strDLCJustification,
        long lIsHighInterest)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vKey", BaseMstr.Key);
        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vAssessment", strAssessment);
        pList.AddInputParameter("pi_nDLCID", lnDLCID);
        pList.AddInputParameter("pi_vDLCJustification", strDLCJustification);
        pList.AddInputParameter("pi_nIsHighInterest", lIsHighInterest);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtAssessment",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtPlan(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strPlan)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vPlan", strPlan);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtPlan",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtTreatmentPlan(BaseMaster BaseMstr, string strPatientID, string strEncounterID, long lnTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtTreatmentPlan",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtAddendum(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strAddendum)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vAddendum", strAddendum);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtAddendum",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool updtAddendumSave(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strAddendumSave)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameterCLOB("pi_vAddendumSave", strAddendumSave);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtAddendumSave",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //"Maintained Not Being Used."
    public bool updtSessionTime(BaseMaster BaseMstr, string strEncounterID, long lnTreatmentID, string strMaintained, string strSessionTime)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lnTreatmentID);
        pList.AddInputParameter("pi_vMaintained", strMaintained);
        pList.AddInputParameter("pi_vSessionTime", strSessionTime);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_SOAPP_REPORT.updtSessionTime",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

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
