using Juris.Api.Exceptions;
using Juris.Api.Services;
using Juris.Data;
using Juris.Data.Repositories;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("InMemory"));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("docker-mssql"))
);

// Identity
builder.Services.AddAuthentication();
builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IAppointmentRequestService, AppointmentRequestService>();

// Controllers
builder.Services.AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAny", cors =>
        cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

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

app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.Seed(app);


app.Run();