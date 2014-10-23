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
/// Summary description for CRevampReport
/// </summary>
public class CRevampReport
{
    protected BaseMaster m_BaseMstr { set; get; }

    public CRevampReport(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataSet GetReportDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);
        
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                           "PCK_REVAMP_REPORT.GetReportRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;
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