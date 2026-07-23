using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.enums;
using JobBoardPlatform.Domain.JobCategories;
using JobBoardPlatform.Domain.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Abstractions
{
    public record GetJobAdCommand
    {
        public string? Title { get; set; } = null!;
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }

        public EmploymentType? EmployementType { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? ProvinceId { get; set; }
        public List<string>? Skils { get; set; }


    }
}
