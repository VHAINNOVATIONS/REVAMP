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

public class CTreatmentPlan
{
    public CTreatmentPlan()
    {
    }

    public DataSet GetTreatmentProblemDS(BaseMaster BaseMstr,
                                        string strPatientID,
                                        long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_nTreatmentID", lTreatmentID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                          "PCK_TREATMENT_PLAN.GetProblemListRS",
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