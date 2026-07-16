using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record UpdateCompanyComand
    {
        public Guid RequesterId { get; set; }
        public Guid CompanyId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }
        public Guid CityId { get; set; }
    }
}
