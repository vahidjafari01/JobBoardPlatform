using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record RegisterDto(
         [Required]
        string UserName,
          [Required]
        string Password,
         [Required]
        string Email,
          [Required]
        string firstName,
          [Required]
        string lastName
     );
    
    public record LoginDto(
        [Required]
        string UserNameOrEmail,
         [Required]
        string Password

    );


    public record AuthResponseDto(
        string? AccessTokenToken,
        string? RefreshToken,
        string Message,
        Guid UserId
        );
}
