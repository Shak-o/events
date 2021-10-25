using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models;

namespace AcademyApp.Service
{
    public class TokenManagement : ITokenManagement
    {
        private List<Token> tokens = new List<Token>();
        public async Task<Token> GetToken(string UserId)
        {
            return tokens.First(x => x.userId == UserId);
        }

        public void AddToken(Token token)
        {
            tokens.Add(token);
        }
    }
}
