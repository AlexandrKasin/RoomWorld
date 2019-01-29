using System.Collections.Generic;
using Data.Entity.UserEntity;

namespace Data.Entity.ChatEntity
{
    public class Dialog : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }
        public User Client { get; set; }
    }
}
    