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
    public class MyBL: IBL
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
        //   return myDAL.getAllHostingUnits();
        //}

        //public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> p)
        //{
        //    return myDAL.getHostingUnits(p);
        //}
    }
}
