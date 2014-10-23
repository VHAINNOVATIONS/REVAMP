using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class education : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RenderEducationTree();

        htxtOPT0.Value = Master.SelectedPatientID;

        if (!IsPostBack) 
        {
            divEduInfo.Style.Add("display", "block");
        }
        
        if (Request.QueryString["opt1"] != null)
        {
            long lEventID = Convert.ToInt32(Request.QueryString["opt1"]);
            CPatientEvent evt = new CPatientEvent(Master);
            evt.CompletedEvent(lEventID);
        }

        if (IsPostBack) 
        {
            string strControlName = Request.Params["__EVENTTARGET"];
            if (strControlName.IndexOf("upCheckVideosStatus") > -1) 
            {
                if (HasViewedVideos())
                {
                    Response.Redirect("portal_start.aspx");
                }
            }
        }
    }

    protected void upEduContents_OnLoad(object sender, EventArgs e) {
        long lEduID = -1;
        if (htxtEduID.Value.Length > 0) {
            lEduID = Convert.ToInt32(htxtEduID.Value);
        }

        string strTitle = String.Empty;

        CContentManagement cms = new CContentManagement(Master);
        DataSet dsPage = cms.GetPageContentsDS(lEduID);
        foreach (DataTable dt in dsPage.Tables)
        {
            foreach (DataRow dr in dt.Rows)
            {

                if (!dr.IsNull("TITLE"))
                {
                    litTitle.Text = dr["TITLE"].ToString();
                    strTitle = dr["TITLE"].ToString();
                }

                if (!dr.IsNull("CONTENTS"))
                {
                    litContents.Text = dr["CONTENTS"].ToString();
                }
            }
        }

        if (!String.IsNullOrEmpty(strTitle))
        {
            ScriptManager.RegisterStartupScript(upEduContents, GetType(), "trackTitle", "window.eduTitle = '" + HttpUtility.HtmlEncode(strTitle) + "'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(upEduContents, GetType(), "trackTitle", "if(typeof(window.eduTitle)!=\"undefined\"){delete window.eduTitle;}", true);
        }

    }

    protected void RenderEducationTree() {
        CContentManagement cms = new CContentManagement(Master);
        cms.RenderEduTreePanel(divMenuTree);
    }

    protected bool HasViewedVideos() {
        CPatientEvent patevt = new CPatientEvent(Master);
        DataSet ds = patevt.GetPatientEventsDS();
        
        long lViewedVideos = 0;
        bool bMailedStudiDevice = false;

        if (ds != null) {
            DataRow[] drs = ds.Tables[0].Select("event_id in (3,4)");
            foreach (DataRow dr in drs) 
            { 
                if(!dr.IsNull("STATUS"))
                {
                    lViewedVideos += Convert.ToInt32(dr["STATUS"]);
                }
            }

            DataRow drSD = ds.Tables[0].Select("event_id = 6")[0];
            if (drSD != null) 
            {
                if (!drSD.IsNull("STATUS"))
                {
                    bMailedStudiDevice = (Convert.ToInt32(drSD["STATUS"]) == 1); 
                }
            }
        }

        return ((lViewedVideos >= 2) && !bMailedStudiDevice);
    }

}