using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_ownerName;
        private readonly string r_ownerPhone;
        private eRepairState m_repairState;

        public VehicleOwner(string i_ownerName, string i_ownerPhone)
        {
            r_ownerName = i_ownerName;
            r_ownerPhone = i_ownerPhone;
        }

        public string OwnerName
        {
            get { return r_ownerName; }
        }

        public string OwnerPhone
        {
            get { return r_ownerPhone; }
        }

        public eRepairState VehicleState
        {
            get { return m_repairState; }
            set { m_repairState = value; }
        }
    }
}
