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
        private int hostingUnitKey;//from static number in configuration
        private int guestRequestKey;
        private int orderKey;
        //private Enums.OrderStatus orderStatus;//only place order once the status is closed
        private DateTime createDate;
        public DateTime OrderDate { get; set; }//sent mail
        #endregion
        #region functions
        public Order(DateTime createDate, GuestRequest guest, HostingUnit hosting)
        {
            this.createDate = createDate;
            this.orderKey = Configuration.Order;
            this.guestRequestKey = guest.guestRequestKey;
            this.hostingUnitKey = hosting.HostingUnitKey;
        }
        public override string ToString()
        {
            return "Hosting Unit Key: " + hostingUnitKey + " Guest Request Key: " + guestRequestKey + " Order Key: " + orderKey
                + " Order Status " + orderStatus + " Date Created: " + createDate + " Order Date: " + OrderDate;

     
        }
    }
    #endregion

}
