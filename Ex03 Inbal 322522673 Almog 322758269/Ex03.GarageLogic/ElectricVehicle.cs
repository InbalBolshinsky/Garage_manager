using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class ElectricVehicle : Vehicle
    {
        private float m_batteryTimeLeft;
        private readonly float r_maxBatteryTime;

        public ElectricVehicle(int i_Wheels, float i_maxAirPressure, float i_maxBatteryTime) : base(i_Wheels, i_maxAirPressure)
        {
            r_maxBatteryTime = i_maxBatteryTime;
        }

        public float BatteryTimeLeft
        {
            get
            {
                return m_batteryTimeLeft;
            }
            set
            {
                try
                {
                    if (value > r_maxBatteryTime)
                    {
                        throw new ValueOutOfRangeException(0, r_maxBatteryTime);
                    }
                    else
                    {
                        m_batteryTimeLeft = value;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public float MaxBatteryHours
        {
            get { return r_maxBatteryTime; }
        }


        public void Recharge(float i_AmountOfMinutesToAdd)
        {
            float hoursToAdd = i_AmountOfMinutesToAdd / 60;

            if (m_batteryTimeLeft + i_AmountOfMinutesToAdd > r_maxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, r_maxBatteryTime);
            }
            m_batteryTimeLeft += hoursToAdd;
        }
    }
}