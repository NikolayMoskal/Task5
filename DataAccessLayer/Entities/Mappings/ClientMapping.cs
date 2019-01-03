using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class ClientMapping : ClassMapping<Client>
    {
        public ClientMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name, m => m.Column(c =>
            {
                c.Length(20);
                c.NotNullable(true);
            }));
            Bag(x => x.Bookings, c =>
            {
                c.Key(y => y.Column("Client_Id"));
                c.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}