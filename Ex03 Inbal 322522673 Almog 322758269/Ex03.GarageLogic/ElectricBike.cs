using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricBike : ElectricVehicle
    {
        private eLicenseType m_licenseType;
        private int m_engineVolume;

        public ElectricBike() : base(2, 32, 2.9)
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
