using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using QLKhoHang.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

// 2. Thêm DbContext với logging SQL
builder.Services.AddDbContext<QLKhoHangContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Đăng ký Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // repository tổng quát
builder.Services.AddScoped<IRepository<LoaiHang>, Repository<LoaiHang>>(); // repository riêng cho LoaiHang

var app = builder.Build();

// 4. Cấu hình pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 5. Map route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
