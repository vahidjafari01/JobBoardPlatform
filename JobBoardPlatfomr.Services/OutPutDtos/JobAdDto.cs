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

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record JobAdDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
        public TimeSpan StartWorkTime { get; set; }
        public TimeSpan EndWorkTIme { get; set; }



        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public bool IsFeatured { get; set; } = false;
        public string Status { get; set; }
        public string EmployementType { get; set; }
        public Guid CompanyId{ get; set; }
        public List<string>? Skils { get; set; }
    }
}
