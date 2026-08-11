using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobBoardPlatform.Mvc.Controllers
{
    public class HomeController : MvcBaseController
    {
        private readonly IJobAdServices _jobAdService;
        private readonly IProvinceService _provinceService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IJobAdServices jobAdService, IProvinceService provinceService, IUnitOfWork unitOfWork)
        {
            _jobAdService = jobAdService;
            _provinceService = provinceService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        [HttpGet("Home")]
        [HttpGet("Home/Index")]
        public async Task<IActionResult> Index(GetJObAdFilterCommand? filter, int? page)
        {
            filter ??= new GetJObAdFilterCommand();
            var paging = new Paging { PageNumber = page ?? 1 };

            var model = new HomeIndexViewModel
            {
                Ads = await _jobAdService.GetJobAdsForCustomersAsync(filter, paging),
                Filter = filter,
                Paging = paging,
                Provinces = (await _provinceService.GetAllAsync()).Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList(),
                Cities = (await _unitOfWork.CityRepo.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList(),
                Categories = (await _unitOfWork.JobCategoryRepo.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList(),
            };
            return View(model);
        }

        [HttpGet("Home/Detail/{id:guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var ad = await _jobAdService.GetJobAdDetailForCustomerAsync(id);
            ViewData["JobAdId"] = id;
            return View(ad);
        }

        [AllowAnonymous]
        [HttpGet("Home/Error")]
        public IActionResult Error(string? code, string? message)
        {
            ViewData["ErrorCode"] = code ?? "500";
            ViewData["ErrorMessage"] = message ?? "Something went wrong. Please try again.";
            return View();
        }
    }
}
