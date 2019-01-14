using System;
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
            try
            {
                return Session.CreateQuery("from Booking")
                    .List<Booking>();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return null;
        }

        public override bool DeleteAll()
        {
            try
            {
                return Session.CreateQuery("delete Booking")
                           .ExecuteUpdate() > 0;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        public override bool Exists(Booking item, out Booking foundItem)
        {
            try
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
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            foundItem = null;
            return false;
        }
    }
}