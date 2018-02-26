using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using Newtonsoft.Json;

namespace MyCodeCamp
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();

      _env = env;
      _config = builder.Build();
    }

    IConfigurationRoot _config;
    private IHostingEnvironment _env;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton(_config);
      services.AddDbContext<CampContext>(ServiceLifetime.Scoped);
      services.AddScoped<ICampRepository, CampRepository>();
      services.AddTransient<CampDbInitializer>();
      services.AddTransient<CampIdentityInitializer>();

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddAutoMapper();

      services.AddIdentity<CampUser, IdentityRole>()
        .AddEntityFrameworkStores<CampContext>();

      services.Configure<IdentityOptions>(config =>
      {
        config.Cookies.ApplicationCookie.Events =
          new CookieAuthenticationEvents()
          {
            OnRedirectToLogin = (ctx) =>
            {
              if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
              {
                ctx.Response.StatusCode = 401;
              }

              return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = (ctx) =>
            {
              if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
              {
                ctx.Response.StatusCode = 403;
              }

              return Task.CompletedTask;
            }
          };
      });

      services.AddCors(cfg =>
      {
        cfg.AddPolicy("Wildermuth", bldr =>
        {
          bldr.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://wildermuth.com");
        });

        cfg.AddPolicy("AnyGET", bldr =>
        {
          bldr.AllowAnyHeader()
              .WithMethods("GET")
              .AllowAnyOrigin();
        });


      });

      // Add framework services.
      services.AddMvc(opt =>
      {
        if (!_env.IsProduction())
        {
          opt.SslPort = 44388;
        }
        opt.Filters.Add(new RequireHttpsAttribute());
      })
        .AddJsonOptions(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
      IHostingEnvironment env, 
      ILoggerFactory loggerFactory,
      CampDbInitializer seeder,
      CampIdentityInitializer identitySeeder)
    {
      loggerFactory.AddConsole(_config.GetSection("Logging"));
      loggerFactory.AddDebug();

      //app.UseCors(cfg =>
      //{
      //  cfg.AllowAnyHeader()
      //     .AllowAnyMethod()
      //     .WithOrigins("http://wildermuth.com");
      //});

      app.UseIdentity();

      app.UseMvc(config =>
      {
        //config.MapRoute("MainAPIRoute", "api/{controller}/{action}");
      });

      seeder.Seed().Wait();
      identitySeeder.Seed().Wait();
    }
  }
}
