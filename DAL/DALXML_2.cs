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
        internal void loadOrders()//loads orders file
        {
            try
            {
                if (File.Exists(orderPath))
                    order= XElement.Load(orderPath);//loads orders
                else
                {
                    order= new XElement("Orders");//creates file
                    order.Save(orderPath);
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
                    
                    XmlSerializer x = new XmlSerializer(units.GetType()/*, new XmlRootAttribute("Units")*/);
                    FileStream fs = new FileStream(hostingUnitPath, FileMode.Open);
                    units = (System.Collections.Generic.List<HostingUnit>)x.Deserialize(fs);
                    fs.Close();//closes file

                }
                //hostingUnits = XElement.Load(guestRequestPath);//loads units into hostingUnits
                else//creates it
                {
                    new FileStream(hostingUnitPath, FileMode.Create);//creates file
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
                    order.Save(orderPath);
                }
                else
                {
                    order = XElement.Load(orderPath);//loads existing configs
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
            //order.(ord => p(ord));
        }
        public void changeOrderStatus(Func<Order, bool> p1, Enums.OrderStatus status)
        {

        }

        #region orders
        #region get list of orders
        public List<Order> getAllOrders()//returns all orders
        {


            List<Order> orders;
            try
            {

                orders = (from p in order.Elements()//get all guestRequest
                          select new Order()
                          {
                              HostingUnitKey = Convert.ToInt32(p.Element("hostingKey").Value),
                              HostName = p.Element("hostName").Value,
                              GuestRequestKey = Convert.ToInt32(p.Element("guestKey").Value),
                              GuestName = p.Element("guestName").Value,
                              OrderKey = Convert.ToInt32(p.Element("orderKey").Value),
                              OrderDate = Convert.ToDateTime(p.Element("orderDate").Value),
                              Status = (Enums.OrderStatus)(Enum.Parse(typeof(Enums.OrderStatus), p.Element("status").Value)),
                              CreateDate = Convert.ToDateTime(p.Element("createDate").Value)


                          }).ToList();
            }
            catch
            {
                orders = null;
            }

            return orders;
        }
        #endregion
        #region add order
        public void addOrder(Order ord)
        {
            try
            {

                XElement guestName = new XElement("guestName", ord.GuestName);
                XElement hostName = new XElement("hostName", ord.HostName);
                XElement guestKey = new XElement("guestKey", ord.GuestRequestKey);
                XElement hostingKey = new XElement("hostingKey", ord.HostingUnitKey);
                XElement orderKey = new XElement("orderKey", ord.OrderKey);
                XElement orderDate = new XElement("orderDate", ord.OrderDate);
                XElement status = new XElement("status", ord.Status);
                XElement createDate = new XElement("createDate", ord.CreateDate);

                order.Add(new XElement("guest", guestName, hostName, guestKey, hostingKey, orderKey, orderDate, status, createDate));
                order.Save(orderPath);
            }
            catch
            {
                throw new loadExceptionDAL("unable to add order to xml file");
            }
        }
        #endregion

        #endregion
        #endregion
    }
}
