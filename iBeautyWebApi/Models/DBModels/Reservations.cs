using System;
using System.Collections.Generic;

namespace iBeautyWebApi
{
    public partial class Reservations
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual Services Service { get; set; }
        public virtual Users User { get; set; }
    }
}
