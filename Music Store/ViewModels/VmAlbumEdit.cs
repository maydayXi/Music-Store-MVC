using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.ViewModels
{
    /// <summary>
    /// Album edit view model
    /// </summary>
    public class VmAlbumEdit
    {
        /// <summary>
        /// Album's identity
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Album's genre for select
        /// </summary>
        public IEnumerable<SelectListItem> SelectGenres { get; set; }

        /// <summary>
        /// Album's genre user selected
        /// </summary>
        public string SelectedGenre { get; set; }

        /// <summary>
        /// Album's artist for select
        /// </summary>
        public IEnumerable<SelectListItem> SelectArtists { get; set; }

        /// <summary>
        /// Album's artist user selected
        /// </summary>
        public string SelectedArtist {  get; set; }

        /// <summary>
        /// Album's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Album's price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Album's artist image url
        /// </summary>
        public string AlbumArtUrl { get; set; }
    }
}