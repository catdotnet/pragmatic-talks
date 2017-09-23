using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PragmaticTalks.Data;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PragmaticTalks
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<PragmaticContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Speaker, IdentityRole>()
               .AddEntityFrameworkStores<PragmaticContext>()
               .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                })
                .AddMicrosoftAccount(microsoftOptions => {
                    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"]; ;
                    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                })
                .AddTwitter(twitterOptions => {
                    twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ClientId"]; ;
                    twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ClientSecret"];
                    twitterOptions.RetrieveUserDetails = true;
                    twitterOptions.SaveTokens = true;
                });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, PragmaticContext context)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new Microsoft.AspNetCore.SpaServices.Webpack.WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            TryToCreateDatabase(context);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pragmatic Talks API V1");
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapSpaFallbackRoute(
                    name: "fallback",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }

        private static void TryToCreateDatabase(PragmaticContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();

            var tags = new Dictionary<string, string> {
                    { "front", "pink" },
                    { "back","red" },
                    { "data","purple" },
                    { "dotnet","deep-purple" },
                    { "js","yellow" },
                    { "node","green" },
                    { "other languages","grey" },
                    { "library","blue-grey" },
                    { "cloud","blue" },
                    { "mobile","cyan" },
                    { "desktop","indigo" },
                    { "infrastructure","amber" },
                    { "big data","deep-orange" },
                    { "devops","light-blue" },
                    { "web","lime" },
                    { "IoT","teal" },
                    { "games", "orange" }
            };

            if (!context.Tags.Any())
            {
                foreach (var kv in tags)
                {
                    var tag = new Data.Tag { Name = kv.Key, Color = kv.Value };
                    context.Tags.Add(tag);
                }

                context.SaveChanges();
            }
        }
    }
}
