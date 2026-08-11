using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatform.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobBoardPlatform.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
    }

    public class LoginViewModel
    {
        [Required]
        public string UserNameOrEmail { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }

    public class EditProfileViewModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class ChangePasswordViewModel
    {
        [Required, DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class CompanyFormViewModel
    {
        [Required, MinLength(3)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Website { get; set; }
        [Required, MinLength(10)]
        public string Location { get; set; } = null!;
        [Required]
        public Guid CityId { get; set; }
    }

    public class JobAdFormViewModel
    {
        public Guid JobAdId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
        public TimeSpan StartWorkTime { get; set; } = new TimeSpan(9, 0, 0);
        public TimeSpan EndWorkTime { get; set; } = new TimeSpan(17, 0, 0);
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        [Required]
        public string EmployementType { get; set; } = "FullTime";
        public string JobAdStatus { get; set; } = "Published";
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid CityId { get; set; }
        public string? Skills { get; set; }
    }

    public class AppStatusViewModel
    {
        public Guid AppId { get; set; }
        [Required]
        public string status { get; set; } = null!;
    }

    public class HomeIndexViewModel
    {
        public List<JobAdViewModel> Ads { get; set; } = new();
        public GetJObAdFilterCommand Filter { get; set; } = new();
        public Paging Paging { get; set; } = new();
        public List<SelectListItem> Provinces { get; set; } = new();
        public List<SelectListItem> Cities { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
    }
}
