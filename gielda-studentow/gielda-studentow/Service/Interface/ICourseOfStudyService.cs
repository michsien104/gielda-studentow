using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface ICourseOfStudyService
    {
        CourseOfStudy GetCourseById(long id);
        ICollection<CourseOfStudy> GetStudentCourses(string studentId);
        CourseOfStudy GetGroupCourse(long groupId);
        ICollection<Group> GetCourseGroups(long courseId);
        ICollection<CourseOfStudy> GetLeftoverStudentCourses(long facultyId, string studentId);
        ICollection<Student> GetCourseOfStudyStudents(long courseId);
        void AddCourseOfStudy(CourseOfStudy courseOfStudy, long facultyId, string creatorId);
    }
}
