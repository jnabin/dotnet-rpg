global using dotnet_rpg.models;
using AspNetCoreRateLimit;
using dotnet_rpg.Data;
using dotnet_rpg.Services.CharacterService;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5088");
// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        if(!builder.Environment.IsDevelopment()){
            var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
            connectionString = string.Format(connectionString, password);
        }
        options.UseSqlServer(connectionString);
    }
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options => {
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = true;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>{
        new RateLimitRule{
            Endpoint = "*",
            Limit = 10,
            Period = "5s"
        }
    };
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                    GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.CreateScope()){
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

app.Run();
