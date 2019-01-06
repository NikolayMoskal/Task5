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
            return Session.CreateQuery("from Client")
                .List<Client>();
        }

        public override void DeleteAll()
        {
            Session.CreateQuery("delete Client")
                .ExecuteUpdate();
        }

        public override bool Exists(Client item, out Client foundItem)
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

            foundItem = null;
            return false;
        }
    }
}