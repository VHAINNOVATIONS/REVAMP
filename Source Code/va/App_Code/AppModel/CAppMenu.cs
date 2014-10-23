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
/// Summary description for CAppMenu
/// </summary>
public class CAppMenu
{
    public CAppMenu(BaseMaster BaseMstr)
    {
        m_BaseMstr = BaseMstr;
    }

    protected BaseMaster m_BaseMstr;

    // Get Root Level Menu Items
    protected DataSet GetRootLevelItemsDS()
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                           "PCK_APP_MENU.GetMenuRootLevelRS",
                                            pList,
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

    // Get Menu Items (other than root level)
    protected DataSet GetMenuItemsDS()
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                           "PCK_APP_MENU.GetMenuItemsRS",
                                            pList,
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

    // Build the Menu's HTML string
    public string RenderMenuHTML()
    {
        String strMenuHTML = String.Empty;
        DataSet dsRootLevel = GetRootLevelItemsDS();
        DataSet dsMenuItems = GetMenuItemsDS();

        bool bSelectedPatient = (!String.IsNullOrEmpty(m_BaseMstr.SelectedPatientID));
        bool bSelectedEncounter = (!String.IsNullOrEmpty(m_BaseMstr.SelectedEncounterID));
        bool bHasOpenCase = false;
        bool bTIU = m_BaseMstr.APPMaster.TIU;

        if (bSelectedPatient)
        {
            bHasOpenCase = m_BaseMstr.APPMaster.PatientHasOpenCase;
        }

        //menu outter wrapper <UL>
        strMenuHTML += "<ul id=\"simple-horizontal-menu\">\n";

        if (dsRootLevel.Tables[0].Rows.Count > 0)
        {
            foreach (DataTable dt in dsRootLevel.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    bool bItmSelectedPatient = false;
                    bool bItmSelectedEncounter = false;
                    bool bItmHasOpenCase = false;
                    bool bCheckPatLock = false;
                    bool bIgnoreOnTIU = false;

                    string strMenuItem = String.Empty;

                    if (!dr.IsNull("MENU_ITEM"))
                    {
                        bool bClosesPatient = (Convert.ToInt32(dr["CLOSES_PATIENT"]) == 1 && Convert.ToInt32(dr["HAS_CHILDREN"]) == 0);

                        strMenuItem += "<li>\n";
                        strMenuItem += "<a href=\"" + dr["href"].ToString() + "\" ";

                        if (!dr.IsNull("IGNORE_ON_TIU"))
                        {
                            bIgnoreOnTIU = Convert.ToInt32(dr["IGNORE_ON_TIU"]) > 0;
                        }
                        
                        if (!dr.IsNull("HTML_PROPERTY"))
                        {
                            strMenuItem += dr["HTML_PROPERTY"].ToString() + " ";
                        }

                        if (!dr.IsNull("JS_PROPERTY"))
                        {
                            strMenuItem += dr["JS_PROPERTY"].ToString() + " ";
                        }

                        if (bClosesPatient)
                        {
                            strMenuItem += "CLOSES_PATIENT = \"CLOSES_PATIENT\"";
                        }

                        strMenuItem += " >";
                        strMenuItem += dr["MENU_ITEM"].ToString();
                        strMenuItem += "</a>\n";

                        strMenuItem += RenderSubMenusHTML(dsMenuItems, Convert.ToInt32(dr["MENU_ITEM_ID"]));

                        strMenuItem += "</li>\n";
                    }

                    bItmSelectedPatient = (Convert.ToInt32(dr["SELECTED_PATIENT"]) > 0);
                    bItmSelectedEncounter = (Convert.ToInt32(dr["SELECTED_ENCOUNTER"]) > 0);
                    bItmHasOpenCase = (Convert.ToInt32(dr["HAS_OPEN_CASE"]) > 0);
                    bCheckPatLock = (Convert.ToInt32(dr["CHECK_PAT_LOCK"]) > 0);

                    if (!bItmHasOpenCase || bHasOpenCase)
                    {
                        // check if the menu item requires a selected patient
                        // if there is no selected patient the menu item is ignored
                        // if there is a selected patient, replace the patient place holder string ~%P~
                        if (bItmSelectedPatient && !bSelectedPatient)
                        {
                            strMenuItem = String.Empty;
                        }

                        if (bItmSelectedPatient && bSelectedPatient)
                        {
                            strMenuItem = strMenuItem.Replace("~%P~", m_BaseMstr.SelectedPatientID);
                            strMenuItem = strMenuItem.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                        }

                        // check if the menu item requires a selected encounter
                        // if there is no selected encounter the menu item is ignored
                        // if there is a selected encounter, replace the encounter place holder string ~%E~
                        if (bItmSelectedEncounter && !bSelectedEncounter)
                        {
                            strMenuItem = String.Empty;
                        }

                        if (bItmSelectedEncounter && bSelectedEncounter)
                        {
                            strMenuItem = strMenuItem.Replace("~%E~", m_BaseMstr.SelectedEncounterID);
                            // replace the treatment id place holder ~%T~
                            strMenuItem = strMenuItem.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                        }
                    }
                    else
                    {
                        strMenuItem = String.Empty;
                    }

                    if (bCheckPatLock && m_BaseMstr.IsPatientLocked)
                    {
                        strMenuItem = String.Empty;
                    }

                    //check for TIU
                    if (bTIU && bIgnoreOnTIU) 
                    {
                        strMenuItem = String.Empty;
                    }

                    strMenuHTML += strMenuItem;
                }
            }
        }

