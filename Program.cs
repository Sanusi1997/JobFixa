using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobFixa.Data;
using JobFixa.Services.Interfaces;
using JobFixa.Services.JobFixaServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<JobFixaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobFixaDatabase")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<JobFixaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobFixaDatabase")));


builder.Services.AddScoped<IJobFixaUserService, JobFixaUserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
