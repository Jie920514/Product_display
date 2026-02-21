using Microsoft.EntityFrameworkCore;
using 商品展示系統.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. 資料庫設定
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. 註冊 Controller 與 View 服務
builder.Services.AddControllersWithViews();

// 3. 加入 Session 服務 (服務註冊必須在 builder.Build 之前)
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// --- HTTP 請求管道設定 (Middleware 順序極其重要) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   // 讀取 CSS/JS 放在最前面

app.UseRouting();      // 1. 定位路由 (先執行)

// --- 關鍵：Session 必須放在 Routing 之後，Authorization 之前 ---
app.UseSession();      // 2. 啟用 Session

app.UseAuthorization(); // 標籤檢查 || 原則檢查

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();