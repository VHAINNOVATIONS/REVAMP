using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json;

public partial class ucEventManagement : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr {set; get;}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cboEvtDate.SelectedIndex != 2)
        {
            divDateRange.Style.Add("display", "none");
        }
        else
        {
            divDateRange.Style.Add("display", "block");
        }
        
        if (!IsPostBack) {
            GetPatientEvents(String.Empty, String.Empty);
        }

        txtSearchPatient.Attributes.Add("onkeyup", "management.event.filterPatients(this);");
    }

    protected void GetPatientEvents(string strDate1, string strDate2) 
    {
        CPatient pat = new CPatient();
        CPatientEvent evt = new CPatientEvent(BaseMstr);
        
        DataSet dsEvts = evt.GetStatEventsDS();
        DataSet dsPats = pat.GetPatByEvtDueDateDS(BaseMstr, strDate1, strDate2);
        dsPats.Tables[0].TableName = "patients";

        DataSet dsAllEvts = evt.GetAllPatientEventsDS();
        dsAllEvts.Tables[0].TableName = "events";

        dsPats.Tables.Add(dsAllEvts.Tables[0].Copy());
        dsPats.Relations.Add("events", dsPats.Tables["patients"].Columns["PATIENT_ID"], dsPats.Tables["events"].Columns["PATIENT_ID"], false);
        dsPats.AcceptChanges();

        repEvtHeaders.DataSource = dsEvts;
        repEvtHeaders.DataBind();

        repPatientNames.DataSource = dsPats;
        repPatientNames.DataBind();
    }

    protected void GetOverduePatientEvents()
    {
        CPatient pat = new CPatient();
        CPatientEvent evt = new CPatientEvent(BaseMstr);

        DataSet dsEvts = evt.GetStatEventsDS();
        DataSet dsPats = pat.GetPatByOverdueEvtDS(BaseMstr);
        dsPats.Tables[0].TableName = "patients";

        DataSet dsAllEvts = evt.GetAllPatientEventsDS();
        dsAllEvts.Tables[0].TableName = "events";

        dsPats.Tables.Add(dsAllEvts.Tables[0].Copy());
        dsPats.Relations.Add("events", dsPats.Tables["patients"].Columns["PATIENT_ID"], dsPats.Tables["events"].Columns["PATIENT_ID"], false);
        dsPats.AcceptChanges();

        repEvtHeaders.DataSource = dsEvts;
        repEvtHeaders.DataBind();

        repPatientNames.DataSource = dsPats;
        repPatientNames.DataBind();
    }

    protected void repPatEvts_OnItemDataBound(object sender, RepeaterItemEventArgs e) {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            Image img = (Image)e.Item.FindControl("imgEvtStatus");
            long lStatus = 0;

            if (!dr.IsNull("status"))
            {
                lStatus = Convert.ToInt32(dr["STATUS"]);
                if (lStatus > 0)
                {
                    img.ImageUrl = "~/Images/tick.png";
                }
                else {
                    if (!dr.IsNull("EVENT_DATE"))
                    {
                        string strEvtDate = dr["EVENT_DATE"].ToString();
                        long lEvtStatus = GetEventStatus(strEvtDate);
                        switch (lEvtStatus)
                        {
                            case 1:
                                img.ImageUrl = "~/Images/error.png";
                                break;

                            case 2:
                                img.ImageUrl = "~/Images/exclamation.png";
                                break;

                            default:
                                img.Visible = false;
                                break;
                        }
                    }
                    else
                    {
                        img.Visible = false;
                    }
                }
            }
            else
            {
                img.Visible = false;
            }
        }
    }

    protected void repPatientNames_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = (DataRow)drv.Row;

            HtmlAnchor lnk = (HtmlAnchor)e.Item.FindControl("lnkPatName");
            Label lbl = (Label)e.Item.FindControl("lblPhone");

            if (!dr.IsNull("PATIENT_ID"))
            {
                lnk.Attributes.Add("href", "#");
                lnk.Attributes.Add("style", "text-decoration: underline; color: blue;");
                lnk.Attributes.Add("onclick", "__doPostBack('PATIENT_LOOKUP',  '" + dr["PATIENT_ID"].ToString() + "|event');");
            }

            if (!dr.IsNull("PHONE"))
            {
                string strPhone = Regex.Replace(dr["PHONE"].ToString(), @"[\(|\)|\s|\.|\-]", String.Empty);
                lbl.Text = Regex.Replace(strPhone, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }
        }
    }

    protected void cboEvtDate_OnSelectedIndexChanged(object sender, EventArgs e) 
    {
        DropDownList cbo = (DropDownList)sender;
        if (cbo.SelectedValue == "-1"){
            divDateRange.Style.Add("display", "none");
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;

            //filter patients list
            GetPatientEvents(String.Empty, String.Empty);
            upEvtList.Update();

        }
        else if (cbo.SelectedValue == "0") {
            divDateRange.Style.Add("display", "none");

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

            string strFromDate = dtOrig.Month.ToString().PadLeft(2, '0') + "/" + dtOrig.Day.ToString().PadLeft(2, '0') + "/" + dtOrig.Year.ToString();
            string strToDate = dtToDate.Month.ToString().PadLeft(2, '0') + "/" + dtToDate.Day.ToString().PadLeft(2, '0') + "/" + dtToDate.Year.ToString();

            txtFromDate.Text = strFromDate;
            txtToDate.Text = strToDate;

            //filter patients list
            GetPatientEvents(strFromDate, strToDate);
            upEvtList.Update();

        }
        else if (cbo.SelectedValue == "1") {
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;

            divDateRange.Style.Add("display","block");
        }
        else if (cbo.SelectedValue == "2")
        {
            //filter patients list
            GetOverduePatientEvents();
            upEvtList.Update();
        }

    }

    protected void btnUpdateList_OnClick(object sender, EventArgs e) 
    {
        string strFromDate = txtFromDate.Text,
               strToDate = txtToDate.Text;

        //filter patients list
        GetPatientEvents(strFromDate, strToDate);
        upEvtList.Update();
    }

    protected long GetEventStatus(string strEventDate) 
    {
        //Status- 0: ok, 1: event due this week, 2: event past due
        
        long lStatus = 0;
        if(!String.IsNullOrEmpty(strEventDate))
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
            if(DateTime.TryParse(strEventDate, out dtEventDate))
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