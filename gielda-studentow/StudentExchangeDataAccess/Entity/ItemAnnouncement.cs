using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("ItemAnnouncements")]
    public abstract class ItemAnnouncement : Announcement
    {
        [StringLength(30, ErrorMessage = "Must be between 1 and 30 characters long", MinimumLength = 1)]
        public string Category { get; set; }

        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
