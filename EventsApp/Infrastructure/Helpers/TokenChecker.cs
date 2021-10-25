using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsApp.Models;
using EventsApp.Service;

namespace EventsApp.Infrastructure.Helpers
{
    public class TokenChecker
    {
        private readonly ITokenManagement _tokenManager;

        public TokenChecker(ITokenManagement tokenmanager)
        {
            _tokenManager = tokenmanager;
        }
        public async Task<Token> Check(UserIdentity currentUser)
        {
            if (currentUser != null)
                return await _tokenManager.GetToken(currentUser.Id);

            return await _tokenManager.GetToken("");
        }
    }
}
