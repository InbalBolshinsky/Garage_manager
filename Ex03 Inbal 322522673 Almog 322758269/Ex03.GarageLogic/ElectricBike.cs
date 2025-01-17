namespace Ex03.GarageLogic
{
    internal class ElectricBike : ElectricVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public ElectricBike() : base(2, 32, 2.9f)
        {
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }
    }
}