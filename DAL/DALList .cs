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
     
        public List<GuestRequest> getRequests()
        {
            return DS.DataSource.guestRequests.Select(guestReq => (GuestRequest)guestReq.Clone()).ToList();
        }

        public List<Order> getAllOrders()
        {
            return DataSource.orders.Select(order => (Order)order.Clone()).ToList();
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
            try
            {
                DataSource.hostingUnits.RemoveAll(hu => hu.HostingUnitKey == toDelete.HostingUnitKey);//removes unit from list.  if unsuccesful
            }
            catch
            {
                throw new dataException("unable to delete item");
            }
        }

        public void deleteGuest(GuestRequest guest)//deletes guest from other hosts' guest list
        {
          foreach(HostingUnit unit in DataSource.hostingUnits)
            {
                unit.guestForUnit.Remove(guest);
            }
        }

        public void deleteSameDate(HostingUnit unit, GuestRequest guest)//deletes units with same dates as added order
        {
            int index = DataSource.hostingUnits.FindIndex(cur => { return unit.HostingUnitKey == cur.HostingUnitKey; });//finds index of it in list
            var curUnit = DataSource.hostingUnits[index];
            var temp= from u in curUnit.guestForUnit
                                   where u.EntryDate > guest.ReleaseDate || u.ReleaseDate < guest.EntryDate
                                   select u;
            curUnit.guestForUnit = temp.ToList();

        }

        public void deleteOrders(Func<Order, bool> p)//deletes orders that have condition
        {
            try
            { DataSource.orders.RemoveAll(or => p(or));//deletes all orders that return true for p
            }
            catch
            {
                throw new dataException("no items to delete");
            }
            }
        #endregion
        #region change items
        public void changeOrder(Func<Order, bool> p1, Func<Order, Order> p2)
        {
            Order ord = DataSource.orders.Find(o=>p1(o));//finds order
            p2(ord);//changes ord
        }

        public void changeUnit(HostingUnit hostingUnit1)
        { 
            int index = DataSource.hostingUnits.FindIndex(cur=> { return hostingUnit1.HostingUnitKey == cur.HostingUnitKey; });//finds index of it in list
            if (index == -1)
                throw new dataException("unit not found");
            //puts new hosting unit with changed details instaed of old one
            DataSource.hostingUnits[index] = hostingUnit1.Clone();//sets it to be a copy of the new updated unit
        }

        public void changeStatus(GuestRequest guest, Enums.OrderStatus status)
        {
            try
            {
                DataSource.guestRequests.Find(gr => guest.Status == gr.Status).Status = status;
            }
            catch
            {
                throw new dataException("object not found");
                    
                    }

        }

        public void addCharge(HostingUnit unit, int numDays)
        {
            var found = DataSource.hostingUnits.Find(u => u.HostingUnitKey == unit.HostingUnitKey);
            found.MoneyPaid += Configuration.TransactionFee * numDays;//adds this transaction fee to total transaction fees
        }

        #endregion
    }
}
