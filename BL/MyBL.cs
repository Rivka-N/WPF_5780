using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MyBL : IBL
    {
        static IDAL myDAL;

        #region Singleton
        private static readonly MyBL instance = new MyBL();

        public static MyBL Instance
        {
            get { return instance; }
        }

        static MyBL()
        {
            // string TypeDAL = ConfigurationSettings.AppSettings.Get("TypeDS");
            string TypeDAL = Configuration.TypeDAL;
            // string TypeDAL = "List";
            myDAL = factoryDAL.getDAL(TypeDAL);
        }
        private MyBL() { }
        #endregion        

        public void addGuest(GuestRequest guest)
        {
            myDAL.addGuest(guest.Clone());
        }

        public void addHostingUnit(HostingUnit unit)
        {
            myDAL.addHostingUnit(unit.Clone());
        }

        public void addOrder(Order ord)
        {
            myDAL.addOrder(ord.Clone());
        }

        public void order(HostingUnit unit, GuestRequest guest, DateTime mailed)
        {
            DateTime end = guest.ReleaseDate;
            for (DateTime start = guest.EntryDate; start <= end; start.AddDays(1))//Check availability
            {

                if (unit.Diary[start.Month, start.Day] == true)
                {
                    return;
                }

            }
            for (DateTime start = guest.EntryDate; start <= end; start.AddDays(1))//set the days
            {
                unit.Diary[start.Month, start.Day] = true;
            }
              
            Order ord = new Order(guest.Registration, mailed);
            ord.hostingUnitKey = unit.HostingUnitKey;
            ord.guestRequestKey = guest.GuestRequestKey;
            addOrder(ord);
            
        }

        public void mail(List<HostingUnit> Offers)
        {
            //sends mail to the guest
            DateTime today = new DateTime();//find out how to sends this to order function
           
        }

        public void findUnit(List<HostingUnit> units, GuestRequest guest)
        {
           
            List<HostingUnit> listOfUnits;
            //code
            mail(listOfUnits);
           

        }

        //public void addHostingUnit(HostingUnit hostingUnit)
        //{
        //    myDAL.addHostingUnit(hostingUnit);
        //}

        //public void addOrder(Order order)
        //{
        //    myDAL.addOrder(order);
        //}

        //public List<HostingUnit> getAllHostingUnits()
        //{
        //    return myDAL.getAllHostingUnits();
        //}

        //public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p)
        //{
        //    return myDAL.getHostingUnits(p);
        //}
    }
}
