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
        List <HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicate = null);
        HostingUnit findUnit(int unitKey);
        void deleteUnit(HostingUnit toDelete);
        #endregion

        #region GuestRequest
        void addGuest(GuestRequest guest);
        void addHostingUnit(HostingUnit unit);
        void addOrder(Order ord);
        List<GuestRequest> getRequests();
        GuestRequest findGuest(int g1);
        GuestRequest findGuest(GuestRequest g1);//finds by guest key
        List<GuestRequest> getRequests(Func<GuestRequest, bool> predicate);
        #endregion

        #region order
        List<Order> getAllOrders();
        #endregion
        //#region Order
        //void addOrder(Order order);
        //List<Order> getOrders(Func<Order, bool> predicate);
        //#endregion
    }
}
