using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        public int GuestRequestKey { get; }
        string name;
        public string Name { get => name; set { name = value; } }
        string lastName;
        public string LastName { get => lastName; set { lastName = value; } }
        string mail;
        public string Mail { get => mail; set { mail = Mail; } }
        public Enums.OrderStatus Status { get; set; }
        DateTime registration;
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

       
        //tostring
        public GuestRequest(string fName, string lName, string em, DateTime enter, DateTime rel, Enums.Area ar, string sArea, Enums.HostingUnitType type, int nAdult, int Nchild, Enums.Preference isPool, Enums.Preference isJacuzzi, Enums.Preference isGarden, Enums.Preference isAttractions)
        {
            //add default to enum type 

            GuestRequestKey = Configuration.GuestRequest;
            name = fName;
            lastName = lName;
            mail = em;
            registration = new DateTime();
            entryDate = enter;
            releaseDate = rel;
            area = ar;
            subArea = sArea;
            typeOfUnit = type;
            numAdult = nAdult;
            numChildren = Nchild;
            pool = isPool;
            jacuzzi = isJacuzzi;
            garden = isGarden;
            childrenAttractions = isAttractions;
            NumSuggestions = -1;//none yet
            Status = Enums.OrderStatus.Started;
            Configuration.GuestRequest++;


        }
        public GuestRequest()
        {
            GuestRequestKey = Configuration.GuestRequest;
            Configuration.GuestRequest++;
            name = null;
            lastName = null;
            mail = null;
            registration = new DateTime();
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
        public override string ToString()
        {
            return "First Name: " + name + "Last Name: " + lastName + "Mail: " + mail + "Order status: " + Status + "Date of registration: " + registration + "Entry day: " + entryDate + "Release date: " + releaseDate + "Area: " + area + "Sub area: " + subArea + "Type of unit: " + typeOfUnit +
                "Number of adults: " + numAdult + "Number of children: " + numChildren + "Want pool: " + pool + "Want jecuzzi: " + jacuzzi + "Want garden: " + garden + "want children attractions" + childrenAttractions;
        }
        
    }
}
