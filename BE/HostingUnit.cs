using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {//kes sure running number is set correctly for all of them(Hosting Unit Key)
        #region fields
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
        #endregion
        #region properties
        public string HostingUnitName { get => hostingUnitName; set { hostingUnitName = value; } }
        public Enums.Area AreaVacation { get => area; set { area = value; } }
        public Host Host { get=>host; set { host = value; } }
        public Enums.HostingUnitType HostingUnitType { get => hostingUnitType; set { hostingUnitType = value; } }
        public Int32 HostingUnitKey { get => hostingUnitKey; set { hostingUnitKey = value; } }
        public Int32 NumAdult { get => numAdult; set { numAdult = value; } }
        public Int32 NumChildren { get => numChildren; set { numChildren = value; } }
        public Enums.Preference Pool { get => pool; set { pool = value; } }
        public Enums.Preference Jacuzzi { get => jacuzzi; set { jacuzzi = value; } }
        public Enums.Preference Garden { get => garden; set { garden = value; } }
        public Enums.MealType Meal{ get; set; }
        public int MoneyPaid { get; set; }//paid to owner

        [XmlIgnore]
        public bool[,] Diary { get; set; }

        [XmlArray("Diary")]
        public bool[] FlatDiary
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(31); }
        }
        #endregion
        #region ctors
        public HostingUnit()
        {
            HostingUnitName = null;
            HostingUnitKey = 0;
            Diary = new bool[13, 32];
            this.hostingUnitType = Enums.HostingUnitType.Zimmer;
            this.AreaVacation = Enums.Area.Center;
            Meal =Enums.MealType.None;
            garden = Enums.Preference.No;
            jacuzzi = Enums.Preference.No;
            pool = Enums.Preference.No;
            HostingUnitName = "";
            Host = new Host();
            MoneyPaid = 0;

        }


        public HostingUnit(Host host, int hostingUnitKey):this()
        {
            this.Host = host;
            this.hostingUnitKey = hostingUnitKey;
            MoneyPaid = 0;
        }
        #endregion

        public override string ToString()
        {
            return "Host Name: " + host.Name + " " + host.LastName + "\n" + " Type of Unit: " + HostingUnitType + " Unit Name: " + hostingUnitName+ "\n";
        }
    }

    public static class Tools//flatten array
    {
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(1);
            int columns = arr.GetLength(0);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    var test = arr[i, j];
                    arrFlattened[i * rows + j] = arr[i, j];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0)+1;//make sure cols and rows are set correctly
            int columns = length / rows+1;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[i, j] = arr[i * rows + j];
                }
            }
            return arrExpanded;
        }
    }
}
