using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        
        private Host host;
        private string hostingUnitName;
        private int hostingUnitKey;
        public int HostingUnitKey { get; }
        private Enums.HostingUnitType hostingUnitType;
        public bool[,] Diary { get; set; }

        #region ctors
        public HostingUnit()
        {
        }

        public HostingUnit(Host host, string hostingUnitName, Enums.HostingUnitType hostingUnitType)
        {
            this.host = host;
            this.hostingUnitName = hostingUnitName;
            this.hostingUnitType = hostingUnitType;
            this.hostingUnitKey = Configuration.HostingUnit;
        }
        #endregion
        public Enums.HostingUnitType HostingUnitType { get => hostingUnitType; }

        public override string ToString()
        {
            return this.hostingUnitName + " " + HostingUnitType ;
        }
    }
}
