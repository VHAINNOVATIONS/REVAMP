using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;


public partial class ucEncounterType : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }

    public long lTypeOfEncounter
    {
        get
        {
            return lEncounterType;
        }
    }

    private long lEncounterType;
    private CDropDownList ddllist = new CDropDownList();
    
    protected void Page_Load(object sender, EventArgs e)

    {
        if (!IsPostBack)
        {
            LoadEncounterTypeDropDownList();  
        }
    }

    public void LoadEncounterTypeDropDownList()
    {
        DataSet ds = new DataSet();

        //Uses Modality Table
        CTreatment trmt = new CTreatment();
        ds = trmt.GetAllStatModalityTypesDS(BaseMstr);

        ddllist.RenderDataSet(BaseMstr, ds, ddlModality, "MODALITY", "STAT_MODALITY_ID", "");

        ddllist.SelectValue(ddlModality, 0); //Default Initial Phone Call
        lEncounterType = Convert.ToInt64(ddlModality.SelectedValue);
        
    }

    protected void ddlModality_SelectedIndexChanged(object sender, EventArgs e)
    {
        lEncounterType = Convert.ToInt64(ddlModality.SelectedValue);
    }
}