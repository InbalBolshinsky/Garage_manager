using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace Ex03.ConsoleUI
{
    internal class UI
    {
        private Ex03.GarageLogic.Garage m_Garage = new GarageLogic.Garage();
        private Ex03.GarageLogic.VehicleFactory m_Factory = new GarageLogic.VehicleFactory();

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
                    Console.WriteLine("Press enter to exit the menu." + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("Getting back to menu..." + Environment.NewLine);
                }
            }
        }

        private string getVehicleLicenseNumber()
        {
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();
            return licenseNumber;
        }

        private string getValidLicenseNumber()
        {
            while (true)
            {
                string licenseNumber = getVehicleLicenseNumber();
                if (m_Garage.CheckIfInGarage(licenseNumber, false))
                {
                    return licenseNumber;
                }
                else
                {
                    Console.WriteLine("This license number is not in the garage. Please enter a different one.");
                }
            }
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

        private void addVehicle()
        {
            GarageLogic.Vehicle vehicle;
            string licenseNumber = getVehicleLicenseNumber();
            if (m_Garage.CheckIfInGarage(licenseNumber))
            {
                m_Garage.ChangeVehicleState(licenseNumber, GarageLogic.eRepairState.InRepair);
                Console.WriteLine("Vehicle already exists in the garage, changing state to 'In Repair'.");
            }
            else
            {
                string ownerName;
                string ownerPhone;
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
                        vehicle = m_Factory.CreateVehicle((GarageLogic.eVehicleTypes)validatedVehicleTypeValue);
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
                            Console.WriteLine($"Please enter {fixPropertyName(propertyInfo.Name)}:");
                            printEnumValues(propertyInfo.PropertyType);
                            while (true)
                            {
                                try
                                {
                                    string enumUserInput = Console.ReadLine();
                                    int parsedEnumUserInput = int.Parse(enumUserInput);
                                    int validatedPropertyInputValue = validateEnumInput(propertyInfo.PropertyType, parsedEnumUserInput);
                                    propertyInfo.SetValue(vehicle, Enum.ToObject(propertyInfo.PropertyType, validatedPropertyInputValue), null);
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
                        else if (propertyInfo.PropertyType == typeof(bool))
                        {
                            Console.WriteLine($"Please enter if {fixPropertyName(propertyInfo.Name)} (for yes enter '1', for no enter '0'):");
                            while (true)
                            {
                                try
                                {
                                    string boolUserInput = Console.ReadLine();
                                    if (boolUserInput != "0" && boolUserInput != "1")
                                    {
                                        throw new GarageLogic.ValueOutOfRangeException(0, 1);
                                    }
                                    propertyInfo.SetValue(vehicle, boolUserInput == "1", null);
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
                            Console.WriteLine($"Please enter {fixPropertyName(propertyInfo.Name)}:");
                            bool userPropertyInputIsValid = false;
                            while (!userPropertyInputIsValid)
                            {
                                string userPropertyInput = Console.ReadLine();
                                if (propertyInfo.PropertyType == typeof(int))
                                {
                                    if (!int.TryParse(userPropertyInput, out int parsedValue))
                                    {
                                        Console.WriteLine($"Input is not a valid number. Valid range is {int.MinValue} to {int.MaxValue}." + Environment.NewLine + "Please enter another number.");
                                        continue;
                                      
                                    }
                                }
                                else if (propertyInfo.PropertyType == typeof(float))
                                {
                                    if (!float.TryParse(userPropertyInput, out float floatParsedValue))
                                    {
                                        Console.WriteLine($"Input is not a valid number. Valid range is {float.MinValue} to {float.MaxValue}." + Environment.NewLine + "Please enter another number.");
                                        continue;
                                    }
                                }
                                try
                                {
                                    propertyInfo.SetValue(vehicle, Convert.ChangeType(userPropertyInput, propertyInfo.PropertyType), null);
                                    object currentValue = propertyInfo.GetValue(vehicle);
                                    if (userPropertyInput == currentValue.ToString())
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
                    }
                }
                Console.WriteLine("Please enter the manufacturer name for all the wheels:");
                string manufacturerUserInput = Console.ReadLine();
                vehicle.SetAllWheelsManufacturer(manufacturerUserInput);
                Console.WriteLine("Please enter air pressure for all the wheels:");
                while (true)
                {
                    try
                    {
                        float airPressureUserInput = float.Parse(Console.ReadLine());
                        vehicle.SetAllWheelsAirPressure(airPressureUserInput);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number for air pressure.");
                    }
                    catch(GarageLogic.ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.WriteLine("Please enter owner's name:");
                ownerName = Console.ReadLine();
                Console.WriteLine("Please enter owner's phone number:");
                ownerPhone = Console.ReadLine();
                m_Garage.AddToGarage(new GarageLogic.VehicleOwner(ownerName, ownerPhone), vehicle);
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

        private void getLicenseNumbers()
        {
            Console.Write("Do you want to select vehicle state to filter your search by?" + Environment.NewLine + "Please enter '1' if yes, '0' if no: ");
            string userAnswer = Console.ReadLine();
            while (userAnswer.Length != 1 || (userAnswer[0] != '0' && userAnswer[0] != '1'))
            {
                Console.Write("Invalid input, please try again:");
                userAnswer = Console.ReadLine();
            }
            if (userAnswer[0] == '0')
            {
                List<string> licenseNumbers = m_Garage.GetLicenseNumbers();
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
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Please enter a value for the repair state:");
                        string userRepairStateInput = Console.ReadLine();
                        int parsedEnum = int.Parse(userRepairStateInput);
                        int validatedValue = validateEnumInput(typeof(GarageLogic.eRepairState), parsedEnum);
                        List<string> licenseNumbers = m_Garage.SortVehiclesByState((GarageLogic.eRepairState)validatedValue);
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
        }

        private void changeVehicleState()
        {
            string licenseNumber = getValidLicenseNumber();
            Console.WriteLine("Please enter the new vehicle state:");
            printEnumValues(typeof(GarageLogic.eRepairState));
            if (m_Garage.CheckIfInGarage(licenseNumber, false))
            {
                while (true)
                {
                    string userNewRepairStateInput = Console.ReadLine();
                    int newVehicleState = int.Parse(userNewRepairStateInput);
                    try
                    {
                        int validatedValue = validateEnumInput(typeof(GarageLogic.eRepairState), newVehicleState);
                        m_Garage.ChangeVehicleState(licenseNumber, (GarageLogic.eRepairState)newVehicleState);
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
            string licenseNumber = getValidLicenseNumber();
            m_Garage.PumpWheelsToMax(licenseNumber);
            Console.WriteLine("Wheels inflated to maximum.");
        }

        private void refuelVehicle()
        {
            string licenseNumber = getValidLicenseNumber();
            while (true)
            {
                int fuelType = getValidFuelType();
                float amountOfLiters = getValidFuelAmount();
                try
                {
                    m_Garage.RefuelVehicle(licenseNumber, (GarageLogic.eFuelType)fuelType, amountOfLiters);
                    Console.WriteLine("Vehicle refueled successfully!");
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please enter a different fuel type.");
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please enter a different amount of liters.");
                }
            }
        }

        private int getValidFuelType()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please select fuel type (numeric value):");
                    printEnumValues(typeof(GarageLogic.eFuelType));
                    string input = Console.ReadLine();
                    int fuelType = int.Parse(input);
                    validateEnumInput(typeof(GarageLogic.eFuelType), fuelType);
                    return fuelType;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value for the fuel type.");
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

        private float getValidFuelAmount()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter the amount of liters to fuel:");
                    string input = Console.ReadLine();
                    float amount = float.Parse(input);
                    if (amount <= 0)
                    {
                        throw new ArgumentException("Fuel amount must be greater than zero. Please try again.");
                    }
                    return amount;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value for the amount.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void chargeVehicle()
        {
            string licenseNumber = getValidLicenseNumber();
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
                    m_Garage.ChargeVehicle(licenseNumber, amountOfMinutesToCharge);
                    Console.WriteLine("Vehicle recharged.");
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

        private void printVehicleData()
        {
            string licenseNumber = getValidLicenseNumber();
            int wheelIndex = 1;
            KeyValuePair<GarageLogic.VehicleOwner, GarageLogic.Vehicle> vehicleData = m_Garage.GetVehicleDetailsByLicenseNumber(licenseNumber);
            if (m_Garage.CheckIfInGarage(licenseNumber, false))
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