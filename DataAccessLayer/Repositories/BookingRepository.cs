using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class BookingRepository : Repository<Booking>
    {
        public BookingRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Booking> GetAll()
        {
            return Session.CreateQuery("from Booking")
                .List<Booking>();
        }

        public override void DeleteAll()
        {
            Session.CreateQuery("delete Booking")
                .ExecuteUpdate();
        }

        public override bool Exists(Booking item, out Booking foundItem)
        {
            var list = Session.CreateQuery(@"from Booking t where t.Id = :id")
                .SetParameter("id", item.Id)
                .List<Booking>();
            if (list.Count != 0)
            {
                item.Id = list.First().Id;
                foundItem = item;
                return true;
            }

            foundItem = null;
            return false;
        }
    }
}