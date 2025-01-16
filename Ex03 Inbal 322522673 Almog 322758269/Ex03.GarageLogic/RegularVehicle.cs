using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class RegularVehicle : Vehicle
    {
        private readonly eFuelType r_FuelType;
        private float m_CurrentFuelAmount;
        private readonly float r_MaxFuelAmount;

        public RegularVehicle(int i_NumberOfWheels, float i_MaxAirPressure, eFuelType i_FuelType, float i_MaxFuelAmount) : base(i_NumberOfWheels, i_MaxAirPressure)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelAmount = i_MaxFuelAmount;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public float MaxFuelAmout
        {
            get
            {
                return r_MaxFuelAmount;
            }
        }

        public float CurrentFuelAmount
        {
            get
            {
                return m_CurrentFuelAmount;
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
                        m_CurrentFuelAmount = value;
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
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Wrong fuel type. Please try again.");
            }
            if (m_CurrentFuelAmount + i_AmountOfFuelToAdd > r_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, r_MaxFuelAmount);
            }
            m_CurrentFuelAmount += i_AmountOfFuelToAdd;
        }
    }
}
