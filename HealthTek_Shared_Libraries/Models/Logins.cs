using System;

namespace HealthTek_Shared_Libraries
{
    public class Logins
    {
        public int LoginId { get; set; }
        public string FkUserId { get; set; }
        public int FkDeviceId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
    }
}
