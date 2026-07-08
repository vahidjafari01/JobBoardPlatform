using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
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
namespace JobBoardPlatform.Domain.entities
{
    public class JobAd:BaseEntity
    {
        public JobAd()
        {
            
        }
        public JobAd(string title, string description, string? location, decimal? salaryMin, decimal? salaryMax, int featurePriority, DateTime? featuredUntil, JobAdStatus status, EmploymentType employementType, Guid companyId, Guid categoryId, string province)
        {
            Title = title;
            Description = description;
            Location = location;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            FeaturePriority = featurePriority;
            FeaturedUntil = featuredUntil;
            Status = status;
            EmployementType = employementType;
            CompanyId = companyId;
            CategoryId = categoryId;
            Validate();
            Province = province;
        }
        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Title)) throw new ArgumentNullException("title can not be null");
            if(string.IsNullOrWhiteSpace(Description)) throw new ArgumentNullException("title can not be null");
            if(string.IsNullOrWhiteSpace(Province)) throw new ArgumentNullException("title can not be null");
            if (FeaturePriority < 1 || FeaturePriority > 3) throw new ArgumentOutOfRangeException("FeatuerPrirate must be in range of (1,3)");
        }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
        public string Province{ get; set; }


        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public bool IsFeatured { get; set; } = false;
        [Range(1,3)]
        public int FeaturePriority { get; set; } = 3;
        public DateTime? FeaturedUntil { get; set; }

        public JobAdStatus Status { get; set; }
        public EmploymentType EmployementType { get; set; }
        public List<Application>? Applications{ get; set; }

        public Company Company{ get; set; }
        public  Guid CompanyId { get; set; }
        public List<Payment>? Payments{ get; set; }
        public JobCategory Category{ get; set; }
        public Guid CategoryId { get; set; }



    }

}