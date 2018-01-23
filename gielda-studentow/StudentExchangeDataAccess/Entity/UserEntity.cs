using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    public class UserEntity : IdentityUser
    {
        [JsonIgnore]
        public virtual ICollection<Announcement> ReceivedAnnouncements { get; set; }
        public string AvatarUrl { get; set; }
    }
}
