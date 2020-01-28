using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Reflection;
using System.Xml;
using System.ComponentModel;
using System.Xml.Serialization;


namespace DAL
{ 

    partial class DALXML : IDAL
    {
        #region Singleton
        private static readonly DALXML instance = new DALXML();

        //xelement
        private string localPath;//saves path
        private string guestRequestPath;
        private string hostingUnitPath;
        private string orderPath;
        private string configPath;

        //unit list
        List<HostingUnit> units = new List<HostingUnit>();
        List<Order> orders = new List<Order>();//orders
      
        private XElement hostingUnits;
        private XElement guestRequest;
        private XElement order;
        private XElement configuration;

        public static volatile bool bankDownloaded = false;//flag if bank was downloaded
        BackgroundWorker worker;

        public static DALXML Instance
        {
            get { return instance; }
        }
        #region c-tors
        private DALXML()
        {
            try//bank download
            {
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerAsync();

            }
            catch
            {

            }

            //open xml files (creates if don't exit) and load items
            try
            {
                #region xelements load
                localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//saves local path for xml files
                while (localPath.Contains("PL"))//takes off all folders but PL
                {
                    localPath = Path.GetDirectoryName(localPath);
                }

                hostingUnitPath = localPath + @"\Units.xml";
                guestRequestPath = localPath + @"\Guests.xml";
                orderPath = localPath + @"\Orders.xml";
                configPath = localPath + @"\Config.xml";
                //h.HostKey = 11111111; h.Name = "yoni"; h.LastName = "cohen"; h.Mail = new System.Net.Mail.MailAddress("rgrin4365@gmail.com"); h.CollectionClearance = true; 
               // { h1.HostKey = 11111112; h1.Name = "liel"; h1.LastName = "levi"; h1.Mail = new System.Net.Mail.MailAddress("rivka.hadara@gmail.com");  h1.CollectionClearance = true; }

               // units.Add(item: new HostingUnit() { HostingUnitKey = 10000001, HostingUnitName = "a", AreaVacation = Enums.Area.Center, Host=h, HostingUnitType = Enums.HostingUnitType.Hotel, Pool = Enums.Preference.Yes, NumAdult = 2, NumChildren = 2, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, Meal = Enums.MealType.Full, MoneyPaid = 23 });
              // units.Add(new HostingUnit() { HostingUnitKey = 10000002, HostingUnitName = "b", Host = h, AreaVacation = Enums.Area.Center, HostingUnitType = Enums.HostingUnitType.Hotel, Pool = Enums.Preference.Yes, NumAdult = 2, NumChildren = 0, Jacuzzi = Enums.Preference.Yes, Garden = Enums.Preference.No, Meal = Enums.MealType.Full, MoneyPaid = 100 });
               //units.Add(new HostingUnit() { HostingUnitKey = 10000003, HostingUnitName = "c", Host = h1, AreaVacation = Enums.Area.Center, HostingUnitType = Enums.HostingUnitType.Hotel, Pool = Enums.Preference.No, NumAdult = 2, NumChildren = 2, Jacuzzi = Enums.Preference.No, Garden = Enums.Preference.Yes, Meal = Enums.MealType.Full });

                //units.Add(DS.DataSource.hostingUnits[0]);
                loadUnits();//puts units into xelement hostingUnits
                loadGuests();//guest requests into guest requests
                loadOrders();
                loadConfig();//creates if doesn't exist
                #endregion
            }
            catch (Exception ex)
            {
                throw new loadExceptionDAL(ex.Message);
            }

        }
        static DALXML() { }
        #endregion
        #endregion

       
        #region hostingUnits
        public List<HostingUnit> getAllHostingUnits()//xelement to hosting unit
                                                     //need to add convert dia
        {
            
            return units;
          
        }
        public HostingUnit findUnit(int unitKey)//returns unit with this unit key
        {
            try
            {
                HostingUnit h = units.Find(ho => { return ho.HostingUnitKey == unitKey; });
                return h;
               
            }
            catch
            {
                return null;//unable to find unit 
            }
        }
        public void deleteUnit(HostingUnit toDelete)//deletes this unit
        {
            FileStream file = new FileStream(hostingUnitPath, FileMode.OpenOrCreate);//opens file

            try
            {
                units.Remove(toDelete);//removes from list
                XmlSerializer xmlSer = new XmlSerializer(units.GetType());
                xmlSer.Serialize(file, units);

            }
            catch
            {
                file.Close();//closes file
                throw new loadExceptionDAL("unable to delete unit");

            }


        }
        public void changeUnit(HostingUnit hostingUnit1)//update unit
        {

            FileStream file = new FileStream(hostingUnitPath, FileMode.OpenOrCreate);//opens file

            try
            {
                HostingUnit h = units.Find(hos => { return hos.HostingUnitKey == hostingUnit1.HostingUnitKey; });
                units.Remove(h);
                units.Add(hostingUnit1);
                XmlSerializer xmlSer = new XmlSerializer(units.GetType());
                xmlSer.Serialize(file, units);

            }
            catch
            {
                file.Close();//closes file
                throw new loadExceptionDAL("unable to delete unit");

            }
            
        }

