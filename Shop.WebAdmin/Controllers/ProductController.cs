using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Domain.Entities;
using shop.Infrastructure.Business;
using shop.Infrastructure.Model;

namespace Shop.WebAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IVirtualItemBusiness _virtualItemBusiness;

        public ProductController(IVirtualItemBusiness virtualItemBusiness)
        {
            _virtualItemBusiness=virtualItemBusiness;
        }

        // GET: ProductController1
        public async  Task<IActionResult> ListProduct()
        {
            var data = await _virtualItemBusiness.GetAllAsync(new VirtualItemQueryModel());

            return View(data.Content);
        }

        // GET: ProductController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
