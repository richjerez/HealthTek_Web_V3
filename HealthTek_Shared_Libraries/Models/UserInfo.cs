using System;

namespace HealthTek_Shared_Libraries
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string? Holder { get; set; }
        public string? Agent { get; set; }
        public string? Browser { get; set; }
        public string? LocalAddress { get; set; }
        public bool Status { get; set; }
        public bool IsVerified { get; set; }
        public int LAChar { get; set; }
        public int RAChar { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
