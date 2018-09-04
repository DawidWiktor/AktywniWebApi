using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Mappers;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Aktywni.Infrastructure.Settings;
using Aktywni.Infrastructure.Repositories;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Aktywni.Core.Model;
using Microsoft.AspNetCore.HttpOverrides;

namespace Aktywni.Api
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
            services.AddMvc();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IObjectService, ObjectService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserEventService, UserEventService>();
            services.AddScoped<IMessageUserService, MessageUserService>();
            services.AddScoped<IMessageEventService, MessageEventService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IObjectRepository, ObjectRepository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserEventRepository, UserEventRepository>();
            services.AddScoped<IMessageUserRepository, MessageUserRepository>();
            services.AddScoped<IMessageEventRepository, MessageEventRepository>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.AddSingleton<IJwtHandler, JwtHandler>();

            services.AddDbContext<AktywniDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DawidDB")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.Audience = Configuration.GetSection("jwt:Audience").Value;
                options.ClaimsIssuer = Configuration.GetSection("TokenProviderOptions:Issuer").Value;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration.GetSection("jwt:issuer").Value,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("jwt:key").Value))
                };
                options.SaveToken = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedProto
            });
        }
    }
}
