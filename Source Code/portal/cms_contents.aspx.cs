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

public partial class cms_contents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                long lPageID = Convert.ToInt32(Request.QueryString["id"]);
                GetPageContents(lPageID);
            }
            else
            {
                litContents.Text = "<h1>Empty page.</h1>";
            } 
        }

        htxtOPT0.Value = Master.SelectedPatientID;

        if (Request.QueryString["opt1"] != null)
        {
            long lEventID = Convert.ToInt32(Request.QueryString["opt1"]);
            CPatientEvent evt = new CPatientEvent(Master);
            evt.CompletedEvent(lEventID);
        }

    }

    protected void GetPageContents(long lPageID) {
        CContentManagement cms = new CContentManagement(Master);
        DataSet dsPage = cms.GetPageContentsDS(lPageID);
        foreach (DataTable dt in dsPage.Tables) {
            foreach (DataRow dr in dt.Rows) {
                
                if (!dr.IsNull("TITLE"))
                {
                    litTitle.Text = dr["TITLE"].ToString();
                }

                if (!dr.IsNull("CONTENTS"))
                {
                    litContents.Text = dr["CONTENTS"].ToString();
                }
            }
        }
    }
}