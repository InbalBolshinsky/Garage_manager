using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class RegularVehicle : Vehicle
    {
        private eFuelType m_fuelType;
        private float m_currentFuelAmount;
        private float m_maxFuelAmount;

        public RegularVehicle(int i_NumberOfWheels, float i_maxAirPressure, eFuelType i_FuelType, float i_MaxFuelAmount) : base(i_NumberOfWheels, i_maxAirPressure)
        {
            m_fuelType = i_FuelType;
            m_maxFuelAmount = i_MaxFuelAmount;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_fuelType;
            }
        }

        public float CurrentFuelAmount
        {
            get
            {
                return m_currentFuelAmount;
            }
            set
            {
                if (value > MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, MaxAirPressure);
                }

                m_currentFuelAmount = value;
            }
        }
    }
}
