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
        DateTime orderDate;
        DateTime createDate;
        #endregion
        #region properties
        public Int32 HostingUnitKey { get => hostingUnitKey; set { hostingUnitKey = value; } }//from static number in configuration
        public Int32 GuestRequestKey { get => guestRequestKey; set { guestRequestKey = value; } }
        public Int32 OrderKey { get => orderKey; set { orderKey = value; } }
        public DateTime CreateDate { get => createDate; set { createDate = value; } }
        public DateTime OrderDate { get => orderDate; set { orderDate = OrderDate; } }//sent mail
        #endregion
        #region ctor
        public Order (DateTime create)
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
        #region toString

        public override string ToString()
        {
            return "Hosting Unit Key: " + hostingUnitKey + " Guest Request Key: " + guestRequestKey + " Order Key: " + orderKey
                + " Date Created: " + createDate.ToString("d") + " Order Date:" + orderDate.ToString("d");

     
        }
        #endregion

    }

}
