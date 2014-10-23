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
/// Summary description for CReports
/// </summary>
public class CReports : CBaseDataClass
{
    bool m_bAltLang = false;

    public CReports(BaseMaster bm)
    {
        m_BaseMstr = bm;
        m_DBConn = m_BaseMstr.DBConn;
        m_strPatientID = m_BaseMstr.SelectedPatientID;
        m_strEncounterID = m_BaseMstr.EncounterID;
        m_nMID = m_BaseMstr.ModuleID;
        m_bAltLang = false;
    }

    public bool GeneratePatientReport( HtmlGenericControl divFlagReview )
    {
        CEncounterIntakePP EncounterIntake = new CEncounterIntakePP( m_BaseMstr );

        if (m_strEncounterID == "")
        {
            ErrorEncounter(m_bAltLang);
            return true;
        }

        DataSet ds = EncounterIntake.GetEncounterFlags(m_strEncounterID);
        if(ds==null)
        {
            ErrorEncounter(m_bAltLang);
            return true;
        }

        //this div holds all the flag info for the report
        divFlagReview.InnerHtml = "";

        //dear patient
        divFlagReview.InnerHtml += GetDearPatientPara(m_bAltLang);
        divFlagReview.InnerHtml += "<br><br><br>";

        ////////////////////////////////////////////////////
        //good news header - only show if we have good flags
        //this will let us use the same report for all types
        //of flags fired by assessments
        ///////////////////////////////////////////////////
        string strGoodFlags = GetFlags(ds, 1, 1);
        if (strGoodFlags.Length > 2)
        {
            divFlagReview.InnerHtml += GetGoodNewsPara(m_bAltLang);
            divFlagReview.InnerHtml += "<br><br><br>";
            //
            //good news flags
            divFlagReview.InnerHtml += strGoodFlags;
            divFlagReview.InnerHtml += "<br><br>";
        }
        ////////////////////////////////////////////////////

        ////////////////////////////////////////////////////
        //critical - only show if we have good flags
        //this will let us use the same report for all types
        //of flags fired by assessments
        ///////////////////////////////////////////////////
        string strCritFlags = GetFlags(ds, 1, 3);
        if (strCritFlags.Length > 2)
        {
            divFlagReview.InnerHtml += GetCriticalPara(m_bAltLang);
            divFlagReview.InnerHtml += "<br><br><br>";
            //critical
            divFlagReview.InnerHtml += strCritFlags;
            divFlagReview.InnerHtml += "<br><br>";
        }
        //////////////////////////////////////////////////

        ////////////////////////////////////////////////////
        //cardiovasc - only show if we have good flags
        //this will let us use the same report for all types
        //of flags fired by assessments
        ///////////////////////////////////////////////////
        string strCVFlags = GetFlags(ds, 1, 4);
        if (strCVFlags.Length > 2)
        {
            divFlagReview.InnerHtml += GetCardiovascularRiskPara(m_bAltLang);
            divFlagReview.InnerHtml += "<br><br><br>";
            //CV
            divFlagReview.InnerHtml += strCVFlags;
            divFlagReview.InnerHtml += "<br><br>";
        }

        ////////////////////////////////////////////////////
        //high - only show if we have good flags
        //this will let us use the same report for all types
        //of flags fired by assessments
        ///////////////////////////////////////////////////
        string strHIGHFlags = GetFlags(ds, 1, 2);
        if (strHIGHFlags.Length > 2)
        {
            divFlagReview.InnerHtml += GetHighPara(m_bAltLang);
            divFlagReview.InnerHtml += "<br><br><br>";
            //
            divFlagReview.InnerHtml += strHIGHFlags;
            divFlagReview.InnerHtml += "<br><br>";
        }

        ///////////////////////////////////////////////////
        //all flags by topic 
        divFlagReview.InnerHtml += GetTopicFlags(ds, 1, m_bAltLang);
        ///////////////////////////////////////////////////
   
        return true;
    }

