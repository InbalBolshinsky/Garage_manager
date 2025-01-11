using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : RegularVehicle
    {
        private bool m_coolTransport;
        private float m_cargoVolume;

        public Truck() : base(14, 29, eFuelType.Soler, 125)
        {

        }

        public bool CoolTransport
        {
            get { return m_coolTransport; }
            set { m_coolTransport = value; }
        }

        public float CargoVolume
        {
            get { return m_cargoVolume; }
            set { m_cargoVolume = value; }
        }
    }
}
