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
        void changeUnit(HostingUnit hostingUnit1);
        void addHostingUnit(HostingUnit unit);
        #endregion

        #region GuestRequest
        void addGuest(GuestRequest guest);
        
        GuestRequest findGuest(int g1);
        GuestRequest findGuest(GuestRequest g1);//finds by guest key
        List<GuestRequest> getRequests(Func<GuestRequest, bool> predicate);
        List<GuestRequest> getRequests();
        #endregion

        #region order
        List<Order> getAllOrders();
        void addOrder(Order ord);
        #endregion
        //#region Order
        //void addOrder(Order order);
        //List<Order> getOrders(Func<Order, bool> predicate);
        //#endregion
    }
}