    public bool GenerateProviderReport(BaseMaster BaseMstr,
                                       string strPatientID,
                                       string strEncounterID,
                                       long   lEncounterIntakeID,
                                       bool   bAltLang,
                                       HtmlGenericControl divFlagReview)
    {
        CEncounterIntakePP EncounterIntake = new CEncounterIntakePP(BaseMstr);
        DataSet ds = EncounterIntake.GetEncounterIntakeFlags(strEncounterID, lEncounterIntakeID);
        if (ds == null)
        {
            return false;
        }

        //this div holds all the flag info for the report
        divFlagReview.InnerHtml = "";

        if (this.HasProviderReportFlags(ds))//do not show flag stuff if no flags
        {
           
            //build a header.........
            string strHeader = "";

            //use the user type to determine the type of report to display
            if (BaseMstr.APPMaster.UserType == 2)
            {
                strHeader += "Provider ";
            }
            else if (BaseMstr.APPMaster.UserType == 3)
            {
                strHeader += "Nurse ";
            }

            //get the patient dataset
            CPatientPP pat = new CPatientPP();
            string strDemographics = "";
            pat.GetDemographicsString(BaseMstr, strPatientID, out strDemographics);

            strHeader += "Report for " + strDemographics;

            strHeader += ". This report is provided for your attention. ";
            strHeader += "The HRA has gathered, scored, interpreted and is now reporting ";
            strHeader += "your participant's self-reported responses. The patient reports the following:";
            strHeader += "<br><br>";

            divFlagReview.InnerHtml += strHeader;

            ////////////////////////////////////////////////////
            //critical - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////

            //

            string strCritFlags = GetFlags(ds, BaseMstr.APPMaster.UserType, 3);
            if (strCritFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderCriticalPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //critical
                divFlagReview.InnerHtml += strCritFlags;
                divFlagReview.InnerHtml += "<br><br>";
            }
            //////////////////////////////////////////////////

            ////////////////////////////////////////////////////
            //cardiovasc - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////
            string strCVFlags = GetFlags(ds, BaseMstr.APPMaster.UserType, 4);
            if (strCVFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderCardiovascularRiskPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //CV
                divFlagReview.InnerHtml += strCVFlags;
                divFlagReview.InnerHtml += "<br><br>";
            }

            ////////////////////////////////////////////////////
            //high - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////
            string strHIGHFlags = GetFlags(ds, BaseMstr.APPMaster.UserType, 2);
            if (strHIGHFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderHighPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //
                divFlagReview.InnerHtml += strHIGHFlags;
                divFlagReview.InnerHtml += "<br><br>";
            }

            ///////////////////////////////////////////////////
            //all flags by topic 
            divFlagReview.InnerHtml += GetProviderTopicFlags(ds, BaseMstr.APPMaster.UserType, bAltLang);
            ///////////////////////////////////////////////////

            divFlagReview.InnerHtml += "<font size=\"-2\"><br/>";
            divFlagReview.InnerHtml += "Privacy Act of 1974, 5 U.S.C 552a, \"No agency shall disclose any record which is ";
            divFlagReview.InnerHtml += "contained in a system of records by any means of communication to any person, or to ";
            divFlagReview.InnerHtml += "another agency, except pursuant to a written request by, or with the prior written ";
            divFlagReview.InnerHtml += "consent of, the individual to whom the record pertains.\"</font>";
        }

        return true;
    }

