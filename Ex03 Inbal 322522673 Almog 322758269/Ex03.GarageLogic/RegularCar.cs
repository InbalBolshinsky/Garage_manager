using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularCar : RegularVehicle
    {
        private eCarColors m_carColor;
        private eNumberOfDoors m_numberOfDoors;

        public RegularCar() : base(5, 34, eFuelType.Octan95, 52)
        {

        }

        public eColor Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_numOfDoors; }
            set { m_numOfDoors = value; }
        }
    }
}
