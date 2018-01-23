using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gielda_studentow.Models;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface ITutorService
    {
        Tutor GeTutorById(string tutorId);
        void AddTutorCourse(string tutorId, long courseId);
        void AddTutorNames(string tutorId, FirstLastNameModel model);
        ICollection<CourseOfStudy> GetCourses(string tutorId);
    }
}
