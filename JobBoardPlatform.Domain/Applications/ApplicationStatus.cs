using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Applications
{
    public enum ApplicationStatus
    {
        Submitted = 1,
        InReview = 2,
        Interview = 3,
        Accepted = 4,
        Rejected = 5,
        Withdrawn = 6
    }

}
