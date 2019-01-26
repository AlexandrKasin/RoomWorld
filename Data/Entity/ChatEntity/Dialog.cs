using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Dialog : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }
        public User Client { get; set; }
    }
}
    