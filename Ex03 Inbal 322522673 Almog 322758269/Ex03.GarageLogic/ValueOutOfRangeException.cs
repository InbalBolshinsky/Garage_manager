using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        float m_minValue;
        float m_maxValue;

        public float MaxValue
        {
            get { return m_maxValue; }
        }

        public float MinValue
        {
            get { return m_minValue; }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) :
            base(string.Format("An error occured while trying to set the value, the allowed range is {0} to {1}.", i_MinValue, i_MaxValue))
        {
            m_minValue = i_MinValue;
            m_maxValue = i_MaxValue;
        }
    }
}
