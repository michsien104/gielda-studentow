using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using gielda_studentow.Models;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Implementation
{
    public class TutorService : ITutorService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;

        public TutorService(IStudentExchangeDataContext studentExchangeDataContext)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
        }

        public Tutor GeTutorById(string tutorId)
        {
            return (from t in _studentExchangeDataContext.Users.OfType<Tutor>().Include(t => t.TutorCoursesOfStudy) select t).ToList()
                .Find(t => t.Id == tutorId);
        }

        public void AddTutorCourse(string tutorId, long courseId)
        {
            var tutor = GeTutorById(tutorId);

            if (tutor.TutorCoursesOfStudy.ToList().Find(c => c.Id == courseId) == null)
            {
                tutor.TutorCoursesOfStudy.Add(_studentExchangeDataContext.CoursesOfStudy.ToList().Find(c => c.Id == courseId));
                _studentExchangeDataContext.SaveChanges();
            }
        }

        public void AddTutorNames(string tutorId, FirstLastNameModel model)
        {
            var tutor = GeTutorById(tutorId);
            tutor.FirstName = model.FirstName;
            tutor.LastName = model.LastName;
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<CourseOfStudy> GetCourses(string tutorId)
        {
            var tutor = GeTutorById(tutorId);
            if (tutor == null)
                return new List<CourseOfStudy>();
            return tutor.TutorCoursesOfStudy;
        }
    }
}