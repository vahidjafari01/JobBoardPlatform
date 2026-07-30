using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record AdminDashbordDto
    {
        public AdminDashbordDto( int activeJobAdsCount, int deactiveJobAdsCount, int submittedAppsCount, int inReviewAppsCount, int interviewAppsCount, int acceptedAppsCount, int rejectedAppsCount, int canceledAppsCount, int employerIsNotApprovedCount, int employersCount, int jObSeekersCount)
        {
            ActiveJobAdsCount = activeJobAdsCount;
            DeactiveJobAdsCount = deactiveJobAdsCount;
            SubmittedAppsCount = submittedAppsCount;
            InReviewAppsCount = inReviewAppsCount;
            InterviewAppsCount = interviewAppsCount;
            AcceptedAppsCount = acceptedAppsCount;
            RejectedAppsCount = rejectedAppsCount;
            CanceledAppsCount = canceledAppsCount;
            EmployerIsNotApprovedCount = employerIsNotApprovedCount;
            EmployersCount = employersCount;
            JObSeekersCount = jObSeekersCount;
        }

        public int EmployersCount { get; set; }
        public int JObSeekersCount { get; set; }
        public int ActiveJobAdsCount { get; set; }
        public int DeactiveJobAdsCount { get; set; }
        public int SubmittedAppsCount { get; set; }

        public int InReviewAppsCount { get; set; }
        public int InterviewAppsCount { get; set; }
        public int AcceptedAppsCount { get; set; }
        public int RejectedAppsCount { get; set; }
        public int CanceledAppsCount { get; set; }
        public int EmployerIsNotApprovedCount {  get; set; }
    }
}
