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
using Ext.Net;

public class CContentManagement
{
    protected BaseMaster m_BaseMstr;

    protected string strMenuHREF = "cms_contents.aspx?id=";

    public CContentManagement(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    public DataSet GetAuthorDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetAuthorRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetPagesListDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetPagesListRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetPageContentsDS(long lPageID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nPageID", lPageID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetPageContentsRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public bool InsertPage(long lAuthorID,
                            string strTitle,
                            string strContents,
                            long lStatus,
                            out long lPageID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        lPageID = -1;

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        /*
            pi_nAuthorID        in number,
            pi_vTitle           in varchar2,
            pi_cContents        in clob,
            pi_nStatus          in number,
            po_nPageID          out number,
         */

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nAuthorID", lAuthorID);
        plist.AddInputParameter("pi_vTitle", strTitle);
        plist.AddInputParameterCLOB("pi_cContents", strContents);
        plist.AddInputParameter("pi_nStatus", lStatus);

        plist.AddOutputParameter("po_nPageID", lPageID);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.InsertPage",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            CDataParameter paramValue = plist.GetItemByName("po_nPageID");
            lPageID = paramValue.LongParameterValue;
            return true;
        }
        return false;
    }

    public bool EditPage(long lPageID,
                        long lAuthorID,
                        string strTitle,
                        string strContents,
                        long lStatus)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nPageID", lPageID);
        plist.AddInputParameter("pi_nAuthorID", lAuthorID);
        plist.AddInputParameter("pi_vTitle", strTitle);
        plist.AddInputParameterCLOB("pi_cContents", strContents);
        plist.AddInputParameter("pi_nStatus", lStatus);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.EditPage",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }



    public DataSet GetALLMenusTop()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetALLMenusTop",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetALLMenuChildren()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetALLMenuChildren",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetALLMenuItems()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetALLMenuItems",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public string RenderMenuHTML(long lPortalID)
    {
        String strMenuHTML = String.Empty;
        DataSet dsRootLevel = GetMenuRootLevelDS(lPortalID);
        DataSet dsMenuItems = GetMenuItemsDS(lPortalID);

        //menu outter wrapper <UL>
        //strMenuHTML += "<ul id=\"cms-horizontal-menu\">\n";

        if (dsRootLevel.Tables[0].Rows.Count > 0)
        {
            foreach (DataTable dt in dsRootLevel.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strMenuItem = String.Empty;

                    if (!dr.IsNull("MENU_TITLE"))
                    {
                        strMenuItem += "<li>\n";

                        string strItmHref = "#";

                        if (!dr.IsNull("PAGE_ID")) {
                            if (Convert.ToInt32(dr["PAGE_ID"]) > 0)
                            {
                                strItmHref = strMenuHREF + dr["PAGE_ID"].ToString();
                            }
                        }

                        strMenuItem += "<a href=\"" + strItmHref + "\" ";

                        strMenuItem += " >";
                        strMenuItem += dr["MENU_TITLE"].ToString();
                        strMenuItem += "</a>\n";

                        strMenuItem += RenderSubMenusHTML(dsMenuItems, Convert.ToInt32(dr["MENU_ID"]));

                        strMenuItem += "</li>\n";
                    }

                    strMenuHTML += strMenuItem;
                }
            }
        }

        //strMenuHTML += "</ul>";

