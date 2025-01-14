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
        private readonly string r_modelName;
        private string m_licenseNumber;
        private float m_remainingEnergy;
        private Wheel[] m_wheels;


        public Vehicle(int i_NumberOfWheels, float i_maxAirPressure)
        {
            Wheel[] m_Wheels = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_maxAirPressure);
            }

        }

        public float RemainingEnergy
        {
            get { return m_remainingEnergy; }
        }

        public Wheel[] Wheels
        {
            get { return m_wheels; }
        }

        public string ModelName
        {
            get { return r_modelName; }
        }

        public string LicenseNumber
        {
            get { return m_licenseNumber; }
            set { m_licenseNumber = value; }
        }

        public void SetRemainingEnergy(float i_remainingEnergy)
        {
            m_remainingEnergy = i_remainingEnergy;
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
