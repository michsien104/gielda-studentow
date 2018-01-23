using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using Newtonsoft.Json;
using StudentExchangeDataAccess.ValidationAttributes;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    public abstract class Announcement
    {
        public enum Status
        {
            Active,
            NotActive,
            Expired
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [PastDate]
        [Column(TypeName = "datetime")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [FutureDate]
        public DateTime ExpirationDate { get; set; }
       
        public UserEntity Sender { get; set; }
             
        [JsonIgnore]
        public virtual ICollection<UserEntity> Receivers { get; set; }

        [StringLength(5000,ErrorMessage = "Must be between 1 and 5000 characters long",MinimumLength = 1)]
        public string Content { get; set; }

        [StringLength(150, ErrorMessage = "Must be between 1 and 150 characters long", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public Status CurrentStatus { get; set; }

        public virtual ICollection<ItemImage> Images { get; set; }
    }
}
