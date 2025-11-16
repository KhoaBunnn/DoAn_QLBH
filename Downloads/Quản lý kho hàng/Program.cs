using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using QLKhoHang.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

// 2. Thêm DbContext với chuỗi kết nối
builder.Services.AddDbContext<QLKhoHangContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLKhoHangConnection"))
           .EnableSensitiveDataLogging() // Hiển thị dữ liệu tham số
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information); // In ra console
});

// 3. Thêm Repository
builder.Services.AddScoped<IKhoRepository, KhoRepository>();
builder.Services.AddScoped<ILoaiHangRepository, LoaiHangRepository>();
builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();

// 4. Build app
var app = builder.Build();

// 5. Cấu hình pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 6. Map route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
