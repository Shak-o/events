using AcademyApp.Infrastructure;
using AcademyApp.Models;
using AcademyApp.Models.Account;
using AcademyApp.Service;
using Client;
using Client.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenManagement _tokenManager;
        private readonly IClientService _clientService;

        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ITokenManagement tokenManager, IClientService clientService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenManager = tokenManager;
            _clientService = clientService;
        }

        [AllowAnonymous]
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
                var resultToken = await _clientService.Authenticate(_configuration.GetConnectionString("ApiUrl"),
                    "auth", login.Adapt<Credentials>());
                _tokenManager.AddToken(new Token { userId = identityUser.Id, userToken = resultToken });
                return Redirect("/account/ManageUsers");
            }

            return BadRequest(result);
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
                return View();
            }

            var convert = user.Adapt<UserIdentity>();
            var regResult = await _userManager.CreateAsync(convert, user.Password);
            if (regResult.Succeeded)
            {
                return Redirect("/account/ManageUsers");
            }

            return View();
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users.Adapt<List<UserManageViewModel>>());
        }


        public async Task<IActionResult> DeleteUser(string UserName)
        {
            var identityUser = await _userManager.FindByNameAsync(UserName);

            if (!identityUser.Equals(null))
            {
                await _userManager.DeleteAsync(identityUser);
                return Redirect("/account/ManageUsers");
            }
            //todo: add error messages
            return Redirect("/");
        }


        public IActionResult ManageRoles()
        {
            //return View();
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(string UserName, string Role)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(Role));
            //var test = await _userManager.AddToRolesAsync(convert, new string[] { "SuperAdmin" });
            throw new NotImplementedException();
        }


        public async Task<IActionResult> Edit(string UserName)
        {
            var identityUser = await _userManager.FindByNameAsync(UserName);
            return View(identityUser.Adapt<UpdateViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateViewModel user)
        {
            if (!user.Equals(null))
            {
                var identityUser = await Helper.GetUpdateableUser(user, _userManager);
                var result = await _userManager.UpdateAsync(identityUser);
            }

            return Redirect("/account/ManageUsers");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
