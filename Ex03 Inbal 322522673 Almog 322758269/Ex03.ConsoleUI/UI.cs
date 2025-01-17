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

                if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 1 || userChoice > 8)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                switch (userChoice)
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

        private string getVehicleLicenseNumber()
        {
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();
            return licenseNumber;
        }

        private static int validateEnumInput(Type i_EnumType, int i_Input)
        {
            if (!Enum.IsDefined(i_EnumType, i_Input))
            {
                int minValue = 0;
                int maxValue = Enum.GetValues(i_EnumType).Length - 1;
                throw new ValueOutOfRangeException(minValue, maxValue);
            }
            return i_Input;
        }

        private void getVehicleType(ref Vehicle io_Vehicle)
        {
            string vehicleType;
            Console.WriteLine("Please enter vehicle type:");
            printEnumValues(typeof(GarageLogic.eVehicleTypes));

            while (true)
            {
                try
                {
                    vehicleType = Console.ReadLine();
                    int parsedVehicleType = int.Parse(vehicleType);

                    int validatedVehicleTypeValue = validateEnumInput(typeof(GarageLogic.eVehicleTypes), parsedVehicleType);

                    io_Vehicle = factory.CreateVehicle((GarageLogic.eVehicleTypes)validatedVehicleTypeValue);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value. Please try again.");
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void setEnumProperty(PropertyInfo i_PropertyInfo, ref Vehicle io_Vehicle)
        {
            Console.WriteLine($"Please enter {fixPropertyName(i_PropertyInfo.Name)}:");
            printEnumValues(i_PropertyInfo.PropertyType);

            while (true)
            {
                try
                {
                    string enumUserInput = Console.ReadLine();
                    int parsedEnumUserInput = int.Parse(enumUserInput);

                    int validatedPropertyInputValue = validateEnumInput(i_PropertyInfo.PropertyType, parsedEnumUserInput);

                    i_PropertyInfo.SetValue(io_Vehicle, Enum.ToObject(i_PropertyInfo.PropertyType, validatedPropertyInputValue), null);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value. Please try again.");
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void setBooleanProperty(PropertyInfo i_PropertyInfo, ref Vehicle io_Vehicle)
        {
            Console.WriteLine($"Please enter if {fixPropertyName(i_PropertyInfo.Name)} (for yes enter '1', for no enter '0'):");

            while (true)
            {
                try
                {
                    string boolUserInput = Console.ReadLine();
                    if (boolUserInput != "0" && boolUserInput != "1")
                    {
                        throw new GarageLogic.ValueOutOfRangeException(0, 1);
                    }

                    i_PropertyInfo.SetValue(io_Vehicle, boolUserInput == "1", null);
                    break;
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void setOtherProperty(PropertyInfo i_PropertyInfo, ref Vehicle io_Vehicle)
        {
            Console.WriteLine($"Please enter {fixPropertyName(i_PropertyInfo.Name)}:");
            bool userPropertyInputIsValid = false;
            while (!userPropertyInputIsValid)
            {
                string UserPropertyInput = Console.ReadLine();
                try
                {
                    i_PropertyInfo.SetValue(io_Vehicle, Convert.ChangeType(UserPropertyInput, i_PropertyInfo.PropertyType), null);
                    object currentValue = i_PropertyInfo.GetValue(io_Vehicle);
                    if (UserPropertyInput == currentValue.ToString())
                    {
                        userPropertyInputIsValid = true;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void getOwnersDetails(ref string io_OwnerName, ref string io_OwnerPhone)
        {
            Console.WriteLine("Please enter owner's name:");
            io_OwnerName = Console.ReadLine();

            Console.WriteLine("Please enter owner's phone number:");
            io_OwnerPhone = Console.ReadLine();
        }

        private void setWheelsDetails(ref Vehicle io_Vehicle)
        {
            Console.WriteLine("Please enter the manufacturer name for all the wheels:");
            string manufacturerUserInput = Console.ReadLine();
            io_Vehicle.SetAllWheelsManufacturer(manufacturerUserInput);

            Console.WriteLine("Please enter air pressure for all the wheels:");
            while (true)
            {
                try
                {
                    float airPressureUserInput = float.Parse(Console.ReadLine());
                    io_Vehicle.SetAllWheelsAirPressure(airPressureUserInput);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for air pressure.");
                }
            }
        }

        private void addVehicle()
        {
            GarageLogic.Vehicle vehicle = null;
            string licenseNumber = getVehicleLicenseNumber();

            if (garage.CheckIfInGarage(licenseNumber))
            {
                garage.ChangeVehicleState(licenseNumber, GarageLogic.eRepairState.InRepair);
                Console.WriteLine("Vehicle already exists in the garage, changing state to 'In Repair'.");
            }
            else
            {  
                getVehicleType(ref vehicle);

                Type typeOfVehicle = vehicle.GetType();
                PropertyInfo[] allProperties = typeOfVehicle.GetProperties();

                foreach (PropertyInfo propertyInfo in allProperties)
                {
                    if (propertyInfo.CanWrite)
                    {
                        if (propertyInfo.Name == "LicenseNumber")
                        {
                            propertyInfo.SetValue(vehicle, licenseNumber, null);
                        }
                        else if (propertyInfo.PropertyType.IsEnum)
                        {
                            setEnumProperty(propertyInfo, ref vehicle);
                        } 
                        else if (propertyInfo.PropertyType == typeof(bool))
                        {
                            setBooleanProperty(propertyInfo, ref vehicle);
                        }
                        else
                        {
                            setOtherProperty(propertyInfo, ref vehicle);
                        }
                    }
                }

                setWheelsDetails(ref vehicle);

                string ownerName = "";
                string ownerPhone = "";
               
                getOwnersDetails(ref ownerName, ref ownerPhone);

                garage.AddToGarage(new GarageLogic.VehicleOwner(ownerName, ownerPhone), vehicle);
                Console.WriteLine("Vehicle added successfully.");
            }
        }

        private StringBuilder fixPropertyName(string i_PropertyName)
        {
            StringBuilder fixedName = new StringBuilder();
            int i = 0;

            for (i = 0; i < i_PropertyName.Length - 1; i++)
            {
                fixedName.Append(i_PropertyName[i]);
                if (char.IsUpper(i_PropertyName[i + 1]) && char.IsUpper(i_PropertyName[i]) == false)
                {
                    fixedName.Append(' ');
                }

            }
            fixedName.Append(i_PropertyName[i]);
            return fixedName;
        }

        private void printEnumValues(Type i_Enum)
        {
            string[] enumOptions = i_Enum.GetEnumNames();

            for (int i = 0; i < enumOptions.Length; i++)
            {
                Console.WriteLine(string.Format("For {0} please enter '{1}'", fixPropertyName(enumOptions[i]), i));
            }
        }

        private void showAllLicenseNumbers()
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

        private void showFilteredLicenseNumbers()
        {
            Console.WriteLine("Which vehicle state license numbers do you want to display?");
            printEnumValues(typeof(GarageLogic.eRepairState));

            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter a value for the repair state:");
                    string userRepairStateInput = Console.ReadLine();
                    int parsedEnum = int.Parse(userRepairStateInput);
                    int validatedValue = validateEnumInput(typeof(GarageLogic.eRepairState), parsedEnum);

                    List<string> licenseNumbers = garage.SortVehiclesByState((GarageLogic.eRepairState)validatedValue);

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
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void getLicenseNumbers()
        {
            Console.Write("Do you want to select vehicle state to filter your search by?\nPlease enter '1' if yes, '0' if no: ");
            string userAnswer = Console.ReadLine();
            while (userAnswer.Length != 1 || (userAnswer[0] != '0' && userAnswer[0] != '1'))
            {
                Console.Write("Invalid input, please try again:");
                userAnswer = Console.ReadLine();
            }

            if (userAnswer[0] == '0')
            {
                showAllLicenseNumbers();
            }
            else 
            {
               showFilteredLicenseNumbers();
            }
        }

        private void changeVehicleState()
        {
            string licenseNumber = getVehicleLicenseNumber();

            Console.WriteLine("Please enter the new vehicle state:");
            printEnumValues(typeof(GarageLogic.eRepairState));
            
            if (garage.CheckIfInGarage(licenseNumber, false))
            {
                while (true)
                {
                    string userNewRepairStateInput = Console.ReadLine();
                    int newVehicleState = int.Parse(userNewRepairStateInput);
                    try
                    {
                        int validatedValue = validateEnumInput(typeof(GarageLogic.eRepairState), newVehicleState);
                        garage.ChangeVehicleState(licenseNumber, (GarageLogic.eRepairState)newVehicleState);
                        Console.WriteLine("Vehicle's state changed.");
                        break;
                    }
                    catch (GarageLogic.ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no vehicle with such license number in the garage.");
            }
        }

        private void pumpAllWheelsToMax()
        {
            string licenseNumber = getVehicleLicenseNumber();
            
            while(!garage.CheckIfInGarage(licenseNumber, false))
            {
                Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number");
                licenseNumber = getVehicleLicenseNumber();
            }
            garage.PumpWheelsToMax(licenseNumber);
            Console.WriteLine("Wheels inflated to maximum.");
        }

        private int getFuelType()
        {
            Console.WriteLine("Please enter fuel type:");
            printEnumValues(typeof(GarageLogic.eFuelType));
            string fuelTypeInput;
            int fuelType;
            while (true)
            {
                try
                {
                    fuelTypeInput = Console.ReadLine();
                    fuelType = int.Parse(fuelTypeInput);

                    validateEnumInput(typeof(GarageLogic.eFuelType), fuelType);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return fuelType;
        }

        private void setAmountOfLitersToFuel(string i_LicenseNumber, int i_FuelType)
        {
            Console.WriteLine("Please enter the amount of liters to fuel:");
            string amountOfLitersToAddInput;
            float AmountOfLitersToAdd;
            while (true)
            {
                try
                {
                    amountOfLitersToAddInput = Console.ReadLine();
                    AmountOfLitersToAdd = float.Parse(amountOfLitersToAddInput);

                    if (AmountOfLitersToAdd <= 0)
                    {
                        throw new ArgumentException("Fuel amount must be greater than zero. Please try again.");
                    }

                    garage.RefuelVehicle(i_LicenseNumber, (GarageLogic.eFuelType)i_FuelType, AmountOfLitersToAdd);
                    Console.WriteLine("Vehicle refueled.");

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void refuelVehicle()
        {
            string licenseNumber;

            while (true)
            {
                licenseNumber = getVehicleLicenseNumber();

                if (garage.CheckIfInGarage(licenseNumber, false))
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number.");
                }
            }

            int fuelType = getFuelType();
            setAmountOfLitersToFuel(licenseNumber, fuelType);
        }

        private void setAmountOfMinutesToCharge(string i_LicenseNumber)
        {
            Console.WriteLine("Please enter the amount of minutes to charge:");
            string amountOfMinutesToChargeInput;
            float amountOfMinutesToCharge;
            while (true)
            {
                try
                {
                    amountOfMinutesToChargeInput = Console.ReadLine();
                    amountOfMinutesToCharge = float.Parse(amountOfMinutesToChargeInput);

                    if (amountOfMinutesToCharge <= 0)
                    {
                        throw new ArgumentException("Fuel amount must be greater than zero. Please try again.");
                    }

                    garage.ChargeVehicle(i_LicenseNumber, amountOfMinutesToCharge);
                    Console.WriteLine("Vehicle refueled.");

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void chargeVehicle()
        {
            string licenseNumber;

            while (true)
            {
                licenseNumber = getVehicleLicenseNumber();

                if (garage.CheckIfInGarage(licenseNumber, false))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("There is no vehicle with this license number in the garage. Please enter a different license number.");
                }
            }
            setAmountOfMinutesToCharge(licenseNumber);
        }

        private void printVehicleData()
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


