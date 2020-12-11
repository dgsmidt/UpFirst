using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LazZiya.ExpressLocalization;
using Web.LocalizationResources;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Web.Services;
using DAL;
using Web.Utilities;
using ViaCep;
using System;
using Microsoft.AspNetCore.Http.Features;
using Web.Hubs;
using Web.Filters;

namespace Web
{
    public class Startup
    {
        public static long Progress { get; set; }
        public static string UploadedFileName { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultures = new[]
            {
                new CultureInfo("pt-br"),
                new CultureInfo("en")

            };

            services.AddCors();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<UpFirstDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("UpFirstConnection")));


            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<HeaderService>();

            // Usado para o Upload de grandes arquivos
            services.Configure<FormOptions>(x =>
            {
                //x.ValueLengthLimit = 5000; // Limit on individual form values
                x.MultipartBodyLengthLimit = long.MaxValue; // Limit on form body size
                //x.MultipartHeadersLengthLimit = int.MaxValue; // Limit on form header size
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = long.MaxValue; // Limit on request body size
            });


            services.AddControllersWithViews();

            services.AddRazorPages(options =>
            {
                options.Conventions
                    .AddPageApplicationModelConvention("/StreamedSingleFileUploadPhysical",
                        model =>
                        {
                            model.Filters.Add(
                                new GenerateAntiforgeryTokenCookieAttribute());
                            model.Filters.Add(
                                new DisableFormValueModelBindingAttribute());
                        });
            })
                //.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(
                    ops =>
                    {
                        // When using all the culture providers, the localization process will
                        // check all available culture providers in order to detect the request culture.
                        // If the request culture is found it will stop checking and do localization accordingly.
                        // If the request culture is not found it will check the next provider by order.
                        // If no culture is detected the default culture will be used.

                        // Checking order for request culture:
                        // 1) RouteSegmentCultureProvider
                        //      e.g. http://localhost:1234/tr
                        // 2) QueryStringCultureProvider
                        //      e.g. http://localhost:1234/?culture=tr
                        // 3) CookieCultureProvider
                        //      Determines the culture information for a request via the value of a cookie.
                        // 4) AcceptedLanguageHeaderRequestCultureProvider
                        //      Determines the culture information for a request via the value of the Accept-Language header.
                        //      See the browsers language settings

                        // Uncomment and set to true to use only route culture provider
                        //ops.UseAllCultureProviders = false;
                        ops.ResourcesPath = "LocalizationResources";
                        ops.RequestLocalizationOptions = o =>
                        {
                            o.SupportedCultures = cultures;
                            o.SupportedUICultures = cultures;
                            o.DefaultRequestCulture = new RequestCulture("en");
                        };
                    });

            services.AddSignalR();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Administrator"));
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");
                    options.CallbackPath = "/signin_google"; // O default é: /signin-google
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.AddSingleton<CultureLocalizer>();

            services.AddHttpClient<IViaCepClient, ViaCepClient>(client =>
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<UploadHub>("/uploadhub");
            });
        }
    }
}
