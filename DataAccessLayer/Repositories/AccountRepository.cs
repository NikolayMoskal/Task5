using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Account> GetAll()
        {
            try
            {
                return Session.CreateQuery("from Account")
                    .List<Account>();
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
                return Session.CreateQuery("delete Account")
                           .ExecuteUpdate() > 0;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        public override bool Exists(Account item, out Account foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from Account o where o.UserName = :userName")
                    .SetParameter("userName", item.UserName)
                    .List<Account>();
                if (list.Count != 0)
                {
                    foundItem = list.First();
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