        //add the cms items
        //Get CMS MENU
        CContentManagement cms = new CContentManagement(m_BaseMstr);
        strMenuHTML += cms.RenderMenuHTML(2); // 1: patient website, 2: provider website

        strMenuHTML += "</ul>";

        return strMenuHTML;
    }

    // Build Sub Menus HTML string (recursive)
    protected string RenderSubMenusHTML(DataSet dsMenuItems, long lParentID)
    {
        string strSubHTML = String.Empty;

        DataRow[] drSubMenus = dsMenuItems.Tables[0].Select("parent_id = " + lParentID.ToString());

        bool bSelectedPatient = (!String.IsNullOrEmpty(m_BaseMstr.SelectedPatientID));
        bool bSelectedEncounter = (!String.IsNullOrEmpty(m_BaseMstr.SelectedEncounterID));
        bool bHasOpenCase = false;
        bool bTIU = m_BaseMstr.APPMaster.TIU;
        bool bIgnoreOnTIU = false;

        if (bSelectedPatient)
        {
            bHasOpenCase = m_BaseMstr.APPMaster.PatientHasOpenCase;
        }

        if (drSubMenus.Length > 0)
        {
            strSubHTML += "<ul>";

            foreach (DataRow dr in drSubMenus)
            {
                bool bItmSelectedPatient = false;
                bool bItmSelectedEncounter = false;
                bool bItmHasOpenCase = false;
                bool bCheckPatLock = false;

                string strMenuItem = String.Empty;

                if (!dr.IsNull("IGNORE_ON_TIU"))
                {
                    bIgnoreOnTIU = Convert.ToInt32(dr["IGNORE_ON_TIU"]) > 0;
                }
                
                if (!dr.IsNull("MENU_ITEM"))
                {
                    bool bClosesPatient = (Convert.ToInt32(dr["CLOSES_PATIENT"]) == 1 && Convert.ToInt32(dr["HAS_CHILDREN"]) == 0);

                    strMenuItem += "<li>\n";
                    strMenuItem += "<a href=\"" + dr["href"].ToString() + "\" ";

                    if (!dr.IsNull("HTML_PROPERTY"))
                    {
                        strMenuItem += dr["HTML_PROPERTY"].ToString() + " ";
                    }

                    if (!dr.IsNull("JS_PROPERTY"))
                    {
                        strMenuItem += dr["JS_PROPERTY"].ToString() + " ";
                    }

                    if (bClosesPatient)
                    {
                        strMenuItem += "CLOSES_PATIENT = \"CLOSES_PATIENT\"";
                    }

                    strMenuItem += " >";
                    strMenuItem += dr["MENU_ITEM"].ToString();
                    strMenuItem += "</a>\n";

                    strMenuItem += RenderSubMenusHTML(dsMenuItems, Convert.ToInt32(dr["MENU_ITEM_ID"]));

                    strMenuItem += "</li>\n";
                }

                bItmSelectedPatient = (Convert.ToInt32(dr["SELECTED_PATIENT"]) > 0);
                bItmSelectedEncounter = (Convert.ToInt32(dr["SELECTED_ENCOUNTER"]) > 0);
                bItmHasOpenCase = (Convert.ToInt32(dr["HAS_OPEN_CASE"]) > 0);
                bCheckPatLock = (Convert.ToInt32(dr["CHECK_PAT_LOCK"]) > 0);

                if (!bItmHasOpenCase || bHasOpenCase)
                {

                    // check if the menu item requires a selected patient
                    // if there is no selected patient the menu item is ignored
                    // if there is a selected patient, replace the patient place holder string ~%P~
                    if (bItmSelectedPatient && !bSelectedPatient)
                    {
                        strMenuItem = String.Empty;
                    }

                    if (bItmSelectedPatient && bSelectedPatient)
                    {
                        strMenuItem = strMenuItem.Replace("~%P~", m_BaseMstr.SelectedPatientID);
                        strMenuItem = strMenuItem.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                    }

                    // check if the menu item requires a selected encounter
                    // if there is no selected encounter the menu item is ignored
                    // if there is a selected encounter, replace the encounter place holder string ~%E~
                    if (bItmSelectedEncounter && !bSelectedEncounter)
                    {
                        strMenuItem = String.Empty;
                    }

                    if (bItmSelectedEncounter && bSelectedEncounter)
                    {
                        strMenuItem = strMenuItem.Replace("~%E~", m_BaseMstr.SelectedEncounterID);
                        // replace the treatment id place holder ~%T~
                        strMenuItem = strMenuItem.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                    }
                }
                else
                {
                    strMenuItem = String.Empty;
                }

                if (bCheckPatLock && m_BaseMstr.IsPatientLocked)
                {
                    strMenuItem = String.Empty;
                }

                // check for TIU
                if (bTIU && bIgnoreOnTIU)
                {
                    strMenuItem = String.Empty;
                }

                strSubHTML += strMenuItem;
            }

            strSubHTML += "</ul>";
        }

        return strSubHTML;
    }

    // Get Toolbar Items
    protected DataSet GetToolbarItemsDS()
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(m_BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                           "PCK_APP_MENU.GetToolbarItemsRS",
                                            pList,
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

    // Build the Toolbar's HTML string
    public string RenderToolbarHTML()
    {
        String strHTML = String.Empty;
        DataSet dsToolbarItems = GetToolbarItemsDS();

        bool bSelectedPatient = (!String.IsNullOrEmpty(m_BaseMstr.SelectedPatientID));
        bool bSelectedEncounter = (!String.IsNullOrEmpty(m_BaseMstr.SelectedEncounterID));
        bool bHasOpenCase = false;
        bool bTIU = m_BaseMstr.APPMaster.TIU;
        bool bIgnoreOnTIU = false;

        if (bSelectedPatient)
        {
            bHasOpenCase = m_BaseMstr.APPMaster.PatientHasOpenCase;
        }

        if (dsToolbarItems.Tables[0].Rows.Count > 0)
        {
            foreach (DataTable dt in dsToolbarItems.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    bool bItmSelectedPatient = false;
                    bool bItmSelectedEncounter = false;
                    bool bItmHasOpenCase = false;
                    bool bCheckPatLock = false;


                    string strToolbarItm = String.Empty;

                    string strHTMLProperty = String.Empty;
                    string strJSProperty = String.Empty;
                    string strTitle = String.Empty;
                    string strImgSrc = String.Empty;
                    string strWidth = String.Empty;
                    string strHeight = String.Empty;
                    string strClosesPatient = String.Empty;

                    bItmSelectedPatient = (Convert.ToInt32(dr["SELECTED_PATIENT"]) > 0);
                    bItmSelectedEncounter = (Convert.ToInt32(dr["SELECTED_ENCOUNTER"]) > 0);
                    bItmHasOpenCase = (Convert.ToInt32(dr["HAS_OPEN_CASE"]) > 0);
                    bCheckPatLock = (Convert.ToInt32(dr["CHECK_PAT_LOCK"]) > 0);

                    bool bClosesPatient = (Convert.ToInt32(dr["CLOSES_PATIENT"]) == 1 && Convert.ToInt32(dr["HAS_CHILDREN"]) == 0);

                    if (!dr.IsNull("IGNORE_ON_TIU"))
                    {
                        bIgnoreOnTIU = Convert.ToInt32(dr["IGNORE_ON_TIU"]) > 0;
                    }
                    
                    if (!dr.IsNull("HTML_PROPERTY"))
                    {
                        strHTMLProperty = dr["HTML_PROPERTY"].ToString();
                    }

                    if (!dr.IsNull("JS_PROPERTY"))
                    {
                        strJSProperty = dr["JS_PROPERTY"].ToString();
                    }

                    if (!dr.IsNull("TITLE"))
                    {
                        strTitle = dr["TITLE"].ToString();
                    }

                    if (!dr.IsNull("IMG_SRC"))
                    {
                        strImgSrc = dr["IMG_SRC"].ToString();
                    }

                    if (!dr.IsNull("WIDTH"))
                    {
                        strWidth = dr["WIDTH"].ToString();
                    }

                    if (!dr.IsNull("HEIGHT"))
                    {
                        strHeight = dr["HEIGHT"].ToString();
                    }

                    if (bClosesPatient)
                    {
                        strClosesPatient = " CLOSES_PATIENT = \"CLOSES_PATIENT\" ";
                    }


                    strToolbarItm += "<div class=\"tbIcon\" " + strJSProperty + " " + strHTMLProperty + " " + strClosesPatient + ">";
                    strToolbarItm += "  <table cellpadding=\"0\" cellspacing=\"0\" width=\"" + strWidth + "\" style=\"margin: 0;\">";
                    strToolbarItm += "      <tr>";
                    strToolbarItm += "          <td height=\"" + strHeight + "\" valign=\"middle\" align=\"center\">";
                    strToolbarItm += "              <img alt=\"" + strTitle + "\" title=\"" + strTitle + "\" ";
                    strToolbarItm += "              src=\"" + strImgSrc + "\" width=\"" + strWidth + "\" height=\"" + strHeight + "\" />";
                    strToolbarItm += "          </td>";
                    strToolbarItm += "      </tr>";
                    strToolbarItm += "  </table>";
                    strToolbarItm += "</div>";

                    if (!bItmHasOpenCase || bHasOpenCase)
                    {
                        // check if the menu item requires a selected patient
                        // if there is no selected patient the menu item is ignored
                        // if there is a selected patient, replace the patient place holder string ~%P~
                        if (bItmSelectedPatient && !bSelectedPatient)
                        {
                            strToolbarItm = String.Empty;
                        }

                        if (bItmSelectedPatient && bSelectedPatient)
                        {
                            strToolbarItm = strToolbarItm.Replace("~%P~", m_BaseMstr.SelectedPatientID);
                            strToolbarItm = strToolbarItm.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                        }

                        // check if the menu item requires a selected encounter
                        // if there is no selected encounter the menu item is ignored
                        // if there is a selected encounter, replace the encounter place holder string ~%E~
                        if (bItmSelectedEncounter && !bSelectedEncounter)
                        {
                            strToolbarItm = String.Empty;
                        }

                        if (bItmSelectedEncounter && bSelectedEncounter)
                        {
                            strToolbarItm = strToolbarItm.Replace("~%E~", m_BaseMstr.SelectedEncounterID);
                            // replace the treatment id place holder ~%T~
                            strToolbarItm = strToolbarItm.Replace("~%T~", m_BaseMstr.SelectedTreatmentID.ToString());
                        }
                    }
                    else
                    {
                        strToolbarItm = String.Empty;
                    }

                    if (bCheckPatLock && m_BaseMstr.IsPatientLocked)
                    {
                        strToolbarItm = String.Empty;
                    }

                    //check for TIU
                    if (bTIU && bIgnoreOnTIU) 
                    {
                        strToolbarItm = String.Empty;
                    }

                    strHTML += strToolbarItm;
                }
            }
        }

        return strHTML;
    }

}