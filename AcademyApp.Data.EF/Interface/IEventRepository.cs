using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;

namespace AcademyApp.Data.EF.Interface
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEvent(int id);
        Task UpdateEvent(Event updateEvent);
        Task DeleteEvent(int id);
        Task AddEvent(Event userEvent);
        Task<Event> GetEventNoTracking(int id);
        Task SetActive(int id);
        Task SetEndDate(int id, DateTime date);
        Task<bool> ValidateUser(string userId);
        Task<User> GetUser(string userId);
    }
}
