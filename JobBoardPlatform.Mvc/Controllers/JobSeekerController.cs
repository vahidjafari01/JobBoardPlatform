using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.InputDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.Mvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieScheme, Roles = "JobSeeker")]
    public class JobSeekerController : MvcBaseController
    {
        private readonly IApplicationService _applicationService;

        public JobSeekerController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var apps = await _applicationService.GetAppsForJobSeekerAsync(CurrentUserId, CurrentUserId);
            return View(apps);
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationDetail(Guid id)
        {
            var detail = await _applicationService.GetAppDetailForJobSeekerAsync(CurrentUserId, id);
            return View(detail);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume(Guid appId)
        {
            var resume = await _applicationService.GetResumeAsync(CurrentUserId, appId);
            return File(resume.Filedb64, resume.contentType, resume.Filename);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(Guid jobAdId, string? note)
        {
            await _applicationService.CreateApplicationAsync(
                new CreateAppCommand(CurrentUserId, CurrentUserId, jobAdId, note));
            TempData["Success"] = "Application submitted successfully.";
            return RedirectToAction("Detail", "Home", new { id = jobAdId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(Guid appId)
        {
            await _applicationService.CancellMyApp(CurrentUserId, appId);
            TempData["Success"] = "Application canceled.";
            return RedirectToAction(nameof(MyApplications));
        }
    }
}
