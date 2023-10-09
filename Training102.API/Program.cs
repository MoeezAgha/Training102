
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Training102.DAL;
using Training102.BAL.Services.ConcreateImplementation;
using Training102.BAL.Services.Contract;
using Training102.BAL;
using Training102.BAL.Base.Repository;
using Training102.BAL.Base;
using Training102.BAL.Interfaces;
using System.Xml;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Training102.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

         builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString: builder.Configuration["ConnectionString:ApplicationDB"]));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtOptions =>
                {
                    JwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AuthSetting:ValidAudience"],
                        ValidIssuer = builder.Configuration["AuthSetting:ValidIssuer"],
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(s: builder.Configuration["AuthSetting:SymmetricSecurityKey"]))
                    };
                });

         
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.IgnoreCycles
            //};

            //var jsonString = JsonSerializer.Serialize(yourObject, options);
            // Add services to the container.
            //     DependencyInjectionConfiguration.ConfigureServices(builder.Services);
            //   builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}