using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.ViewModels
{
    public class VmLogon
    {
        /// <summary>
        /// User name 
        /// </summary>
        [Required(ErrorMessage = "Username is required !")]
        [MaxLength(100, ErrorMessage = "Username must be between 1 and 100")]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is reuqired !")]
        public string Password { get; set; }

        /// <summary>
        /// User is administrator
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}