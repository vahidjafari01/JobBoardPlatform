using JobBoardPlatform.Domain.JobCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class JobCategoryRepo : GenericRepository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepo(AppDbContext context) : base(context)
        {
        }
    }
}
