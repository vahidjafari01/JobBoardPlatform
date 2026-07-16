using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record AddCompanyCommand
    {
        public AddCompanyCommand(Guid userId, string name, string? description, string? website, string location, Guid cityId)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Website = website;
            Location = location;
            CityId = cityId;
        }

        public Guid UserId { get; set; }

        public string Name { get; set; } 
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }
        public Guid CityId { get; set; }
    }
}
