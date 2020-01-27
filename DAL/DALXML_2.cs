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
         internal void loadOrders()//loads orders file
        {

        }

        #region save files in lists
        private void loadUnits()
        {


            try
            {
                if (File.Exists(hostingUnitPath))//file exists
                {
                    
                    XmlSerializer x = new XmlSerializer(units.GetType()/*, new XmlRootAttribute("Units")*/);
                    FileStream fs = new FileStream(hostingUnitPath, FileMode.Open);
                    units = (List<HostingUnit>)x.Deserialize(fs);
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
            if (File.Exists(guestRequestPath))
                guestRequest = XElement.Load(guestRequestPath);//loads guests into guestsRequest
            else
            {
                guestRequest = new XElement("Requests");//creates file
                guestRequest.Save(guestRequestPath);
            }

        }
        #endregion

    }
}
