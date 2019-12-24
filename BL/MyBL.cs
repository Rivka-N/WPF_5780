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

        #region add items
        public void addGuest(GuestRequest guest)//add guest to the data list in DS
        {
            myDAL.addGuest(guest.Clone());
        }

        public void addHostingUnit(HostingUnit unit)//add hostingUnit to the hostingUnit list in DS
        {
            myDAL.addHostingUnit(unit.Clone());
        }
        public void addOrder(Order ord)//add order to the order list in DS
        {
            myDAL.addOrder(ord.Clone());
        }

        public void order(HostingUnit unit, GuestRequest guest)//makes sure that the days in the request available
        {
            DateTime end = guest.ReleaseDate;
            for (DateTime start = guest.EntryDate; start <= end; start.AddDays(1))//Check availability
            {
                if (unit.Diary[start.Month, start.Day] == true)
                {
                    //code to send message to the host
                    return;//if its already occupied
                }
            }
            for (DateTime start = guest.EntryDate; start <= end; start.AddDays(1))//set the days
            {
                unit.Diary[start.Month, start.Day] = true;
            }
            guest.Status = Enums.OrderStatus.Closed;//closed status
            Order ord = new Order(guest.Registration);//makes new order
            ord.HostingUnitKey = unit.HostingUnitKey;
            ord.GuestRequestKey = guest.GuestRequestKey;
            ord.OrderDate = guest.Mailed;
            addOrder(ord);//send to the function which adds the order to the order list

        }
        #endregion


        public void mail(List<HostingUnit> Offers, GuestRequest guest)//sends mail with the list of units to the guest
        {
            //sends mail to the guest
           
            guest.Mailed = new DateTime();
        }

        public void notFounde()//if there are no units that match
        {

        }

        public bool available(HostingUnit unit, GuestRequest guest)
        {
            DateTime end = guest.ReleaseDate;
            for (DateTime start = guest.EntryDate; start <= end; start.AddDays(1))//Check availability
            {

                if (unit.Diary[start.Month, start.Day] == true)
                {
                    return false;
                }

            }
            return true;
        }

        public List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest)
        {

            List <HostingUnit> listOfUnits = new List<HostingUnit>(); ;
            //code

            for (int i = 0; i < units.Count(); i++)
            {
                if (guest.TypeOfUnit == units[i].HostingUnitType && guest.AreaVacation == units[i].AreaVacation && available(units[i], guest))
                {
                    listOfUnits.Add(units[i]);//adds to the guest list
                }
            }
            if (listOfUnits.Count() == 0)
                   notFounde();
            else
            {
               if (listOfUnits.Count() <= 5)
                   mail(listOfUnits, guest);
               else
                {
                    //code to delete units
                }
                

            }
            return listOfUnits;


        }

        #region gets
        public List<HostingUnit> getAllHostingUnits()
        {
            return myDAL.getAllHostingUnits();
        }

        public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p)
        {
            return myDAL.getHostingUnits(p);
        }

        public List<GuestRequest> getRequests()
        {
            return myDAL.getRequests();
        }

        public List<Order> getAllOrders()
        {
            return myDAL.getAllOrders();
        }
        #endregion
    }
}
