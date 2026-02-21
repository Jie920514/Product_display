using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 商品展示系統.Data;
using 商品展示系統.Models;
using System.Linq;

namespace 商品展示系統.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool Permissions()
        {
            return HttpContext.Session.GetInt32("Permissions") == 1;
        }

        // 產品列表頁
        public async Task<IActionResult> Index()
        {
            if (!Permissions())
            {
                TempData["Error"] = "權限不足，請以管理員帳號登入。";
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.Products.ToListAsync());
        }

        // 新增產品頁 (GET)
        public IActionResult Create()
        {
            if (!Permissions()) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!Permissions()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "商品加入成功！";
                return RedirectToAction(nameof(Index));
            }

            var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["ErrorMessage"] = "加入失敗！原因：" + string.Join(", ", errorList);
            return View(product);
        }

        [HttpGet]
        public async Task<JsonResult> GetProductJson(int id)
        {
            if (!Permissions()) return Json(null);
            var product = await _context.Products.FindAsync(id);
            return Json(product);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(Product product)
        {
            if (!Permissions()) return RedirectToAction("Index", "Home");

            if (product.Prod_Id == 0) _context.Add(product);
            else _context.Update(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet] // 需優化[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!Permissions()) return RedirectToAction("Index", "Home");

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "商品已成功刪除！";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet] // 需優化[HttpPost]
        public async Task<IActionResult> BulkDelete(string ids)
        {
            if (!Permissions()) return RedirectToAction("Index", "Home");

            if (!string.IsNullOrEmpty(ids))
            {
                var idList = ids.Split(',').Select(int.Parse);
                var products = _context.Products.Where(p => idList.Contains(p.Prod_Id));
                _context.Products.RemoveRange(products);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
