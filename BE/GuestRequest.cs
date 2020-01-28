using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class GuestRequest
    {
        #region fields
        #region email
        [XmlIgnore]
        public System.Net.Mail.MailAddress Mail { get => new System.Net.Mail.MailAddress(mail); set { mail = value.Address; } }
        private string mail;

        public string MailSerializable { get => mail; set { mail = value; } }
        #endregion
        public int GuestRequestKey { get; set; }
        string name;
        public string Name { get => name; set { name = value; } }
        string lastName;
        public string LastName { get => lastName; set { lastName = value; } }

      
        public Enums.OrderStatus Status { get; set; }
        DateTime registration;
        public DateTime Registration { get => registration; set { registration = value; } }
        DateTime entryDate;
        public DateTime EntryDate { get => entryDate; set { entryDate = value; } }
        DateTime releaseDate;
        public DateTime ReleaseDate { get => releaseDate; set { releaseDate = value; } }
        Enums.Area area;
        public Enums.Area AreaVacation { get => area; set { area = value; } }
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
        public Enums.MealType Meal { get; set; }

        #endregion

       
        #region ctors

        public GuestRequest(int guestKey=0, DateTime registerDate=default(DateTime))
        {
            name = null;
            lastName = null;
            mail = null;
            registration = registerDate == default(DateTime) ? new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day) : registerDate;
            entryDate = new DateTime();
            releaseDate = new DateTime();
            area = Enums.Area.Center;
            typeOfUnit = Enums.HostingUnitType.Hotel;
            numAdult = 0;
            numChildren = 0;
            pool = Enums.Preference.Maybe;
            jacuzzi = Enums.Preference.Maybe;
            garden = Enums.Preference.Maybe;
            Status = Enums.OrderStatus.Started;
        }
        #endregion
        public override string ToString()
        {
            return "Name: " + name + " " + lastName + " \nMail: " + mail + "\nDays: " + entryDate.ToString("d") + " to " + releaseDate + "\nArea: " + area + " Type of unit: " + typeOfUnit +
                "\nNumber of adults: " + numAdult + " Number of children: " + numChildren + "\n";
        }
        
    }
}
