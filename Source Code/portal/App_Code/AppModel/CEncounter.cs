//
//************************************************************************************
//********************************* WARNINIG *****************************************
//************************************************************************************
//
//                This software is protected by copyright laws and international treaties.
//                Unauthorized reproduction or distribution of this software or any portion
//                of it may result in severe civil and criminal penalties and will be
//                prosecuted to the maximum posible extend of the law.
//                For licencing information contact:
//                info@intellicacorp.com
//------------------------------------------------------------------------------------
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
/// Summary description for CEncounter
/// </summary>
public class CEncounter
{
    public CEncounter()
    {

    }

    //wrapping this incase we have to do other things later
    public bool CreateAdminNoteEncounter(BaseMaster BaseMstr,
                                         string strPatientID,
                                         long lTreatmentID,
                                         out string strNewEncID)
    {
        strNewEncID = "";

        if (CreateEncounter(BaseMstr,
                                strPatientID,
                                lTreatmentID,
                                7, //admin note is 7
                                out strNewEncID))
        {
            //do other stuff

            return true;
        }

        return false;
    }

    //wrapping this incase we have to do other things later
    public bool CreateGroupNoteEncounter(BaseMaster BaseMstr,
                                         string strPatientID,
                                         long lTreatmentID,
                                         out string strNewEncID)
    {
        strNewEncID = "";

        if (CreateEncounter(BaseMstr,
                                strPatientID,
                                lTreatmentID,
                                8, //group is 8
                                out strNewEncID))
        {
            //do other stuff

            return true;
        }

        return false;
    }

    //wrapping this incase we have to do other things later
    public bool CreateOtherEncounter(BaseMaster BaseMstr,
                                         string strPatientID,
                                         long lTreatmentID,
                                         out string strNewEncID)
    {
        strNewEncID = "";

        if (CreateEncounter(BaseMstr,
                                strPatientID,
                                lTreatmentID,
                                6, //other is 6
                                out strNewEncID))
        {
            //do other stuff

            return true;
        }

        return false;
    }

