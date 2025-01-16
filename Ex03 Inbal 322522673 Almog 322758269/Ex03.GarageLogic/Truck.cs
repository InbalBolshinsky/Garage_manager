using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : RegularVehicle
    {
        private bool m_CoolTransport;
        private float m_CargoVolume;

        public Truck() : base(14, 29, eFuelType.Soler, 125)
        {

        }

        public bool CoolTransport
        {
            get { return m_CoolTransport; }
            set { m_CoolTransport = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }
    }
}
