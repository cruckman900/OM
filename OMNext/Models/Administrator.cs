using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMNext.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }

        [StringLength(25, ErrorMessage = "First Name cannot be longer than 25 characters.")]
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "Last Name cannot be longer than 25 characters.")]
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "User Name value cannot be longer than 50 characters.")]
        [Display(Name = "User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [StringLength(25, ErrorMessage = "Password value cannot be longer than 25 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
