using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMNext.Models
{
    public enum ChatMember
    {
        Hurricane, Volcano, Communications, Evacuation, MedComm, FD, All
    }

    public class Chat
    {
        [Key]
        public int ChatID { get; set; }

        public int MissionID { get; set; }

        public ChatMember From { get; set; }

        public ChatMember To { get; set; }

        [StringLength(8000, MinimumLength = 2, ErrorMessage = "Message must be between 2 and 8000 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message is required.")]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Timestamp")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SentDateTime { get; set; }
    }
}
