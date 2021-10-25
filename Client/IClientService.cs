using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.Models;

namespace Client
{
    public interface IClientService
    {
        Task<List<EventModel>> GetAll(string token, string baseUrl, string target);
        Task AddEvent(EventModel userEvent, string token, string baseUrl, string target);
        Task<EventModel> GetOne(string token, string baseUrl, string target, int id);
        Task UpdateEvent(EventModel userEvent, string token, string baseUrl, string target, string currentUserId);
        Task AddAttendant(string token, string baseUrl, string target, int id, string userId);
        Task<string> Authenticate(string baseUrl, string controller, Credentials credentials);
        Task SetEndDate(string token ,string baseUrl, string controller, int id, DateTime date);
        Task SetAlive(string token, string baseUrl, string controller, int id);

    }
}
