using EgyShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EgyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactFormObject contactFormObject)
        {
            /*MailMessage mailMesage = new MailMessage("mahmoudreda027@gmail.com", "mahmoudreda027@gmail.com");
            mailMesage.Subject = "Egyshop";
            mailMesage.Body = contactFormObject.Name + " " + contactFormObject.About + " " + contactFormObject.Text;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com" ,587);
            smtpClient.Credentials = new System.Net.NetworkCredential(){
                UserName = "mahmoudreda027@gmail.com" , Password ="Mahm@Captano_#_241"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMesage);
            var x = contactFormObject;*/
            using (StreamWriter writer = new StreamWriter("message.txt", true)) //// true to append data to the file
            {
                writer.WriteLine("from : "+ contactFormObject.Email );
                writer.WriteLine("name : " + contactFormObject.Name);
                writer.WriteLine("about : " + contactFormObject.About);
                writer.WriteLine("subject : " + contactFormObject.Text);
                writer.WriteLine();
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
