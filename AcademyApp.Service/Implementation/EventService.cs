using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Interfaces;
using AcademyApp.Service.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcademyApp.Service.Exceptions;


namespace AcademyApp.Service.Implementation
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;
        public EventService(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<EventServiceModel> GetEvent(int id)
        {
            var result = await _repo.GetEvent(id);
            if (result == null)
                throw new EventNotFoundException("Event with this id not found: " + id);
            var convert = _mapper.Map<Event, EventServiceModel>(result);
            return  convert;
        }

        public async Task<List<EventServiceModel>> GetAllEvents()
        {
            var result = await _repo.GetAllEvents();
            var convert = _mapper.Map<List<Event>, List<EventServiceModel>>(result);
            return convert;
        }

        public async Task AddEvent(EventServiceModel userEvent)
        {
            var convert = _mapper.Map<EventServiceModel, Event>(userEvent);
            convert.CreateDate = DateTime.Now;
            convert.EditEndDate = convert.CreateDate;
            if (userEvent.Users != null)
            {
                foreach (var attendant in userEvent.Users)
                {
                    if (attendant.Id == userEvent.UserId)
                    {
                        throw new InvalidAttendantException("Author of event cant be attendant");
                    }
                }
            }
            await _repo.AddEvent(convert);

            if (_repo.GetEvent(convert.Id) == null)
                throw new EventNotFoundException("Event with this id not found: " + convert.Id);
        }

        public async Task DeleteEvent(int id)
        {
            var check = await _repo.GetEvent(id);

            if (check == null)
                throw new EventNotFoundException("Event with this id not found: " + id);

            await _repo.DeleteEvent(id);
        }

        public async Task UpdateEvent(string requesterId, EventServiceModel updateEvent)
        {
         
            var check = await _repo.GetEventNoTracking(updateEvent.Id);

            if (check == null)
                throw new EventNotFoundException("event with this id not found");
          
            if (updateEvent.Users != null)
            {
                foreach (var attendant in updateEvent.Users)
                {
                    if (attendant.Id == updateEvent.UserId)
                    {
                        throw new InvalidAttendantException("owner of event cant be attendant");
                    }
                }
            }

            check.Title = updateEvent.Title;
            check.Description = updateEvent.Description;
            check.StartDate = updateEvent.StartDate;
            check.EndDate = updateEvent.EndDate;
            check.ImgName = updateEvent.ImgName;
            check.ImgPath = updateEvent.ImgPath;
           
            if (check.EditEndDate > DateTime.Now && requesterId == check.UserId)
            {
                await _repo.UpdateEvent(check);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task SetActive(int id)
        {
            await _repo.SetActive(id);
        }

        public async Task SetEndDate(int id, DateTime date)
        {
            await _repo.SetEndDate(id, date);
        }

        public async Task AddAttendant(string userId, int eventId)
        {
            var toUpdate = await _repo.GetEvent(eventId);
            if (toUpdate.User.Id.Equals(userId))
                throw new InvalidAttendantException("Author of event cant be attendant");

            if (!await _repo.ValidateUser(userId))
                throw new UserNotFoundException("User with this id not found");
           
            foreach (var item in toUpdate.EventAttendant)
            {
                if (item.AttendantId == userId)
                    throw new InvalidAttendantException("Attendant already added in this event");
            }

            var user = await _repo.GetUser(userId);
            toUpdate.EventAttendant.Add(new EventAttendant{AttendantId = userId, Event = toUpdate, EventId = eventId, Attendant = user});

            await _repo.UpdateEvent(toUpdate);

        }
    }
}
