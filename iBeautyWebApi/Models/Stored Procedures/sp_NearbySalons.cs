using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBeautyWebApi.Models.Stored_Procedures
{
    public class sp_NearbySalons
    {
        [Key]
        public int SalonId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public double Distance { get; set; }
        public bool Status { get; set; }
    }
}
