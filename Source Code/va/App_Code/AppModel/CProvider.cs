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
/// Summary description for CProvider
/// </summary>
public class CProvider
{
	public CProvider()
	{
		
	}

    //get a dataset of States
    public DataSet GetProviderDS(BaseMaster BaseMstr,
                                 string strDIMSID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vDIMSID", strDIMSID);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_PROVIDER.GetProviderRS",
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

    //load a dropdown list of providers for a base
    public void LoadBaseProviderDDL(BaseMaster BaseMstr,
                                    DropDownList cbo,
                                    string strSelectedID,
                                    string strDIMSID)
    {
        //get the data to load
        DataSet ds = GetProviderDS(BaseMstr, strDIMSID);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "NAME",
                          "PROVIDER_ID", 
                          strSelectedID);
    }

}
