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

//[TestInitialize] is run before each test. Likewise [TestCleanup] is after each test
//[ClassInitialize] and [ClassCleanup] are run before and after the 'suite' of tests inside the TestClass.

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class UniversityServiceTests
    {
        private Mock<IStudentExchangeDataContext> _mockStudentExchangeDataContext;
        private Mock<IFacultyService> _mockFacultyService;
        private IUniversityService _universityService;

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
            _mockFacultyService = new Mock<IFacultyService>();
            _universityService =
                new UniversityService(_mockStudentExchangeDataContext.Object, _mockFacultyService.Object);
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
                Name = "Informatyka",
                Faculty = faculty1,
                RepresentativeStudent = student1,
                StartYear = "2015"
            };
            var courseOfStudy2 = new CourseOfStudy()
            {
                Name = "Automatyka i Robotyka",
                Faculty = faculty1,
                RepresentativeStudent = student1,
                StartYear = "2014"
            };

            var groupList1 = new List<Group>();
            var group1 = new Group()
            {
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

        [TestCleanup]
        public void AfterEach()
        {
            CleanupFields();
        }

        private void CleanupFields()
        {
            _mockStudentExchangeDataContext = null;
            _universityService = null;
            _mockFacultyService = null;
        }

        [TestMethod]
        public void TestGetFacultyUniversity()
        {
            var mockFacultySet = CreateMockFacultySet();
            var mockUniversitySet = CreateMockUniversitySet();

            _mockStudentExchangeDataContext.Setup(c => c.Faculties).Returns(mockFacultySet.Object);
            _mockStudentExchangeDataContext.Setup(c => c.Universities).Returns(mockUniversitySet.Object);

            Assert.AreSame(_universitySet.ToList().Find(u => u.Id == 1L), _universityService.GetFacultyUniversity(1L));
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

        private Mock<DbSet<University>> CreateMockUniversitySet()
        {
            var mockUniversitySet = new Mock<DbSet<University>>();
            mockUniversitySet.As<IQueryable<University>>().Setup(m => m.Provider).Returns(_universitySet.Provider);
            mockUniversitySet.As<IQueryable<University>>().Setup(m => m.ElementType)
                .Returns(_universitySet.ElementType);
            mockUniversitySet.As<IQueryable<University>>().Setup(m => m.Expression).Returns(_universitySet.Expression);
            mockUniversitySet.As<IQueryable<University>>().Setup(m => m.GetEnumerator())
                .Returns(_universitySet.GetEnumerator);
            return mockUniversitySet;
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

        private Mock<DbSet<Group>> CreateMockGroupSet()
        {
            var mockGroupSet = new Mock<DbSet<Group>>();
            mockGroupSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(_groupSet.Provider);
            mockGroupSet.As<IQueryable<Group>>().Setup(m => m.ElementType)
                .Returns(_groupSet.ElementType);
            mockGroupSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(_groupSet.Expression);
            mockGroupSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator())
                .Returns(_groupSet.GetEnumerator());
            return mockGroupSet;
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
        public void TestGetUniversityFaculties()
        {
            var mockUniversitySet = CreateMockUniversitySet();
            mockUniversitySet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockUniversitySet.Object);
            _mockStudentExchangeDataContext.Setup(c => c.Universities).Returns(mockUniversitySet.Object);
            var result = _universityService.GetUniversityFaculties(1L);
            Assert.IsTrue(_universitySet.ToList().Find(u => u.Id == 1L).Faculties.Intersect(result).Count() ==
                          result.Count);
        }

        [TestMethod]
        public void TestGetUniversityStudents()
        {
            var mockUniversitySet = CreateMockUniversitySet();
            mockUniversitySet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockUniversitySet.Object);
            _mockStudentExchangeDataContext.Setup(c => c.Universities).Returns(mockUniversitySet.Object);
            _mockFacultyService.Setup(m => m.GetFacultyStudents(It.IsAny<long>())).Returns(_studentList);
            var result = _universityService.GetUniversityStudents(1L);
            Assert.IsTrue(_studentList.Intersect(result).Count() == result.Count);
        }

        [TestMethod]
        public void TestGetStudentUniverities()
        {
            var mockGroupSet = CreateMockGroupSet();
            _mockStudentExchangeDataContext.Setup(c => c.Groups).Returns(mockGroupSet.Object);
            _mockStudentExchangeDataContext.Setup(c => c.GetUserById(It.IsAny<string>())).Returns(_studentList.ToList()
                .Find(u => u.LastName == "Kowalski" && u.FirstName == "Andrzej"));
            var mockCoursesOfStudySet = CreateMockCoursesOfStudySet();
            _mockStudentExchangeDataContext.Setup(c => c.CoursesOfStudy).Returns(mockCoursesOfStudySet.Object);
            var mockFacultiesSet = CreateMockFacultySet();
            _mockStudentExchangeDataContext.Setup(c => c.Faculties).Returns(mockFacultiesSet.Object);
            var mockUniversitySet = CreateMockUniversitySet();
            _mockStudentExchangeDataContext.Setup(c => c.Universities).Returns(mockUniversitySet.Object);

            var result = _universityService.GetStudentUniversities("any_id");
            Assert.IsTrue(_universitySet.ToList().Intersect(result).Count() == result.Count);
        }
    }
}