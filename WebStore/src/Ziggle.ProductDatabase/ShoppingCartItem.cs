using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class ShoppingCartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
