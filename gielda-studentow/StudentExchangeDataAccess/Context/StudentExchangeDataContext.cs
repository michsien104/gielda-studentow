using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentExchangeDataAccess.Entity;
using StudentExchangeDataAccess.Initializer;

namespace StudentExchangeDataAccess.Context
{

    public class StudentExchangeDataContext : IdentityDbContext<UserEntity>, IStudentExchangeDataContext
    {

        public StudentExchangeDataContext() : base("Server=LENOVO-PC\\SIENMICH;Database=gs;User Id=root;Password=root;")
        {
            Database.SetInitializer<StudentExchangeDataContext>(new MyInitializer());
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Groups)
                .WithMany(r => r.Students)
                .Map(m =>
                {
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("GroupId");
                    m.ToTable("StudentGroups");
                });
            modelBuilder.Entity<Announcement>()
                .HasMany(a => a.Receivers)
                .WithMany(r => r.ReceivedAnnouncements)
                .Map(m =>
                {
                    m.MapLeftKey("AnnouncementId");
                    m.MapRightKey("UserId");
                    m.ToTable("AnnouncementReceivers");
                });
            modelBuilder.Entity<Message>()
                .HasMany(m => m.Receivers)
                .WithMany(s => s.ReceivedMessages)
                .Map(m =>
                {
                    m.MapLeftKey("MessageId");
                    m.MapRightKey("StudentId");
                    m.ToTable("MessageReceivers");
                });
            modelBuilder.Entity<Tutor>()
                .HasMany(t => t.TutorCoursesOfStudy)
                .WithMany(c => c.Tutors)
                .Map(m =>
                {
                    m.MapLeftKey("TutorId");
                    m.MapRightKey("CourseOfStudyId");
                    m.ToTable("TutorsCoursesOfStudy");
                });
            modelBuilder.Entity<University>()
                .HasMany(u => u.Administrators)
                .WithMany(a => a.AdministratedUniversities)
                .Map(m =>
                {
                    m.MapLeftKey("UniversityId");
                    m.MapRightKey("AdministratorId");
                    m.ToTable("UniversityAdministrators");
                });
            modelBuilder.Entity<Faculty>()
                .HasMany(u => u.Administrators)
                .WithMany(a => a.AdministratedFaculties)
                .Map(m =>
                {
                    m.MapLeftKey("FacultyId");
                    m.MapRightKey("AdministratorId");
                    m.ToTable("FacultyAdministrators");
                });
            modelBuilder.Entity<CourseOfStudy>()
                .HasMany(u => u.Administrators)
                .WithMany(a => a.AdministratedCourses)
                .Map(m =>
                {
                    m.MapLeftKey("CourseOfStudyId");
                    m.MapRightKey("AdministratorId");
                    m.ToTable("CourseOfStudyAdministrators");
                });
            modelBuilder.Entity<Group>()
                .HasMany(u => u.Administrators)
                .WithMany(a => a.AdministratedGroups)
                .Map(m =>
                {
                    m.MapLeftKey("GroupId");
                    m.MapRightKey("AdministratorId");
                    m.ToTable("GroupAdministrators");
                });

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<CourseOfStudy> CoursesOfStudy { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<University> Universities { get; set; }

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<ItemImage> ItemImages { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        public UserEntity GetUserById(string id)
        {
            return Users.ToList().Find(u => u.Id == id);
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}