using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class RoleMapping : ClassMapping<Role>
    {
        public RoleMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Native));
            Property(x => x.RoleName, m => m.Column(c =>
            {
                c.Length(15);
                c.NotNullable(true);
            }));
            Bag(x => x.Accounts, c =>
            {
                c.Cascade(Cascade.All);
                c.Key(y => y.Column("Role_Id"));
                c.Inverse(true);
            }, m => m.OneToMany());
        }
    }
}