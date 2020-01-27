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

        //#region banks
        ////save banks
        //public List<BankAccount> getAllBranches()
        //{


        //    List<BankAccount> banks = new List<BankAccount>();
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(@"atm.xml");
        //    XmlNode rootNode = doc.DocumentElement;
        //    //DisplayNodes(rootNode);

        //    XmlNodeList children = rootNode.ChildNodes;
        //    foreach (XmlNode child in children)
        //    {
        //        BankAccount b = GetBranchByXmlNode(child);
        //        if (b != null)
        //        {
        //            banks.Add(b);
        //        }
        //    }

        //    return banks;
        //}


        //private static BankAccount GetBranchByXmlNode(XmlNode node)
        //{
        //    if (node.Name != "BRANCH") return null;
        //    BankAccount branch = new BankAccount();
        //    branch.BankAcountNumber = -1;

        //    XmlNodeList children = node.ChildNodes;

        //    foreach (XmlNode child in children)
        //    {
        //        switch (child.Name)
        //        {
        //            case "Bank_Code":
        //                branch.BankNumber = int.Parse(child.InnerText);
        //                break;
        //            case "Bank_Name":
        //                branch.BankName = child.InnerText;
        //                break;
        //            case "Branch_Code":
        //                branch.BranchNumber = int.Parse(child.InnerText);
        //                break;
        //            case "Branch_Address":
        //                branch.BranchAddress = child.InnerText;
        //                break;
        //            case "City":
        //                branch.BranchCity = child.InnerText;
        //                break;

        //        }

        //    }

        //    if (branch.BranchNumber > 0)
        //        return branch;

        //    return null;

        //}

        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{

        //    object ob = e.Argument;
        //    while (bankDownloaded == false)//continues until it downloads
        //    {
        //        try
        //        {
        //            DownloadBank();
        //            Thread.Sleep(2000);//sleeps before trying
        //        }
        //        catch
        //        { }
        //    }

        //    getAllBranches();//saves branches to ds
        //}
        //void DownloadBank()
        //{
        //    #region downloadBank
        //    string xmlLocalPath = @"atm.xml";
        //    WebClient wc = new WebClient();
        //    try
        //    {
        //        string xmlServerPath =
        //       @"https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
        //        wc.DownloadFile(xmlServerPath, xmlLocalPath);
        //        bankDownloaded = true;
        //    }
        //    catch
        //    {

        //        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
        //        wc.DownloadFile(xmlServerPath, xmlLocalPath);
        //        bankDownloaded = true;

        //    }
        //    finally
        //    {
        //        wc.Dispose();
        //    }
        //    #endregion

        //}

        //#endregion

        #region hostingUnits
        public List<HostingUnit> getAllHostingUnits()//xelement to hosting unit
                                                     //need to add convert dia
        {

            return units;
            //converts xelement with units to list and returns it
            //return (from host in hostingUnits.Elements()
            // select new HostingUnit()//saves to new hosting unit
            // {
            //     HostingUnitKey = Convert.ToInt32(host.Element("Unit_Key").Value),
            //     HostingUnitName = Convert.ToString(host.Element("Unit_Name").Value),
            //     HostingUnitType = (Enums.HostingUnitType)(Enum.Parse(typeof(Enums.HostingUnitType), host.Element("Unit_Type").Value)),
            //     AreaVacation = (Enums.Area)(Enum.Parse(typeof(Enums.Area), host.Element("Unit_Type").Value)),
            //     NumAdult = Convert.ToInt32(host.Element("Adults").Value),
            //     NumChildren = Convert.ToInt32(host.Element("Children").Value),
            //     Pool = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Pool").Value)),
            //     Garden = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Garden").Value)),
            //     Jacuzzi = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Jacuzzi").Value)),
            //     Meal = (Enums.MealType)(Enum.Parse(typeof(Enums.MealType), host.Element("Meal").Value)),
            //     MoneyPaid=Convert.ToInt32(host.Element("Paid").Value),
            //     #region diary

            //#endregion
            //      //host
            //        Host = new Host()
            //     {
            //         HostKey=Convert.ToInt32(host.Element("Host").Element("Host_Key").Value),
            //         Name=host.Element("Host").Element("Host_Name").Value,
            //         LastName=host.Element("Host").Element("Host_Last").Value,
            //         Mail=new System.Net.Mail.MailAddress(host.Element("Host").Element("Email").Value, host.Element("Host_Name").Value + host.Element("Host Last").Value),
            //         CollectionClearance=Convert.ToBoolean(host.Element("Host").Element("Clearance").Value),
            //         Bank = new BankAccount()//bank
            //         {
            //             BankAcountNumber = Convert.ToInt32(host.Element("Host").Element("Bank").Element("Account Number").Value),
            //             BankName = host.Element("Host").Element("Bank").Element("Bank Name").Value,
            //             BankNumber = Convert.ToInt32(host.Element("Host").Element("Bank").Element("Bank Number").Value),
            //             BranchNumber = Convert.ToInt32(host.Element("Host").Element("Bank").Element("Branch Number").Value),
            //             BranchAddress = host.Element("Host").Element("Bank").Element("Branch Address").Value
            //             //add
            //         }
            //     }
            // }).ToList();
            //converts hostingunits to list
        }
        public HostingUnit findUnit(int unitKey)//returns unit with this unit key
        {
            try
            {
                return (from unit in hostingUnits.Elements()
                        where Convert.ToInt32(unit.Element("Unit_Key").Value) == unitKey//this unit
                        select new HostingUnit()//saves to new hosting unit
                        {
                            HostingUnitKey = Convert.ToInt32(unit.Element("Unit_Key").Value),
                            HostingUnitName = Convert.ToString(unit.Element("Unit_Name").Value),
                            HostingUnitType = (Enums.HostingUnitType)(Enum.Parse(typeof(Enums.HostingUnitType), unit.Element("Unit Type").Value)),
                            AreaVacation = (Enums.Area)(Enum.Parse(typeof(Enums.Area), unit.Element("Unit_Type").Value)),
                            NumAdult = Convert.ToInt32(unit.Element("Adults").Value),
                            NumChildren = Convert.ToInt32(unit.Element("Children").Value),
                            Pool = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), unit.Element("Pool").Value)),
                            Garden = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), unit.Element("Garden").Value)),
                            Jacuzzi = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), unit.Element("Jacuzzi").Value)),
                            Meal = (Enums.MealType)(Enum.Parse(typeof(Enums.MealType), unit.Element("Meal").Value)),
                            MoneyPaid = Convert.ToInt32(unit.Element("Paid").Value),
                            //host
                            Host = new Host()
                            {
                                HostKey = Convert.ToInt32(unit.Element("Host").Element("Key").Value),
                                Name = unit.Element("Host").Element("First_Name").Value,
                                LastName = unit.Element("Host").Element("Last_Name").Value,
                                Mail = new System.Net.Mail.MailAddress(unit.Element("Host").Element("Email").Value, unit.Element("Host_Name").Value + unit.Element("Host Last").Value),
                                CollectionClearance = Convert.ToBoolean(unit.Element("Host").Element("Clearance").Value),
                                Bank = new BankAccount()//bank
                                {
                                    BankAcountNumber = Convert.ToInt32(unit.Element("Host").Element("Bank").Element("Account_Number").Value),
                                    BankName = unit.Element("Host").Element("Bank").Element("Bank_Name").Value,
                                    BankNumber = Convert.ToInt32(unit.Element("Host").Element("Bank").Element("Bank_Number").Value),
                                    BranchNumber = Convert.ToInt32(unit.Element("Host").Element("Bank").Element("Branch_Number").Value),
                                    BranchAddress = unit.Element("Host").Element("Bank").Element("Branch_Address").Value
                                    //add branch city?
                                }
                            }
                        }).First();//returns first matching unit found
            }
            catch
            {
                return null;//unable to find unit 
            }
        }
        public void deleteUnit(HostingUnit toDelete)//deletes this unit
        {
            try
            {
                (from unit in hostingUnits.Elements()
                 where Convert.ToInt32(unit.Element("Unit Key").Value) == toDelete.HostingUnitKey//this unit
                 select unit).First().Remove();//removes first found
            }
            catch
            {
                throw new objectErrorDAL();//didn't find item
            }
            try
            {
                hostingUnits.Save(hostingUnitPath);
            }
            catch
            {
                throw new loadExceptionDAL("unable to save elements after deleting");//error in loading or saving the file
            }


        }

        public void changeUnit(HostingUnit hostingUnit1)//update unit
        {
            try
            {
                XElement host = (from unit in hostingUnits.Elements()
                                 where Convert.ToInt32(unit.Element("Unit Key").Value) == hostingUnit1.HostingUnitKey//this unit
                                 select unit).First();//first found
                //updates 

                host.Element("Unit Key").Value = hostingUnit1.HostingUnitKey.ToString();
                host.Element("Unit Name").Value = hostingUnit1.HostingUnitName;
                host.Element("Unit Type").Value = hostingUnit1.HostingUnitType.ToString();
                host.Element("Unit Area").Value = hostingUnit1.AreaVacation.ToString();
                host.Element("Adults").Value = hostingUnit1.NumAdult.ToString();
                host.Element("Children").Value = hostingUnit1.NumChildren.ToString();
                host.Element("Pool").Value = hostingUnit1.Pool.ToString();
                host.Element("Garden").Value = hostingUnit1.Garden.ToString();
                host.Element("Jacuzzi").Value = hostingUnit1.Jacuzzi.ToString();
                host.Element("Meal").Value = hostingUnit1.Meal.ToString();
                host.Element("Paid").Value = hostingUnit1.MoneyPaid.ToString();
                //host
                host.Element("Host").Element("Host Key").Value = hostingUnit1.Host.HostKey.ToString();
                host.Element("Host").Element("Host Name").Value = hostingUnit1.Host.Name;
                host.Element("Host").Element("Host Last").Value = hostingUnit1.Host.LastName;
                host.Element("Host").Element("Email").Value = hostingUnit1.Host.Mail.Address;
                host.Element("Host").Element("Clearance").Value = hostingUnit1.Host.CollectionClearance.ToString();
                host.Element("Host").Element("Bank").Element("Account Number").Value = hostingUnit1.Host.Bank.BankAcountNumber.ToString();
                host.Element("Host").Element("Bank").Element("Bank Name").Value = hostingUnit1.Host.Bank.BankName;
                host.Element("Host").Element("Bank").Element("Bank Number").Value = hostingUnit1.Host.Bank.BankNumber.ToString();
                host.Element("Host").Element("Bank").Element("Branch Number").Value = hostingUnit1.Host.Bank.BranchNumber.ToString();
                host.Element("Host").Element("Bank").Element("Branch Address").Value = hostingUnit1.Host.Bank.BranchAddress;

            }

            catch
            {
                throw new objectErrorDAL();//didn't find item
            }
            try
            {
                hostingUnits.Save(hostingUnitPath);//saves
            }
            catch
            {
                throw new loadExceptionDAL("unable to save elements after deleting");//error in loading or saving the file
            }
        }

   

        public void addHostingUnit(HostingUnit hosting)
        {
            FileStream file = new FileStream(hostingUnitPath, FileMode.OpenOrCreate);//opens file
            try
            {
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

        #region add charge to unit
        public void addCharge(HostingUnit unit, int numDays)
        {
            //find hosting unit, update field and save again
            //var found = DataSource.hostingUnits.Find(u => u.HostingUnitKey == unit.HostingUnitKey);
            //found.MoneyPaid += Configuration.TransactionFee * numDays;//adds this transaction fee to total transaction fees

        }
        #endregion
        #region guestRequests
        #region add guest
        public void addGuest(GuestRequest guest)
        {
            try
            {

                XElement guestName = new XElement("name", guest.Name);
                XElement guestLastName = new XElement("lastName", guest.LastName);
                XElement guestKey = new XElement("guestkey", guest.GuestRequestKey);
                XElement jacuzzi = new XElement("jacuzzi", guest.Jacuzzi);
                XElement pool = new XElement("pool", guest.Pool);
                XElement garden = new XElement("garden", guest.Garden);
                XElement mail = new XElement("mail", guest.Mail);
                XElement mael = new XElement("mael", guest.Meal);
                XElement numAdults = new XElement("numAdults", guest.NumAdult);
                XElement numChildren = new XElement("children", guest.NumChildren);
                XElement status = new XElement("status", guest.Status);
                XElement area = new XElement("areavacation", guest.AreaVacation);
                XElement type = new XElement("typeofunit", guest.TypeOfUnit);
                XElement entryDate = new XElement("entrydate", guest.EntryDate);
                XElement releaseDate = new XElement("releasedate", guest.ReleaseDate);
                XElement registrationDate = new XElement("registrationDate", guest.Registration);

                guestRequest.Add(new XElement("guest", guestLastName, guestName, guestKey, jacuzzi, pool, garden, mail, mael, numAdults, numChildren, status, area, type, entryDate, releaseDate, registrationDate));
                guestRequest.Save(guestRequestPath);
            }
            catch
            {
                throw new loadExceptionDAL("unable to save new guest to xml file");
            }

        }
        #endregion
        #region get list

        public List<GuestRequest> getRequests()
        {

            List<GuestRequest> guest;
            try
            {

                guest = (from p in guestRequest.Elements()//get all guestRequest
                         select new GuestRequest()
                         {
                             Name = p.Element("name").Value,
                             LastName = p.Element("lastName").Value,
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
                             Mail = new System.Net.Mail.MailAddress(p.Element("Email").Value, p.Element("name").Value + p.Element("guest last name").Value),
                             Status = (Enums.OrderStatus)(Enum.Parse(typeof(Enums.OrderStatus), p.Element("status").Value))

                         }).ToList();
            }
            catch
            {
                guest = null;
            }

            return guest;
        }
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
        #endregion

        #endregion
    }
}


