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
    partial class DALXML:IDAL
    {
        #region init and save files to lists
        internal void loadOrders()//loads orders file if exist
        {
            try
            {
                if (File.Exists(orderPath))
                {
                    XmlSerializer x = new XmlSerializer(orders.GetType());
                    FileStream fs = new FileStream(orderPath, FileMode.Open);
                    orders = (List<Order>)x.Deserialize(fs);
                    fs.Close();//closes file

                }
            }
            catch
            {
                throw new loadExceptionDAL("Unable to load Orders");
            }
        }

        private void loadUnits()
        {
            try
            {
                if (File.Exists(hostingUnitPath))//file exists
                {

                    XmlSerializer x = new XmlSerializer(units.GetType());
                    FileStream fs = new FileStream(hostingUnitPath, FileMode.Open);
                    units = (System.Collections.Generic.List<HostingUnit>)x.Deserialize(fs);
                    fs.Close();//closes file

                }

            }
            catch
            {
                throw new loadExceptionDAL("Unable to load Hosting Units");

            }
        }
        private void loadGuests()//guest file load
        {
            try
            {
                if (File.Exists(guestRequestPath))
                    guestRequest = XElement.Load(guestRequestPath);//loads guests into guestsRequest
                else
                {
                    guestRequest = new XElement("Requests");//creates file
                    guestRequest.Save(guestRequestPath);
                }
            }
            catch
            {
                throw new loadExceptionDAL("Unable to load Guests");

            }
        }

        internal void loadConfig()//loads orders file
        {
            try
            {
                if (!File.Exists(configPath))//file doesn't exist. creates it with all the information it needs
                {
                    configuration = new XElement("Configurations",
                        new XElement("HostingUnit", 10000000), new XElement("Order", 10000000), new XElement("GuestRequest", 10000000),
                        new XElement("BankKey", 10000000), new XElement("LastStatusUpdate", default(DateTime).ToString()));//creates file
                    configuration.Save(configPath);
                }
                else
                {
                    configuration = XElement.Load(configPath);//loads existing configs
                }
            }
            catch
            {
                throw new loadExceptionDAL("unable to load configuration file");
            }
        }

        #endregion
        #region last Update order status time
        public void setLastUpdatedStatus()//save today's date to config file 
        {
            configuration.Element("LastStatusUpdate").Value=DateTime.Today.ToString();//sets value to be today
        }
        public DateTime getLastUpdatedStatus()
        {
            return Convert.ToDateTime(configuration.Element("LastStatusUpdate").Value);//returns value as datetime
        }
        #endregion
        #region orders
        public void deleteOrders(Func<Order, bool> p)
        {
            FileStream file = new FileStream(orderPath, FileMode.OpenOrCreate);//opens file
            
            try
            {
                orders.RemoveAll(ord => p(ord));//removes from list
                XmlSerializer xmlSer = new XmlSerializer(orders.GetType());
                xmlSer.Serialize(file, orders);
                
            }
            catch
            {
                file.Close();//closes file
                throw new loadExceptionDAL("unable to delete orders");
            
        }
    }
        public void changeOrderStatus(Func<Order, bool> p1, Enums.OrderStatus status)
        {
            var deleting = orders.Where(ord => p1(ord))
                .Select(ord => {ord.Status = status; return ord; }).ToList();
            
        }

        #region get list of orders
        public List<Order> getAllOrders()//returns all orders
        {
            try {
                if (orders.Count == 0)//no items
                    return null;
                return orders;
            }
            catch
            {
                return null;
            }

        }
        #endregion
        #region add order
        public void addOrder(Order ord)
        {
            FileStream file = new FileStream(orderPath, FileMode.OpenOrCreate);//opens file
            try
            {
                orders.Add(ord);//adds order to list
                XmlSerializer xmlSer = new XmlSerializer(orders.GetType());
                xmlSer.Serialize(file, orders);

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
        #endregion

        #endregion

        #region banks
        private List<BankAccount> banks = null;

        public List<BankAccount> getAllBranches()
        {
            if (bankDownloaded)//already downloaded
            { if (banks == null)
                {
                    banks = new List<BankAccount>();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(@"atm.xml");
                    XmlNode rootNode = doc.DocumentElement;
                    XmlNodeList children = rootNode.ChildNodes;
                    foreach (XmlNode child in children)
                    {
                        BankAccount b = GetBranchByXmlNode(child);
                        if (b != null)
                        {
                            banks.Add(b);
                        }
                    }
                }
                return banks;//returns list of banks. gets them all from file if banks is empty
            }
            else
                throw new notDownloadedException();//bank didn't download
        }

        private static BankAccount GetBranchByXmlNode(XmlNode node)
        {
            if (node.Name != "BRANCH") return null;
            BankAccount branch = new BankAccount();
            branch.BankAcountNumber = -1;

            XmlNodeList children = node.ChildNodes;

            foreach (XmlNode child in children)
            {
                switch (child.Name)
                {
                    case "Bank_Code":
                        branch.BankNumber = int.Parse(child.InnerText);
                        break;
                    case "Bank_Name":
                        branch.BankName = child.InnerText;
                        break;
                    case "Branch_Code":
                        branch.BranchNumber = int.Parse(child.InnerText);
                        break;
                    case "Branch_Address":
                        branch.BranchAddress = child.InnerText;
                        break;
                    case "City":
                        branch.BranchCity = child.InnerText;
                        break;

                }

            }

            if (branch.BranchNumber > 0)
                return branch;

            return null;

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            object ob = e.Argument;
            while (bankDownloaded == false)//continues until it downloads
            {
                try
                {
                    DownloadBank();
                    Thread.Sleep(2000);//sleeps before trying
                }
                catch
                { }
            }
            getAllBranches();//saves branches to ds
        }
        void DownloadBank()
        {
            #region downloadBank
            string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch
            {

                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;

            }
            finally
            {
                wc.Dispose();
            }
            #endregion

        }

        #endregion
    }
}
