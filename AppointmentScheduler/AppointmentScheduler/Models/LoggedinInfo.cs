using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class LoggedinInfo
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}