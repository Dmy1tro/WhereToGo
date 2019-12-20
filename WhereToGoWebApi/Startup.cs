using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WhereToGoWebApi.DataBaseContext;
using WhereToGoWebApi.DbRepository;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Services;
using WhereToGoWebApi.Services.Interfaces;
using WhereToGoWebApi.TokenSettings;

namespace WhereToGoWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<EventDbContext>(x => x.UseSqlServer(connection));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IEventDbRepository, EventDbRepository>();

            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // User
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null;
            }
            ).AddEntityFrameworkStores<EventDbContext>()
            .AddDefaultTokenProviders();

            services.AddCors();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtSettings:Site"],
                    ValidIssuer = Configuration["JwtSettings:Site"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SigningKey"]))
                };
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new Info { Title = "WhereToGoApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(option => 
                option.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()); 

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "WhereToGoApi Version 1"));
        }
    }
}
