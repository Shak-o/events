using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Service.Models;

namespace AcademyApp.API.Infrastructure.Models
{
    public class EventPutModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImgPath { get; set; }
        public string ImgName { get; set; }
    }
}
