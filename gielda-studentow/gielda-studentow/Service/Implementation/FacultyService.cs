using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Implementation
{
    public class FacultyService : IFacultyService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;
        private readonly ICourseOfStudyService _courseOfStudyService;

        public FacultyService(IStudentExchangeDataContext studentExchangeDataContext, ICourseOfStudyService courseOfStudyService)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
            _courseOfStudyService = courseOfStudyService;
        }

        public Faculty GetFacultyById(long id)
        {
            return _studentExchangeDataContext.Faculties.Include(f => f.University).ToList().Find(f => f.Id == id);
        }

        public Faculty GetCourseFaculty(long courseId)
        {
            var course = _studentExchangeDataContext.CoursesOfStudy.ToList().Find(c => c.Id == courseId);
            return _studentExchangeDataContext.Faculties.Include(f => f.University).ToList()
                .Find(f => f.CoursesOfStudy.Contains(course));
        }

        public ICollection<CourseOfStudy> GetFacultyCourses(long facultyId)
        {
            return _studentExchangeDataContext.Faculties.Include(f => f.CoursesOfStudy).ToList()
                .Find(f => f.Id == facultyId).CoursesOfStudy;
        }

        public ICollection<Student> GetFacultyStudents(long facultyId)
        {
            var facultyCourses = GetFacultyCourses(facultyId);
            var students = new List<Student>();
            foreach (var course in facultyCourses)
            {
                students.AddRange(_courseOfStudyService.GetCourseOfStudyStudents(course.Id));
            }
            return students.Distinct().ToList();
        }

        public void AddFaculty(Faculty faculty, long universityId, string creatorId)
        {
            var newFaculty = new Faculty()
            {
                Name = faculty.Name,
                ShortName = faculty.ShortName,
                University = _studentExchangeDataContext.Universities.Find(universityId),
                Administrators = new List<Student>()
                {
                    _studentExchangeDataContext.Users.OfType<Student>().ToList().Find(s => s.Id == creatorId)
                }
            };
            _studentExchangeDataContext.Faculties.Add(newFaculty);
            _studentExchangeDataContext.SaveChanges();
        }
    }
}