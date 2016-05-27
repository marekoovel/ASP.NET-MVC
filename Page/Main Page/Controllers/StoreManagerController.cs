using DAL.Interfaces;
using Domain;
using Main_Page.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Main_Page.Controllers
{
    [Authorize(Users= "marekoovel")]
    public class StoreManagerController : BaseController
    {

        //// GET: Store
        private readonly IUOW _uow;

        public StoreManagerController(IUOW uow)
        {
            _uow = uow;
        }

        //
        // GET: /StoreManager/
        public ViewResult Index()
        {
            var albums = _uow.Album.GetAlbums();
            return View(albums);
        }

        //
        // GET: /StoreManager/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album =  _uow.Albums.GetById(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            var vm = new CreateEditAlbumViewModel();
            vm.ArtistSelectList = new SelectList(_uow.Artists.All, "ArtistId", "Name");
            vm.GenreSelectList = new SelectList(_uow.Genre.All, "GenreId", "Name");
            return View(vm);

        }

        //
        // POST: /StoreManager/Create
        [HttpPost]
        public ActionResult Create(CreateEditAlbumViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Albums.Add(vm.Album);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            vm.ArtistSelectList = new SelectList(_uow.Artists.All, "ArtistId", "Name", vm.Album.ArtistId);
            vm.GenreSelectList = new SelectList(_uow.Genre.All, "GenreId", "Name", vm.Album.GenreId);
            return View(vm);

        }

        //
        // GET: /StoreManager/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = _uow.Albums.GetById(id);

            if (album == null)
            {
                return HttpNotFound();
            }

            var vm = new CreateEditAlbumViewModel();
            vm.Album = album;
            vm.ArtistSelectList = new SelectList(_uow.Artists.All, "ArtistId", "Name", vm.Album.ArtistId);
            vm.GenreSelectList = new SelectList(_uow.Genre.All, "GenreId", "Name", vm.Album.GenreId);
            return View(vm);
        }

        //
        // POST: /StoreManager/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateEditAlbumViewModel vm)
        {

            if (ModelState.IsValid)
            {
                _uow.Albums.Update(vm.Album);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            vm.ArtistSelectList = new SelectList(_uow.Artists.All, "ArtistId", "Name", vm.Album.ArtistId);
            vm.GenreSelectList = new SelectList(_uow.Genre.All, "GenreId", "Name", vm.Album.GenreId);
            return View(vm);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = _uow.Albums.GetById(id);

            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Albums.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}