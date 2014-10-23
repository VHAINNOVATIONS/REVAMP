using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class ucPatLookup : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    public CDataUtils m_utils = new CDataUtils();

    public long lOpenCasesCount;
    public DataSet dsOpenCases;

    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (ListItem li in rblSearchType.Items)
        {
            li.Attributes.Add("onclick", "auditRadButton(this);");
        }
    }

    protected void btnPopupPatlookupSearch_Click(object sender, EventArgs e)
    {
        divPatLookupStatus.InnerHtml = String.Empty;

        CPatient pat = new CPatient();
     
        string strSearchText = "";

        strSearchText = txtSearch.Text.ToUpper();

        //TIU SUPPORT - transfer MDWS patient
        if (BaseMstr.APPMaster.TIU)
        {
            //1 = last name, 2 = LSSN
            long lMatchType = Convert.ToInt32(rblSearchType.SelectedValue);
            if (lMatchType == 3)
            {
                lMatchType = 2;
            }

            //transfer patients from MDWS to revamp db
            MDWSPatientTransfer(lMatchType, strSearchText);
        }

        DataSet ds = pat.GetPatientLookupDS(BaseMstr,
                                            1, 
                                            Convert.ToInt32(rblSearchType.SelectedValue),
                                            strSearchText
                                            );
        if (ds != null)
        {
            Store1.DataSource = m_utils.GetDataTable(ds);
            Store1.DataBind();
            
            if (ds.Tables[0].Rows.Count < 1) 
            {
                ShowLookupMesage();
            }
        }
        else
        {
            ShowLookupMesage();
        }

        ScriptManager.RegisterClientScriptBlock(upPatLookup, typeof(string), "initRadios", "InitRadios();", true);
    }

    public void ClearPatLookup(object sender, EventArgs e) 
    {
        txtSearch.Text = String.Empty;
  
        Store1.RemoveAll();
        Store1.DataBind();
    }

    public void ShowLookupMesage() 
    {
        divPatLookupStatus.InnerHtml = "<font color=\"blue\"><img alt=\"\" src=\"Images/table_delete.png\" >&nbsp;No records found!</font>";
        ScriptManager.RegisterClientScriptBlock(upPatLookup, typeof(string), "saved", "clearLookupStatusDiv(4);", true);
    }

    /// <summary>
    /// TIU SUPPORT - transfer MDWS patients
    /// </summary>
    /// <param name="strSearch"></param>
    /// <returns></returns>
    protected bool MDWSPatientTransfer(long lMatchType, string strSearch)
    {
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(BaseMstr,
                              Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //check connection login if we are not connected
        long lMDWSUserID = 0;
        status = mdwsUtils.MDWSLogin(data,
                                     BaseMstr,
                                     strConnectionString,
                                     bAudit,
                                     out lMDWSUserID);
        if (!status.Status)
        {
            return false;
        }
        
        //transfer the patient(s)
        long lPatCount = 0;
        REVAMP.TIU.CMDWSOps mdws = new REVAMP.TIU.CMDWSOps(data);
        status = mdws.GetMDWSMatchPatients(strConnectionString,
                                            bAudit,
                                            BaseMstr.FXUserID,
                                            BaseMstr.Key,
                                            BaseMstr.Session,
                                            lMatchType,
                                            strSearch,
                                            out lPatCount);

        if (!status.Status)
        {
            return false;
        }

        return true;
    }   
}

