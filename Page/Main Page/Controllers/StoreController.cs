using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;


namespace Main_Page.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Store

        private readonly IUOW _uow;

        public StoreController(IUOW uow)
        {
            _uow = uow;
        }

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = _uow.Genres.All;
            return View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco
        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            //var genreModel = storeDB.Genres.Include("Albums")
            //    .Single(g => g.Name == genre);

            var genreModel = _uow.Genre.GetGenreWithAlbums(genre);

            return View(genreModel);
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var album = _uow.Albums.GetById(id);
            return View(album);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = _uow.Genres.All;
            return PartialView(genres);
        }
    }
}