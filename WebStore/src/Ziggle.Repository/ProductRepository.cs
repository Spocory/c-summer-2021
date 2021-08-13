using System.Linq;

namespace Ziggle.Repository
{
    public interface IProductRepository
    {
        ProductModel[] ForCategory(int categoryId);
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductRepository : IProductRepository
    {
        public ProductModel[] ForCategory(int categoryId)
        {
            var products = DatabaseAccessor.Instance.ProductCategory.Where(t => t.CategoryId == categoryId);

            return products
                    .Select(t => new ProductModel
                    {
                        Id = t.ProductId,
                        Name = t.Product.ProductName,
                        Price = t.Product.ProductPrice,
                        Quantity = t.Product.ProductQuantity
                    })
                    .ToArray();
        }

        public ProductModel GetProduct(int productId)
        {
            return DatabaseAccessor.Instance.Product
                        .Where(t => t.ProductId == productId)
                        .Select(t => new ProductModel
                        {
                            Id = t.ProductId,
                            Name = t.ProductName,
                            Price = t.ProductPrice,
                            Quantity = t.ProductQuantity
                        })
                        .First();
        }
    }


}