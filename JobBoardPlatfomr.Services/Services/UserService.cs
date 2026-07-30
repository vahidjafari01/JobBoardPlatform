using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class UserService:IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IAttachService _attachService;

        public UserService(IUnitOfWork unitOfWork, IAttachService attachService)
        {
            _unitOfWork = unitOfWork;
            _attachService = attachService;
        }

        public async Task<UserProfileDto> GetmyProfile(Guid userId,Guid requesterId)
        {

            if (!await IsAdmin(requesterId))
            {
                if (userId != requesterId)
                {
                    throw new PermisionException("this Profile Does not belong to you", "jobAd-403");
                }
            }
            var user =await _unitOfWork.userManager.FindByIdAsync(requesterId.ToString());
            if (user == null || user.IsDeleted == true)
            {
                throw new NotFoundException("User Not found","user-404");
            }
            return new UserProfileDto(user.UserName,user.Email,user.FirstName,user.LastName,user.CreatedAt,user.ResumeId,user.IsActive);
        }
        private async Task<bool> IsAdmin(Guid requesterid)
        {
            var user = await _unitOfWork.userManager.FindByIdAsync(requesterid.ToString());
            return await _unitOfWork.userManager.IsInRoleAsync(user, "Admin");
        }
        public async Task<string> UpdateProfile(UpdateProfileCommand command)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var firstName = command.FirsName?.Trim();
                var lastName = command.lastname?.Trim();
                var email = command.Email?.Trim();
                var userName = command.UserName?.Trim();

                if (!await IsAdmin(command.Requester))
                {
                    if (command.UserId != command.Requester)
                    {
                        throw new PermisionException("This profile does not belong to you.", "user-403");
                    }
                }

                var user = await _unitOfWork.userManager.FindByIdAsync(command.UserId.ToString());
                if (user == null || user.IsDeleted)
                {
                    throw new NotFoundException("User not found.", "user-404");
                }

                if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2)
                {
                    throw new BadRequestException("FirstName must be at least 2 characters.");
                }

                if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2)
                {
                    throw new BadRequestException("LastName must be at least 2 characters.");
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new BadRequestException("Email is required.");
                }

                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new BadRequestException("UserName is required.");
                }

                var userByEmail = await _unitOfWork.userManager.FindByEmailAsync(email);
                if (userByEmail != null && userByEmail.Id != user.Id)
                {
                    throw new BadRequestException("The email already exists.");
                }

                var userByUserName = await _unitOfWork.userManager.FindByNameAsync(userName);
                if (userByUserName != null && userByUserName.Id != user.Id)
                {
                    throw new BadRequestException("The username already exists.");
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.UserName = userName;

                var updateResult = await _unitOfWork.userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    var errors = string.Join(" | ", updateResult.Errors.Select(e => e.Description));
                    throw new BadRequestException(errors);
                }
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return "Profile updated successfully.";
            }
            catch (BadRequestException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (NotFoundException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (PermisionException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (Exception ex) {
            await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }

        }
        public async Task<string> UpdatePassword(UpdatePasswordCommand command)
        {
            if (!await IsAdmin(command.Requester))
            {
                if (command.UserId != command.Requester)
                {
                    throw new PermisionException("This profile does not belong to you.", "jobAd-403");
                }
            }

            var user = await _unitOfWork.userManager.FindByIdAsync(command.UserId.ToString());
            if (user == null || user.IsDeleted)
            {
                throw new NotFoundException("User not found.", "user-404");
            }

            if (string.IsNullOrWhiteSpace(command.CurrentPassword))
            {
                throw new BadRequestException("Current password is required.");
            }

            if (string.IsNullOrWhiteSpace(command.NewPassword))
            {
                throw new BadRequestException("New password is required.");
            }

            if (command.CurrentPassword == command.NewPassword)
            {
                throw new BadRequestException("New password must be different from current password.");
            }

            var result = await _unitOfWork.userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

            return "Password updated successfully.";
        }
            
        

        public async Task<string> DeleteMyResumeAsync(Guid userId, Guid requesterId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (!await IsAdmin(requesterId))
                {
                    if (userId != requesterId)
                    {
                        throw new PermisionException("This profile does not belong to you.", "jobAd-403");
                    }
                }

                var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    throw new NotFoundException("the user not found", "user-404");
                }

                if (user.ResumeId == null)
                {
                    throw new BadRequestException("there is no resume for you");
                }

                await _attachService.HardDeleteAttachmentAsync(user.ResumeId.Value);

                user.ResumeId = null;
                var result = await _unitOfWork.userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                    throw new BadRequestException(errors);
                }

                return "Resume deleted successfully.";
            }
            catch (BadRequestException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (NotFoundException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (PermisionException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }

        }

        public async Task<Guid> UploadResume(Guid userId, Guid requesterId,IFormFile file)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (!await IsAdmin(requesterId))
                {
                    if (userId != requesterId)
                    {
                        throw new PermisionException("This profile does not belong to you.", "jobAd-403");
                    }
                }
                var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    throw new NotFoundException("the user not found", "user-404");
                }
                var AttachId =await _attachService.UploadAsync(file);
                user.ResumeId = AttachId;
                var result = await _unitOfWork.userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new BadRequestException(string.Join(" | ", result.Errors.Select(e => e.Description)));
                }
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return AttachId;
            }
            catch (BadRequestException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (NotFoundException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (PermisionException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }
        public async Task<AttachOutputDto> DownloadMyResumeAsync(Guid requesterId,Guid userId)
        {
            if (!await IsAdmin(requesterId))
            {
                if (userId != requesterId)
                {
                    throw new PermisionException("This profile does not belong to you.", "jobAd-403");
                }
            }
            var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException("the user not found", "user-404");
            }
            if(user.ResumeId == null)
            {
                throw new NotFoundException("Resume Not found...please upload your Resume","Resume-404");
            }
            return await _attachService.DownloadAsync(user.ResumeId.Value);
        }
        public async Task<string> DeleteUser(Guid userId,Guid requesterId)
        {
            if (!await IsAdmin(requesterId))
            {
                if (userId != requesterId)
                {
                    throw new PermisionException("This profile does not belong to you.", "jobAd-403");
                }
            }
            var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException("the user not found", "user-404");
            }
            user.IsDeleted = true;
            var result = await _unitOfWork.userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(" | ", result.Errors.Select(e => e.Description)));
            }
            var refreshtokens =await _unitOfWork.RefreshTokenRepo.QueryAsync(r => r.UserId == userId,true);
            if (refreshtokens is not null && refreshtokens.Any())
            {
                foreach (var refreshtoken in refreshtokens)
                {
                    refreshtoken.IsActive = false;
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return "User Deleted succesfully";
        }




    }
}
