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
using System.Collections.Generic;
using Newtonsoft.Json;


/// <summary>
/// Summary description for CTrendUtils
/// </summary>
public class CTrendUtils
{
	public CTrendUtils()
	{ }

    protected enum TrendStatus
    { 
        Undefined = 0,
        Done,
        Good,
        Incomplete,
        Bad
    }

    protected string[] cssClases = { String.Empty, String.Empty, "problem-targetmet", "problem-noinfo", "problem-alert" };

    public bool AlterMeasuresDS(DataSet ds) 
    {
        if(ds != null)
        {
            ds.Tables[0].Columns.Add(new DataColumn("CSS_CLASS", typeof(string)));
            foreach (DataRow dr in ds.Tables[0].Rows) 
            {
                dr["CSS_CLASS"] = cssClases[Convert.ToInt32(dr["trend_status"])];
            }

            ds.AcceptChanges();            
            return true;
        }
        return false;
    }
}

public class TrendItem
{
    public long CurrentValue { set; get; }
    public long OriginValue { set; get; }
    public long TargetValue { set; get; }
    public string TargetOperator { set; get; }
}