        public int getHostConfi()
        {
            Int32 stati = Convert.ToInt32(configuration.Element("HostingUnit").Value) + 1;
            configuration.Element("HostingUnit").Value = stati.ToString();
            configuration.Save(configPath);

            return stati;

        }


        public void addHostingUnit(HostingUnit hosting)
        {
            FileStream file = new FileStream(hostingUnitPath, FileMode.OpenOrCreate);//opens file
            try
            {
                Int32 x = getHostConfi();
                hosting.HostingUnitKey = x;
                units.Add(hosting);//adds order to list
                XmlSerializer xmlSer = new XmlSerializer(units.GetType());
                xmlSer.Serialize(file, units);

            }
            catch
            {
                throw new loadExceptionDAL("unable to add order to xml file");
            }
            finally
            {
                file.Close();//closes file
            }

        }
        #endregion      //finish and need to checks if work

        #region add charge to unit
        public void addCharge(HostingUnit unit, int numDays)
        {
            var change=units.Find(u => u.HostingUnitKey == unit.HostingUnitKey);
            if (change == null)//is null
                throw new NullReferenceException();//can't proceed
            change.MoneyPaid += numDays * Configuration.TransactionFee;//changes unit
            FileStream file = new FileStream(hostingUnitPath, FileMode.OpenOrCreate);//opens file
            try
            {
                
                XmlSerializer xmlSer = new XmlSerializer(units.GetType());
                xmlSer.Serialize(file, units);//resaves it
            }
            catch
            {
                throw;
            }
            finally
            {
                file.Close();
            }

        }
        #endregion


        #region guestRequests
        #region add guest
        public int getConfi()
        {
            Int32 stati = Convert.ToInt32(configuration.Element("GuestRequest").Value)+1;
            configuration.Element("GuestRequest").Value = stati.ToString();
            configuration.Save(configPath);
            
            return stati;

        }

        public void addGuest(GuestRequest guest)
        {

            try
            {
                Int32 x = getConfi();
                XElement guestKey = new XElement("GuestRequestKey", x.ToString());

                XElement guestName = new XElement("Name", guest.Name);
                XElement guestLastName = new XElement("LastName", guest.LastName);
                XElement mail = new XElement("MailSerializable", guest.Mail.Address.ToString());
   
                XElement jacuzzi = new XElement("Jacuzzi", guest.Jacuzzi.ToString());
                XElement pool = new XElement("Pool", guest.Pool.ToString());
                XElement garden = new XElement("Garden", guest.Garden.ToString());
                XElement mael = new XElement("Meal", guest.Meal.ToString());
                XElement numAdults = new XElement("NumAdult", guest.NumAdult.ToString());
                XElement numChildren = new XElement("NumChildren", guest.NumChildren.ToString());
                XElement status = new XElement("Status", guest.Status.ToString());
                XElement area = new XElement("AreaVacation", guest.AreaVacation.ToString());
                XElement type = new XElement("TypeOfUnit", guest.TypeOfUnit.ToString());
                XElement entryDate = new XElement("EntryDate", guest.EntryDate.ToString());
                XElement releaseDate = new XElement("ReleaseDate", guest.ReleaseDate.ToString());
                XElement registrationDate = new XElement("Registration", guest.Registration.ToString());

                guestRequest.Add(new XElement("guest", guestKey, guestName, guestLastName, mail, status, registrationDate, entryDate, releaseDate, area, type, numAdults, numChildren, pool, jacuzzi, garden,  mael));
                guestRequest.Save(guestRequestPath);
            }
            catch
            {
                throw new loadExceptionDAL("unable to save new guest to xml file");
            }

        }
        #endregion
        #region get list

        GuestRequest ConvertGuest(XElement element)
        {
            GuestRequest g = new GuestRequest();

            foreach (PropertyInfo item in typeof(GuestRequest).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                if (item.Name != "Mail")
                {
                    object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                    item.SetValue(g, convertValue);

                }
            }

            return g;
        }

        public List<GuestRequest> getRequests()
        {
            loadGuests();
            List<GuestRequest> guest = new List<GuestRequest>();
            foreach (XElement o in guestRequest.Elements())
            {
                GuestRequest t = ConvertGuest(o);

                guest.Add(t);
            }
            return guest;

        }





