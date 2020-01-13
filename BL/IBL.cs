

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
        List<GuestRequest> getRequests(Func<GuestRequest, bool> p);
        GuestRequest findGuest(GuestRequest g1, string text);//puts text as g1's number and returns instance of it in ds if found

        #endregion

        #region hosting Units
        HostingUnit findUnit(int unitKey);
        List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest);//find hostingUnit accoerding to the guestRequest
        List<HostingUnit> getAllHostingUnits();
        List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p);
        List<Order> ordersByUnit();
        void deleteUnit(int unit);
        void changeUnit(HostingUnit hostingUnit1);
        List<HostingUnit> searchUnits(string text, Enums.FunctionSender fs=0);


        #endregion
        #region calculations
        int TotalSumCollectedFromUnits();

        #endregion
        #region orders
        void addOrder(Order ord);
        List<Order> getAllOrders();
        void checkOrder(Host h1, HostingUnit hu1, GuestRequest g1, GuestRequest foundGuest);
        void order(HostingUnit unit, GuestRequest guest);//add order
        bool available(HostingUnit unit, GuestRequest guest);
        List<Order> getOrders(Func<Order, bool> predicate);
        List<GuestRequest> searchRequests(Enums.OrderStatus status, DateTime? selectedDate, string query, Enums.FunctionSender owner);
        List<GuestRequest> searchRequests(DateTime? selectedDate, string query, Enums.FunctionSender owner);//all statuses selected
        List<Order> searchOrders(DateTime? selectedDate, string text, Enums.FunctionSender owner, Enums.OrderStatus status = Enums.OrderStatus.Closed);

        //not finished functions
        /*   void mail(List<HostingUnit> Offers);//sends mail to the guest with all the hostingUnits which appropriate. 
                //needs to recieve guestRequest or mailing address too

            */

        #endregion

        #region pl set fields check

        void addEntryDate(DateTime? selectedDate, GuestRequest g1);
        void addReleaseDate(DateTime? selectedDate, GuestRequest g1);
        void addHostNum(string text, out Int32 h1);
        void addHostingUnitNum(string text, out int unitKey);
        void addMail(string text, GuestRequest g1);
        void addMail(string text, Host h1);
        void checkPhone(string text, Host host);//checks if it's a number and adds it to the host if so
        bool sameUnit(HostingUnit hu1, int hostsKey);
        bool checkGuest(GuestRequest g1);

        #endregion
        #region grouping and queries
        //IEnumerable<IGrouping<int, Order>>groupOrdersbyUnit()
        IEnumerable<Order> ordersByUnit(HostingUnit hu);
        IEnumerable<Order> ordersByUnit(int unitNum);
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> groupByArea();
        string printOrdersByUnit(int unitNum);
        bool checkUnit(HostingUnit hostingUnit1);
        #endregion
    }
}