using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("Students")]
    public class Student : UserEntity
    {
        [StringLength(30, ErrorMessage = "Must be between 1 and 30 characters long", MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Must be between 1 and 30 characters long", MinimumLength = 1)]
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        [JsonIgnore]
        public virtual ICollection<University> AdministratedUniversities { get; set; }

        [JsonIgnore]
        public virtual ICollection<Faculty> AdministratedFaculties { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseOfStudy> AdministratedCourses { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> AdministratedGroups { get; set; }

        public Student()
        {
        }
    }
}
