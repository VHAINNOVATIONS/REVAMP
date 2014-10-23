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

public partial class template_mgmt : System.Web.UI.Page
{
    protected CTemplate template = new CTemplate();
    public bool bAllowUpdate;
    protected void Page_Load(object sender, EventArgs e)
    {
        CSec usrsec = new CSec();
        bAllowUpdate = (usrsec.GetRightMode(Master, (long)SUATUserRight.DataManagementUR) > (long)RightMode.ReadOnly);

        ucTemplate.BaseMstr = Master;
        ucTemplate.bAllowUpdate = bAllowUpdate;

        if (!IsPostBack) 
        {
            //this page does not require a patient
            Master.ClosePatient();
            
            //get dataset for template tags and tag groups
            DataSet dsTagGroups = template.GetTemplateDataTagGroupDS(Master);
            DataSet dsTags = template.GetTemplateDataTagDS(Master);

            if (dsTagGroups != null && dsTags != null) 
            {
                dsTagGroups.Tables[0].TableName = "groups";
                dsTags.Tables[0].TableName = "tags";

                //copy the "tags" table from dsTags to dsTagGroups
                dsTagGroups.Tables.Add(dsTags.Tables["tags"].Copy());

                //define relation between tables
                dsTagGroups.Relations.Add("taggroup", dsTagGroups.Tables["groups"].Columns["group_id"], dsTagGroups.Tables["tags"].Columns["item_group_id"], false);
                dsTagGroups.AcceptChanges();

                repTempItemGroups.DataSource = dsTagGroups.Tables["groups"];
                repTempItemGroups.DataBind();
            }
        }
    }
}
