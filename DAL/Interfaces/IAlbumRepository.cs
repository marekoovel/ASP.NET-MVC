﻿using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAlbumRepository : IEFRepository<Album>
    {
        List<Album> GetTopAlbums(int count);
        List<Album> GetAlbums();
    }
}
