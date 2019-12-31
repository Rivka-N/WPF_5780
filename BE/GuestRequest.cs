using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        #region fields
        public int GuestRequestKey { get; set; }
        string name;
        public string Name { get => name; set { name = value; } }
        string lastName;
        public string LastName { get => lastName; set { lastName = value; } }
        System.Net.Mail.MailAddress mail;
        public System.Net.Mail.MailAddress Mail { get => mail; set { mail = value; } }
        DateTime mailed;
        public DateTime Mailed { get => mailed; set { mailed = value; } }
        public Enums.OrderStatus Status { get; set; }
        DateTime registration;
        public DateTime Registration { get => registration; set { registration = value; } }
        DateTime entryDate;
        public DateTime EntryDate { get => entryDate; set { entryDate = value; } }
        DateTime releaseDate;
        public DateTime ReleaseDate { get => releaseDate; set { releaseDate = value; } }
        Enums.Area area;
        public Enums.Area AreaVacation { get => area; set { area = value; } }
        string subArea;
        public string  SubArea { get => subArea; set { subArea = value; } }
        Enums.HostingUnitType typeOfUnit;
        public Enums.HostingUnitType TypeOfUnit { get => typeOfUnit; set { typeOfUnit = value; } }
        int numAdult;
        public int NumAdult { get => numAdult; set { numAdult = value; } }
        int numChildren;
        public int NumChildren{ get => numChildren; set { numChildren= value; } }
        Enums.Preference pool;
        public Enums.Preference Pool { get=>pool; set { pool = value; } }
        Enums.Preference jacuzzi;
        public Enums.Preference Jacuzzi { get => jacuzzi; set { jacuzzi = value; } }
        Enums.Preference garden;
        public Enums.Preference Garden { get => garden; set { garden = value; } }
        Enums.Preference childrenAttractions;
        public Enums.Preference ChildrenAttractions { get => childrenAttractions; set { childrenAttractions = value; } }
        public int NumSuggestions { get; set; }//number of hosting suggestions


        #endregion
        #region ctors

        public GuestRequest(int guestKey=0, DateTime registerDate=default(DateTime))
        {
            GuestRequestKey = guestKey == 0 ? Configuration.GuestRequest++ : GuestRequestKey = guestKey;
            name = null;
            lastName = null;
            mail = null;
            registration = registerDate == default(DateTime) ? new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day) : registerDate;
            entryDate = new DateTime();
            releaseDate = new DateTime();
            area = Enums.Area.Center;
            subArea = null;
            typeOfUnit = Enums.HostingUnitType.Hotel;
            numAdult = 0;
            numChildren = 0;
            pool = Enums.Preference.Maybe;
            jacuzzi = Enums.Preference.Maybe;
            garden = Enums.Preference.Maybe;
            childrenAttractions = Enums.Preference.Maybe;
            NumSuggestions = -1;//none yet
            Status = Enums.OrderStatus.Started;
        }
        #endregion
        public override string ToString()
        {
            return "Name: " + name + " " + lastName + " \nMail: " + mail + "Days: " + entryDate.ToString("d") + " to " + releaseDate + "\nArea: " + area + " Type of unit: " + typeOfUnit +
                "\nNumber of adults: " + numAdult + " Number of children: " + numChildren + "\n";
        }
        
    }
}
