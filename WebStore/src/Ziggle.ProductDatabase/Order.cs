using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
