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
    }
}
