using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PillPalAPI.Model;
using PillPalAPI.Options;
using PillPalAPI.Repositories;
using PillPalAPI.Validators;
using PillPalLib;
using System.Text;

namespace PillPalAPI;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            }
        );
        builder.Services.AddControllers();
        if (!args.Any(x=>x.Contains("testing")))
        {
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));//args.Length > 0 ? "DefaultConnection" : args[0]
            });
        }
        builder.Services.AddScoped<IDataStore, DataStore>();
        builder.Services.AddScoped<IItemStore<Medicine>, MedicineRepository>();
        builder.Services.AddScoped<IItemStore<SideEffect>, SideEffectRepository>();
        builder.Services.AddScoped<IJoinStore<Reminder>, ReminderRepository>();
        builder.Services.AddScoped<IJoinStore<MedicineSideEffect>, MedicineSideEffectRepository>();
        builder.Services.AddScoped<IItemStore<User>, UserRepository>();
        builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}