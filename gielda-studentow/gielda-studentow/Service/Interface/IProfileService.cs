using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IProfileService
    {
        UserEntity GetUserById(string id);
        void ChangeUserAvatar(string avatarUrl, string userId);
        void ChangeUsername(string newUsername, string userId);
        void ChangePassword(string password, string userId);
        bool IsStudent(string profileId);
        bool IsTutor(string profileId);
    }
}