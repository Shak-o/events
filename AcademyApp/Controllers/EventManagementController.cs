using AcademyApp.Models;
using AcademyApp.Models.Event;
using AcademyApp.Service;
using Client;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyApp.Controllers
{
    [Authorize(Policy = "Admin")]
    public class EventManagementController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ITokenManagement _tokenManager;
        private readonly IClientService _clientService;

        public EventManagementController(IConfiguration configuration, UserManager<UserIdentity> userManager, ITokenManagement tokenManager, IClientService clientService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenManager = tokenManager;
            _clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await _tokenManager.GetToken(currentUser.Id);
            var result =
                await _clientService.GetAll(token.userToken, _configuration.GetConnectionString("ApiUrl"), "event");
            return View(result.Adapt<List<EventViewModel>>());
        }

        public async Task<IActionResult> SetPublished(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await _tokenManager.GetToken(currentUser.Id);
            await _clientService.SetAlive(token.userToken, _configuration.GetConnectionString("ApiUrl"), "event", id);
            return Redirect("/eventmanagement");
        }
        [HttpGet("{id}")]
        public IActionResult SetEndDate(int id)
        {
            return View();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SetEndDate(int id, EndDateViewModel endDate)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await _tokenManager.GetToken(currentUser.Id);
            await _clientService.SetEndDate(token.userToken, _configuration.GetConnectionString("ApiUrl"), "setEnd", id,
                endDate.Date);
            return Redirect("/eventmanagement");
        }
    }
}
