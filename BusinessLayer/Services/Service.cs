using System;
using System.Collections.Generic;
using BusinessLayer.Models;
using BusinessLayer.UnitsOfWork;

namespace BusinessLayer.Services
{
    public abstract class Service<TEntity> where TEntity : Model
    {
        protected readonly UnitOfWork UnitOfWork;

        protected Service(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public abstract IEnumerable<TEntity> GetAll();

        public abstract TEntity GetOne(int id);

        public abstract void Save(TEntity entity);

        public abstract void Delete(TEntity entity);

        public abstract void DeleteAll();
    }
}