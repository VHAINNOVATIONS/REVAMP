using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ucProblemList : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    
    protected DataSet dsProblemList;
    protected CTreatmentPlan tplan = new CTreatmentPlan();

    protected void Page_Load(object sender, EventArgs e)
    {
        MaintainDivVisibilityStatus();
    }

    public bool Initialize()
    {
        // load dataset with patient's problems for current Treatment_ID
        if (Session["PROBLEMS_LIST_DS"] == null)
        {
            Session["PROBLEMS_LIST_DS"] = tplan.GetTreatmentProblemDS(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedTreatmentID);
        }
        dsProblemList = (DataSet)Session["PROBLEMS_LIST_DS"];
        dsProblemList.Tables[0].DefaultView.Sort = "sort_order ASC";
        dsProblemList.AcceptChanges();

        // Pass on and bind the data to the repeaters that render 
        // the problem list for each Axis(1)
        repProblemsA1.DataSource = GetProblemTable(dsProblemList, 1);
        repProblemsA1.DataBind();
        
        DefaultProblemSelect(dsProblemList);

        return true;
    }

    // Filters the Problems DataSet and return a DataTable
    // only of record for the AxisType passed on as argument
    protected DataTable GetProblemTable(DataSet ds, long lAxisType)
    {
        DataTable dt;
        if (ds != null)
        {
            dt = ds.Tables[0].Clone();
            DataRow[] arrDR = ds.Tables[0].Select("DIAGNOSTIC_AXIS_TYPE = " + lAxisType.ToString());
            foreach (DataRow dr in arrDR)
            {
                DataRow ndr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    ndr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dt.Rows.Add(ndr);
            }
        }
        else
        {
            dt = null;
        }
        return dt;
    }

    protected void ResetProblemSelections(object sender, EventArgs e) 
    {
        Control uc = (Control)this.Parent;
        HiddenField htxtOMProblemID = (HiddenField)uc.FindControl("htxtOMProblemID");
        htxtOMProblemID.Value = "-1";
        RaiseBubbleEvent(sender, EventArgs.Empty);
    }
    protected void lnkbtnProblem_OnClick(object sender, EventArgs e) 
    {
        LinkButton lnkbtn = (LinkButton)sender;
        long lProblemID = Convert.ToInt32(lnkbtn.Attributes["problemid"]);
        Control uc = (Control)this.Parent;
        HiddenField htxtOMProblemID = (HiddenField)uc.FindControl("htxtOMProblemID");
        htxtOMProblemID.Value = lProblemID.ToString();
        RaiseBubbleEvent(sender, EventArgs.Empty);
    }

    protected void DefaultProblemSelect(DataSet ds)
    {
        if(repProblemsA1.Items.Count >0)
        {
            LinkButton lnkBtn = (LinkButton)repProblemsA1.Items[0].FindControl("lnkbtnA1Problem");
            lnkbtnProblem_OnClick(lnkBtn, EventArgs.Empty);
            rblAxes.SelectedValue = "1";
        }

        Control uc = (Control)this.Parent;
        HiddenField htxtOMProblemID = (HiddenField)uc.FindControl("htxtOMProblemID");

        if (ds != null)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("inactive"))
                    {
                        if (Convert.ToInt32(dr["inactive"]) != 1)
                        {
                            if (htxtOMProblemID != null)
                            {
                                htxtOMProblemID.Value = dr["problem_id"].ToString();
                                rblAxes.SelectedValue = dr["diagnostic_axis_type"].ToString();
                                long lAxisSelectedIndex = rblAxes.SelectedIndex;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (htxtOMProblemID != null)
                        {
                            htxtOMProblemID.Value = dr["problem_id"].ToString();
                            rblAxes.SelectedValue = dr["diagnostic_axis_type"].ToString();
                            return;
                        }
                    }
                }
            }
        }
    }

    protected void MaintainDivVisibilityStatus()
    {
        if (rblAxes.SelectedIndex >= 0)
        {
            if (rblAxes.SelectedIndex == 0)
            {
                divAxis1.Style.Add("display", "block");
            }
        }
    }
    
    protected void repProblemsA1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            System.Web.UI.WebControls.LinkButton lnkbtn = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("lnkbtnA1Problem");
            if (!dr.IsNull("problem_id"))
            {
                lnkbtn.Attributes.Add("problemid", dr["problem_id"].ToString());
            }
        }
    }
    
    public void UpdateProblemSummary() 
    {
        return;
    }
}
