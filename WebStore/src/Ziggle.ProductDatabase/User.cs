using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserHashedPassword { get; set; }
        public bool UserFailedPasswordAttempts { get; set; }
        public bool UserLockedOut { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
