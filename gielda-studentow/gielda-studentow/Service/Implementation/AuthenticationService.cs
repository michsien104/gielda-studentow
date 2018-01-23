using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gielda_studentow.Service.Implementation;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Context;

namespace gielda_studentow.Service.Implementation
{
    public class AuthenticationService : IAuthenticatonService
    {
        private readonly IStudentExchangeDataContext _studentExchangeDataContext;

        public AuthenticationService(IStudentExchangeDataContext studentExchangeDataContext)
        {
            _studentExchangeDataContext = studentExchangeDataContext;
        }

        public string GetUserIdByUsername(string username)
        {
            return _studentExchangeDataContext.Users.ToList().Find(a => a.UserName == username).Id;
        }
    }
}