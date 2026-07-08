using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Abstractions
{
    public abstract class BaseEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [Key]
        public Guid Id { get; set; }
    }
}
