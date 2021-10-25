using Client;
using EventsApp.Infrastructure.Helpers;
using EventsApp.Models;
using EventsApp.Models.Event;
using EventsApp.Models.Event.Requests;
using EventsApp.Service;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace EventsApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ITokenManagement _tokenManager;
        private readonly IClientService _clientService;

        public EventController(IConfiguration configuration, UserManager<UserIdentity> userManager, ITokenManagement tokenManager, IClientService clientService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenManager = tokenManager;
            _clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            var events = await _clientService.GetAll(token.UserToken, _configuration.GetConnectionString("ApiUrl"), "event");
            var converted = events.Adapt<List<EventViewModel>>();
            ValidateEvents.Validate(converted, currentUser);

            return View(converted);
        }
        public async Task<IActionResult> Archive()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            var events = await _clientService.GetAll(token.UserToken, _configuration.GetConnectionString("ApiUrl"), "archive");
            var converted = events.Adapt<List<EventViewModel>>();
            ValidateEvents.Validate(converted, currentUser);
            
            return View(converted);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequest newEvent)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            newEvent.UserId = currentUser.Id;

            await _clientService.AddEvent(newEvent.Adapt<EventModel>(), token.UserToken, _configuration.GetConnectionString("ApiUrl"), "event");

            return Redirect("/event");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            var events = await _clientService.GetOne(token.UserToken, _configuration.GetConnectionString("ApiUrl"), "event", id);
            var converted = events.Adapt<UpdateRequest>();

            return View(converted);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequest updateView)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            updateView.UserId = currentUser.Id;
            updateView.Id = id;
            await _clientService.UpdateEvent(updateView.Adapt<EventModel>(), token.UserToken, _configuration.GetConnectionString("ApiUrl"), "event",currentUser.Id);
            return Redirect("/Event");
        }

        public async Task<IActionResult> Details(int id, string dest)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            var events = await _clientService.GetOne(token.UserToken, _configuration.GetConnectionString("ApiUrl"), dest, id);
            var converted = events.Adapt<EventViewModel>();

            return View(converted);
        }

        [HttpGet("/addAttendant/{id}")]
        public async Task<IActionResult> AddAttendant(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var token = await new TokenChecker(_tokenManager).Check(currentUser);
            await _clientService.AddAttendant(token.UserToken, _configuration.GetConnectionString("ApiUrl"), "addAttendant",
                id, currentUser.Id);
           
            return Redirect("/event");
        }

    }
}
