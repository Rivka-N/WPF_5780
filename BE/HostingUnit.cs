using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        #region fields
        private Host host;
        private string hostingUnitName;
        readonly private Int32 hostingUnitKey;
        private Enums.HostingUnitType hostingUnitType;
        public bool[,] Diary { get; set; }

        public Enums.HostingUnitType HostingUnitType { get => hostingUnitType; }
        public Int32 HostingUnitKey { get => hostingUnitKey; }
        #endregion
        #region ctors
        public HostingUnit()
        {
        }
        public HostingUnit(Host host, string hostingUnitName, Enums.HostingUnitType hostingUnitType)
        {
            this.host = host;
            this.hostingUnitName = hostingUnitName;
            this.hostingUnitType = hostingUnitType;
            this.hostingUnitKey = Configuration.HostingUnit++;
        }
        #endregion

        public override string ToString()
        {
            return "Name: " + this.hostingUnitName + " Type of Unit " + HostingUnitType + "Unit Name: " + hostingUnitName;
        }
    }
}
