using System;
using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    public class Garage
    {
        private VehicleFactory m_vehicleFactory = new VehicleFactory();
        private Dictionary<VehicleOwner, Vehicle> m_allVehiclesInGarage = new Dictionary<VehicleOwner, Vehicle>();

        public bool CheckIfInGarage(string i_LicenseNumber, bool ChangeRepairState = true)
        {
            bool inGarage = false;
            if (m_allVehiclesInGarage != null)
            {
                foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
                {
                    if (pair.Value.LicenseNumber == i_LicenseNumber)
                    {
                        if (ChangeRepairState == true)
                        {
                            pair.Key.RepairState = eRepairState.InRepair;
                        }
                        inGarage = true;
                        break;
                    }
                }
            }
            return inGarage;
        }

        public void AddToGarage(VehicleOwner i_VehicleOwner, Vehicle i_Vehicle)
        {
            m_allVehiclesInGarage.Add(i_VehicleOwner, i_Vehicle);
        }

        public List<string> GetLicenseNumbers()
        {
            List<string> licenseNumbers = new List<string>();
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                licenseNumbers.Add(pair.Value.LicenseNumber);
            }
            return licenseNumbers;
        }

        public List<string> SortVehiclesByState(eRepairState i_VehicleState)
        {
            List<string> sortedVehicles = new List<string>();
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Key.RepairState == i_VehicleState)
                {
                    sortedVehicles.Add(pair.Value.LicenseNumber);
                }
            }
            return sortedVehicles;
        }

        public void ChangeVehicleState(string i_LicenseNumber, eRepairState i_VehicleState)
        {
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Value.LicenseNumber == i_LicenseNumber)
                {
                    pair.Key.RepairState = i_VehicleState;
                    break;
                }
            }
        }

        public void PumpWheelsToMax(string i_LicenseNumber)
        {
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Value.LicenseNumber == i_LicenseNumber)
                {
                    foreach (Wheel vehicleTire in pair.Value.Wheels)
                    {
                        vehicleTire.Pump(vehicleTire.MaxAirPressure - vehicleTire.CurrentAirPressure);
                    }
                    break;
                }
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountOfLitersToAdd)
        {
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Value.LicenseNumber == i_LicenseNumber)
                {
                    if (pair.Value is RegularVehicle)
                    {
                        (pair.Value as RegularVehicle).Refuel(i_AmountOfLitersToAdd, i_FuelType);
                        break;
                    }
                    else
                    {
                        throw new ArgumentException("Electric vehicle can not be refueled.");
                    }
                }
            }
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_AmountOfMinutesToAdd)
        {
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Value.LicenseNumber == i_LicenseNumber)
                {
                    if (pair.Value is ElectricVehicle)
                    {
                        (pair.Value as ElectricVehicle).Recharge(i_AmountOfMinutesToAdd / 60);
                        break;
                    }
                    else
                    {
                        throw new ArgumentException("Fuel vehicle can not be charged.");
                    }
                }
            }
        }

        public KeyValuePair<VehicleOwner, Vehicle> GetVehicleDetailsByLicenseNumber(string i_LicenseNumber)
        {
            KeyValuePair<VehicleOwner, Vehicle> vehicleDetailsPair = new KeyValuePair<VehicleOwner, Vehicle>();
            foreach (KeyValuePair<VehicleOwner, Vehicle> pair in m_allVehiclesInGarage)
            {
                if (pair.Value.LicenseNumber == i_LicenseNumber)
                {
                    vehicleDetailsPair = pair;
                }
            }
            return vehicleDetailsPair;
        }
    }
}