using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_ModelName;
        private string m_LicenseNumber;
        private float m_energyPercentageLeft;
        private Wheel[] m_wheels;


        public Vehicle(int i_NumberOfWheels, float i_maxAirPressure)
        {
            Wheel[] m_Wheels = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_maxAirPressure);
            }

        }

        public float EnergyPercentageLeft
        {
            get { return m_energyPercentageLeft; }
        }

        public Wheel[] Wheels
        {
            get { return m_wheels; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public void SetEnergyPersentageLeft(float i_energyPersentageLeft)
        {
            m_energyPercentageLeft = i_energyPersentageLeft;
        }

        public void SetAllWheelsAirPressure(float i_airPressure)
        {
            for (int i = 0; i < Wheels.Length; i++)
            {
                Wheels[i].CurrentAirPressure = i_airPressure;
            }
        }

        public void SetAllWheelsManufacturer(string i_manufacturer) 
        {
            for (int i = 0; i < this.Wheels.Length; i++)
            {
                Wheels[i].Manufacturer = i_manufacturer;
            }
        }

    }
}
