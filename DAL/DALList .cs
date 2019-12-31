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
            guest.GuestRequestKey = Configuration.GuestRequest++;//sets request key
            DS.DataSource.guestRequests.Add(guest.Clone());
        }

        public void addHostingUnit(HostingUnit unit)
        {
            if (unit == null)
                throw new dataException("no unit");
            
            //check no other host exists with same id?
            if (unit.Host != null)//if there's a host
            {
                var allUnits = getAllHostingUnits();
                if (allUnits.Find(u => u.Host.HostKey == unit.Host.HostKey) != null)//if another host with same id exists
                    throw new dataException("host already exists");
                if (unit.Host.Bank != null)//if there is a bank
                {
                    unit.Host.Bank.BankAcountNumber = Configuration.BankAccountKey++;//sets running bank account number
                }

            }
            else
                throw new dataException("no host");
            unit.HostingUnitKey = Configuration.HostingUnit++;//sets unit number
            DS.DataSource.hostingUnits.Add(unit.Clone());

        }

        public void addOrder(Order ord)
        {
            ord.OrderKey = Configuration.Order;//sets order number
            Configuration.Order++;
            DS.DataSource.orders.Add(ord.Clone());
        }

        public void addGuestToUnit(HostingUnit hostingUnit, GuestRequest guest)
        {
           int index = DataSource.hostingUnits.FindIndex(a => a.HostingUnitKey == hostingUnit.HostingUnitKey);
            DataSource.hostingUnits[index].guestForUnit.Add(guest.Clone());
        }
        #endregion
        #region get Lists
        public List<HostingUnit> getAllHostingUnits()
        {
            return DS.DataSource.hostingUnits.Select(hus=>(HostingUnit)hus.Clone()).ToList();
            //return hosting units
        }
        public List<Order> getOrders(Func<Order, bool> predicate = null)
        {
            var ords = from ord in DataSource.orders
                       let p=predicate(ord)
                       where p
                       select ord.Clone();
            return ords.ToList();

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
            if (!DataSource.hostingUnits.Remove(toDelete))//removes unit from list.  if unsuccesful
                throw new dataException("unable to delete item");
        }

        public void deleteGuest(GuestRequest guest)
        {
          foreach(HostingUnit unit in DataSource.hostingUnits)
            {
                unit.guestForUnit.Remove(guest);
            }
        }

        public void deleteSameDate(HostingUnit unit, GuestRequest guest)
        {
            int index = DataSource.hostingUnits.FindIndex(cur => { return unit.HostingUnitKey == cur.HostingUnitKey; });//finds index of it in list
            var curUnit = DataSource.hostingUnits[index];
            var temp= from u in curUnit.guestForUnit
                                   where u.EntryDate > guest.ReleaseDate || u.ReleaseDate < guest.EntryDate
                                   select u;
            curUnit.guestForUnit = temp.ToList();

        }

        #endregion
        #region change items
        public void changeUnit(HostingUnit hostingUnit1)
        { 
            int index = DataSource.hostingUnits.FindIndex(cur=> { return hostingUnit1.HostingUnitKey == cur.HostingUnitKey; });//finds index of it in list
            if (index == -1)
                throw new dataException("unit not found");
            //puts new hosting unit with changed details instaed of old one
            DataSource.hostingUnits[index] = hostingUnit1.Clone();//sets it to be a copy of the new updated unit
        }




        #endregion
    }
}
