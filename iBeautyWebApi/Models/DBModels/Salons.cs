using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Salons
    {
        public Salons()
        {
            Categories = new HashSet<Categories>();
            Products = new HashSet<Products>();
            Promotions = new HashSet<Promotions>();
            Services = new HashSet<Services>();
        }

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
        public string Description { get; set; }

        public virtual Cities City { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Promotions> Promotions { get; set; }
        public virtual ICollection<Services> Services { get; set; }
    }
}
