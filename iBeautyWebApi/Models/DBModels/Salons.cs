using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Salons
    {
        public int SalonId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
