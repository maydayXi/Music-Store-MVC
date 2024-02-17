using System;
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
        /// Title of album
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Genre of album
        /// </summary>
        public VmGenre Genre { get; set; }
    }
}