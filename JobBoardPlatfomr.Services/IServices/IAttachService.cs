using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IAttachService
    {

        Task<AttachOutputDto> DownloadAsync(Guid attachmentId);


         Task HardDeleteAttachmentAsync(Guid attachmentId);


       Task<Guid> UploadAsync(IFormFile formFile);
        
    }
}

