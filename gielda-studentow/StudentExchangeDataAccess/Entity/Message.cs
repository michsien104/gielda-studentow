using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using StudentExchangeDataAccess.ValidationAttributes;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("Messages")]
    public class Message
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "datetime")]
        [PastDate]
        public DateTime IssueDate { get; set; }

        public Student Sender { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 1 and 100 characters long", MinimumLength = 1)]
        public string Subject { get; set; }

        [StringLength(5000, ErrorMessage = "Must be between 1 and 5000 characters long", MinimumLength = 1)]
        public string Content { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Receivers { get; set; }

        public Message()
        {
        }
    }
}
