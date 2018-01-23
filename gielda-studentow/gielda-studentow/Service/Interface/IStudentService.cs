using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gielda_studentow.Models;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IStudentService
    {
        Student GetStudentById(string id);
        ICollection<Group> GetStudentGroups(string studentId);
        void AddStudentNames(string studentId, FirstLastNameModel model);
        ICollection<Group> GetAdministratedGroups(string studentId);
        ICollection<CourseOfStudy> GetAdministratedCourses(string studentId);
        ICollection<Faculty> GetAdministratedFaculties(string studentId);
        ICollection<University> GetAdministratedUniversities(string studentId);
    }
}
