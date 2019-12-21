

using BE;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        void addGuest(GuestRequest guest);
        void addHostingUnit(HostingUnit unit);
        void addOrder(Order ord);
        void deleteUnit(HostingUnit unit);
        
        //#region HostingUnit
        //List<HostingUnit> getAllHostingUnits();
       // #endregion
    }
}