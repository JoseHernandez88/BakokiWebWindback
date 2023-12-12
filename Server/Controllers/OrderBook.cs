using BakokiWeb.Shared;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MimeKit;

namespace BakokiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderBook : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendOrderBookEmail([FromBody] string OrderBookEmail)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("bankokiservices@gmail.com"));
            message.To.Add(MailboxAddress.Parse("bankokiservices@gmail.com"));
            message.Subject = "Checkbook order";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = OrderBookEmail };

            using var smtp = new SmtpClient();
            /*for when oauth is available*/
            //smtp.Connect("smtp.gmail.com", 465 ,true);
            //smtp.Authenticate("bankokiservices@gmail.com", "Algobuenoyfacil01");
            smtp.Connect("smtp.freesmtpservers.com", 25, false);
            await smtp.SendAsync(message);
            smtp.Dispose();

            
            return Ok();
        }
    }
}
