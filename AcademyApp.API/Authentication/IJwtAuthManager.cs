using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyApp.API.Authentication
{
    public interface IJwtAuthManager
    {
        Task<string> Authenticate(string userName, string password);
    }
}
