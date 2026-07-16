using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record ApplicationDto
    {
        public Guid ApplicationId { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public DateTime SubmitedAt {  get; set; }
       

    }
}
