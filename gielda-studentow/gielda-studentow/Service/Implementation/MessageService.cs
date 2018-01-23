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
    public class MessageService : IMessageService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;
        private readonly IUniversityService _universityService;
        private readonly IFacultyService _facultyService;
        private readonly ICourseOfStudyService _courseOfStudyService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;

        public MessageService(IStudentExchangeDataContext studentExchangeDataContext, IUniversityService universityService, IFacultyService facultyService, ICourseOfStudyService courseOfStudyService, IGroupService groupService, IStudentService studentService)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
            _universityService = universityService;
            _facultyService = facultyService;
            _courseOfStudyService = courseOfStudyService;
            _groupService = groupService;
            _studentService = studentService;
        }

        public ICollection<Message> SortMessagesNewestFirst(ICollection<Message> messages)
        {
            return messages.OrderByDescending(m => m.IssueDate).ToList();
        }

        public ICollection<Message> GetAllStudentMessages(string studentId)
        {
            var student = _studentService.GetStudentById(studentId);
            var studentMessages = _studentExchangeDataContext.Messages.Include(m => m.Receivers).Include(m => m.Sender).ToList()
                .FindAll(m => m.Receivers.Contains(student));
            return studentMessages;
        }

        public Message GetMessageById(long id)
        {
            return _studentExchangeDataContext.Messages.ToList().Find(m => m.Id == id);
        }

        public void NewUniversityMessage(Message message, long universityId, string senderId)
        {
            var newMessage = new Message()
            {
                Subject = message.Subject,
                Content = message.Content,
                Receivers = _universityService.GetUniversityStudents(universityId),
                IssueDate = DateTime.Now,
                Sender = _studentService.GetStudentById(senderId)
            };
            _studentExchangeDataContext.Messages.Add(newMessage);
            _studentExchangeDataContext.SaveChanges();
            
        }

        public void NewFacultyMessage(Message message, long facultyId, string senderId)
        {
            var newMessage = new Message()
            {
                Subject = message.Subject,
                Content = message.Content,
                Receivers = _facultyService.GetFacultyStudents(facultyId),
                IssueDate = DateTime.Now,
                Sender = _studentService.GetStudentById(senderId)
            };
            _studentExchangeDataContext.Messages.Add(newMessage);
            _studentExchangeDataContext.SaveChanges();
        }

        public void NewCourseOfStudyMessage(Message message, long courseId, string senderId)
        {
            var newMessage = new Message()
            {
                Subject = message.Subject,
                Content = message.Content,
                Receivers = _courseOfStudyService.GetCourseOfStudyStudents(courseId),
                IssueDate = DateTime.Now,
                Sender = _studentService.GetStudentById(senderId)
            };
            _studentExchangeDataContext.Messages.Add(newMessage);
            _studentExchangeDataContext.SaveChanges();
        }

        public void NewGroupMessage(Message message, long groupId, string senderId)
        {
            var newMessage = new Message()
            {
                Subject = message.Subject,
                Content = message.Content,
                Receivers = _groupService.GetGroupMembers(groupId),
                IssueDate = DateTime.Now,
                Sender = _studentService.GetStudentById(senderId)
            };
            _studentExchangeDataContext.Messages.Add(newMessage);
            _studentExchangeDataContext.SaveChanges();
        }
    }
}