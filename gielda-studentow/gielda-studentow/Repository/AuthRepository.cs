using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentExchangeDataAccess.Context;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Repository
{
    public class AuthRepository : IDisposable
    {
        private readonly StudentExchangeDataContext _studentExchangeDataContext;

        private readonly UserManager<UserEntity> _userManager;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<Tutor> _tutorManager;

        public AuthRepository()
        {
            _studentExchangeDataContext = new StudentExchangeDataContext();
            _userManager = new UserManager<UserEntity>(new UserStore<UserEntity>(_studentExchangeDataContext));
            _studentManager = new UserManager<Student>(new UserStore<Student>(_studentExchangeDataContext));
            _tutorManager = new UserManager<Tutor>(new UserStore<Tutor>(_studentExchangeDataContext));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            UserEntity user = new UserEntity()
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityResult> RegisterTutor(UserModel userModel)
        {
            Tutor tutor = new Tutor()
            {
                UserName = userModel.UserName,
            };
            var result = await _tutorManager.CreateAsync(tutor, userModel.Password);
            return result;
        }

        public async Task<IdentityResult> RegisterStudent(UserModel userModel)
        {
            Student student = new Student()
            {
                UserName = userModel.UserName,

            };
            var result = await _studentManager.CreateAsync(student, userModel.Password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _studentExchangeDataContext.Dispose();
            _userManager.Dispose();

        }
    }
}