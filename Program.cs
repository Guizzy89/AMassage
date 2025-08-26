using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AMassage.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и представление (Razor Pages)
builder.Services.AddControllersWithViews();

// Используем Cookie-аутентификацию
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/Account/Login");

// Регистрируем Identity (если планируется использование встроенной авторизации)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Регистрируем контекст базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройки JSON сериализации (опционально)
builder.Services.AddMvc().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Обрабатываем исключения и запускаем приложение
var app = builder.Build();

// Проверка режима разработки
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Включаем HSTS для продакшена
}

// Дополнительная настройка безопасности
app.UseHttpsRedirection();
app.UseStaticFiles();

// Аутентификация и авторизация
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Маршруты контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запускаем приложение
app.Run();