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
using System.Text.RegularExpressions;
using DataAccess;

/// <summary>
/// Summary description for CPatient
/// </summary>
public class CPatient
{
    // 2012-02-15 DS
    // Regular Expression patter for validating emails
    const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    const string MatchFMPSSNPattern = @"^([0-9]{2})/([0-9]{3})-([0-9]{2})-([0-9]{4})$";
    
    //Patient Demographics
    const int cnFirstName                   = 101;
    const int cnMiddleName                  = 102;
    const int cnLastName                    = 103;
    const int cnFMPSSN                      = 104;
    const int cnFMPSSNConfirm               = 105;
    const int cnEDIPN                       = 106;
    const int cnAddress1                    = 107;
    const int cnAddress2                    = 108;
    const int cnCity                        = 109;
    const int cnPostalCode                  = 110;
    const int cnDateOfBirth                 = 111;
    const int cnWorkPhone                   = 112;
    const int cnHomePhone                   = 113;
    const int cnPatEmail                    = 114;
    
    //Patient Sponsor
    const int cnPatSponsorName              = 201;
    const int cnPatSponsorWorkPhone         = 202;
    const int cnPatSponsorHomePhone         = 203;
    const int cnPatSponsorStreetAddress     = 204;
    const int cnPatSponsorCity              = 205;
    const int cnPatSponsorPostalCode        = 206;
    const int cnPatSponsorRelationship      = 207;

    //Patient Emergency Contact
    const int cnPatEmergencyName            = 301;
    const int cnPatEmergencyWorkPhone       = 302;
    const int cnPatEmergencyHomePhone       = 303;
    const int cnPatEmergencyStreetAddress   = 304;
    const int cnPatEmergencyCity            = 305;
    const int cnPatEmergencyPostalCode      = 306;
    const int cnPatEmergencyRelationship    = 307;

    //Patient Military Details
    const int cnPatMilDetailsFMPSSN         = 401;

	public CPatient()
	{
		
	}

    public bool CreateEncounterIntake( BaseMaster BaseMstr, 
                                       string strEncounterID, 
                                       Int32 lTreatmentID,
                                       Int32 lMID,
                                       Int32 lIntakeType,
                                       out Int32 lEncounterIntakeID)
    {
        lEncounterIntakeID = 0;
        
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);

