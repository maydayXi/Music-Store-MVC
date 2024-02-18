using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.ViewModels
{
    public class VmAlbumManager
    {
        /// <summary>
        /// Album identity
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Album's Genre
        /// </summary>
        public VmGenre Genre { get; set; }

        /// <summary>
        /// Album's artist
        /// </summary>
        public VmArtist Artist { get; set; }

        /// <summary>
        /// Album's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Album's price
        /// </summary>
        [DisplayName("Album Price")]
        public decimal AlbumPrice { get; set; }
    }
}