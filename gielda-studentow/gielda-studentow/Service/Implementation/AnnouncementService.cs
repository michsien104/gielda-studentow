using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Implementation
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;
        private readonly ICourseOfStudyService _courseOfStudyService;
        private readonly IUniversityService _universityService;

        public AnnouncementService(IStudentExchangeDataContext studentExchangeDataContext, ICourseOfStudyService courseOfStudyService, IUniversityService universityService)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
            _courseOfStudyService = courseOfStudyService;
            _universityService = universityService;
        }

        public Announcement GetAnnouncementById(long id, string receiverId)
        {
            return GetAllAnnouncements(receiverId).ToList().Find(a => a.Id == id);
        }
        public ICollection<Announcement> GetAllSenderAnnouncements(string senderID)
        {
            var sender = _studentExchangeDataContext.Users.Find(senderID);
            var activeAnnouncements = _studentExchangeDataContext.Announcements.ToList().FindAll(a => a.CurrentStatus == 0);
            var announcements = activeAnnouncements.ToList().FindAll(a => a.Sender.Equals(sender));
            foreach (var announcement in announcements)
            {
                announcement.Images = GetAnnouncementImages(announcement.Id);
            }
            return announcements;
        }

        public ICollection<Announcement> GetAllAnnouncements(string receiverId)
        {
            var receiver = _studentExchangeDataContext.Users.Find(receiverId);
            var announcements = _studentExchangeDataContext.Announcements.ToList().FindAll(a => a.Receivers.Contains(receiver));
            foreach (var announcement in announcements)
            {
                announcement.Images = GetAnnouncementImages(announcement.Id);
            }
            return announcements;
        }

        public ICollection<Announcement> SortAnnouncementsByIssueDateNewestFirst(ICollection<Announcement> announcements)
        {
            return announcements.OrderByDescending(a => a.IssueDate).ToList();
        }

        public ICollection<ItemAnnouncement> GetItemAnnouncements(string receiverId)
        {
            return (from i in _studentExchangeDataContext.Announcements.OfType<ItemAnnouncement>() select i ).ToList().FindAll(i => i.Receivers.Contains(_studentExchangeDataContext.GetUserById(receiverId)));
        }

        public void AddBuyItemAnnouncement(BuyItemAnnouncement announcement, string senderId)
        {
            announcement.Sender = _studentExchangeDataContext.Users.Find(senderId);
            announcement.IssueDate = DateTime.Now;
            announcement.Receivers = SetBuyItemAnnouncementReceivers(senderId);
            _studentExchangeDataContext.Announcements.Add(announcement);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<BuyItemAnnouncement> GetBuyItemAnnouncementsByReceiverId(string receiverId)
        {
            var user = GetUserById(receiverId);
            return _studentExchangeDataContext.Announcements.OfType<BuyItemAnnouncement>().Include(a => a.Receivers)
                .ToList().FindAll(a => a.Receivers.Contains(user));
        }

        public BuyItemAnnouncement GetBuyItemAnnouncementById(long id)
        {
            return _studentExchangeDataContext.Announcements.OfType<BuyItemAnnouncement>().ToList().Find(a => a.Id == id);
        }

        private ICollection<UserEntity> SetBuyItemAnnouncementReceivers(string senderId)
        {
            var senderCourses = _courseOfStudyService.GetStudentCourses(senderId);
            var receivers = new List<UserEntity>();
            foreach (var course in senderCourses)
            {
                var sameCourses = _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Groups)
                    .Include(c => c.Faculty).ToList().FindAll(c => c.Faculty.Id == course.Faculty.Id && c.Name == course.Name);
                var yearFilteredCourses = sameCourses.Where(c => int.Parse(c.StartYear) <= int.Parse(course.StartYear));
                foreach (var fcourse in yearFilteredCourses)
                {
                    foreach (var group in fcourse.Groups)
                    {
                        receivers.AddRange(group.Students);
                    }
                }
            }
            receivers.RemoveAll(r => r.Id == senderId);
            return receivers.Distinct().ToList();
        }

        public void AddSellItemAnnouncement(SellItemAnnouncement announcement, string senderId)
        {
            announcement.Sender = _studentExchangeDataContext.Users.Find(senderId);
            announcement.IssueDate = DateTime.Now;
            announcement.Receivers = SetSellItemAnnouncementReceivers(senderId);
            _studentExchangeDataContext.Announcements.Add(announcement);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<SellItemAnnouncement> GetSellItemAnnouncementsByReceiverId(string receiverId)
        {
            var user = GetUserById(receiverId);
            return _studentExchangeDataContext.Announcements.OfType<SellItemAnnouncement>().Include(a => a.Receivers)
                .ToList().FindAll(a => a.Receivers.Contains(user));
        }

        public SellItemAnnouncement GetSellItemAnnouncementById(long id)
        {
            return _studentExchangeDataContext.Announcements.OfType<SellItemAnnouncement>().ToList().Find(a => a.Id == id);
        }

        private ICollection<UserEntity> SetSellItemAnnouncementReceivers(string senderId)
        {
            var senderCourses = _courseOfStudyService.GetStudentCourses(senderId);
            var receivers = new List<UserEntity>();
            foreach (var course in senderCourses)
            {
                var sameCourses = _studentExchangeDataContext.CoursesOfStudy.Include(c => c.Groups)
                    .Include(c => c.Faculty).ToList().FindAll(c => c.Faculty.Id == course.Faculty.Id && c.Name == course.Name);
                var yearFilteredCourses = sameCourses.Where(c => int.Parse(c.StartYear) >= int.Parse(course.StartYear));
                foreach (var fcourse in yearFilteredCourses)
                {
                    foreach (var group in fcourse.Groups)
                    {
                        receivers.AddRange(group.Students);
                    }
                }
            }
            receivers.RemoveAll(r => r.Id == senderId);
            return receivers.Distinct().ToList();
        }

        public ICollection<ItemImage> GetAnnouncementImages(long announcementId)
        {
            try
            {
                return _studentExchangeDataContext.Announcements.Find(announcementId).Images;
            }
            catch (NullReferenceException)
            {
                return new List<ItemImage>();
            }
        }

        public void ChangeStatus(long announcementId)
        {
            _studentExchangeDataContext.Announcements.Find(announcementId).CurrentStatus = Announcement.Status.NotActive;          
            _studentExchangeDataContext.SaveChanges();
        }

        public void AddAnnouncementImage(ItemImage image, long announcementId)
        {
            try
            {
                _studentExchangeDataContext.Announcements.Find(announcementId).Images.Add(image);
            }
            catch (NullReferenceException)
            {
                var newList = new List<ItemImage>()
                {
                    image
                };
                _studentExchangeDataContext.Announcements.Find(announcementId).Images = newList;
            }
            finally
            {
                _studentExchangeDataContext.SaveChanges();
            }
        }

        

        public void AddTakePrivateLessonsAnnouncement(TakePrivateLessonsAnnouncement announcement, string senderId)
        {
            announcement.Sender = _studentExchangeDataContext.Users.Find(senderId);
            announcement.IssueDate = DateTime.Now;
            announcement.Receivers = SetPrivateLessonsAnnouncementReceivers(senderId);
            _studentExchangeDataContext.Announcements.Add(announcement);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<TakePrivateLessonsAnnouncement> GetTakePrivateLessonsAnnouncementsByReceiverId(string receiverId)
        {
            var user = GetUserById(receiverId);
            return _studentExchangeDataContext.Announcements.OfType<TakePrivateLessonsAnnouncement>().Include(a => a.Receivers)
                .ToList().FindAll(a => a.Receivers.Contains(user));
        }

        public TakePrivateLessonsAnnouncement GetTakePrivateLessonsAnnouncementById(long id)
        {
            return _studentExchangeDataContext.Announcements.OfType<TakePrivateLessonsAnnouncement>().ToList().Find(a => a.Id == id);
        }

        public void AddGivePrivateLessonsAnnouncement(GivePrivateLessonsAnnouncement announcement, string senderId)
        {
            announcement.Sender = _studentExchangeDataContext.Users.Find(senderId);
            announcement.IssueDate = DateTime.Now;
            announcement.Receivers = SetPrivateLessonsAnnouncementReceivers(senderId);
            _studentExchangeDataContext.Announcements.Add(announcement);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<GivePrivateLessonsAnnouncement> GetGivePrivateLessonsAnnouncementsByReceiverId(string receiverId)
        {
            var user = GetUserById(receiverId);
            return _studentExchangeDataContext.Announcements.OfType<GivePrivateLessonsAnnouncement>().Include(a => a.Receivers)
                .ToList().FindAll(a => a.Receivers.Contains(user));
        }

        public GivePrivateLessonsAnnouncement GetGivePrivateLessonsAnnouncementById(long id)
        {
            return _studentExchangeDataContext.Announcements.OfType<GivePrivateLessonsAnnouncement>().ToList().Find(a => a.Id == id);
        }

        private ICollection<UserEntity> SetPrivateLessonsAnnouncementReceivers(string senderId)
        {
            var senderUniversities = _universityService.GetStudentUniversities(senderId);
            var students = new List<Student>();
            foreach (var university in senderUniversities)
            {
                students.AddRange(_universityService.GetUniversityStudents(university.Id));
            }
            students.RemoveAll(s => s.Id == senderId);
            return students.Distinct().Cast<UserEntity>().ToList();
        }

        public void AddChangeGroupAnnouncement(ChangeGroupAnnouncement announcement, string senderId)
        {
            var newAnnouncement = new ChangeGroupAnnouncement()
            {
                Content = announcement.Content,
                CurrentStatus = announcement.CurrentStatus,
                DestinationGroup = announcement.DestinationGroup,
                ExpirationDate = announcement.ExpirationDate,
                FromGroup = announcement.FromGroup,
                Images = announcement.Images,
                IssueDate = announcement.IssueDate,
                Sender = _studentExchangeDataContext.Users.ToList().Find(s => s.Id == senderId),
                Receivers = _studentExchangeDataContext.Groups.Include(g => g.Students).ToList().Find(g => g.Id == announcement.DestinationGroup.Id).Students.Cast<UserEntity>().ToList()
            };

            _studentExchangeDataContext.Announcements.Add(newAnnouncement);
            _studentExchangeDataContext.SaveChanges();
        }

        public ICollection<ChangeGroupAnnouncement> GetChangeGroupAnnouncementsByReceiverId(string receiverId)
        {
            var user = GetUserById(receiverId);
            return _studentExchangeDataContext.Announcements.OfType<ChangeGroupAnnouncement>().Include(a => a.Receivers)
                .ToList().FindAll(a => a.Receivers.Contains(user));
        }

        public ChangeGroupAnnouncement GetChangeGroupAnnouncementById(long id)
        {
            return _studentExchangeDataContext.Announcements.OfType<ChangeGroupAnnouncement>().ToList()
                .Find(a => a.Id == id);
        }

        private UserEntity GetUserById(string userId)
        {
            return _studentExchangeDataContext.Users.ToList().Find(u => u.Id == userId);
        }
    }
}