        //public List<GuestRequest> getRequests()
        //{

        //    List<GuestRequest> guest;

        //    try
        //    {

         //  guest = (from p in guestRequest.Elements()//get all guestRequest
        //                 select new GuestRequest()
        //                 {
        //                     Name = p.Element("name").Value,
        //                     LastName = p.Element("lastName").Value,
        //                     GuestRequestKey = Convert.ToInt32(p.Element("guestkey").Value),
        //                     TypeOfUnit = (Enums.HostingUnitType)(Enum.Parse(typeof(Enums.HostingUnitType), p.Element("typeofunit").Value)),
        //                     AreaVacation = (Enums.Area)(Enum.Parse(typeof(Enums.Area), p.Element("areavacation").Value)),
        //                     NumAdult = Convert.ToInt32(p.Element("numAdults").Value),
        //                     NumChildren = Convert.ToInt32(p.Element("children").Value),
        //                     Pool = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("pool").Value)),
        //                     Garden = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("garden").Value)),
        //                     Jacuzzi = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("jacuzzi").Value)),
        //                     Meal = (Enums.MealType)(Enum.Parse(typeof(Enums.MealType), p.Element("mael").Value)),
        //                     EntryDate = Convert.ToDateTime(p.Element("entrydate").Value),
        //                     ReleaseDate = Convert.ToDateTime(p.Element("releasedate").Value),
        //                     Registration = Convert.ToDateTime(p.Element("registrationdate").Value),
        //                     Mail = new System.Net.Mail.MailAddress(p.Element("Email").Value, p.Element("name").Value + p.Element("guest last name").Value),
        //                     Status = (Enums.OrderStatus)(Enum.Parse(typeof(Enums.OrderStatus), p.Element("status").Value))

        //                 }).ToList();
        //    }
        //    catch
        //    {
        //        throw new NullReferenceException();//there were no guests
        //    }

        //    return guest;
        //}
        #endregion
        #region change status
        public void changeStatus(GuestRequest guest, Enums.OrderStatus status)//change status
        {

            XElement guestElement = (from p in guestRequest.Elements()
                                     where Convert.ToInt32(p.Element("guest key").Value) == guest.GuestRequestKey
                                     select p).FirstOrDefault();
            guestElement.Element("status").Value = status.ToString();//sets status
            try
            {
                guestRequest.Save(guestRequestPath);

            }
            catch
            {
                throw new loadExceptionDAL("unable to save elements after deleting");//error in loading or saving the file

            }


        }
        #endregion

        #region find guest
        public GuestRequest findGuest(int key)//find guest by key
        {
            GuestRequest guest;
            try
            {
                guest = (from p in guestRequest.Elements()
                         where Convert.ToInt32(p.Element("guestkey").Value) == key
                         select new GuestRequest()
                         {
                             Name = p.Element("name").Value,
                             LastName = p.Element("name").Element("guest last name").Value,
                             GuestRequestKey = Convert.ToInt32(p.Element("guestkey").Value),
                             TypeOfUnit = (Enums.HostingUnitType)(Enum.Parse(typeof(Enums.HostingUnitType), p.Element("typeofunit").Value)),
                             AreaVacation = (Enums.Area)(Enum.Parse(typeof(Enums.Area), p.Element("areavacation").Value)),
                             NumAdult = Convert.ToInt32(p.Element("numAdults").Value),
                             NumChildren = Convert.ToInt32(p.Element("children").Value),
                             Pool = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("pool").Value)),
                             Garden = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("garden").Value)),
                             Jacuzzi = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), p.Element("jacuzzi").Value)),
                             Meal = (Enums.MealType)(Enum.Parse(typeof(Enums.MealType), p.Element("mael").Value)),
                             EntryDate = Convert.ToDateTime(p.Element("entrydate").Value),
                             ReleaseDate = Convert.ToDateTime(p.Element("releasedate").Value),
                             Registration = Convert.ToDateTime(p.Element("registrationdate").Value),
                             Mail = new System.Net.Mail.MailAddress(p.Element("Email").Value, p.Element("name").Value + p.Element("lastname").Value),
                             Status = (Enums.OrderStatus)(Enum.Parse(typeof(Enums.OrderStatus), p.Element("status").Value))
                         }).FirstOrDefault();
            }
            catch
            {
                guest = null;
            }
            return guest;
        }


        public GuestRequest findGuest(GuestRequest g1)//find guest
        {
            return findGuest(g1.GuestRequestKey);
        }

        #endregion
        #endregion     //finish and work

    
    }
}


