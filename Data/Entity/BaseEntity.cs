using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; protected set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public long CreatedBy { get; set; }

        public long? ModifiedBy { get; set; }


    }
}
