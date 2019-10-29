using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class LoggedinInfo
    {
        #region Properties
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int UserId { get; private set; }
        #endregion
    }
}