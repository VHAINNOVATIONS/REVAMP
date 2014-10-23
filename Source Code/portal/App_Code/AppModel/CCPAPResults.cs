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
/// Summary description for CCPAPResults
/// </summary>
public class CCPAPResults
{
    protected BaseMaster m_BaseMstr;

    public CCPAPResults(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
    }

    public DataSet GetCPAPResults()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CPAP_RESULTS.GetCPAPResultsRS",
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

    public DataSet GetPatientCPAP()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_PATIENT.GetPatientCPAP",
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

    public string GetTxAdherence() 
    {
        string strSeries = String.Empty;
        DataSet ds = this.GetCPAPResults();
        
        if(ds != null){
            foreach (DataTable dt in ds.Tables) {
                foreach (DataRow dr in dt.Rows) {
                    if (!dr.IsNull("THERAPY_DATE")) {
                        DateTime dtm = DateTime.Parse(dr["THERAPY_DATE"].ToString());
                        //string strDate = GetJavascriptTimestamp(dtm).ToString();
                        string strDate = dtm.ToShortDateString();

                        decimal dTotalTime = 0;

                        if (!dr.IsNull("MINUTES_OF_USE"))
                        {
                            dTotalTime = (Convert.ToDecimal(dr["MINUTES_OF_USE"].ToString())) / (60);
                        }

                        string strPlot = String.Format("['{0}', {1}],", strDate, dTotalTime.ToString("0.00"));

                        strSeries += strPlot;
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty(strSeries))
        {
            strSeries = strSeries.Substring(0, strSeries.Length - 1);
            strSeries = "[" + strSeries + "]";
        }
        else
        {
            strSeries = "[]";
        }

        return strSeries;
    }

    public string GetAHI()
    {
        string strSeries = String.Empty;
        DataSet ds = this.GetCPAPResults();

        if (ds != null)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("THERAPY_DATE"))
                    {
                        DateTime dtm = DateTime.Parse(dr["THERAPY_DATE"].ToString());
                        string strDate = dtm.ToShortDateString();

                        decimal dAHI = 0;

                        if (!dr.IsNull("AHI"))
                        {
                            dAHI = (Convert.ToDecimal(dr["AHI"].ToString()));
                        }

                        string strPlot = String.Format("['{0}', {1}],", strDate, dAHI.ToString());

                        strSeries += strPlot;
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty(strSeries))
        {
            strSeries = strSeries.Substring(0, strSeries.Length - 1);
            strSeries = "[" + strSeries + "]";
        }
        else
        {
            strSeries = "[]";
        }

        return strSeries;
    }

    public string GetMaskLeak()
    {
        string strSeries = String.Empty;
        DataSet ds = this.GetCPAPResults();

        if (ds != null)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("THERAPY_DATE"))
                    {
                        DateTime dtm = DateTime.Parse(dr["THERAPY_DATE"].ToString());
                        string strDate = dtm.ToShortDateString();

                        decimal dAvgLeak = 0;

                        if (!dr.IsNull("LEAK"))
                        {
                            dAvgLeak = (Convert.ToDecimal(dr["LEAK"].ToString()));
                        }

                        string strPlot = String.Format("['{0}', {1}],", strDate, dAvgLeak.ToString());

                        strSeries += strPlot;
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty(strSeries))
        {
            strSeries = strSeries.Substring(0, strSeries.Length - 1);
            strSeries = "[" + strSeries + "]";
        }
        else
        {
            strSeries = "[]";
        }

        return strSeries;
    }

    public static long GetJavascriptTimestamp(System.DateTime input)
    {
        string strDate = input.ToShortDateString();
        DateTime dt;
        DateTime.TryParse(strDate, out dt);

        System.TimeSpan span = new System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks);
        System.DateTime time = dt.Subtract(span);
        return (long)(time.Ticks / 10000);
    }

    private DataSet GetQuestionnaireListRS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CPAP_RESULTS.GetQuestionnaireListRS",
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

    public void LoadQuestionnaireCombo(DropDownList cbo) 
    {
        DataSet ds = this.GetQuestionnaireListRS();
        cbo.DataSource = ds;
        cbo.DataValueField = "MID";
        cbo.DataTextField = "MODULE";
        cbo.DataBind();

        cbo.Items.Insert(0, new ListItem("<Select Questionnaire>", "-1"));
    }

}