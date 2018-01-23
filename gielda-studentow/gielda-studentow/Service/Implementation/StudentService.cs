using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using gielda_studentow.Models;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;
using WebGrease.Css.Extensions;

namespace gielda_studentow.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;

        public StudentService(IStudentExchangeDataContext studentExchangeDataContext)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
        }

        public Student GetStudentById(string id)
        {
            return (from s in _studentExchangeDataContext.Users.OfType<Student>() select s).ToList().Find(s => s.Id == id);
        }

        public ICollection<Group> GetStudentGroups(string studentId)
        {
            return _studentExchangeDataContext.Groups.Include(g => g.CourseOfStudy).ToList()
                .FindAll(g => g.Students.Contains(GetStudentById(studentId)));
        }

        public void AddStudentNames(string studentId, FirstLastNameModel model)
        {
            var student = GetStudentById(studentId);
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<Group> GetAdministratedGroups(string studentId)
        {
            var student = _studentExchangeDataContext.Users.OfType<Student>().Include(s => s.AdministratedGroups).ToList().Find(s => s.Id == studentId);
            return student.AdministratedGroups;
        }

        public ICollection<CourseOfStudy> GetAdministratedCourses(string studentId)
        {
            var student = _studentExchangeDataContext.Users.OfType<Student>().Include(s => s.AdministratedCourses).ToList().Find(s => s.Id == studentId);
            return student.AdministratedCourses;
        }

        public ICollection<Faculty> GetAdministratedFaculties(string studentId)
        {
            var student = _studentExchangeDataContext.Users.OfType<Student>().Include(s => s.AdministratedFaculties).ToList().Find(s => s.Id == studentId);
            return student.AdministratedFaculties;
        }

        public ICollection<University> GetAdministratedUniversities(string studentId)
        {
            var student = _studentExchangeDataContext.Users.OfType<Student>().Include(s => s.AdministratedUniversities).ToList().Find(s => s.Id == studentId);
            return student.AdministratedUniversities;
        }
    }
}