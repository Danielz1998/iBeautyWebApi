using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Users
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public int VerificationCode { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
