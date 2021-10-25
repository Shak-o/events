using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyApp.Service.Models
{
    public class ArchiveServiceModel
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
        public UserServiceModel User { get; set; }
        public bool IsActive { get; set; }
    }
}
