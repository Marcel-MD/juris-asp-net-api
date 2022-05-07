using Azure.Storage.Blobs;
using Juris.Bll.Configuration;
using Juris.Api.Extensions;
using Juris.Api.Filters;
using Juris.Bll.IServices;
using Juris.Bll.Services;
using Juris.Dal;
using Juris.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Database Context
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssql"))
);

// Mail
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

// Blob
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("azurite")));
builder.Services.AddSingleton<IBlobService>(sp =>
    new BlobService(sp.GetService<BlobServiceClient>(), builder.Configuration.GetValue<string>("BlobContainer")));

// Identity
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);

// Services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppointmentRequestService, AppointmentRequestService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IProfileCategoryService, ProfileCategoryService>();

// Controllers
builder.Services.AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Custom Validation Errors Response
builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
        new BadRequestObjectResult(new
        {
            errors =
                actionContext.ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
        });
});

// Cors Policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAny", cors =>
        cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File(
        "..\\logs\\log-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAny");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.Seed(app);


app.Run();

public partial class Program
{
}