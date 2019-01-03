using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class BookingRepository : Repository<Booking>
    {
        public BookingRepository(ISession session) : base(session)
        {
        }
    }
}