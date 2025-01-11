using Ex03.GarageLogic;
using System;
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
                            addVehicleUI();
                            break;
                        case 2:
                            getLicenseNumbersUI();
                            break;
                        case 3:
                            changeVehicleStateUI();
                            break;
                        case 4:
                            inflateTiresToMaxUI();
                            break;
                        case 5:
                            refuelVehicleUI();
                            break;
                        case 6:
                            chargeVehicleUI();
                            break;
                        case 7:
                            printVehicleInfo();
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
                    Console.WriteLine("Press enter to exit...");
                }
                else
                {
                    Console.WriteLine("\nGetting back to menu...\n");
                }
            }
        }

        public void addVehicleUI()
        {
            GarageLogic.Vehicle vehicle;
            string licenseNumber;
            Console.WriteLine("Please enter the vehicle license number:");
            licenseNumber = Console.ReadLine();

            if (garage.CheckIfInGarage(licenseNumber))
            {
                garage.ChangeVehicleState(licenseNumber, GarageLogic.eRepairState.InRepair);
                Console.WriteLine("Vehicle already exists in the garage, changing state to 'In Repair'.");
            }
            else
            {
                string OwnerName;
                string OwnerPhone;
                string VehicleType;
                Console.WriteLine("Please enter vehicle type:");
                printEnumValues(typeof(GarageLogic.eVehicleTypes));
                VehicleType = Console.ReadLine();
                vehicle = factory.CreateVehicle((GarageLogic.eVehicleTypes)int.Parse(VehicleType));
                
                GetVehicleInfo(vehicle, VehicleType);


                garage.AddToGarage((vehicle, licenseNumber));
                Console.WriteLine("Vehicle added successfully.");
            }


        }

        public eVehicleTypes GetVehicleInfo(GarageLogic.Vehicle i_vehicle)
        {
            foreach (string property in i_vehicle.GetProperties())
            {
                Console.WriteLine(string.Format("Please enter {0}:", fixPropertyName(property)));
                string value = Console.ReadLine();
                i_vehicle.SetProperty(property, value);
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
    }
        
}


