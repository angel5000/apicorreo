using Correos.MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Correos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreosController : ControllerBase
    {
        private readonly MailKIT _mail;
        public CorreosController(MailKIT mail)
        {

            _mail = mail;
        }

        [HttpPost("EnviarCorreos")]
        public async Task<IActionResult> EnviarCorreos([FromBody] MailRequest request)
        {
            await _mail.SendEmailAsync(request);
            return Ok();
         /*   if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);*/
        }
    }
}
