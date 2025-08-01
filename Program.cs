using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
//using DotNetEnv;
using System.Text;

//Env.Load();

// var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
// var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
// var jwtDuration = Environment.GetEnvironmentVariable("JWT_DURATION");
// var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
// var baseUrl = Environment.GetEnvironmentVariable("BASE_URL");

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration["Jwt:Issuer"] = jwtIssuer;
// builder.Configuration["Jwt:Audience"] = jwtAudience;
// builder.Configuration["Jwt:Duration"] = jwtDuration;
// builder.Configuration["Jwt:Key"] = jwtKey;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json");

// builder.Configuration["GlobalConfiguration:BaseUrl"] = baseUrl;

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer("GatewayAuthentication", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!))
    };
});

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.UseOcelot();

app.Run();