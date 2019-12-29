

using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface IBL
    {
        #region guest functions
        void addGuest(GuestRequest guest);
        void addHostingUnit(HostingUnit unit);
        List<GuestRequest> getRequests();
        GuestRequest findGuest(GuestRequest g1, string text);//puts text as g1's number and returns instance of it in ds if found

        #endregion

        #region hosting Units
        HostingUnit findUnit(int unitKey);
        List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        List<HostingUnit> getAllHostingUnits();

        #endregion
        #region orders
        void addOrder(Order ord);
        List<Order> getAllOrders();
        void checkOrder(Host h1, HostingUnit hu1, GuestRequest g1, GuestRequest foundGuest);
        void order(HostingUnit unit, GuestRequest guest);//add order
        bool available(HostingUnit unit, GuestRequest guest);

        //not finished functions
        /*        void deleteUnit(HostingUnit unit);

                void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate. 
                //needs to recieve guestRequest or mailing address too

            */
        //void notFounde();

        #endregion

        #region pl set fields check

        void addEntryDate(DateTime? selectedDate, GuestRequest g1);
        void addReleaseDate(DateTime? selectedDate, GuestRequest g1);
        void addHostNum(string text, Int32 h1);
        void addHostingUnitNum(string text, int unitKey);
        void addMail(string text, GuestRequest g1);
        bool sameUnit(HostingUnit hu1, int hostsKey);
        bool checkGuest(GuestRequest g1);

        #endregion
        #region grouping and queries
        IEnumerable<Order> ordersByUnit(HostingUnit hu);
        IEnumerable<Order> ordersByUnit(int unitNum);
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> groupByArea();
        string printOrdersByUnit(int unitNum);

        #endregion
    }
}