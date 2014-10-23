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
	public CIntake()
    {

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

    //insert initial modules...
    public bool AssignInitialAssessments(BaseMaster BaseMstr, string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.AssignInitialAssessments",
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


    //get a dataset of patient assessment report
    public DataSet GetIntakeReportDS(BaseMaster BaseMstr,
                                       string strEncounterID,
                                       long lEncIntakeID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_nMID", lMID);
        //plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nEncIntakeID", lEncIntakeID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetIntakeReport",
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

    public DataSet GetIntakeReportCSVDS(
        BaseMaster BaseMstr,
        string strEncounterID,
        long lEncIntakeID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nEncIntakeID", lEncIntakeID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_INTAKE.GetIntakeReportCSVRS",
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

    public DataSet GetScoreDataStringDS(BaseMaster BaseMstr,
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
                                           "PCK_INTAKE.GetScoreDataStringRS",
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
