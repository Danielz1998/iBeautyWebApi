using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Categories
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int SalonId { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
