using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EditEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImgPath { get; set; }
        public string ImgName { get; set; }
        public UserModel User { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; } = false;
        public bool IsCurrent { get; set; } = false;
        public List<AttendantModel> Users { get; set; }
    }
}