    //creates an encounter for the patient/treatment/encounter type/
    public bool CreateEncounter(BaseMaster BaseMstr,
                                 string strPatientID,
                                 long lTreatmentID,
                                 long lEncounterType,
                                 out string strNewEncID)
    {
        strNewEncID = "";

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //get a new encounter id
        string strEncID = BaseMstr.APPMaster.GetNewEncounterID();

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vTreatmentID", lTreatmentID);
        pList.AddInputParameter("pi_vNewEncID", strEncID);

        //0=initial, 6=other, 7=admin note, 8=group note
        pList.AddInputParameter("pi_nEncounterType", lEncounterType);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.CreateEncounter",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            //pass back the ecnounter id
            strNewEncID = strEncID;
            return true;
        }
        else
        {
            return false;
        }
    }

    //update the suicide risk for the encounter
    public bool UpdateSuicideRisk(BaseMaster BaseMstr,
                                   string strPatientID,
                                   string strEncounterID,
                                   string strRiskLevel,
                                   long lRiskLevelID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_vRiskLevel", strRiskLevel);
        pList.AddInputParameter("pi_nRiskLevelID", lRiskLevelID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateSuicideRisk",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            BaseMstr.StatusComment = "Suicide Risk Level Successfully Updated!";
            return true;
        }
        else
        {
            return false;
        }
    }

    //get a dataset of the open treatment
    public bool GetCurrentTreatmentID(BaseMaster BaseMstr,
                                       string strPatientID,
                                       out long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        lTreatmentID = 0;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetCurrentTreatmentRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        CDataUtils utils = new CDataUtils();
        if (lStatusCode == 0)
        {
            lTreatmentID = utils.GetLongValueFromDS(ds, "TREATMENT_ID");
            return true;
        }

        return false;
    }



    //get a dataset of patients encounters
    public DataSet GetPatientEncountersDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                          string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetPatientEncountersRS",
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

    //get a dataset for 1 encounter
    public DataSet GetEncounterDS(BaseMaster BaseMstr,
                                   string strPatientID,
                                   long lTreatmentID,
                                   string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterRS",
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
    public DataSet GetTreatmentEncountersDS(BaseMaster BaseMstr,
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
                                           "PCK_ENCOUNTER.GetTreatmentEncountersRS",
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

    //gets dataset of encounter flags
    public DataSet GetEncounterFlags(BaseMaster BaseMstr,
                                     string strPatientID,
                                     string strEncounterID,
                                     long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterFlags",
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

    //gets dataset of encounter flags
    public DataSet GetEncounterTODODS(BaseMaster BaseMstr,
                                       string strPatientID,
                                        string strEncounterID,
                                       long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterTODORS",
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


    //update dataset of encounter flags
    public bool UpdateEncounterTODO(BaseMaster BaseMstr,
                                     string strPatientID,
                                     string strEncounterID,
                                    long lTODOID,
                                    string strResult,
                                    string strComments)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTODOID", lTODOID);
        plist.AddInputParameter("pi_vResult", strResult);
        plist.AddInputParameter("pi_vComments", strComments);

        //
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateEncounterTODO",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            //BaseMstr.StatusComment = "To Do's - Updated";
            return true;
        }
        else
        {
            return false;
        }
    }

 
    public DataSet GetEncounterListDS(BaseMaster BaseMstr,
                             string strPatientID,
                             long lnSelectedTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lnSelectedTreatmentID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterListRS",
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


    public DataSet GetAllEncounterListDS(BaseMaster BaseMstr,
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
                                           "PCK_ENCOUNTER.GetAllEncounterListRS",
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


    //get a dataset of patients encounters w/Additional Demographics Data
    public DataSet GetEncounterIntakeDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                            long lnTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lnTreatmentID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterIntakeRS",
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

    public DataSet GetAllEncounterIntakeDS(BaseMaster BaseMstr,
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
                                           "PCK_ENCOUNTER.GetAllEncounterIntakeRS",
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



    //get a dataset of patients encounters w/Additional Demographics Data
    public DataSet GetInitialVisitDS(BaseMaster BaseMstr,
                                          string strPatientID,
                                            long lnTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lnTreatmentID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetInitialVisitRS",
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

    //get a dataset of patients encounters w/Additional Demographics Data
    public DataSet GetEncounterMentalStatusDS(BaseMaster BaseMstr,
                                              string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterMentalStatusRS",
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

    public bool InsertEncounterMentalStatus(BaseMaster BaseMstr,
                                  string strEncounterId,
                                  string strPTAffect,
                                  string strPTAppearance,
                                  string strPTAttention,
                                  string strPTAttitude,
                                  string strPTSuicidalEvidence,
                                  string strPTEyeContact,
                                  string strPTInsight,
                                  string strPTJudgement,
                                  string strPTMemory,
                                  string strPTMood,
                                  string strPTThoughtContent,
                                  string strPTThoughtProcess,
                                  long lVisual,
                                  long lAuditory,
                                  long lOlfactory,
                                  long lTactile,
                                  string strOrientationPerson,
                                  string strOrientationPlace,
                                  string strOrientationTime,
                                  string strOrientationContext)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterId);
        plist.AddInputParameter("pi_vPTAffect", strPTAffect);
        plist.AddInputParameter("pi_vPTAppearance", strPTAppearance);
        plist.AddInputParameter("pi_vPTAttention", strPTAttention);
        plist.AddInputParameter("pi_vPTAttitude", strPTAttitude);
        plist.AddInputParameter("pi_vPTSuicidalEvidence", strPTSuicidalEvidence);
        plist.AddInputParameter("pi_vPTEyeContact", strPTEyeContact);
        plist.AddInputParameter("pi_vPTInsight", strPTInsight);
        plist.AddInputParameter("pi_vPTJudgement", strPTJudgement);
        plist.AddInputParameter("pi_vPTMemory", strPTMemory);
        plist.AddInputParameter("pi_vPTMood", strPTMood);
        plist.AddInputParameter("pi_vPTThoughtContent", strPTThoughtContent);
        plist.AddInputParameter("pi_vPTThoughtProcess", strPTThoughtProcess);
        plist.AddInputParameter("pi_nHallucinateVisual", lVisual);
        plist.AddInputParameter("pi_nHallucinateAuditory", lAuditory);
        plist.AddInputParameter("pi_nHallucinateOlfactory", lOlfactory);
        plist.AddInputParameter("pi_nHallucinateTactile", lTactile);
        plist.AddInputParameter("pi_vOrientationPerson", strOrientationPerson);
        plist.AddInputParameter("pi_vOrientationPlace", strOrientationPlace);
        plist.AddInputParameter("pi_vOrientationTime", strOrientationTime);
        plist.AddInputParameter("pi_vOrientationContext", strOrientationContext);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.InsertEncounterMentalStatus",
                                        plist,
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

    //update dataset of encounter flags
    public bool UpdateEncounterMentalStatus(BaseMaster BaseMstr,
                                  string strEncounterId,
                                  string strPTAffect,
                                  string strPTAppearance,
                                  string strPTAttention,
                                  string strPTAttitude,
                                  string strPTSuicidalEvidence,
                                  string strPTEyeContact,
                                  string strPTInsight,
                                  string strPTJudgement,
                                  string strPTMemory,
                                  string strPTMood,
                                  string strPTThoughtContent,
                                  string strPTThoughtProcess,
                                  long lVisual,
                                  long lAuditory,
                                  long lOlfactory,
                                  long lTactile,
                                  string strOrientationPerson,
                                  string strOrientationPlace,
                                  string strOrientationTime,
                                  string strOrientationContext)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterId);
        plist.AddInputParameter("pi_vPTAffect", strPTAffect);
        plist.AddInputParameter("pi_vPTAppearance", strPTAppearance);
        plist.AddInputParameter("pi_vPTAttention", strPTAttention);
        plist.AddInputParameter("pi_vPTAttitude", strPTAttitude);
        plist.AddInputParameter("pi_vPTSuicidalEvidence", strPTSuicidalEvidence);
        plist.AddInputParameter("pi_vPTEyeContact", strPTEyeContact);
        plist.AddInputParameter("pi_vPTInsight", strPTInsight);
        plist.AddInputParameter("pi_vPTJudgement", strPTJudgement);
        plist.AddInputParameter("pi_vPTMemory", strPTMemory);
        plist.AddInputParameter("pi_vPTMood", strPTMood);
        plist.AddInputParameter("pi_vPTThoughtContent", strPTThoughtContent);
        plist.AddInputParameter("pi_vPTThoughtProcess", strPTThoughtProcess);
        plist.AddInputParameter("pi_nHallucinateVisual", lVisual);
        plist.AddInputParameter("pi_nHallucinateAuditory", lAuditory);
        plist.AddInputParameter("pi_nHallucinateOlfactory", lOlfactory);
        plist.AddInputParameter("pi_nHallucinateTactile", lTactile);
        plist.AddInputParameter("pi_vOrientationPerson", strOrientationPerson);
        plist.AddInputParameter("pi_vOrientationPlace", strOrientationPlace);
        plist.AddInputParameter("pi_vOrientationTime", strOrientationTime);
        plist.AddInputParameter("pi_vOrientationContext", strOrientationContext);

        //
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateEncounterMentalStatus",
                                         plist,
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

    //get a dataset of patients encounters w/Additional Demographics Data
    public DataSet GetSuicideRiskAssessmentDS(BaseMaster BaseMstr,
                                          string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetSuicideRiskAssessmentRS",
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



    public bool InsertSuicideRiskAssessment(BaseMaster BaseMstr,
                                  string strEncounterId,
                                  string strPTPlan,
                                  string strPTPlanWhen,
                                  string strPTPlanWhere,
                                  string strPTPlanHow,
                                  string strPTMean,
                                  string strPTRehearsed,
                                  string strPTIntensity,
                                  string strPTAttempts,
                                  string strPTSeverity,
                                  string strPTIntensityComment,
                                  string strPTPlanComment,
                                  string strPTMeanComment,
                                  string strPTRehearsedComment,
                                  string strPTSeverityComment,
                                  string strPTAttemptsComment)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterId);
        plist.AddInputParameter("pi_vPTPlan", strPTPlan);
        plist.AddInputParameter("pi_vPTPlanWhen", strPTPlanWhen);
        plist.AddInputParameter("pi_vPTPlanWhere", strPTPlanWhere);
        plist.AddInputParameter("pi_vPTPlanHow", strPTPlanHow);
        plist.AddInputParameter("pi_vPTMean", strPTMean);
        plist.AddInputParameter("pi_vPTRehearsed", strPTRehearsed);
        plist.AddInputParameter("pi_vPTIntensity", strPTIntensity);
        plist.AddInputParameter("pi_vPTAttempts", strPTAttempts);
        plist.AddInputParameter("pi_vPTSeverity", strPTSeverity);
        plist.AddInputParameter("pi_vPTIntensityComment", strPTIntensityComment);
        plist.AddInputParameter("pi_vPTPlanComment", strPTPlanComment);
        plist.AddInputParameter("pi_vPTMeanComment", strPTMeanComment);
        plist.AddInputParameter("pi_vPTRehearsedComment", strPTRehearsedComment);
        plist.AddInputParameter("pi_vPTSeverityComment", strPTSeverityComment);
        plist.AddInputParameter("pi_vPTAttemptsComment", strPTAttemptsComment);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.InsertSuicideRiskAssessment",
                                        plist,
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

    //update dataset of encounter flags
    public bool UpdateSuicideRiskAssessment(BaseMaster BaseMstr,
                                  string strEncounterId,
                                  string strPTPlan,
                                  string strPTPlanWhen,
                                  string strPTPlanWhere,
                                  string strPTPlanHow,
                                  string strPTMean,
                                  string strPTRehearsed,
                                  string strPTIntensity,
                                  string strPTAttempts,
                                  string strPTSeverity,
                                  string strPTIntensityComment,
                                  string strPTPlanComment,
                                  string strPTMeanComment,
                                  string strPTRehearsedComment,
                                  string strPTSeverityComment,
                                  string strPTAttemptsComment)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterId);
        plist.AddInputParameter("pi_vPTPlan", strPTPlan);
        plist.AddInputParameter("pi_vPTPlanWhen", strPTPlanWhen);
        plist.AddInputParameter("pi_vPTPlanWhere", strPTPlanWhere);
        plist.AddInputParameter("pi_vPTPlanHow", strPTPlanHow);
        plist.AddInputParameter("pi_vPTMean", strPTMean);
        plist.AddInputParameter("pi_vPTRehearsed", strPTRehearsed);
        plist.AddInputParameter("pi_vPTIntensity", strPTIntensity);
        plist.AddInputParameter("pi_vPTAttempts", strPTAttempts);
        plist.AddInputParameter("pi_vPTSeverity", strPTSeverity);
        plist.AddInputParameter("pi_vPTIntensityComment", strPTIntensityComment);
        plist.AddInputParameter("pi_vPTPlanComment", strPTPlanComment);
        plist.AddInputParameter("pi_vPTMeanComment", strPTMeanComment);
        plist.AddInputParameter("pi_vPTRehearsedComment", strPTRehearsedComment);
        plist.AddInputParameter("pi_vPTSeverityComment", strPTSeverityComment);
        plist.AddInputParameter("pi_vPTAttemptsComment", strPTAttemptsComment);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateSuicideRiskAssessment",
                                         plist,
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

    public DataSet GetEncounterASAMPPCDS(BaseMaster BaseMstr,
                                         string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";


        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncounterASAMPPCRS",
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

    public bool InsertEncounterASAMPPC(BaseMaster BaseMstr,
                                       string strEncounterID,
                                       string strAcuteIntox,
                                       string strBioMedCond,
                                       string strEmotionBehavior,
                                       string strReadinessChange,
                                       string strRelapsedContinued,
                                       string strRecoveryLiving,
                                       string strLevelOfCareID
                                       )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_vAcuteIntox", strAcuteIntox);                 //D1
        pList.AddInputParameter("pi_vBioMedCond", strBioMedCond);                 //D2
        pList.AddInputParameter("pi_vEmotionBehavior", strEmotionBehavior);       //D3
        pList.AddInputParameter("pi_vReadinessChange", strReadinessChange);       //D4
        pList.AddInputParameter("pi_vRelapsedContinued", strRelapsedContinued);   //D5
        pList.AddInputParameter("pi_vRecoveryLiving", strRecoveryLiving);         //D6
        pList.AddInputParameter("pi_vLevelOfCareID", strLevelOfCareID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.InsertEncounterASAMPPC",
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

    public bool UpdateEncounterASAMPPC(BaseMaster BaseMstr,
                                       string strEncounterID,
                                       string strAcuteIntox,
                                       string strBioMedCond,
                                       string strEmotionBehavior,
                                       string strReadinessChange,
                                       string strRelapsedContinued,
                                       string strRecoveryLiving,
                                       string strLevelOfCareID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_vAcuteIntox", strAcuteIntox);                 //D1
        pList.AddInputParameter("pi_vBioMedCond", strBioMedCond);                 //D2
        pList.AddInputParameter("pi_vEmotionBehavior", strEmotionBehavior);       //D3
        pList.AddInputParameter("pi_vReadinessChange", strReadinessChange);       //D4
        pList.AddInputParameter("pi_vRelapsedContinued", strRelapsedContinued);   //D5
        pList.AddInputParameter("pi_vRecoveryLiving", strRecoveryLiving);         //D6
        pList.AddInputParameter("pi_vLevelOfCareID", strLevelOfCareID);
        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateEncounterASAMPPC",
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


    public DataSet GetEncounterIntakeFlagsRS(BaseMaster BaseMstr, string strEncounterID, int nMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nMID", nMID);

        //Execute Stored Procedure Call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_ENCOUNTER.GetEncounterIntakeFlagsRS",
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
        else
        {
            return null;
        }
    }

    //--------------------------------------------------------------------
    // Build Diagnoses String for ENCOUNTER.ENCOUNTER_DIAGNOSIS

    public bool UpdateEncounterDiagnosis(BaseMaster BaseMstr,
                                            string strPatientID,
                                            string strEncounterID,
                                            long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateEncounterDiagnosis",
                                        plist,
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

    public DataSet GetAllEncWithProblemsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetAllEncWithProblemsRS",
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


    //--------------------------------------------------------------------
    // Build Patient's Treatment Plan Summary String for ENCOUNTER.TREATMENT_PLAN

    public bool UpdateEncPlanTextDS(BaseMaster BaseMstr,
                                    string strPatientID,
                                    string strEncounterID,
                                    long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateEncPlanTextRS",
                                        plist,
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


    //--------------------------------------------------------------------
    // Update EcounterDate & SessionTime

    public bool UpdateDateSessionTime(BaseMaster BaseMstr,
                                        string strPatientID,
                                        string strEncounterID,
                                        string strEncounterDate,
                                        string strSessionTime)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        DateTime dtEncounterDate = DateTime.Parse(strEncounterDate);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_dtEncDate", dtEncounterDate);
        plist.AddInputParameter("pi_vSessionTime", strSessionTime);

        //Execute Stored Procedure Call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdateDateSessionTime",
                                        plist,
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

    // ----------------------------------------------------------------
    public bool CreateSelfMgntEncounter(BaseMaster BaseMstr, out string strNewEncID)
    {
        strNewEncID = "";

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //get a new encounter id
        string strEncID = BaseMstr.APPMaster.GetNewEncounterID();

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vNewEncID", strEncID);


        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.CreateSelfMgntEncounter",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            //pass back the ecnounter id
            strNewEncID = strEncID;
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataSet GetSelfMgntEncounterDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetSelfMgntEncounterRS",
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

    public DataSet GetModuleGroupStatusDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetModuleGroupStatusRS",
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

    public bool CloseSelfMgntEncounter(BaseMaster BaseMstr, string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);


        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.CloseSelfMgntEncounter",
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

    private DataSet GetEncIntakeIdDS(BaseMaster BaseMstr, long lMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vEncounterID", BaseMstr.SelectedEncounterID);
        plist.AddInputParameter("pi_nMID", lMID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetEncIntakeIdRS",
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

    public long GetEncIntakeId(BaseMaster BaseMstr, long lMID) 
    {
        long lEIID = 1;
        CDataUtils utils = new CDataUtils();
        DataSet ds = GetEncIntakeIdDS(BaseMstr, lMID);
        if (ds != null) 
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lEIID = utils.GetLongValueFromDS(ds, "ENCOUNTER_INTAKE_ID") + 1; 
            }
        }

        return lEIID;
    }

    private DataSet IsModuleAssignedDS(BaseMaster BaseMstr, long lMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nMID", lMID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.IsModuleAssigned",
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

    public bool IsModuleAssigned(BaseMaster BaseMstr, long lMID) 
    {
        CDataUtils utils = new CDataUtils();
        DataSet ds = this.IsModuleAssignedDS(BaseMstr, lMID);
        long lIsAssigned = utils.GetLongValueFromDS(ds, "IS_ASSIGNED");
        return lIsAssigned == 1;
    }

    public bool WriteIntakeResponses(BaseMaster BaseMstr, 
                                    string strEncounterID, 
                                    long lEncIntakeID, 
                                    long lMID, 
                                    long lIntakeGrpID,
                                    string strResponses)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        /*
            pi_vEncounterID in varchar2,
            pi_nEncIntakeID in number,
            pi_nMID         in number,
            pi_nIntakeGrpID in number,
            pi_vResponses   in varchar2,
        */

        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nEncIntakeID", lEncIntakeID);
        pList.AddInputParameter("pi_nMID", lMID);
        pList.AddInputParameter("pi_nIntakeGrpID", lIntakeGrpID);
        pList.AddInputParameter("pi_vResponses", strResponses);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.WriteIntakeResponses",
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

    public bool CompleteModule(BaseMaster BaseMstr,
                               long lMID,
                               long lGrpID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        /*
            pi_vPatientID   in varchar2,
            pi_nMID         in number,
            pi_nGroupID     in number,
        */

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_nMID", lMID);
        pList.AddInputParameter("pi_nGroupID", lGrpID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_INTAKE.CompleteModule",
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

    public String GetRecordForInsert(String[] strInRec, String str)
    {
        String strF1 = null;

        strF1 = strInRec[0];
        strInRec[0] = str + " " + strF1;

        return GetRecordForInsert(strInRec);
    }

    public String GetRecordForInsert(String[] strInRec)
    {
        String strRetVal = null;
        String strF1 = null;
        String strF2 = null;
        String strF3 = null;

        strF1 = strInRec[0];

        if (strInRec.Length > 1)
        {
            strF2 = strInRec[1];
        }

        if (strInRec.Length > 2)
        {
            strF3 = strInRec[2];
        }

        strRetVal += strF1 + "|" + strF2 + "|" + strF3 + "|^";


        return strRetVal;
    }

    public bool UpdatePatientDetails(BaseMaster BaseMstr,
                                    string strEncounterID,
                                    long lHeight,
                                    long lWeight)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        /*
            pi_vEncounterID in varchar2,
            pi_nWeight      in number,
            pi_nHeight      in number,
         */

        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nHeight", lHeight);
        pList.AddInputParameter("pi_nWeight", lWeight);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.UpdatePatientDetials",
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

    public DataSet GetPatientDetailsDS(BaseMaster BaseMstr, string strEncounterID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetPatientDetialsRS",
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


} //END OF CLASS

