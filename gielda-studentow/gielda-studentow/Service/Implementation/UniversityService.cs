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
    public class UniversityService : IUniversityService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;
        private readonly IFacultyService _facultyService;

        public UniversityService(IStudentExchangeDataContext studentExchangeDataContext, IFacultyService facultyService)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
            _facultyService = facultyService;
        }

        public University GetUniversityById(long id)
        {
            return _studentExchangeDataContext.Universities.Include(u => u.Faculties).ToList().Find(u => u.Id == id);
        }

        public ICollection<University> GetAll()
        {
            return _studentExchangeDataContext.Universities.ToList();
        }

        public ICollection<University> GetStudentUniversities(string studentId)
        {
            var groups = _studentExchangeDataContext.Groups.ToList().FindAll(g =>
                g.Students.Contains(_studentExchangeDataContext.GetUserById(studentId)));
            var courses = groups.Select(@group => _studentExchangeDataContext.CoursesOfStudy.ToList().Find(c => c.Groups.Contains(@group))).ToList();
            var faculties = courses.Select(course => _studentExchangeDataContext.Faculties.ToList().Find(f => f.CoursesOfStudy.Contains(course))).ToList();
            var universities = faculties.Select(faculty => _studentExchangeDataContext.Universities.ToList().Find(u => u.Faculties.Contains(faculty))).ToList();
            universities.RemoveAll(u => u == null);
            return universities.Distinct().ToList();
        }

        public University GetFacultyUniversity(long facultyId)
        {
            var faculty = _studentExchangeDataContext.Faculties.ToList().Find(f => f.Id == facultyId);
            return _studentExchangeDataContext.Universities.ToList().Find(u => u.Faculties.Contains(faculty));
        }

        public ICollection<Faculty> GetUniversityFaculties(long universityId)
        {
            return _studentExchangeDataContext.Universities.Include(u => u.Faculties).ToList()
                .Find(u => u.Id == universityId).Faculties;
        }

        public ICollection<Student> GetUniversityStudents(long universityId)
        {
            var universityFaculties = GetUniversityFaculties(universityId);
            var students = new List<Student>();
            foreach (var faculty in universityFaculties)
            {
                students.AddRange(_facultyService.GetFacultyStudents(faculty.Id));
            }
            return students.Distinct().ToList();
        }

        public void AddUniversity(University university, string creatorId)
        {
            var newUniversity = new University()
            {
                Name = university.Name,
                ShortName = university.ShortName,
                Administrators = new List<Student>()
                {
                    _studentExchangeDataContext.Users.OfType<Student>().ToList().Find(s => s.Id == creatorId)
                }
            };
            _studentExchangeDataContext.Universities.Add(newUniversity);
            _studentExchangeDataContext.SaveChanges();
        }
    }
}