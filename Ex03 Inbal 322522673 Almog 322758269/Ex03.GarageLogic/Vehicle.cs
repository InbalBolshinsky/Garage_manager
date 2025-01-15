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
        private string m_modelName;
        private string m_licenseNumber;
        private float m_remainingEnergy;
        private readonly Wheel[] r_wheels;


        public Vehicle(int i_NumberOfWheels, float i_maxAirPressure)
        {
            r_wheels = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_wheels[i] = new Wheel(i_maxAirPressure);
            }

        }

        public float RemainingEnergy
        {
            get { return m_remainingEnergy; }
        }

        public Wheel[] Wheels
        {
            get { return r_wheels; }
        }

        public string ModelName
        {
            get { return m_modelName; }
            set { m_modelName = value; }
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
