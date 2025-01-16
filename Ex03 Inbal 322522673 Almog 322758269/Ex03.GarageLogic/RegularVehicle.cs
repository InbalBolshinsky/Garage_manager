using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class RegularVehicle : Vehicle
    {
        private readonly eFuelType m_fuelType;
        private float m_currentFuelAmount;
        private readonly float r_maxFuelAmount;

        public RegularVehicle(int i_NumberOfWheels, float i_maxAirPressure, eFuelType i_FuelType, float i_MaxFuelAmount) : base(i_NumberOfWheels, i_maxAirPressure)
        {
            m_fuelType = i_FuelType;
            r_maxFuelAmount = i_MaxFuelAmount;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_fuelType;
            }
        }

        public float MaxFuelAmout
        {
            get
            {
                return r_maxFuelAmount;
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
                try
                {
                    if (value > MaxFuelAmout)
                    {
                        throw new ValueOutOfRangeException(0, MaxFuelAmout);
                    }
                    else
                    {
                        m_currentFuelAmount = value;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Refuel(float i_AmountOfFuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != m_fuelType)
            {
                throw new ArgumentException("Wrong fuel type. Please try again.");
            }
            if (m_currentFuelAmount + i_AmountOfFuelToAdd > r_maxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, r_maxFuelAmount);
            }
            m_currentFuelAmount += i_AmountOfFuelToAdd;
        }
    }
}
