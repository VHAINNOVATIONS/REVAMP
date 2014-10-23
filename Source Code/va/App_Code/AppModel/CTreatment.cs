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
/// Summary description for CTreatment
/// </summary>
public class CTreatment
{
	public CTreatment()
	{
		
	}

    //get a dataset of patients encounters
    public DataSet GetTreatmentListDS( BaseMaster BaseMstr,
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
                                           "PCK_TREATMENT.GetTreatmentListRS",
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

    public DataSet GetRecordList(BaseMaster BaseMstr,
                                 string strPatientID,
                                 long lnLookupSearchCase)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nSelectCases", lnLookupSearchCase);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetTreatmentsList",
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

    public DataSet GetStatModalityByModalityIDDS(BaseMaster BaseMstr,
                                         long lModalityID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from BaseMstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        pList.AddInputParameter("pi_nModalityID", lModalityID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetStatModalityByModalityIDRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    // STAT MODALITY ENTRIES BY ENCOUNTER TYPE
    // Get STAT_MODALITY
    public DataSet GetAllStatModalityTypesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TREATMENT.GetAllStatModalityTypesRS",
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
