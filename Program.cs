using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tuto.Data;
namespace tuto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<tutoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("tutoContext") ?? throw new InvalidOperationException("Connection string 'tutoContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
                options.IdleTimeout = TimeSpan.FromMinutes(120)
            ); ;


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
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=Logins}/{action=Login}/{id?}");

            app.Run();
        }
    }
}