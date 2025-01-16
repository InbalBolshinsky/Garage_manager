using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularBike : RegularVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public RegularBike() : base(2, 32, eFuelType.Octan98, 6.2f)
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
