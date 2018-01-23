using System.Collections.Generic;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IFacultyService
    {
        Faculty GetFacultyById(long id);
        Faculty GetCourseFaculty(long courseId);
        ICollection<CourseOfStudy> GetFacultyCourses(long facultyId);
        ICollection<Student> GetFacultyStudents(long facultyId);
        void AddFaculty(Faculty faculty, long universityId, string creatorId);
    }
}
