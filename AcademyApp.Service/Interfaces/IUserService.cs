using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Service.Models;

namespace AcademyApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<CredsServiceModel> GetAll();
    }
}
