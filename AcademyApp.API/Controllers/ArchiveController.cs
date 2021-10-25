using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.API.Infrastructure.Models;
using AcademyApp.Service;
using AcademyApp.Service.Interfaces;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveService _service;
        private readonly IMapper _mapper;

        public ArchiveController(IArchiveService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetArchive()
        {
            var result = await _service.GetAllArchives();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _service.GetArchive(id);
            if (result.Item1.Equals(Statuses.Success))
            {
                return Ok(result.Item2);
            }

            return NotFound();
        }

    
        [HttpPost]
        public async Task<IActionResult> AddEvent(ArchivePostModel newArchive)
        {
            var convert = _mapper.Map<ArchivePostModel, ArchiveServiceModel>(newArchive);
            var result = await _service.AddArchive(convert);

            if (result == Statuses.Success)
                return Ok();

            return BadRequest();
        }
    }
}
