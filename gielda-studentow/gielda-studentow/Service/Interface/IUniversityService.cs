using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IUniversityService
    {
        University GetUniversityById(long id);
        ICollection<University> GetAll();
        ICollection<University> GetStudentUniversities(string studentId);
        University GetFacultyUniversity(long facultyId);
        ICollection<Faculty> GetUniversityFaculties(long universityId);
        ICollection<Student> GetUniversityStudents(long universityId);
        void AddUniversity(University university, string creatorId);
    }
}
