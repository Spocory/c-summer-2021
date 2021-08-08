using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        public int Sequence { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
