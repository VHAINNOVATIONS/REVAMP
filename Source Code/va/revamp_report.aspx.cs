using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft;
using Newtonsoft.Json;

public partial class revamp_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContents = GetRevampReport();
        Response.Clear();
        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(strContents);
        Response.End();
    }

    protected string GetRevampReport() {
        string value = String.Empty;
        CRevampReport report = new CRevampReport(Master);
        DataSet ds = report.GetReportDS();
        if(ds != null){
            foreach (DataTable dt in ds.Tables) {
                foreach (DataRow dr in dt.Rows) {
                    if (!dr.IsNull("REVAMP_REPORT")) {
                        value = dr["REVAMP_REPORT"].ToString();
                    }
                }
            }
        }
        return value;
    }
}