using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Domain.Abstractions
{
    public record EmployerDto
    {
        public EmployerDto(Guid id, string firstName, string lastName, DateTime createdAt, DateTime modifiedAt, bool isDeleted, bool isApproved, Guid companyId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            IsDeleted = isDeleted;
            IsApproved = isApproved;
            CompanyId = companyId;
        }

        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } 
        public bool IsApproved { get; set; } 



    }
}
