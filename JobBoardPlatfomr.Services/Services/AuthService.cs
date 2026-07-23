using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.BaseExceptions;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ICompanyService companyService;
        private readonly IUnitOfWork _unitOfWork;


        public AuthService(IJwtService jwtService, IUnitOfWork unitOfWork, ICompanyService companyService)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _userManager = _unitOfWork.userManager;
            _roleManager = _unitOfWork.roleManager;
            this.companyService = companyService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserNameOrEmail)
                    ?? await _userManager.FindByEmailAsync(dto.UserNameOrEmail);

            if (user is null)
                throw new BadRequestException("the username or password is incorrect");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
                throw new BadRequestException("the username or password is incorrect");


            var roles = await _userManager.GetRolesAsync(user);

            var token =await _jwtService.GenerateTokenAsync(user, roles);
            var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);


            return new AuthResponseDto(token,refreshToken, $"welcome {user.FirstName + " " + user.LastName}", user.Id);

        }

        public async Task<AuthResponseDto> RegisterJobSeekerAsync(RegisterDto registerDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingUserByUserName = await _userManager.FindByNameAsync(registerDto.UserName) is not null;
                var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email) is not null;


                if (existingUserByEmail || existingUserByUserName)
                    throw new BadRequestException("this username is already exsist");


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
                    throw new BadRequestException("eror in regestering");



                var roleResult = await _userManager.AddToRoleAsync(user, "JobSeeker");

                var roles = await _userManager.GetRolesAsync(user);


                var token = await _jwtService.GenerateTokenAsync(user, roles);

                var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);
                await _unitOfWork.CommitTransactionAsync();

                return new AuthResponseDto(token, refreshToken, $"welcome {user.FirstName + " " + user.LastName}", user.Id);
            }catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Eror in Registering",ex);
            }
           

        }
        public async Task<AuthResponseDto> RegisterEmployerAsync(AddCompanyCommand command)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await companyService.CreateCompany(command);


                var user = await _userManager.FindByIdAsync(command.UserId.ToString());

                var roles = await _userManager.GetRolesAsync(user);


                var token = await _jwtService.GenerateTokenAsync(user, roles);
                var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);
                await _unitOfWork.CommitTransactionAsync();
                return new AuthResponseDto(token, refreshToken, $"welcome {user.FirstName + " " + user.LastName}", user.Id);

            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Eror in company Registering",ex);
            }
        }
        public async Task<RefreshDto> Refresh(string RefreshToken,Guid userId)
        {
            var refresht =await _unitOfWork.RefreshTokenRepo.GetTokenByRefreshTokenAsync(RefreshToken);
            if(refresht == null)
            {
                throw new UnauthorizedAccessException("you must login");
            }
            if(refresht.UserId != userId)
            {
                throw new PermisionException("you do not have Permission to this User","403");
            }
            if (refresht.ExpiredAt < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("you must login again");
            }
            if(refresht.IsActive == false)
            {
                throw new UnauthorizedAccessException("you must login again");
            }
            var roles = await _userManager.GetRolesAsync(refresht.User);

            return new RefreshDto(await _jwtService.GenerateTokenAsync(refresht.User, roles),userId);
        }
        public async Task<string> LogOutAsync(string refreshToken,Guid userId)
        {
            var refresht = await _unitOfWork.RefreshTokenRepo.GetTokenByRefreshTokenAsync(refreshToken);
            if (refresht == null)
            {
                return "you were loged out";
            }
            if (refresht.IsActive == false)
            {
                return "you were loged out";
            }
            if (refresht.UserId != userId)
            {
                throw new PermisionException("you do not have Permission to this User", "403");
            }
           
            refresht.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return "you loged out succesfully";
        }

       
    }
}
