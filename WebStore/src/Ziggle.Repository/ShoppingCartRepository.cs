using System.Linq;

namespace Ziggle.Repository
{
    public interface IShoppingCartRepository
    {
        ShoppingCartModel Add(int productId, string productName, decimal productPrice, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartModel Add(int productId, string productName, decimal productPrice, int quantity)
        {
            var item = DatabaseAccessor.Instance.ShoppingCartItem.Add(
                new Ziggle.ProductDatabase.ShoppingCartItem
                {
                    ProductName = productName,
                    ProductId = productId,
                    ProductPrice = productPrice,
                    Quantity = quantity
                });

            DatabaseAccessor.Instance.SaveChanges();

            return new ShoppingCartModel
            {
                UserId = item.Entity.UserId,
                ProductId = item.Entity.ProductId,
                Quantity = item.Entity.Quantity
            };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItem
                .Where(t => t.UserId == userId)
                .Select(t => new ShoppingCartModel
                {
                    UserId = t.UserId,
                    ProductId = t.ProductId,
                    Quantity = t.Quantity
                })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int productId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItem
                                .Where(t => t.UserId == userId && t.ProductId == productId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.ShoppingCartItem.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }
}