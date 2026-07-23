using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.Payments;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public class JobAdDetailForCustomer
    {
        public JobAdDetailForCustomer(string title, string description, string? location, TimeSpan startWorkTime, TimeSpan endWorkTIme, decimal? salaryMin, decimal? salaryMax, string status, string employementType, List<string>? skils, string categoryName, Guid? logoId, string companyName, string? companyDescription, string? companyWebSite, string companyLocation)
        {
            Title = title;
            Description = description;
            Location = location;
            StartWorkTime = startWorkTime;
            EndWorkTIme = endWorkTIme;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            Status = status;
            EmployementType = employementType;
            Skils = skils;
            CategoryName = categoryName;
            LogoId = logoId;
            CompanyName = companyName;
            CompanyDescription = companyDescription;
            CompanyWebSite = companyWebSite;
            CompanyLocation = companyLocation;
        }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
        public TimeSpan StartWorkTime { get; set; }
        public TimeSpan EndWorkTIme { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string Status { get; set; }
        public string EmployementType { get; set; }
        public List<string>? Skils { get; set; }
        
        public string CategoryName { get; set; }
        public Guid? LogoId { get; set; }

        public string CompanyName { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CompanyWebSite { get; set; }
        public string CompanyLocation { get; set; }
    }
}
