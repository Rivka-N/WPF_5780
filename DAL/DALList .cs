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
        public List<GuestRequest> getRequests(Func<GuestRequest, bool> predicate)
        {
            var requests = from guest in DataSource.guestRequests
                           let p = predicate(guest)
                           where p
                           select guest.Clone();
            return requests.ToList();
            //alternative: return DataSource.guestRequests.Where(predicate).Select(gr => (GuestRequest)gr.Clone()).ToList();
        }

        #endregion
        #region search functions
        public HostingUnit findUnit(int unitKey)//returns first instance of unitKey as a hosting unit key inside list of hosting units
        {
            return DataSource.hostingUnits.Find(hu => hu.HostingUnitKey == unitKey).Clone();
        }

        public GuestRequest findGuest(GuestRequest g1)
        {
            return findGuest(g1.GuestRequestKey);
        }
        public GuestRequest findGuest(int g1)
        {
            return DataSource.guestRequests.Find(guest => guest.GuestRequestKey == g1).Clone();
        }

        #endregion
        #region delete
        public void deleteUnit(HostingUnit toDelete)
        {
            if (!DataSource.hostingUnits.Remove(toDelete))//removes unit from list
                throw new dataException("unable to delete item");
        }
        #endregion
        #region change items
        public void changeUnit(HostingUnit hostingUnit1)
        { 
            int index = DataSource.hostingUnits.FindIndex(cur=> { return hostingUnit1.HostingUnitKey == cur.HostingUnitKey; });//finds index of it in list
            if (index == -1)
                throw new dataException("unit not found");
            HostingUnit hu = DataSource.hostingUnits[index].Clone();//clones current unit
            //checks what changed
            {

            }
        }
        #endregion
    }
}
