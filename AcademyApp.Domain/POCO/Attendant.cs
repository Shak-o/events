using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcademyApp.Domain.POCO
{
    public class Attendant
    {
     
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<EventAttendant> EventAttendant { get; set; }
    }
}
