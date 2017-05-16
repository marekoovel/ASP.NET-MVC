using DAL.Repositories;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Repositories
{
    public class CartRepository : EFRepository<Cart>, ICartRepository
    {
        public CartRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public Cart GetCartByAlbum(string ShoppingCartId, int albumId)
        {

            var cart = DbSet.SingleOrDefault(c => c.CartId == ShoppingCartId && c.AlbumId == albumId);
            return cart;

        }
        public Cart GetAlbumByCart(string ShoppingCartId, int cartId)
        {
            var cartItem = DbSet.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == cartId);
            return cartItem;
        }

        public int? GetCartSum(string ShoppingCartId)
        {
             var sum = (from cartItems in DbSet
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
             return sum;       

        }

        public decimal? GetCartTotal(string ShoppingCartId)
        {
            decimal? total = (from cartItems in DbSet
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();
            return total;
        }

        public List<Cart> GetCartItems(string ShoppingCartId)
        {
            return DbSet.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }
 

    }
}
