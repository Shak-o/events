using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyApp.Domain.POCO
{
    public class EventAttendant
    {

        public string AttendantId { get; set; }
        public int EventId { get; set; }
        public User Attendant { get; set; }
        public Event Event { get; set; }
    }
}
