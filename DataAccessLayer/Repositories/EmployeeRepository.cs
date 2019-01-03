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

        public override bool Exists(Employee item, out Employee foundItem)
        {
            var list = Session.CreateQuery(@"from :type o where o.Name = :employeeName")
                .SetParameter("type", typeof(Employee))
                .SetParameter("employeeName", item.Name)
                .List<Employee>();
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