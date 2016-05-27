using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_Page.ViewModels
{
    public class CreateEditAlbumViewModel
    {
        public Album Album { get; set; }
        public SelectList ArtistSelectList { get; set; }
        public SelectList GenreSelectList { get; set; }
    }
}