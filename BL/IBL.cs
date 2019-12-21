

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
        void order(HostingUnit unit, GuestRequest guest, DateTime mailed);//add order
        void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate
        void findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        
        //#region HostingUnit
        //List<HostingUnit> getAllHostingUnits();
        // #endregion
    }
}