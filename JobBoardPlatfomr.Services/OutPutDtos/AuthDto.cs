using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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


    public record AuthResponseDto
    {
        public AuthResponseDto(string? accessTokenToken, string? refreshToken, string message, Guid userId, Guid? companyId)
        {
            AccessTokenToken = accessTokenToken;
            RefreshToken = refreshToken;
            Message = message;
            UserId = userId;
            this.companyId = companyId;
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AccessTokenToken { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RefreshToken { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? companyId { get; set; }

    }
}
