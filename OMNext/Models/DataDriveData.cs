using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMNext.Models
{
    public enum DataMember
    {
        Communications, Hurricane, Volcano, Evacuation, MedComm, FD, All
    }

    public class DataDriveData
    {
        [Key]
        public int DataDriveDataID { get; set; }

        public int MissionID { get; set; }

        public DataMember From { get; set; }

        public DataMember To { get; set; }

        [StringLength(8000, ErrorMessage = "Data Input cannot be longer than 8000 characters.")]
        [Display(Name = "Data Input")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data Input is required.")]
        public string DataInput { get; set; }
    }
}
