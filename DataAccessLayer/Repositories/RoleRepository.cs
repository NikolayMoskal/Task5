using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(ISession session) : base(session)
        {
        }

        public override IEnumerable<Role> GetAll()
        {
            try
            {
                return Session.CreateQuery("from Role")
                    .List<Role>();
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
                Session.CreateQuery("delete Role")
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public override bool Exists(Role item, out Role foundItem)
        {
            try
            {
                var list = Session.CreateQuery(@"from Role o where o.RoleName = :roleName")
                    .SetParameter("roleName", item.RoleName)
                    .List<Role>();
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