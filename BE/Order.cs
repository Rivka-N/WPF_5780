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
        public Int32 hostingUnitKey { get => hostingUnitKey; set { hostingUnitKey = value; } }//from static number in configuration
        public Int32 guestRequestKey { get => guestRequestKey; set { guestRequestKey = value; } }
        public Int32 orderKey { get => orderKey; set { orderKey = value; } }
        //private Enums.OrderStatus orderStatus;//only place order once the status is closed
        DateTime createDate;
        public DateTime CreateDate { get => createDate; }
        DateTime orderDate;
        public DateTime OrderDate { get => orderDate; set { orderDate = OrderDate; } }//sent mail
        #endregion
        #region ctor
        public Order (DateTime create)//erase or change function at end
        {
            createDate = create;
            
        }
        public Order(DateTime create, DateTime order, GuestRequest guest, HostingUnit hosting)
        {
            createDate = create;
            orderDate = order;
            this.guestRequestKey = guest.GuestRequestKey;
            this.hostingUnitKey = hosting.HostingUnitKey;
            this.orderKey = Configuration.Order++;

        }

        public Order(DateTime create, DateTime orderDate) : this(create)
        {
            this.orderDate = orderDate;
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
