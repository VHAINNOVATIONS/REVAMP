using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Threading;
using System.Data;
using System.Data.OracleClient;
using System.Data.Sql;
using System.Data.SqlClient;

//using Oracle.DataAccess.Client;

public enum DataConnectionType : int
{
    Oracle = 1,
};

namespace DataAccess
{
    [Serializable]
    public class CDataConnection
    {
        //private oracleconnection member
        [NonSerialized]
        private OracleConnection m_OracleConnection;

        //connection type, 1= oracle, 2=oledb etc...
        private int m_nConnectionType;

        //todo:
        public bool AddStatusOutputParams { get; private set; }

        //should we audit transactions?
        private bool m_bAudit;
        public bool Audit
        {
            get
            {
                return m_bAudit;
            }
        }

        //status member
        private string m_strStatus;

        //status code member
        private long m_lStatusCode;

        /// <summary>
        /// constructor
        /// </summary>
        public CDataConnection()
        {
            m_OracleConnection = null;
            m_strStatus = "";
            m_nConnectionType = -1;

            AddStatusOutputParams = true;
        }

        /// <summary>
        /// audits a transaction
        /// </summary>
        /// <param name="strSPName"></param>
        /// <param name="ParamList"></param>
        /// <param name="lStatusCode"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public bool AuditTransaction(string strSPName,
                                     CDataParameterList ParamList,
                                     long lTxnStatusCode,
                                     string strTxnStatusComment,
                                     out long lStatusCode,
                                     out string strStatus)
        {
            //init status info 0 = good/ok
            strStatus = "";
            lStatusCode = 0;

            //some transactions do not get audited such as the AUDIT itself, LOGIN is audited seperately
            string strSP = strSPName.ToUpper();
            if (strSP.IndexOf("AUDIT") > -1 ||
                strSP.IndexOf("GETSESSIONVALUE") > -1 ||
                strSP.IndexOf("SETSESSIONVALUE") > -1 ||
                strSP.IndexOf("LOGIN") > -1)
            {
                //ignore the audit
                return true;
            }

            //data utils
            CDataUtils utils = new CDataUtils();

            //full audit string
            StringBuilder sbAudit = new StringBuilder();
            sbAudit.Append(strSPName);
            sbAudit.Append("|");

            //return null if no conn
            if (m_OracleConnection == null)
            {
                lStatusCode = 1;
                strStatus = "Unable to connect to data source, CDataConnection is null";
                return false;
            }

            //add the parameters from the parameter list to the audit string
            for (int i = 0; i < ParamList.Count; i++)
            {
                CDataParameter parameter = ParamList.GetItemByIndex(i);
                if (parameter != null)
                {
                    if (parameter.ParameterName.ToLower() == "pi_vkey")
                    {
                        //do not include encryption key in the audit
                        sbAudit.Append(":key");
                    }
                    else
                    {
                        if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                        {
                            sbAudit.Append(parameter.StringParameterValue);
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                        {
                            sbAudit.Append(Convert.ToString(parameter.LongParameterValue));
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.DoubleParameter)
                        {
                            sbAudit.Append(Convert.ToString(parameter.DoubleParameterValue));
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.IntParameter)
                        {
                            sbAudit.Append(Convert.ToString(parameter.IntParameterValue));
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                        {
                            sbAudit.Append(utils.GetDateTimeAsString(parameter.DateParameterValue));
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.CLOBParameter)
                        {
                            sbAudit.Append(parameter.CLOBParameterValue);
                        }
                        else if (parameter.ParameterType == (int)DataParameterType.BLOBParameter)
                        {
                            sbAudit.Append("BLOB Data");
                        }
                        else
                        {
                            sbAudit.Append(parameter.StringParameterValue);
                        }
                    }

                    sbAudit.Append("|");
                }
            }

            //add in the txn status
            sbAudit.Append(Convert.ToString(lTxnStatusCode));
            sbAudit.Append("|");
            sbAudit.Append(strTxnStatusComment);

            //call the audit SP
            CDataParameterList plistAudit = new CDataParameterList();
            plistAudit.AddInputParameter("pi_vSessionID", ParamList.GetItemByName("pi_vSessionID").StringParameterValue);
            plistAudit.AddInputParameter("pi_vSessionClientIP", ParamList.GetItemByName("pi_vSessionClientIP").StringParameterValue);
            plistAudit.AddInputParameter("pi_nUserID", ParamList.GetItemByName("pi_nUserID").LongParameterValue);
            plistAudit.AddInputParameter("pi_vSPName", strSPName);
            plistAudit.AddInputParameterCLOB("pi_clAuditXML", sbAudit.ToString());
            try
            {
                ExecuteOracleSP("PCK_FX_SEC.AuditTransaction",
                                plistAudit,
                                out lStatusCode,
                                out strStatus);
            }
            catch (InvalidOperationException e)
            {
                strStatus = e.Message;
                lStatusCode = 1;
            }
            catch (OracleException e)
            {
                strStatus = e.Message;
                lStatusCode = 1;
            }

            if (lStatusCode != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// connection type property
        /// </summary>
        public int ConnectionType
        {
            get
            {
                return m_nConnectionType;
            }
        }
        
        /// <summary>
        /// status property
        /// </summary>
        public string Status
        {
            get
            {
                return m_strStatus;
            }
        }

        /// <summary>
        /// status code property
        /// </summary>
        public long StatusCode
        {
            get
            {
                return m_lStatusCode;
            }
        }

        /// <summary>
        /// set a session value
        /// </summary>
        /// <param name="lAppUserID"></param>
        /// <param name="strSessionSource"></param>
        /// <param name="strKeySessionID"></param>
        /// <param name="strKey"></param>
        /// <param name="strKeyValue"></param>
        /// <returns></returns>
        public bool SetSessionValue( long lAppUserID,
                                     string strSessionSource,
                                     string strKeySessionID,
                                     string strKey,
                                     string strKeyValue)
        {
            m_strStatus = "";
            m_lStatusCode = 0;

            //get the SQL for employee lookup from oracle sp
            CDataParameterList parameterList = new CDataParameterList();
            parameterList.AddParameter("pi_nAppUserID", lAppUserID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vAppSessionSource", strSessionSource, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKeySessionID", strKeySessionID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKeyValue", strKeyValue, ParameterDirection.Input);
            if (!ExecuteOracleSP("PCK_SESSION.setSessionValue",
                                    parameterList,
                                    out m_lStatusCode,
                                    out m_strStatus))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// clear a session values
        /// </summary>
        /// <param name="lAppUserID"></param>
        /// <param name="strSessionSource"></param>
        /// <param name="strKeySessionID"></param>
        /// <returns></returns>
        public bool ClearAllSessionValues( long lAppUserID,
                                           string strSessionSource,
                                           string strKeySessionID )
        {
            m_strStatus = "";
            m_lStatusCode = 0;

            //get the SQL for employee lookup from oracle sp
            CDataParameterList parameterList = new CDataParameterList();
            parameterList.AddParameter("pi_nAppUserID", lAppUserID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vAppSessionSource", strSessionSource, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKeySessionID", strKeySessionID, ParameterDirection.Input);
                       
            if (!ExecuteOracleSP("PCK_SESSION.clearAllSessionValues",
                              parameterList,
                              out m_lStatusCode,
                              out m_strStatus))
            {
                m_strStatus = "Error clearing session values.";
                m_lStatusCode = 1;
                return false;
            }

            return true;
        }

        /// <summary>
        /// clear session values
        /// </summary>
        /// <param name="lAppUserID"></param>
        /// <param name="strSessionSource"></param>
        /// <param name="strKeySessionID"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public bool ClearSessionValue( long lAppUserID,
                                       string strSessionSource,
                                       string strKeySessionID,
                                       string strKey)
        {

            m_strStatus = "";
            m_lStatusCode = 0;
           
            //get the SQL for employee lookup from oracle sp
            CDataParameterList parameterList = new CDataParameterList();
            parameterList.AddParameter("pi_nAppUserID", lAppUserID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vAppSessionSource", strSessionSource, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKeySessionID", strKeySessionID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
            if (!ExecuteOracleSP("PCK_SESSION.deleteSessionValue",
                              parameterList,
                              out m_lStatusCode,
                              out m_strStatus))
            {
                m_strStatus = "Error getting session value.";
                m_lStatusCode = 1;
                return false;
            }

            return true;
        }

        /// <summary>
        /// set a session value
        /// </summary>
        /// <param name="lAppUserID"></param>
        /// <param name="strSessionSource"></param>
        /// <param name="strKeySessionID"></param>
        /// <param name="strKey"></param>
        /// <param name="strKeyValue"></param>
        /// <returns></returns>
        public bool GetSessionValue( long lAppUserID,
                                     string strSessionSource,
                                     string strKeySessionID,
                                     string strKey,
                                     out string strKeyValue)
        {
            m_strStatus = "";
            m_lStatusCode = 0;
            strKeyValue = "";

            //get the SQL for employee lookup from oracle sp
            CDataParameterList parameterList = new CDataParameterList();
            parameterList.AddParameter("pi_nAppUserID", lAppUserID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vAppSessionSource", strSessionSource, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKeySessionID", strKeySessionID, ParameterDirection.Input);
            parameterList.AddParameter("pi_vKey", strKey, ParameterDirection.Input);
            parameterList.AddParameter("po_vKeyValue", "", ParameterDirection.Output);
            if (!ExecuteOracleSP("PCK_SESSION.getSessionValue",
                              parameterList,
                              out m_lStatusCode,
                              out m_strStatus))
            {
                m_strStatus = "Error getting session value.";
                m_lStatusCode = 1;
                return false;
            }

            CDataParameter param = new CDataParameter();
            param = parameterList.GetItemByName("po_vKeyValue");
            if (param != null)
            {
                strKeyValue = param.StringParameterValue;
            }

            return true;
        }

      
        /// <summary>
        /// desctructor
        /// </summary>
        ~CDataConnection()
        {
            //we should not really be closing here, the user must call close!!!!
            if (m_OracleConnection != null)
            {
                try
                {
                    m_OracleConnection.Close();
                    m_OracleConnection.Dispose();
                    m_OracleConnection = null;
                }
                catch (OracleException e)
                {
                    string strError = e.Message;
                    m_OracleConnection = null;
                }
                //catch (InvalidOperationException e)
                //{
                //    m_OracleConnection = null;
                //}
            }
        }

        /// <summary>
        /// close the connection
        /// </summary>
        public void Close()
        {
            if (m_OracleConnection != null)
            {
                m_OracleConnection.Close();
                m_OracleConnection.Dispose();
                m_OracleConnection = null;
            }
        }

        /// <summary>
        /// get the underlying oracle connection
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetOracleConnection()
        {
            return m_OracleConnection;
        }

        /// <summary>
        /// executes an oracle stored proc
        /// </summary>
        /// <param name="strSPName"></param>
        /// <param name="ParamList"></param>
        /// <param name="lStatusCode"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        /*public bool ExecuteOracleSP(string strSPName, 
                                    CDataParameterList ParamList,
                                    out long lStatusCode,
                                    out string strStatus)
        {
            CDataUtils utils = new CDataUtils();

            string strAuditXML = "";
            strAuditXML += "<sp_name>" + strSPName + "</sp_name>";

            m_strStatus = "";
            m_lStatusCode = 0;

            //return null if no conn
            if (m_OracleConnection == null)
            {
                m_lStatusCode = 1;
                m_strStatus = "Unable to connect to data source, CDataConnection is null";
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
                return false;
            }
            
            //create a new command object and set the command objects connection, text and type
            //must use OracleCommand or you cannot get back a ref cur out param which is how
            //we do things in medbase
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = m_OracleConnection;
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
                    OracleParameter oraParameter = new OracleParameter();
                    oraParameter.ParameterName = parameter.ParameterName;

                    strAuditXML += "<" + oraParameter.ParameterName + ">";

                    //set the parameter value, default to string. Probably a better way than the
                    //if then else, but this works and we can find it later,
                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                        oraParameter.OracleType = OracleType.VarChar;
                        oraParameter.Size = 4000;
                        
                        //audit value
                        strAuditXML += parameter.StringParameterValue;

                    }
                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                    {
                        oraParameter.Value = parameter.LongParameterValue;
                        oraParameter.OracleType = OracleType.Int32;

                        //audit value
                        strAuditXML += Convert.ToString(parameter.LongParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DoubleParameter)
                    {
                        oraParameter.Value = parameter.DoubleParameterValue;
                        oraParameter.OracleType = OracleType.Double;

                        //audit value
                        strAuditXML += Convert.ToString(parameter.DoubleParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.IntParameter)
                    {
                        oraParameter.Value = parameter.IntParameterValue;
                        oraParameter.OracleType = OracleType.Int32;

                        //audit value
                        strAuditXML += Convert.ToString(parameter.IntParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                    {
                        oraParameter.Value = parameter.DateParameterValue;
                        oraParameter.OracleType = OracleType.DateTime;

                        //audit value
                        strAuditXML += utils.GetDateAsString(parameter.DateParameterValue);
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.CLOBParameter)
                    {
                        //You must begin a transaction before obtaining a temporary LOB. 
                        //Otherwise, the OracleDataReader may fail to obtain data later.
                        OracleTransaction transaction = m_OracleConnection.BeginTransaction();

                        //make a new command
                        OracleCommand command = m_OracleConnection.CreateCommand();
                        command.Connection = m_OracleConnection;
                        command.Transaction = transaction;
                        command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                        command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        
                        //get a temp lob
                        OracleLob tempLob = (OracleLob)command.Parameters[0].Value;
                        
                        //begin batch
                        tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
                        
                        //write the data to the lob 
                        //old, really slow way! 
                        //byte[] bt = new byte[1];
                        //for (int l = 0; l < parameter.CLOBParameterValue.Length; l++)
                        //{
                           // char c = Convert.ToChar(parameter.CLOBParameterValue.Substring(l, 1));
                           // bt[0] = Convert.ToByte(c);
                           // tempLob.Write(bt, 0, 1);
                        //}

                        //convert string to byte array and write to lob, FAST WAY!
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        tempLob.Write(encoding.GetBytes(parameter.CLOBParameterValue),
                                       0,
                                       parameter.CLOBParameterValue.Length);

                        //end batch
                        tempLob.EndBatch();
                        
                        //set the value of the param =  lob
                        oraParameter.OracleType = OracleType.Clob;
                        oraParameter.Value = tempLob;
                        
                        //all done so commit;
                        transaction.Commit();

                        //audit value
                        strAuditXML += parameter.CLOBParameterValue;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.BLOBParameter)
                    {
                        //You must begin a transaction before obtaining a temporary LOB. 
                        //Otherwise, the OracleDataReader may fail to obtain data later.
                        OracleTransaction transaction = m_OracleConnection.BeginTransaction();

                        //make a new command
                        OracleCommand command = m_OracleConnection.CreateCommand();
                        command.Connection = m_OracleConnection;
                        command.Transaction = transaction;
                        command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                        command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();

                        //get a temp lob
                        OracleLob tempLob = (OracleLob)command.Parameters[0].Value;

                        //begin batch
                        tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);

                        //convert string to byte array and write to lob, FAST WAY!
                        tempLob.Write(parameter.BLOBParameterValue,
                                      0,
                                      parameter.BLOBParameterValue.Length);


                        //end batch
                        tempLob.EndBatch();

                        //set the value of the param =  lob
                        oraParameter.OracleType = OracleType.Blob;
                        oraParameter.Value = tempLob;

                        //all done so commit;
                        transaction.Commit();

                        //audit value
                        strAuditXML += "BLOB";
                    }
                    else
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                        oraParameter.OracleType = OracleType.VarChar;
                        oraParameter.Size = 4000;

                        //audit value
                        strAuditXML += parameter.StringParameterValue;
                    }
                    strAuditXML += "</" + oraParameter.ParameterName + ">";

                    oraParameter.Direction = parameter.Direction;
                    cmd.Parameters.Add(oraParameter);
                }
            }

            //add in out params for stored proc, all sp's will return a status 0 = good, 1 = bad
            //
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
            try
            {
                //execute the stored proc and move the out param values back into 
                //our list
                cmd.ExecuteNonQuery();

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
                                                parameter.StringParameterValue = parameter.StringParameterValue.Trim();
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.LongParameterValue = Convert.ToInt64(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DoubleParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.DoubleParameterValue = Convert.ToDouble(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.BoolParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.BoolParameterValue = Convert.ToBoolean(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.IntParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.IntParameterValue = Convert.ToInt32(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                if (!oP.Value.ToString().Equals(""))
                                                {
                                                    parameter.DateParameterValue = Convert.ToDateTime(oP.Value);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (oP.Value != null)
                                        {
                                            parameter.StringParameterValue = oP.Value.ToString();
                                            parameter.StringParameterValue = parameter.StringParameterValue.Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                CDataParameter param = ParamList.GetItemByName("po_nStatusCode");
                if (param != null)
                {
                    m_lStatusCode = param.LongParameterValue;
                    param = ParamList.GetItemByName("po_vStatusComment");
                    if (param != null)
                    {
                        m_strStatus = param.StringParameterValue;
                    }
                }
                else
                {
                    m_lStatusCode = 0;
                    m_strStatus = "";
                }

                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;

                //now audit the call.... ignore audit and get/set session values or
                //login is audited seperately
                //audit the transaction
                if (m_bAudit)
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
                        CDataParameterList plistAudit = new CDataParameterList();
                        plistAudit.AddInputParameter("pi_vSessionID", ParamList.GetItemByName("pi_vSessionID").StringParameterValue);
                        plistAudit.AddInputParameter("pi_vSessionClientIP", ParamList.GetItemByName("pi_vSessionClientIP").StringParameterValue);
                        plistAudit.AddInputParameter("pi_nUserID", ParamList.GetItemByName("pi_nUserID").LongParameterValue);
                        plistAudit.AddInputParameter("pi_vSPName", strSPName);
                        plistAudit.AddInputParameterCLOB("pi_clAuditXML", strAuditXML);

                        long lStat = 0;
                        string strStat = "";
                        ExecuteOracleSP("PCK_FX_SEC.AuditTransaction",
                                        plistAudit,
                                        out lStat,
                                        out strStat);
                    }
                }

                if (lStatusCode != 0)
                {
                    return false;
                }
                                
                return true;
            }
            catch (InvalidOperationException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
            }
            catch (OracleException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
            }

            lStatusCode = m_lStatusCode;
            strStatus = m_strStatus;
            return false;
        }*/

        /// <summary>
        /// executes an oracle stored proc
        /// </summary>
        /// <param name="strSPName"></param>
        /// <param name="ParamList"></param>
        /// <param name="lStatusCode"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public bool ExecuteOracleSP(string strSPName,
                                    CDataParameterList ParamList,
                                    out long lStatusCode,
                                    out string strStatus)
        {
            CDataUtils utils = new CDataUtils();

            m_strStatus = "";
            m_lStatusCode = 0;

            //return null if no conn
            if (m_OracleConnection == null)
            {
                m_lStatusCode = 1;
                m_strStatus = "Unable to connect to data source, CDataConnection is null";
                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;
                return false;
            }

            //create a new command object and set the command objects connection, text and type
            //must use OracleCommand or you cannot get back a ref cur out param which is how
            //we do things in medbase
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = m_OracleConnection;
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
                    OracleParameter oraParameter = new OracleParameter();
                    oraParameter.ParameterName = parameter.ParameterName;

                    //set the parameter value, default to string. Probably a better way than the
                    //if then else, but this works and we can find it later,
                    if (parameter.ParameterType == (int)DataParameterType.StringParameter)
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                        oraParameter.OracleType = OracleType.VarChar;
                        oraParameter.Size = 4000;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                    {
                        oraParameter.Value = parameter.LongParameterValue;
                        oraParameter.OracleType = OracleType.Int32;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DoubleParameter)
                    {
                        oraParameter.Value = parameter.DoubleParameterValue;
                        oraParameter.OracleType = OracleType.Double;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.IntParameter)
                    {
                        oraParameter.Value = parameter.IntParameterValue;
                        oraParameter.OracleType = OracleType.Int32;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                    {
                        //pass in a "real" null date if the year is less than 1800
                        if (parameter.DateParameterValue.Year < 1800)
                        {
                            oraParameter.Value = System.DBNull.Value;
                        }
                        else
                        {
                            oraParameter.Value = parameter.DateParameterValue;
                        }

                        oraParameter.OracleType = OracleType.DateTime;
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.CLOBParameter)
                    {
                        //You must begin a transaction before obtaining a temporary LOB. 
                        //Otherwise, the OracleDataReader may fail to obtain data later.
                        OracleTransaction transaction = m_OracleConnection.BeginTransaction();

                        //make a new command
                        OracleCommand command = m_OracleConnection.CreateCommand();
                        command.Connection = m_OracleConnection;
                        command.Transaction = transaction;
                        command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                        command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();

                        //get a temp lob
                        OracleLob tempLob = (OracleLob)command.Parameters[0].Value;

                        //begin batch
                        tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);

                        //write the data to the lob 
                        //old, really slow way! 
                        //byte[] bt = new byte[1];
                        //for (int l = 0; l < parameter.CLOBParameterValue.Length; l++)
                        //{
                        // char c = Convert.ToChar(parameter.CLOBParameterValue.Substring(l, 1));
                        // bt[0] = Convert.ToByte(c);
                        // tempLob.Write(bt, 0, 1);
                        //}

                        //convert string to byte array and write to lob, FAST WAY!
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        tempLob.Write(encoding.GetBytes(parameter.CLOBParameterValue),
                                       0,
                                       parameter.CLOBParameterValue.Length);

                        //end batch
                        tempLob.EndBatch();

                        //set the value of the param =  lob
                        oraParameter.OracleType = OracleType.Clob;
                        oraParameter.Value = tempLob;

                        //all done so commit;
                        transaction.Commit();
                    }
                    else if (parameter.ParameterType == (int)DataParameterType.BLOBParameter)
                    {
                        //You must begin a transaction before obtaining a temporary LOB. 
                        //Otherwise, the OracleDataReader may fail to obtain data later.
                        OracleTransaction transaction = m_OracleConnection.BeginTransaction();

                        //make a new command
                        OracleCommand command = m_OracleConnection.CreateCommand();
                        command.Connection = m_OracleConnection;
                        command.Transaction = transaction;
                        command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                        command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();

                        //get a temp lob
                        OracleLob tempLob = (OracleLob)command.Parameters[0].Value;

                        //begin batch
                        tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);

                        //convert string to byte array and write to lob, FAST WAY!
                        tempLob.Write(parameter.BLOBParameterValue,
                                      0,
                                      parameter.BLOBParameterValue.Length);


                        //end batch
                        tempLob.EndBatch();

                        //set the value of the param =  lob
                        oraParameter.OracleType = OracleType.Blob;
                        oraParameter.Value = tempLob;

                        //all done so commit;
                        transaction.Commit();
                    }
                    else
                    {
                        oraParameter.Value = parameter.StringParameterValue;
                        oraParameter.OracleType = OracleType.VarChar;
                        oraParameter.Size = 4000;
                    }
                    oraParameter.Direction = parameter.Direction;
                    cmd.Parameters.Add(oraParameter);
                }
            }

            //add in out params for stored proc, all sp's will return a status 0 = good, 1 = bad
            //
            //status
            if (AddStatusOutputParams) //defaults to true
            {
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
            }

            //
            try
            {
                //execute the stored proc and move the out param values back into 
                //our list
                cmd.ExecuteNonQuery();

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
                                            parameter.StringParameterValue = parameter.StringParameterValue.Trim();
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.LongParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.LongParameterValue = Convert.ToInt64(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DoubleParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.DoubleParameterValue = Convert.ToDouble(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.BoolParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.BoolParameterValue = Convert.ToBoolean(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.IntParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                parameter.IntParameterValue = Convert.ToInt32(oP.Value);
                                            }
                                        }
                                    }
                                    else if (parameter.ParameterType == (int)DataParameterType.DateParameter)
                                    {
                                        if (oP.Value != null)
                                        {
                                            if (oP.Value.ToString() != "")
                                            {
                                                if (!oP.Value.ToString().Equals(""))
                                                {
                                                    parameter.DateParameterValue = Convert.ToDateTime(oP.Value);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (oP.Value != null)
                                        {
                                            parameter.StringParameterValue = oP.Value.ToString();
                                            parameter.StringParameterValue = parameter.StringParameterValue.Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                CDataParameter param = ParamList.GetItemByName("po_nStatusCode");
                if (param != null)
                {
                    m_lStatusCode = param.LongParameterValue;
                    param = ParamList.GetItemByName("po_vStatusComment");
                    if (param != null)
                    {
                        m_strStatus = param.StringParameterValue;
                    }
                }
                else
                {
                    m_lStatusCode = 0;
                    m_strStatus = "";
                }

                lStatusCode = m_lStatusCode;
                strStatus = m_strStatus;

                //now audit the call if needed....
                if (m_bAudit)
                {
                    long lAuditStatusCode = 0;
                    string strAuditStatus = String.Empty;
                    AuditTransaction(strSPName,
                                    ParamList,
                                    lStatusCode,
                                    strStatus,
                                    out lAuditStatusCode,
                                    out strAuditStatus);
                }

                if (lStatusCode != 0)
                {
                    return false;
                }

                return true;
            }
            catch (InvalidOperationException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
            }
            catch (OracleException e)
            {
                m_strStatus = e.Message;
                m_lStatusCode = 1;
            }

            lStatusCode = m_lStatusCode;
            strStatus = m_strStatus;
            return false;
        }


        /// <summary>
        /// connect the data connection
        /// </summary>
        /// <param name="strConnectionString"></param>
        /// <param name="nConnectionType"></param>
        /// <returns></returns>
        public bool Connect(string strConnectionString, int nConnectionType, bool bAudit)
        {
            m_strStatus = "";
            m_nConnectionType = nConnectionType;
            m_bAudit = bAudit;

            if (m_nConnectionType == (int)DataConnectionType.Oracle)
            {
                //close connection if it is open
                if (m_OracleConnection != null)
                {
                    try
                    {
                        m_OracleConnection.Close();
                        m_OracleConnection = null;
                    }
                    catch (OracleException e)
                    {
                        m_OracleConnection = null;
                        m_strStatus = e.Message;
                    }
                }

                //trim the connection string
                string strConnString = strConnectionString;
                strConnString.Trim();

                try
                {
                    m_OracleConnection = new OracleConnection(strConnString);
                    m_OracleConnection.Open();
                    return true;
                }
                catch (OracleException e)
                {
                    m_strStatus = e.Message;
                    m_OracleConnection = null;
                    return false;
                }
                catch (ArgumentException ee)
                {
                    m_strStatus = ee.Message;
                    m_OracleConnection = null;
                    return false;
                }
                catch (Exception eee)
                {
                    m_strStatus = eee.Message;
                    m_OracleConnection = null;
                    return false;
                }
                finally
                {
                    //
                }
            }
            else
            {
                return false;
            }
        }
    }
}