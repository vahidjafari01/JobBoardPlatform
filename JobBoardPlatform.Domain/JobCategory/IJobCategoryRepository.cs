using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.JobCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.JobCategories
{
    public interface IJobCategoryRepository:IGenericRepository<JobCategory>
    {
    }
}
