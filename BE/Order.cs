using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        private Enums.OrderStatus orderStatus;
        private DateTime createDate;

        public Order(Enums.OrderStatus orderStatus, DateTime createDate)
        {
            this.orderStatus = orderStatus;
            this.createDate = createDate;
        }
    }
}
