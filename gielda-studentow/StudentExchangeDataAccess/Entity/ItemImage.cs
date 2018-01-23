using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    public class ItemImage
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public Announcement Announcement { get; set; }
    }
}
