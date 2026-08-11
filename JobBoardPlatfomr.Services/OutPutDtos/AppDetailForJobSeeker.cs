using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record AppDetailForJobSeeker
    {
        public AppDetailForJobSeeker(Guid jobAdId, Guid appId, string status, DateTime? reviewedAt, DateTime? modifiedAt, string? noteWritenByUser, Guid? resumeId)
        {
            JobAdId = jobAdId;
            AppId = appId;
            Status = status;
            ReviewedAt = reviewedAt;
            ModifiedAt = modifiedAt;
            NoteWritenByUser = noteWritenByUser;
            ResumeId = resumeId;
        }

        public Guid JobAdId { get; set; }
        public Guid AppId { get; set; }
        public string Status { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime? ModifiedAt{ get; set; }
        public string? NoteWritenByUser { get; set; }
        public Guid? ResumeId { get; set; }
    }
}
