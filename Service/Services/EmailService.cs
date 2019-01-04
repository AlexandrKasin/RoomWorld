using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Service.Exceptions;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new InvalidDataException("Incorrect email address");
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("RoomWorld email service",
                _configuration["EmailSettings:EmailFromLogin"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["EmailSettings:EmailSmtp"],
                    int.Parse(_configuration["EmailSettings:EmailClientPort"]), false);
                await client.AuthenticateAsync(_configuration["EmailSettings:EmailFromLogin"],
                    _configuration["EmailSettings:EmailFromPassword"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}