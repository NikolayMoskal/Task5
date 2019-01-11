namespace DataAccessLayer.Entities
{
    public class Account : Entity
    {
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}