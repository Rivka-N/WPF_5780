using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        #region fields
        private Int32 hostingUnitKey;//from static number in configuration
        private Int32 guestRequestKey;
        private Int32 orderKey;
        //private Enums.OrderStatus orderStatus;//only place order once the status is closed
        private DateTime createDate;
        private DateTime orderDate;//sent mail
        #endregion
        #region ctor
        public Order (DateTime createDate)//erase or change function at end
        {
            this.createDate = createDate;
        }
        public Order(DateTime createDate, DateTime orderDate, GuestRequest guest, HostingUnit hosting)
        {
            this.createDate = createDate;
            this.orderDate = orderDate;
            this.guestRequestKey = guest.GuestRequestKey;
            this.hostingUnitKey = hosting.HostingUnitKey;
            this.orderKey = Configuration.Order++;

        }
#endregion
        #region functions

        public override string ToString()
        {
            return "Hosting Unit Key: " + hostingUnitKey + " Guest Request Key: " + guestRequestKey + " Order Key: " + orderKey
                + " Date Created: " + createDate + " Order Date: " + orderDate;

     
        }
    }
    #endregion

}
