using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("PrivateLessonsAnnouncements")]
    public abstract class PrivateLessonsAnnouncement : Announcement
    {
        [StringLength(100, ErrorMessage = "Must be between 1 and 100 characters long", MinimumLength = 1)]
        public string Subject { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 1 and 255 characters long", MinimumLength = 1)]
        public string Location { get; set; }
    }
}
