

using BE;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        #region guest functions
        void addGuest(GuestRequest guest);
        void addHostingUnit(HostingUnit unit);
        #endregion
        void addOrder(Order ord);
        void order(HostingUnit unit, GuestRequest guest);//add order
        bool available(HostingUnit unit, GuestRequest guest);

        //not finished functions
        /*        void deleteUnit(HostingUnit unit);

                void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate

            */
        void notFounde();
        void findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        List<GuestRequest> getRequests();


        #region HostingUnit
        List<HostingUnit> getAllHostingUnits();
        #endregion
    }
}