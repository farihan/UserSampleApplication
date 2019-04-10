using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hans.UserSample.Core.Entities
{
    public class User : BaseEntity
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The field is required")]
        public string Username { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "The field is required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "The field is required")]
        public string LastName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "The field is required")]
        public string Role { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The field is required")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The field is required")]
        public string Phone { get; set; }
    }
}
