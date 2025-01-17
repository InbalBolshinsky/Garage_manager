namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhone;
        private eRepairState m_RepairState;

        public VehicleOwner(string i_ownerName, string i_ownerPhone)
        {
            r_OwnerName = i_ownerName;
            r_OwnerPhone = i_ownerPhone;
        }

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return r_OwnerPhone; }
        }

        public eRepairState RepairState
        {
            get { return m_RepairState; }
            set { m_RepairState = value; }
        }
    }
}