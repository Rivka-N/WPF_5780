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
        private Int32 hostingUnitKey;
        private Enums.HostingUnitType hostingUnitType;
        private Enums.Area area;
        private Host host;
        Int32 numAdult;
        Int32 numChildren;
        Enums.Preference pool;
        Enums.Preference jacuzzi;
        Enums.Preference garden;
        Enums.Preference childrenAttractions;
        #endregion
        #region properties
        public bool[,] Diary { get; set; }
        public string HostingUnitName { get => hostingUnitName; set { hostingUnitName = value; } }
        public Enums.Area AreaVacation { get => area; set { area = value; } }
        public Host Host { get=>host; set { host = value; } }
        public Enums.HostingUnitType HostingUnitType { get => hostingUnitType; set { hostingUnitType = value; } }
        public Int32 HostingUnitKey { get => hostingUnitKey; set { hostingUnitKey = value; } }
        public Enums.HostingUnitType Zimmer { get=>zimmer; set { zimmer = value; } }
        public Int32 NumAdult { get => numAdult; set { numAdult = value; } }
        public Int32 NumChildren { get => numChildren; set { numChildren = value; } }
        public Enums.Preference Pool { get => pool; set { pool = value; } }
        public Enums.Preference Jacuzzi { get => jacuzzi; set { jacuzzi = value; } }
        public Enums.Preference Garden { get => garden; set { garden = value; } }
        public Enums.Preference ChildrenAttractions { get => childrenAttractions; set { childrenAttractions = value; } }
        #endregion
        #region ctors
        public HostingUnit()
        {
            HostingUnitName = null;
            HostingUnitKey = 0;
            Diary = new bool[13, 32];
            this.hostingUnitType = Enums.HostingUnitType.Zimmer;
            this.AreaVacation = Enums.Area.Center;
            childrenAttractions = Enums.Preference.No;
            garden = Enums.Preference.No;
            jacuzzi = Enums.Preference.No;
            pool = Enums.Preference.No;
            HostingUnitName = "";
            Host = new Host();

        }

        public HostingUnit(Host host, string hostingUnitName, Enums.HostingUnitType hostingUnitType, Enums.Area area=Enums.Area.Center)
        {
            this.host = host;
            this.hostingUnitName = hostingUnitName;
            this.hostingUnitType = hostingUnitType;
            this.hostingUnitKey = Configuration.HostingUnit++;
            this.AreaVacation = area;
            Diary = new bool[13,32];
            childrenAttractions = Enums.Preference.No;
            garden = Enums.Preference.No;
            jacuzzi = Enums.Preference.No;
            pool= Enums.Preference.No;

        }


        public HostingUnit(Host host, int hostingUnitKey)
        {
            this.Host = host;
            this.hostingUnitKey = hostingUnitKey;
            Diary = new bool[13, 32];
            this.hostingUnitType = Enums.HostingUnitType.Zimmer;
            this.hostingUnitKey = 0;
            this.AreaVacation = Enums.Area.Center;
            childrenAttractions = Enums.Preference.No;
            garden = Enums.Preference.No;
            jacuzzi = Enums.Preference.No;
            pool = Enums.Preference.No;
            HostingUnitName = "";
        }
        #endregion

        public override string ToString()
        {
            return "Name: " + this.hostingUnitName+ "\n" + " Type of Unit: " + HostingUnitType + " Unit Name: " + hostingUnitName+"\n" + " pool: " + pool + "\n" + " jacuzzi: " + jacuzzi + "\n" + " garden: "+ garden + "\n" + " childrenAttractions: " + childrenAttractions + "\n";
        }
    }
}
