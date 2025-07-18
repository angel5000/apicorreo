using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace Correos.MailKit
{
    public class MailKIT
    {
        public async Task SendEmailAsync(MailRequest request)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = Path.Combine(baseDirectory, "MailKit", "plantilla.html");

            // Verificar si el archivo existe
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"No se encontró el archivo de plantilla en: {templatePath}");
            }

            var htmlBody = await File.ReadAllTextAsync(templatePath);
            htmlBody = htmlBody.Replace("{Nombre}", request.usuario);
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("joselitorugel17@gmail.com"));
            email.To.Add(MailboxAddress.Parse(request.toEmail));
            email.Subject = request.subject;
           
            // Construye el cuerpo del mensaje con multipart
            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
          
            // Adjuntar archivo si viene incluido
            if (request.Attachment != null && request.Attachment.Length > 0)
            {
              //  using var ms = new MemoryStream();
            //   await request.Attachment.CopyToAsync(ms);
               // builder.Attachments.Add( "archivo.pdf", request.Attachment, ContentType.Parse( "application/pdf"));
                if (!string.IsNullOrEmpty(request.Attachment))
                {
                    var fileBytes = Convert.FromBase64String(request.Attachment);
                    builder.Attachments.Add(
                        "archivo.pdf",
                        fileBytes,
                        ContentType.Parse("application/pdf")
                    );
                }

            }
        
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("joselitorugel17@gmail.com", "hbow qzjq elqn tlyz");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
