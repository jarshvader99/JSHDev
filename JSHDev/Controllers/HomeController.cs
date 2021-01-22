using JSHDev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JSHDev.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "emailNotSent";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string fromName, string fromEmail, string emailBody)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var sendGridFromEmail = _configuration.GetSection("SENDGRID_FROM_EMAIL").Value;
            var sendGridToEmail = _configuration.GetSection("SENDGRID_TO_EMAIL").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sendGridFromEmail, "JSH | DEV");
            var to = new EmailAddress(sendGridToEmail, "Joshua Harrison");

            var subject = "New Message from " + fromName;
            var htmlContent = "<strong>From: " + fromName + "</strong>" +
                "<strong>Email: " + fromEmail + "</strong>" +
                "<p>Message: " + emailBody + "</p>";
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
            if(response.StatusCode.Equals(HttpStatusCode.Accepted))
            {
                ViewData["Message"] = "emailSent";
            }
            else
            {
                ViewData["Message"] = "emailFail";
            }
            return View(response);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
