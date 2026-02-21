using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 商品展示系統.Data;
using 商品展示系統.Models; // 確保有引用 Model

namespace 商品展示系統.Controllers
{
    public class MyFavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyFavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. 顯示收藏頁面
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var myFavoriteProducts = await (from f in _context.Favorites
                                            join p in _context.Products on f.Prod_Id equals p.Prod_Id
                                            where f.User_Id == userId
                                            select p).ToListAsync();

            return View(myFavoriteProducts);
        }

        // 2. 【新增】處理愛心點擊的 Action
        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int prodId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "請先登入" });

            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.User_Id == userId && f.Prod_Id == prodId);

            if (existing == null)
            {
                _context.Favorites.Add(new Favorite { User_Id = (int)userId, Prod_Id = prodId });
                await _context.SaveChangesAsync();
                return Json(new { success = true, status = "added" });
            }
            else
            {
                _context.Favorites.Remove(existing);
                await _context.SaveChangesAsync();
                return Json(new { success = true, status = "removed" });
            }
        }
    }
}
