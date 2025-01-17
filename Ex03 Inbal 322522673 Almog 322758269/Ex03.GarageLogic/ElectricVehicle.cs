using System;
namespace Ex03.GarageLogic
{
    internal abstract class ElectricVehicle : Vehicle
    {
        private float m_BatteryTimeLeft;
        private readonly float r_MaxBatteryTime;

        public ElectricVehicle(int i_Wheels, float i_MaxAirPressure, float i_MaxBatteryTime) : base(i_Wheels, i_MaxAirPressure)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
        }

        public float BatteryTimeLeft
        {
            get { return m_BatteryTimeLeft; }
            set {
                try
                {
                    if (value > r_MaxBatteryTime)
                    {
                        throw new ValueOutOfRangeException(0, r_MaxBatteryTime);
                    }
                    else
                    {
                        m_BatteryTimeLeft = value;
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
            get { return r_MaxBatteryTime; }
        }

        public void Recharge(float i_AmountOfMinutesToAdd)
        {
            float hoursToAdd = i_AmountOfMinutesToAdd / 60;
            if (m_BatteryTimeLeft + i_AmountOfMinutesToAdd > r_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, r_MaxBatteryTime);
            }
            m_BatteryTimeLeft += hoursToAdd;
        }
    }
}