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
            //update guest status
            //take off transaction fee
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

        #region order
        public void mail(List<HostingUnit> Offers, GuestRequest guest)//sends mail with the list of units to the guest
        {
            //sends mail to the guest
           
            guest.Mailed = new DateTime();
        }

        //public void notFounde()//if there are no units that match
        //{

        //}

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

        public void checkOrder(Host h1, HostingUnit hu1, GuestRequest g1, GuestRequest foundGuest)//if it's a valid order, adds it to orders
        {
            if (h1.HostKey != hu1.Host.HostKey)
                throw new InvalidException("host details don't match");
            if (g1.GuestRequestKey != foundGuest.GuestRequestKey)
                throw new InvalidException("guest details don't match");
            if (foundGuest.Status == Enums.OrderStatus.Closed)
                throw new InvalidException("guest already booked");
            order(hu1, foundGuest);//adds order
        }

        #endregion


        #region finding methods
        public HostingUnit findUnit(int unitKey)
        {
            return myDAL.findUnit(unitKey);
        }
        public List<HostingUnit> findUnit(List<HostingUnit> units, GuestRequest guest)
        {

            List<HostingUnit> listOfUnits = new List<HostingUnit>(); 
            //code

            for (int i = 0; i < units.Count(); i++)
            {
                if (guest.TypeOfUnit == units[i].HostingUnitType && guest.AreaVacation == units[i].AreaVacation && available(units[i], guest))
                {
                    listOfUnits.Add(units[i]);//adds to the guest list
                }
            }
            if (listOfUnits.Count() == 0)
                throw new InvalidException("no units found");
            //notFounde();
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

        public GuestRequest findGuest(GuestRequest g1, string text)
        {
            int guestNum;
            if (!Int32.TryParse(text, out guestNum)||guestNum<=0)
                throw new InvalidException("invalid guest number");
            g1.GuestRequestKey = guestNum;
            return (myDAL.findGuest(g1));
            

        }
        #endregion

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
        #region add and check fields from pl
        public void addEntryDate(DateTime? selectedDate, GuestRequest g1)//adds selected date to guest
        {
            g1.EntryDate = (DateTime)selectedDate;
            if (g1.ReleaseDate!=default(DateTime))
            {
                if (g1.ReleaseDate <= g1.EntryDate)
                    throw new InvalidException("entry date is smaller");
            }

         }

        public void addReleaseDate(DateTime? selectedDate, GuestRequest g1)
        {
            g1.ReleaseDate = (DateTime)selectedDate;
            if (g1.EntryDate != default(DateTime))
            {
                if (g1.ReleaseDate <= g1.EntryDate)
                    throw new InvalidException("entry date is smaller");
            }
            else if (g1.ReleaseDate == DateTime.Today)
                throw new InvalidException("invalid release date");
        }

        public void addHostNum(string text, Int32 h1)//adds host number to host
        {
            if (Int32.TryParse(text, out h1))
            {
                if (h1 < 0)
                    throw new InvalidException("invalid host num");
            }
            else
                throw new InvalidException("invalid host num");
           
        }

        public void addHostingUnitNum(string text, int unitKey)//adds hosting unit number recieved to hosting unit
        {
            if (Int32.TryParse(text, out unitKey))
            {
                if (unitKey<=0)
                {
                    throw new InvalidException("invalid unit number");
                }
            }
            else
                throw new InvalidException("invalid unit number");

        }

        public bool sameUnit(HostingUnit hu1, int hostsKey)//checks if hu1 and hostkey point to the same unit
        {
            return (hu1.Host.HostKey == hostsKey) ;
        }

        public void addMail(string text, GuestRequest g1)//checks if recieved mail is valid
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(text);
                if (mail.Address != text)
                    throw new InvalidException("invalid email");
                g1.Mail = mail;
            }
            catch (Exception ex)
            {
                throw new InvalidException(ex.Message);
            }

        }


        public bool checkGuest(GuestRequest g1)


        {
            if (g1.EntryDate == default(DateTime) || g1.ReleaseDate < g1.EntryDate)//invalid date
                throw new InvalidException("invalid date");
            if (g1.NumAdult == 0 && g1.NumChildren == 0)//no guests
                throw new InvalidException("invalid number of guests");
            if (g1.Name == null || g1.Name == "")
                throw new InvalidException("invalid first name");
            if (g1.LastName == null || g1.LastName == "")//invalid last name
                throw new InvalidException("invalid last name" +
                    "");
            if (g1.Mail == null)//has mail address
                throw new InvalidException("invalid email");
            try
            {
                addGuest(g1);//if it's valid adds the guest
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidException(ex.Message);
            }

        }

        #endregion
        #region grouping

        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> groupByArea()
        {
            var guests=myDAL.getRequests();
            var groupArea = from GuestRequest in guests
                            group GuestRequest by GuestRequest.AreaVacation into newGroup
                            select newGroup;
            return groupArea;
        }
       public IEnumerable<Order> ordersByUnit(HostingUnit hu)
        {
            return groupOrdersByUnit(hu.HostingUnitKey);
        }
        public IEnumerable<Order>ordersByUnit(int unitNum)
        {
            var allOrders = myDAL.getAllOrders();
            var thisUnit = from order in allOrders
                           let orderKey = order.HostingUnitKey//saves unit key for easier access
                           where orderKey == unitNum
                           select order;
            return thisUnit;
        }

        public string printOrdersByUnit(int unitNum)//return string of all orders from that unit
        {
            var orderUnit = ordersByUnit(unitNum);
            string ans = "";
            foreach (Order ord in orderUnit)
                ans += ord;
            return ans;
        }

        #endregion
    }
}
