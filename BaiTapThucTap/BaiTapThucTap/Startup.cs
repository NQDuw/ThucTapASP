using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDienThoai.Models;

namespace BaiTapThucTap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DBContext>(options => options.UseSqlServer("name=ThucTapConnection"));
            services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();

            services.AddRazorPages();
            services.AddScoped<IEmailSender, EmailSender>();
            services.ConfigureApplicationCookie(op =>
            {
                op.LoginPath = "/Identity/Account/Login";
                op.AccessDeniedPath = "/Identity/AccessDenied";
                op.LogoutPath = "//Identity/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Tạo vai trò Admin và tài khoản Admin nếu chưa có
            CreateRolesAndAdminUser(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        // Phương thức tạo vai trò và tài khoản Admin
        private static void CreateRolesAndAdminUser(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Tạo vai trò Admin nếu chưa có
                CreateRoleIfNotExists(roleManager, SD.Role_Admin).Wait();
                CreateRoleIfNotExists(roleManager, SD.Role_Customer).Wait();
                CreateRoleIfNotExists(roleManager, SD.Role_Employee).Wait();

                // Kiểm tra xem tài khoản Admin đã tồn tại chưa
                var adminUser = userManager.FindByEmailAsync("admin@gmail.com").Result;
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        Id = "1",
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true // Đánh dấu email đã được xác nhận
                    };

                    var adminPassword = "Admin_1"; // Mật khẩu cho tài khoản Admin
                    var createResult = userManager.CreateAsync(adminUser, adminPassword).Result;

                    if (createResult.Succeeded)
                    {
                        // Gán vai trò Admin cho người dùng này
                        userManager.AddToRoleAsync(adminUser, SD.Role_Admin).Wait();
                    }
                }
            }
        }

        // Phương thức tạo vai trò nếu vai trò chưa tồn tại
        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
