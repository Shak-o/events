using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyApp.Models.Event
{
    public class EndDateViewModel
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
