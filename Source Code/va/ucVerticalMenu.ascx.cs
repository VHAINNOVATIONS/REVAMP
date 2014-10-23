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

public partial class ucVerticalMenu : System.Web.UI.UserControl
{
    //get|set basemaster info ----------------------------------------
    protected BaseMaster m_BaseMstr;
    public BaseMaster BaseMstr
    {
        get { return m_BaseMstr; }
        set { m_BaseMstr = value; }
    }
    protected CTreatment treatment = new CTreatment();
    protected CEncounter encounter = new CEncounter();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void RenderVerticalMenu() 
    {
        if (!String.IsNullOrEmpty(BaseMstr.SelectedPatientID))
        {
            DataSet dsTreatments, dsEncounters, dsAssessments;

            if (Session["TREATMENTS_LIST_DS"] == null)
            {
                Session["TREATMENTS_LIST_DS"] = treatment.GetTreatmentListDS(BaseMstr, BaseMstr.SelectedPatientID);
            }
            dsTreatments = (DataSet)Session["TREATMENTS_LIST_DS"];

            if (Session["ENCOUNTERS_LIST_DS"] == null)
            {
                Session["ENCOUNTERS_LIST_DS"] = encounter.GetAllEncounterListDS(BaseMstr, BaseMstr.SelectedPatientID);
            }
            dsEncounters = (DataSet)Session["ENCOUNTERS_LIST_DS"];

            if (Session["ASSESSMENTS_LIST_DS"] == null)
            {
                Session["ASSESSMENTS_LIST_DS"] = encounter.GetAllEncounterIntakeDS(BaseMstr, BaseMstr.SelectedPatientID);
            }
            dsAssessments = (DataSet)Session["ASSESSMENTS_LIST_DS"];


            CVerticalMenu vmMenu = new CVerticalMenu(BaseMstr, dsTreatments, dsEncounters, dsAssessments);
            vmMenu.RenderTreePanel(divVerticalMenu);
        }
    }
}
