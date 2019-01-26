using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}