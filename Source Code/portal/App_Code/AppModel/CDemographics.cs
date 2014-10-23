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
/// Summary description for CDemographics
/// </summary>
public class CDemographics
{
	public CDemographics()
	{
		
    }

    //get a dataset of States
    public DataSet GetStateDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetStateRS",
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

    //load a dropdown list of military services
    public void LoadDemStateDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetStateDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "ABBREBIATION,STATE_TITLE",
                          "STATE_ID",
                          strSelectedID);
    }

    //get a dataset of Genders
    public DataSet GetGenderDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetGenderRS",
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

    //load a dropdown list of Genders
    public void LoadDemGenderDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetGenderDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "GENDER_DESC",
                          "GENDER_ID",
                          strSelectedID);
    }

    //get a dataset of Supervisor Relations
    public DataSet GetSupervisorRelationDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetSupervisorRelationRS",
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

    //load a dropdown list of Supervisor Relations
    public void LoadDemSupervisorRelationDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetSupervisorRelationDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "RELATION_DESC",
                          "RELATION_DESC",
                          strSelectedID);
    }

    //get a dataset of Client Work Performance
    public DataSet GetWorkPerformanceDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetWorkPerformanceRS",
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

    //load a dropdown list of Client Work Performance
    public void LoadDemWorkPerformanceDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetWorkPerformanceDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "SUPERVISORY_ASSESSMENT",
                          "SUPERVISORY_ASSESSMENT",
                          strSelectedID);
    }

    //get a dataset of Relationships
    public DataSet GetRelationshipDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetRelationshipRS",
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

    //get a dataset of Relationships
    public DataSet GetRelationshipSelfDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEMOGRAPHICS.GetRelationshipSelfRS",
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

    //load a dropdown list of Supervisor relationship
    public void LoadDemRelationshipDropDownList(BaseMaster BaseMstr,
                                            DropDownList cbo,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetRelationshipSelfDS(BaseMstr);

        //load the combo
        CDropDownList dl = new CDropDownList();
        dl.RenderDataSet(BaseMstr,
                          ds,
                          cbo,
                          "DESCRIPTION",
                          "RELATIONSHIP_ID",
                          strSelectedID);
    }

    ///////////////////////////////////////////////////////////////////
    //gender
    public void LoadGenderCheckList(BaseMaster BaseMstr,
                                   CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = GetGenderDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "GENDER_DESC",
                          "GENDER_ID");
    }
}
