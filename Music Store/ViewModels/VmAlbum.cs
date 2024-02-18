using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.ViewModels
{
    /// <summary>
    /// Album ViewModel
    /// </summary>
    public class VmAlbum
    {
        /// <summary>
        /// Album identity
        /// </summary>
        [Required]
        public int AlbumId { get; set; }

        /// <summary>
        /// Title of album
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Genre of album
        /// </summary>
        public VmGenre Genre { get; set; }
    }
}