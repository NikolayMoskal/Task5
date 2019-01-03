using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Client : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<Booking> Bookings { get; set; }
    }
}