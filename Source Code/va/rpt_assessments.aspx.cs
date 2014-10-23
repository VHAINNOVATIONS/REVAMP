using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text;

public partial class rpt_assessments : System.Web.UI.Page
{
    private string EncounterID
    {
        get { return (ViewState[ClientID + "EncounterID"] == null) ? string.Empty : ViewState[ClientID + "EncounterID"].ToString(); }
        set { ViewState[ClientID + "EncounterID"] = value; }
    }

    private long EncounterIntakeID
    {
        get { return (ViewState[ClientID + "EncounterIntakeID"] == null) ? -1 : Convert.ToInt64(ViewState[ClientID + "EncounterIntakeID"]); }
        set { ViewState[ClientID + "EncounterIntakeID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack
            && Request.QueryString[0] != null
            && Request.QueryString[1] != null)
        {
            EncounterID = Request.QueryString[0].ToString();
            EncounterIntakeID = Convert.ToInt64(Request.QueryString[1]);

            CIntake rpt = new CIntake();
            DataSet ds = rpt.GetIntakeReportDS(Master, EncounterID, EncounterIntakeID);
            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0)
                litReport.Text = ds.Tables[0].Rows[0]["REPORT_TEXT"].ToString();
        }

        //show system feedback popup
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "showerror", "parent.window.sysfeedback('" + Master.StatusComment + "')", true);
        }

    }

    protected void OnClickExportCsv(object sender, EventArgs e)
    {
        CIntake rpt = new CIntake();
        DataSet ds = rpt.GetIntakeReportCSVDS(
            Master,
            EncounterID,
            EncounterIntakeID);

        if(ds == null
            || ds.Tables.Count < 1)
            return;

        DataTable dt = ds.Tables[0];

        StringBuilder sb = new StringBuilder();
        foreach (DataColumn dc in dt.Columns)
            sb.Append("\"" + dc.ColumnName + "\",");

        sb.Append(Environment.NewLine);

        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
                sb.Append("\"" + dr[i].ToString() + "\",");

            sb.Append(Environment.NewLine);
        }

        HttpResponse r = HttpContext.Current.Response;
        r.Clear();
        r.ClearContent();
        r.ClearHeaders();
        r.Buffer = true;
        r.AddHeader("Content-Disposition", "attachment;filename=PatientAssessment_" + DateTime.Now.ToString("MMddyy") + ".csv");
        r.ContentType = "text/csv";
        r.Charset = "utf-8";
        r.Write(sb.ToString());
        r.End();
    }
}
