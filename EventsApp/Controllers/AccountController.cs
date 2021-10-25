using EventsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client;
using Client.Models;
using EventsApp.Infrastructure.Helpers;
using EventsApp.Models.Account;
using EventsApp.Service;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EventsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly ITokenManagement _tokenManager;
        private readonly IConfiguration _configuration;
        private readonly IClientService _clientService;
        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, ITokenManagement tokenManager, IConfiguration configuration, IClientService clientService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
            _configuration = configuration;
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {

            var identityUser = await _userManager.FindByNameAsync(login.UserName);
            if (identityUser.Equals(null))
                return NotFound();

            var checkPassword = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, login.Password);

            if (checkPassword.Equals(PasswordVerificationResult.Failed))
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(identityUser.UserName, login.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var resultToken = await _clientService.Authenticate(_configuration.GetConnectionString("ApiUrl"), "auth", login.Adapt<Credentials>());
                _tokenManager.AddToken(new Token { UserId = identityUser.Id, UserToken = resultToken });
                return Redirect("/Event");

            }

            return Redirect("/");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationViewModel user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var convert = user.Adapt<UserIdentity>();
            var regResult = await _userManager.CreateAsync(convert, user.Password);
            if (regResult.Succeeded)
            {
                return Redirect("/");
            }

            return BadRequest(regResult);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

    }
}
