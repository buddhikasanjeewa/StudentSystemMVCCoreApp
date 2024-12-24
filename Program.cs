using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

using SoftOneStudentSystemWebApi.RequestModel;
using StudentBL;
using StudentSystemMvcCore.DBModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GitstudentContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("StuConStr")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//builder.Services.AddScoped<IStudentService, StudentService>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
