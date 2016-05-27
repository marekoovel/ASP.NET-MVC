using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartRepository : IEFRepository<Cart>
    {
        Cart GetCartByAlbum(string ShoppingCartId, int albumId);
        Cart GetAlbumByCart(string ShoppingCartId, int cartId);
        int? GetCartSum(string ShoppingCartId);
        decimal? GetCartTotal(string ShoppingCartId);
        List<Cart> GetCartItems(string ShoppingCartId);
    }
}
