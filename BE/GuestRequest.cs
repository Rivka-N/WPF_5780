using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        public int guestRequestKey { get; }
        string name;
        string LastName;
        string Mail;
        public Enums.OrderStatus Status { get; set; }
        DateTime Registration;
        DateTime entryDate;
        DateTime releaseDate;
        string area;
        string subArea;
        Enums.HostingUnitType TypeOfUnit;
        int numAdult;
        int numChildren;
        Enums.Preference pool;
        Enums.Preference jacuzzi;
        Enums.Preference garden;
        Enums.Preference childrenAttractions;
        //tostring
        public GuestRequest(int fName, int lName, string mail, DateTime enter, DateTime rel, string area, string sArea, Enums.HostingUnitType type, int nAdult, int Nchild, Enums.Preference isPool, Enums.Preference isJacuzzi, Enums.Preference isGarden, Enums.Preference.isAttractions)
        {
            guestRequestKey = Configuration.GuestRequest;

        }
        public override string ToString()
        {
            return "First Name: " + name + "Last Name: " + LastName + "Mail: " + Mail + "Order status: " + Status + "Date of registration: " + Registration + "Entry day: " + entryDate + "Release date: " + releaseDate + "Area: " + area + "Sub area: " + subArea + "Type of unit: " + TypeOfUnit +
                "Number of adults: " + numAdult + "Number of children: " + numChildren + "Want pool: " + pool + "Want jecuzzi: " + jacuzzi + "Want garden: " + garden + "want children attractions" + childrenAttractions;
        }
        
    }
}
