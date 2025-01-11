using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularBike : RegularVehicle
    {
        private eLicenseType m_licenseType;
        private int m_engineVolume;

        public RegularBike() : base(2, 32, eFuelType.Octan98, 6.2f)
        {
        }

        public eLicenseType LicenseType
        {
            get { return m_licenseType; }
            set { m_licenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_engineVolume; }
            set { m_engineVolume = value; }
        }
    }

}
