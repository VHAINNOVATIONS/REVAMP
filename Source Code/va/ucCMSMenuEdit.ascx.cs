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
using System.Text.RegularExpressions;

public partial class ucCMSMenuEdit : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }
    protected CContentManagement m_cms;
    protected CDataUtils utils = new CDataUtils();

    protected void Page_Load(object sender, EventArgs e)
    {
        m_cms = new CContentManagement(BaseMstr);

        if (!IsPostBack){
                LoadPagesListDD();
                LoadUserRightsChkbx();
        }

        if (IsPostBack)
        {
            if (BaseMstr.OnMasterSAVE())
            {
                if (Save()) {
                    divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;Menu item was saved!</font>";
                    ScriptManager.RegisterClientScriptBlock((UpdatePanel)this.Parent.FindControl("upWrapperUpdatePanel"), typeof(string), "saved", "clearStatusDiv(1.2);", true);
                }
            } 
        }

        InitMenuEdit();
    }

    public void InitMenuEdit() {
        RenderMenuTree();
        btnDeleteMenuItem.Attributes.Add("onclick", "return cms.menu.confirmDeleteMenuItem();");
        txtSortOrder.Attributes.Add("onkeyup", "cms.onlyNumbers(this);");
    }
    
    protected bool ValidateMenuItm() {
        if(String.IsNullOrEmpty(txtMenuTitle.Text)){
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Please enter some text in the Title textbox.";
            return false;
        }
        return true;
    }
    
    protected bool Save() {
        bool bSave = false;

        long lSortOrder = -1;
        if (Regex.Replace(txtSortOrder.Text, "\\D", String.Empty).Length > 0)
        {
            lSortOrder = Convert.ToInt32(txtSortOrder.Text);
        }

        CContentManagement cms = new CContentManagement(BaseMstr);

        if (ValidateMenuItm())
        {
            if (!String.IsNullOrEmpty(htxtEditMode.Value) || htxtCurrentID.Value != "0")
            {
                long lMode = Convert.ToInt32(htxtEditMode.Value);

                //Insert mode
                if (lMode == 1)
                {
                    if (cms.InsertMenuItem(txtMenuTitle.Text.Trim(),
                            Convert.ToInt32(htxtCurrentID.Value),
                            Convert.ToInt32(cboTargetPage.SelectedValue),
                            Convert.ToInt32(htxtUserRights.Value),
                            Convert.ToInt32(rblTargetPortal.SelectedValue),
                            lSortOrder))
                    {
                                
                        bSave = true;
                    }
                }

                //update mode
                if (lMode == 2)
                {
                    if (cms.UpdatetMenuItem(Convert.ToInt32(htxtCurrentID.Value),
                        txtMenuTitle.Text.Trim(),
                        Convert.ToInt32(htxtParentID.Value),
                        Convert.ToInt32(cboTargetPage.SelectedValue),
                        Convert.ToInt32(htxtUserRights.Value),
                        Convert.ToInt32(rblTargetPortal.SelectedValue),
                        lSortOrder))
                    {
                            
                        bSave = true;
                    }
                }
            } 
        }

        //clear hidden fields
        htxtCurrentID.Value = String.Empty;
        htxtParentID.Value = String.Empty;
        htxtUserRights.Value = String.Empty;
        htxtEditMode.Value = String.Empty;

        txtMenuTitle.Text = String.Empty;

        //sysfeedback
        ShowSysFeedback();

        return bSave;
    }
    
    protected void RenderMenuTree() {
        CContentManagement cms = new CContentManagement(BaseMstr);
        cms.RenderTreePanel(divMenuTree);

        DataSet dsMenus = cms.GetALLMenuItems();
        htxtMenuData.Value = utils.GetJSONString(dsMenus);
    }

    protected void LoadPagesListDD() { 
        CContentManagement cms = new CContentManagement(BaseMstr);
        DataSet dsPages = cms.GetPagesListDS();

        cboTargetPage.DataSource = dsPages;
        cboTargetPage.DataTextField = "TITLE";
        cboTargetPage.DataValueField = "PAGE_ID";
        cboTargetPage.DataBind();

        cboTargetPage.Items.Insert(0, new ListItem("--MENU GROUP--", "0"));
    }

    protected void LoadUserRightsChkbx(){
        CUserAdmin usr = new CUserAdmin();
        DataSet dsUserRights = usr.GetUserRightsDS(BaseMstr);

        repUserRights.DataSource = dsUserRights;
        repUserRights.DataBind();
    }

    protected void btnDeleteMenuItem_OnClick(object sender, EventArgs e) {
        m_cms.DeleteMenuItem(Convert.ToInt32(htxtCurrentID.Value));
        Response.Redirect("cms_menu_edit.aspx", true);
    }

    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode > 0 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.BaseMstr.FindControl("divSysFeedback");
            div.InnerHtml = BaseMstr.StatusComment;
            ScriptManager.RegisterStartupScript((UpdatePanel)this.Parent.FindControl("upWrapperUpdatePanel"), typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            BaseMstr.ClearStatus();
        }
    }

}