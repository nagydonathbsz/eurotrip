using eurotrip.Auth;
using eurotrip.Modell;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace eurotrip
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. ADATBÁZIS KAPCSOLAT
            var connectionString = builder.Configuration.GetConnectionString("eurotrip");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Adatbázis csatlakozási karakterlánc hiányzik!");

            builder.Services.AddDbContext<EuroContext>(options =>
                options.UseMySQL(connectionString));

            // 2. CORS BEÁLLÍTÁSA (A React eléréséhez)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact",
                    policy => policy.WithOrigins("http://localhost:5173") // Ellenõrizd a portot!
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());
            });

            // 3. AUTH ÉS TOKEN KEZELÉS
            var tokenManager = new TokenManager(builder.Configuration);
            builder.Services.AddSingleton(tokenManager);

            AddJwtAuthentication(builder, tokenManager);

            // 4. KONTROLLEREK ÉS JSON FORMÁZÁS (camelCase kényszerítése a React miatt)
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Ez alakítja át a backend "Id"-t a frontend "id"-vé a válaszokban
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    // Megakadályozza az objektum-ciklusokat (pl. Város -> Étterem -> Város)
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();

            // SWAGGER KONFIGURÁCIÓ (Bearer Token támogatással)
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // --- MIDDLEWARE SORREND (Ez kritikus a CORS miatt!) ---

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // A CORS-nak az auth elõtt kell lennie!
            app.UseCors("AllowReact");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        // JWT és Authorization segédmetódus
        private static void AddJwtAuthentication(WebApplicationBuilder builder, TokenManager tm)
        {
            var jwtConf = builder.Configuration.GetSection("Auth:JWT");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConf["Issuer"],
                        ValidAudience = jwtConf["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf["Key"]!))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                foreach (var policyName in tm.Permissions)
                {
                    options.AddPolicy(policyName, policy => policy.RequireClaim("permission", policyName));
                }
            });
        }
    }
}