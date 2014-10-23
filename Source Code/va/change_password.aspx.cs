using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//TIU SUPPORT
using System.Web.UI.HtmlControls;

public partial class change_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.ClosePatient();
        ucLogin.BaseMstr = Master;

        //TIU SUPPORT
        if (!IsPostBack)
        {
            if (Master.APPMaster.TIU)
            {
                CMDWSUtils mdws = new CMDWSUtils();

                pnlMDWS.Visible = true;

                txtMDWSUserName.Text = String.Empty;
                txtMDWSPWD.Text = String.Empty;
                ddlRegion.SelectedIndex = -1;
                ddlSite.SelectedIndex = -1;

                string strUserName = String.Empty;
                string strPWD = String.Empty;
                long lRegionID = 0;
                long lSiteID = 0;
                string strNoteTitleLabel = String.Empty;
                long lNoteClinicID = 0;

                bool bStatus = mdws.GetMDWSAccountInfo(Master.FXUserID,
                                                        out strUserName,
                                                        out strPWD,
                                                        out lRegionID,
                                                        out lSiteID,
                                                        out strNoteTitleLabel,
                                                        out lNoteClinicID,
                                                        Master);

                if (bStatus)
                {
                    //update account info on the screen
                    txtMDWSUserName.Text = strUserName;
                    txtMDWSPWD.Text = strPWD;
                }

                mdws.LoadRegionsDDL(lRegionID, Master, ddlRegion);
                mdws.LoadSitesDDL(lRegionID, lSiteID, Master, ddlSite);

                //hide defaults if no account
                if (strUserName == null || strUserName.Trim() == String.Empty)
                {
                    lblNoteTitle.Visible = false;
                    ddlNoteTitle.Visible = false;
                    lblClinic.Visible = false;
                    ddlClinic.Visible = false;
                    btnSaveTIU.Visible = false;
                }
                else
                {
                    //show and load defaults
                    lblNoteTitle.Visible = true;
                    ddlNoteTitle.Visible = true;
                    lblClinic.Visible = true;
                    ddlClinic.Visible = true;
                    btnSaveTIU.Visible = true;

                    mdws.LoadNoteTitlesDDL(strNoteTitleLabel,
                                           Master,
                                           ddlNoteTitle);
                    mdws.LoadClinicsDDL(lNoteClinicID,
                                        Master,
                                        ddlClinic);
                }
            }
            else
            {
                pnlMDWS.Visible = false;
            }
        }

        //TIU SUPPORT
        //get the postback control
        if (Master.APPMaster.TIU)
        {
            CMDWSUtils mdwsUtils = new CMDWSUtils();
            string strPostBackControl = Request.Params["__EVENTTARGET"];
            if (strPostBackControl != null)
            {
                //did we do a patient lookup?
                if (strPostBackControl.IndexOf("btnSaveMDWS") > -1)
                {
                    long lRegionID = REVAMP.TIU.CDropDownList.GetSelectedLongID(ddlRegion);
                    long lSiteID = REVAMP.TIU.CDropDownList.GetSelectedLongID(ddlSite);
                    string strUserName = txtMDWSUserName.Text;
                    string strPWD = txtMDWSPWD.Text;
                    if (!mdwsUtils.UpdateMDWSAccount(lRegionID,
                                                lSiteID,
                                                strUserName,
                                                strPWD,
                                                Master))
                    {
                        ShowSysFeedback();
                    }
                    else
                    {
                        //load titles and clinics if they are not loaded
                        if (ddlNoteTitle.Items.Count < 1)
                        {
                            lblNoteTitle.Visible = true;
                            ddlNoteTitle.Visible = true;
                            lblClinic.Visible = true;
                            ddlClinic.Visible = true;
                            btnSaveTIU.Visible = true;

                            mdwsUtils.LoadNoteTitlesDDL("",
                                                       Master,
                                                       ddlNoteTitle);
                            mdwsUtils.LoadClinicsDDL(-1,
                                                    Master,
                                                    ddlClinic);
                        }
                    }
                }
                if (strPostBackControl.IndexOf("btnSaveTIU") > -1)
                {
                    long lClinicID = Convert.ToInt32(ddlClinic.SelectedValue);
                    string strNoteTitle = ddlNoteTitle.SelectedValue;
                    if (!mdwsUtils.UpdateMDWSDefaults(lClinicID,
                                                      strNoteTitle,
                                                      Master))
                    {
                        ShowSysFeedback();
                    }
                }
            }
        }
    }

    /// <summary>
    /// TIU SUPPORT
    /// </summary>
    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(upMDWS, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }

    /// <summary>
    /// TIU SUPPORT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != -1)
        {
            long lRegionID = REVAMP.TIU.CDataUtils.ToLong(ddlRegion.SelectedValue);
            CMDWSUtils mdws = new CMDWSUtils();
            mdws.LoadSitesDDL(lRegionID, -1, Master, ddlSite);
        }
    }

}