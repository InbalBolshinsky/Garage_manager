namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
            set {  m_Manufacturer = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (value > MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, MaxAirPressure);
                }
                m_CurrentAirPressure = value;
            }
        }

        public void Pump(float i_PreassureToAdd)
        {
            if (i_PreassureToAdd + m_CurrentAirPressure > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure);
            }
            else
            {
                m_CurrentAirPressure += i_PreassureToAdd;
            }
        }
    }
}