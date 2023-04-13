using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMNext.Models
{
    public class DataDrive
    {
        [Key]
        public int DataDriveID { get; set; }

        public int MissionID { get; set; }

        public int TeamID { get; set; }

        public int CurrentRecord { get; set; }

        public int CurrentTimerInterval { get; set; } = 5000;
    }
}
