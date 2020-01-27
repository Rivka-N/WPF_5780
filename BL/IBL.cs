

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
        Func<GuestRequest, bool> GuestSearchQuery(string query, string child, string adult, bool? garden, bool? jaccuzi, bool? pool, int meal);

        #endregion

        #region hosting Units
        void addHostingUnit(HostingUnit unit);
        HostingUnit findUnit(int unitKey);
        List<HostingUnit> getAllHostingUnits();
        List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p);
        void deleteUnit(int unit);
        void changeUnit(HostingUnit hostingUnit1);
        List<HostingUnit> searchUnits(string text, Enums.FunctionSender fs=0);
        List<HostingUnit> searchUnits(string text, int unitType, int area, Enums.FunctionSender sender);
        HostingUnit copy(HostingUnit hosting);

        List<GuestRequest> getReleventRequests(HostingUnit unit);//returns all requests that are applicable for unit

        #endregion
        #region calculations
        int TotalSumCollectedFromUnits();

        #endregion
        #region orders
        void addOrder(Order ord);
        List<Order> getAllOrders();
        void order(HostingUnit unit, GuestRequest guest);//add order
        void sendGuestMail(HostingUnit unit, GuestRequest guest);//guest and hosting unit, sends mail to guest and creates order from details
        #endregion

        #region check PL
        System.Net.Mail.MailAddress checkMail(string text);//cheks if text is email address and returns it if it is
        #endregion
        #region searches PL
        List<Order> getOrders(Func<Order, bool> predicate);
        List<GuestRequest> searchRequests(Enums.OrderStatus status, DateTime? selectedDate, string query, Enums.FunctionSender owner);
        List<GuestRequest> searchRequests(DateTime? selectedDate, string query, Enums.FunctionSender owner);//all statuses selected
        List<Order> searchOrders(DateTime? selectedDate, string text, Enums.FunctionSender owner, Enums.OrderStatus status = Enums.OrderStatus.Closed);

        #endregion

        #region grouping and queries
        IEnumerable<Order> ordersOfUnit(HostingUnit hu);
        IEnumerable<Order> ordersOfUnit(int unitNum);
        IEnumerable<IGrouping<Host, HostingUnit>> groupHostsByUnits();
        IEnumerable<IGrouping<Enums.Area, HostingUnit>> groupUnitsByArea();
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> groupRequestsByArea();
        IEnumerable<IGrouping<Enums.MealType, GuestRequest>> groupRequestsByMeal();
        IEnumerable<IGrouping<int, BankAccount>> groupBranchesByBank();

        #endregion
    }
}