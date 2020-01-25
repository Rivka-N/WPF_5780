using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using BE;


namespace DAL
{
    class DALXML //: IDAL
    {
        #region Singleton
        private static readonly DALXML instance = new DALXML();

        //xelement
        private string hostingUnitPath;//where it's saved
        private XElement hostingUnits;

        public static DALXML Instance
        {
            get { return instance; }
        }
        #region c-tors
        private DALXML()
        {
            try
            {
                DownloadBank();//start bank download. in new thread?
            }
            catch
            {
                //throw? print error? retry?
            }

            //open xml files (creates if don't exit) and load items
            try
            {
                #region xelements load
                hostingUnitPath = @"\hostingUnits.xml";//saves hostingUnit path
                hostingUnits = XElement.Load(hostingUnitPath);//loads units into hostingUnits
                #endregion
            }
            catch
            {
                throw new loadExceptionDAL();
            }

           
        }
        static DALXML() { }
        #endregion
        #endregion

        #region banks
        //save banks
        public static volatile bool bankDownloaded = false;//flag if bank was downloaded
        void DownloadBank()
        {
            #region downloadBank
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch (Exception ex)
            {
                try
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    bankDownloaded = true;
                }
                catch (Exception exeption)
                {
                    //tries again if the connection didn't allow to download it
                }
            }
            finally
            {
                wc.Dispose();
            }
            #endregion

        }
        //List<BankAccount> GetBankAccounts()
        //{

        //}
        #endregion

        #region hostingUnits
        public List<HostingUnit> getAllHostingUnits()//xelement to hosting unit
                                                     //need to add convert diary

        {
            //converts xelement with units to list and returns it
            return (from host in hostingUnits.Elements()
             select new HostingUnit()//saves to new hosting unit
             {
                 HostingUnitKey = Convert.ToInt32(host.Element("Unit Key").Value),
                 HostingUnitName = Convert.ToString(host.Element("Unit Name").Value),
                 HostingUnitType = (Enums.HostingUnitType)(Enum.Parse(typeof(Enums.HostingUnitType), host.Element("Unit Type").Value)),
                 AreaVacation = (Enums.Area)(Enum.Parse(typeof(Enums.Area), host.Element("Unit Type").Value)),
                 NumAdult = Convert.ToInt32(host.Element("Adults").Value),
                 NumChildren = Convert.ToInt32(host.Element("Children").Value),
                 Pool = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Pool").Value)),
                 Garden = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Garden").Value)),
                 Jacuzzi = (Enums.Preference)(Enum.Parse(typeof(Enums.Preference), host.Element("Jacuzzi").Value)),
                 Meal = (Enums.MealType)(Enum.Parse(typeof(Enums.MealType), host.Element("Meal").Value)),
                 MoneyPaid=Convert.ToInt32(host.Element("Paid").Value),
                 //host
                 Host =new Host()
                 {
                     HostKey=Convert.ToInt32(host.Element("Host Key").Value),
                     Name=host.Element("Host Name").Value,
                     LastName=host.Element("Host Last").Value,
                     Mail=new System.Net.Mail.MailAddress(host.Element("Email").Value, host.Element("Host Name").Value + host.Element("Host Last").Value),
                     CollectionClearance=Convert.ToBoolean(host.Element("Clearance").Value),
                     Bank=new BankAccount()//bank
                     {
                         BankAcountNumber=Convert.ToInt32(host.Element("Account Number").Value),
                         BankName=host.Element("Bank Name").Value,
                         BankNumber=Convert.ToInt32(host.Element("Bank Number").Value),
                         BranchNumber=Convert.ToInt32(host.Element("Branch Number").Value),
                         BranchAddress= host.Element("Branch Address").Value
                         //add
                     }
                 }
             }).ToList();
                //converts hostingunits to list
        }
        #endregion

    }
}
