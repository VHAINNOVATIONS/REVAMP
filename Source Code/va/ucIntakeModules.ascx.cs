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
using System.Text;
using DataAccess;

public partial class ucIntakeModules : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    public bool bReadOnly { get; set; }

    protected DataSet dsMods;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void LoadModuleGroups() 
    {
        CIntake intake = new CIntake();
        DataSet ds = intake.GetModGroups(BaseMstr);
        dsMods = intake.GetPatientModulesDS(BaseMstr, BaseMstr.SelectedPatientID);

        if (ds != null)
        {
            repGroup.DataSource = ds.Tables["groups"];
        }
        else
        {
            repGroup.DataSource = null;
        }
        Page.DataBind();
    }

    protected void repModules_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            HtmlInputCheckBox chkbx = (HtmlInputCheckBox)e.Item.FindControl("chkModule");
            Label lblMod = (Label)e.Item.FindControl("lblModuleTitle");

            string strMID = dr["MID"].ToString();
            string strGrpID = dr["MODULE_GROUP_ID"].ToString();

            chkbx.Attributes.Add("value", strMID + "|" + strGrpID + "|");
            chkbx.Attributes.Add("groupid", strGrpID);

            lblMod.AssociatedControlID = chkbx.ID;
            lblMod.Attributes.Add("title", dr["DESCRIPTION"].ToString());
            lblMod.Text = dr["MODULE"].ToString();

            if(dsMods != null)
            {
                DataRow[] drs = dsMods.Tables[0].Select("MID = " + strMID + " AND MODULE_GROUP_ID = " + strGrpID);
                if(drs.Length > 0)
                {
                    chkbx.Checked = true;
                    long lStatus = 0;
                    foreach (DataRow dr1 in drs) 
                    {
                        if (!dr1.IsNull("STATUS"))
                        {
                            lStatus = Convert.ToInt32(dr1["STATUS"]);
                        }
                    }
                    chkbx.Disabled = (lStatus > 0);
                }
            }

            if (bReadOnly) 
            {
                chkbx.Disabled = true;
            }
        }
    }

    protected void repGroup_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;
            HtmlInputCheckBox chkbx = (HtmlInputCheckBox)e.Item.FindControl("chkSelectGroup");
            chkbx.Attributes.Add("groupid", dr["module_group_id"].ToString());

            if (bReadOnly)
            {
                chkbx.Disabled = true;
            }
        }
    }
    
    public string GetAssignedModules() 
    {
        string strSelModules = String.Empty;
        string strDupCheck = ",";

        CIntake intake = new CIntake();
        dsMods = intake.GetPatientModulesDS(BaseMstr, BaseMstr.SelectedPatientID);

        foreach (RepeaterItem ri in repGroup.Items) 
        {
            Repeater repMod = (Repeater)ri.FindControl("repModules");
            if (repMod != null) 
            {
                foreach (RepeaterItem itm in repMod.Items) 
                {
                    HtmlInputCheckBox chkbx = (HtmlInputCheckBox)itm.FindControl("chkModule");
                    
                    long lStatus = 0;
                    string strMID = chkbx.Value.Split('|')[0];
                    string strGrpID = chkbx.Value.Split('|')[1];

                    if(dsMods != null)
                    {
                        DataRow[] drs = dsMods.Tables[0].Select("MID = " + strMID + " AND MODULE_GROUP_ID = " + strGrpID);
                        if(drs.Length > 0)
                        {
                            foreach (DataRow dr in drs) 
                            { 
                                if(!dr.IsNull("STATUS"))
                                {
                                    lStatus = Convert.ToInt32(dr["STATUS"]);
                                }
                            }
                        }
                    }

                    if (chkbx.Checked) 
                    {
                        if (strDupCheck.IndexOf("," + chkbx.Value.Substring(0, chkbx.Value.Length - 1)  + ",") < 0)
                        {
                            if (lStatus < 1)
                            {
                                strDupCheck += chkbx.Value.Substring(0, chkbx.Value.Length - 1) + ",";
                                strSelModules += chkbx.Value + "^"; 
                            }
                        }
                    }
                }
            }
        }
        return strSelModules;
    }

    public string GetPatientModules() 
    {
        string strPatientModules = String.Empty;
        CIntake intake = new CIntake();
        DataSet dsMods = intake.GetPatientModulesDS(BaseMstr, BaseMstr.SelectedPatientID);
        if (dsMods != null) 
        {
            foreach (DataTable dt in dsMods.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    strPatientModules += dr["MID"].ToString() + "|";
                    strPatientModules += dr["MODULE_GROUP_ID"].ToString() + "|^";
                }
            }
        }
        return strPatientModules;
    }
}
