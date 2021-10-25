using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.API.Infrastructure.Models;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Service;
using AcademyApp.Service.Interfaces;
using AcademyApp.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AcademyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
        private readonly IMapper _mapper;

        public EventController(IEventService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var result = await _service.GetAllEvents();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _service.GetEvent(id);
            return Ok(result);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddEvent
        ///     {
        ///         "userId": "47e8fa6b-1fc8-46e5-9ff4-cd57a59e90ea",
        ///         "title": "testTitleNew",
        ///         "description": "test Description New",
        ///         "startDate": "2021-10-12T00:00:00",
        ///         "endDate": "2021-10-12T00:00:00",
        ///         "editEndDate": "2021-10-12T00:00:00",
        ///         "imgPath": "home",
        ///         "imgName": "test",
        ///         "user": {
        ///             "id": "47e8fa6b-1fc8-46e5-9ff4-cd57a59e90ea",
        ///             "name": "Shako",
        ///             "lastName": "Turashvili"
        ///         },
        ///         "isActive": false,
        ///         "users": [
        ///                     {
        ///                         "id": "3b589afa-c2ea-4084-a582-5a48f86bf6af",
        ///                         "name": "efsf",
        ///                         "lastName": "turashvili4"
        ///                     }
        ///                  ]
        ///        }
        /// </remarks>
        /// <param name="newEvent"></param>
        /// <returns>A newly created TodoItem</returns>
        [HttpPost]
        public async Task<IActionResult> AddEvent(EventPostModel newEvent)
        {
            var convert = _mapper.Map<EventPostModel, EventServiceModel>(newEvent);
            await _service.AddEvent(convert);

            return Ok(convert.Id);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _service.DeleteEvent(id);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(string requesterId, EventPutModel updateEvent)
        {
            var convert = _mapper.Map<EventPutModel, EventServiceModel>(updateEvent);

            await _service.UpdateEvent(requesterId,convert);

            return Ok(updateEvent.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SetActive(int id)
        {
            await _service.SetActive(id);
            var result = await _service.GetEvent(id);
            if (result.IsActive)
                return Ok();

            return BadRequest();

        }

        [HttpPut("/setEnd/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, DateTime date)
        {
            await _service.SetEndDate(id, date);
            return Ok();
        }

        [HttpPut("/addAttendant/{id}")]
        public async Task<IActionResult> AddAttendant(int id, string attendantId)
        {
            await _service.AddAttendant(attendantId, id);
            return Ok();
        }
    }
}
