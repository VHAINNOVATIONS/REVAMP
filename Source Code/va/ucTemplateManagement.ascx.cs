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

public partial class ucTemplateManagement : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    public bool bAllowUpdate { set; get; }
    CTemplate template = new CTemplate();
    CDataUtils utils = new CDataUtils();
    protected DataSet dsTemplates;
    protected DataSet dsTTypes;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region NotPostback
        if (!IsPostBack)
        {
            Session["TEMPLATE_TYPES"] = null;

            //load radio button list for template types
            dsTTypes = template.GetTemplateTypeDS(BaseMstr);
            if (dsTTypes != null)
            {
                rblTemplateType.DataSource = dsTTypes;
                rblTemplateType.DataTextField = "DESCRIPTION";
                rblTemplateType.DataValueField = "TYPE_ID";
                rblTemplateType.DataBind();

                //initially auto select first radio button
                if (rblTemplateType.Items.Count > 0)
                {
                    rblTemplateType.Items[0].Selected = true;
                }
            }

            Load_Template_Groups();
            checkSelectedGroup();
        }
        #endregion

        if (IsPostBack) 
        {
            string strEvtTarget = Request.Params.Get("__EVENTTARGET");
            string strEvtArgs = Request.Params.Get("__EVENTARGUMENT");

            //selected a template group
            if (strEvtTarget.IndexOf("btnSelectGroup") > -1)
            {
                Load_Templates();

                //hide the templates if no template group is selected
                if (htxtTemplateGroupID.Value.Length < 1)
                {
                    divTemplates.Style.Add("display", "none");
                }
                else
                {
                    divTemplates.Style.Add("display", "block");
                }

                checkSelectedGroup();
            }
        }

        SetAttributes();

        if (Session["TEMPLATE_TYPES"] == null)
        {
            DataSet dsTypes = template.GetTemplateTypeDS(BaseMstr);
            Session["TEMPLATE_TYPES"] = dsTypes;
        }

        //hide the templates if no template group is selected
        if (htxtTemplateGroupID.Value.Length < 1)
        {
            divTemplates.Style.Add("display", "none");
        }
        else
        {
            divTemplates.Style.Add("display", "block");
        }
    }

    protected void SetAttributes() 
    {
        btnTemplateAdd.Attributes.Add("onclick","return management.template.validateAdd();");
    }

    protected void Load_Templates() 
    {
        //Get Group's templates
        long lGroupID = -1;
        if (htxtTemplateGroupID.Value.Length > 0) 
        {
            long.TryParse(htxtTemplateGroupID.Value, out lGroupID);
        }

        if (lGroupID > 0)
        {
            dsTemplates = template.GetGroupTemplatesDS(BaseMstr, lGroupID);
            repTemplates.DataSource = dsTemplates;
        }
        else
        {
            repTemplates.DataSource = null;
        }
        repTemplates.DataBind();
    }

    protected void Load_Template_Groups() 
    {
        DataSet dsTempGroups = template.GetTemplateGroupsDS(BaseMstr);
        DataSet ds = new DataSet("TEMPLATE_GROUP");
        ds.Tables.Add(dsTempGroups.Tables[0].Clone());

        if (dsTempGroups != null) {
            foreach (DataTable dt in dsTempGroups.Tables) {
                foreach (DataRow dr in dt.Rows) {
                    if (Convert.ToInt32(dr["READ_ONLY"]) == 0) {
                        DataRow newDR = ds.Tables[0].NewRow();
                        foreach (DataColumn dc in ds.Tables[0].Columns) {
                            newDR[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        ds.Tables[0].Rows.Add(newDR);
                    }
                }
            }
            ds.AcceptChanges();
        }
        repTemplateGroups.DataSource = ds;
        repTemplateGroups.DataBind();
    }

    protected void repTemplates_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            ImageButton btnDel = (ImageButton)e.Item.FindControl("btnDeleteTemplate");
            if (btnDel != null)
            {
                if (!dr.IsNull("TEMPLATE_ID")) 
                {
                    btnDel.Attributes.Add("templateid", dr["TEMPLATE_ID"].ToString());
                }
                btnDel.Attributes.Add("onclick", "return management.template.deleteConfirm();");
            }

            ImageButton btnUpdate = (ImageButton)e.Item.FindControl("btnUpdateTemplate");
            if (btnUpdate != null)
            {
                if (!dr.IsNull("TEMPLATE_ID"))
                {
                    btnUpdate.Attributes.Add("templateid", dr["TEMPLATE_ID"].ToString());
                }
                btnUpdate.Attributes.Add("onclick", "return management.template.validateRow(this);");
            }
            
            DropDownList cbo = (DropDownList)e.Item.FindControl("cboTemplateType");
            if (cbo != null)
            {
                LoadTemplateTypeCombo(cbo);
                if (!dr.IsNull("TYPE_ID"))
                {
                    cbo.SelectedValue = dr["TYPE_ID"].ToString();
                } 
            }
        }
    }

    protected void LoadTemplateTypeCombo(DropDownList cbo) 
    {
        if (Session["TEMPLATE_TYPES"] != null)
        {
            DataSet ds = (DataSet)Session["TEMPLATE_TYPES"];
            if (ds != null)
            {
                cbo.DataSource = ds;
                cbo.DataTextField = "DESCRIPTION";
                cbo.DataValueField = "TYPE_ID";
                cbo.DataBind();

                cbo.Items.Insert(0, new ListItem(" ", "-1"));
            }
        }
    }

    protected void InsertTemplate(object sender, EventArgs e) 
    {
        long lNewTemplateID;
        long lGroupID = -1;

        if (htxtTemplateGroupID.Value.Length > 0) 
        {
            lGroupID = Convert.ToInt32(htxtTemplateGroupID.Value);
        }

        if (lGroupID > 0)
        {
            template.InsertTemplate(BaseMstr,
                                    Convert.ToInt32(rblTemplateType.SelectedValue),
                                    txtTemplateNameAdd.Text.Trim(),
                                    txtTemplateTextAdd.Text.Trim(),
                                    lGroupID,
                                    out lNewTemplateID);
            Load_Templates(); 
        }
    }

    protected void UpdateTemplate(object sender, EventArgs e) 
    {
        ImageButton btnSender = (ImageButton)sender;
        long lTemplateID = Convert.ToInt32(btnSender.Attributes["templateid"]);
        foreach (RepeaterItem ri in repTemplates.Items) 
        {
            TextBox txtName = (TextBox)ri.FindControl("txtTemplateName");
            DropDownList cboType = (DropDownList)ri.FindControl("cboTemplateType");
            TextBox txtTemplate = (TextBox)ri.FindControl("txtTemplateText");
            ImageButton btnUpdate = (ImageButton)ri.FindControl("btnUpdateTemplate");
            long lCurrTemplateID = Convert.ToInt32(btnUpdate.Attributes["templateid"]);
            if (lTemplateID == lCurrTemplateID) 
            {
                template.UpdateTemplate(BaseMstr,
                                        lTemplateID,
                                        Convert.ToInt32(cboType.SelectedValue),
                                        txtName.Text.Trim(),
                                        txtTemplate.Text.Trim());
                break;
            }
        }
        Load_Templates();
    }

    protected void DeleteTemplate(object sender, EventArgs e) 
    {
        ImageButton btn = (ImageButton)sender;
        long lTemplateID = Convert.ToInt32(btn.Attributes["templateid"]);
        bool bDeleteTemplate = template.DiscontinueTemplate(BaseMstr, lTemplateID);
        Load_Templates();
    }

    protected void checkSelectedGroup() 
    {
        foreach (RepeaterItem ri in repTemplateGroups.Items)
        {
            HtmlTableRow tr = (HtmlTableRow)ri.FindControl("trTempGroup");
            tr.Attributes["class"] = String.Empty;
        }

        //mark selected template group's row
        long lGroupID = -1;
        if (htxtTemplateGroupID.Value.Length > 0)
        {
            long.TryParse(htxtTemplateGroupID.Value, out lGroupID);
        }

        if (lGroupID > 0) 
        {
            foreach (RepeaterItem ri in repTemplateGroups.Items) 
            {
                HtmlTableRow tr = (HtmlTableRow)ri.FindControl("trTempGroup");
                long lTempGroupID = Convert.ToInt32(tr.Attributes["tempgroupid"]);
                if (lTempGroupID == lGroupID) 
                {
                    tr.Attributes.Add("class", "selected");
                }
            }
        }
    }

    protected void repTemplateGroups_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            HiddenField htxt = (HiddenField)e.Item.FindControl("htxtGroupName");
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trTempGroup");
            HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdTempGroupEdit");
            HtmlTableCell tdName = (HtmlTableCell)e.Item.FindControl("tdTempGroupName");
            HtmlContainerControl div = (HtmlContainerControl)e.Item.FindControl("divGroupLabel");

            ImageButton btn1 = (ImageButton)e.Item.FindControl("btnDeleteTempGroup");
            ImageButton btn2 = (ImageButton)e.Item.FindControl("btnUpdateTempGroup");

            if (!bAllowUpdate) 
            { 
                //hide the edit controls
                td.Visible = false;
            }

            if (!dr.IsNull("TEMPLATE_GROUP_ID")) 
            {
                tr.Attributes.Add("tempgroupid", dr["TEMPLATE_GROUP_ID"].ToString());
                div.Style.Add("cursor", "pointer");
                div.Attributes.Add("onclick", "management.template.group.selectGroup('" + dr["TEMPLATE_GROUP_ID"].ToString() + "');");

                btn1.Attributes.Add("tempgroupid", dr["TEMPLATE_GROUP_ID"].ToString());
                btn2.Attributes.Add("tempgroupid", dr["TEMPLATE_GROUP_ID"].ToString());

                btn1.Attributes.Add("onclick", "return management.template.deleteConfirm();");
            }

            if (!dr.IsNull("GROUP_NAME")) 
            {
                htxt.Value = dr["GROUP_NAME"].ToString();
            }
            
        }
    }

    protected void InsertTempGroup(object sender, EventArgs e) 
    {
        long lNewTemplateGroupID;

        if (txtTempGroupNameAdd.Text.Trim().Length < 1) 
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Please enter a name for the Group!";
            return;
        }

        if (template.InsertTemplateGroup(BaseMstr,
                                         txtTempGroupNameAdd.Text.Trim(), 
                                         String.Empty, 
                                         out lNewTemplateGroupID)) 
        {
            htxtTemplateGroupID.Value = lNewTemplateGroupID.ToString();
        }

        Load_Template_Groups();
        checkSelectedGroup();
        Load_Templates();

        //hide the templates if no template group is selected
        if (htxtTemplateGroupID.Value.Length < 1)
        {
            divTemplates.Style.Add("display", "none");
        }
        else
        {
            divTemplates.Style.Add("display", "block");
        }
    }

    protected void btnDeleteTempGroup_OnClick(object sender, EventArgs e) 
    {
        ImageButton btn = (ImageButton)sender;
        long lGroupID = Convert.ToInt32(btn.Attributes["tempgroupid"]);

        if (template.DiscontinueTemplateGroup(BaseMstr, lGroupID))
        {
            htxtTemplateGroupID.Value = String.Empty;

            Load_Template_Groups();
            Load_Templates();
            checkSelectedGroup();

            //hide the templates if no template group is selected
            if (htxtTemplateGroupID.Value.Length < 1)
            {
                divTemplates.Style.Add("display", "none");
            }
            else
            {
                divTemplates.Style.Add("display", "block");
            } 
        }
    }

    protected void btnUpdateTempGroup_OnClick(object sender, EventArgs e) 
    {
        ImageButton btn = (ImageButton)sender;
        long lGroupID = Convert.ToInt32(btn.Attributes["tempgroupid"]);
        bool bUpdated = false;

        foreach (RepeaterItem ri in repTemplateGroups.Items) 
        {
            TextBox txt = (TextBox)ri.FindControl("txtGroupName");
            ImageButton imgbtn = (ImageButton)ri.FindControl("btnUpdateTempGroup");

            if (lGroupID == Convert.ToInt32(imgbtn.Attributes["tempgroupid"])) 
            {
                if (txt.Text.Trim().Length > 0)
                {
                    bUpdated = template.UpdateTemplateGroup(BaseMstr, txt.Text.Trim(), String.Empty, lGroupID);
                }
                else
                {
                    BaseMstr.StatusCode = 1;
                    BaseMstr.StatusComment = "Update failed. Group name textbox was left empty.";
                }
            }
        }

        if (bUpdated) 
        {
            Load_Template_Groups();
            Load_Templates();
            checkSelectedGroup();
        }
    }

}
