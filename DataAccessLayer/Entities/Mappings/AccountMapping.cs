using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class AccountMapping : ClassMapping<Account>
    {
        public AccountMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Native));
            Property(x => x.UserName, m => m.Column(c =>
            {
                c.Length(15);
                c.NotNullable(true);
            }));
            Property(x => x.PasswordHash, m => m.Column(c => { c.NotNullable(true); }));
            ManyToOne(x => x.User, c =>
            {
                c.Column("User_Id");
                c.NotNullable(true);
            });
            ManyToOne(x => x.Role, c =>
            {
                c.Column("Role_Id");
                c.NotNullable(true);
            });
        }
    }
}