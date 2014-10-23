using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAccess;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CDiagnosis
/// </summary>
public class CDiagnosis
{
	public CDiagnosis()
	{
	
    }

    public DataSet GetAxisChildItemsDS(BaseMaster BaseMstr, int intAxis)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);
        pList.AddInputParameter("pi_nAxis", intAxis);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetAxisChildItems",
                                            pList,
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

    //render axis selection screen

    public string RenderAxisScreen(BaseMaster BaseMstr, int axisId)
    {
        string str = String.Empty;
        string[] axisTitle = { String.Empty, "Axis1", "Axis2", "Axis3", "Axis4", "Axis5", String.Empty, String.Empty, String.Empty, String.Empty, "Axis10" };

        //DataSet parentDS = this.GetAxisTopLevelDS(BaseMstr, axisId);

        DataSet parentDS = this.GetAxesTitlesDS(BaseMstr);
        DataSet childDS = this.GetAxisChildItemsDS(BaseMstr, axisId);


        bool has_data = false;
        string m_axis = axisTitle[axisId];

        if (parentDS != null)
        {
            parentDS.Tables[0].TableName = "toplevel";
            if (childDS != null)
            {
                childDS.Tables[0].TableName = "childOne";
                parentDS.Tables.Add(childDS.Tables[0].Copy());
                parentDS.AcceptChanges();
                has_data = true;
            }
            else
            {
                has_data = false;
            }
        }

        if (has_data)
        {
            //level zero
            str += "<div id=\"axis_container_" + axisId + "\" style=\"width:520px; height:500px; text-align: left; \" >";
            str += "<div id=\"toplevel\" class=\"level0\" style=\"width:250px; float:left;\">";
            str += "<ul>";
            /*
            if(axisId == 1)
            {
                str += "<li>";
                str += "<a class=\"lastLnk\" href=\"#\" onclick=\"__doPostBack('"
                    + m_axis + "','1')\" >";
                str += "V 71.09 - No Diagnosis";
                str += "</a>";
                str += "</li>";
            }
            */
            foreach (DataRow dr in parentDS.Tables["toplevel"].Rows)
            {
                if (Convert.ToInt32(dr["menu_has_child"]) == 1)
                {
                    str += "<li id=\"li_" + dr["diagnosis_id"] + "\" class=\"hChild selAxis\">";
                    str += "<a level=\"0\" id=\"" + dr["diagnosis_id"]
                        + "\" href=\"#\" onclick=\"toggleNode({id:this.id, classId:1, hasChild:" 
                        + dr["menu_has_child"] + ", isTopLevel:true, axis:" + axisId + "})\" >";
                }
                else
                {
                    str += "<li>";
                    str += "<a class=\"lastLnk\" href=\"#\" onclick=\"__doPostBack('" 
                        + m_axis + "','" + dr["diagnosis_id"] + "')\" >";
                }
                str += dr["diagnosis_title"];
                str += "</a>";
                str += "</li>";
            }
            str += "</ul>";
            str += "</div>";

            //level 1 divs
            str += "<div id=\"level1_container\" style=\"width:250px; float:left;\">";
            DataTable tblLevelOne = parentDS.Tables["childOne"].Clone();
            foreach (DataRow dr in parentDS.Tables["toplevel"].Rows)
            {
                DataRow[] level_one = parentDS.Tables["childOne"].Select("menu_parent_id = " + dr["diagnosis_id"], "");

                foreach (DataRow mdr in level_one)
                {
                    if (Convert.ToInt32(mdr["menu_has_child"]) == 1)
                    {
                        tblLevelOne.ImportRow(mdr);
                    }
                }

                if (level_one.GetLength(0) > 0)
                {
                    str += "<div class=\"level1\" id=\"ChildOf_" + dr["diagnosis_id"] + "\" style=\"display:none;\">";
                    str += "<ul>";
                    for (int a = 0; a < level_one.GetLength(0); a++)
                    {
                        if (Convert.ToInt32(level_one[a]["menu_has_child"]) == 1)
                        {
                            str += "<li class=\"hChild\" id=\"li_" + level_one[a]["diagnosis_id"] + "\">";
                            str += "<a level=\"1\" id=\"" + level_one[a]["diagnosis_id"]
                                        + "\" href=\"#\" onclick=\"toggleNode({id:this.id, classId:2, hasChild:" 
                                        + level_one[a]["menu_has_child"] + ", axis:" + axisId + "})\" >";
                        }
                        else
                        {
                            str += "<li id=\"li_" + level_one[a]["diagnosis_id"] + "\">";
                            str += "<a class=\"lastLnk\" href=\"#\" onclick=\"__doPostBack('" 
                                + m_axis + "','" + level_one[a]["diagnosis_id"] + "')\" >";
                        }

                        str += level_one[a]["diagnosis_title"];
                        str += "</a>";
                        str += "</li>";
                    }
                    str += "</ul>";
                    str += "</div>";
                }
            }
            str += "</div>";
            //level 1 --ends

            //level 2 divs
            str += "<div id=\"level2_container\" style=\"width:250px; float:left;\">";
            DataTable tblLevelTwo = parentDS.Tables["childOne"].Clone();
            foreach (DataRow dr in tblLevelOne.Rows)
            {
                DataRow[] level_two = parentDS.Tables["childOne"].Select("menu_parent_id = " + dr["diagnosis_id"], "");
                foreach (DataRow mdr in level_two)
                {
                    if (Convert.ToInt32(mdr["menu_has_child"]) == 1)
                    {
                        tblLevelTwo.ImportRow(mdr);
                    }
                }
                if (level_two.GetLength(0) > 0)
                {
                    str += "<div class=\"level2\" id=\"ChildOf_" + dr["diagnosis_id"] + "\" style=\"display:none;\">";
                    str += "<ul>";
                    for (int b = 0; b < level_two.GetLength(0); b++)
                    {
                        if (Convert.ToInt32(level_two[b]["menu_has_child"]) == 1)
                        {
                            str += "<li class=\"hChild\" id=\"li_" + level_two[b]["diagnosis_id"] + "\">";
                            str += "<a level=\"2\" id=\"" + level_two[b]["diagnosis_id"]
                                        + "\" href=\"#\" onclick=\"toggleNode({id:this.id, classId:3, hasChild:" 
                                        + level_two[b]["menu_has_child"] + ", axis:" + axisId + "})\" >";
                        }
                        else
                        {
                            str += "<li id=\"li_" + level_two[b]["diagnosis_id"] + "\">";
                            str += "<a class=\"lastLnk\" href=\"#\" onclick=\"__doPostBack('" + m_axis + "','" 
                                + level_two[b]["diagnosis_id"] + "')\" >";
                        }
                        str += level_two[b]["diagnosis_title"];
                        str += "</a>";
                        str += "</li>";
                    }
                    str += "</ul>";
                    str += "</div>";
                }
            }
            str += "</div>";
            //level 2 --ends

            //level 3 divs
            str += "<div id=\"level3_container\" style=\"width:250px; float:left;\">";
            DataTable tblLevelThree = parentDS.Tables["childOne"].Clone();
            foreach (DataRow dr in tblLevelTwo.Rows)
            {
                DataRow[] level_three = parentDS.Tables["childOne"].Select("menu_parent_id = " + dr["diagnosis_id"], "");
                foreach (DataRow mdr in level_three)
                {
                    if (Convert.ToInt32(mdr["menu_has_child"]) == 1)
                    {
                        tblLevelThree.ImportRow(mdr);
                    }
                }
                if (level_three.GetLength(0) > 0)
                {
                    str += "<div class=\"level3\" id=\"ChildOf_" + dr["diagnosis_id"] + "\" style=\"display:none;\">";
                    str += "<ul>";
                    for (int c = 0; c < level_three.GetLength(0); c++)
                    {
                        if (Convert.ToInt32(level_three[c]["menu_has_child"]) == 1)
                        {
                            str += "<li class=\"hChild\" id=\"li_" + level_three[c]["diagnosis_id"] + "\">";
                            str += "<a level=\"3\" id=\"" + level_three[c]["diagnosis_id"]
                                        + "\" href=\"#\" onclick=\"toggleNode({id:this.id, classId:4, hasChild:" 
                                        + level_three[c]["menu_has_child"] + ", axis:" + axisId + "})\" >";
                        }
                        else
                        {
                            str += "<li id=\"li_" + level_three[c]["diagnosis_id"] + "\">";
                            str += "<a class=\"lastLnk\" href=\"#\" onclick=\"__doPostBack('" 
                                + m_axis + "','" + level_three[c]["diagnosis_id"] + "')\" >";
                        }
                        str += level_three[c]["diagnosis_title"];
                        str += "</a>";
                        str += "</li>";
                    }
                    str += "</ul>";
                    str += "</div>";
                }
            }
            str += "</div>";
            //level 3 --ends

            str += "</div>";
        }
        //level zero --ends   

        return str;
    }

    public DataSet LoadSpecifiers(BaseMaster BaseMstr, int diagId)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nDiagId", diagId);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.LoadSpecifiers",
                                            pList,
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
	
	public DataSet GetAllSpecifiersDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);
		
		CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetAllSpecifiersRS",
                                            pList,
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

    //INSERT DIAGNOSIS ---------------------------------------------------
    public bool InsertDiagnosis(BaseMaster BaseMstr,
                                string strPatientID,
                                string strEncounterID,
                                long lTreatmentID,
                                //
                                long lDiagnosisID,
                                long lSpecifierID,
                                string strAxis3Text,
                                out long lNewProblemID)
    {
        lNewProblemID = -1;
        
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        //
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nSpecifierID", lSpecifierID);
        pList.AddInputParameter("pi_vAxis3Text", strAxis3Text);

        pList.AddOutputParameter("po_nProblemID", lNewProblemID);


        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.InsertDiagnosis",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            CDataParameter paramValue = pList.GetItemByName("po_nProblemID");
            lNewProblemID = paramValue.LongParameterValue;
            return true;
        }
        else
        {
            return false;
        }
    }

    //UPDATE DIAGNOSIS ---------------------------------------------------
    public bool UpdateDiagnosis(BaseMaster BaseMstr,
                                string strPatientID,
                                string strEncounterID,
                                long lTreatmentID,
                                long lProblemID,                        
                                long lDiagnosisID,
                                long lSpecifierID,
                                string strAxis3Text,
								string strComment,
                                long lSortOrder)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        //
        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        pList.AddInputParameter("pi_nSpecifierID", lSpecifierID);
        pList.AddInputParameter("pi_vAxis3Text", strAxis3Text);
		pList.AddInputParameter("pi_vComment", strComment);
        pList.AddInputParameter("pi_nSortOrder", lSortOrder);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.UpdateDiagnosis",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateDiagnosisA3(BaseMaster BaseMstr,
                                long lProblemID,
                                string strPatientID,
                                string strDiagText,
                                long lSeeMedRec,
                                long lContributory,
                                string strComment,
                                long lSortOrder)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vDiagText", strDiagText);
        pList.AddInputParameter("pi_nSeeMedRec", lSeeMedRec);
        pList.AddInputParameter("pi_nContributory", lContributory);
        pList.AddInputParameter("pi_vDiagComment", strComment);
        pList.AddInputParameter("pi_nSortOrder", lSortOrder);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.UpdateDiagnosisA3",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataSet GetPotentialProblemsDS(BaseMaster BaseMstr, 
                                            string strPatientID,
                                            string strEncounterID,
                                            long lTreatmentID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);


        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetPotentialProblemsRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetProblemCriteriaDS(BaseMaster BaseMstr,long lProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vEncounterID", BaseMstr.SelectedEncounterID);
        pList.AddInputParameter("pi_nIntkProblemID", lProblemID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetProblemCriteriaRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetCriteriaDefinitionsDS(BaseMaster BaseMstr, long lProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vEncounterID", BaseMstr.SelectedEncounterID);
        pList.AddInputParameter("pi_nIntkProblemID", lProblemID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetCriteriaDefinitionsRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetTxProblemCriteriaDS(
                                        BaseMaster BaseMstr,
                                        long lProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", BaseMstr.SelectedEncounterID);
        pList.AddInputParameter("pi_nTreatmentId", BaseMstr.SelectedTreatmentID);
        pList.AddInputParameter("pi_nProblemID", lProblemID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetTxProblemCriteriaRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetTxProblemCriteriaDefDS(
                                        BaseMaster BaseMstr,
                                        long lProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_vEncounterID", BaseMstr.SelectedEncounterID);
        pList.AddInputParameter("pi_nTreatmentId", BaseMstr.SelectedTreatmentID);
        pList.AddInputParameter("pi_nProblemID", lProblemID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetTxProblemCriteriaDefRS",
                                            pList,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public bool InsertProblemCriteria(BaseMaster BaseMstr,
                                    string strPatientID,
                                    string strEncounterID,
                                    long lTreatmentID,
                                    long lProblemID,
                                    long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", strPatientID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        //
        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);
        //pList.AddInputParameter("pi_nIntakeProblemID", lIntakeProblemID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.InsertCriteriaDefinition",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateCriteria(BaseMaster BaseMstr,
                                long lProblemID,
                                long lCriteriaID,
                                long lStatus)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nCriteriaID", lCriteriaID);
        pList.AddInputParameter("pi_nStatus", lStatus);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.UpdateCriteria",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateCriteriaDefinition(BaseMaster BaseMstr,
                                long lProblemID,
                                long lCriteriaID,
                                long lDefinitionID,
                                long lStatus)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nCriteriaID", lCriteriaID);
        pList.AddInputParameter("pi_nDefinitionID", lDefinitionID);
        pList.AddInputParameter("pi_nStatus", lStatus);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.UpdateCriteriaDefinition",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataSet GetDiagnosisByIdDS(BaseMaster BaseMstr, long lDiagID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nDiagnosisID", lDiagID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetDiagnosisByIdRS",
                                            pList,
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


    public bool DiscontinueDiagnosis(BaseMaster BaseMstr,
                                     long lProblemID,
                                     string strInactiveDesc)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_vInactiveComment", strInactiveDesc);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.DiscontinueDiagnosis",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string BuildQAString(DataSet ds) 
    {
        string strQA = String.Empty;
        long lLastMID = -1;
        long lLastRID = -1;
        if(ds != null)
        {
            foreach (DataTable dt in ds.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    string strWrapper = string.Empty;
                    string strQuestion = string.Empty;
                    string strResponses = string.Empty;
                    
                    if (lLastMID != Convert.ToInt32(dr["mid"]) && lLastRID != Convert.ToInt32(dr["qid"]) && !dr.IsNull("QUESTION"))
                    {
                        DataRow[] drResponses = dt.Select("mid = " + dr["mid"].ToString() + " AND rid = " + dr["rid"].ToString());
                        
                        strWrapper += String.Format("<div mid=\"{0}\" rid=\"{1}\" class=\"criteria-qa-hide\">", dr["mid"].ToString(), dr["rid"].ToString());
                        strQuestion += String.Format("<div class=\"criteria-question\">{0}</div>", dr["QUESTION"].ToString());

                        //get responses
                        strResponses += "<ul style=\"margin-top:5px; margin-bottom: 15px; border-bottom: 1px solid #e1e1e1;\">";
                        foreach (DataRow dr1 in drResponses)
                        {
                            if (Convert.ToInt32(dr["display_type"]) != 1)
                            {
                                strResponses += "<li>" + dr["RESPONSE_VALUE"].ToString() + "</li>";
                            }
                            else
                            {
                                strResponses += "<li>" + dr["RESPONSE"].ToString() + "</li>";
                            }
                        }
                        strResponses += "</ul>";
                        strWrapper += String.Format("{0}{1}</div>", strQuestion, strResponses);
                        strQA += strWrapper;

                        lLastMID = Convert.ToInt32(dr["mid"]);
                        lLastRID = Convert.ToInt32(dr["rid"]);
                    }
                }
            }
        }
        return strQA;
    }

    public bool DiscardPossibleProblem(BaseMaster BaseMstr,
                                        string strEncounterID,
                                        long lIntakeProblemID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);
        
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nIntakeProblemID", lIntakeProblemID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.DiscardPossibleProblem",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public bool UpdateDiagnosisProb(BaseMaster BaseMstr,
                            long lProblemID,
                            long lDiagnosisID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        pList.AddInputParameter("pi_nProblemID", lProblemID);
        pList.AddInputParameter("pi_nDiagnosisID", lDiagnosisID);

        //Execute Oracle stored procedure
        BaseMstr.DBConn.ExecuteOracleSP("PCK_DIAGNOSIS.UpdateDiagnosisProb",
                                        pList,
                                        out lStatusCode,
                                        out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataSet GetAllChildItemsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetAllChildItemsRS",
                                            pList,
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

    protected DataTable GetChildItemsTable(DataTable dtParent, DataSet dsAllDiagnosis, string strTableName)
    {
        DataTable dtChild = dsAllDiagnosis.Tables[0].Clone();
        dtChild.TableName = strTableName;

        if (dtParent != null)
        {
            if (dtParent.Rows.Count > 0)
            {
                dtChild = dsAllDiagnosis.Tables[0].Clone();
                dtChild.TableName = strTableName;

                foreach (DataRow drP in dtParent.Rows)
                {
                    DataRow[] drChild = dsAllDiagnosis.Tables[0].Select("MENU_PARENT_ID = " + drP["DIAGNOSIS_ID"].ToString());
                    foreach (DataRow drC in drChild)
                    {
                        DataRow drNew = dtChild.NewRow();
                        for (int a = 0; a < dtChild.Columns.Count; a++)
                        {
                            drNew[a] = drC[a];
                        }
                        dtChild.Rows.Add(drNew);
                    }
                }
            }
        }
        return dtChild;
    }

    public DataSet GetAxesTitlesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList pList = new CDataParameterList(BaseMstr);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_DIAGNOSIS.GetAxesTitlesRS",
                                            pList,
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
