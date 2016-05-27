using DAL.Interfaces;
using Domain;
using Main_Page.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_Page.Controllers
{
    public class HomeController : BaseController
    {
         // GET: Store
        private readonly IUOW _uow;

        public HomeController(IUOW uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedUICulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }         



        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return _uow.Album.GetTopAlbums(count);
        }
    }
}