using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IGroupService
    {
        Group GetGroupById(long id);
        ICollection<Group> GetStudentCourseGroups(string studentId, long courseId);
        ICollection<Student> GetGroupMembers(long groupId);
        void AddStudentToGroup(string studentId, long groupId);
        void RemoveStudentFromGroup(string studentId, long groupId);
        ICollection<Group> GetCourseGroupsWhereStudentIsNotAssigned(long courseId, string studentId);
        void AddGroup(Group group, long courseOfStudyId, string creatorId);
    }
}
