using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AMassage.Data;

var builder = WebApplication.CreateBuilder(args);

// ��������� ����������� � ������������� (Razor Pages)
builder.Services.AddControllersWithViews();

// ���������� Cookie-��������������
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/Account/Login");

// ������������ Identity (���� ����������� ������������� ���������� �����������)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ������������ �������� ���� ������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� JSON ������������ (�����������)
builder.Services.AddMvc().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// ������������ ���������� � ��������� ����������
var app = builder.Build();

// �������� ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // �������� HSTS ��� ����������
}

// �������������� ��������� ������������
app.UseHttpsRedirection();
app.UseStaticFiles();

// �������������� � �����������
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// �������� ������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ��������� ����������
app.Run();