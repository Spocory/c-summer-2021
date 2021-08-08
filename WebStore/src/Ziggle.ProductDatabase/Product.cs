using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
            ProductCategory = new HashSet<ProductCategory>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
