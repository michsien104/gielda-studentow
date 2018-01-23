using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    public class CourseOfStudy
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 1 and 100 characters long", MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(4, ErrorMessage = "Must be exactly 4 characters long", MinimumLength = 4)]
        public string StartYear { get; set; }

        public Student RepresentativeStudent { get; set; }

        public Faculty Faculty { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tutor> Tutors { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Administrators { get; set; }

        public CourseOfStudy()
        {
        }
    }
}
