using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
            Services = new HashSet<Services>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int SalonId { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Salons Salon { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Services> Services { get; set; }
    }
}
