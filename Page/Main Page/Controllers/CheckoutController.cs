using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Main_Page.Service;
using DAL.Interfaces;
using Main_Page.Controllers;

namespace MvcMusicStore.Controllers
{
    public class CheckoutController : BaseController
    {
        const string PromoCode = "FREE";

        private readonly IUOW _uow;

        public CheckoutController(IUOW uow)
        {
            _uow = uow;
        }

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    _uow.Orders.Add(order);
                    _uow.Commit();
                    

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext, _uow);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
             //Validate customer owns this order
            bool isValid = _uow.Order.GetCustomerOrder(id, User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
