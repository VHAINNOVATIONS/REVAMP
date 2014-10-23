using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

public enum DataParameterType : int
{
    StringParameter = 1,
    LongParameter = 2,
    DateParameter = 3,
    IntParameter = 4,
    BoolParameter = 5,
    DoubleParameter = 6,
    CLOBParameter = 7,
    BLOBParameter = 8,
};

namespace DataAccess
{
    public class CDataParameter
    {
        //private parameter name and value
        private string m_strParameterName;
        private string m_strParameterValue;
        private ParameterDirection m_Direction;
        private long m_lParameterValue;
        private int m_nParameterType;
        private DateTime m_dtDateValue;
        private int m_nParameterValue;
        private bool m_bParamaterValue;
        private double m_dblParamaterValue;
        private string m_strCLOBValue;
        private byte[] m_byBLOB;

        /// <summary>
        /// constructor
        /// </summary>
        public CDataParameter()
        {
            m_strParameterName = "";
            m_strParameterValue = "";
            m_lParameterValue = -1;
            m_nParameterValue = -1;
            m_nParameterType = -1;
            m_dblParamaterValue = -1;
            m_bParamaterValue = false;
            m_strCLOBValue = "";
            m_byBLOB = null;

        }

        /// <summary>
        /// override for CDataParameter to string method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (m_nParameterType == (int)DataParameterType.StringParameter)
            {
                return m_strParameterValue;
            }
            else if (m_nParameterType == (int)DataParameterType.LongParameter)
            {
                return Convert.ToString(m_lParameterValue);
            }
            else if (m_nParameterType == (int)DataParameterType.IntParameter)
            {
                return Convert.ToString(m_nParameterValue);
            }
            else if (m_nParameterType == (int)DataParameterType.DateParameter)
            {
                return Convert.ToString(m_dtDateValue);
            }
            else if (m_nParameterType == (int)DataParameterType.BoolParameter)
            {
                return Convert.ToString(m_bParamaterValue);
            }
            else if (m_nParameterType == (int)DataParameterType.DoubleParameter)
            {
                return Convert.ToString(m_dblParamaterValue);
            }
            else if (m_nParameterType == (int)DataParameterType.CLOBParameter)
            {
                return Convert.ToString(m_strCLOBValue);
            }
            else
            {
                return m_strParameterValue;
            }

        }

        /// <summary>
        /// parameter type
        /// </summary>
        public int ParameterType
        {
            get
            {
                return m_nParameterType;
            }
            set
            {
                m_nParameterType = value;
            }
        }

        /// <summary>
        /// parameter direction
        /// </summary>
        public ParameterDirection Direction
        {
            get
            {
                return m_Direction;
            }
            set
            {
                m_Direction = value;
            }
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        public string ParameterName
        {
            get
            {
                return m_strParameterName;
            }
            set
            {
                m_strParameterName = value;
            }
        }

        /// <summary>
        /// Parameter value - long
        /// </summary>
        public long LongParameterValue
        {
            get
            {
                return m_lParameterValue;
            }
            set
            {
                m_lParameterValue = value;
            }
        }

        /// <summary>
        /// parameter value - double
        /// </summary>
        public double DoubleParameterValue
        {
            get
            {
                return m_dblParamaterValue;
            }
            set
            {
                m_dblParamaterValue = value;
            }
        }        

        /// <summary>
        /// Parameter value - long
        /// </summary>
        public bool BoolParameterValue
        {
            get
            {
                return m_bParamaterValue;
            }
            set
            {
                m_bParamaterValue = value;
            }
        }

        /// <summary>
        /// Parameter value - int
        /// </summary>
        public int IntParameterValue
        {
            get
            {
                return m_nParameterValue;
            }
            set
            {
                m_nParameterValue = value;
            }
        }

        /// <summary>
        /// Parameter value - string
        /// </summary>
        public string StringParameterValue
        {
            get
            {
                return m_strParameterValue;
            }
            set
            {
                m_strParameterValue = value;
            }
        }

        /// <summary>
        /// Parameter value - date
        /// </summary>
        public DateTime DateParameterValue
        {
            get
            {
                return m_dtDateValue;
            }
            set
            {
                m_dtDateValue = value;
            }
        }

        /// <summary>
        /// Parameter value - CLOB
        /// </summary>
        public string CLOBParameterValue
        {
            get
            {
                return m_strCLOBValue;
            }
            set
            {
                m_strCLOBValue = value;
            }
        }

        /// <summary>
        /// blob parameter value (byte array)
        /// </summary>
        public byte[] BLOBParameterValue
        {
            get
            {
                return m_byBLOB;
            }
            set
            {
                m_byBLOB = value;
            }
        }
    }
}
