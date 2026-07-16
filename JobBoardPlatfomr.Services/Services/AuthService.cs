using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<RoleEntity> _roleManager;


        public AuthService(IJwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<RoleEntity> roleManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserNameOrEmail)
                    ?? await _userManager.FindByEmailAsync(dto.UserNameOrEmail);

            if (user is null)
                return new AuthResponseDto(false, null, "با اطلاعات ورودی  کاربری یافت نشد.", null);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
                return new AuthResponseDto(false, null, "با اطلاعات ورودی  کاربری یافت نشد.", null);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);

            return new AuthResponseDto(true, token, $"کاربر {user.FirstName + " " + user.LastName} به سیستم خوش امدید .", user);

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUserByUserName = await _userManager.FindByNameAsync(registerDto.UserName) is not null;
            var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email) is not null;


            if (existingUserByEmail || existingUserByUserName)
                return new AuthResponseDto(false, null, "با این اطلاعات کاربری ثبت شده است.", null);


            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.firstName,
                LastName = registerDto.lastName,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return new AuthResponseDto(false, null, "خطا در ثبت نام کاربر.", null);



            var roleResult = await _userManager.AddToRoleAsync(user, "JobSeeker");

            var roles = await _userManager.GetRolesAsync(user);


            var token = _jwtService.GenerateToken(user, roles);
            return new AuthResponseDto(true, token, $"کاربر {user.FirstName + " " + user.LastName} به سیستم خوش امدید .", user);

        }
    }
}
