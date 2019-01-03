using System;

namespace DataAccessLayer.Entities
{
    public class Booking : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }
    }
}