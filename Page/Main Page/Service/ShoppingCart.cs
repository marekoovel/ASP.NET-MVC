using DAL;
using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_Page.Service
{
    public class ShoppingCart
    {
        #region Properties

        private readonly IUOW _uow;
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        #endregion

        public ShoppingCart(IUOW uow)
        {
            _uow = uow;
        }

        public static ShoppingCart GetCart(HttpContextBase context, IUOW uow)
        {
            var cart = new ShoppingCart(uow);
            cart.ShoppingCartId = cart.GetCartId(context, uow);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        //public static ShoppingCart GetCart(Controller controller)
        //{
        //    return GetCart(controller.HttpContext, );
        //}

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context, IUOW uow)
        {
            if (context.Session[CartSessionKey] == null)
            {
                //Carts = new List<Cart>();

                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;

                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public void AddToCart(Album album)
        {
            // Get the matching cart and album instances
            //var cartItem = Carts.SingleOrDefault(c => c.AlbumId == album.AlbumId && c.CartId == ShoppingCartId);
            var albumid = album.AlbumId;

            var cartItem = _uow.Cart.GetCartByAlbum(ShoppingCartId, albumid);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                   // Album = album,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                //Carts.Add(cartItem);
                _uow.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
            _uow.Commit();

        }

        public int RemoveFromCart(int id)
        {

            var cartItem = _uow.Cart.GetAlbumByCart(ShoppingCartId, id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                     _uow.Carts.Delete(cartItem);
                }

                // Save changes
                _uow.Commit();
            }

            return itemCount;
        }


         public void EmptyCart()
        {
            var cartItems = _uow.Cart.GetCartItems(ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _uow.Carts.Delete(cartItem);
            }

            // Save changes
            _uow.Commit();
        }


        public List<Cart> GetCartItems()
        {
            return _uow.Cart.GetCartItems(ShoppingCartId);
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
             int? count = _uow.Cart.GetCartSum(ShoppingCartId);
            //// Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            decimal? total = _uow.Cart.GetCartTotal(ShoppingCartId);

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                _uow.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            //storeDB.SaveChanges();
            _uow.Commit();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }


        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _uow.Cart.GetCartItems(ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }

             _uow.Commit();

        }

    }
}