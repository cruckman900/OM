using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMNext.Models
{
    public enum MissionEnding
    {
        Hurricane1, Hurricane2
    }

    public class RunningMission
    {
        [Key]
        public int MissionID { get; set; }

        [StringLength(50, ErrorMessage = "Flight Director value cannot be longer than 50 characters.")]
        [Display(Name = "Flight Director")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Flight Director value is required.")]
        public string FlightDirector { get; set; }

        [StringLength(10, ErrorMessage = "Booth value cannot be longer than 10 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Booth value is required.")]
        public string Booth { get; set; }

        [Display(Name = "Mission Ending")]
        public MissionEnding MissionEnding { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Mission Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MissionDate { get; set; }

        [Display(Name = "Script")]
        public int? ScriptID { get; set; }

        [Display(Name = "CLC Center ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must be a registered center to run a mission.")]
        public int CLCCenterID { get; set; }

        [ForeignKey("CLCCenterID")]
        public CLCCenter CLCCenter { get; set; }
    }
}