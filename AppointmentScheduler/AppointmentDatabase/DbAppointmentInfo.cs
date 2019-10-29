using System;

namespace AppointmentDatabase
{
    public class DbAppointmentInfo
    {
        #region Properties
        public int AppointmentID { get; set; }
        public string HostUser { get; set; }
        public string GuestUser { get; set; }
        public DateTime DatenTime { get; set; }
        public string Subject { get; set; }
        #endregion
    }


}

