using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IMessageService
    {
        ICollection<Message> SortMessagesNewestFirst(ICollection<Message> messages);
        ICollection<Message> GetAllStudentMessages(string studentId);
        Message GetMessageById(long id);
        void NewUniversityMessage(Message message, long universityId, string senderId);
        void NewFacultyMessage(Message message, long facultyId, string senderId);
        void NewCourseOfStudyMessage(Message message, long courseId, string senderId);
        void NewGroupMessage(Message message, long groupId, string senderId);
    }
}
