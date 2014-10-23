using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;

/// <summary>
/// Summary description for CMilitaryData
/// </summary>
public class CMilitary
{
    public CMilitary()
    {
    }

    public bool InsertAuxSquadron( BaseMaster BaseMstr,
                                   string strDMISID,
                                   string strAuxDMISID,
                                   long lAuxSquadronID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vAuxDMISID", strAuxDMISID);
        plist.AddInputParameter("pi_nAuxSQID", lAuxSquadronID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.InsertAuxSquadron",
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

    public bool DeleteAuxSquadrdon(BaseMaster BaseMstr,
                                   string strDMISID,
                                   string strAuxDMISID,
                                   long lAuxSquadronID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vAuxDMISID", strAuxDMISID);
        plist.AddInputParameter("pi_nAuxSQID", lAuxSquadronID);
        

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.DeleteAuxSquadron",
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

    /*
    public bool InsertAuxBase(BaseMaster BaseMstr,
                               string strDMISID,
                               string strAuxDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vAuxDMISID", strAuxDMISID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.InsertAuxBase",
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
    */

    public bool DeleteAuxBase(BaseMaster BaseMstr,
                               string strDMISID,
                               string strAuxDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vAuxDMISID", strAuxDMISID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.DeleteAuxBase",
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


    //afsc
    public DataSet GetAFSCDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetAFSCRS",
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

    //fmp
    public DataSet GetFMPDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetFMPRS",
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

    //fmp
    public DataSet GetFMPGroupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetFMPGroupRS",
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
        
    //marital status
    public DataSet GetMaritalStatusDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMaritalStatusRS",
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
            

    //Load the Rank grade CheckBox list
     public DataSet GetRankDS(BaseMaster BaseMstr)
     {
         //status info
         long lStatusCode = -1;
         string strStatusComment = "";

         //create a new parameter list with standard params from basemstr
         CDataParameterList plist = new CDataParameterList(BaseMstr);

         //get and return a dataset
         CDataSet cds = new CDataSet();
         DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                            "PCK_MILITARY.GetMilitaryPayGradeRS",
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
    ////////////////////////////
    
    //Load the Rank grade CheckBox list
     public DataSet GetMilitaryDaysDeployedDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryDaysDeployedRS",
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
    ////////////////////////////

    //get a dataset of majcoms
    public DataSet GetMAJCOMDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMAJCOMRS",
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

        //CDataParameter param = new CDataParameter();
        //param = pListCriteria.GetItemByName("po_vSelectedOption");
        //if (param != null)
        //{
        //    strCriteria = param.StringParameterValue;
        //}
    }

    //get a dataset of majcoms
    public DataSet GetAllMAJCOMDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetAllMAJCOMRS",
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

        //CDataParameter param = new CDataParameter();
        //param = pListCriteria.GetItemByName("po_vSelectedOption");
        //if (param != null)
        //{
        //    strCriteria = param.StringParameterValue;
        //}
    }

    //get a dataset of majcoms
    public DataSet GetMAJCOMBaseDS( BaseMaster BaseMstr,
                                    long lMAJCOMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_nMAJCOMID", lMAJCOMID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMAJCOMBaseRS",
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

        //CDataParameter param = new CDataParameter();
        //param = pListCriteria.GetItemByName("po_vSelectedOption");
        //if (param != null)
        //{
        //    strCriteria = param.StringParameterValue;
        //}
    }


    //get a dataset of squadrons
    public DataSet GetDMISSquadronDS( BaseMaster BaseMstr,
                                      string strDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDMISID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetDMISSquadronRS",
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

    //get a dataset of auxillary DMIS
    public DataSet GetDMISAuxDS(BaseMaster BaseMstr,
                                string strDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDMISID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetDMISAuxRS",
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

    
    //get a dataset of auxillary squadrons
    public DataSet GetAuxSquadronDS( BaseMaster BaseMstr,
                                     string strDIMSID,
                                     string strAuxDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDIMSID);
        plist.AddInputParameter("pi_vAuxDMISID", strAuxDMISID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetDMISAuxSquadronRS",
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

    //get a dataset of squadrons by DMIS ID
    public DataSet GetDMISAllSquadronsDS(BaseMaster BaseMstr,
                                      string strDMISID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDMISID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetDMISAllSquadronsRS",
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

    //get a dataset of squadrons by DMIS ID
    public DataSet GetSquadronsDS(BaseMaster BaseMstr,
                                      string strDMISID,
                                      long lSquadron)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_nSquadronID", lSquadron);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetSquadronsRS",
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

    //get a dataset of Base by MAJCOMID
    public DataSet GetBaseDS(BaseMaster BaseMstr,
                                      string strDMISID,
                                      long lMajCom)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_nMajcomID", lMajCom);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetBaseRS",
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

    //get a single majcom
    public DataSet GetSingleMAJCOMDS(BaseMaster BaseMstr, long lMajComID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_nMAJCOMID", lMajComID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetSingleMAJCOMRS",
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

    //get a dataset of bases for a majcom
    public DataSet GetAllMAJCOMBaseDS(BaseMaster BaseMstr,
                                      long lMAJCOMID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_nMAJCOMID", lMAJCOMID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetAllMAJCOMBaseRS",
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

        return null;
    }
   
    //get a dataset of military services
    public DataSet GetMilitaryServiceDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryServiceRS",
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

        //CDataParameter param = new CDataParameter();
        //param = pListCriteria.GetItemByName("po_vSelectedOption");
        //if (param != null)
        //{
        //    strCriteria = param.StringParameterValue;
        //}
    }

    //get a dataset of military status
    public DataSet GetMilitaryStatusDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryStatusRS",
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

    //get a dataset of military pay grade
    public DataSet GetMilitaryPayGradeDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryPayGradeRS",
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

    //get a dataset of military duty station
    public DataSet GetMilitaryDutyStationDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryDutyStationRS",
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
            
        return null;
    }

    //get a dataset of military duty station name
    public DataSet GetMilitaryDutyStationNameDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryDutyStationNameRS",
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

    //get a dataset of military duty station name
    public DataSet GetMilitarySquadronDS(BaseMaster BaseMstr,
                                         string strSelectedDMIS)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDimsID", strSelectedDMIS);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitarySquadronRS",
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

    //get a dataset of military duty station name
    public DataSet GetMilSquadronDIMSListDS(BaseMaster BaseMstr,
                                         string strSelectedDMIS)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vDimsID", strSelectedDMIS);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilSquadronDIMSListRS",
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

    public bool InsertMajCom(BaseMaster BaseMstr,
                                      //long lMajComID,
                                      string strMajComTitle,
                                      int iActive
                             )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vMajComTitle", strMajComTitle);
        plist.AddInputParameter("pi_nActive", iActive);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.InsertMajCom",
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

    public bool UpdateMajCom(BaseMaster BaseMstr,
                                      long lMajComID,
                                      string strMajComTitle,
                                      int iActive
                             )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nMajComID", lMajComID);
        plist.AddInputParameter("pi_vMajComTitle", strMajComTitle);
        plist.AddInputParameter("pi_nActive", iActive);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_Military.UpdateMajCom",
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

    public bool InsertDMISBase(BaseMaster BaseMstr,
                                      string strDMISID,
                                      string strBaseDesc,
                                      string strBaseShortName,
                                      int iActive,
                                      long lMajComID
                             )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vBaseDesc", strBaseDesc);
        plist.AddInputParameter("pi_vBaseShortName", strBaseShortName);
        plist.AddInputParameter("pi_nActive", iActive);
        plist.AddInputParameter("pi_nMajComID", lMajComID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.InsertDMISBase",
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

    public bool UpdateDMISBase(BaseMaster BaseMstr,
                                      string strDMISID,
                                      string strBaseDesc,
                                      string strBaseShortName,
                                      int iActive,
                                      long lMajComID
                              )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vBaseDesc", strBaseDesc);
        plist.AddInputParameter("pi_vBaseShortName", strBaseShortName);
        plist.AddInputParameter("pi_nActive", iActive);
        plist.AddInputParameter("pi_nMajComID", lMajComID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_Military.UpdateDMISBase",
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

    public bool InsertDMISSquadron(BaseMaster BaseMstr,
                                      string strDMISID,
                                      string strSquadronDesc,
                                      int    iActive
                                   )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_vSquadronDesc", strSquadronDesc);
        plist.AddInputParameter("pi_nActive", iActive);
        
        BaseMstr.DBConn.ExecuteOracleSP("PCK_MILITARY.InsertDMISSquadron",
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

    public bool UpdateDMISSquadron(BaseMaster  BaseMstr,
                                      string   strDMISID,
                                      long     lSquadronID,
                                      string   strSquadronDesc,
                                      int      iActive
                                    )
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vDMISID", strDMISID);
        plist.AddInputParameter("pi_nSquadronID", lSquadronID);
        plist.AddInputParameter("pi_vSquadronDesc", strSquadronDesc);
        plist.AddInputParameter("pi_nActive", iActive);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_Military.UpdateDMISSquadron",
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

    public DataSet GetMilitaryServiceByIdDS(BaseMaster BaseMstr,
                                            int iMilitaryServiceID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vMilitaryServiceID", iMilitaryServiceID);
        
        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryServiceByIdRS",
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

    public DataSet GetMilitaryStatusByIdDS(BaseMaster BaseMstr,
                                            int iMilitaryStatusID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vMilitaryStatusID", iMilitaryStatusID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryStatusByIdRS",
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

    public DataSet GetMilitaryRankByIdDS(BaseMaster BaseMstr,
                                         int iMilitaryRankID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vMilitaryRankID", iMilitaryRankID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMilitaryRankByIdRS",
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

    public DataSet GetMaritalStatusByIdDS(BaseMaster BaseMstr,
                                          int iMaritalStatusID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vMaritalStatusID", iMaritalStatusID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_MILITARY.GetMaritalStatusByIdRS",
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
