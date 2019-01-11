using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using NHibernate;
using NLog;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     Declares a common functionality of CRUD repository
    /// </summary>
    /// <typeparam name="TEntity">The entity type for existing table in DB</typeparam>
    public class Repository<TEntity> where TEntity : Entity, new()
    {
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
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
            try
            {
                return Session.Load<TEntity>(id);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return null;
        }

        public virtual TEntity Save(TEntity item)
        {
            try
            {
                if (!Exists(item, out var foundItem))
                    Session.SaveOrUpdate(item);
                return item;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return null;
        }

        public virtual void Delete(TEntity item)
        {
            try
            {
                Session.Delete(item);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
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