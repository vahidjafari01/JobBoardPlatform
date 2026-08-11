using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatform.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.Mvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieScheme, Roles = "Admin")]
    public class AdminController : MvcBaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var dto = await _adminService.GetAdminDashboardAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> JobSeekers(int? page)
        {
            var users = await _adminService.GetJobSeekers(new Paging { PageNumber = page ?? 1, PageSize = 10 });
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> JobSeekerDetail(Guid id)
        {
            var profile = await _adminService.GetJobseekerDetailAsync(id, CurrentUserId);
            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> Employers(int? page)
        {
            var employers = await _adminService.GetEmployerAsync(new Paging { PageNumber = page ?? 1, PageSize = 10 });
            return View(employers);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveCompany(Guid companyId)
        {
            await _adminService.SetApprovedCompanyAsync(companyId);
            return RedirectToAction(nameof(Employers));
        }

        [HttpPost]
        public async Task<IActionResult> RejectCompany(Guid companyId)
        {
            await _adminService.SetNotApprovedCompanyAsync(companyId);
            return RedirectToAction(nameof(Employers));
        }

        [HttpGet]
        public async Task<IActionResult> JobAds(int? page)
        {
            var ads = await _adminService.GetAllJobAds(new Paging { PageNumber = page ?? 1, PageSize = 10 });
            return View(ads);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateJobAd(Guid jobAdId)
        {
            await _adminService.ActivateJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveJobAd(Guid jobAdId)
        {
            await _adminService.ArchiveJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> CloseJobAd(Guid jobAdId)
        {
            await _adminService.CloseJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> MakeNormalJobAd(Guid jobAdId)
        {
            await _adminService.MakeNormalJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> MakeProJobAd(Guid jobAdId)
        {
            await _adminService.MakeProJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> MakePlusJobAd(Guid jobAdId)
        {
            await _adminService.MakePlusJobAd(jobAdId);
            return RedirectToAction(nameof(JobAds));
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(Guid userId)
        {
            await _adminService.ActivateUser(userId);
            return RedirectToAction(nameof(JobSeekers));
        }

        [HttpPost]
        public async Task<IActionResult> DeactivateUser(Guid userId)
        {
            await _adminService.DeactivateUser(userId);
            return RedirectToAction(nameof(JobSeekers));
        }
    }
}
