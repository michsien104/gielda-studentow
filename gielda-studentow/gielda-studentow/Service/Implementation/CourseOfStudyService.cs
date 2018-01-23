using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Implementation
{
    public class CourseOfStudyService : ICourseOfStudyService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;

        public CourseOfStudyService(IStudentExchangeDataContext studentExchangeDataContext)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
        }

        public CourseOfStudy GetCourseById(long id)
        {
            return _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Faculty).ToList().Find(c => c.Id == id);
        }

        public ICollection<CourseOfStudy> GetStudentCourses(string studentId)
        {
            var groups = _studentExchangeDataContext.Groups.Include(g => g.CourseOfStudy).ToList().FindAll(g =>
                g.Students.Contains(_studentExchangeDataContext.GetUserById(studentId)));
            var courses = groups.Select(@group => @group.CourseOfStudy).Distinct().ToList();

            var faculties = new List<Faculty>();
            foreach (var course in courses)
            {
                course.Faculty = _studentExchangeDataContext.Faculties.Include(f => f.CoursesOfStudy).ToList()
                    .Find(f => f.CoursesOfStudy.Contains(course));
                faculties.Add(course.Faculty);
            }

            foreach (var faculty in faculties)
            {
                faculty.University = _studentExchangeDataContext.Universities.Include(u => u.Faculties).ToList()
                    .Find(u => u.Faculties.Contains(faculty));
            }
            return courses;
        }

        public ICollection<CourseOfStudy> GetLeftoverStudentCourses(long facultyId, string studentId)
        {
            var facultyCourses = _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Faculty).ToList()
                .FindAll(c => c.Faculty.Id == facultyId);
            return facultyCourses.Except(GetStudentCourses(studentId)).ToList();
        }

        public CourseOfStudy GetGroupCourse(long groupId)
        {
            var group = _studentExchangeDataContext.Groups.ToList().Find(g => g.Id == groupId);
            return _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Faculty).ToList()
                .Find(c => c.Groups.Contains(group));
        }

        public ICollection<Group> GetCourseGroups(long courseId)
        {
            return _studentExchangeDataContext.Groups.Include(g => g.CourseOfStudy).ToList()
                .FindAll(g => g.CourseOfStudy.Id == courseId);
        }

        public ICollection<Student> GetCourseOfStudyStudents(long courseId)
        {
            var groups = GetCourseGroups(courseId);
            var students = new List<Student>();
            foreach (var group in groups)
            {
                students.AddRange(group.Students);
            }
            return students.Distinct().ToList();
        }

        public void AddCourseOfStudy(CourseOfStudy courseOfStudy, long facultyId, string creatorId)
        {
            var newCourse = new CourseOfStudy()
            {
                Faculty = _studentExchangeDataContext.Faculties.Find(facultyId),
                Name = courseOfStudy.Name,
                Administrators = new List<Student>()
                {
                    _studentExchangeDataContext.Users.OfType<Student>().ToList().Find(s => s.Id == creatorId)
                },
                StartYear = courseOfStudy.StartYear
            };
            _studentExchangeDataContext.CoursesOfStudy.Add(newCourse);
            _studentExchangeDataContext.SaveChanges();
        }
    }
}