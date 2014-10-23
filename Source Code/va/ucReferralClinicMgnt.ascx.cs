using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

public partial class ucReferralClinicMgnt : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }
    public bool bAllowUpdate { get; set; }

    protected CDataUtils utils = new CDataUtils();
    protected CReferralClinic rfc = new CReferralClinic();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //bind 'onkeyup' event to the 'txtClinic' text box
        //text in this field is required to enable the save button
        txtClinic.Attributes.Add("onkeyup","management.clinic.checkLength(this);");
        
        if(!IsPostBack)
        {
            //Load State combo
            CDemographics rfcState = new CDemographics();
            rfcState.LoadDemStateDropDownList(BaseMstr, cboState, "-1");
        }

        CheckUsrRightsMode();
    }
    public bool LoadReferralClinic() 
    {
        DataSet dsReferralClinics = rfc.GetReferralClinicLookupDS(BaseMstr);
        if (dsReferralClinics != null)
        {
            //Create a JSON string of the Clinics Dataset and 
            //hold it in a hidden field in order to re-use the data without 
            //the need of further call to the DB server.
            htxtClinicData.Value = utils.GetJSONString(dsReferralClinics);

            repReferralClinic.DataSource = dsReferralClinics;
            repReferralClinic.DataBind();
            return true;
        }
        return false;
    }

    protected void btnClinicSave_OnClick(object sender, EventArgs e) 
    {
        if (bAllowUpdate)
        {
            //perform an insert if no referralID is selected
            if (String.IsNullOrEmpty(htxtReferralID.Value) || Convert.ToInt32(htxtReferralID.Value) < 0)
            {
                if (InsertReferralClinic())
                {
                    LoadReferralClinic();
                }
            }
            else
            {
                long lReferralID = Convert.ToInt32(htxtReferralID.Value);
                if (UpdateReferralClinic(lReferralID))
                {
                    LoadReferralClinic();
                }
            } 
        }
        return;
    }

    //repReferralClinic_OnItemDataBound
    protected void repReferralClinic_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            ImageButton imgBtn = (ImageButton)e.Item.FindControl("btnDeleteClinic");
            imgBtn.Attributes.Add("onclick", "return management.clinic.confirmDelete();");
        }
    }
    
    //Insert Referral Clinic
    protected bool InsertReferralClinic()
    {
        string strStateID = "-1";
        if(cboState.SelectedIndex > -1)
        {
            strStateID = cboState.SelectedValue;
        }
        long lNewReferralID;

        bool bInserClinic = rfc.InsertReferralClinic(BaseMstr,
                                                    txtClinic.Text.Trim(),
                                                    txtComment.Text.Trim(),
                                                    txtProviderName.Text.Trim(),
                                                    txtAddress.Text.Trim(),
                                                    txtCity.Text.Trim(),
                                                    strStateID,
                                                    txtPostalCode.Text.Trim(),
                                                    txtPhone.Text.Trim(),
                                                    txtFax.Text.Trim(),
                                                    out lNewReferralID);
        if (bInserClinic) 
        {
            htxtReferralID.Value = lNewReferralID.ToString();
        }

        return bInserClinic;
    }

    //Update Referral Clinic
    protected bool UpdateReferralClinic(long lReferralID)
    {
        string strStateID = "-1";
        if (cboState.SelectedIndex > -1)
        {
            strStateID = cboState.SelectedValue;
        }

        bool bUpdatedClinic = rfc.UpdateReferralClinic(BaseMstr,
                                                    lReferralID,
                                                    txtClinic.Text.Trim(),
                                                    txtComment.Text.Trim(),
                                                    txtProviderName.Text.Trim(),
                                                    txtAddress.Text.Trim(),
                                                    txtCity.Text.Trim(),
                                                    strStateID,
                                                    txtPostalCode.Text.Trim(),
                                                    txtPhone.Text.Trim(),
                                                    txtFax.Text.Trim());
        return bUpdatedClinic;
    }

    //Delete Referral Clinic
    protected void DeleteReferralClinic(object sender, CommandEventArgs e) 
    {
        long lReferralID = Convert.ToInt32(e.CommandArgument);
        bool bDeletedClinic = rfc.DiscontinueReferralClinic(BaseMstr, lReferralID);
        if (bDeletedClinic)
        {
            htxtReferralID.Value = "-1";
            LoadReferralClinic();
        }
    }

    protected void CheckUsrRightsMode() 
    {
        txtClinic.Enabled = bAllowUpdate;
        txtComment.Enabled = bAllowUpdate;
        txtProviderName.Enabled = bAllowUpdate;
        txtAddress.Enabled = bAllowUpdate;
        txtCity.Enabled = bAllowUpdate;
        cboState.Enabled = bAllowUpdate;
        txtPostalCode.Enabled = bAllowUpdate;
        txtPhone.Enabled = bAllowUpdate;
        txtFax.Enabled = bAllowUpdate;
        btnClinicSave.Visible = bAllowUpdate;
        btnClinicCancel.Visible = bAllowUpdate;
    }

}
