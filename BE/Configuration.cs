using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static string TypeDAL = ConfigurationSettings.AppSettings.Get("TypeDS");
        public static Int32 HostingUnit=0;
        public static Int32 Order=0;
        public static Int32 GuestRequest=0;
        public static Int32 BankAccountKey = 0;
    }
}
