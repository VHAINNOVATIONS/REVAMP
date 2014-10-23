using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class referral_clinic_management : System.Web.UI.Page
{
    public bool bAllowUpdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        CSec usrsec = new CSec();
        bAllowUpdate = (usrsec.GetRightMode(Master, (long)SUATUserRight.DataManagementUR) > (long)RightMode.ReadOnly);
        
        //this page does not require a patient
        Master.ClosePatient();

        ucReferralClinicManagement.BaseMstr = Master;
        ucReferralClinicManagement.bAllowUpdate = bAllowUpdate;

        if (!IsPostBack)
        {
            ucReferralClinicManagement.LoadReferralClinic();
        }
    }
}
