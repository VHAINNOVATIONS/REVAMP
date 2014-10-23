using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccess;

/// <summary>
/// Summary description for CTemplate
/// </summary>
public class CTemplate
{
	public CTemplate()
	{
		
	}

    /////////////////////////////////////////////////////////
    //get a dataset of template tag items
    public DataSet GetTemplateDataTagDS( BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetTemplateDataTagRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetParsedTemplate2DS(BaseMaster BaseMstr,
                                    string strPatientID,
                                    string strEncounterID,
                                    long lTemplateID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", strPatientID);
        plist.AddInputParameter("pi_vEncounterID", strEncounterID);
        plist.AddInputParameter("pi_nTemplateID", lTemplateID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetParsedTemplate2RS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public string GetParsedTemplateText2(BaseMaster BaseMstr,
                                         string strPatientID,
                                        string strEncounterID,
                                         long lTemplateID)
    {
        DataSet ds = GetParsedTemplate2DS(BaseMstr,
                                         strPatientID,
                                         strEncounterID,
                                         lTemplateID);

        CDataUtils utils = new CDataUtils();
        string strText = utils.GetStringValueFromDS(ds, "TEMPLATE_TEXT");

        return strText;
    }

    /////////////////////////////////////////////////////////
    //get a dataset of template tag group items
    public DataSet GetTemplateDataTagGroupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetTemplateDataTagGroupRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }

    /////////////////////////////////////////////////////////
    //get a dataset of template types
    public DataSet GetTemplateTypeDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetTemplateTypeRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    //public bool 

    public bool LoadTemplateTypeCombo( BaseMaster BaseMstr,
                                       DropDownList cboType)
    {
        CDropDownList cbo = new CDropDownList();

        DataSet ds = GetTemplateTypeDS(BaseMstr);
        cbo.RenderDataSet(BaseMstr,
                          ds,
                          cboType,
                          "DESCRIPTION",
                          "TYPE_ID",
                          "");

        return true;
    }

    public bool UpdateTemplate( BaseMaster BaseMstr,
                                long lTemplateID,
                                long lGroupID,
                                string strName,
                                string strTemplateText)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nTemplateID", lTemplateID);
        plist.AddInputParameter("pi_nGroupID", lGroupID);
        plist.AddInputParameter("pi_vTemplateName", strName);
        plist.AddInputParameterCLOB("pi_vTemplateText", strTemplateText);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.UpdateTemplate",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            BaseMstr.StatusComment = "Template - Updated. ";
            return true;
        }

        return false;
    }

    public bool InsertTemplate( BaseMaster BaseMstr,
                                long lSOAPsectID,
                                string strName,
                                string strTemplateText,
                                long lGroupID,
                                out long lTemplateID )
    {

        lTemplateID = 0;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nSOAPsectID", lSOAPsectID);
        plist.AddInputParameter("pi_vTemplateName", strName);
        plist.AddInputParameterCLOB("pi_vTemplateText", strTemplateText);
        plist.AddInputParameter("pi_nTempGroupID", lGroupID);

        //long lFXUserID = -1;
        plist.AddOutputParameter("po_nTemplateID", lTemplateID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.InsertTemplate",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            CDataParameter paramValue = plist.GetItemByName("po_nTemplateID");
            lTemplateID = paramValue.LongParameterValue;

            BaseMstr.StatusComment = "Template - Updated. ";
            return true;
        }

        return false;
    }

    public bool LoadTemplateEdit( BaseMaster BaseMstr,
                                  long lTemplateID,
                                  TextBox txtName,
                                  DropDownList cboGroup,
                                  TextBox txtTemplate)
    {
        txtName.Text = "";
        cboGroup.SelectedIndex = -1;
        txtTemplate.Text = "";
        if (lTemplateID < 1)
        {
            txtName.Focus();
            return true;
        }

        CCheckBoxList lst = new CCheckBoxList();
        CDropDownList cbo = new CDropDownList();

        DataSet ds = GetTemplateDS(BaseMstr);
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull("TEMPLATE_ID"))
                {
                    long lID = Convert.ToInt32(row["TEMPLATE_ID"]);
                    if (lID == lTemplateID)
                    {
                        if (!row.IsNull("TEMPLATE"))
                        {
                            txtTemplate.Text = Convert.ToString(row["TEMPLATE"]).Replace("<","[").Replace(">","]");
                            txtName.Text = Convert.ToString(row["DESCRIPTION"]);
                            cbo.SelectValue(cboGroup, Convert.ToInt32(row["TYPE_ID"]));
                            return true;
                        }
                    }
                }
            }
        }
                
        return true;
    }

    public bool LoadTemplateList( BaseMaster BaseMstr,
                                  ListBox lstTemplates)
    {
        CListBox lst = new CListBox();

        DataSet dsTemplates = GetTemplateDS(BaseMstr);
        lst.RenderDataSet(BaseMstr,
                          dsTemplates,
                          lstTemplates,
                          "DESCRIPTION",
                          "TEMPLATE_ID");
        
        return true;
    }

    public bool LoadTemplateComboByType(BaseMaster BaseMstr,
                                        long lTemplateType,
                                        DropDownList cboTemplates)
    {
        CDropDownList cbo = new CDropDownList();
        DataSet dsTemplates = GetTemplateDS(BaseMstr);
        cbo.RenderDataSetByMatch(BaseMstr,
                                  dsTemplates,
                                  cboTemplates,
                                  "DESCRIPTION",
                                  "TEMPLATE_ID",
                                  "",
                                  "TYPE_ID",
                                  Convert.ToString(lTemplateType));
        
        return true;
    }

    /////////////////////////////////////////////////////////
    //get a dataset of templates
    public DataSet GetTemplateDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetTemplateRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public bool DiscontinueTemplate(BaseMaster BaseMstr, long lTemplateID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nTemplateID", lTemplateID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.DiscontinueTemplate",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    // -----------------------------------------------------
    // TEMPLATE GROUPS

    public bool InsertTemplateGroup(BaseMaster BaseMstr,
                            string strName,
                            string strComments,
                            out long lTemplateGrpID)
    {

        lTemplateGrpID = -1;

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vTemplateGrpName", strName);
        plist.AddInputParameter("pi_vComments", strComments);

        //long lFXUserID = -1;
        plist.AddOutputParameter("po_nTemplateGrpID", lTemplateGrpID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.InsertTemplateGroup",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            CDataParameter paramValue = plist.GetItemByName("po_nTemplateGrpID");
            lTemplateGrpID = paramValue.LongParameterValue;
            return true;
        }

        return false;
    }

    public bool UpdateTemplateGroup(BaseMaster BaseMstr,
                            string strName,
                            string strComments,
                            long lTemplateGrpID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        /*
            pi_nTemplateGrpID   in number,
            pi_vTemplateGrpName in varchar2,
            pi_vComments        in varchar2,
         */

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vTemplateGrpName", strName);
        plist.AddInputParameter("pi_vComments", strComments);
        plist.AddInputParameter("pi_nTemplateGrpID", lTemplateGrpID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.UpdateTemplateGroup",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    public bool DiscontinueTemplateGroup(BaseMaster BaseMstr, long lTemplateGrpID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nTemplateGrpID", lTemplateGrpID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_TEMPLATE.DiscontinueTemplateGroup",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    public DataSet GetTemplateGroupsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetTemplateGroupsRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetGroupTemplatesDS(BaseMaster BaseMstr, long lGroupID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_nGroupID", lGroupID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_TEMPLATE.GetGroupTemplatesRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

}
