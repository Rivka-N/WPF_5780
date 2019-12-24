using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    internal class DALList : IDAL
    {
        #region Singleton
        private static readonly DALList instance = new DALList();
        public static DALList Instance
        {
            get { return instance; }
        }

        private DALList() { }
        static DALList() { }

        #endregion

        #region add Items
        public void addGuest(GuestRequest guest)
        {
            DS.DataSource.guestRequests.Add(guest.Clone());
        }

        public void addHostingUnit(HostingUnit unit)
        {
            DS.DataSource.hostingUnits.Add(unit.Clone());
        }

        public void addOrder(Order ord)
        {
            DS.DataSource.orders.Add(ord.Clone());
        }
        #endregion
        #region get Lists

        public List<HostingUnit> getAllHostingUnits()
        {
            return DS.DataSource.hostingUnits.Select(hus=>(HostingUnit)hus.Clone()).ToList();
            //return hosting units
        }

        public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicate = null)
        {
            return DataSource.hostingUnits.Where(predicate).Select(hu => (HostingUnit)hu.Clone()).ToList();

        }
        public List<GuestRequest> getRequests()
        {
            return DS.DataSource.guestRequests.Select(guestReq => (GuestRequest)guestReq.Clone()).ToList();
        }

        public List<Order> getAllOrders()
        {
            return DataSource.orders.Select(order=>(Order)order.Clone()).ToList();
        }


        #endregion
        #region search functions
        public HostingUnit findUnit(int unitKey)//returns first instance of unitKey as a hosting unit key inside list of hosting units
        {
            return DataSource.hostingUnits.Find(hu => hu.HostingUnitKey == unitKey);
        }


        #endregion
    }
}
