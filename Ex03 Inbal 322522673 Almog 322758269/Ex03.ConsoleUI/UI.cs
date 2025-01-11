using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace Ex03.ConsoleUI
{
    internal class UI
    {
        private Ex03.GarageLogic.Garage garage = new GarageLogic.Garage();
        private Ex03.GarageLogic.VehicleFactory factory = new GarageLogic.VehicleFactory();


        public void GarageMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Garage Managment System Menu");
                Console.WriteLine("============================");
                Console.WriteLine("Please select an option (enter function number):");
                Console.WriteLine("1. Add vehicle to garage.");
                Console.WriteLine("2. Get license numbers (get all or sort by vehicle state).");
                Console.WriteLine("3. Change vehicle state.");
                Console.WriteLine("4. Inflate vehicle tires to maximum.");
                Console.WriteLine("5. Refuel a fuel driven vehicle.");
                Console.WriteLine("6. Charge an electric vehicle.");
                Console.WriteLine("7. Get all vehicle data by license number.");
                Console.WriteLine("8. Exit system.");
                int input;
                bool validInput = int.TryParse(Console.ReadLine(), out input);
                while (validInput == false || input > 8 || input < 1)
                {
                    Console.Write("Invalid input, please try again: ");
                    validInput = int.TryParse(Console.ReadLine(), out input);
                }

                try
                {
                    switch (input)
                    {
                        case 1:
                            addVehicle();
                            break;
                        case 2:
                            getLicenseNumbers();
                            break;
                        case 3:
                            changeVehicleState();
                            break;
                        case 4:
                            pumpAllWheelsToMax();
                            break;
                        case 5:
                            refuelVehicle();
                            break;
                        case 6:
                            chargeVehicle();
                            break;
                        case 7:
                            printVehicleData();
                            break;
                        case 8:
                            exitMenu = true;
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("An error occured while trying to set the value, the value is not matching the expected value type.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

                if (exitMenu)
                {
                    Console.WriteLine("Press enter to exit the menu.");
                }
                else
                {
                    Console.WriteLine("\nGetting back to menu...\n");
                }
            }
        }
        public string getVehicleLicenseNumber()
        {
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();
            return licenseNumber;
        }

        public void addVehicle()
        {
            GarageLogic.Vehicle vehicle;
            string licenseNumber = getVehicleLicenseNumber();  

            if (garage.CheckIfInGarage(licenseNumber))
            {
                garage.ChangeVehicleState(licenseNumber, GarageLogic.eRepairState.InRepair);
                Console.WriteLine("Vehicle already exists in the garage, changing state to 'In Repair'.");
            }
            else
            {
                string ownerName;
                string ownerPhone;
                string vehicleType;

                Console.WriteLine("Please enter vehicle type:");
                printEnumValues(typeof(GarageLogic.eVehicleTypes));
                vehicleType = Console.ReadLine();
                vehicle = factory.CreateVehicle((GarageLogic.eVehicleTypes)int.Parse(vehicleType));

                Type typeOfVehicle = vehicle.GetType();
                PropertyInfo[] allProperties = typeOfVehicle.GetProperties();

                for (int i = 0; i < allProperties.Length; i++)
                {
                    PropertyInfo propertyInfo = allProperties[i];
                    if (propertyInfo.CanWrite)
                    {
                        if (propertyInfo.Name == "LicenseNumber")
                        {
                            propertyInfo.SetValue(vehicle, licenseNumber, null);
                        }
                        else
                        {
                            if (propertyInfo.PropertyType.IsEnum)
                            {
                                Console.WriteLine(string.Format("Please enter {0}: ", fixPropertyName(propertyInfo.Name)));
                                printEnumValues(propertyInfo.PropertyType);
                                int inputEnumVal;
                                inputEnumVal = int.Parse(Console.ReadLine());
                                if (!Enum.IsDefined(propertyInfo.PropertyType, inputEnumVal))
                                {
                                    throw new GarageLogic.ValueOutOfRangeException(0, Enum.GetValues(propertyInfo.PropertyType).Length - 1);
                                }

                                propertyInfo.SetValue(vehicle, Enum.ToObject(propertyInfo.PropertyType, inputEnumVal), null);
                            }
                            else
                            {
                                if (propertyInfo.PropertyType == typeof(bool))
                                {
                                    Console.Write(string.Format("Please enter if {0}: \nfor yes enter 'true', for no enter 'false': ", fixPropertyName(propertyInfo.Name)));
                                }
                                else
                                {
                                    Console.Write(string.Format("Please enter {0}: ", fixPropertyName(propertyInfo.Name)));
                                }

                                propertyInfo.SetValue(vehicle, Convert.ChangeType(Console.ReadLine(), propertyInfo.PropertyType), null);
                            }
                        }
                    }
                }
                Console.Write("Please enter the manufacturer name for all the wheels:");
                Console.Write("Enter all of the wheels manufacturer name:");
                vehicle.SetAllWheelsManufacturer(Console.ReadLine());

                Console.Write("Please enter air preassure for all the wheels:");
                Console.Write("Enter all of the wheels current air pressure:");
                try
                {
                    float airPressure = float.Parse(Console.ReadLine());
                    vehicle.SetAllWheelsAirPressure(airPressure);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input was not a valid number. Please try again.");
                }

                Console.Write("Please enter owner's name:");
                ownerName = Console.ReadLine();
                Console.Write("Please enter owner's phone number:");
                ownerPhone = Console.ReadLine();
                garage.AddToGarage(new GarageLogic.VehicleOwner(ownerName, ownerPhone), vehicle);
                Console.WriteLine("Vehicle added successfully.");
            }
        }

        private StringBuilder fixPropertyName(string i_propertyName)
        {
            StringBuilder fixedName = new StringBuilder();
            int i = 0;

            for (i = 0; i < i_propertyName.Length - 1; i++)
            {
                fixedName.Append(i_propertyName[i]);
                if (char.IsUpper(i_propertyName[i + 1]) && char.IsUpper(i_propertyName[i]) == false)
                {
                    fixedName.Append(' ');
                }
            }

            fixedName.Append(i_propertyName[i]);
            return fixedName;
        }

        private void printEnumValues(Type i_enum)
        {
            string[] enumOptions = i_enum.GetEnumNames();
            for (int i = 0; i < enumOptions.Length; i++)
            {
                Console.WriteLine(string.Format("For {0} please enter '{1}'", fixPropertyName(enumOptions[i]), i));
            }
        }

        public void getLicenseNumbers()
        {
            Console.Write("Do you want to select vehicle state to filter your search by?\nPlease enter '1' if yes, '0' if no: ");
            string answer = Console.ReadLine();
            while (answer.Length != 1 || (answer[0] != '0' && answer[0] != '1'))
            {
                Console.Write("Invalid input, please try again:");
                answer = Console.ReadLine();
            }

            if (answer[0] == '0')
            {
                List<string> licenseNumbers = garage.GetLicenseNumbers();
                if (licenseNumbers.Count == 0)
                {
                    Console.WriteLine("There are no vehicles in the garage.");
                }
                else
                {
                    Console.WriteLine("The license numbers are:");
                    foreach (string licenseNumber in licenseNumbers)
                    {
                        Console.WriteLine(licenseNumber);
                    }
                }
            }
            else
            {
                Console.WriteLine("Which vehicle state license numbers do you want to display?");
                printEnumValues(typeof(GarageLogic.eRepairState));
                List<string> licenseNumbers = garage.SortVehiclesBySate((GarageLogic.eRepairState)int.Parse(Console.ReadLine()));
                if (licenseNumbers.Count == 0)
                {
                    Console.WriteLine("There are no vehicles in the garage in that state.");
                }
                else
                {
                    Console.WriteLine("The license numbers are:");
                    foreach (string licenseNumber in licenseNumbers)
                    {
                        Console.WriteLine(licenseNumber);
                    }
                }
            }
        }

        public void changeVehicleState()
        {
            string licenseNumber = getVehicleLicenseNumber();

            Console.WriteLine("Please enter the new vehicle state:");
            printEnumValues(typeof(GarageLogic.eRepairState));
            int newVehicleState = int.Parse(Console.ReadLine());

            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                garage.ChangeVehicleState(licenseNumber, (GarageLogic.eRepairState)newVehicleState);
                if (!Enum.IsDefined(typeof(GarageLogic.eRepairState), newVehicleState))
                {
                    throw new GarageLogic.ValueOutOfRangeException(0, Enum.GetValues(typeof(GarageLogic.eRepairState)).Length - 1);
                }
                Console.WriteLine("Vehicle's state changed.");
            }
            else
            {
                Console.WriteLine("There is no vehicle with such license number in the garage.");
            }
        }

        public void pumpAllWheelsToMax()
        {
            string licenseNumber = getVehicleLicenseNumber();   
            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                garage.PumpWheelsToMax(licenseNumber);
                Console.WriteLine("Wheels inflated to maximum.");
            }
            else
            {
                Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number");
            }
        }
        public void refuelVehicle()
        {
           string licenseNumber = getVehicleLicenseNumber();

            Console.WriteLine("Please enter fuel type:");
            printEnumValues(typeof(GarageLogic.eFuelType));
            int inputFuelType = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the amount of liters to fuel:");
            float inputAmountOfLitersToAdd = float.Parse(Console.ReadLine());
            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                garage.RefuelVehicle(licenseNumber, (GarageLogic.eFuelType)inputFuelType, inputAmountOfLitersToAdd);
                Console.WriteLine("Vehicle refuled.");
            }
            else
            {
                Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number");
            }
        }

        public void chargeVehicle()
        {
            string licenseNumber = getVehicleLicenseNumber();
            Console.WriteLine("Please enter the amount of minutes to charge:");
            float inputAmountOfMinutesToCharge = float.Parse(Console.ReadLine());

            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                garage.ChargeVehicle(licenseNumber, inputAmountOfMinutesToCharge);
                Console.WriteLine("Vehicle charged.");
            }
            else
            {
                Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number");
            }
        }

        public void printVehicleData()
        {
            string licenseNumber = getVehicleLicenseNumber();
            int wheelIndex = 1;
            KeyValuePair<GarageLogic.VehicleOwner, GarageLogic.Vehicle> vehicleData = garage.GetVehicleDetailsByLicenseNumber(licenseNumber);
            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                Console.WriteLine("Vehicle data:");
                Console.WriteLine(string.Format("Owner name: {0}", vehicleData.Key.OwnerName));
                Console.WriteLine(string.Format("Vehicle state: {0}", vehicleData.Key.RepairState));
                Console.WriteLine("Wheels data:");

                foreach (Wheel wheel in vehicleData.Value.Wheels)
                {
                    Console.WriteLine("Wheel number: {0}", wheelIndex);
                    Console.WriteLine("Wheel manufacturer: {0}", wheel.Manufacturer);
                    Console.WriteLine("Wheel current air pressure: {0}", wheel.CurrentAirPressure);
                    wheelIndex++;
                }

                Type typeOfVehicle = vehicleData.Value.GetType();
                PropertyInfo[] allProperties = typeOfVehicle.GetProperties();
                MethodInfo[] allMethods = typeOfVehicle.GetMethods();

                foreach (PropertyInfo propertyInfo in allProperties)
                {
                    if (propertyInfo.Name != "Wheels")
                    {
                        Console.WriteLine(string.Format("Vehicle's {0}: {1}", fixPropertyName(propertyInfo.Name), propertyInfo.GetValue(vehicleData.Value, null)));
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number");
            }
        }
    }   
}


