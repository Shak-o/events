using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.API.Authentication;
using AcademyApp.API.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;

namespace AcademyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthManager _authManager;
        public AuthController(IJwtAuthManager authManager)
        {
            _authManager = authManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(Credentials creds)
        {
            var result = await _authManager.Authenticate(creds.UserName, creds.Password);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        } 
    }
}
