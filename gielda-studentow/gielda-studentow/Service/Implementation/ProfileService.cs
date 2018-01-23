using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using gielda_studentow.Service.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Implementation
{
    public class ProfileService : IProfileService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;

        public ProfileService(IStudentExchangeDataContext studentExchangeDataContext)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
        }

        public UserEntity GetUserById(string id)
        {
            return _studentExchangeDataContext.Users.ToList().Find(u => u.Id == id);
        }

        public void ChangeUserAvatar(string avatarUrl, string userId)
        {
            GetUserById(userId).AvatarUrl = avatarUrl;
            _studentExchangeDataContext.SaveChanges();
        }

        public void ChangeUsername(string newUsername, string userId)
        {
            if(_studentExchangeDataContext.Users.ToList().Find(u => u.UserName == newUsername) != null)
                throw new Exception("Username already in use");
            _studentExchangeDataContext.Users.ToList().Find(u => u.Id == userId).UserName = newUsername;
            _studentExchangeDataContext.SaveChanges();
        }

        public void ChangePassword(string password, string userId)
        {
            var userManager = new UserManager<UserEntity>(new UserStore<UserEntity>((DbContext)_studentExchangeDataContext));
            var user = _studentExchangeDataContext.Users.ToList().Find(u => u.Id == userId);
            user.PasswordHash = userManager.PasswordHasher.HashPassword(password);
            _studentExchangeDataContext.SaveChanges();
        }

        public bool IsStudent(string profileId)
        {
            var student = (from s in _studentExchangeDataContext.Users.OfType<Student>() where s.Id == profileId select s).ToList();
            return student.Count > 0;
        }

        public bool IsTutor(string profileId)
        {
            var student = (from s in _studentExchangeDataContext.Users.OfType<Tutor>() where s.Id == profileId select s).ToList();
            return student.Count > 0;
        }
    }
}