using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("SellItemAnnouncements")]
    public class SellItemAnnouncement : ItemAnnouncement
    {
        public SellItemAnnouncement()
        {
        }
    }
}
