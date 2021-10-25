using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Service.Models;

namespace AcademyApp.Service.Interfaces
{
    public interface IEventService
    {
        Task<EventServiceModel> GetEvent(int id);
        Task<List<EventServiceModel>> GetAllEvents();
        Task AddEvent (EventServiceModel UserEvent);
        Task DeleteEvent(int id);
        Task UpdateEvent(string requesterId,EventServiceModel updateEvent);
        Task SetActive(int id);
        Task SetEndDate(int id, DateTime date);
        Task AddAttendant(string userId, int eventId);
    }
}
