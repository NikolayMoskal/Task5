using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class User : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}