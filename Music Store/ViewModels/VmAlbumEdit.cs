using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

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
        [Required]
        public int AlbumId { get; set; }

        /// <summary>
        /// Album's genre for select
        /// </summary>
        public IEnumerable<SelectListItem> SelectGenres { get; set; }

        /// <summary>
        /// Album's genre user selected
        /// </summary>
        [DisplayName("Genre")]
        public string SelectedGenre { get; set; }

        /// <summary>
        /// Album's artist for select
        /// </summary>
        public IEnumerable<SelectListItem> SelectArtists { get; set; }

        /// <summary>
        /// Album's artist user selected
        /// </summary>
        [DisplayName("Aritst")]
        public string SelectedArtist {  get; set; }

        /// <summary>
        /// Album's title
        /// </summary>
        [Required(ErrorMessage = "Ablum title is requeired!")]
        [StringLength(100, ErrorMessage = "Title max 100 characters")]
        public string Title { get; set; }

        /// <summary>
        /// Album's price
        /// </summary>
        [Range(0.00, 100.00, ErrorMessage = "Price must be between 0.00 and 100.00")]
        public decimal Price { get; set; }

        /// <summary>
        /// Album's artist image url
        /// </summary>
        public string AlbumArtUrl { get; set; }
    }
}