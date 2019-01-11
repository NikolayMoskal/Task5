using System;
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
            try
            {
                return Session.CreateQuery("from Product")
                    .List<Product>();
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
                Session.CreateQuery("delete Product")
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public override bool Exists(Product item, out Product foundItem)
        {
            try
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