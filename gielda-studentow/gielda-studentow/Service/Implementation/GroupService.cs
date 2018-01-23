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
    public class GroupService : IGroupService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;
        private readonly ICourseOfStudyService _courseOfStudyService;

        public GroupService(IStudentExchangeDataContext studentExchangeDataContext, ICourseOfStudyService courseOfStudyService)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
            _courseOfStudyService = courseOfStudyService;
        }

        public Group GetGroupById(long id)
        {
            return _studentExchangeDataContext.Groups.Include(g => g.CourseOfStudy).ToList().Find(g => g.Id == id);
        }

        private ICollection<Group> GetStudentGroups(string studentId)
        {
            return _studentExchangeDataContext.Groups.Include(g => g.CourseOfStudy).ToList()
                .FindAll(g => g.Students.Contains(GetStudentById(studentId)));
        }

        private Student GetStudentById(string id)
        {
            return (from s in _studentExchangeDataContext.Users.OfType<Student>() select s).ToList().Find(s => s.Id == id);
        }

        public ICollection<Group> GetStudentCourseGroups(string studentId, long courseId)
        {
            var studentGroups = GetStudentGroups(studentId);
            return studentGroups.ToList().FindAll(g => g.CourseOfStudy.Id == courseId);
        }

        public ICollection<Student> GetGroupMembers(long groupId)
        {
            return _studentExchangeDataContext.Groups.ToList().Find(g => g.Id == groupId).Students;
        }

        public void AddStudentToGroup(string studentId, long groupId)
        {
            var student = GetStudentById(studentId);
            var group = GetGroupById(groupId);
            group.Students.Add(student);
            _studentExchangeDataContext.SaveChanges();
        }

        public void RemoveStudentFromGroup(string studentId, long groupId)
        {
            var student = GetStudentById(studentId);
            var group = GetGroupById(groupId);
            group.Students.Remove(student);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<Group> GetCourseGroupsWhereStudentIsNotAssigned(long courseId, string studentId)
        {
            var course = _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Groups).ToList()
                .Find(c => c.Id == courseId);
            var groups = _studentExchangeDataContext.Groups.Include(g => g.Students).ToList()
                .FindAll(g => g.CourseOfStudy == course);
            groups.RemoveAll(g => g.Students.Contains(GetStudentById(studentId)));
            return groups;
        }

        public void AddGroup(Group group, long courseOfStudyId, string creatorId)
        {
            var newGroup = new Group()
            {
                Name = group.Name,
                CourseOfStudy = _courseOfStudyService.GetCourseById(courseOfStudyId),
                Administrators = new List<Student>()
                {
                    _studentExchangeDataContext.Users.OfType<Student>().ToList().Find(s => s.Id == creatorId)
                }
            };
            _studentExchangeDataContext.Groups.Add(newGroup);
            _studentExchangeDataContext.SaveChanges();
        }
    }
}