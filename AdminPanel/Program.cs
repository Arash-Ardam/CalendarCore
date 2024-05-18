using AdminPanel.Controllers;
using AdminPanel.IdentityConfigs;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace AdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddProblemDetails();
            builder.AddIdentityConfig();
            builder.Services.AddMvc()
                .AddRazorRuntimeCompilation();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient<MyCalendarApi>(async (services, httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");

                var httpContext = services.GetRequiredService<IHttpContextAccessor>();

                var accessToken = await httpContext.HttpContext.GetTokenAsync("access_token");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
