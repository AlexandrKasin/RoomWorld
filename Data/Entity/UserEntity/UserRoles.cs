namespace Data.Entity.UserEntity
{
    public class UserRoles : BaseEntity
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}