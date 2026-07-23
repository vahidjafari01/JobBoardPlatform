using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class AttachService: IAttachService
    {
        private IUnitOfWork _unitofWork;

        public AttachService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<AttachOutputDto> DownloadAsync(Guid attachmentId)
        {
            var attachment = await _unitofWork.AttacheRepo.GetByIdAsync(attachmentId);

            if (attachment == null)
                throw new NotFoundException($"the attachment with id {attachmentId} was not found","attach-404");

            return new AttachOutputDto()
            {
                contentType = attachment.ContentType,
                Filedb64 = attachment.Filedb64,
                Filename = attachment.FileName
            };
        }

        public async Task HardDeleteAttachmentAsync(Guid attachmentId)
        {

            await _unitofWork.AttacheRepo.DeleteAsync(attachmentId);

            await _unitofWork.SaveChangesAsync();
        }

        public async Task<Guid> UploadAsync(IFormFile formFile)
        {
            if (formFile == null)
                throw new ValidationException("file is required");

            using var stream = new MemoryStream();

            await formFile.CopyToAsync(stream);

            var attachment = new Attach(stream.ToArray(),formFile.ContentType,formFile.FileName);

            await _unitofWork.AttacheRepo.AddAsync(attachment);

            await _unitofWork.SaveChangesAsync();

            return attachment.Id;
        }
    }
}
