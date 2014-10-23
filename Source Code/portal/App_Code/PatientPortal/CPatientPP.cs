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
/// Summary description for CPatientPP
/// </summary>
public class CPatientPP
{
	public CPatientPP()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //-----------------------------------------------------------
    public bool GetDemographicsHTML(BaseMaster BaseMstr, string strPatientID, out string strHTML)
    {
        strHTML = "";

        DataSet ds = GetDemographicsDS(BaseMstr, strPatientID);
        if (ds == null)
            return false;

        string strFirstName = "";
        string strLastName = "";
        string strAge = "";
        string strRace = "";
        string strEthnicity = "";
        string strSex = "";

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull("first_name"))
                {
                    strFirstName = row["first_name"].ToString();
                }
                if (!row.IsNull("last_name"))
                {
                    strLastName = row["last_name"].ToString();
                }
                if (!row.IsNull("age"))
                {
                    strAge = row["age"].ToString();
                }
                if (!row.IsNull("ethnicity"))
                {
                    strEthnicity = row["ethnicity"].ToString();
                    if (strEthnicity == "Unknown")
                    {
                        strEthnicity += "(Ethnicity)";
                    }
                }
                if (!row.IsNull("sex"))
                {
                    strSex = row["sex"].ToString();
                    if (strSex == "Unknown")
                    {
                        strSex += "(Sex)";
                    }
                }
                if (!row.IsNull("race"))
                {
                    strRace = row["race"].ToString();
                    if (strRace == "Unknown")
                    {
                        strRace += "(Race)";
                    }
                }
            }
        }

        strHTML += strLastName + ", " + strFirstName + " ";
        strHTML += strAge + " year old ";
        strHTML += strRace + " ";
        strHTML += strEthnicity + " ";
        strHTML += strSex;

        return true;
    }

    //-----------------------------------------------------------
    public bool GetDemographicsString(BaseMaster BaseMstr, string strPatientID, out string strDemo)
    {
        strDemo = "";

        DataSet ds = GetDemographicsDS(BaseMstr, strPatientID);
        if (ds == null)
            return false;

        string strFirstName = "";
        string strLastName = "";
        string strAge = "";
        string strRace = "";
        string strEthnicity = "";
        string strSex = "";

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull("first_name"))
                {
                    strFirstName = row["first_name"].ToString();
                }
                if (!row.IsNull("last_name"))
                {
                    strLastName = row["last_name"].ToString();
                }
                if (!row.IsNull("age"))
                {
                    strAge = row["age"].ToString();
                }
                if (!row.IsNull("ethnicity"))
                {
                    strEthnicity = row["ethnicity"].ToString();
                    if (strEthnicity == "Unknown")
                    {
                        strEthnicity += "(Ethnicity)";
                    }
                }
                if (!row.IsNull("sex"))
                {
                    strSex = row["sex"].ToString();
                    if (strSex == "Unknown")
                    {
                        strSex += "(Sex)";
                    }
                }
                if (!row.IsNull("race"))
                {
                    strRace = row["race"].ToString();
                    if (strRace == "Unknown")
                    {
                        strRace += "(Race)";
                    }
                }
            }
        }

        strDemo += strLastName + ", " + strFirstName + " ";
        strDemo += strAge + " year old ";
        strDemo += strRace + " ";
        strDemo += strEthnicity + " ";
        strDemo += strSex;

        return true;
    }

    //-----------------------------------------------------------
    public DataSet GetDemographicsDS(BaseMaster BaseMstr, string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);

        // store procedure specific params
        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientDemographics",
                                          pList,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
            return null;

        return ds;
    }

    //-----------------------------------------------------------
    //create a new app_user and psam_user records....
    public bool UpdatePatientModules( BaseMaster BaseMstr,
                                      string strSelectedIDs,
                                      string strUnSelectedIDs )
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";
        
        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        string strSelIDs = strSelectedIDs;
        string strUnSelIDs = strUnSelectedIDs;
        if (strSelIDs == "")
        {
            strSelIDs = "-1";
        }
        if (strUnSelIDs == "")
        {
            strUnSelIDs = "-1";
        }
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID);
        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vSelectedIDs", strSelIDs);
        pList.AddInputParameter("pi_vUnSelectedIDs", strUnSelIDs);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientModules",
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
            return true;
        }

        return false;

    }

    //-----------------------------------------------------------

    //-----------------------------------------------------------
    //When a module is skipped, it needs to be delete
    //-----------------------------------------------------------
    public bool DeletePatientModuleAssigment(BaseMaster BaseMstr, string strPatientID, int nMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("nMID", nMID);


        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.DeletePatientAssignedModule",
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
            return true;
        }

        return false;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientEncounterIntakeDS(BaseMaster BaseMstr, string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
     
        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientEncounterIntakeList",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientDS(BaseMaster BaseMstr,
                                string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        
        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientList",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetModuleGroupDS(BaseMaster BaseMstr, int nModGroupID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_nSelectedID", nModGroupID);
     
        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetModuleGroupList",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientModuleDS( BaseMaster BaseMstr,
                                       string strPatientID,
                                       int nModuleGroupID )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nModuleGroupID", nModuleGroupID);
        //
        //

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientModuleList",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetFlagPatient(BaseMaster BaseMstr, string strClinicID, string strProviderID, string startDate, string endDate)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);

        // store procedure specific params
        pList.AddInputParameter("pi_vClinicID", strClinicID);
        pList.AddInputParameter("pi_vProviderID", strProviderID);
        pList.AddInputParameter("pi_vDateRange_Start", startDate);
        pList.AddInputParameter("pi_vDateRange_End", endDate);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetFlagPatient",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetNonFlagPatient(BaseMaster BaseMstr, string strClinicID, string strProviderID, string startDate, string endDate)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);

        // store procedure specific params
        pList.AddInputParameter("pi_vClinicID", strClinicID);
        pList.AddInputParameter("pi_vProviderID", strProviderID);
        pList.AddInputParameter("pi_vDateRange_Start", startDate);
        pList.AddInputParameter("pi_vDateRange_End", endDate);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetNonFlagPatient",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientIntakes(BaseMaster BaseMstr, string strPatientID, int nReportType)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nReportType", nReportType);

        //
        //

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetPatientIntakes",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetAlertItemsDS(BaseMaster BaseMstr, string strPatientID, int nReportType)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nReportTypeID", nReportType);

        //
        //

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetAlertItemsDS",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetReportDS(BaseMaster BaseMstr, string strEncounterID, string strPatientID, int nReportType)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        //pList.AddInputParameter("pi_nAlerts", nAlerts);
        pList.AddInputParameter("pi_nReportTypeID", nReportType);

        //
        //

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetReportDS",
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
            return null;
        }
        ds.Tables[0].TableName = "REPORT_DS";

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetFlaggedPatientIntakes(BaseMaster BaseMstr, bool flagged, string strPatientID, int nReportType)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_nReportTypeID", nReportType);

        //
        //
        string OracleSP;

        if (flagged == true)
        {
             OracleSP = "PCK_PROVIDER_REPORTS.GetFlagEncounter";
        }
        else
        { 
             OracleSP = "PCK_PROVIDER_REPORTS.GetNonFlagEncounter";
        }

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          OracleSP,
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetAlertFlagsDS(BaseMaster BaseMstr,
                                   string strEncounterID,
                                   int nReportTypeID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nReportTypeID", nReportTypeID);


        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PROVIDER_REPORTS.GetAlertSummary",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientsforClinic(BaseMaster BaseMstr,
                                        string strClinicID,
                                        string strProviderID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);

        // store procedure specific params
        pList.AddInputParameter("pi_vClinicID", strClinicID);
        pList.AddInputParameter("pi_vProviderID", strProviderID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientsforClinic",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetModuleGroupList2(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetModuleGroupList2",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetModulesforGroup(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetModulesforGroup",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public bool BulkUpdatePatientModules(BaseMaster BaseMstr,
                                         DataRow dr)
    {
            //status info
            long lStatusCode = -1;
            string strStatusComment = "";

            //create a new parameter list
            CDataParameterList pList = new CDataParameterList();

            //add params for the DB call
            //
            //in paramaters
            //these will always be passed in to all sp calls
            string strSelIDs = dr["selected_mid"].ToString();
            string strUnSelIDs = String.Empty;
            if (strSelIDs == "")
            {
                strSelIDs = "-1";
            }
            if (strUnSelIDs == "")
            {
                strUnSelIDs = "-1";
            }
            pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
            pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
            pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
            pList.AddInputParameter("pi_vProviderID", dr["provider_id"].ToString());
            pList.AddInputParameter("pi_vPatientID", dr["patient_id"].ToString());
            pList.AddInputParameter("pi_vSelectedIDs", strSelIDs);
            pList.AddInputParameter("pi_vUnSelectedIDs", strUnSelIDs);

            //get a dataset from the sp call
            BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientModules",
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
                return true;
            }

            return false;
    }

    //-----------------------------------------------------------
    public DataSet GetPatModGrpList(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);


        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatModGrpList",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public bool BulkUpdatePatientModules2(BaseMaster BaseMstr,
                                         DataRow dr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        string strSelIDs = dr["selected_mid"].ToString();
        string strUnSelIDs = dr["unselected_mid"].ToString();
        if (strSelIDs == "")
        {
            strSelIDs = "-1";
        }
        if (strUnSelIDs == "")
        {
            strUnSelIDs = "-1";
        }
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_vProviderID", dr["provider_id"].ToString());
        pList.AddInputParameter("pi_vPatientID", dr["patient_id"].ToString());
        pList.AddInputParameter("pi_vSelectedIDs", strSelIDs);
        pList.AddInputParameter("pi_vUnSelectedIDs", strUnSelIDs);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientModules",
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
            return true;
        }

        return false;
    }

    //-----------------------------------------------------------
    public bool BulkUpdatePatientModules3(BaseMaster BaseMstr,
                                        DataRow dr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        string strSelIDs = dr["selected_mid"].ToString();
        string strUnSelIDs = dr["unselected_mid"].ToString();
        if (strSelIDs == "")
        {
            strSelIDs = "-1";
        }
        if (strUnSelIDs == "")
        {
            strUnSelIDs = "-1";
        }

        string strSelModIDs = dr["sel_mod_group"].ToString();
        string strUnSelModIDs = dr["unsel_mod_group"].ToString();
        if (strSelModIDs == "")
        {
            strSelModIDs = "-1";
        }
        if (strUnSelModIDs == "")
        {
            strUnSelModIDs = "-1";
        }

        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_vProviderID", dr["provider_id"].ToString());
        pList.AddInputParameter("pi_vPatientID", dr["patient_id"].ToString());
        pList.AddInputParameter("pi_vSelectedIDs", strSelIDs);
        pList.AddInputParameter("pi_vUnSelectedIDs", strUnSelIDs);

        pList.AddInputParameter("pi_vSelModGroups", strSelModIDs);
        pList.AddInputParameter("pi_vUnselModGroups", strUnSelModIDs);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientModules2",
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
            return true;
        }

        return false;
    }

    //-----------------------------------------------------------
    public bool ClearIncompleteIntake(BaseMaster BaseMstr, string strPatientIDs, string strMIDs)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();


        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        pList.AddInputParameter("pi_vPatientIDs", strPatientIDs);
        pList.AddInputParameter("pi_vSelectedIDs", strMIDs);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.ClearIncompleteIntake",
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
            return true;
        }

        return false;
    }

    //-----------------------------------------------------------
    public DataSet GetPatientEncountersDS(BaseMaster BaseMstr,
                                            string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_ENCOUNTER.GetAllEncounterListRS",
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
            return null;
        }

        return ds;
    }

    //-----------------------------------------------------------
    public DataSet GetEducationFlagsDS(BaseMaster BaseMstr,
                                        string strPatientID, 
                                        string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
        //
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_ENCOUNTER.GetEducationFlagsRS",
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
            return null;
        }

        return ds;
    }

    //----------------------------------------------------------
    public int GetNextPatientModule(CDataConnection conn,
                                    string strSessionID,
                                    string strClientIP,
                                    long lUserID,
                                    string strPatientID,
                                    int nSelectedLanguage,
                                    out long lStatusCode,
                                    out string strStatusComment)
    {
        int nMID = -1;


        DataSet ds = GetPatientModuleDS( conn,
                                         strSessionID,
                                         strClientIP,
                                         lUserID,
                                         strPatientID,
                                         nSelectedLanguage,
                                         out lStatusCode,
                                         out strStatusComment );
        if(ds == null)
        {
            return -1;
        }

        //ds is ordered by oldest first... 
        //so grab the first one and return
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull("mid"))
                {
                    nMID = Convert.ToInt32(row["mid"].ToString());
                    lStatusCode = 0;
                    strStatusComment = "";
                    return nMID;
                }
            }
        }

        return 0;
    }

    //----------------------------------------------------------
    public DataSet GetPatientModuleDS(CDataConnection conn,
                                          string strSessionID,
                                          string strClientIP,
                                          long lUserID,
                                          string strPatientIP,
                                          int nSelectedLanguage,
                                          out long lStatusCode,
                                          out string strStatusComment)
    {
        //status info
        lStatusCode = -1;
        strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call
        //
        //in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", strSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", strClientIP);
        pList.AddInputParameter("pi_nUserID", lUserID);
        pList.AddInputParameter("pi_vPatientID", strPatientIP);
        pList.AddInputParameter("pi_nSelectedLanguage", nSelectedLanguage);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(conn,
                                          "PCK_PATIENT.getPatientModuleRS",
                                          pList,
                                          out lStatusCode,
                                          out strStatusComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return ds;
        }

        return null;
    }

    //----------------------------------------------------------
    // updates the status field of the module assigment:
    // 0 = not done, 1 = completed, 2 = skipped
    //----------------------------------------------------------
    public bool UpdatePatientModuleStatus(CDataConnection conn, 
                                           string strPatID, 
                                           int nMID, 
                                           int nStatus,
                                           out long lStatusCode,
                                           out string strStatusComment )
    {
        //status info
        lStatusCode = -1;
        strStatusComment = "";

        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter( "pi_vPATIENT_ID", strPatID );
        pList.AddInputParameter( "pi_vMID", nMID );
        pList.AddInputParameter( "pi_vSTATUS", nStatus );

        conn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientModuleStatus",
                              pList,
                              out lStatusCode,
                              out strStatusComment);


        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    //----------------------------------------------------------
    // counts the modules that are not completed(e.g. status = 0)
    //----------------------------------------------------------
    public int getPatientModuleCount ( CDataConnection conn, 
                                        string strPatID, 
                                        out long lStatusCode,
                                        out string strStatusComment )
    {
        lStatusCode = 0;
        strStatusComment = "";
        int nMIDCount = 0;
        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vPatientID", strPatID);
        pList.AddOutputParameter("po_nMIDCount", nMIDCount);

        conn.ExecuteOracleSP( "PCK_PATIENT.getPatientModuleCount",
                              pList,
                              out lStatusCode,
                              out strStatusComment);


        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode > 0)
        {
            return -1;
        }

        CDataParameter paramValue = pList.GetItemByName("po_nMIDCount");
        nMIDCount = (int) paramValue.LongParameterValue;

        return nMIDCount;
    }

} // END CLASS