        plist.AddInputParameter("pi_nMID", lMID);
        plist.AddInputParameter("pi_nIntakeType", lIntakeType);
        
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.GetNewEncounterIntakeRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            if (lStatusCode == 0)
            {
                if (ds != null)
                {
                    //loop and set roles on the base master
                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!row.IsNull("ENCOUNTER_INTAKE_ID"))
                            {
                                lEncounterIntakeID = Convert.ToInt32(row["ENCOUNTER_INTAKE_ID"]);
                            }
                        }
                    }

                    return true;
                }
            }
        }


        return false;
    }

    public bool GetLatestEncounterInfo(BaseMaster BaseMstr,
                                        out string strEncounterID,
                                        out Int32 lTreatmentID)
    {
        strEncounterID = "";
        lTreatmentID = 0;
        
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
                                           "PCK_PATIENT.GetLatestEncounterRS",
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
                //loop and set roles on the base master
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (!row.IsNull("ENCOUNTER_ID"))
                        {
                            strEncounterID = Convert.ToString(row["ENCOUNTER_ID"]);
                        }

                        if (!row.IsNull("TREATMENT_ID"))
                        {
                            lTreatmentID = Convert.ToInt32(row["TREATMENT_ID"]);
                        }
                    }
                }

                return true;
            }
        }

        return false;
    }

    public bool InitialEncounter( BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";
        long lInitialEncounter = 0;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddOutputParameter("po_nInitialEnc", lInitialEncounter);

        //
        BaseMstr.DBConn.ExecuteOracleSP( "PCK_PATIENT.InitialEncounter",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            //get the out params
            CDataParameter param = plist.GetItemByName("po_nInitialEnc");
            lInitialEncounter = param.LongParameterValue;

            if (lInitialEncounter > 0)
            {
                return true;
            }
        }

        return false;
    }

    //get a dataset of patients
    public DataSet GetPortalLookupDS(BaseMaster BaseMstr,
                                      string strSearchValue,
                                      long lMilitary
                                      )
    {
        //status info               
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vSearchValue", strSearchValue);
        plist.AddInputParameter("pi_nMilitary", lMilitary);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.GetPortalLookupRS",
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

    public DataSet GetPatientIDRS( BaseMaster BaseMstr,
                                   long lFXUserID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vKey", BaseMstr.Key);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientIDRS",
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
    public DataSet GetPatientDemographicsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientDemographicsRS",
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
    
    //get a dataset of patients Supervisor Input
    public DataSet GetPatientSupervisorInputDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID); 

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientSupervisorInputRS",
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

    //does the patient have a reason for referreral scheduled?
    public bool HasReasonForReferralScheduled (BaseMaster BaseMstr)
    {
        //todo: craig
        return true;

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
                                           "PCK_PATIENT.GetScheduledPatientModuleRS",
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
                //loop and set roles on the base master
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (!row.IsNull("MID"))
                        {
                            long lMID = Convert.ToInt32(row["MID"]);
                            if(lMID == 1 || lMID == 100 || lMID == 1050) //todo: hard coded
                            {
                                return true;
                            }

                        }
                    }
                }
            }
        }
       
        return false;
    }

    //schedule a reason for referral
    public bool ScheduleReasonForReferral( BaseMaster BaseMstr, bool bPatientConsents)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        if (bPatientConsents)
        {
            plist.AddInputParameter("pi_nPatientConsents", 1);
        }
        else
        {
            plist.AddInputParameter("pi_nPatientConsents", 0);
        }





        //
        CDataSet cds = new CDataSet();
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.ScheduleReasonForReferral",
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
        
        return false;
    }

    //complete an assessment
    public bool CompletePatientAssessment(BaseMaster BaseMstr,
                                          string strPatientID,
                                          long lMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nMID", lMID);

        //
        CDataSet cds = new CDataSet();
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.CompletePatientAssessment",
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

        return false;
    }


    //get a dataset of patients Treatment ID
    public DataSet GetPatientTreatmentIdDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientTreatmentIdRS",
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

    //get a dataset of patients Sponsor
    public DataSet GetPatientSponsorDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientSponsorRS",
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

   
    //get a dataset of patients Emergency Contacts
    public DataSet GetPatientEmergencyContactDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientEmergencyContactRS",
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
    public DataSet GetPatientProfileDS(BaseMaster BaseMstr,
                                       string  strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.GetPatientProfileRS",
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

    public bool UpdatePatientDemographics(BaseMaster BaseMstr,
                                        string strFName,
                                        string strMi,
                                        string strLName,
                                        string strSponsorSSN,
                                        string strSSN,
                                        string strGender,
                                        string strDOB,
                                        string strProviderID,
                                        string strAddress1,
                                        string strAddress2,
                                        string strCity,
                                        string strPostalCode,
                                        string strHomePhone,
                                        string strCellPhone,
                                        string strWorkPhone,
                                        string strEmail,
                                        string strStateID,
                                        long lCallPreference,
                                        long lHomePhoneMsg,
                                        long lEmailMsg)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        //Patient Table
        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vFirstName", strFName);
        plist.AddInputParameter("pi_vMI", strMi);
        plist.AddInputParameter("pi_vLastName", strLName);
        plist.AddInputParameter("pi_vSponsorSSN", strSponsorSSN);
        plist.AddInputParameter("pi_vSSN", strSSN);
        plist.AddInputParameter("pi_vGender", strGender);
        plist.AddInputParameter("pi_vDateOfBirth", strDOB);
        plist.AddInputParameter("pi_vProviderID", strProviderID);
        plist.AddInputParameter("pi_vAddress1", strAddress1);
        plist.AddInputParameter("pi_vAddress2", strAddress2);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_vPostal_Code", strPostalCode);
        plist.AddInputParameter("pi_vHomePhone", strHomePhone);
        plist.AddInputParameter("pi_vCellPhone", strCellPhone);
        plist.AddInputParameter("pi_vWorkPhone", strWorkPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_vStateID", strStateID);
        plist.AddInputParameter("pi_nCallPreference", lCallPreference);
        plist.AddInputParameter("pi_nCellPhoneMsg", lHomePhoneMsg);
        plist.AddInputParameter("pi_nEmailMsg", lEmailMsg);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientDemographics",
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

    public bool InsertSupervisorInput(BaseMaster BaseMstr,
                                             int    intSupInputID,
                                             string strSupRelationship,
                                             string strSupRelationComment,
                                             string strWorkPerformance,
                                             string strWorkPerformanceComment,
                                             string strOtherComments,
                                             long   lTreatmentId
                                      )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nSupInputId", intSupInputID);
        plist.AddInputParameter("pi_vRelationship", strSupRelationship);
        plist.AddInputParameter("pi_vRelationComment", strSupRelationComment);
        plist.AddInputParameter("pi_vPerformance", strWorkPerformance);
        plist.AddInputParameter("pi_vPerformanceComment", strWorkPerformanceComment);
        plist.AddInputParameter("pi_vCommentText", strOtherComments);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentId);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.InsertPatientSupervisorInput",
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
            BaseMstr.StatusComment = "Patient Supervisor Input - Saved. ";
            return true;
        }

        return false;
    }


    public bool UpdateSupervisorInput(BaseMaster BaseMstr,
                                      int intSupInputID,
                                      string strSupRelationship,
                                      string strSupRelationComment,
                                      string strWorkPerformance,
                                      string strWorkPerformanceComment,
                                      string strOtherComments
                                      )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_nSupInputId", intSupInputID);
        plist.AddInputParameter("pi_vRelationship", strSupRelationship);
        plist.AddInputParameter("pi_vRelationComment", strSupRelationComment);
        plist.AddInputParameter("pi_vPerformance", strWorkPerformance);
        plist.AddInputParameter("pi_vPerformanceComment", strWorkPerformanceComment);
        plist.AddInputParameter("pi_vCommentText", strOtherComments);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientSupervisorInput",
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
            BaseMstr.StatusComment = "Patient Supervisor Input - Updated. ";
            return true;
        }

        return false;
    }

    public bool InsertPatientSponsor(BaseMaster BaseMstr,
                                        string strName,
                                        string strRelationshipID,
                                        string strAddress1,
                                        string strCity,
                                        string strPostalCode,
                                        string strHomePhone,
                                        string strWorkPhone,
                                        string strEmail,
                                        string strStateID
                                      )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_nRelationshipID", strRelationshipID);
        plist.AddInputParameter("pi_vAddress1", strAddress1);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_vPostalCode", strPostalCode);
        plist.AddInputParameter("pi_vHomePhone", strHomePhone);
        plist.AddInputParameter("pi_vWorkPhone", strWorkPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_nStateID", strStateID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.InsertPatientSponsor",
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
            BaseMstr.StatusComment = "Patient Sponsor - Saved. ";
            return true;
        }

        return false;
    }

    public bool UpdatePatientSponsor(BaseMaster BaseMstr,
                                        string strName,
                                        string strRelationshipID,
                                        string strAddress1,
                                        string strCity,
                                        string strPostalCode,
                                        string strHomePhone,
                                        string strWorkPhone,
                                        string strEmail,
                                        string strStateID
                                     )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_nRelationshipID", strRelationshipID);
        plist.AddInputParameter("pi_vAddress1", strAddress1);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_vPostalCode", strPostalCode);
        plist.AddInputParameter("pi_vHomePhone", strHomePhone);
        plist.AddInputParameter("pi_vWorkPhone", strWorkPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_nStateID", strStateID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientSponsor",
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
            BaseMstr.StatusComment = "Patient Sponsor - Updated. ";
            return true;
        }

        return false;
    }

    public bool InsertPatientEmergencyContact(BaseMaster BaseMstr,
                                       string strName,
                                       int intRelationshipID,
                                       string strAddress1,
                                       string strCity,
                                       string strPostalCode,
                                       string strHomePhone,
                                       string strWorkPhone,
                                       string strEmail,
                                       int intStateID
                                     )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_nRelationshipID", intRelationshipID);
        plist.AddInputParameter("pi_vAddress1", strAddress1);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_vPostal_Code", strPostalCode);
        plist.AddInputParameter("pi_vHomePhone", strHomePhone);
        plist.AddInputParameter("pi_vWorkPhone", strWorkPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_nStateID", intStateID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.InsertPatientEmergencyContact",
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
            BaseMstr.StatusComment = "Patient Contact - Saved. ";
            return true;
        }

        return false;
    }

    public bool UpdatePatientEmergencyContact(BaseMaster BaseMstr,
                                        string strName,
                                        int intRelationshipID,
                                        string strAddress1,
                                        string strCity,
                                        string strPostalCode,
                                        string strHomePhone,
                                        string strWorkPhone,
                                        string strEmail,
                                        int intStateID
                                     )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_nRelationshipID", intRelationshipID);
        plist.AddInputParameter("pi_vAddress1", strAddress1);
        plist.AddInputParameter("pi_vCity", strCity);
        plist.AddInputParameter("pi_vPostal_Code", strPostalCode);
        plist.AddInputParameter("pi_vHomePhone", strHomePhone);
        plist.AddInputParameter("pi_vWorkPhone", strWorkPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_nStateID", intStateID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.UpdatePatientEmergencyContact",
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
            BaseMstr.StatusComment = "Patient Contact - Updated. ";
            return true;
        }

        return false;
    }


    public bool DelIncPatIntakeAssessments(BaseMaster BaseMstr,
                                           string strPatientID
                                           )                                 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
                       
        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.DelIncPatIntakeAssessments",
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

    public bool IncPatIntakeAssessments(BaseMaster BaseMstr,
                                        string strPatientID
                                        )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";
        long lHasIncPatAssessments = 0;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);


        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddOutputParameter("po_nHasIncPatAssessments", lHasIncPatAssessments);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.IncPatIntakeAssessments",
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
            //get the out params
            CDataParameter param = plist.GetItemByName("po_nHasIncPatAssessments");
            lHasIncPatAssessments = param.LongParameterValue;

            if (lHasIncPatAssessments > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool AcceptPatientTransfer(BaseMaster BaseMstr,
                                      string strPatientID,
                                      string strProviderID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vProviderID", strProviderID);

        //get a dataset from the sp call todo change call to executeSP and use connection to
        //switch on type and call correct code
        BaseMstr.DBConn.ExecuteOracleSP("PCK_PATIENT.AcceptPatientTransfer",
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
            BaseMstr.StatusComment = "Accept Transfers - Completed. ";
            return true;
        }

        return false;
    }

    //load a checkbox list of patients awaiting transfer
    public void LoadPatientTransfersCheckboxList(BaseMaster BaseMstr,
                                                 CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = GetPatientTransferDS(BaseMstr,
                                          BaseMstr.APPMaster.UserDMISID);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "LAST_NAME,FIRST_NAME",
                          "PATIENT_ID"
                          );
    }

    public DataSet GetPatientTransferDS(BaseMaster BaseMstr,
                                        string strDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vDMISID", strDMISID);

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PATIENT.getPatientTransferRS",
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

    public bool PatientTransfersAvailable(BaseMaster BaseMstr,
                                          string strDMISID)
    {
        //get patients awaiting transfer
        DataSet ds = GetPatientTransferDS(BaseMstr, strDMISID);

        //if dataset has records, return true
        //if dataset is null or has no records, return false
        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool ValidatePatientDemographicRules(BaseMaster BaseMstr,
                                                int iFieldCode,
                                                string strData,
                                                out string strStatusOut)
    {
        //set the status to good to start
        strStatusOut = "";

        //********************* Patient Demographics *********************

        if ((iFieldCode == cnFirstName) && (string.IsNullOrEmpty(strData)))
        {
             strStatusOut = "First Name field is required.<br />";
        }

        if ((iFieldCode == cnMiddleName) && (string.IsNullOrEmpty(strData)))
        {
             strStatusOut = "Middle Name field is required.<br />";
        }

        if ((iFieldCode == cnLastName) && (string.IsNullOrEmpty(strData)))
        {
             strStatusOut = "Last Name field is required.<br />";
        }

        if ((iFieldCode == cnFMPSSN) && (string.IsNullOrEmpty(strData)))
        {
             strStatusOut = "FMP/SSN Length error. Please check.<br />";
        }

        if ((iFieldCode == cnFMPSSNConfirm) && (string.IsNullOrEmpty(strData)))
        {
             strStatusOut = "FMP/SSN Length error. Please check.<br />";
        }

        //if ((iFieldCode == cnAddress1) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "Address1 cannot be blank.<br />";
        //}

        //if ((iFieldCode == cnAddress2) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "Address2 cannot be blank.<br />";
        //}

        //if ((iFieldCode == cnCity) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "City cannot be blank.<br />";
        //}

        //if ((iFieldCode == cnPostalCode) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "Postal Code cannot be blank.<br />";
        //}

        if ((iFieldCode == cnDateOfBirth) && (string.IsNullOrEmpty(strData)))
        {
              strStatusOut = "You must enter a Date Of Birth.<br />";
        }

        //if ((iFieldCode == cnHomePhone) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "Home Phone cannot be blank.<br />";
        //}

        //if ((iFieldCode == cnWorkPhone) && (string.IsNullOrEmpty(strData)))
        //{
        //    strStatusOut = "Work Phone cannot be blank.<br />";
        //}

        if ((iFieldCode == cnFirstName) && (GetNameSpecialCharCount(strData) > 0))
        {
             strStatusOut = "First Name must not contain Special Characters.<br />";
        }

         if ((iFieldCode == cnMiddleName) &&(GetNameSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Middle Name must not contain Special Characters.<br />";
        }

         if ((iFieldCode == cnLastName) && (GetNameSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Last Name must not contain Special Characters.<br />";
        }

         if ((iFieldCode == cnCity) && (GetCitySpecialCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Special Characters.<br />";
        }

         if ((iFieldCode == cnEDIPN) && (GetSpecialCharCount(strData) > 0))
        {
            strStatusOut = "DoD ID Number must not contain Special Characters.<br />";
        }

        if ((iFieldCode == cnFirstName) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "First Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnMiddleName) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "Middle Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnLastName) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "Last Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnCity) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPostalCode) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Postal Code must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnWorkPhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Work Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnHomePhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Home Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnEDIPN) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "DoD ID Number must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnDateOfBirth) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Date Of Birth must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnFMPSSN) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "FMP/SSN must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnFMPSSNConfirm) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "FMP/SSN Confirm Field must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnDateOfBirth) && (GetDateSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Date Of Birth '/' only Special Character accepted.<br />";
        }

        if (iFieldCode == cnDateOfBirth)
        {
            if (strData.Trim().Length >= 10)
            {
                //validate DOB in patient demographics
                int iMaxYears = 100; //maximum age to be determined
                DateTime dtDOBTest;
                if (DateTime.TryParse(strData, out dtDOBTest)) //checks for valid date input
                {
                    DateTime dtComparison = DateTime.Now.AddDays(-1);
                    int iDateRel = DateTime.Compare(dtDOBTest, dtComparison);
                    if (iDateRel < 0)
                    {
                        if ((DateTime.Now.Year - dtDOBTest.Year) > iMaxYears) //checks for maximum age
                        {
                            strStatusOut = "Date of Birth - Please enter a valid date with the format mm/dd/yyyy.<br/>"; // > max years
                        }
                    }
                    else
                    {
                        strStatusOut = "Date of Birth - Date must be in the past with the format mm/dd/yyyy.<br/>"; //need to be in th past
                    }
                }
                else
                {
                    strStatusOut = "Date of Birth - Please enter a valid date with the format mm/dd/yyyy.<br/>"; //not valid date
                }
            }
            else
            {
                strStatusOut = "Date of Birth - Please enter a valid date with the format mm/dd/yyyy.<br/>"; //not enought chars.
            }
        }

        if ((iFieldCode == cnWorkPhone) && (GetPhoneSpecialCharCount(strData) > 0))
            
        {
            strStatusOut = "Work Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnHomePhone) && (GetPhoneSpecialCharCount(strData) > 0))
            
        {
            strStatusOut = "Home Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnPatEmail) && (strData.Length > 0))
        {
            if(!Regex.IsMatch(strData, MatchEmailPattern))
            {
                strStatusOut = "Email - Please enter a valid email address.<br/>";
            }
        }

        //************************* Patient Sponsor **********************************

        if ((iFieldCode == cnPatSponsorName) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Sponsor Name field is required.<br />";
        }
        
        if ((iFieldCode == cnPatSponsorName) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "Sponsor Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorName) && (GetNameSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Sponsor Name must not contain Special Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorCity) && (GetCitySpecialCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Special Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorCity) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorPostalCode)  && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Postal Code must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorPostalCode) && (GetPostalCodeSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Postal Code '-' only Special Character accepted.<br />";
        }

        if ((iFieldCode == cnPatSponsorWorkPhone) && (GetPhoneSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Work Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnPatSponsorHomePhone) && (GetPhoneSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Home Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnPatSponsorWorkPhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Work Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorHomePhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Home Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatSponsorRelationship) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Sponsor Relationship field is required.<br />";
        }

        //************************* Patient Emergency Contact ***************************

        if ((iFieldCode == cnPatEmergencyName) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Emergency Contact Name field is required.<br />";
        }

        if ((iFieldCode == cnPatEmergencyName) && (GetNumberCharCount(strData) > 0))
        {
             strStatusOut = "Emergency Contact Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyName) && (GetNameSpecialCharCount(strData) > 0))
        {
             strStatusOut = "Emergency Contact Name must not contain Special Characters.<br />";
        }
        
        if ((iFieldCode == cnPatEmergencyCity) && (GetCitySpecialCharCount(strData) > 0))
        {
             strStatusOut = "City must not contain Special Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyCity) && (GetNumberCharCount(strData) > 0))
        {
             strStatusOut = "City must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyPostalCode) && (GetAlphaCharCount(strData) > 0))
        {
             strStatusOut = "Postal Code must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyPostalCode) && (GetPostalCodeSpecialCharCount(strData) > 0))
        {
             strStatusOut = "Postal Code '-' only Special Character accepted.<br />";
        }

        if ((iFieldCode == cnPatEmergencyWorkPhone) && (GetPhoneSpecialCharCount(strData) > 0))
        {
             strStatusOut = "Work Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnPatEmergencyHomePhone) && (GetPhoneSpecialCharCount(strData) > 0))
        {
             strStatusOut = "Home Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        if ((iFieldCode == cnPatEmergencyWorkPhone) && (GetAlphaCharCount(strData) > 0))
        {
             strStatusOut = "Work Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyHomePhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Home Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPatEmergencyRelationship) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Emergency Contact Relationship field is required.<br />";
        }

        //******************** PATIENT MILITARY DETAILS ******************************
        if (iFieldCode == cnPatMilDetailsFMPSSN)
        {
            if (!Regex.IsMatch(strData, MatchFMPSSNPattern))
            {
                strStatusOut = "FMP/SSN - Please enter a valid FMP/SSN.<br/>";
            }
        }

        //false is status is not empty 
        if (strStatusOut != "")
        {
            return false;
        }

        //good
        return true;
    }

    //gets the number of numbers in a string
    public int GetNumberSpacesCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == " ")
            {
                nCount++;
            }
        }

        return nCount;
    }

    //gets the number of numbers in a string
    public int GetNumberCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "0") { nCount++; }
            if (strC == "1") { nCount++; }
            if (strC == "2") { nCount++; }
            if (strC == "3") { nCount++; }
            if (strC == "4") { nCount++; }
            if (strC == "5") { nCount++; }
            if (strC == "6") { nCount++; }
            if (strC == "7") { nCount++; }
            if (strC == "8") { nCount++; }
            if (strC == "9") { nCount++; }
        }

        return nCount;
    }

    public int GetAlphaCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "A") { nCount++; }
            if (strC == "B") { nCount++; }
            if (strC == "C") { nCount++; }
            if (strC == "D") { nCount++; }
            if (strC == "E") { nCount++; }
            if (strC == "F") { nCount++; }
            if (strC == "G") { nCount++; }
            if (strC == "H") { nCount++; }
            if (strC == "I") { nCount++; }
            if (strC == "J") { nCount++; }
            if (strC == "K") { nCount++; }
            if (strC == "L") { nCount++; }
            if (strC == "M") { nCount++; }
            if (strC == "N") { nCount++; }
            if (strC == "O") { nCount++; }
            if (strC == "P") { nCount++; }
            if (strC == "Q") { nCount++; }
            if (strC == "R") { nCount++; }
            if (strC == "S") { nCount++; }
            if (strC == "T") { nCount++; }
            if (strC == "U") { nCount++; }
            if (strC == "V") { nCount++; }
            if (strC == "W") { nCount++; }
            if (strC == "X") { nCount++; }
            if (strC == "Y") { nCount++; }
            if (strC == "Z") { nCount++; }
            if (strC == "a") { nCount++; }
            if (strC == "b") { nCount++; }
            if (strC == "c") { nCount++; }
            if (strC == "d") { nCount++; }
            if (strC == "e") { nCount++; }
            if (strC == "f") { nCount++; }
            if (strC == "g") { nCount++; }
            if (strC == "h") { nCount++; }
            if (strC == "i") { nCount++; }
            if (strC == "j") { nCount++; }
            if (strC == "k") { nCount++; }
            if (strC == "l") { nCount++; }
            if (strC == "m") { nCount++; }
            if (strC == "n") { nCount++; }
            if (strC == "o") { nCount++; }
            if (strC == "p") { nCount++; }
            if (strC == "q") { nCount++; }
            if (strC == "r") { nCount++; }
            if (strC == "s") { nCount++; }
            if (strC == "t") { nCount++; }
            if (strC == "u") { nCount++; }
            if (strC == "v") { nCount++; }
            if (strC == "w") { nCount++; }
            if (strC == "x") { nCount++; }
            if (strC == "y") { nCount++; }
            if (strC == "z") { nCount++; }

        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Permitting dash hyphen in name ???
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetDateSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            if (strC == "-") { nCount++; } 
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; } 
            if (strC == "\"") { nCount++; } 
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            //if (strC == "/") { nCount++; } //Character Allowed
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetPhoneSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            //if (strC == "(") { nCount++; } Character Allowed
            //if (strC == ")") { nCount++; } Character Allowed
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; } 
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetCitySpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetPostalCodeSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetNameSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //1 for male, 0 for female
    public long GetPatientGender(BaseMaster BaseMstr)
    {
        string str = null;
        str = BaseMstr.PatientGender;
        str.Trim();
        str.ToUpper();

        long n = 0;
        if (str == "M")
        {
            n = 1;
        }
        else if (str == "F")
        {
            n = 0;
        }
        else
        {
            n = -1;
        }

        return n;
    }

    public long GetPatientAge(BaseMaster BaseMstr)
    {
        long lAge = BaseMstr.PatientAge;
        return lAge;
    }

    public double GetPatientHeight(BaseMaster BaseMstr)
    {
        long lInches = BaseMstr.PatientHeight;
        double dMeters = lInches * 0.0254;
        return dMeters;
    }

    public double GetPatientWeight(BaseMaster BaseMstr)
    {
        long lPounds = BaseMstr.PatientWeight;
        double dKilos = lPounds * 0.453592;
        return dKilos;
    }
}
