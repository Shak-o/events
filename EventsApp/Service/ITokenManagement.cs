using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsApp.Models;

namespace EventsApp.Service
{
    public interface ITokenManagement
    {
        Task<Token> GetToken(string userId);
        void AddToken(Token token);
    }
}
