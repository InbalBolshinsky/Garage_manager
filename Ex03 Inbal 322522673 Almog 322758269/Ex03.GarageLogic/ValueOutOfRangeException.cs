using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ValueOutOfRangeException : Exception
    {
        float MinValue;
        float MaxValue;

        public float MaxValue
        {
            get { return m_maxValue; }
        }

        public float MinValue
        {
            get { return m_minValue; }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) :
            base(string.Format("An error occured while trying to set the value, the allowed range is {0} to {1}.", i_minValue, i_maxValue))
        {
            m_minValue = i_MinValue;
            m_maxValue = i_MaxValue;
        }
    }
}
