using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsApp.Models;
using EventsApp.Models.Event;

namespace EventsApp.Infrastructure.Helpers
{
    public static class ValidateEvents
    {
        public static List<EventViewModel> Validate(List<EventViewModel> events, UserIdentity currentUser)
        {
            foreach (var eve in events)
            {
                if (currentUser != null)
                {
                    if (eve.UserId == currentUser.Id)
                        eve.IsCurrent = true;
                }

                if (eve.EditEndDate > DateTime.Now)
                    eve.IsEditable = true;

                if (eve.User == null)
                    eve.User = new UserViewModel { Name = "", LastName = "" };
                
            }

            return events;
        }
    }
}
