using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Music_Store.ViewModels
{
    /// <summary>
    /// Genre ViewModel
    /// </summary>
    public class VmGenre
    {
        /// <summary>
        /// Name of genre
        /// </summary>
        [DisplayName("Genre")]
        public string Name {  get; set; }

        /// <summary>
        /// Image path of Genre
        /// </summary>
        public string ImgPath { get; set; }
    }
}