        return strMenuHTML;
    }

    protected string RenderSubMenusHTML(DataSet dsMenuItems, long lParentID)
    {
        string strSubHTML = String.Empty;

        DataRow[] drSubMenus = dsMenuItems.Tables[0].Select("parent_id = " + lParentID.ToString());

        if (drSubMenus.Length > 0)
        {
            strSubHTML += "<ul>";

            foreach (DataRow dr in drSubMenus)
            {
                string strMenuItem = String.Empty;

                string strItmHref = "#";

                if (!dr.IsNull("PAGE_ID"))
                {
                    if (Convert.ToInt32(dr["PAGE_ID"]) > 0)
                    {
                        strItmHref = strMenuHREF + dr["PAGE_ID"].ToString();
                    }
                }

                if (!dr.IsNull("MENU_TITLE"))
                {
                    strMenuItem += "<li>\n";
                    strMenuItem += "<a href=\"" + strItmHref + "\" ";

                    strMenuItem += " >";
                    strMenuItem += dr["MENU_TITLE"].ToString();
                    strMenuItem += "</a>\n";

                    strMenuItem += RenderSubMenusHTML(dsMenuItems, Convert.ToInt32(dr["MENU_ID"]));

                    strMenuItem += "</li>\n";
                }

                strSubHTML += strMenuItem;
            }

            strSubHTML += "</ul>";
        }

        return strSubHTML;
    }

    public void RenderTreePanel(HtmlGenericControl ph)
    {
        // Define Ext.Net.TreePanel object
        Ext.Net.TreePanel tree = new Ext.Net.TreePanel();
        tree.ID = "CMSMenuTree";
        tree.Width = Unit.Pixel(350);
        tree.AutoWidth = true;
        tree.Title = String.Empty;
        tree.TitleCollapse = true;
        tree.AutoScroll = true;
        tree.RootVisible = true;
        tree.Frame = false;
        tree.Border = false;

        // Create a root node
        Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root");
        root.Expanded = true;
        root.Icon = Icon.Folder;

        root.Listeners.Click.Handler = "cms.menu.selectNode({menu_id: 0, page_id: 0, parent_id: 0, sort_order: '0'})";

        tree.Root.Add(root);

        RenderTopMenuNode(root);

        ph.Controls.Add(tree);
    }

    // Top Menu TreeNode
    protected void RenderTopMenuNode(Ext.Net.TreeNode tnRoot)
    {
        DataSet dsRootLevel = GetALLMenusTop();
        DataSet dsMenuItems = GetALLMenuChildren();

        if (dsRootLevel.Tables[0].Rows.Count > 0)
        {
            foreach (DataTable dt in dsRootLevel.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("MENU_TITLE")) {

                        long lPageID = Convert.ToInt32(dr["PAGE_ID"]);
                        
                        Ext.Net.TreeNode tnTop = new Ext.Net.TreeNode(dr["MENU_TITLE"].ToString());
                        tnTop.Expanded = true;

                        if (lPageID == 0) {
                            tnTop.Icon = Icon.Folder;
                        }

                        string strOpts = "menu_id: " + dr["MENU_ID"].ToString() + ", ";
                        strOpts += "menu_title: '" + dr["MENU_TITLE"].ToString() + "', ";
                        strOpts += "parent_id: " + dr["PARENT_ID"].ToString() + ", ";
                        strOpts += "page_id: " + dr["PAGE_ID"].ToString() + ", ";
                        strOpts += "active: " + dr["ACTIVE"].ToString() + ", ";
                        strOpts += "status: " + dr["STATUS"].ToString() + ", ";
                        strOpts += "user_rights: " + dr["USER_RIGHTS"].ToString() + ", ";
                        strOpts += "target_portal_id: " + dr["TARGET_PORTAL_ID"].ToString() + ", ";
                        strOpts += "sort_order: '" + dr["SORT_ORDER"].ToString() + "'";

                        tnTop.Listeners.Click.Handler = "cms.menu.selectNode({" + strOpts + "})";

                        //render submenu items
                        RenderSubMenusNode(dsMenuItems, Convert.ToInt32(dr["MENU_ID"]), tnTop);

                        tnRoot.Nodes.Add(tnTop);
                    }
                }
            }
        }
    }

    // Sub-Menu TreeNode (recursive)
    protected void RenderSubMenusNode(DataSet dsMenuItems, long lParentID, Ext.Net.TreeNode tnParent)
    {
        DataRow[] drSubMenus = dsMenuItems.Tables[0].Select("parent_id = " + lParentID.ToString());

        if (drSubMenus.Length > 0)
        {
            foreach (DataRow dr in drSubMenus)
            {
                if (!dr.IsNull("MENU_TITLE"))
                {
                    long lPageID = Convert.ToInt32(dr["PAGE_ID"]);
                    
                    Ext.Net.TreeNode tnSub = new Ext.Net.TreeNode(dr["MENU_TITLE"].ToString());
                    tnSub.Expanded = true;

                    if (lPageID == 0)
                    {
                        tnSub.Icon = Icon.Folder;
                    }

                    string strOpts = "menu_id: " + dr["MENU_ID"].ToString() + ", ";
                    strOpts += "menu_title: '" + dr["MENU_TITLE"].ToString() + "', ";
                    strOpts += "parent_id: " + dr["PARENT_ID"].ToString() + ", ";
                    strOpts += "page_id: " + dr["PAGE_ID"].ToString() + ", ";
                    strOpts += "active: " + dr["ACTIVE"].ToString() + ", ";
                    strOpts += "status: " + dr["STATUS"].ToString() + ", ";
                    strOpts += "user_rights: " + dr["USER_RIGHTS"].ToString() + ", ";
                    strOpts += "target_portal_id: " + dr["TARGET_PORTAL_ID"].ToString() + ", ";
                    strOpts += "sort_order: '" + dr["SORT_ORDER"].ToString() + "'";

                    tnSub.Listeners.Click.Handler = "cms.menu.selectNode({" + strOpts + "})";

                    //render submenu items
                    RenderSubMenusNode(dsMenuItems, Convert.ToInt32(dr["MENU_ID"]), tnSub);

                    tnParent.Nodes.Add(tnSub);
                }
            }
        }
    }

    public bool InsertMenuItem(string strMenuTitle,
                                long lParentID,
                                long lPageID,
                                long lUserRights,
                                long lTargetPortal,
                                long lSortOrder)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        /*
            pi_vMenuTitle      in varchar2,
            pi_nParentID       in number,
            pi_nPageID         in number,
            pi_nUserRights     in number,
            pi_nTargetPortalID in number,
         */

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vMenuTitle", strMenuTitle);
        plist.AddInputParameter("pi_nParentID", lParentID);
        plist.AddInputParameter("pi_nPageID", lPageID);
        plist.AddInputParameter("pi_nUserRights", lUserRights);
        plist.AddInputParameter("pi_nTargetPortalID", lTargetPortal);
        plist.AddInputParameter("pi_nSortOrder", lSortOrder);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.InsertMenuItem",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    public bool UpdatetMenuItem(long lMenuID,
                                string strMenuTitle,
                                long lParentID,
                                long lPageID,
                                long lUserRights,
                                long lTargetPortal,
                                long lSortOrder)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        /*
            pi_vMenuTitle      in varchar2,
            pi_nParentID       in number,
            pi_nPageID         in number,
            pi_nUserRights     in number,
            pi_nTargetPortalID in number,
         */

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMenuID", lMenuID);
        plist.AddInputParameter("pi_vMenuTitle", strMenuTitle);
        plist.AddInputParameter("pi_nParentID", lParentID);
        plist.AddInputParameter("pi_nPageID", lPageID);
        plist.AddInputParameter("pi_nUserRights", lUserRights);
        plist.AddInputParameter("pi_nTargetPortalID", lTargetPortal);
        plist.AddInputParameter("pi_nSortOrder", lSortOrder);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.UpdateMenuItem",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    public bool DeleteMenuItem(long lMenuID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nMenuID", lMenuID);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.DeleteMenuItem",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    public DataSet GetMenuRootLevelDS(long lPortalID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nPortalID", lPortalID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetMenuRootLevelRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetMenuItemsDS(long lPortalID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nPortalID", lPortalID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetMenuItemsRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public bool DeleteCMSPage(long lMenuID)
    {

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nPageID", lMenuID);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CMS.DeleteCMSPage",
                                        plist,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }
        return false;
    }

    public DataSet GetTemplatesListDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CMS.GetTemplatesListRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

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