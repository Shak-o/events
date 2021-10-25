using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Exceptions;
using AcademyApp.Service.Interfaces;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.Service.Implementation
{
    public class ArchiveService : IArchiveService
    {
        private readonly IArchiveRepository _repo;
        private readonly IMapper _mapper;
        public ArchiveService(IArchiveRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<(Statuses, ArchiveServiceModel)> GetArchive(int id)
        {
            var result = await _repo.GetAsync(id);
            if (result == null)
                throw new EventNotFoundException("Event with this id not found: " + id);
            var convert = _mapper.Map<Archive, ArchiveServiceModel>(result);
            return (Statuses.Success, convert);
        }

        public async Task<List<ArchiveServiceModel>> GetAllArchives()
        {
            var result = await _repo.GetAllAsync();
            var convert = _mapper.Map<List<Archive>, List<ArchiveServiceModel>>(result);
            return convert;
        }

        public async Task<Statuses> AddArchive(ArchiveServiceModel archive)
        {
            var convert = _mapper.Map<ArchiveServiceModel, Archive>(archive);

            await _repo.AddAsync(convert);

            if (_repo.GetAsync(convert.Id) == null)
                throw new EventNotFoundException("Event with this id not found: " + convert.Id);

            return Statuses.Success;
        }
    }
}
