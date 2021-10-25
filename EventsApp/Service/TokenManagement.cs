using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client;
using Client.Models;
using EventsApp.Infrastructure.Helpers;
using EventsApp.Models;
using EventsApp.Models.Account;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace EventsApp.Service
{
    public class TokenManagement : ITokenManagement
    {
        private List<Token> tokens = new List<Token>();
        private readonly IConfiguration _configuration;
        private readonly IClientService _clientService;

        public TokenManagement(IConfiguration configuration, IClientService clientService)
        {
            _configuration = configuration;
            _clientService = clientService;
        }
        
        public async Task<Token> GetToken(string userId)
        {
            var check = tokens.Find(x => x.UserId == userId);
            if (check == null)
            {
                return await GetDefaultUser();
            }
            return check;
        }

        public void AddToken(Token token)
        {
            tokens.Add(token);
        }

        private async Task<Token> GetDefaultUser()
        {
            LoginViewModel login = new LoginViewModel {Password = "Qwerty", UserName = "sparta" };

            var token = await _clientService.Authenticate(_configuration.GetConnectionString("ApiUrl"), "auth", login.Adapt<Credentials>());
            return new Token {UserId = "", UserToken = token};
        }
    }
}
