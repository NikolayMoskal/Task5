using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Role : Entity
    {
        public virtual string RoleName { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}