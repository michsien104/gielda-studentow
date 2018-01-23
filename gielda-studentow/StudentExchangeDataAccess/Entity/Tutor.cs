using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExchangeDataAccess.Entity
{
    [Serializable]
    [Table("Tutors")]
    public class Tutor : UserEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<CourseOfStudy> TutorCoursesOfStudy { get; set; }

        public Tutor()
        {
        }
    }
}