    public bool GenerateProviderHTMLReport(CDataConnection DBConn,
                                       BaseMaster Mastr,
                                       string strPatientID,
                                       string strEncounterID,
                                       int nReportType,
                                       bool bAltLang,
                                       HtmlGenericControl divFlagReview)
    {
        CPatientPP cpt = new CPatientPP();
        DataSet ds = cpt.GetReportDS(Mastr, strEncounterID, strPatientID, nReportType);

        //this div holds all the flag info for the report
        divFlagReview.InnerHtml = "";

        if (this.HasProviderReportFlags(ds))//do not show flag stuff if no flags
        {
            #region build a header
            //build a header.........
            string strHeader = "";

            //strHeader += "Report for " + strPatInfo;
            strHeader += "This report is provided for your attention. ";
            strHeader += "The HRA has gathered, scored, interpreted and is now reporting ";
            strHeader += "your participant's self-reported responses. The patient reports the following:";
            strHeader += "<br><br>";

            divFlagReview.InnerHtml += strHeader;
            #endregion

            ////////////////////////////////////////////////////
            //critical - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////
            #region Critical flags
            string strCritFlags = GetFlags(ds, nReportType, 3);
            if (strCritFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderCriticalPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //critical
                divFlagReview.InnerHtml += "<ul>";
                divFlagReview.InnerHtml += "<li>" + strCritFlags + "</li>";
                divFlagReview.InnerHtml += "</ul>";
                divFlagReview.InnerHtml += "<br><br>";
            }
            #endregion

            ////////////////////////////////////////////////////
            //cardiovasc - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////
            #region Cardio flags
            string strCVFlags = GetFlags(ds, nReportType, 4);
            if (strCVFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderCardiovascularRiskPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //CV
                divFlagReview.InnerHtml += "<ul>";
                divFlagReview.InnerHtml += "<li>" + strCVFlags + "</li>";
                divFlagReview.InnerHtml += "</ul>";
                divFlagReview.InnerHtml += "<br><br>";
            }
            #endregion

            ////////////////////////////////////////////////////
            //high - only show if we have good flags
            //this will let us use the same report for all types
            //of flags fired by assessments
            ///////////////////////////////////////////////////
            #region High flags
            string strHIGHFlags = GetFlags(ds, nReportType, 2);
            if (strHIGHFlags.Length > 2)
            {
                divFlagReview.InnerHtml += GetProviderHighPara(bAltLang);
                divFlagReview.InnerHtml += "<br><br><br>";
                //
                divFlagReview.InnerHtml += "<ul>";
                divFlagReview.InnerHtml += "<li>" + strHIGHFlags + "</li>";
                divFlagReview.InnerHtml += "</ul>";
                divFlagReview.InnerHtml += "<br><br>";
            }
            #endregion

            ///////////////////////////////////////////////////
            //all flags by topic 
            ///////////////////////////////////////////////////
            divFlagReview.InnerHtml += GetProviderTopicFlags(ds, nReportType, bAltLang);
            

            #region Privacy statement
            divFlagReview.InnerHtml += "<font size=\"-2\"><br/>";
            divFlagReview.InnerHtml += "Privacy Act of 1974, 5 U.S.C 552a, \"No agency shall disclose any record which is ";
            divFlagReview.InnerHtml += "contained in a system of records by any means of communication to any person, or to ";
            divFlagReview.InnerHtml += "another agency, except pursuant to a written request by, or with the prior written ";
            divFlagReview.InnerHtml += "consent of, the individual to whom the record pertains.\"</font>";
            #endregion
        }

        return true;
    }

