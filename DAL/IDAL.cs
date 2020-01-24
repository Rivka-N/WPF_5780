using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        
        #region HostingUnit        
        //void addHostingUnit(HostingUnit hostingUnit);//duplicate function
        List<HostingUnit> getAllHostingUnits();
        HostingUnit findUnit(int unitKey);
        void deleteUnit(HostingUnit toDelete);
        void changeUnit(HostingUnit hostingUnit1);
        void addHostingUnit(HostingUnit unit);

        #endregion

        #region GuestRequest
        void addGuest(GuestRequest guest);
        
        GuestRequest findGuest(int g1);
        GuestRequest findGuest(GuestRequest g1);//finds by guest key
        List<GuestRequest> getRequests();
        void changeStatus(GuestRequest guest, Enums.OrderStatus status);
        #endregion

        #region order
        List<Order> getAllOrders();
        void addOrder(Order ord);
        void changeOrder(Func<Order, bool> p1, Func<Order, Order> p2);
        void addCharge(HostingUnit unit, int numDays);

        #region delete
        void deleteOrders(Func<Order, bool> p);
        #endregion

        #endregion
    }
}
