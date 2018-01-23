using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace StudentExchangeDataAccess.Initializer
{
    class MyInitializer : DropCreateDatabaseAlways<StudentExchangeDataContext>
    {
        public MyInitializer()
        {
        }

        public override void InitializeDatabase(StudentExchangeDataContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(StudentExchangeDataContext context)
        {
            var userManager = new UserManager<UserEntity>(new UserStore<UserEntity>(context));
            var studentList= new List<Student>();
            var student1 = new Student()
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                UserName = "akowalski",
                PhoneNumber = "123456789",
                Email = "akowalski@agh.pl",
                PasswordHash = userManager.PasswordHasher.HashPassword("abc123"),
                AvatarUrl = "http://i.imgur.com/wlf4fRX.jpg"
            };

            studentList.Add(student1);
            var university1 = new University()
            {
                Name = "Akademia Gorniczo-Hutnicza",
                ShortName = "AGH",
            };
            context.Universities.Add(university1);
            var faculty1 = new Faculty()
            {
                Name = "Informatyki, Elektroniki i Telekomunikacji",
                ShortName = "IEiT",
                University = university1
            };
            var faculty2 = new Faculty()
            {
                Name = "Elektrotechniki, Automatyki, Informatyki i Inżynierii Biomedycznej",
                ShortName = "EAIiIB",
                University = university1
            };
            context.Faculties.Add(faculty1);
            context.Faculties.Add(faculty2);
            var courseOfStudy1 = new CourseOfStudy()
            {
                Name = "Informatyka",
                Faculty = faculty1,
                RepresentativeStudent = student1,
                StartYear = "2015"
            };
            var courseOfStudy2 = new CourseOfStudy()
            {
                Name = "Automatyka i Robotyka",
                Faculty = faculty2,
                RepresentativeStudent = student1,
                StartYear = "2014"
            };
            context.CoursesOfStudy.Add(courseOfStudy1);
            context.CoursesOfStudy.Add(courseOfStudy2);
            var groupList1 = new List<Group>();
            var group1 = new Group()
            {
                Name = "1a",
                CourseOfStudy = courseOfStudy1
            };
            groupList1.Add(group1);
            var group2 = new Group()
            {
                Name = "1b",
                CourseOfStudy = courseOfStudy1
            };
            groupList1.Add(group2);
            context.Groups.AddRange(groupList1);
            student1.Groups = groupList1;

            var groupList2 = new List<Group>();
            var group3 = new Group()
            {
                Name = "1",
                CourseOfStudy = courseOfStudy2
            };

            student1.Groups.Add(group3);

            groupList2.Add(group3);
            var group4 = new Group()
            {
                Name = "2",
                CourseOfStudy = courseOfStudy2
            };
            groupList2.Add(group4);
            context.Groups.AddRange(groupList2);

            var admUniversities = new List<University>()
            {
                university1
            };
            student1.AdministratedUniversities = admUniversities;

            var admFaculties = new List<Faculty>()
            {
                faculty1
            };
            student1.AdministratedFaculties = admFaculties;

            var admCourses = new List<CourseOfStudy>()
            {
                courseOfStudy1
            };
            student1.AdministratedCourses = admCourses;

            var admGroups = new List<Group>()
            {
                group1,
                group2
            };
            student1.AdministratedGroups = admGroups;

            context.Users.Add(student1);

            var announcement1 = new SellItemAnnouncement()
            {
                Sender = student1,
                Category = "Podrecznik",
                Title = "Sprzedam podrecznik",
                Content = "Sprzedam podrecznik do angielskiego",
                CurrentStatus = Announcement.Status.Active,
                Price = new decimal(25.99),
                Receivers = studentList.Cast<UserEntity>().ToList(),
                IssueDate = new DateTime(2017,1,1),
                ExpirationDate = new DateTime(2020,1,1)
            };
            context.Announcements.Add(announcement1);

            var announcement2 = new BuyItemAnnouncement()
            {
                Sender = student1,
                Category = "Dlugopis",
                Title = "Sprzedam długopis",
                Content = "Sprzedam długopis",
                CurrentStatus = Announcement.Status.Active,
                Price = new decimal(10.99),
                Receivers = studentList.Cast<UserEntity>().ToList(),
                IssueDate = new DateTime(2017, 2, 2),
                ExpirationDate = new DateTime(2021, 1, 1)
            };
            context.Announcements.Add(announcement2);

            var announcementImage1 = new ItemImage()
            {
                Announcement = announcement1,
                Url = "http://smartmobilestudio.com/wp-content/uploads/2012/06/leather-book-preview.png"
            };
            context.ItemImages.Add(announcementImage1);
            var announcementImage2 = new ItemImage()
            {
                Announcement = announcement1,
                Url = "http://www.nekretnine-interijeri.com/wp-content/uploads/2017/02/book.jpg"
            };
            context.ItemImages.Add(announcementImage2);

            base.Seed(context);
        }
    }
}