    public string GetTopicFlags(DataSet ds, long lReportType, bool bAltLang)
    {
        long lCurrentTopicID = -1;
        long lCurrentModuleID = -1;
        string strFlags = "";

        if (ds != null)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("SESSION_FLAG_TX"))
                    {
                        if (!row.IsNull("FLAG_GROUP"))
                        {
                            //JRL - Topic flags do not belong to a flag group
                            long lFlagGroup = Convert.ToInt32(row["FLAG_GROUP"]);
                            if (lFlagGroup == 0)
                            {
                                if (!row.IsNull("REPORT_TYPE_ID"))
                                {
                                    long lReportTypeID = Convert.ToInt32(row["REPORT_TYPE_ID"]);
                                    if (lReportTypeID == lReportType)
                                    {
                                        if (!row.IsNull("MID"))
                                        {
                                            long lMID = Convert.ToInt32(row["MID"]);
                                            if (lMID != lCurrentModuleID)
                                            {
                                                lCurrentTopicID = -1;
                                                lCurrentModuleID = lMID;
                                            }
                                        }

                                        long lTID = Convert.ToInt32(row["TID"]);
                                        if (lTID != lCurrentTopicID)
                                        {

                                            string strPara = "";

                                            strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
                                            strPara += "<tr>";

                                            strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
                                            if (!row.IsNull("TOPIC_IMAGE"))
                                            {
                                                strPara += "<img border=\"0\" src=\"picts/";
                                                strPara += Convert.ToString(row["TOPIC_IMAGE"]);
                                                strPara += "\">";
                                            }
                                            strPara += "</td>";

                                            strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

                                            if (!bAltLang)
                                            {
                                                strPara += "<font class=\"fontGoodNewsHeader\">";
                                                if (!row.IsNull("TOPIC"))
                                                {
                                                    strPara += Convert.ToString(row["TOPIC"]);
                                                }
                                                strPara += "</font>";
                                            }
                                            else
                                            {
                                                strPara += "<font class=\"fontGoodNewsHeader\">";
                                                if (!row.IsNull("TOPIC_ALT"))
                                                {
                                                    strPara += Convert.ToString(row["TOPIC_ALT"]);
                                                }
                                                strPara += "</font>";
                                            }

                                            strPara += "</td>";
                                            strPara += "</tr>";
                                            strPara += "</table><br><br>";

                                            strFlags += strPara;
                                            lCurrentTopicID = lTID;
                                        }

                                        strFlags += Convert.ToString(row["SESSION_FLAG_TX"]);
                                        strFlags += "<br><br>";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return strFlags;
    }

    public bool HasPatientReportFlags(DataSet ds)
    {
        if (ds == null)
            return false;

        if (ds != null)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("SESSION_FLAG_TX"))
                    {
                        if (!row.IsNull("REPORT_TYPE_ID"))
                        {
                            long lReportTypeID = Convert.ToInt32(row["REPORT_TYPE_ID"]);
                            if (lReportTypeID == 1)//1 = patient report
                            {
                                if (!row.IsNull("FLAG_GROUP"))
                                {
                                    long lFlagGroup = Convert.ToInt32(row["FLAG_GROUP"]);
                                    if (lFlagGroup == 1 ||
                                        lFlagGroup == 2 ||
                                        lFlagGroup == 3 ||
                                        lFlagGroup == 4)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    public string GetFlags(DataSet ds, long lReportType, long lGroup)
    {
        string strFlags = "";
        if (ds != null)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("SESSION_FLAG_TX"))
                    {
                        if (!row.IsNull("REPORT_TYPE_ID"))
                        {
                            long lReportTypeID = Convert.ToInt32(row["REPORT_TYPE_ID"]);
                            if (lReportTypeID == lReportType)
                            {
                                if (!row.IsNull("FLAG_GROUP"))
                                {
                                    long lFlagGroup = Convert.ToInt32(row["FLAG_GROUP"]);
                                    if (lFlagGroup == lGroup)
                                    {
                                        string strVal = Convert.ToString(row["SESSION_FLAG_TX"]);
                                        strFlags += strVal + "<br><br>";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        /*    if(strFlags.Length < 1)
            {
                //good news
                if(lGroup == 1)
                {
                    if(!m_bAltLang)
                    {
                        strFlags = "<font class=\"fontGoodNewsFlags\">Congratulations! you have completed your health assessment. Please review the information below for additional details.</font>";
                    }
                    else
                    {
                        strFlags = "<font class=\"fontGoodNewsFlags\">¡Felicidades! Usted ha terminado su evaluación de salud. Por favor revise la información con detalles adicionales que se le ofrece en los siguientes párrafos.</font>";
                    }
                }
                else if (lGroup == 3)//crit
                {
                    if (!m_bAltLang)
                    {
                        strFlags = "<font class=\"fontCriticalItemsFlags\">Congratulations! you have no critical items.</font>";
                    }
                    else
                    {
                        strFlags = "<font class=\"fontCriticalItemsFlags\">¡Felicidades!  Usted no tiene situaciones críticas en este momento.</font>";
                    }
                }
                else if (lGroup == 2)//high
                {
                    if (!m_bAltLang)
                    {
                        strFlags = "<font class=\"fontHiPriorityItemsFlags\">Congratulations! you have no High Priority items.</font>";
                    }
                    else
                    {
                        strFlags = "<font class=\"fontHiPriorityItemsFlags\">¡Felicidades!  Usted no tiene situaciones de Alta Prioridad en este momento.</font>";
                    }
                }
                else if (lGroup == 4)//cv
                {
                    if (!m_bAltLang)
                    {
                        strFlags = "<font class=\"fontHiPriorityItemsFlags\">Congratulations! you have no Cardiovascular items.</font>";
                    }
                    else
                    {
                        strFlags = "<font class=\"fontHiPriorityItemsFlags\">¡Felicidades!  Usted no tiene situaciones de Alta Prioridad en este momento.</font>";
                    }
                }
                else
                {
                    if (!m_bAltLang)
                    {
                        strFlags = "<font class=\"fontGoodNewsFlags\">No Flags</font>";
                    }
                    else
                    {
                        strFlags = "<font class=\"fontGoodNewsFlags\">No Flags</font>";
                    }
                }
            }*/

        return strFlags;
    }

    public string ErrorEncounter(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontGoodNewsHeader\">Thank you for your participation</font>";
        }
        else
        {
            strPara += "<font class=\"fontGoodNewsHeader\">Gracias por su participacion</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetGoodNewsPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        strPara += "    <img border=\"0\" src=\"picts/rpt_good_news.gif\">";
        strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontGoodNewsHeader\">Good News</font>";
        }
        else
        {
            strPara += "<font class=\"fontGoodNewsHeader\">Buenas noticias</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetCriticalPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        strPara += "    <img border=\"0\" src=\"picts/rpt_critical.gif\">";
        strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontCriticalHeader\">Critical Items</font>";
        }
        else
        {
            strPara += "<font class=\"fontCriticalHeader\">Situaciones Críticas</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetCardiovascularRiskPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        strPara += "    <img border=\"0\" src=\"picts/rpt_critical.gif\">";
        strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontCriticalHeader\">Cardiovascular Risk</font>";
        }
        else
        {
            strPara += "<font class=\"fontCriticalHeader\">Riesgo Cardiovascular</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetHighPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        strPara += "    <img border=\"0\" src=\"picts/rpt_high_priority.gif\">";
        strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontHighPriorityHeader\">High Priority Items</font>";
        }
        else
        {
            strPara += "<font class=\"fontHighPriorityHeader\">Situaciones de Alta Prioridad</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetDearPatientPara(bool bAltLang)
    {
        string strPara = "<font class=\"fontDearParagraph\">";
        if (!bAltLang)
        {
            strPara += "Dear Participant<br><br>";
            strPara += "Thank you for taking an interest in your health by completing the provided health assessment. The following report ";
            strPara += "reflects the analysis of your responses. Being aware of your personal healthcare needs is a good step on the road to ";
            strPara += "wellness. We encourage you to take this report to your health care provider on your next visit.";
        }
        else
        {
            strPara += "Apreciado participante<br><br>";
            strPara += "Gracias por demostrar interés por su salud al participar en esta evaluación de salud. El siguiente reporte refleja ";
            strPara += "el análisis de sus respuestas. El estar consciente de sus necesidades de cuidado de salud es un paso importante en ";
            strPara += "el camino lograr un bienestar integral. Le sugerimos llevar este reporte a su proveedor de salud en su próxima visita.";
        }

        strPara += "</font>";

        return strPara;
    }
    ///////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////

    public string GetProviderTopicFlags(DataSet ds, long lReportType, bool bAltLang)
    {
        long lCurrentTopicID = -1;
        string strFlags = "";
        if (ds != null)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("SESSION_FLAG_TX"))
                    {
                        if (!row.IsNull("REPORT_TYPE_ID"))
                        {
                            long lReportTypeID = Convert.ToInt32(row["REPORT_TYPE_ID"]);
                            if (lReportTypeID == lReportType)
                            {
                                if (!row.IsNull("FLAG_GROUP"))
                                {
                                    long lFlagGroup = Convert.ToInt32(row["FLAG_GROUP"]);
                                    if (lFlagGroup == 0)
                                    {
                                        if (!row.IsNull("TID"))
                                        {
                                            long lTID = Convert.ToInt32(row["TID"]);
                                            if (lTID != lCurrentTopicID)
                                            {
                                                // if it isn't the first topic in the list, close the unordered list
                                                // from the last topic
                                                if (lCurrentTopicID != -1)
                                                {
                                                    strFlags += "</ul>";
                                                }
                                                string strPara = "";

                                                strPara += "<table cellspacing=\"0\" cellpadding=\"0\" valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
                                                strPara += "<tr>";

                                                //strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
                                                //if (!row.IsNull("TOPIC_IMAGE"))
                                                //{
                                                //  strPara += "<img border=\"0\" src=\"picts/";
                                                //    strPara += Convert.ToString(row["TOPIC_IMAGE"]);
                                                //    strPara += "\">";
                                                //}
                                                //strPara += "</td>";

                                                strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

                                                if (!bAltLang)
                                                {
                                                    strPara += "<font class=\"fontGoodNewsHeader\">";
                                                    if (!row.IsNull("TOPIC"))
                                                    {
                                                        strPara += Convert.ToString(row["TOPIC"]);
                                                    }
                                                    strPara += "</font>";
                                                }
                                                else
                                                {
                                                    strPara += "<font class=\"fontGoodNewsHeader\">";
                                                    if (!row.IsNull("TOPIC_ALT"))
                                                    {
                                                        strPara += Convert.ToString(row["TOPIC_ALT"]);
                                                    }
                                                    strPara += "</font>";
                                                }

                                                strPara += "</td>";
                                                strPara += "</tr>";
                                                strPara += "</table><br><br>";

                                                strFlags += strPara;
                                                strFlags += "<ul>";
                                                lCurrentTopicID = lTID;
                                            }

                                            strFlags += "<li>";
                                            strFlags += Convert.ToString(row["SESSION_FLAG_TX"]);
                                            strFlags += "</li>";
                                            strFlags += "<br><br>";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // close out last unordered list
        strFlags += "</ul>";
        return strFlags;
    }

    public bool HasProviderReportFlags(DataSet ds)
    {
        if (ds == null)
        {
            return false;
        }
        else
        {
            return true;
        }
        /*
        if (ds != null)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull("SESSION_FLAG_TX"))
                    {
                        if (!row.IsNull("REPORT_TYPE_ID"))
                        {
                            long lReportTypeID = Convert.ToInt32(row["REPORT_TYPE_ID"]);
                            if (lReportTypeID == 1)//1 = patient report
                            {
                                if (!row.IsNull("FLAG_GROUP"))
                                {
                                    long lFlagGroup = Convert.ToInt32(row["FLAG_GROUP"]);
                                    if (lFlagGroup == 1 ||
                                        lFlagGroup == 2 ||
                                        lFlagGroup == 3 ||
                                        lFlagGroup == 4)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;*/
    }
       
   
    public string GetProviderCriticalPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table cellspacing=\"0\" cellpadding=\"0\" valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        //strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        //strPara += "    <img border=\"0\" src=\"picts/rpt_critical.gif\">";
        //strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontCriticalHeader\">Critical Items</font>";
        }
        else
        {
            strPara += "<font class=\"fontCriticalHeader\">Situaciones Críticas</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetProviderCardiovascularRiskPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table cellspacing=\"0\" cellpadding=\"0\" valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        //strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        //strPara += "    <img border=\"0\" src=\"picts/rpt_critical.gif\">";
        //strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontCriticalHeader\">Cardiovascular Risk</font>";
        }
        else
        {
            strPara += "<font class=\"fontCriticalHeader\">Riesgo Cardiovascular</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetProviderHighPara(bool bAltLang)
    {
        string strPara = "";

        strPara += "<table cellspacing=\"0\" cellpadding=\"0\" valign=\"top\" align=\"left\" border=\"0\" width=\"100%\">";
        strPara += "<tr>";

        //strPara += "<td valign=\"top\" align=\"left\"  width=\"15%\">";
        //strPara += "    <img border=\"0\" src=\"picts/rpt_high_priority.gif\">";
        //strPara += "</td>";

        strPara += "<td valign=\"top\" align=\"left\" width=\"85%\">";

        if (!bAltLang)
        {
            strPara += "<font class=\"fontHighPriorityHeader\">High Priority Items</font>";
        }
        else
        {
            strPara += "<font class=\"fontHighPriorityHeader\">Situaciones de Alta Prioridad</font>";
        }
        strPara += "</td>";
        strPara += "</tr>";
        strPara += "</table>";

        return strPara;
    }

    public string GetProviderDearPatientPara(bool bAltLang)
    {
        string strPara = "<font class=\"fontDearParagraph\">";
        if (!bAltLang)
        {
            /*strPara += "Dear Participant<br><br>";

            strPara += "Thank you for taking an interest in your health by completing the provided health assessment. The following report ";
            strPara += "reflects the analysis of your responses. Being aware of your personal healthcare needs is a good step on the road to wellness. We encourage you to take this report to your ";
            strPara += "health care provider on your next visit.";*/
        }
        else
        {
            /*strPara += "Apreciado participante<br><br>";
            strPara += "Gracias por demostrar interés por su salud al participar en esta evaluación de salud. El siguiente reporte refleja el análisis de sus respuestas. El estar consciente de sus necesidades de cuidado de salud es un paso importante en el camino lograr un bienestar integral. Le sugerimos llevar este reporte a su proveedor de salud en su próxima visita. ";
            strPara += "";*/
        }

        strPara += "</font>";

        return strPara;
    }
}
