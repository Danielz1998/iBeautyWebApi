﻿using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Services
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int SalonId { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
