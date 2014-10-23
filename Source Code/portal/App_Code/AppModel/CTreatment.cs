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
/// Summary description for CTreatment
/// </summary>
public class CTreatment
{
	public CTreatment()
	{
		
	}

    //get a dataset of patients encounters
    public DataSet GetPatientTreatmentsDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                          long    lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        
        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetPatientTreatmentsRS",
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

    //get a dataset of patients encounters
    public DataSet GetTreatmentListDS( BaseMaster BaseMstr,
                                       string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
       
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentListRS",
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

    public DataSet GetTreatmentEventsDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                          long    lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentEventsRS",
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

    public DataSet GetTreatmentReferralReasonsDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                          long lTreatmentID,
                                          long lEventID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nEventID", lEventID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentReferralReasonsRS",
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

    public DataSet GetTreatmentASAMPPC2sDS(BaseMaster BaseMstr,
                                                string strPatientID,
                                                long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        
        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentASAMPPC2sRS",
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

    public DataSet GetTreatmentSubstancesDS(BaseMaster BaseMstr,
                                                string strPatientID,
                                                long lTreatmentID,
                                                long lEventID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nEventID", lEventID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentSubstancesRS",
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

    public DataSet GetTreatmentSupervisorInputDS(BaseMaster BaseMstr,
                                               string strPatientID,
                                               long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        
        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentSupervisorInputRS",
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

    public DataSet GetTreatmentLearningDS(BaseMaster BaseMstr,
                                               string strPatientID,
                                               long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentLearningRS",
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

    public DataSet GetTreatment18MSBDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatment18MSBRS",
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

    public DataSet GetTreatmentGoalDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentGoalRS",
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

    public DataSet GetTreatmentGoalObjectiveDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentGoalObjectiveRS",
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


    public DataSet GetTreatmentMEALabQuantDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID,
                                       long lGoalID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nGoalID", lGoalID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentMEALabQuantRS",
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

    public DataSet GetTreatmentMEALabQualDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID,
                                       long lGoalID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nGoalID", lGoalID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentMEALQualRS",
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

    public DataSet GetTreatmentMEAValueDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID,
                                       long lGoalID,
                                       long lObjectiveID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nGoalID", lGoalID);
        plist.AddInputParameter("pi_nObjectiveID", lObjectiveID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentMEAValueRS",
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

    public bool GetTreatmentCount(BaseMaster BaseMstr,
                                 string strPatientID,
                                 long lnLookupSearchCase)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nSelectCases", lnLookupSearchCase);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentsCount",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            if (ds != null)
            {
                int m_records = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        m_records += Convert.ToInt32(dr["total_records"].ToString());
                    }
                }
                if (m_records > 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public DataSet GetRecordList(BaseMaster BaseMstr,
                                 string strPatientID,
                                 long lnLookupSearchCase)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nSelectCases", lnLookupSearchCase);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentsList",
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

    public DataSet GetClinicOpenCases(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetClinicOpenCases",
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

    public bool CloseCase(BaseMaster BaseMstr, bool bFailedTreatment)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_nFailedTreatment", Convert.ToInt32(bFailedTreatment));

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.CloseCase",
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

    public bool OpenCase(BaseMaster BaseMstr,
                         string strPatientID,
                         string strEncounterID,
                         out long lNewTreatmentID)
    {
        lNewTreatmentID = 0;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddOutputParameter("po_nNewTreatmentID", lNewTreatmentID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.CreateNewCase",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            CDataParameter paramValue = pList.GetItemByName("po_nNewTreatmentID");
            lNewTreatmentID = paramValue.LongParameterValue;
            return true;
        }
        else
        {
           return false;
        }
    }

    public bool isOpen(BaseMaster BaseMstr)
    {
        if ( (BaseMstr.SelectedPatientID != "")
             && (BaseMstr.SelectedTreatmentID > 0))
        {
            DataSet recs = this.GetRecordList(BaseMstr, BaseMstr.SelectedPatientID, BaseMstr.LookupSearchCase);
            if (recs != null)
            {
                DataRow[] arrRecs = recs.Tables[0].Select("treatment_id = " + BaseMstr.SelectedTreatmentID, "");
                if (arrRecs.GetLength(0) > 0)
                {
                    if (arrRecs[0]["case_closed"].ToString() == "0")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public DataSet GetLevelOfCareDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetLevelOfCareRS",
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

    //load a dropdown list of Client Work Performance
    public void LoadLevelOfCareDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetLevelOfCareDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "DESCRIPTION",
                          "LEVEL_OF_CARE_ID",
                          strSelectedID);
    }

    public bool UpdateLevelOfCare(BaseMaster BaseMstr,
                                  string strPatientID,
                                  long lTreatmentID,
                                  long lLevelOfCareID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        pList.AddInputParameter("pi_nLevelOfCareID", lLevelOfCareID);


        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateTreatmentLevelOfCare",
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

    #region plan data management
    public DataSet GetStatGoalDS(BaseMaster BaseMstr,
                                 long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatGoalRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatStrengthDS(BaseMaster BaseMstr,
                                     long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatStrengthRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatBarrierDS(BaseMaster BaseMstr,
                                    long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatBarrierRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatObjectiveDS(BaseMaster BaseMstr,
                                      long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatObjectiveRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatInterventionDS(BaseMaster BaseMstr,
                                         long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatInterventionRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatHomeworkDS(BaseMaster BaseMstr,
                                         long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatHomeworkRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatModalityGroupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatModalityGroupRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatModalityTypeDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatModalityTypeRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetStatModalityDS(BaseMaster BaseMstr,
                                         long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatModalityRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public bool InsertStatGoal(BaseMaster BaseMstr,
                               string strGoalDesc,
                               string strGoalText,
                               long lDiagnosisID,
                               long lIsActive,
                               out long lGoalID)
    {
        // initialize out params
        lGoalID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vGoalDesc", strGoalDesc);
        pList.AddInputParameter("pi_vGoalText", strGoalText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nGoalID", lGoalID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatGoal",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpGoalID = pList.GetItemByName("po_nGoalID");
        lGoalID = dpGoalID.LongParameterValue;

        return true;
    }

    public bool InsertStatStrength(BaseMaster BaseMstr,
                                   string strStrengthDesc,
                                   string strStrengthText,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   out long lStrengthID)
    {
        // initialize out params
        lStrengthID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vStengthDesc", strStrengthDesc);
        pList.AddInputParameter("pi_vStengthText", strStrengthText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nStengthID", lStrengthID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatStrength",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpStrengthID = pList.GetItemByName("po_nStengthID");
        lStrengthID = dpStrengthID.LongParameterValue;

        return true;
    }

    public bool InsertStatBarrier(BaseMaster BaseMstr,
                                   string strBarrierDesc,
                                   string strBarrierText,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   out long lBarrierID)
    {
        // initialize out params
        lBarrierID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vBarrierDesc", strBarrierDesc);
        pList.AddInputParameter("pi_vBarrierText", strBarrierText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nBarrierID", lBarrierID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatBarrier",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpBarrierID = pList.GetItemByName("po_nBarrierID");
        lBarrierID = dpBarrierID.LongParameterValue;

        return true;
    }

    public bool InsertStatObjective(BaseMaster BaseMstr,
                                   string strObjectiveDesc,
                                   string strObjectiveText,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   out long lObjectiveID)
    {
        // initialize out params
        lObjectiveID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vObjectiveDesc", strObjectiveDesc);
        pList.AddInputParameter("pi_vObjectiveText", strObjectiveText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nObjectiveID", lObjectiveID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatObjective",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpObjectiveID = pList.GetItemByName("po_nObjectiveID");
        lObjectiveID = dpObjectiveID.LongParameterValue;

        return true;
    }

    public bool InsertStatIntervention(BaseMaster BaseMstr,
                                   string strInterventionDesc,
                                   string strInterventionText,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   out long lInterventionID)
    {
        // initialize out params
        lInterventionID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vInterventionDesc", strInterventionDesc);
        pList.AddInputParameter("pi_vInterventionText", strInterventionText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nInterventionID", lInterventionID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatIntervention",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpInterventionID = pList.GetItemByName("po_nInterventionID");
        lInterventionID = dpInterventionID.LongParameterValue;

        return true;
    }

    public bool InsertStatHomework(BaseMaster BaseMstr,
                                   string strHomeworkDesc,
                                   string strHomeworkText,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   out long lHomeworkID)
    {
        // initialize out params
        lHomeworkID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vHomeworkDesc", strHomeworkDesc);
        pList.AddInputParameter("pi_vHomeworkText", strHomeworkText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //out params
        pList.AddOutputParameter("po_nHomeworkID", lHomeworkID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatHomework",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpHomeworkID = pList.GetItemByName("po_nHomeworkID");
        lHomeworkID = dpHomeworkID.LongParameterValue;

        return true;
    }

    public bool InsertStatModality(BaseMaster BaseMstr,
                                   long lModalityGroupID,
                                   string strModality,
                                   string strCPT,
                                   string strDuration,
                                   long lModTypeID,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   long lIsMedicalCheck,
                                   out long lModalityID)
    {
        // initialize out params
        lModalityID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nModalityGroupID", lModalityGroupID);
        pList.AddInputParameter("pi_vModality", strModality);
        pList.AddInputParameter("pi_vCPT", strCPT);
        pList.AddInputParameter("pi_vDuration", strDuration);
        pList.AddInputParameter("pi_nModTypeID", lModTypeID);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);
        pList.AddInputParameter("pi_nIsMedicalCheck", lIsMedicalCheck);

        //out params
        pList.AddOutputParameter("po_nModalityID", lModalityID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertStatModality",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        //get the out params
        CDataParameter dpModalityID = pList.GetItemByName("po_nModalityID");
        lModalityID = dpModalityID.LongParameterValue;

        return true;
    }

    public bool UpdateStatGoal(BaseMaster BaseMstr,
                               long lGoalID,
                               string strGoalDesc,
                               string strGoalText,
                               long lDiagnosisID,
                               long lIsActive)
    {
         //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nGoalID", lGoalID);
        pList.AddInputParameter("pi_vGoalDesc", strGoalDesc);
        pList.AddInputParameter("pi_vGoalText", strGoalText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatGoal",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatStrength(BaseMaster BaseMstr,
                               long lStrengthID,
                               string strStrengthDesc,
                               string strStrengthText,
                               long lDiagnosisID,
                               long lIsActive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nStengthID", lStrengthID);
        pList.AddInputParameter("pi_vStengthDesc", strStrengthDesc);
        pList.AddInputParameter("pi_vStengthText", strStrengthText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatStrength",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatBarrier(BaseMaster BaseMstr,
                               long lBarrierID,
                               string strBarrierDesc,
                               string strBarrierText,
                               long lDiagnosisID,
                               long lIsActive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nBarrierID", lBarrierID);
        pList.AddInputParameter("pi_vBarrierDesc", strBarrierDesc);
        pList.AddInputParameter("pi_vBarrierText", strBarrierText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatBarrier",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatObjective(BaseMaster BaseMstr,
                                    long lObjectiveID,
                                    string strObjectiveDesc,
                                    string strObjectiveText,
                                    long lDiagnosisID,
                                    long lIsActive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nObjectiveID", lObjectiveID);
        pList.AddInputParameter("pi_vObjectiveDesc", strObjectiveDesc);
        pList.AddInputParameter("pi_vObjectiveText", strObjectiveText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatObjective",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatIntervention(BaseMaster BaseMstr,
                                    long lInterventionID,
                                    string strInterventionDesc,
                                    string strInterventionText,
                                    long lDiagnosisID,
                                    long lIsActive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nInterventionID", lInterventionID);
        pList.AddInputParameter("pi_vInterventionDesc", strInterventionDesc);
        pList.AddInputParameter("pi_vInterventionText", strInterventionText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatIntervention",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatHomework(BaseMaster BaseMstr,
                                    long lHomeworkID,
                                    string strHomeworkDesc,
                                    string strHomeworkText,
                                    long lDiagnosisID,
                                    long lIsActive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nHomeworkID", lHomeworkID);
        pList.AddInputParameter("pi_vHomeworkDesc", strHomeworkDesc);
        pList.AddInputParameter("pi_vHomeworkText", strHomeworkText);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatHomework",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateStatModality(BaseMaster BaseMstr,
                                   long lModalityID,
                                   long lModalityGroupID,
                                   string strModality,
                                   string strCPT,
                                   string strDuration,
                                   long lModTypeID,
                                   long lDiagnosisID,
                                   long lIsActive,
                                   long lIsMedicalCheck)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nModalityID", lModalityID);
        pList.AddInputParameter("pi_nModalityGroupID", lModalityGroupID);
        pList.AddInputParameter("pi_vModality", strModality);
        pList.AddInputParameter("pi_vCPT", strCPT);
        pList.AddInputParameter("pi_vDuration", strDuration);
        pList.AddInputParameter("pi_nModTypeID", lModTypeID);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nIsActive", lIsActive);
        pList.AddInputParameter("pi_nIsMedicalCheck", lIsMedicalCheck);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateStatModality",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    #endregion

    public DataSet GetDiagnosisSymptomsDS(BaseMaster BaseMstr,
                                          long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetDiagnosisSymptomsRS",
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

    //load a check list of Diagnosis Symptoms
    public void LoadDiagnosisSymptomsCheckList(BaseMaster BaseMstr,
                                     CheckBoxList chklst,
                                     string strSelectedID,
                                     long lDiagID)
    {
        //get the data to load
        DataSet ds = GetDiagnosisSymptomsDS(BaseMstr,
                                            lDiagID);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "SYMPTOM",
                          "SYMPTOM_ID");
    }


    public DataSet GetTreatmentProblemListDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID,
                                       string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_TREATMENT.GetTreatmentProblemListRS",
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

    public DataSet GetDiagnosticIDDS(BaseMaster BaseMstr,
                                       string strPatientID,
                                       long lTreatmentID,
                                       string strEncounterID,
                                       long lProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nProblemID", lProblemID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_TREATMENT.GetDiagnosticIDRS",
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

    public DataSet GetTreatmentProblemSymptomsDS(BaseMaster BaseMstr,
                                                 string strPatientID,
                                                 long lTreatmentID,
                                                 long lProblemID)
                                                 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

      
        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_nProblemID", lProblemID);
                
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                            "PCK_TREATMENT.GetTreatmentProblemSymptomsRS",
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

    public bool InsertTreatmentProblemSymptoms(BaseMaster BaseMstr,
                                               string strPatientID,
                                               long lTreatmentID,
                                               long lProblemID,
                                               long lSymptomID
                                               )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nSymptomID", lSymptomID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.InsertTreatmentProblemSymptoms",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }

    public bool UpdateTreatmentProblemSymptoms(BaseMaster BaseMstr,
                                                string strPatientId,
                                                long lTreatmentID,
                                                long lProblemID,
                                                long lSymptomID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vPatientID", strPatientId);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nSymptomID", lSymptomID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_TREATMENT.UpdateTreatmentProblemSymptoms",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return false;
        }

        return true;
    }


    public DataSet GetReferralSourceDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetReferralSourceRS",
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
