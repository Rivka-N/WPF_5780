using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {//makes sure running number is set correctly for all of them(Hosting Unit Key)
        #region fields
        private Enums.HostingUnitType zimmer;
        private string hostingUnitName;
        readonly private Int32 hostingUnitKey;
        private Enums.HostingUnitType hostingUnitType;
        private Enums.Area area;
        #endregion
        #region properties
        public bool[,] Diary { get; set; }
        public string HostingUnitName { get => hostingUnitName; set { hostingUnitName = value; } }
        public Enums.Area AreaVacation { get => area; set { area = value; } }
        public Host host { get; }
        public Enums.HostingUnitType HostingUnitType { get => hostingUnitType; set { hostingUnitType = value; } }
        public Int32 HostingUnitKey { get => hostingUnitKey; }
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

        public HostingUnit(Host host, string hostingUnitName, Enums.HostingUnitType zimmer) : this(host)
        {
            this.hostingUnitName = hostingUnitName;
            this.zimmer = zimmer;
        }

        public HostingUnit(Host host, int hostingUnitKey) : this(host)
        {
            this.hostingUnitKey = hostingUnitKey;
        }
        #endregion

        public override string ToString()
        {
            return "Name: " + this.hostingUnitName + " Type of Unit " + HostingUnitType + "Unit Name: " + hostingUnitName;
        }
    }
}
