using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gielda_studentow.Service.Interface
{
    public interface IAuthenticatonService
    {
        string GetUserIdByUsername(string username);
    }
}
