using Blog.API.Auth;
using Blog.Domain.Entities;
using Blog.Common.Middlewares;
using Blog.Infrastructure.Repositories.Implementations;
using Blog.Infrastructure.Repositories.Interfaces;
using Blog.API.Services.Implementation;
using Blog.API.Services.Interfaces;
using Blog.Infrastructure.UnitOfWork.Implementations;
using Blog.Infrastructure.UnitOfWork.Interfaces;
using Blog.Common.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Blog.Infrastructure.Db;
using Blog.Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Services.AddLogging(options =>
{
    options.AddSerilog(dispose: true);
});
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddDbContextPool<BlogDbContext>(c =>
    c.UseSqlServer(builder.Configuration["DefaultConnection"]));

AuthConfigurator.Configure(builder);

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlogService, BlogServices>();
builder.Services.AddScoped<IValidator<AuthorDTO>, AuthorValidations>();
builder.Services.AddScoped<IValidator<BlogDTO>, BlogValidations>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\\r\\n\\r\\nExample: \"Bearer1safsfsdfdfd\"\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandleMiddlewares>();

app.UseAuthorization();

app.MapControllers();

app.Run();
