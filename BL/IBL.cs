

using BE;
using System;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        #region guest functions
        void addGuest(GuestRequest guest);
        void addHostingUnit(HostingUnit unit);
        List<GuestRequest> getRequests();
        #endregion
       
        #region hosting Units
        HostingUnit findUnit(int unitKey);
        List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        List<HostingUnit> getAllHostingUnits();

        #endregion
        #region orders
        void addOrder(Order ord);
        List<Order> getAllOrders();
        void order(HostingUnit unit, GuestRequest guest);//add order
        bool available(HostingUnit unit, GuestRequest guest);
       

        //not finished functions
        /*        void deleteUnit(HostingUnit unit);

                void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate

            */
        void notFounde();
        bool sameUnit(HostingUnit hu1, int hostsKey);
        #endregion
        #region pl set fields check

        void addEntryDate(DateTime? selectedDate, GuestRequest g1);
        void addReleaseDate(DateTime? selectedDate, GuestRequest g1);
        void addHostNum(string text, Int32 h1);
        void addHostingUnitNum(string text, int unitKey);

        #endregion
    }
}