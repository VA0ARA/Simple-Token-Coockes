using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;

namespace One
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            #region ServiceFor Coockes
            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
               // options.LoginPath = "/Account1/Login";
               options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
            });
            #endregion
            #region create Polciles
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("MustBelongToHRDepartment", policy => policy.RequireClaim("Department", "HR"));
                options.AddPolicy("HRManagerOnly", policy => policy
                  .RequireClaim("Department", "HR")
                  .RequireClaim("Manager")
                  .Requirements.Add(new HRManagerProbationRequirement(3)));
                //use custom policy
            });
            #endregion
            #region  custom policy-cookes
            builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();
            #endregion
            #region connect to API
            builder.Services.AddHttpClient("OurWebAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7033/");
            });
            #endregion
            #region Session
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapRazorPages();

            app.Run();
        }
    }
}
