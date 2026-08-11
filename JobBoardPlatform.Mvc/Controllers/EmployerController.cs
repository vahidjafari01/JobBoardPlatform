using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobBoardPlatform.Mvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieScheme, Roles = "Employer")]
    public class EmployerController : MvcBaseController
    {
        private readonly ICompanyService _companyService;
        private readonly IJobAdServices _jobAdService;
        private readonly IApplicationService _applicationService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployerController(
            ICompanyService companyService,
            IJobAdServices jobAdService,
            IApplicationService applicationService,
            IUnitOfWork unitOfWork)
        {
            _companyService = companyService;
            _jobAdService = jobAdService;
            _applicationService = applicationService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var company = await _unitOfWork.CompanyRepo.GetByUserId(CurrentUserId);
            if (company is null)
                return RedirectToAction(nameof(Company));

            var ads = await _jobAdService.GetMyJobAds(new GetMyJobAdsCommand(CurrentUserId, company.Id));
            return View(ads);
        }

        [HttpGet]
        public async Task<IActionResult> Company()
        {
            var company = await _unitOfWork.CompanyRepo.GetByUserId(CurrentUserId);
            if (company is null)
            {
                ViewData["Cities"] = (await _unitOfWork.CityRepo.GetAllAsync())
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
                return View("CreateCompany", new CompanyFormViewModel());
            }

            var dto = await _companyService.GetMyCompanyDetailAsync(
                new GetCompanyDetailCommand { CompanyId = company.Id, RequesterId = CurrentUserId });

            var model = new CompanyFormViewModel
            {
                Name = dto.Name,
                Description = dto.Description,
                Website = dto.Website,
                Location = dto.Location,
                CityId = company.CityId,
            };
            ViewData["CompanyId"] = company.Id;
            ViewData["LogoId"] = dto.LogoId;
            ViewData["Cities"] = (await _unitOfWork.CityRepo.GetAllAsync())
                .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            return View("EditCompany", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Cities"] = (await _unitOfWork.CityRepo.GetAllAsync())
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
                return View(model);
            }

            await _companyService.CreateCompany(new AddCompanyCommand(
                CurrentUserId, model.Name, model.Description, model.Website, model.Location, model.CityId));

            var user = await _unitOfWork.userManager.FindByIdAsync(CurrentUserId.ToString());
            if (user is not null)
                await SignInUserAsync(user, _unitOfWork);

            TempData["Success"] = "Company created. Waiting for admin approval.";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompany(CompanyFormViewModel model)
        {
            var company = await _unitOfWork.CompanyRepo.GetByUserId(CurrentUserId);
            if (company is null)
                return RedirectToAction(nameof(Company));

            if (!ModelState.IsValid)
            {
                ViewData["CompanyId"] = company.Id;
                ViewData["Cities"] = (await _unitOfWork.CityRepo.GetAllAsync())
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
                return View("EditCompany", model);
            }

            await _companyService.UpdateCompanyAsync(new UpdateCompanyComand
            {
                RequesterId = CurrentUserId,
                CompanyId = company.Id,
                Name = model.Name,
                Description = model.Description,
                Website = model.Website,
                Location = model.Location,
                CityId = model.CityId,
            });

            TempData["Success"] = "Company updated successfully.";
            return RedirectToAction(nameof(Company));
        }

        [HttpPost]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            var company = await _unitOfWork.CompanyRepo.GetByUserId(CurrentUserId);
            if (company is not null && file is not null && file.Length > 0)
            {
                await _companyService.UploadCompanyLogo(company.Id, CurrentUserId, file);
                TempData["Success"] = "Logo uploaded successfully.";
            }
            return RedirectToAction(nameof(Company));
        }

        [HttpGet]
        public async Task<IActionResult> CreateJob()
        {
            return View(await BuildJobFormAsync(new JobAdFormViewModel()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobAdFormViewModel model)
        {
            var company = await _unitOfWork.CompanyRepo.GetByUserId(CurrentUserId);
            if (company is null)
                return RedirectToAction(nameof(Company));

            if (!ModelState.IsValid)
                return View(await BuildJobFormAsync(model));

            await _jobAdService.AddJobAd(new JobAdCreateCommand(
                model.Title, model.Description, model.Location, model.StartWorkTime, model.EndWorkTime,
                model.SalaryMin, model.SalaryMax, model.EmployementType, model.CategoryId, model.CityId,
                CurrentUserId, ParseSkills(model.Skills)));

            TempData["Success"] = "Job ad created successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> EditJob(Guid id)
        {
            var detail = await _jobAdService.GetDetailJobAd(new GetJObAdDetailCommand(CurrentUserId, id));
            var model = new JobAdFormViewModel
            {
                JobAdId = id,
                Title = detail.Title,
                Description = detail.Description,
                Location = detail.Location,
                StartWorkTime = detail.StartWorkTime,
                EndWorkTime = detail.EndWorkTIme,
                SalaryMin = detail.SalaryMin,
                SalaryMax = detail.SalaryMax,
                EmployementType = detail.EmployementType,
                JobAdStatus = detail.Status,
                Skills = detail.Skils is null ? null : string.Join(",", detail.Skils),
            };
            return View(await BuildJobFormAsync(model));
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobAdFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(await BuildJobFormAsync(model));

            await _jobAdService.UpdateJobAd(new JobEditCommand(
                model.Title, model.Description, model.Location, model.StartWorkTime, model.EndWorkTime,
                model.SalaryMin, model.SalaryMax, model.EmployementType, model.JobAdStatus,
                model.CategoryId, model.CityId, CurrentUserId, model.JobAdId, ParseSkills(model.Skills)));

            TempData["Success"] = "Job ad updated successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJob(Guid jobAdId)
        {
            await _jobAdService.DeleteJobAd(new JObAdDeleteCommand { JObAdID = jobAdId, RequesterId = CurrentUserId });
            TempData["Success"] = "Job ad deleted.";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> ActivateJob(Guid jobId)
        {
            await _jobAdService.ActiveMyJObAd(new ActiveJobAdCommand { RequesterId = CurrentUserId, JobId = jobId });
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveJob(Guid jobId)
        {
            await _jobAdService.ArchiveMyJobAd(new ArchiveMyJobAdCommand { RequesterId = CurrentUserId, JobId = jobId });
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> MakePro(Guid jobId)
        {
            await _jobAdService.MakeProJobAd(jobId, CurrentUserId);
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> MakePlus(Guid jobId)
        {
            await _jobAdService.MakePlusJobAd(jobId, CurrentUserId);
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> Applications(Guid jobAdId)
        {
            var apps = await _applicationService.GetAppsByJObAdId(
                new ApplicationJobAdCommand { JobAdId = jobAdId, RequesterId = CurrentUserId });
            ViewData["JobAdId"] = jobAdId;
            return View(apps);
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationDetail(Guid id, Guid? jobAdId)
        {
            var detail = await _applicationService.GetDetailApp(
                new AppDetailCommand { RequesterId = CurrentUserId, AppId = id });
            ViewData["JobAdId"] = jobAdId;
            return View(detail);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume(Guid appId)
        {
            var resume = await _applicationService.GetResumeAsync(CurrentUserId, appId);
            return File(resume.Filedb64, resume.contentType, resume.Filename);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAppStatus(Guid appId, string status, Guid? jobAdId)
        {
            await _applicationService.ChangeApplicationStatusAsync(
                new ChangeAppStatusCommand(CurrentUserId, appId, status));
            return jobAdId.HasValue
                ? RedirectToAction(nameof(Applications), new { jobAdId })
                : RedirectToAction(nameof(Dashboard));
        }

        private async Task<JobAdFormViewModel> BuildJobFormAsync(JobAdFormViewModel model)
        {
            ViewData["Cities"] = (await _unitOfWork.CityRepo.GetAllAsync())
                .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            ViewData["Categories"] = (await _unitOfWork.JobCategoryRepo.GetAllAsync())
                .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            return model;
        }

        private static List<string>? ParseSkills(string? skills) =>
            string.IsNullOrWhiteSpace(skills)
                ? null
                : skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
    }
}
