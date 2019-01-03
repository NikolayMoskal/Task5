using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     Declares a common functionality of CRUD repository
    /// </summary>
    /// <typeparam name="TEntity">The entity type for existing table in DB</typeparam>
    public class Repository<TEntity> where TEntity : Entity
    {
        protected readonly ISession Session;

        public Repository(ISession session)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Session.CreateQuery("from :type")
                .SetParameter("type", typeof(TEntity))
                .List<TEntity>();
        }

        public virtual TEntity GetOne(int id)
        {
            return Session.Load<TEntity>(id);
        }

        public virtual void Save(TEntity item)
        {
            if (!Exists(item, out var foundItem))
                Session.SaveOrUpdate(item);
            else
                item.Id = foundItem.Id;
        }

        public virtual void Delete(TEntity item)
        {
            Session.Delete(item);
        }

        public virtual void DeleteAll()
        {
            Session.CreateQuery("delete :type")
                .SetParameter("type", typeof(TEntity))
                .ExecuteUpdate();
        }

        public virtual bool Exists(TEntity item, out TEntity foundItem)
        {
            var list = Session.CreateQuery(@"from :type t where t.Id = :id")
                .SetParameter("type", typeof(TEntity))
                .SetParameter("id", item.Id)
                .List<TEntity>();
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