﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyApp.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EditEndDate { get; set; }
        public string ImgPath { get; set; }
        public string ImgName { get; set; }
        public UserViewModel User { get; set; }
        public bool IsActive { get; set; }
        public List<AttendantViewModel> Users { get; set; }
    }
}
