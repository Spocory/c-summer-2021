using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface IShoppingCartManager
    {
        ShoppingCartModel Add(int productId, string productName, decimal productPrice, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCartModel Add(int productId, string productName, decimal productPrice, int quantity)
        {
            var item = shoppingCartRepository.Add(productId,productName,productPrice,quantity);

            return new ShoppingCartModel {
                ProductId = item.ProductId, ProductName = item.ProductName, ProductPrice = item.ProductPrice, Quantity = item.Quantity
             };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = shoppingCartRepository.GetAll(userId)
                .Select(t => {
                    var product = getProduct(t.ProductId);

                    return new ShoppingCartModel
                    {
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        CartQuantity = t.Quantity
                    };
                })
                .ToArray();

            return items;
        }

        public bool Remove(int userId, int productId)
        {
            return shoppingCartRepository.Remove(userId, productId);
        }
    }
}