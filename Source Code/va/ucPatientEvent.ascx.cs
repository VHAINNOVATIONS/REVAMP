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

public partial class ucPatientEvent : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get;  }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPatientEvents();
        }
    }

    protected void GetPatientEvents() 
    {
        CPatientEvent evt = new CPatientEvent(BaseMstr);
        DataSet dsPatEvt = evt.GetPatientEventsDS();

        repPatientEvents.DataSource = dsPatEvt;
        repPatientEvents.DataBind();
    }

    protected void repPatientEvents_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            Label lbl = (Label)e.Item.FindControl("lblEvtDate");
            Label lbl2 = (Label)e.Item.FindControl("lblStatusDate");
            TextBox txtDate = (TextBox)e.Item.FindControl("txtEvtDate");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkEventStatus");
            Image imgDone = (Image)e.Item.FindControl("imgEvtDone");
            Image imgNotDone = (Image)e.Item.FindControl("imgEvtNotDone");
            TextBox txtComments = (TextBox)e.Item.FindControl("txtEvtComments");
            ImageButton imgBtn = (ImageButton)e.Item.FindControl("imgBtnUpdt");
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trPatEvt");

            HtmlContainerControl spQuest = (HtmlContainerControl)e.Item.FindControl("spQuest");

            bool bDateReadonly = false;
            
            long lTriggeredBy = 0;
            if (!dr.IsNull("TRIGGERED_BY"))
            {
                lTriggeredBy = Convert.ToInt32(dr["TRIGGERED_BY"]);
            }

            if (!dr.IsNull("date_readonly"))
            {
                bDateReadonly = Convert.ToInt32(dr["date_readonly"]) == 1;
            }
            
            if (!dr.IsNull("EVENT_DATE"))
            {
                lbl.Text = Convert.ToDateTime(dr["EVENT_DATE"]).ToShortDateString();
                txtDate.Text = Convert.ToDateTime(dr["EVENT_DATE"]).ToShortDateString();
            }

            long lStatus = 0;
            if (!dr.IsNull("STATUS"))
            {
                lStatus = Convert.ToInt32(dr["STATUS"]);
            }
            chk.Checked = (lStatus == 1) ? true : false;

            if (!dr.IsNull("STATUS_DATE"))
            {
                if (lStatus == 1)
                {
                    lbl2.Text = Convert.ToDateTime(dr["STATUS_DATE"]).ToShortDateString();
                }
                else
                {
                    lbl2.Text = String.Empty;
                }
            }

            if (!dr.IsNull("EVENT_ID"))
            {
                imgBtn.Attributes.Add("eventid", dr["EVENT_ID"].ToString());
            }

            if (!dr.IsNull("COMMENTS"))
            {
                txtComments.Text = dr["COMMENTS"].ToString();
            }

            if (lTriggeredBy == 1)
            {
                chk.Visible = false;

                imgDone.Visible = (lStatus == 1);
                imgNotDone.Visible = (lStatus == 0);

                txtDate.Visible = false;
                lbl.Visible = true;

                if (bDateReadonly)
                {
                    if (lStatus == 0)
                    {
                        spQuest.Visible = true;
                    }
                }
            }
            else if (lTriggeredBy == 4)
            {
                chk.Visible = false;

                imgDone.Visible = (lStatus == 1);
                imgNotDone.Visible = (lStatus == 0);

                txtDate.Visible = true;
                lbl.Visible = false;

                if (bDateReadonly) 
                {
                    txtDate.CssClass = String.Empty;
                    txtDate.ReadOnly = true;
                    txtDate.Enabled = false;
                    if (lStatus == 0) {
                        spQuest.Visible = true;
                    }
                }
            }
            else 
            {
                chk.Visible = true;

                imgDone.Visible = false;
                imgNotDone.Visible = false;

                txtDate.Visible = true;
                lbl.Visible = false;
            }

            // check event date's status
            if (lStatus == 0) 
            { 
                if(!dr.IsNull("EVENT_DATE"))
                {
                    string strEventDate = dr["EVENT_DATE"].ToString();
                    long lEvtStatus = GetEventStatus(strEventDate);
                    
                    switch (lEvtStatus) 
                    { 
                        case 1:
                            tr.Style.Add("background-color", "#CCFFCC");
                            break;

                        case 2:
                            tr.Style.Add("background-color", "#FFFF00");
                            break;
                    }
                }
            }
        }
    }

    protected void imgBtnUpdt_OnClick(object sender, EventArgs e) 
    {
        bool bUpdated = false;

        ImageButton obj = (ImageButton)sender;
        long lEvtID = Convert.ToInt32(obj.Attributes["eventid"]);
        string strEvtDate;
        long lStatus;
        string strComments;

        foreach (RepeaterItem ri in repPatientEvents.Items) 
        {
            Label lbl = (Label)ri.FindControl("lblEvtDate");
            TextBox txtDate = (TextBox)ri.FindControl("txtEvtDate");
            CheckBox chk = (CheckBox)ri.FindControl("chkEventStatus");
            Image imgDone = (Image)ri.FindControl("imgEvtDone");
            Image imgNotDone = (Image)ri.FindControl("imgEvtNotDone");
            TextBox txtComments = (TextBox)ri.FindControl("txtEvtComments");
            ImageButton imgBtn = (ImageButton)ri.FindControl("imgBtnUpdt");

            if (lEvtID == Convert.ToInt32(imgBtn.Attributes["eventid"])) 
            { 
                strEvtDate = txtDate.Text;
                lStatus = (chk.Checked) ? 1 : 0;
                strComments = txtComments.Text;

                //update the event in the DB
                CPatientEvent evt = new CPatientEvent(BaseMstr);
                evt.UpdateEvent(lEvtID, strEvtDate, lStatus, strComments);

                bUpdated = true;
            }
        }

        if (bUpdated) 
        {
            GetPatientEvents();
        }
    }

    protected long GetEventStatus(string strEventDate)
    {
        //Status- 0: ok, 1: event due this week, 2: event past due

        long lStatus = 0;
        if (!String.IsNullOrEmpty(strEventDate))
        {
            DateTime dtOrig = DateTime.Now;
            if (dtOrig.DayOfWeek != DayOfWeek.Sunday)
            {
                do
                {
                    dtOrig = dtOrig.AddDays(-1);
                }
                while (dtOrig.DayOfWeek != DayOfWeek.Sunday);
            }
            DateTime dtToDate = dtOrig.AddDays(6);

            DateTime dtEventDate;
            if (DateTime.TryParse(strEventDate, out dtEventDate))
            {
                if (dtEventDate >= dtOrig && dtEventDate <= dtToDate)
                {
                    lStatus = 1;
                }

                if (dtEventDate <= DateTime.Today)
                {
                    lStatus = 2;
                }
            }
        }
        return lStatus;
    }

}