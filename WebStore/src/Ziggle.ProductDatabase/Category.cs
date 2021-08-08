using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
