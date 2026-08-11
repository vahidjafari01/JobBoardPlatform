using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.Mvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieScheme)]
    public class ProfileController : MvcBaseController
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profile = await _userService.GetmyProfile(CurrentUserId, CurrentUserId);
            var model = new EditProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.Username,
                Email = profile.Email,
            };
            ViewData["ResumeId"] = profile.ResumeId;
            ViewData["CreatedAt"] = profile.CreatedAt;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ResumeId"] = Guid.Empty;
                return View("Index", model);
            }

            await _userService.UpdateProfile(new UpdateProfileCommand(
                model.FirstName, model.LastName, model.UserName, model.Email, CurrentUserId, CurrentUserId));

            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return RedirectToAction(nameof(Index));
            }

            await _userService.UpdatePassword(new UpdatePasswordCommand(
                CurrentUserId, CurrentUserId, model.CurrentPassword, model.NewPassword));

            TempData["Success"] = "Password changed successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume()
        {
            var attach = await _userService.DownloadMyResumeAsync(CurrentUserId, CurrentUserId);
            return File(attach.Filedb64, attach.contentType, attach.Filename);
        }

        [HttpPost]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                TempData["Error"] = "Please choose a file.";
                return RedirectToAction(nameof(Index));
            }

            await _userService.UploadResume(CurrentUserId, CurrentUserId, file);
            TempData["Success"] = "Resume uploaded successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResume()
        {
            await _userService.DeleteMyResumeAsync(CurrentUserId, CurrentUserId);
            TempData["Success"] = "Resume deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
