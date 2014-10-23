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
/// Summary description for CPatEthnicityRace
/// </summary>
public class CPatEthnicityRace
{
	public CPatEthnicityRace()
	{
	}

    public bool InsertPatientEthnicity(
        BaseMaster BaseMstr,
        string strPatientID,
        long lEthnicityID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nEthnicityID", lEthnicityID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.InsertPatientEthnicity",
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

    public bool InsertPatientRace(
        BaseMaster BaseMstr,
        string strPatientID,
        long lRaceID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nRaceID", lRaceID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.InsertPatientRace",
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

    public bool InsertPatEthRaceSource(
        BaseMaster BaseMstr,
        string strPatientID,
        long lSourceID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nSourceID", lSourceID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.InsertPatEthRaceSource",
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

    public DataSet GetEthnicityDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetEthnicityRS",
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

    public DataSet GetRaceDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetRaceRS",
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

    public DataSet GetEthRaceSourceDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetEthRaceSourceRS",
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

    public DataSet GetPatientEthnicityDS(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetPatientEthnicityRS",
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

    public DataSet GetPatientRaceDS(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetPatientRaceRS",
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

    public DataSet GetPatEthRaceSourceDS(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_PAT_ETHNICITY_RACE.GetPatEthRaceSourceRS",
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

    public bool DeletePatientEthnicity(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.DeletePatientEthnicity",
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

    public bool DeletePatientRace(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.DeletePatientRace",
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

    public bool DeletePatEthRaceSource(
        BaseMaster BaseMstr,
        string strPatientID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = string.Empty;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        BaseMstr.DBConn.ExecuteOracleSP(
            "PCK_PAT_ETHNICITY_RACE.DeletePatEthRaceSource",
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
}
