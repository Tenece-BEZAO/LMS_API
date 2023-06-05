using LMS.BLL.DTOs.Request;
using LMS.BLL.Interfaces;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }


        public async Task<bool> sendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
           return send(emailMessage) ? true : false;
           // return  

        }


        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message?.Content };
            return emailMessage;
        }


       bool send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                
                
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, SecureSocketOptions.SslOnConnect);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
              
            }

           

        }

       
    }
}
