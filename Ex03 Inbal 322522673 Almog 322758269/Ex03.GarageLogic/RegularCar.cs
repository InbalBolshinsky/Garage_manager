using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularCar : RegularVehicle
    {
        private eCarColors m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        public RegularCar() : base(5, 34, eFuelType.Octan95, 52)
        {

        }

        public eCarColors Color
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
    }
}
