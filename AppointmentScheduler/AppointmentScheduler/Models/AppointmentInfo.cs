using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class AppointmentInfo
    {
        public string HostUser { get; set; }
        [Required]
        public string GuestUser { get; set; }
        [Required]
        public DateTime DatenTime {get;set;}
        [Required]
        public String Subject { get; set; }
    }
}