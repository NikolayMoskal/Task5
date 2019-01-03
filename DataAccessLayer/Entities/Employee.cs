using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Employee : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<Booking> Bookings { get; set; }
    }
}