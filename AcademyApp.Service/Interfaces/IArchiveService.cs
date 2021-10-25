using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Service.Models;

namespace AcademyApp.Service.Interfaces
{
    public interface IArchiveService
    {
        Task<(Statuses, ArchiveServiceModel)> GetArchive(int id);
        Task<List<ArchiveServiceModel>> GetAllArchives();
        Task<Statuses> AddArchive(ArchiveServiceModel archive);
    }
}
