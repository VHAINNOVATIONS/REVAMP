using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
//using Oracle.DataAccess.Client;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CDataSet
    {
        //members/fields
        private DataSet m_DataSet;
        private string m_strStatus;
        private long m_lStatusCode;
        
        /// <summary>
        /// get status
        /// </summary>
        public string Status
        {
            get
            {
                return m_strStatus;
            }
        }

        /// <summary>
        /// get status code
        /// </summary>
        public long StatusCode
        {
            get
            {
                return m_lStatusCode;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public CDataSet()
        {
            m_DataSet = null;
            m_strStatus = "";
            m_lStatusCode = -1;
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~CDataSet()
        {
            if (m_DataSet != null)
            {
                m_DataSet = null;
            }
        }

        /// <summary>
        /// append all the columns and data from one ds (dsfrom) to another ds (dsto)
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dsFrom"></param>
        /// <param name="dsTo"></param>
        /// <returns></returns>
        public bool AppendData( CDataConnection conn,
                                DataSet dsFrom,
                                DataSet dsTo )
        {
            //keep the offeset of where the new columns start
            int nStartIndex = dsTo.Tables[0].Columns.Count;

            //loop over all the tables in the new ds and the cols to the original ds
            foreach (System.Data.DataTable table in dsFrom.Tables)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    try
                    {
                        //add the new cols to the original dataset 
                        System.Data.DataColumn colnew = new System.Data.DataColumn();

                        colnew.AllowDBNull = col.AllowDBNull;
                        colnew.AutoIncrement = col.AutoIncrement;
                        colnew.AutoIncrementSeed = col.AutoIncrementSeed;
                        colnew.AutoIncrementStep = col.AutoIncrementStep;
                        colnew.Caption = col.Caption;
                        colnew.ColumnMapping = col.ColumnMapping;
                        colnew.ColumnName = col.ColumnName;
                        //colnew.Container = col.Container;
                        colnew.DataType = col.DataType;
                        colnew.DateTimeMode = col.DateTimeMode;
                        colnew.DefaultValue = col.DefaultValue;
                        //colnew.DesignMode = col.DesignMode;
                        colnew.Expression = col.Expression;
                        //colnew.ExtendedProperties = col.ExtendedProperties;
                        colnew.MaxLength = col.MaxLength;
                        colnew.Namespace = col.Namespace;
                        //colnew.Ordinal = col.Ordinal;
                        colnew.Prefix = col.Prefix;
                        colnew.ReadOnly = col.ReadOnly;
                        colnew.Site = col.Site;
                        //colnew.Table = col.Table;
                        colnew.Unique = col.Unique;

                        //add the new column to the table
                        dsTo.Tables[0].Columns.Add(colnew);
                    }
                    catch (Exception ee)
                    {
                        string strError = ee.Message;
                        //ignore is already there
                    }
                }

                //accept any changes made
                dsTo.AcceptChanges();
            }

            //loop over all the rows in the table of the new ds
            //and add to original dataset
            foreach (System.Data.DataTable table in dsFrom.Tables)
            {
                foreach (System.Data.DataRow row in table.Rows)
                {
                    //loop over all the column values
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        //loop over the "to" table and set the new col value = this col value
                        foreach (System.Data.DataTable table1 in dsTo.Tables)
                        {
                            foreach (System.Data.DataRow row1 in table1.Rows)
                            {
                                //loop over all the rows in the "to" table to find the new one
                                for (int ii = nStartIndex; ii < row1.ItemArray.Length; ii++)
                                {
                                    if (row1.Table.Columns[ii].ColumnName == row.Table.Columns[i].ColumnName)
                                    {
                                        //set the rows value
                                        row1[ii] = row[i];

                                        //increment the start index to make the loop execute faster
                                        nStartIndex++;

                                        //break the for
                                        break;
                                    }
                                }
                            }
                        }

                        //accept any changes made
                        dsTo.AcceptChanges();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// get a dataset from the connection and a stored proc
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="strSPName"></param>
        /// <param name="ParamList"></param>
        /// <param name="lStatusCode"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
      /*  public DataSet GetOracleDataSet(CDataConnection conn, 
                                        string strSPName, 
                                        CDataParameterList ParamList,
                                        out long lStatusCode,
                                        out string strStatus)
        {
            lStatusCode = 0;
            strStatus = "";
            m_lStatusCode = 0;
            m_strStatus = "";

            CDataUtils utils = new CDataUtils();
            string strAuditXML = "";
            strAuditXML += "<sp_name>" + strSPName + "</sp_name>";
            
            //return null if no conn
            if (conn == null)
            {
                m_lStatusCode = 1;
                m_strStatus = "Unable to connect to data source, CDataConnection is null";
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
                return null;
            }

            //create a new command object and set the command objects connection, text and type
            //must use OracleCommand or you cannot get back a ref cur out param which is how
            //we do things in medbase
            OracleCommand cmd = new OracleCommand(); // OleDbCommand();
            cmd.Connection = conn.GetOracleConnection();
            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;

            //add the parameters from the parameter list to the command parameter list
            for (int i = 0; i < ParamList.Count; i++)
            {
                CDataParameter parameter = ParamList.GetItemByIndex(i);
                if (parameter != null)
                {   
                    //create a new oledb param from our param and add it to the list
                    //this follows how we currently do it in medbase
                    //TODO: get direction, length etc from the parameter not hard coded below
                    OracleParameter oraParameter = new OracleParameter();
                    oraParameter.ParameterName = parameter.ParameterName;

                    strAuditXML += "<" + oraParameter.ParameterName + ">";

                    //set the parameter value, default to string. Probably a better way than the
                    //if then else, but this works and we can find it later,
                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                    {
                        oraParameter.Value = parameter.StringParameterValue;

                        //audit value
                        strAuditXML += parameter.StringParameterValue;

                    }
                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                    {
                        oraParameter.Value = parameter.LongParameterValue;

                        //audit value
                        strAuditXML += Convert.ToString(parameter.LongParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                    {
                        oraParameter.Value = parameter.DateParameterValue;

                        //audit value
                        strAuditXML += utils.GetDateAsString(parameter.DateParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.CLOBParameter)
                    {
                        oraParameter.Value = parameter.CLOBParameterValue;

                        //audit value
                        strAuditXML += parameter.CLOBParameterValue;
                    }
                    else
                    {
                        oraParameter.Value = parameter.StringParameterValue;
               
                        //audit value
                        strAuditXML += parameter.StringParameterValue;
                    }

                    strAuditXML += "</" + oraParameter.ParameterName + ">";

                    oraParameter.Direction = parameter.Direction;
                    cmd.Parameters.Add(oraParameter);
                }
            }
                    
            //add in out params for stored proc, all sp's will return a status 1 = good, 0 = bad
            //status
            ParamList.AddParameter("po_nStatusCode", 0, ParameterDirection.Output);
            OracleParameter oraStatusParameter = new OracleParameter("po_nStatusCode",
                                                                       OracleType.Int32);
            oraStatusParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraStatusParameter);
            //
            //comment
            ParamList.AddParameter("po_vStatusComment", "", ParameterDirection.Output);
            OracleParameter oraCommentParameter = new OracleParameter("po_vStatusComment",
                                                                        OracleType.VarChar,
                                                                        4000);
            oraCommentParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraCommentParameter);
            // 
            //now add an out parameter to hold the ref cursor to the commands parameter list
            //returned ref cursor must always be named "RS" because OracleClient binds these 
            //parameters by name, so you must name your parameter correctly
            //so the OracleParameter must be named the same thing.
            OracleParameter oraRSParameter = new OracleParameter( "RS",
                                                                    OracleType.Cursor);
                                                                   //OracleType.Cursor
            oraRSParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraRSParameter);

            //create a new dataset to hold the conntent of the reference cursor
            m_DataSet = new DataSet();
            
            //now audit the call.... ignore audit and get/set session values or
            //login is audited seperately
            if (conn.Audit)
            {
                if (strSPName.ToUpper().IndexOf("AUDIT") > -1 ||
                    strSPName.ToUpper().IndexOf("GETSESSIONVALUE") > -1 ||
                    strSPName.ToUpper().IndexOf("SETSESSIONVALUE") > -1 ||
                    strSPName.ToUpper().IndexOf("LOGIN") > -1)
                {
                    //ignore the audit
                }
                else
                {
                    //audit the transaction
                    CDataParameterList plistAudit = new CDataParameterList();
                    plistAudit.AddInputParameter("pi_vSessionID", ParamList.GetItemByName("pi_vSessionID").StringParameterValue);
                    plistAudit.AddInputParameter("pi_vSessionClientIP", ParamList.GetItemByName("pi_vSessionClientIP").StringParameterValue);
                    plistAudit.AddInputParameter("pi_nUserID", ParamList.GetItemByName("pi_nUserID").LongParameterValue);
                    plistAudit.AddInputParameter("pi_vSPName", strSPName);
                    plistAudit.AddInputParameterCLOB("pi_clAuditXML", strAuditXML);

                    long lStat = 0;
                    string strStat = "";
                    conn.ExecuteOracleSP("PCK_FX_SEC.AuditTransaction",
                                    plistAudit,
                                    out lStat,
                                    out strStat);
                }
            }
                        
            //create an adapter and fill the dataset. I like datasets because they are completely
            //disconnected and provide the most flexibility for later porting to a web service etc.
            //It could be argued that a data reader is faster and offers easier movement back and forth
            //through a dataset. But for the web and the fact that we work from lists
            //I think a dataset is best. Concept is similar to current medbase architecture
            try
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                dataAdapter.Fill(m_DataSet);
            }
            catch (InvalidOperationException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
                m_DataSet = null;
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
            }
            catch (OracleException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
                m_DataSet = null;
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
            }

            if (m_lStatusCode == 0)
            {
                //now read back out params into our list
                for (int i = 0; i < ParamList.Count; i++)
                {
                    CDataParameter parameter = ParamList.GetItemByIndex(i);
                    if (parameter != null)
                    {
                        if (parameter.Direction == ParameterDirection.Output ||
                            parameter.Direction == ParameterDirection.InputOutput)
                        {
                            foreach (OracleParameter oP in cmd.Parameters)
                            {
                                if (oP.ParameterName.Equals(parameter.ParameterName))
                                {
                                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            parameter.StringParameterValue = oP.Value.ToString();
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (!oP.Value.ToString().Equals(""))
                                            {
                                                parameter.LongParameterValue = Convert.ToInt64(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (!oP.Value.ToString().Equals(""))
                                            {
                                                parameter.DateParameterValue = Convert.ToDateTime(oP.Value);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        parameter.StringParameterValue = oP.Value.ToString();
                                    }
                                }
                            }
                        }
                    }
                }

                //set status code and text
                CDataParameter pStatusCode = ParamList.GetItemByName("po_nStatusCode");
                if (pStatusCode != null)
                {
                    m_lStatusCode = pStatusCode.LongParameterValue;

                }
                CDataParameter pStatusComment = ParamList.GetItemByName("po_vStatusComment");
                if (pStatusComment != null)
                {
                    m_strStatus = pStatusComment.StringParameterValue;
                }
            }

            lStatusCode = m_lStatusCode;
            strStatus = m_strStatus;
            
            return m_DataSet;
        }*/

        /// <summary>
        /// get a dataset from the connection and a stored proc
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="strSPName"></param>
        /// <param name="ParamList"></param>
        /// <param name="lStatusCode"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public DataSet GetOracleDataSet(CDataConnection conn,
                                        string strSPName,
                                        CDataParameterList ParamList,
                                        out long lStatusCode,
                                        out string strStatus)
        {
            lStatusCode = 0;
            strStatus = "";
            m_lStatusCode = 0;
            m_strStatus = "";

            CDataUtils utils = new CDataUtils();

            //return null if no conn
            if (conn == null)
            {
                m_lStatusCode = 1;
                m_strStatus = "Unable to connect to data source, CDataConnection is null";
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
                return null;
            }

            //create a new command object and set the command objects connection, text and type
            //must use OracleCommand or you cannot get back a ref cur out param which is how
            //we do things in medbase
            OracleCommand cmd = new OracleCommand(); // OleDbCommand();
            cmd.Connection = conn.GetOracleConnection();
            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;

            //add the parameters from the parameter list to the command parameter list
            for (int i = 0; i < ParamList.Count; i++)
            {
                CDataParameter parameter = ParamList.GetItemByIndex(i);
                if (parameter != null)
                {
                    //create a new oledb param from our param and add it to the list
                    //this follows how we currently do it in medbase
                    //TODO: get direction, length etc from the parameter not hard coded below
                    OracleParameter oraParameter = new OracleParameter();
                    oraParameter.ParameterName = parameter.ParameterName;

                    //set the parameter value, default to string. Probably a better way than the
                    //if then else, but this works and we can find it later,
                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                    {
                        oraParameter.Value = parameter.LongParameterValue;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                    {
                        oraParameter.Value = parameter.DateParameterValue;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.CLOBParameter)
                    {
                        oraParameter.Value = parameter.CLOBParameterValue;
                    }
                    else
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                    }

                    oraParameter.Direction = parameter.Direction;
                    cmd.Parameters.Add(oraParameter);
                }
            }

            //add in out params for stored proc, all sp's will return a status 1 = good, 0 = bad
            //status
            ParamList.AddParameter("po_nStatusCode", 0, ParameterDirection.Output);
            OracleParameter oraStatusParameter = new OracleParameter("po_nStatusCode",
                                                                       OracleType.Int32);
            oraStatusParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraStatusParameter);
            //
            //comment
            ParamList.AddParameter("po_vStatusComment", "", ParameterDirection.Output);
            OracleParameter oraCommentParameter = new OracleParameter("po_vStatusComment",
                                                                        OracleType.VarChar,
                                                                        4000);
            oraCommentParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraCommentParameter);
            // 
            //now add an out parameter to hold the ref cursor to the commands parameter list
            //returned ref cursor must always be named "RS" because OracleClient binds these 
            //parameters by name, so you must name your parameter correctly
            //so the OracleParameter must be named the same thing.
            OracleParameter oraRSParameter = new OracleParameter("RS",
                                                                    OracleType.Cursor);
            //OracleType.Cursor
            oraRSParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(oraRSParameter);

            //create a new dataset to hold the conntent of the reference cursor
            m_DataSet = new DataSet();

            //create an adapter and fill the dataset. I like datasets because they are completely
            //disconnected and provide the most flexibility for later porting to a web service etc.
            //It could be argued that a data reader is faster and offers easier movement back and forth
            //through a dataset. But for the web and the fact that we work from lists
            //I think a dataset is best. Concept is similar to current medbase architecture
            try
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                dataAdapter.Fill(m_DataSet);
            }
            catch (InvalidOperationException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
                m_DataSet = null;
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
            }
            catch (OracleException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
                m_DataSet = null;
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
            }

            if (m_lStatusCode == 0)
            {
                //now read back out params into our list
                for (int i = 0; i < ParamList.Count; i++)
                {
                    CDataParameter parameter = ParamList.GetItemByIndex(i);
                    if (parameter != null)
                    {
                        if (parameter.Direction == ParameterDirection.Output ||
                            parameter.Direction == ParameterDirection.InputOutput)
                        {
                            foreach (OracleParameter oP in cmd.Parameters)
                            {
                                if (oP.ParameterName.Equals(parameter.ParameterName))
                                {
                                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            parameter.StringParameterValue = oP.Value.ToString();
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (!oP.Value.ToString().Equals(""))
                                            {
                                                parameter.LongParameterValue = Convert.ToInt64(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (!oP.Value.ToString().Equals(""))
                                            {
                                                parameter.DateParameterValue = Convert.ToDateTime(oP.Value);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        parameter.StringParameterValue = oP.Value.ToString();
                                    }
                                }
                            }
                        }
                    }
                }

                //set status code and text
                CDataParameter pStatusCode = ParamList.GetItemByName("po_nStatusCode");
                if (pStatusCode != null)
                {
                    m_lStatusCode = pStatusCode.LongParameterValue;

                }
                CDataParameter pStatusComment = ParamList.GetItemByName("po_vStatusComment");
                if (pStatusComment != null)
                {
                    m_strStatus = pStatusComment.StringParameterValue;
                }
            }

            lStatusCode = m_lStatusCode;
            strStatus = m_strStatus;

            //now audit the call if needed.... 
            if (conn.Audit)
            {
                long lAuditStatusCode = 0;
                string strAuditStatus = String.Empty;
                conn.AuditTransaction(strSPName,
                                ParamList,
                                lStatusCode,
                                strStatus,
                                out lAuditStatusCode,
                                out strAuditStatus);
            }

            return m_DataSet;
        }
    }
}
