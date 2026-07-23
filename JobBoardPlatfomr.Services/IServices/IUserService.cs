using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IUserService
    {
        Task<UserProfileDto> GetmyProfile(Guid userId, Guid requesterId);
        Task<string> UpdateProfile(UpdateProfileCommand command);

        Task<string> UpdatePassword(UpdatePasswordCommand command);
        Task<string> DeleteMyResumeAsync(Guid userId, Guid requesterId);
        Task<Guid> UploadResume(Guid userId, Guid requesterId, IFormFile file);
        Task<AttachOutputDto> DownloadMyResumeAsync(Guid requesterId, Guid userId);
        Task<string> DeleteUser(Guid userId, Guid requesterId);
    }
}
