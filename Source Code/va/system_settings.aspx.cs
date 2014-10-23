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


public partial class system_settings : System.Web.UI.Page
{
    public bool bAllowUpdate;
    protected void Page_Load(object sender, EventArgs e)
    {
        CSec usrsec = new CSec();
        bAllowUpdate = (usrsec.GetRightMode(Master, (long)SUATUserRight.AdministratorUR) > (long)RightMode.ReadOnly)
            && (Master.APPMaster.UserType == (long)SUATUserType.ADMINISTRATOR);
 
        if (!IsPostBack)
        {
            CDataUtils utils = new CDataUtils();
            //get system settings
            DataSet dsSys = new DataSet();
            if (Session["SYSSETTINGS"] == null)
            {
                CSystemSettings sys = new CSystemSettings();
                Session["SYSSETTINGS"] = sys.GetSystemSettingsDS(Master);
            }
            dsSys = (DataSet)Session["SYSSETTINGS"];
                       
 
            txtMailSMTPHost.Text = utils.GetStringValueFromDS(dsSys, "MAIL_SMTP_HOST");
            txtSenderEmailAddress.Text = utils.GetStringValueFromDS(dsSys, "MAIL_SMTP_SENDER");
            txtMailSMTPPort.Text = Convert.ToString(utils.GetLongValueFromDS(dsSys, "MAIL_SMTP_PORT"));
            txtWebSiteUrl.Text = utils.GetStringValueFromDS(dsSys, "SITE_URL");
            txtNotifyEmailAddress.Text = utils.GetStringValueFromDS(dsSys, "NOTIFY_EMAIL");

            //New Text Message Fields
            txtTextingHost.Text = utils.GetStringValueFromDS(dsSys, "TEXTING_HOST");
            txtTextingPort.Text = Convert.ToString(utils.GetLongValueFromDS(dsSys, "TEXTING_PORT"));
            txtTextingUser.Text = utils.GetStringValueFromDS(dsSys, "TEXTING_USER");
            txtTextingPswd.Text = utils.GetStringValueFromDS(dsSys, "TEXTING_PSWD");
            txtOraWinDir.Text = utils.GetStringValueFromDS(dsSys, "ORA_WIN_DIR");
                     
            Master.ClosePatient();
        }

        if (Master.OnMasterSAVE())
        {
            Save();
        }
    }

    protected void Save()
    {
        if (bAllowUpdate)
        {
            CSystemSettings sysChg = new CSystemSettings();
            bool bSaved = sysChg.UpdateSystemSettings(Master, txtMailSMTPHost.Text, txtSenderEmailAddress.Text, Convert.ToInt64(txtMailSMTPPort.Text), txtWebSiteUrl.Text, txtNotifyEmailAddress.Text, txtTextingHost.Text,Convert.ToInt64(txtTextingPort.Text), txtTextingUser.Text, txtTextingPswd.Text, txtOraWinDir.Text);

            if (bSaved)
            {
                CDataUtils utils = new CDataUtils();

                DataSet dsSys = new DataSet();
                CSystemSettings sys = new CSystemSettings();
                Session["SYSSETTINGS"] = sys.GetSystemSettingsDS(Master);
                dsSys = (DataSet)Session["SYSSETTINGS"];

                Master.StatusComment = "System Settings Updated.";
            }
        }
        else
        {
            Master.StatusCode = 1;
            Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
        }

        ShowSysFeedback();
    }

    protected void CheckUsrRightsMode() 
    {
        txtWebSiteUrl.Enabled = bAllowUpdate;
        txtMailSMTPHost.Enabled = bAllowUpdate;
        txtMailSMTPPort.Enabled = bAllowUpdate;
        txtSenderEmailAddress.Enabled = bAllowUpdate;
        txtNotifyEmailAddress.Enabled = bAllowUpdate;
        //New Texting Fields
        txtTextingHost.Enabled = bAllowUpdate;
        txtTextingPort.Enabled = bAllowUpdate;
        txtTextingUser.Enabled = bAllowUpdate;
        txtTextingPswd.Enabled = bAllowUpdate;
        txtOraWinDir.Enabled = bAllowUpdate;
    }


    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }

}
