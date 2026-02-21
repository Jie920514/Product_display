using Microsoft.AspNetCore.Mvc;
using 商品展示系統.Data;
using 商品展示系統.Models;
using Microsoft.AspNetCore.Http; // 為了使用 Session

namespace 商品展示系統.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        //------------------------------我是分隔線------------------------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string account, string password)
        {
            var user = _context.Members.FirstOrDefault(m => m.Account == account && m.Password == password);
            //找不到為null

            if (user != null)
            {
                int authValue = user.Permissions ? 1 : 0;
                HttpContext.Session.SetInt32("Permissions", authValue);
                HttpContext.Session.SetInt32("UserId", user.Member_Id);
                HttpContext.Session.SetString("UserName", user.Name ?? user.Account);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "帳號或密碼錯誤！";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // HttpContext.Session.Remove("UserId"); //刪除Session特定項目
            return RedirectToAction("Index", "Home");
        }

        //------------------------------我是分隔線------------------------------
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Member member, string password_check)
        //非同步處理
        {
            if (member.Password != password_check)
            {
                ViewBag.Error = "兩次輸入的密碼不一致，請重新輸入。";
                return View(member);
            }

            if (ModelState.IsValid)
            {
                var exists = _context.Members.Any(m => m.Account == member.Account);
                if (exists)
                {
                    ViewBag.Error = "此帳號已被使用，請換一個。";
                    return View(member);
                }

                _context.Members.Add(member);
                await _context.SaveChangesAsync(); // 確保資料入庫後，才繼續執行後面的邏輯

                HttpContext.Session.SetInt32("UserId", member.Member_Id);
                HttpContext.Session.SetString("UserName", member.Name ?? member.Account);

                return RedirectToAction("Index", "Home");
            }

            return View(member);
        }

        //------------------------------我是分隔線------------------------------
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(string Email)
        {
            var user = _context.Members.FirstOrDefault(m => m.Email == Email);

            if (user == null)
            {
                ViewBag.Error = "此電子信箱尚未註冊！";
                return View();
            }

            ViewBag.Success = $"找回成功！您的帳號是：{user.Account}，密碼已寄至您的信箱。";
            // ViewBag 模擬
            // 寄信服務：SendEmail(user.Email, user.Account, user.Password);

            return View();
        }

        //------------------------------我是分隔線------------------------------
        public IActionResult Modify()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            if (HttpContext.Session.GetInt32("Permissions") == 1)
            {
                TempData["Error"] = "管理員帳號禁止修改資料";
                return RedirectToAction("Index", "Home");
            }

            var user = _context.Members.Find(userId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(Member updateData , string password_check)
        {
            if (updateData.Password != password_check)
            {
                ViewBag.Error = "兩次輸入的密碼不一致，請重新輸入。";
                return View(updateData);
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || HttpContext.Session.GetInt32("Permissions") == 1)
                return RedirectToAction("Index", "Home");

            var user = _context.Members.Find(userId);
            if (user != null)
            {
                user.Name = updateData.Name;
                user.Email = updateData.Email;
                user.Password = updateData.Password; //密碼需做更嚴格的確認

                _context.Update(user);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("UserName", user.Name ?? user.Account);
                TempData["Success"] = "資料已成功更新！";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
