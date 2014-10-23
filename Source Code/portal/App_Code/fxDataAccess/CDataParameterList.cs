using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace DataAccess
{
    public class CDataParameterList : ArrayList
    {
        /// <summary>
        /// constructor
        /// </summary>
        public CDataParameterList()
        {
            //set the lists initial capacity
            this.Capacity = 25;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public CDataParameterList(BaseMaster BaseMstr)
		{
            //set the lists initial capacity
			this.Capacity = 500;

            //add in standard params from basemstr
            if (BaseMstr != null)
            {
                //use ASP session id NOT DBSession id!
                AddInputParameter("pi_vSessionID", BaseMstr.ASPSessionID);

                AddInputParameter("pi_vSessionClientIP", BaseMstr.ClientIP);
                AddInputParameter("pi_nUserID", BaseMstr.FXUserID);
            }
		}

        public void AddStatusCodeParams()
        {
            long lStatusCode = 0;
            string strStatusComment = "";

            AddOutputParameter("po_nStatusCode", lStatusCode);
            AddOutputParameter("po_vStatusComment", strStatusComment);
        }

       
        /// <summary>
        /// add a date parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public bool AddInputParameter(string strName, DateTime dtValue)
        {
            return AddParameter(strName, dtValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a string parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool AddInputParameter(string strName, string strValue)
        {
            return AddParameter(strName, strValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a string parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool AddInputParameterCLOB(string strName, string strValue)
        {
            return AddCLOBParameter(strName, strValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a long parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="lValue"></param>
        /// <returns></returns>
        public bool AddInputParameter(string strName, long lValue)
        {
            return AddParameter(strName, lValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a bool parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="bValue"></param>
        /// <returns></returns>
        public bool AddInputParameter(string strName, bool bValue)
        {
            return AddParameter(strName, bValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a double parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool AddInputParameter(string strName, double dblValue)
        {
            return AddParameter(strName, dblValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a date parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public bool AddOutputParameter(string strName, DateTime dtValue)
        {
            return AddParameter(strName, dtValue, ParameterDirection.Output);
        }

        /// <summary>
        /// add a string parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool AddOutputParameter(string strName, string strValue)
        {
            return AddParameter(strName, strValue, ParameterDirection.Output);
        }

        /// <summary>
        /// add a long parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="lValue"></param>
        /// <returns></returns>
        public bool AddOutputParameter(string strName, long lValue)
        {
            return AddParameter(strName, lValue, ParameterDirection.Output);
        }

        /// <summary>
        /// add a bool parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="bValue"></param>
        /// <returns></returns>
        public bool AddOutputParameter(string strName, bool bValue)
        {
            return AddParameter(strName, bValue, ParameterDirection.Output);
        }

        /// <summary>
        /// add a double parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool AddOutputParameter(string strName, double dblValue)
        {
            return AddParameter(strName, dblValue, ParameterDirection.Output);
        }

        /// <summary>
        /// add a bool parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="bValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddParameter(string strName, bool bValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;

            p.ParameterType = (int)DataParameterType.BoolParameter;
            p.BoolParameterValue = bValue;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// add a double parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dblValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddParameter(string strName, double dblValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;

            p.ParameterType = (int)DataParameterType.DoubleParameter;
            p.DoubleParameterValue = dblValue;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// add a date parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="dtValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddParameter(string strName, DateTime dtValue, ParameterDirection pmDirection)
        {   
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;
            
            if (dtValue.Year < 1800)
            {
                dtValue = new System.DateTime(0);
            }
           
            p.DateParameterValue = dtValue;
            
            p.ParameterType = (int)DataParameterType.DateParameter;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// add a string parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddParameter(string strName, string strValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;
            p.StringParameterValue = strValue;
            p.ParameterType = (int)DataParameterType.StringParameter;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        public bool AddCLOBParameter(string strName, string strValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;
            p.CLOBParameterValue = strValue;
            p.ParameterType = (int)DataParameterType.CLOBParameter;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// add a long parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="lValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddParameter(string strName, long lValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;
            p.LongParameterValue = lValue;
            p.ParameterType = (int)DataParameterType.LongParameter;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// get an item by ID
        /// </summary>
        /// <param name="strParameterName"></param>
        /// <returns></returns>
        public CDataParameter GetItemByName(string strParameterName)
        {
            foreach (CDataParameter parameter in this)
            {
                if (parameter.ParameterName.Equals(strParameterName))
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// get an item by index
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public CDataParameter GetItemByIndex(int nIndex)
        {
            return (CDataParameter)this[nIndex];
        }

        /// <summary>
        /// add a parameter to the list
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool Add(CDataParameter parameter)
        {
            if (parameter != null)
            {
                this.Add(parameter);
            }

            return true;
        }

        /// <summary>
        /// add a blob paramateter
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="byValue"></param>
        /// <param name="pmDirection"></param>
        /// <returns></returns>
        public bool AddBLOBParameter(string strName, byte[] byValue, ParameterDirection pmDirection)
        {
            CDataParameter p = new CDataParameter();
            p.ParameterName = strName;
            p.BLOBParameterValue = byValue;
            p.ParameterType = (int)DataParameterType.BLOBParameter;
            p.Direction = pmDirection;
            base.Add(p);

            return true;
        }

        /// <summary>
        /// add a blob parameter to the list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool AddInputParameterBLOB(string strName, byte[] byValue)
        {
            return AddBLOBParameter(strName, byValue, ParameterDirection.Input);
        }

        /// <summary>
        /// add a blob parameter to the list from an input control
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool AddInputParameterBLOB(string strName,
                                          System.Web.UI.HtmlControls.HtmlInputFile FileInput)
        {
            //get the contents of the file as a stream
            System.IO.Stream stream = FileInput.PostedFile.InputStream;

            //read the stream to a memory stream
            byte[] buffer = new byte[16 * 1024];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            int nRead;
            while ((nRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, nRead);
            }

            //get a byte array of the full file, this is what we store in oracle
            byte[] byFile = ms.ToArray();

            //add the blob paramater
            return AddBLOBParameter(strName, byFile, ParameterDirection.Input);
        }
    }
}
