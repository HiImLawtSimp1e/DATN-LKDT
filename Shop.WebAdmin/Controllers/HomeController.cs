using Microsoft.AspNetCore.Mvc;
using shop.Infrastructure.Business;
using shop.Infrastructure.Business.VirtualItem;
using Shop.WebAdmin.Models;
using System.Diagnostics;

namespace Shop.WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVirtualItemBusiness _virtualItemBusiness;

        public HomeController(IVirtualItemBusiness virtualItemBusiness)
        {
            _virtualItemBusiness=virtualItemBusiness;
        }

        public  IActionResult Index()
        {
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
