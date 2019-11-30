using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Cities
    {
        public Cities()
        {
            Salons = new HashSet<Salons>();
            Users = new HashSet<Users>();
        }

        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Salons> Salons { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
