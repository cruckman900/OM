using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMNext.Models
{
    public class CLCCenter
    {
        [Key]
        public int CLCCenterID { get; set; }

        [Display(Name = "CLC Center Abbr")]
        [StringLength(10, ErrorMessage = "Abbreviation value cannot be longer than 10 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Abbreviation value is required.")]
        public string Abbreviation { get; set; }

        [Display(Name = "Center Name")]
        [StringLength(50, ErrorMessage = "Center Name value cannot be longer than 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Center Name is required.")]
        public string CenterName { get; set; }

        [Display(Name = "Address Line 1")]
        [StringLength(100, ErrorMessage = "Address1 value cannot be longer than 100 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(100, ErrorMessage = "Address2 value cannot be longer than 100 characters.")]
        public string Address2 { get; set; }

        [StringLength(50, ErrorMessage = "City value cannot be longer than 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required.")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "State value cannot be longer than 2 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "State abbreviation is required.")]
        public string State { get; set; }

        [StringLength(15, ErrorMessage = "Zipcode value cannot be longer than 15 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Zipcode is required.")]
        public string Zipcode { get; set; }
        
        [StringLength(20, ErrorMessage = "Phone value cannot be longer than 20 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Has Access To MedComm")]
        public bool? HasAccessToMedComm { get; set; } = false;

        public List<RunningMission> RunningMission { get; set; }
    }
}