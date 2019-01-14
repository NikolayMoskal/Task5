using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Employee> GetAll()
        {
            try
            {
                return Session.CreateQuery("from Employee")
                    .List<Employee>();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return null;
        }

        public override bool DeleteAll()
        {
            try
            {
                return Session.CreateQuery("delete Employee")
                           .ExecuteUpdate() > 0;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        public override bool Exists(Employee item, out Employee foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from Employee o where o.Name = :employeeName")
                    .SetParameter("employeeName", item.Name)
                    .List<Employee>();
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