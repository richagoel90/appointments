using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class AppointmentInfo
    {
        #region Properties
        public int AppointmentID { get; set; }
        /// <summary>
        /// User who is requested the appointment
        /// </summary>
        public string HostUser { get; set; }
        /// <summary>
        /// User who is invited for an appointment
        /// </summary>
        [Required]
        public string GuestUser { get; set; }
        /// <summary>
        /// Date and Time of the appointment
        /// </summary>
        [Required]
        public DateTime DatenTime {get;set;}
        /// <summary>
        /// Subject of the requested appointment
        /// </summary>
        [Required]
        public string Subject { get; set; }
        public string Status { get; private set; }
        #endregion
    }
}