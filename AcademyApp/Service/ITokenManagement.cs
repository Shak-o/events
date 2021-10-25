using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models;

namespace AcademyApp.Service
{
    public interface ITokenManagement
    {
        Task<Token> GetToken(string userId);
        void AddToken(Token token);
    }
}
