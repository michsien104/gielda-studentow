using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("GivePrivateLessonsAnnouncements")]
    public class GivePrivateLessonsAnnouncement : PrivateLessonsAnnouncement
    {
        public GivePrivateLessonsAnnouncement()
        {
        }
    }
}
