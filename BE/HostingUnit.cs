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
        public Host host { get; }
        public string hostingUnitName { get => hostingUnitName; set { hostingUnitName = value; } }
        readonly private Int32 hostingUnitKey;
        private Enums.HostingUnitType hostingUnitType;
        public bool[,] Diary { get; set; }
        Enums.Area area;
        public Enums.Area AreaVacation { get => area; set { area = value; } }

        public Enums.HostingUnitType HostingUnitType { get => HostingUnitType; set { HostingUnitType = value; } }
        public Int32 HostingUnitKey { get => hostingUnitKey; set { HostingUnitKey = value; } }
        #endregion
        #region ctors
        public HostingUnit(Host host)
        {
            this.host = host;
            Diary = new bool[12, 31];
        }
        public HostingUnit(Host host, string hostingUnitName, Enums.HostingUnitType hostingUnitType, Enums.Area area)
        {
            this.host = host;
            this.hostingUnitName = hostingUnitName;
            this.hostingUnitType = hostingUnitType;
            this.hostingUnitKey = Configuration.HostingUnit++;
            this.AreaVacation = area;

            Diary = new bool[12,31];
        }
        #endregion

        public override string ToString()
        {
            return "Name: " + this.hostingUnitName + " Type of Unit " + HostingUnitType + "Unit Name: " + hostingUnitName;
        }
    }
}
