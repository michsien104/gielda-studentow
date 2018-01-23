using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("BuyItemAnnouncements")]
    public class BuyItemAnnouncement : ItemAnnouncement
    {
        public BuyItemAnnouncement()
        {
        }
    }
}
