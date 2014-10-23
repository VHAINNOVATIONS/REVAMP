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
/// Summary description for CIntake
/// </summary>
public class CIntake
{
	public CIntake(){}

    //gets current mids from the stat_module_constant table
    public bool GetCurrentMIDS( BaseMaster BaseMstr,
                                out int nReasonForReferralMID,
                                out int nSuicideMID,
                                out int nMSEMID,
                                out int nSubsequentEventMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //init the outs
        nReasonForReferralMID = -1;
        nSuicideMID = -1;
        nMSEMID = -1;
        nSubsequentEventMID = -1;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetStatModuleConstantRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            CDataUtils util = new CDataUtils();

            nReasonForReferralMID = util.GetIntValueFromDS(ds, "RFR_MODULE");
            nMSEMID = util.GetIntValueFromDS(ds, "MSE_MODULE");
            nSuicideMID = util.GetIntValueFromDS(ds, "SUICIDE_MODULE");
            nSubsequentEventMID = util.GetIntValueFromDS(ds, "RFR_SE_MODULE");
             
            return true;
        }

        return false;
    }

    //get a dataset of patients Demographicss
    public DataSet GetIntakeSummaryDS( BaseMaster BaseMstr,
                                       long lMID,
                                       string strPatientID,
                                       string strEncounterID,
                                       long lEncIntakeID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMID", lMID); 
        plist.AddInputParameter("pi_vPatientID", strPatientID); 
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nEncIntakeID", lEncIntakeID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetIntakeSummaryRS",
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


    //get a dataset of patients Demographicss
    public DataSet GetIntakeModulesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID); 
        
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetIntakeModulesRS2",
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

    //load a check list of Intake Modules
    public void LoadIntakeCheckList(BaseMaster BaseMstr,
                                     CheckBoxList chklst,
                                     string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetIntakeModulesDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "MODULE",
                          "MID");
    }

    //get a dataset of patients modules
    public DataSet GetPatientModulesDS(BaseMaster BaseMstr,
                                       string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID); 

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetPatientModulesRS",
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

    //get a dataset of patients modules
    public DataSet GetScheduledPatientModulesDS(BaseMaster BaseMstr,
                                                string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.GetScheduledPatientModuleRS",
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

    ////load a check list of Intake Modules
    //public void LoadPatientIntakeCheckList(BaseMaster BaseMstr,
    //                                 CheckBoxList chklst)
    //{
    //    //get the data to load
    //    DataSet ds = GetPatientModulesDS(BaseMstr,
    //                                       strPatientID);
    //    //load the combo
    //    CCheckBoxList cl = new CCheckBoxList();
    //    cl.RenderDataSet(BaseMstr,
    //                      ds,
    //                      chklst,
    //                      "MODULE",
    //                      "MID");
    //}

    //insert patient modules...
    public bool InsertPatientModules(BaseMaster BaseMstr,
                                    string strPatientID,
                                    string strAssignProviderID,
                                    DateTime dtDateAssigned,
                                    long lMID,
                                    long lModuleGroup,
                                    string strStatus)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vAssignProviderID", strAssignProviderID);
        plist.AddInputParameter("pi_dDateAssigned", dtDateAssigned);
        plist.AddInputParameter("pi_nMID", lMID);
        plist.AddInputParameter("pi_nModuleGroup", lModuleGroup);
        
        plist.AddInputParameter("pi_vStatus", strStatus);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.InsertPatientModules",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
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

    //insert treatment modules...
    public bool InsertTreatmentProblems(BaseMaster BaseMstr,
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
        
        BaseMstr.DBConn.ExecuteOracleSP( "PCK_INTAKE.InsertTreatmentProblems",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base mastekr status code and status for display
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

    //update patient modules...
    public bool UpdatePatientModules(BaseMaster BaseMstr,
                                     string    strPatientID,
                                     string    strAssignProviderID,
                                     DateTime  dtDateAssigned,
                                     DateTime  dtDateCompleted,
                                     int       iMID,
                                     string    strStatus
                                     )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vAssignProviderID", strAssignProviderID);
        plist.AddInputParameter("pi_dDateAssigned", dtDateAssigned);
        plist.AddInputParameter("pi_dDateCompleted", dtDateCompleted);
        plist.AddInputParameter("pi_nMID", iMID);
        plist.AddInputParameter("pi_vStatus", strStatus);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_USER_ADMIN.UpdatePatientModules",
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

    //delete patient modules...
    public bool DeletePatientModules(BaseMaster BaseMstr,
                                     string strPatientID,
                                     string strStatus)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vStatus", strStatus);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.DeletePatientModules",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
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

    //insert treatment modules...
    public bool UpdateEncounterMSE( BaseMaster BaseMstr,
                                    string strPatientID,
                                    long lTreatmentID,
                                    string strEncounterID,
                                    long lEncounterIntakeID)
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
        plist.AddInputParameter("pi_nEncounterIntakeID", lEncounterIntakeID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.UpdateEncounterMSE",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base mastekr status code and status for display
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


    //get a dataset of Intake Module Groups
    protected DataSet GetModuleGroupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetModuleGroupRS",
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

    //get a dataset of Intake Modules List
    protected DataSet GetModulesListDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetModulesListRS",
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

    public DataSet GetModGroups(BaseMaster BaseMstr) 
    {
        DataSet dsGroups = this.GetModuleGroupDS(BaseMstr);
        DataSet dsMods = this.GetModulesListDS(BaseMstr);
        
        if (dsGroups != null && dsMods != null) 
        {
            dsGroups.Tables[0].TableName = "groups";
            dsGroups.Tables.Add(dsMods.Tables[0].Copy());
            dsGroups.Tables[1].TableName = "modules";
            dsGroups.Relations.Add("modgroups", dsGroups.Tables["groups"].Columns["module_group_id"], dsGroups.Tables["modules"].Columns["module_group_id"], false);
            dsGroups.AcceptChanges();
            return dsGroups;
        }

        return null;
    }

    public bool AssignPatientModules(BaseMaster BaseMstr,
                                    string strPatientID,
                                    string strProviderID,
                                    string strModules)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vProviderID", strProviderID);
        plist.AddInputParameter("pi_vModules", strModules);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.AssignPatientModules",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
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


    //get a dataset of modules groups assigned to the patient
    public DataSet GetPatIntakeAssignedDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetPatIntakeAssignedRS",
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

    public bool InsertEncIntakeScore(BaseMaster BaseMstr,
                                    string strEncounterID,
                                    long lEncIntakeID,
                                    long lMID,
                                    long lScoreType,
                                    double lScore,
                                    long lPCent,
                                    string strInterpret,
                                    long lSeries,
                                    long lIntakeGroup)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nEncIntakeID", lEncIntakeID);
        plist.AddInputParameter("pi_nMID", lMID);
        plist.AddInputParameter("pi_nScoreType", lScoreType);
        plist.AddInputParameter("pi_nScore", lScore);
        plist.AddInputParameter("pi_nPCent", lPCent);
        plist.AddInputParameter("pi_vInterpret", strInterpret);
        plist.AddInputParameter("pi_nSeries", lSeries);
        plist.AddInputParameter("pi_nIntakeGroup", lIntakeGroup);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.InsertEncIntakeScore",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
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

    public DataSet GetPatIntakeScoresDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetPatIntakeScoresRS",
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
	
	//get a dataset of previous responses
    public DataSet GetSymptomsFUResponsesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetSymptomsFUResponsesRS",
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
