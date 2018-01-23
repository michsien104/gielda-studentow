using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace StudentExchangeDataAccess.Context
{
    public interface IStudentExchangeDataContext : IDisposable
    {
        IDbSet<UserEntity> Users { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<CourseOfStudy> CoursesOfStudy { get; set; }
        DbSet<Faculty> Faculties { get; set; }
        DbSet<University> Universities { get; set; }

        DbSet<Announcement> Announcements { get; set; }
        DbSet<ItemImage> ItemImages { get; set; }
        DbSet<Message> Messages { get; set; }

        UserEntity GetUserById(string id);
        int SaveChanges();
    }
}
