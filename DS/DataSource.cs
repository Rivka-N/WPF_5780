using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<HostingUnit> hostingUnits = new List<HostingUnit>();
        public static List<Order> orders = new List<Order>();
        public static List<GuestRequest> guestRequests = new List<GuestRequest>();
        public static List<Host> hosts = new List<Host>();
        public static List<BankAccount> banks = new List<BankAccount>();


       

        static DataSource()
        {
            initData();
        }

        private static void initData()
        {
            #region addGeustRequest
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000011, Name = "miri", LastName = "green", Mail = new System.Net.Mail.MailAddress("rgrin4365@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2020, 01, 01), EntryDate = new DateTime(2020, 01, 10), ReleaseDate = new DateTime(2020, 01, 15), AreaVacation = Enums.Area.Center, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.Yes, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, Meal= Enums.MealType.None});
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000012, Name = "gal", LastName = "bloom", Mail = new System.Net.Mail.MailAddress("gal@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2019, 12, 31), EntryDate = new DateTime(2020, 02, 01), ReleaseDate = new DateTime(2019, 02, 10), AreaVacation = Enums.Area.Galil, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.Yes, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.No, Meal= Enums.MealType.Half});
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000013, Name = "ben", LastName = "lev", Mail = new System.Net.Mail.MailAddress("lev@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2020, 01, 14), EntryDate = new DateTime(2020, 01, 20), ReleaseDate = new DateTime(2020, 01, 28), AreaVacation = Enums.Area.Golan, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.No, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.Yes, Meal = Enums.MealType.Full});
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000118, Name = "galit", LastName = "levi", Mail = new System.Net.Mail.MailAddress("levi@gmail.com"), Status = Enums.OrderStatus.Mailed, Registration = new DateTime(2020, 03, 03), EntryDate = new DateTime(2020, 01, 02), ReleaseDate = new DateTime(2020, 01, 03), AreaVacation = Enums.Area.Center, TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 1, NumChildren = 1, Pool = Enums.Preference.No });
            #endregion

            #region addHosts
            hosts.Add(new Host() { HostKey = 11111111, Name = "yoni", LastName = "cohen", Mail = new System.Net.Mail.MailAddress("yoni@gmail.com"), Bank = banks[0], CollectionClearance = true });
            hosts.Add(new Host() { HostKey = 11111112, Name = "liel", LastName = "levi", Mail = new System.Net.Mail.MailAddress("liel@gmail.com"), Bank = banks[1], CollectionClearance = true });
            hosts.Add(new Host() { HostKey = 11111231, Name = "tehila", LastName = "yosef", Mail = new System.Net.Mail.MailAddress("tehila@gmail.com"), Bank = banks[2], CollectionClearance = true });
            #endregion

            #region  addHostingUnit
            hostingUnits.Add(item: new HostingUnit() { HostingUnitKey = 10000001, HostingUnitName = "a", Host = hosts[2], AreaVacation = Enums.Area.Center, HostingUnitType = Enums.HostingUnitType.Hotel, Pool = Enums.Preference.Yes, NumAdult = 12, NumChildren = 2, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, Meal=Enums.MealType.Full, MoneyPaid=23});
            hostingUnits.Add(new HostingUnit() { HostingUnitKey = 10000002, HostingUnitName = "b", Host = hosts[1], AreaVacation = Enums.Area.Center, HostingUnitType = Enums.HostingUnitType.Zimmer, Pool = Enums.Preference.Yes, NumAdult = 2, NumChildren = 0, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.No, Meal = Enums.MealType.Half, MoneyPaid=100});
            hostingUnits.Add(new HostingUnit() { HostingUnitKey = 10000003, HostingUnitName = "c", Host = hosts[0], AreaVacation = Enums.Area.Golan, HostingUnitType = Enums.HostingUnitType.Camping, Pool = Enums.Preference.No, NumAdult = 10, NumChildren = 2, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, Meal=Enums.MealType.Half });
            #endregion

            #region addOrders
            orders.Add(new Order() { HostingUnitKey = 10000001, GuestRequestKey = 10000118, OrderKey = 1000001, Status = Enums.OrderStatus.Mailed, CreateDate=new DateTime(01/02/2020), OrderDate=new DateTime(01/02/2034) });
            //orders.Add(new Order() { HostingUnitKey = 10000001, GuestRequestKey = 10000011, OrderKey = 10000111, OrderDate = new DateTime(2020, 01, 01), CreateDate= new DateTime(2020, 01, 01), Status=Enums.OrderStatus.Mailed });
            //orders.Add(new Order() { HostingUnitKey = 10000002, GuestRequestKey = 10000012, OrderKey = 10000112, OrderDate = new DateTime(2020, 01, 10), CreateDate=new DateTime(2020,01,01), Status=Enums.OrderStatus.Closed });
            //orders.Add(new Order() { HostingUnitKey = 10000003, GuestRequestKey = 10000013, OrderKey = 10000113, OrderDate = new DateTime(2020, 01, 15), CreateDate = new DateTime(2019, 08, 03), Status=Enums.OrderStatus.Closed });
            #endregion 
            
        }

    }
}
