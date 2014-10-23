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
/// Summary description for CEncounterIntake
/// </summary>
public class CEncounterIntake
{
    public CEncounterIntake()
    {

    }

    //get a dataset of patients encounter intakes
    public DataSet GetResponsesCountDS(BaseMaster BaseMstr,
                                       string strEncounterID,
                                       string strMIDs)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_vMIDs", strMIDs);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_ENCOUNTER_INTAKE.GetResponsesCountRS",
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
