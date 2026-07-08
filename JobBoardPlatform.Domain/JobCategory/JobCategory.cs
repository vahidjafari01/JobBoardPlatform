using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.JobCategories
{
    public class JobCategory:BaseEntity
    {
        public JobCategory()
        {
            
        }
        public JobCategory(string name,Guid parentId)
        {
            Name = name;
            ParentId = parentId;
            Validate();
        }
            
        public JobCategory(string name)
        {
            Name = name;
            Validate();
        }
        public string Name { get; set; } = null!;

        public Guid? ParentId { get; set; }
        public List<JobAd> JobAds { get; set; }
        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {

                throw new ArgumentNullException("Name of category can not be null");
            }
            if (Name.Length < 3)
            {
                throw new Exception("lenth of category name must be longer than 2 characters");
            }
        }
    }
}



