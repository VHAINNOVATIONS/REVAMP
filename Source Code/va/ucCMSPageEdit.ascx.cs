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

public partial class ucCMSPageEdit : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }

    public UpdatePanel up;

    protected void Page_Init(object sender, EventArgs e) 
    {
        up = (UpdatePanel)Parent.FindControl("upWrapperUpdatePanel");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetAttributes();
        
        if(!IsPostBack){
            
            Session["IsNewPage"] = true;
            Session["CMS_TEMPLATES_DS"] = null;

            cboPageStatus.SelectedValue = "0";
            htxtSelectedPageID.Value = "-1";

            GetAuthorList();
            GetPagesList();
            GetTemplatesList();
            
            // select current user as the default author
            foreach (ListItem li in cboAuthors.Items) {
                if (li.Value == BaseMstr.FXUserID.ToString()) {
                    li.Selected = true;
                }
            }

        }

        GetEditMode();

        // master save
        //Checking for page postback
        if (IsPostBack)
        {
            string strPostBackControl = Request.Params["__EVENTTARGET"];
            if (strPostBackControl != null)
            {
                //did we do a patient lookup?
                if (strPostBackControl.IndexOf("btnMasterSave") > -1)
                {
                    if (Save())
                    {
                        GetPagesList();
                        SetAttributes();

                        divAddPage.Style.Add("display", "none");
                        divEditPage.Style.Add("display", "inline");

                        ScriptManager.RegisterClientScriptBlock(upEditPage, typeof(string), "saved", "clearStatusDiv(2);", true);
                    }
                }
            }
        }

    }

    protected void SetAttributes(){
        foreach (ListItem rad in rblPageEditMode.Items) 
        {
            rad.Attributes.Add("onclick", "cms.page.setEditMode(this);");
        }

        cboAuthorsPopup.Attributes.Add("onchange", "cms.page.filterPages();");
        cboStatusPopup.Attributes.Add("onchange", "cms.page.filterPages();");

        btnSelectPage.Attributes.Add("onclick", "return cms.page.removeEditorInstance();");
        btnSelTemplate.Attributes.Add("onclick", "return cms.page.removeEditorInstance();");

        txtPageTitle.Attributes.Add("onfocus", "return cms.page.forceHideDropdowns();");
        cboAuthors.Attributes.Add("onfocus", "return cms.page.forceHideDropdowns();");
        cboPageStatus.Attributes.Add("onfocus", "return cms.page.forceHideDropdowns();");
    }

    protected void GetEditMode() {
        switch (Convert.ToInt32(rblPageEditMode.SelectedValue)) { 
            case 0:
                divAddPage.Style.Add("display", "block");
                divEditPage.Style.Add("display", "none");
                break;

            case 1:
                divAddPage.Style.Add("display", "none");
                divEditPage.Style.Add("display", "block");
                break;
        }
    }

    protected void GetAuthorList() {
        CContentManagement cms = new CContentManagement(BaseMstr);
        DataSet dsAuthors = cms.GetAuthorDS();

        //load combo for the main screen
        cboAuthors.DataTextField = "NAME";
        cboAuthors.DataValueField = "FX_USER_ID";
        cboAuthors.DataSource = dsAuthors;
        cboAuthors.DataBind();
        cboAuthors.ClearSelection();

        //load combo for the popup
        cboAuthorsPopup.DataTextField = "NAME";
        cboAuthorsPopup.DataValueField = "FX_USER_ID";
        cboAuthorsPopup.DataSource = dsAuthors;
        cboAuthorsPopup.DataBind();

        cboAuthorsPopup.Items.Insert(0, new ListItem("-View All-", "-1"));
        cboAuthorsPopup.SelectedIndex = 0;
    }

    protected void GetPagesList() {
        CContentManagement cms = new CContentManagement(BaseMstr);
        DataSet dsPages = cms.GetPagesListDS();

        repPagesList.DataSource = dsPages;
        repPagesList.DataBind();
    }

    protected void GetTemplatesList() 
    { 
        if(Session["CMS_TEMPLATES_DS"]==null)
        {
            CContentManagement cms = new CContentManagement(BaseMstr);
            DataSet ds = cms.GetTemplatesListDS();
            Session["CMS_TEMPLATES_DS"] = ds;
        }

        DataSet dsTemplates = (DataSet)Session["CMS_TEMPLATES_DS"];

        cboTemplates.DataSource = dsTemplates;
        cboTemplates.DataTextField = "TITLE";
        cboTemplates.DataValueField = "PAGE_ID";
        cboTemplates.DataBind();

        cboTemplates.Items.Insert(0, new ListItem("-- Select Template --", "-1"));
    }

    protected void SelectPage(object sender, EventArgs e) {
        CContentManagement cms = new CContentManagement(BaseMstr);
        long lAuthorID = 0;

        if (!String.IsNullOrEmpty(htxtPreSelPageID.Value)) {

            long lPageID = Convert.ToInt32(htxtPreSelPageID.Value);

            DataSet dsPage = cms.GetPageContentsDS(lPageID);
            foreach (DataTable dt in dsPage.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    if (!dr.IsNull("TITLE"))
                    {
                        txtPageTitle.Text = dr["TITLE"].ToString();
                    }

                    if (!dr.IsNull("AUTHOR_ID"))
                    {
                        cboAuthors.SelectedValue = dr["AUTHOR_ID"].ToString();
                        lAuthorID = Convert.ToInt32(dr["AUTHOR_ID"]);
                    }

                    if (!dr.IsNull("STATUS"))
                    {
                        cboPageStatus.SelectedValue = dr["STATUS"].ToString();
                    }

                    if (!dr.IsNull("CONTENTS"))
                    {
                        this.txtPageContents.Text = dr["CONTENTS"].ToString();
                    }
                }
            }

            divStatus.InnerHtml = string.Empty;

            ScriptManager.RegisterStartupScript(upSelPage, typeof(string), "hideEditPopup", "winSelectPage.hide();", true);
            
            htxtPreSelPageID.Value = String.Empty;
            htxtSelectedPageID.Value = lPageID.ToString();

            if (lPageID > 0) 
            {
                divEditControls.Style.Add("display","block");
                if (lAuthorID == BaseMstr.FXUserID || BaseMstr.APPMaster.HasUserRight(12582912)) // CMS Editor + CMS Publisher
                {
                    spDeletePage.Style.Add("display", "inline");
                }
                else
                {
                    spDeletePage.Style.Add("display", "none");
                }
            }
        }
    }

    protected void DeletePage(object sender, EventArgs e) 
    {
        CContentManagement cms = new CContentManagement(BaseMstr);

        long lPageID = -1;
        long.TryParse(htxtSelectedPageID.Value, out lPageID);
        if (lPageID > 0) 
        {
            if (cms.DeleteCMSPage(lPageID)) 
            {
                GetPagesList();
                
                htxtPreSelPageID.Value = "-1";
                htxtSelectedPageID.Value = "-1";

                divEditControls.Style.Add("display", "block");
                divAddPage.Style.Add("display", "block");
                divEditPage.Style.Add("display", "none");
                spDeletePage.Style.Add("display", "none");

                rblPageEditMode.SelectedIndex = 0;


                txtPageTitle.Text = String.Empty;
                cboAuthors.SelectedValue = BaseMstr.FXUserID.ToString();
                cboPageStatus.SelectedValue = "0";
                this.txtPageContents.Text = String.Empty;
            }
        }
    }

    protected void repPagesList_OnItemDataBound(object sender, RepeaterItemEventArgs e) {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;
            Image img = (Image)e.Item.FindControl("imgStatus");

            long lstatus = 0;
            if(!dr.IsNull("STATUS")){
                lstatus = Convert.ToInt32(dr["STATUS"]);
            }

            switch (lstatus) { 
                case 0:
                    img.ImageUrl = "Images/page_error.png";
                    break;

                case 1:
                    img.ImageUrl = "Images/page.png";
                    break;
            }
        }
    }

    protected bool ValidateContents() {
        
        if(String.IsNullOrEmpty(txtPageTitle.Text)){
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Please enter some text in the Title textbox.";
            return false;
        }
        
        return true;
    }

    protected bool Save() {
        bool bSave = false;

        CContentManagement cms = new CContentManagement(BaseMstr);

        if (ValidateContents())
        {
            int iSelectedIndex = rblPageEditMode.SelectedIndex; //delete this after testing

            //insert page
            if (rblPageEditMode.SelectedIndex == 0)
            {
                long lAuthorID = Convert.ToInt32(cboAuthors.SelectedValue);
                string strAuthorName = cboAuthors.SelectedItem.Text;
                
                long lPageID = -1;
                bool bInsert = cms.InsertPage(lAuthorID,
                                            txtPageTitle.Text.Trim(),
                                            txtPageContents.Text,
                                            Convert.ToInt32(cboPageStatus.SelectedValue),
                                            out lPageID);
                if (bInsert)
                {
                    bSave = true;
                    
                    htxtSelectedPageID.Value = lPageID.ToString();
                    rblPageEditMode.SelectedIndex = 1;

                    divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;Page contents were saved!</font>";
                }

                ShowSysFeedback();
                return bSave;
            }

            //update page
            if (rblPageEditMode.SelectedIndex == 1)
            {
                long lPageID = Convert.ToInt32(htxtSelectedPageID.Value);
                bool bUpdate = cms.EditPage(lPageID, 
                                            Convert.ToInt32(cboAuthors.SelectedValue),
                                            txtPageTitle.Text.Trim(),
                                            txtPageContents.Text,
                                            Convert.ToInt32(cboPageStatus.SelectedValue));
                if (bUpdate)
                {
                    bSave = true;

                    divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;Page contents were saved!</font>";
                }

                ShowSysFeedback();
                return bSave;
            } 
        }

        return false;
    }

    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode > 0 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.BaseMstr.FindControl("divSysFeedback");
            div.InnerHtml = BaseMstr.StatusComment;
            ScriptManager.RegisterStartupScript(upEditPage, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            BaseMstr.ClearStatus();
        }
    }

    protected void cboTemplates_OnSelectedIndexChanged(object sender, EventArgs e) 
    {
        DropDownList cbo = (DropDownList)sender;
        if (cbo.SelectedIndex < 1)
        {
            litTemplateContents.Text = String.Empty;
        }
        else
        {
            string strPageID = cbo.SelectedValue;
            DataSet ds = (DataSet)Session["CMS_TEMPLATES_DS"];
            DataRow[] drTemplates = ds.Tables[0].Select("PAGE_ID = " + strPageID);
            foreach(DataRow dr in drTemplates)
            {
                if (!dr.IsNull("CONTENTS")) 
                {
                    string strTemplate = dr["CONTENTS"].ToString();
                    litTemplateContents.Text = strTemplate;
                }
            }
        }

        winSelectTemplate.Show();
    }

    protected void btnSelTemplate_OnClick(object sender, EventArgs e) 
    {
        if (cboTemplates.SelectedIndex < 1)
        {
            winSelectTemplate.Hide();
            return;
        }
        else 
        {
            txtPageContents.Text = litTemplateContents.Text;
            cboTemplates.SelectedIndex = 0;
            litTemplateContents.Text = String.Empty;
            winSelectTemplate.Hide();
        }
    }
}