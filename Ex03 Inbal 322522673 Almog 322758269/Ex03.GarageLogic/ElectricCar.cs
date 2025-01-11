using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : ElectricVehicle
    {
        private eCarColors m_carColor;
        private eNumberOfDoors m_numberOfDoors;

        public ElectricCar() : base(5, 34, i_MaxBatteryTime)
        {

        }

        public eColor CarColor
        {
            get { return m_carColor; }
            set { m_carColor = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_numberOfDoors; }
            set { m_numberOfDoors = value; }
        }
    }
}
