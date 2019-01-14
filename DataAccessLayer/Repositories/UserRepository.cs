using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            try
            {
                return Session.CreateQuery("from User")
                    .List<User>();
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
                return Session.CreateQuery("delete User")
                           .ExecuteUpdate() > 0;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        public override bool Exists(User item, out User foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from User o where o.FirstName = :firstName" +
                                               @" and o.LastName = :lastName" +
                                               @" and o.BirthDate = :date")
                    .SetParameter("firstName", item.FirstName)
                    .SetParameter("lastName", item.LastName)
                    .SetParameter("date", item.BirthDate)
                    .List<User>();
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