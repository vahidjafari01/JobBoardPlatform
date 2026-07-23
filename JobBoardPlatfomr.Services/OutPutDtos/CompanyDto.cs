using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public class CompanyDto
    {
        public CompanyDto(string name, string? description, string? website, string location, string ownerName, DateTime createdAt, DateTime modifiedAt, Guid? logoId)
        {
            Name = name;
            Description = description;
            Website = website;
            Location = location;

            OwnerName = ownerName;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            LogoId = logoId;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }

        public string OwnerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid? LogoId{ get; set; }
    }
}
