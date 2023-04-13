using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMNext.Models
{
    public enum TeamName
    {
        Communication, Hurricane, Volcano, Evacuation, MedComm
    }

    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Display(Name = "Team Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Team Name is required.")]
        public TeamName TeamName { get; set; }

        [StringLength(25, ErrorMessage = "Password value cannot be longer than 25 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public int MissionID { get; set; }

        [NotMapped]
        public bool HasMedCommTeam { get; set; }
    }
}