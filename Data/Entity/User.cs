using System.Collections.Generic;

namespace Data.Entity
{
    public class User : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Role { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string PhoneNumber { get; set; }
        public ICollection<Flat> Flats { get; set; }
    }
}