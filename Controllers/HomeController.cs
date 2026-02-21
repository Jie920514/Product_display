using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using 商品展示系統.Data;
using 商品展示系統.Models;

namespace 商品展示系統.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // ?
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var products = from p in _context.Products select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => (s.Prod_Name ?? "").Contains(searchString)
                                            || (s.Prod_Brand ?? "").Contains(searchString));
                //在Prod_Name & Prod_Brand 設定如果為 null 直接當空字串 (防警告)
            }

            return View("Homepage",await products.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> ToFavorite(int prodId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "請先登入" });
            var existingFav = await _context.Favorites
                .FirstOrDefaultAsync(f => f.User_Id == userId && f.Prod_Id == prodId);
            if (existingFav == null)
            {
                _context.Favorites.Add(new Favorite
                {
                    User_Id = (int)userId,
                    Prod_Id = prodId
                });
                await _context.SaveChangesAsync();
                return Json(new { success = true, states = "added" });
            }
            else
            {
                _context.Favorites.Remove(existingFav);
                await _context.SaveChangesAsync();
                return Json(new { success = true, status = "removed" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // 展示錯誤 (正式環境)
    }
}
