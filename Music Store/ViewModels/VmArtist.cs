using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Music_Store.ViewModels
{
    public class VmArtist
    {
        /// <summary>
        /// Name of artist
        /// </summary>
        [DisplayName("Artist")]
        public string Name { get; set; }
    }
}