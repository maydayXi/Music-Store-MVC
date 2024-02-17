using Music_Store.Models;
using Music_Store.Repository;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.Services
{
    public class StoreService
    {
        private readonly EFRepository<Album> _albumRepository;
        private readonly EFRepository<Artist> _artistRepository;
        private readonly EFRepository<Genre> _genreRepository;

        /// <summary>
        /// Constructor：Initialize repository
        /// </summary>
        public StoreService()
        {
            MusicShopEntities _context = new MusicShopEntities();
            _albumRepository = new EFRepository<Album>(_context);
            _artistRepository = new EFRepository<Artist>(_context);
            _genreRepository = new EFRepository<Genre>(_context);
        }

        /// <summary>
        /// Get album list for manager page
        /// </summary>
        /// <returns> album data set </returns>
        public IEnumerable<VmAlbumManager> GetAlbums() => _albumRepository.Reads()
            .Join(_artistRepository.Reads(), album => album.ArtistId, artist => artist.ArtistId,
            (album, artist) => new
            {
                album.AlbumId,
                album.GenreId,
                artist.ArtistName,
                album.Title,
                album.Price
            })
            .Join(_genreRepository.Reads(), src => src.GenreId, g => g.GenreId,
            (src, g) => new VmAlbumManager()
            {
                AlbumId = src.AlbumId,
                Genre = new VmGenre() { Name = g.GenreName },
                Artist = new VmArtist() { Name = src.ArtistName },
                Title = src.Title,
                AlbumPrice = src.Price ?? 0
            });

        /// <summary>
        /// Get default album model for create
        /// </summary>
        /// <returns> default album model </returns>
        public VmAlbumEdit GetDefaultAlbum() => new VmAlbumEdit()
        {
            AlbumId = -1,
            SelectGenres = _genreRepository.Reads().Select(g => new SelectListItem()
            {
                Text = g.GenreName,
                Value = $"{g.GenreId}"
            }),
            SelectArtists = _artistRepository.Reads().Select(a => new SelectListItem()
            {
                Text = a.ArtistName,
                Value = $"{a.ArtistId}"
            }),
            Title = string.Empty,
            Price = 0
        };

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id"> Album's identity </param>
        /// <returns> Album information </returns>
        public VmAlbumEdit GetAlbumById(int id)
        {
            Album album = _albumRepository.Read(a => a.AlbumId == id);

            return new VmAlbumEdit()
            {
                AlbumId = album.AlbumId,
                SelectGenres = _genreRepository.Reads().Select(g => new SelectListItem()
                {
                    Text = g.GenreName,
                    Value = $"{g.GenreId}",
                    Selected = g.GenreId == album.GenreId,
                }),
                SelectArtists = _artistRepository.Reads().Select(a => new SelectListItem()
                {
                    Text = a.ArtistName,
                    Value = $"{a.ArtistId}",
                    Selected = a.ArtistId == album.ArtistId,
                }),
                Title = album.Title,
                Price = album.Price ?? 0
            };
        }

        /// <summary>
        /// Get album user want to delete 
        /// </summary>
        /// <param name="id"> Album identity </param>
        /// <returns> Album title and identity </returns>
        public VmAlbum GetDeleteAlbumById(int id)
        {
            Album album = _albumRepository.Read(a => a.AlbumId == id);

            return new VmAlbum()
            {
                Title = album.Title,
                AlbumId = album.AlbumId
            };
        }

        /// <summary>
        /// Create new album
        /// </summary>
        /// <param name="album"> New album model </param>
        public void CreateNewAlbum(VmAlbumEdit album)
        {
            _albumRepository.Create(new Album()
            {
                GenreId = int.Parse(album.SelectedGenre),
                ArtistId = int.Parse(album.SelectedArtist),
                Title = album.Title,
                Price = album.Price,
                AlbumArtUrl = album.AlbumArtUrl,
            });
            _albumRepository.SaveChanges();
        }

        /// <summary>
        /// Update album data
        /// </summary>
        /// <param name="vmAlbumEdit"> New album data </param>
        public void UpdateAlbum(VmAlbumEdit vmAlbumEdit)
        {
            Album album = _albumRepository.Read(a => a.AlbumId == vmAlbumEdit.AlbumId);

            if (album != null)
            {
                album.Title = vmAlbumEdit.Title;
                album.Price = vmAlbumEdit.Price;
                album.ArtistId = int.Parse(vmAlbumEdit.SelectedArtist);
                album.GenreId = int.Parse(vmAlbumEdit.SelectedGenre);
                album.AlbumArtUrl = vmAlbumEdit.AlbumArtUrl;
                _albumRepository.Update(album);
                _albumRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Update album failed.");
            }
        }

        /// <summary>
        /// Delete album data
        /// </summary>
        /// <param name="vmAlbum"> Album data </param>
        public void DeleteAlbum(VmAlbum vmAlbum)
        {
            Album album = _albumRepository.Read(a => a.AlbumId == vmAlbum.AlbumId);

            if(album != null)
            {
                _albumRepository.Delete(album);
                _albumRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Album delete failed.");
            }
        }
    }
}