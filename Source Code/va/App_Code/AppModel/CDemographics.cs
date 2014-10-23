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
    public void LoadGenderCheckList(BaseMaster BaseMstr, CheckBoxList chklst)
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


    //get a dataset of Genders
    public DataSet GetDevicesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DEVICES.GetDevicesRS",
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

    public void LoadDevicesCombo(BaseMaster BaseMstr, DropDownList cbo) 
    {
        DataSet ds = this.GetDevicesDS(BaseMstr);
        cbo.DataSource = ds;
        cbo.DataValueField = "DEVICE_ID";
        cbo.DataTextField = "SERIAL_NUMBER";
        cbo.DataBind();

        cbo.Items.Insert(0, new ListItem("-- Select Serial Number --", "-1"));
        cbo.Items.Insert(1, new ListItem("Add Device", "0"));
    }

}
