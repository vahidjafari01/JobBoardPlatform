using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record JobEditCommand
    {
        public JobEditCommand(string title, string description, string? location, TimeSpan startWorkTime, TimeSpan endWorkTIme, decimal? salaryMin, decimal? salaryMax, string employementType, string jobadstatus, Guid categoryId, Guid cityId, Guid requesterId, Guid jobadId, List<string>? skils)
        {
            Title = title;
            Description = description;
            Location = location;
            StartWorkTime = startWorkTime;
            EndWorkTIme = endWorkTIme;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            EmployementType = employementType;
            Jobadstatus = jobadstatus;
            CategoryId = categoryId;
            CityId = cityId;
            RequesterId = requesterId;
            JobadId = jobadId;
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
        public string Jobadstatus {  get; set; }
        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }
        public Guid RequesterId { get; set; }
        public Guid JobadId { get; set; }
        public List<string>? Skils { get; set; }
    }
}
