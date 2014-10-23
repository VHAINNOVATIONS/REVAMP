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
    
    //Get Encounter Types
    public DataSet GetAllEncounterTypesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER.GetAllEncounterTypesRS",
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

    // Toggle Note's Lock state
    public bool LockNote(BaseMaster BaseMstr,
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
        BaseMstr.DBConn.ExecuteOracleSP("PCK_ENCOUNTER.LockNote",
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



} //END OF CLASS

