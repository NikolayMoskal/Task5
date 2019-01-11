using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Client> GetAll()
        {
            try
            {
                return Session.CreateQuery("from Client")
                    .List<Client>();
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
                Session.CreateQuery("delete Client")
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public override bool Exists(Client item, out Client foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from Client o where o.Name = :clientName")
                    .SetParameter("clientName", item.Name)
                    .List<Client>();
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