using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcCreditApp.Models;
using MvcCreditApp.Data;

namespace MvcCreditApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CreditContext db;

    // Конструктор — DI (Dependency Injection) подаёт зависимости автоматически
    public HomeController(ILogger<HomeController> logger, CreditContext context)
    {
        _logger = logger;
        db = context;
    }

    // return View() — ищет Views/Home/Index.cshtml. ViewBag передаёт данные в представление
    public IActionResult Index()
    {
        GiveCredits();
        return View();
    }

    private void GiveCredits()
    {
        var allCredits = db.Credits.ToList<Credit>();
        ViewBag.Credits = allCredits;
    }

    [HttpGet]
    [Authorize] // без логина — редирект на страницу входа. Identity ограничивает доступ
    public ActionResult CreateBid()
    {
        GiveCredits();
        var allBids = db.Bids.ToList<Bid>();
        ViewBag.Bids = allBids;
        return View();
    }

    [HttpPost]
    [Authorize]
    public string CreateBid(Bid newBid)
    {
        newBid.bidDate = DateTime.Now;
        db.Bids.Add(newBid);
        db.SaveChanges();
        return "Спасибо, " + newBid.Name + ", за выбор нашего банка. Ваша заявка будет рассмотрена в течении 10 дней.";
    }

    // AJAX-метод — вызывается через fetch(), возвращает частичное представление (PartialView)
    public IActionResult BidSearch(string creditHead)
    {
        var bids = db.Bids.Where(b => b.CreditHead == creditHead).ToList();
        return PartialView("_BidSearchPartial", bids);
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
