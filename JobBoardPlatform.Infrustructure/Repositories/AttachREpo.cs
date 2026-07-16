using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class AttachREpo : GenericRepository<Attach>, IAttachRepo
    {
        public AttachREpo(AppDbContext context) : base(context)
        {
        }
    }
}
