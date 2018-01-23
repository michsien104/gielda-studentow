using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("TakePrivateLessonsAnnouncements")]
    public class TakePrivateLessonsAnnouncement : PrivateLessonsAnnouncement
    {
        public TakePrivateLessonsAnnouncement()
        {
        }
    }
}
