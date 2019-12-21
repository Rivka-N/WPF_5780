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

        public void addGuest(GuestRequest guest)
        {

        }

        //public void addHostingUnit(HostingUnit hostingUnit)
        //{
        //    DataSource.hostingUnits.Add(hostingUnit);
        //}

        //public void addOrder(Order order)
        //{
        //    DataSource.orders.Add(order);
        //}

        //public List<HostingUnit> getAllHostingUnits()
        //{
        //   return DataSource.hostingUnits;
        //}

        //public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicate = null)
        //{
        //    return DataSource.hostingUnits.Where(predicate).ToList();
        //}

        //public void addGuestRequest(string id, string name, int age)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Order> getOrders(Func<Order, bool> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
