namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_RemainingEnergy;
        private readonly Wheel[] r_Wheels;


        public Vehicle(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            r_Wheels = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_Wheels[i] = new Wheel(i_MaxAirPressure);
            }
        }

        public float RemainingEnergy
        {
            get { return m_RemainingEnergy; }
        }

        public Wheel[] Wheels
        {
            get { return r_Wheels; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public void SetRemainingEnergy(float i_RemainingEnergy)
        {
            m_RemainingEnergy = i_RemainingEnergy;
        }

        public void SetAllWheelsAirPressure(float i_AirPressure)
        {
            for (int i = 0; i < Wheels.Length; i++)
            {
                Wheels[i].CurrentAirPressure = i_AirPressure;
            }
        }

        public void SetAllWheelsManufacturer(string i_Manufacturer) 
        {
            for (int i = 0; i < this.Wheels.Length; i++)
            {
                Wheels[i].Manufacturer = i_Manufacturer;
            }
        }

    }
}