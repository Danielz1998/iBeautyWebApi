using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class ReservationsStatus
    {
        public ReservationsStatus()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int RservationsStatusId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
