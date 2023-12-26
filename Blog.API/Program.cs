using Blog.App.Auth;
using Blog.App.Db;
using Blog.App.Db.Entities;
using Blog.App.Middlewares;
using Blog.App.Repositories.Implementations;
using Blog.App.Repositories.Interfaces;
using Blog.App.Services.Implementation;
using Blog.App.Services.Interfaces;
using Blog.App.UnitOfWork.Implementations;
using Blog.App.UnitOfWork.Interfaces;
using Blog.App.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

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
builder.Services.AddScoped<IValidator<AuthorEntity>, AuthorValidations>();
builder.Services.AddScoped<IValidator<BlogPostEntity>, BlogValidations>();

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
