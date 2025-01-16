using System;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public VehicleFactory()
        {
        }

        public Vehicle CreateVehicle(eVehicleTypes i_VehicleType)
        {
            Vehicle newVehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleTypes.RegularCar:
                    newVehicle = new RegularCar();
                    break;
                case eVehicleTypes.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case eVehicleTypes.RegularBike:
                    newVehicle = new RegularBike();
                    break;
                case eVehicleTypes.ElectricBike:
                    newVehicle = new ElectricBike();
                    break;
                case eVehicleTypes.Truck:
                    newVehicle = new Truck();
                    break;
                default:
                    throw new ValueOutOfRangeException(0, Enum.GetValues(typeof(eVehicleTypes)).Length - 1);
            }

            return newVehicle;
        }
    }
}
