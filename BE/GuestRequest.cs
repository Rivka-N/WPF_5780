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
        string lastName;
        string mail;
        public Enums.OrderStatus Status { get; set; }
        DateTime registration;
        DateTime entryDate;
        DateTime releaseDate;
        string area;
        string subArea;
        Enums.HostingUnitType typeOfUnit;
        int numAdult;
        int numChildren;
        Enums.Preference pool;
        Enums.Preference jacuzzi;
        Enums.Preference garden;
        Enums.Preference childrenAttractions;

       
        //tostring
        public GuestRequest(string fName, string lName, string em, DateTime enter, DateTime rel, string ar, string sArea, Enums.HostingUnitType type, int nAdult, int Nchild, Enums.Preference isPool, Enums.Preference isJacuzzi, Enums.Preference isGarden, Enums.Preference isAttractions)
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

            Configuration.GuestRequest++;


        }
        public GuestRequest()
        {

        }
        public override string ToString()
        {
            return "First Name: " + name + "Last Name: " + lastName + "Mail: " + mail + "Order status: " + Status + "Date of registration: " + registration + "Entry day: " + entryDate + "Release date: " + releaseDate + "Area: " + area + "Sub area: " + subArea + "Type of unit: " + typeOfUnit +
                "Number of adults: " + numAdult + "Number of children: " + numChildren + "Want pool: " + pool + "Want jecuzzi: " + jacuzzi + "Want garden: " + garden + "want children attractions" + childrenAttractions;
        }
        
    }
}
