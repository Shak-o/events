using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Data.EF.Implementation
{
    public class EventRepository : IEventRepository
    {
        private readonly IBaseRepository<Event> _eventRepo;
        private readonly IBaseRepository<User> _userRepo;

        public EventRepository(IBaseRepository<Event> eventRepo, IBaseRepository<User> userRepo)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
        }
        public async Task<List<Event>> GetAllEvents()
        {
            var test = await _eventRepo.Table
                .Include(x => x.User)
                .Include(x => x.EventAttendant)
                .ThenInclude(x => x.Attendant).ToListAsync();

            return await _eventRepo.Table
                .Include(x => x.User)
                .Include(x => x.EventAttendant)
                .ThenInclude(x => x.Attendant).ToListAsync(); ;
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _eventRepo.Table
                .Include(x => x.User)
                .Include(x => x.EventAttendant)
                .ThenInclude(x => x.Attendant).FirstOrDefaultAsync(x => x.Id == id); ;
        }
        public async Task<Event> GetEventNoTracking(int id)
        {
            return await _eventRepo.Table.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.EventAttendant)
                .ThenInclude(x => x.Attendant).FirstOrDefaultAsync(x => x.Id == id); ;
        }
        public async Task UpdateEvent(Event updateEvent)
        {
            if (updateEvent.EventAttendant != null)
            {
                foreach (var evAttendant in updateEvent.EventAttendant)
                {
                    _userRepo.SetState(evAttendant.Attendant, EntityState.Unchanged);
                }
            }

            if (updateEvent.User != null)
            {
                _userRepo.SetState(updateEvent.User, EntityState.Unchanged);
            }
            

            await _eventRepo.UpdateAsync(updateEvent);
        }

        public async Task DeleteEvent(int id)
        {
            await _eventRepo.DeleteAsync(id);
        }

        public async Task AddEvent(Event userEvent)
        {
            await _eventRepo.CreateAsync(userEvent);
        }

        public async Task SetActive(int id)
        {
            var toUpdate = await _eventRepo.GetAsync(id);
            toUpdate.IsActive = true;
            await _eventRepo.UpdateAsync(toUpdate);
        }

        public async Task SetEndDate(int id, DateTime date)
        {
            var toUpdate = await  _eventRepo.Table.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.EventAttendant)
                .ThenInclude(x => x.Attendant).FirstOrDefaultAsync(x => x.Id == id);
            toUpdate.EditEndDate = date;
            await _eventRepo.UpdateAsync(toUpdate);
        }

        public async Task<bool> ValidateUser(string userId)
        {
            var result = await _userRepo.GetAsync(userId);
            if (result != null)
                return true;

            return false;
        }

        public async Task<User> GetUser(string userId)
        {
            var res = await _userRepo.GetAsync(userId);
            return res;
        }
    }
}
