using JobBoardPlatfomr.Services.BussinesExceptions;
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
    //public class AttachService
    //{
    //    public async Task<AttachmentResponseDto> DownloadAsync(Guid attachmentId)
    //    {
    //        var attachment = await _unitOfWork.AttachmentRepository.GetAttachmentByIdAsync(a => new AttachmentResponseDto
    //        {
    //            FileName = a.FileName,
    //            ContentType = a.ContentType,
    //            Data = a.Data
    //        }, attachmentId);

    //        if (attachment == null)
    //            throw new NotFoundException($"the attachment with id {attachmentId} was not found");

    //        return attachment;
    //    }

    //    public async Task<bool> HardDeleteAttachmentAsync(Guid attachmentId)
    //    {

    //        var result = await _unitOfWork.AttachmentRepository.HardDeleteAttachmentAsync(attachmentId);

    //        if (!result)
    //            throw new NotFoundException($"the attachment with id {attachmentId} was not found","File-404");

    //        return await _unitOfWork.SaveChangesAsync() > 0;
    //    }

    //    public async Task<Guid> UploadAsync(IFormFile formFile)
    //    {
    //        if (formFile == null)
    //            throw new ValidationException("file is required");

    //        using var stream = new MemoryStream();

    //        await formFile.CopyToAsync(stream);

    //        var attachment = new Attachment(formFile.FileName, formFile.ContentType, stream.ToArray(), _currentUser.UserId);

    //        await _unitOfWork.AttachmentRepository.AddAsync(attachment);

    //        await _unitOfWork.SaveChangesAsync();

    //        return attachment.Id;
    //    }
    //}
}
