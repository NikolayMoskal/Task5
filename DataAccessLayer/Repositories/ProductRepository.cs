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

        public override bool Exists(Product item, out Product foundItem)
        {
            var list = Session.CreateQuery(@"from :type o where o.Name = :productName")
                .SetParameter("type", typeof(Product))
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