using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Promotions
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SalonId { get; set; }
        public string Image { get; set; }

        public virtual Salons Salon { get; set; }
    }
}
