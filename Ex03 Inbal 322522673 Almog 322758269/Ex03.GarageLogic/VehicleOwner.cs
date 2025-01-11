using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class VehicleOwner
    {
        private string m_ownerName;
        private string m_ownerPhone;
        private eRepairState m_repairState;

        public VehicleOwner(string i_ownerName, string i_ownerPhone)
        {
            m_ownerName = i_ownerName;
            m_ownerPhone = i_ownerPhone;
        }

        public string OwnerName
        {
            get { return r_ownerName; }
        }

        public string OwnerPhoneNumber
        {
            get { return r_ownerPhoneNumber; }
        }

        public eVehicleState VehicleState
        {
            get { return m_vehicleState; }
            set { m_vehicleState = value; }
        }
    }
}
