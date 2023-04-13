using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMNext.Models
{
    public class Script
    {
        [Key]
        public int ScriptID { get; set; }

        [StringLength(50, ErrorMessage = "Script Title value cannot be longer than 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title value is required.")]
        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Script Body")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Script Body is required.")]
        public string ScriptBody { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created On Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOnDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Modified Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }
    }
}
