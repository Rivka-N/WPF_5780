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
    }
}
