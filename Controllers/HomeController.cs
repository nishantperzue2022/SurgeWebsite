using Microsoft.AspNetCore.Mvc;
using surgeweb.IService;
using surgeweb.Models;
using System.Diagnostics;

namespace surgeweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;

        public HomeController(ILogger<HomeController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Capacity()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Certifications()
        {
            return View();
        }
        public IActionResult RequestQuote()
        {
            return View();
        }
        public IActionResult CallBackRequest()
        {
            return View();
        }

        public IActionResult Sustainability()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> PostRequest(QuoteRequest mailRequest)
        {
            try
            {
                await _mailService.SendEmailQuoteWithTemplate(mailRequest);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }

        }

        public async Task<IActionResult> SendMail(MailRequest mailRequest)
        {
            try
            {
                await _mailService.SendEmailWithTemplate(mailRequest);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
