using DAL.Interfaces;
using Main_Page.Service;
using Main_Page.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_Page.Controllers
{
    public class ShoppingCartController : BaseController
    {

        private readonly IUOW _uow;

        public ShoppingCartController(IUOW uow)
        {
            _uow = uow;
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _uow);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {

            var addedAlbum = _uow.Albums.GetById(id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext, _uow);

            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext, _uow);
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

             // Get the name of the album to display confirmation

            //string albumName = _uow.Cart..Album.Title;
            //string albumName = ShoppingCart.Carts.Single(item => item.AlbumId == id).Album.Title;
            

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {

                Message = Server.HtmlEncode("sdasd") +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _uow);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}