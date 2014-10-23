using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;

/// <summary>
/// Summary description for CMilitaryRender
/// </summary>
public class CMilitaryRender
{
    CMilitary m_Military = new CMilitary();

	public CMilitaryRender()
	{
	}

    //load a dropdown list of military duty station
    public void LoadMilDutyStationDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryDutyStationDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "SHORT_NAME,BASE,DIMS_ID",
                          "DIMS_ID",
                          strSelectedID);
    }

    //load a dropdown list of military duty station name
    public void LoadMilDutyStationNameDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryDutyStationNameDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "BASE",
                          "DIMS_ID",
                          strSelectedID);
    }

    //load a dropdown list of military services
    public void LoadDMISSquadronCheckList(BaseMaster BaseMstr,
                                           CheckBoxList lst,
                                           string strDMISID)
    {
        //get the data to load
        DataSet ds = m_Military.GetDMISSquadronDS(BaseMstr,
                                                  strDMISID);

        //load the combo
        CCheckBoxList l = new CCheckBoxList();
        l.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "SQUADRON",
                          "SQUADRON_ID");
    }

    //loads a combo with auxilary bases
    public bool LoadAuxBaseCombo(BaseMaster BaseMstr,
                                  string strDMISID,
                                  string strSelectedID,
                                  DropDownList cbo)
    {
        DataSet ds = m_Military.GetDMISAuxDS(BaseMstr,
                                             strDMISID);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "BASE",
                          "AUX_DIMS_ID",
                          strSelectedID);


        return true;
    }

    //loads a combo with auxilary bases
    public bool LoadAuxSquadronList(BaseMaster BaseMstr,
                                     string strDIMSID,
                                     string strAuxDMISID,
                                     ListBox lst)
    {
        DataSet ds = m_Military.GetAuxSquadronDS(BaseMstr,
                                                 strDIMSID,
                                                 strAuxDMISID);

        //load the combo
        CListBox lb = new CListBox();
        lb.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "SQUADRON",
                          "SQUADRON_ID");


        return true;
    }

    //load a dropdown list of military services
    public void LoadDMISSquadronList(BaseMaster BaseMstr,
                                     ListBox lst,
                                     string strDMISID)
    {
        //get the data to load
        DataSet ds = m_Military.GetDMISSquadronDS(BaseMstr,
                                                  strDMISID);

        //load the combo
        CListBox l = new CListBox();
        l.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "SQUADRON",
                          "SQUADRON_ID");
    }

    //load a dropdown list of military services
    public void LoadDMISAllSquadronsList(BaseMaster BaseMstr,
                                     ListBox lst,
                                     string strDMISID)
    {
        //get the data to load by DMISID
        DataSet ds = m_Military.GetDMISAllSquadronsDS(BaseMstr,
                                                      strDMISID);

        //load the combo
        CListBox l = new CListBox();
        l.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "SQUADRON",
                          "SQUADRON_ID");
    }

    //load a dropdown list of majcoms
    public void LoadMAJCOMDropDownList(BaseMaster BaseMstr,
                                       DropDownList cbo,
                                       string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "MAJCOM_TITLE",
                          "MAJCOM_ID",
                          strSelectedID);
    }

    //load a dropdown list of all the majcoms
    public void LoadAllMAJCOMDDL(BaseMaster BaseMstr,
                                       DropDownList cbo,
                                       string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetAllMAJCOMDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "MAJCOM_TITLE",
                          "MAJCOM_ID",
                          strSelectedID);
    }

    //load a dropdown list of the bases for a majcom
    public void LoadAllMAJCOMBaseDDL(BaseMaster BaseMstr,
                                     DropDownList cbo,
                                     long lMAJCOMID,
                                     string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetAllMAJCOMBaseDS(BaseMstr, lMAJCOMID);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "BASE",
                          "DIMS_ID",
                          strSelectedID);
    }

    //load a dropdown list of majcoms
    public void LoadMAJCOMBaseDropDownList(BaseMaster BaseMstr,
                                           DropDownList cbo,
                                           long lMAJCOMID,
                                           string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMBaseDS(BaseMstr, lMAJCOMID);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "BASE",
                          "DIMS_ID",
                          strSelectedID);
    }

    //load a check list of majcoms
    public void LoadMAJCOMCheckList(BaseMaster BaseMstr,
                                     CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "MAJCOM_TITLE",
                          "MAJCOM_ID");
    }

    //load a dropdown list of military services
    public void LoadMAJCOMList(BaseMaster BaseMstr,
                                ListBox lst)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMDS(BaseMstr);

        //load the combo
        CListBox l = new CListBox();
        l.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "MAJCOM_TITLE",
                          "MAJCOM_ID");
    }

    //load a dropdown list of military services
    public void LoadMAJCOMBaseCheckList(BaseMaster BaseMstr,
                                        CheckBoxList chklst,
                                        long lMAJCOMID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMBaseDS(BaseMstr, lMAJCOMID);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "BASE",
                          "DIMS_ID");
    }

    //load a dropdown list of military services
    public void LoadMAJCOMBaseList(BaseMaster BaseMstr,
                                    ListBox lst,
                                    long lMAJCOMID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMAJCOMBaseDS(BaseMstr, lMAJCOMID);

        //load the combo
        CListBox cl = new CListBox();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          lst,
                          "BASE",
                          "DIMS_ID");
    }

    //load a dropdown list of military services
    public void LoadMilServiceDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryServiceDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "MILITARY_SERVICE_TITLE",
                          "MILITARY_SERVICE_ID",
                          strSelectedID);
    }

    //load a dropdown list of military services
    public void LoadMilStatusDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryStatusDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "MILITARY_STATUS_TITLE",
                          "MILITARY_STATUS_ID",
                          strSelectedID);
    }

    //load a dropdown list of military pay grade
    public void LoadMilPayGradeDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryPayGradeDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "GRADE",
                          "RANK_ID",
                          strSelectedID);
    }

    //load a dropdown list of military duty station name
    public void LoadMilSquadronDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID,
                                            string strDMISID)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitarySquadronDS(BaseMstr,
                                                      strDMISID);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "SQUADRON",
                          "SQUADRON_ID",
                          strSelectedID);
    }

    ///////////////////////////////////////////////////////////////////
    //afsc
    public void LoadAFSCCheckList(BaseMaster BaseMstr,
                                   CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetAFSCDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "DUTY_SPECIALTY,DESCRIPTION",
                          "DUTY_SPECIALTY_ID");
    }

    //fmp
    public void LoadFMPCheckList(BaseMaster BaseMstr,
                                   CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetFMPDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "FMP_TITLE,FMP_DESCRIPTION",
                          "FMP_ID");
    }

    //fmp
    public void LoadFMPGroupCheckList(BaseMaster BaseMstr,
                                   CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetFMPGroupDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "GROUP_NAME",
                          "GROUP_ID");
    }



    public void LoadMilServiceCheckList(BaseMaster BaseMstr,
                                         CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryServiceDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "MILITARY_SERVICE_TITLE",
                          "MILITARY_SERVICE_ID");
    }

    public void LoadMilStatusCheckList(BaseMaster BaseMstr,
                                        CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetMilitaryStatusDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "MILITARY_STATUS_TITLE",
                          "MILITARY_STATUS_ID");
    }

    //marital status
    public void LoadMaritalStatusCheckList(BaseMaster BaseMstr,
                                            CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetMaritalStatusDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "MARITAL_STATUS_TITLE",
                          "MARITAL_STATUS_ID");
    }

    //Load the Rank grade CheckBox list
    public void LoadRankCheckList(BaseMaster BaseMstr,
                                   CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = m_Military.GetRankDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "GRADE",
                          "RANK_ID");
    }
}
