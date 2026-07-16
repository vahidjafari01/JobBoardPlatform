using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.enums;
using JobBoardPlatform.Domain.JobCategories;
using JobBoardPlatform.Domain.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record JobAdCreateCommand
    {
        public JobAdCreateCommand(string title, string description, string? location, TimeSpan startWorkTime, TimeSpan endWorkTIme, decimal? salaryMin, decimal? salaryMax, string employementType, Guid categoryId, Guid cityId, Guid requesterId, List<string>? skils)
        {
            Title = title;
            Description = description;
            Location = location;
            StartWorkTime = startWorkTime;
            EndWorkTIme = endWorkTIme;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            EmployementType = employementType;
            CategoryId = categoryId;
            CityId = cityId;
            RequesterId = requesterId;
            Skils = skils;
        }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
        public TimeSpan StartWorkTime { get; set; }
        public TimeSpan EndWorkTIme { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string EmployementType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; } 
        public Guid RequesterId { get; set; }
        public List<string>? Skils { get; set; }
    }
}
