using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gielda_studentow.Service.Implementation;
using gielda_studentow.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class ProfileServiceTests
    {
        private Mock<IStudentExchangeDataContext> _mockStudentExchangeDataContext;
        private IProfileService _profileService;

        private IQueryable<Faculty> _facultySet;
        private IQueryable<University> _universitySet;

        private ICollection<Student> _studentList;
        private IQueryable<CourseOfStudy> _courseOfStudySet;
        private IQueryable<Group> _groupSet;
        private IQueryable<UserEntity> _userSet;

        [TestInitialize]
        public void BeforeEach()
        {
            InitializeFields();
            ProvideTestData();
        }

        private void InitializeFields()
        {
            _mockStudentExchangeDataContext = new Mock<IStudentExchangeDataContext>();
            _profileService = new ProfileService(_mockStudentExchangeDataContext.Object);
        }

        private void ProvideTestData()
        {
            var university1 = new University()
            {
                Id = 1L,
                Name = "Uniwersytet Jagielloński",
                ShortName = "UJ"
            };
            var faculty1 = new Faculty()
            {
                Id = 1L,
                Name = "Wydział Prawa",
                ShortName = "WP",
                University = university1
            };
            university1.Faculties = new List<Faculty>()
            {
                faculty1
            };

            _studentList = new List<Student>();
            var student1 = new Student()
            {
                Id = "testId",
                FirstName = "Andrzej",
                LastName = "Kowalski",
                UserName = "akowalski",
                PhoneNumber = "123456789",
                Email = "akowalski@agh.pl",
                PasswordHash = "abc123",
                AvatarUrl = "http://i.imgur.com/wlf4fRX.jpg"
            };
            _studentList.Add(student1);
            _userSet = _studentList.AsQueryable();

            var courseOfStudy1 = new CourseOfStudy()
            {
                Id = 1L,
                Name = "Informatyka",
                Faculty = faculty1,
                RepresentativeStudent = student1,
                StartYear = "2015"
            };
            var courseOfStudy2 = new CourseOfStudy()
            {
                Id = 2L,
                Name = "Automatyka i Robotyka",
                Faculty = faculty1,
                RepresentativeStudent = student1,
                StartYear = "2014"
            };

            var groupList1 = new List<Group>();
            var group1 = new Group()
            {
                Id = 1L,
                Name = "1a",
                CourseOfStudy = courseOfStudy1,
                Students = new List<Student>()
                {
                    student1
                }
            };
            groupList1.Add(group1);
            var group2 = new Group()
            {
                Id = 2L,
                Name = "1b",
                CourseOfStudy = courseOfStudy1,
                Students = new List<Student>()
                {
                    student1
                }
            };
            groupList1.Add(group2);
            courseOfStudy1.Groups = groupList1;
            student1.Groups = groupList1;

            var groupList2 = new List<Group>();
            var group3 = new Group()
            {
                Id = 3L,
                Name = "1",
                CourseOfStudy = courseOfStudy2,
                Students = new List<Student>()
                {
                    student1
                }
            };

            student1.Groups.Add(group3);

            groupList2.Add(group3);
            var group4 = new Group()
            {
                Id = 4L,
                Name = "2",
                CourseOfStudy = courseOfStudy2,
                Students = new List<Student>()
                {
                    student1
                }
            };
            groupList2.Add(group4);
            courseOfStudy2.Groups = groupList2;

            _universitySet = new List<University>()
            {
                university1
            }.AsQueryable();
            _facultySet = new List<Faculty>()
            {
                faculty1
            }.AsQueryable();
            university1.Faculties = _facultySet.ToList();
            _courseOfStudySet = new List<CourseOfStudy>()
            {
                courseOfStudy1,
                courseOfStudy2
            }.AsQueryable();
            faculty1.CoursesOfStudy = _courseOfStudySet.ToList();
            _groupSet = new List<Group>()
            {
                group1,
                group2,
                group3,
                group4
            }.AsQueryable();
        }

        private Mock<DbSet<Faculty>> CreateMockFacultySet()
        {
            var mockFacultySet = new Mock<DbSet<Faculty>>();
            mockFacultySet.As<IQueryable<Faculty>>().Setup(m => m.Provider).Returns(_facultySet.Provider);
            mockFacultySet.As<IQueryable<Faculty>>().Setup(m => m.ElementType).Returns(_facultySet.ElementType);
            mockFacultySet.As<IQueryable<Faculty>>().Setup(m => m.Expression).Returns(_facultySet.Expression);
            mockFacultySet.As<IQueryable<Faculty>>().Setup(m => m.GetEnumerator())
                .Returns(_facultySet.GetEnumerator());
            return mockFacultySet;
        }

        private Mock<DbSet<CourseOfStudy>> CreateMockCoursesOfStudySet()
        {
            var mockCoursesOfStudySet = new Mock<DbSet<CourseOfStudy>>();
            mockCoursesOfStudySet.As<IQueryable<CourseOfStudy>>().Setup(m => m.Provider).Returns(_courseOfStudySet.Provider);
            mockCoursesOfStudySet.As<IQueryable<CourseOfStudy>>().Setup(m => m.ElementType)
                .Returns(_courseOfStudySet.ElementType);
            mockCoursesOfStudySet.As<IQueryable<CourseOfStudy>>().Setup(m => m.Expression).Returns(_courseOfStudySet.Expression);
            mockCoursesOfStudySet.As<IQueryable<CourseOfStudy>>().Setup(m => m.GetEnumerator())
                .Returns(_courseOfStudySet.GetEnumerator());
            return mockCoursesOfStudySet;
        }

        private Mock<DbSet<UserEntity>> CreateMockUserSet()
        {
            var mockUserSet = new Mock<DbSet<UserEntity>>();
            mockUserSet.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(_userSet.Provider);
            mockUserSet.As<IQueryable<UserEntity>>().Setup(m => m.ElementType)
                .Returns(_userSet.ElementType);
            mockUserSet.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(_userSet.Expression);
            mockUserSet.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator())
                .Returns(_userSet.GetEnumerator());
            return mockUserSet;
        }

        [TestMethod]
        public void TestGetUserById()
        {
            var mockUserSet = CreateMockUserSet();
            _mockStudentExchangeDataContext.Setup(c => c.Users).Returns(mockUserSet.Object);
            var result = _profileService.GetUserById("testId");
            Assert.AreSame(_userSet.ToList().Find(u => u.Id == "testId"), result);
        }

        [TestMethod]
        public void TestChangeUserAvatar()
        {
            var mockUserSet = CreateMockUserSet();
            _mockStudentExchangeDataContext.Setup(c => c.Users).Returns(mockUserSet.Object);
            _mockStudentExchangeDataContext.Setup(c => c.SaveChanges());
            _profileService.ChangeUserAvatar("www.google.pl","testId");
            _mockStudentExchangeDataContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void TestIsStudent()
        {
            var mockUserSet = CreateMockUserSet();
            mockUserSet.SetReturnsDefault(mockUserSet.Object.Cast<Student>());
            _mockStudentExchangeDataContext.Setup(c => c.Users).Returns(mockUserSet.Object);
            Assert.IsTrue(_profileService.IsStudent("testId"));
        }

        [TestMethod]
        public void TestIsTutor()
        {
            var mockUserSet = CreateMockUserSet();
            mockUserSet.SetReturnsDefault(mockUserSet.Object.Cast<Tutor>());
            _mockStudentExchangeDataContext.Setup(c => c.Users).Returns(mockUserSet.Object);
            Assert.IsFalse(_profileService.IsTutor("testId"));
        }
    }
}
