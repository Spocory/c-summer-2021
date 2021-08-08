using System.Collections.Generic;
using System.Linq;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Models.xProduct> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Models.xProduct> Products
        {
            get
            {
                IEnumerable<Models.xProduct> items;

                var database = new HelloWorldEntities();

                items = database.Products
                .Select(t => new Models.xProduct
                {
                    ProductId = t.ProductId,
                    Description = t.Description,
                    Name = t.Name,
                    Price = t.Price, 
                    ProductCount = t.ProductCount
                })
                .ToArray();

                return items;
            }
        }
    }
}