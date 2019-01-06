using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     Declares a common functionality of CRUD repository
    /// </summary>
    /// <typeparam name="TEntity">The entity type for existing table in DB</typeparam>
    public class Repository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ISession Session;

        public Repository(ISession session)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return null;
        }

        public virtual TEntity GetOne(int id)
        {
            return Session.Load<TEntity>(id);
        }

        public virtual void Save(TEntity item)
        {
            if (!Exists(item, out var foundItem))
                Session.SaveOrUpdate(item);
            //Session.Merge(item);
            else
                item = foundItem;
        }

        public virtual void Delete(TEntity item)
        {
            Session.Delete(item);
        }

        public virtual void DeleteAll()
        {
        }

        public virtual bool Exists(TEntity item, out TEntity foundItem)
        {
            foundItem = null;
            return false;
        }
    }
}