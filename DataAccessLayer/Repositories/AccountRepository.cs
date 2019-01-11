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

        public override void DeleteAll()
        {
            try
            {
                Session.CreateQuery("delete Account")
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public override bool Exists(Account item, out Account foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from Account o where o.UserName = :userName" +
                                               @" and o.PasswordHash = :hash")
                    .SetParameter("userName", item.UserName)
                    .SetParameter("hash", item.PasswordHash)
                    .List<Account>();
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