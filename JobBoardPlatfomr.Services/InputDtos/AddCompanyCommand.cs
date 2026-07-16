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
        public Guid UserId { get; set; }

        public string Name { get; set; } 
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }
        public Guid CityId { get; set; }
    }
}
