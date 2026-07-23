using JobBoardPlatform.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public class GetJObAdFilterCommand
    {
        public string? Title { get; set; } = null!;
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }

        public string? EmployementType { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? ProvinceId { get; set; }
        public List<string>? Skils { get; set; }

    }
}
