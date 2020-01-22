

using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface IBL
    {
        #region guest functions
        void addGuest(GuestRequest guest);
        List<GuestRequest> getRequests();
        List<GuestRequest> getRequests(Func<GuestRequest, bool> p);
        GuestRequest findGuest(GuestRequest g1, string text);//puts text as g1's number and returns instance of it in ds if found
        Func<GuestRequest, bool> GuestSearchQuery(DateTime? selectedDate, string query, Enums.FunctionSender owner);

        #endregion

        #region hosting Units
        void addHostingUnit(HostingUnit unit);
        HostingUnit findUnit(int unitKey);
        List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        List<HostingUnit> getAllHostingUnits();
        List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p);
        void deleteUnit(int unit);
        void changeUnit(HostingUnit hostingUnit1);
        List<HostingUnit> searchUnits(string text, Enums.FunctionSender fs=0);
        List<HostingUnit> searchUnits(string text, int unitType, int area, Enums.FunctionSender sender);

        bool checkUnit(HostingUnit hostingUnit1);

        List<GuestRequest> getReleventRequests(HostingUnit unit);//returns all requests that are applicable for unit

        #endregion
        #region calculations
        int TotalSumCollectedFromUnits();

        #endregion
        #region orders
        void addOrder(Order ord);
        List<Order> getAllOrders();
        void checkOrder(Host h1, HostingUnit hu1, GuestRequest g1, GuestRequest foundGuest);
        void order(HostingUnit unit, GuestRequest guest);//add order
        List<Order> getOrders(Func<Order, bool> predicate);
        List<GuestRequest> searchRequests(Enums.OrderStatus status, DateTime? selectedDate, string query, Enums.FunctionSender owner);
        List<GuestRequest> searchRequests(DateTime? selectedDate, string query, Enums.FunctionSender owner);//all statuses selected
        List<Order> searchOrders(DateTime? selectedDate, string text, Enums.FunctionSender owner, Enums.OrderStatus status = Enums.OrderStatus.Closed);
        string printOrdersByUnit(int unitNum);
        void sendGuestMail(HostingUnit unit, GuestRequest guest);//guest and hosting unit, sends mail to guest and creates order from details

        //not finished functions
        /*   void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate. 
                //needs to recieve guestRequest or mailing address too

            */

        #endregion

        #region pl set fields check

       
        void addHostNum(string text, out Int32 h1);
        void addHostingUnitNum(string text, out int unitKey);
        void addMail(string text, GuestRequest g1);
        void addMail(string text, Host h1);
        bool sameUnit(HostingUnit hu1, int hostsKey);

        #endregion
        #region grouping and queries
        IEnumerable<Order> ordersOfUnit(HostingUnit hu);
        IEnumerable<Order> ordersOfUnit(int unitNum);
        IEnumerable<IGrouping<Host, HostingUnit>> groupHostsByUnits();
        IEnumerable<IGrouping<Enums.Area, HostingUnit>> groupUnitsByArea();
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> groupRequestsByArea();
        #endregion
    }
}