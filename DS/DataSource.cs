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
       

       

        static DataSource()
        {
            initData();
        }

        private static void initData()
        {
            #region addGeustRequest
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000001, Name = "miri", LastName = "green", Mail = new System.Net.Mail.MailAddress("miri@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2020, 01, 01), EntryDate = new DateTime(2020, 01, 10), ReleaseDate = new DateTime(2020, 01, 15), AreaVacation = Enums.Area.Center, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.Yes, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, ChildrenAttractions = Enums.Preference.Maybe });
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000002, Name = "gal", LastName = "bloom", Mail = new System.Net.Mail.MailAddress("gal@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2019, 12, 31), EntryDate = new DateTime(2020, 02, 01), ReleaseDate = new DateTime(2019, 02, 10), AreaVacation = Enums.Area.Galil, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.Yes, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.No, ChildrenAttractions = Enums.Preference.No });
            guestRequests.Add(new GuestRequest() { GuestRequestKey = 10000003, Name = "ben", LastName = "lev", Mail = new System.Net.Mail.MailAddress("lev@gmail.com"), Status = Enums.OrderStatus.Started, Registration = new DateTime(2020, 01, 14), EntryDate = new DateTime(2020, 01, 20), ReleaseDate = new DateTime(2020, 01, 28), AreaVacation = Enums.Area.Golan, SubArea = "Tel Aviv", TypeOfUnit = Enums.HostingUnitType.Hotel, NumAdult = 2, NumChildren = 5, Pool = Enums.Preference.No, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.Yes, ChildrenAttractions = Enums.Preference.Maybe});
            #endregion

            #region  addHostingUnit
            hostingUnits.Add(new HostingUnit() { HostingUnitKey = 10000001, HostingUnitName = "a", Host = hosts.First(), AreaVacation = Enums.Area.Center, HostingUnitType = Enums.HostingUnitType.Hotel, Pool = Enums.Preference.Yes, NumAdult = 2, NumChildren = 6, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, ChildrenAttractions = Enums.Preference.Yes });
            hostingUnits.Add(new HostingUnit() { HostingUnitKey = 10000002, HostingUnitName = "b", Host = hosts.First(), AreaVacation = Enums.Area.Galil, HostingUnitType = Enums.HostingUnitType.Zimmer, Pool = Enums.Preference.Yes, NumAdult = 2, NumChildren = 0, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.No, ChildrenAttractions = Enums.Preference.No });
            #endregion
        }

    }
}
