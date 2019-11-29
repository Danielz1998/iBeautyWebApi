using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Countries
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
