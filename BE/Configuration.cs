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
        public static Int32 HostingUnit= 10000000;
        public static Int32 Order=10000000;
        public static Int32 GuestRequest= 10000000;
        public static Int32 BankAccountKey = 10000000;
        public const Int32 TransactionFee= 10;//amount to charge host for every successful. has to be static?
    }
}
