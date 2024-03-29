﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppointmentScheduler.Models
{
    public class UserInfo
    {
        #region Properties
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailID { get; set; }
        public long PhoneNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        #endregion
    }
}