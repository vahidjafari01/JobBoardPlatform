using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public class JobAdViewModel
    {
        public string Title { get; set; }
        public Guid JobAdId { get; set; }

        public Guid? LogoId { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public bool IsFeatured { get; set; }
        public string EmployementType { get; set; }
    }
}
