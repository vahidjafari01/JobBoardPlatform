using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record JobEditCommand
    {
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
