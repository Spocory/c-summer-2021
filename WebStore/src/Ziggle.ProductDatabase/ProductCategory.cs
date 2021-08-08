using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
