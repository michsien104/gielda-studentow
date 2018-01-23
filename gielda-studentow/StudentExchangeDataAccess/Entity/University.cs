using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    public class University
    {
        [Key]
        public long Id { get; set; }

        [StringLength(50, ErrorMessage = "Must be between 1 and 50 characters long", MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "Must be between 1 and 10 characters long", MinimumLength = 1)]
        public string ShortName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Faculty> Faculties { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Administrators { get; set; }

        public University()
        {
        }
    }
}
