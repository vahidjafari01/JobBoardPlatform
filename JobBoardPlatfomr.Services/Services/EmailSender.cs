using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public EmailSender(IOptions<EmailSettings> settings, IUnitOfWork unitOfWork)
        {
            _settings = settings.Value;
            _unitOfWork = unitOfWork;
        }



        private async Task SendEmailAsync(string to, string subject, string body, bool isHtml)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

            message.To.Add(MailboxAddress.Parse(to));

            message.Subject = subject;

            message.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
            {
                Text = body
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();


            var secureOption = _settings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

            await client.ConnectAsync(_settings.Host, _settings.Port, secureOption);

            await client.AuthenticateAsync(_settings.UserName, _settings.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
        public async Task SendAsync(Guid userId, string subject, string body)
        {
            var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException("user not found", "user-404");
            }
            try
            {
                await SendEmailAsync(user.Email, subject, body, false);
                Console.WriteLine($"was sent to {user.Email}");
            }
            catch (Exception)
            {
                Console.WriteLine("eror in sending email");
            }
        }
    }
}
