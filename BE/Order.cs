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
        Enums.OrderStatus status;
        #endregion
        #region properties
        public Int32 HostingUnitKey { get => hostingUnitKey; set { hostingUnitKey = value; } }//from static number in configuration
        public Int32 GuestRequestKey { get => guestRequestKey; set { guestRequestKey = value; } }
        public Int32 OrderKey { get => orderKey; set { orderKey = value; } }
        public DateTime CreateDate { get => createDate; set { createDate = value; } }
        public DateTime OrderDate { get => orderDate; set { orderDate = OrderDate; } }//sent mail
        public Enums.OrderStatus Status { get => status; set { status = value; } }

        //added
        public string HostName { get; set; }
        public string GuestName { get; set; }//add application of these to functions and creating hosts and guests

        #endregion
        #region ctor


        public Order (DateTime create = default(DateTime))
        {
            createDate = create == default(DateTime) ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) : createDate;
           
        }
    

        public Order(DateTime create, DateTime orderDate) : this(create)
        {
            this.orderDate = orderDate;

        }
        public Order() { }//default c-tor


        #endregion
        #region toString

        public override string ToString()
        {
            return "Hosting Unit Key: " + hostingUnitKey + " \nGuest Request Key: " + guestRequestKey + " \nOrder Key: " + orderKey
                + " \nDate Request Created: " + createDate.ToString("d") + " Order Date:" + orderDate.ToString("d")+"\n";

     
        }
        #endregion

    }

}
