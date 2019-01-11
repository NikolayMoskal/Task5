using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.FirstName, m => m.Column(c =>
            {
                c.Length(20);
                c.NotNullable(true);
            }));
            Property(x => x.LastName, m => m.Column(c =>
            {
                c.Length(30);
                c.NotNullable(true);
            }));
            Property(x => x.BirthDate, m => m.Column(c =>
            {
                c.SqlType("date");
                c.NotNullable(true);
            }));
            Bag(x => x.Accounts, c =>
            {
                c.Key(y => y.Column("User_Id"));
                c.Inverse(true);
            }, m => m.OneToMany());
        }
    }
}