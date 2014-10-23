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
}
