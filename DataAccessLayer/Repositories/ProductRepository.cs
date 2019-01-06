using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Product> GetAll()
        {
            return Session.CreateQuery("from Product")
                .List<Product>();
        }

        public override void DeleteAll()
        {
            Session.CreateQuery("delete Product")
                .ExecuteUpdate();
        }

        public override bool Exists(Product item, out Product foundItem)
        {
            var list = Session.CreateQuery(@"from Product o where o.Name = :productName")
                .SetParameter("productName", item.Name)
                .List<Product>();
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