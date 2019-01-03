using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class ProductMapping : ClassMapping<Product>
    {
        public ProductMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name, m => m.Column(c =>
            {
                c.Length(20);
                c.NotNullable(true);
            }));
            Property(x => x.Price, m => m.Column(c =>
            {
                c.Scale(2);
                c.Default(0.0);
            }));
            Bag(x => x.Bookings, c =>
            {
                c.Key(y => y.Column("Product_Id"));
                c.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}