using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("ChangeGroupAnnouncements")]
    public class ChangeGroupAnnouncement : Announcement
    {
        public Group FromGroup { get; set; }

        [Required]
        public Group DestinationGroup { get; set; }

        public ChangeGroupAnnouncement()
        {
        }
    }
}
