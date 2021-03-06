﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Common.Interfaces;
using Notifications.DataAccess;
using Notifications.DataAccess.Access;
using Notifications.Services;
using Notifications.Utils;
using Swashbuckle.AspNetCore.Swagger;

namespace Notifications
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddCors(options => { options.AddPolicy("Allow", builder => builder.AllowAnyOrigin()); });

            var connection = @"Server=.;Database=notifications-db;User ID=sa;password=R3adytolean;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<NotificationsDbContext>
                (options => options.UseSqlServer(connection));

            services.AddTransient<INotificationsAccess, NotificationsAccess>();
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddSingleton<IBodyParser, BodyParser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
