namespace Ex03.GarageLogic
{
    internal class ElectricCar : ElectricVehicle
    {
        private eCarColors m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        public ElectricCar() : base(5, 34, 5.4f)
        {
        }

        public eCarColors CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
    }
}