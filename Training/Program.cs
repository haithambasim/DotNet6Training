using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Training.Data.EntityFrameworkCore;
using Training.Exceptions;
using Training.Services;
using Training.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionsFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins
            (
                "https://localhost:7169",
                "http://localhost:5169"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<CmsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CmsConnectionString"));
});

// add auto mapper to the ioc container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.SaveToken = true;
                    config.RequireHttpsMetadata = false;
                    config.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration.GetSection("Jwt:Key").Get<string>()))
                    };
                });

// register app services ...
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<TagService>();
//... 

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .Filter.ByIncludingOnly(x => x.Level >= Serilog.Events.LogEventLevel.Error)
    .WriteTo.File("./Logger.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("CmsConnectionString"), new MSSqlServerSinkOptions()
    {
        TableName = "Logger",
        SchemaName = "Cms",
        AutoCreateSqlTable = true,
    });
});

builder.Services.Configure<List<AppClient>>(builder.Configuration.GetSection("AppClients"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ========> API Clients Middleware <========
//var AppClients = app.Configuration.GetSection("AppClients").Get<List<AppClient>>();

//app.Use(async (ctx, next) =>
//{
//    var clientName = ctx.Request.Headers.FirstOrDefault(x => x.Key == "Client-Name").Value.ToString();

//    var clientKey = ctx.Request.Headers.FirstOrDefault(x => x.Key == "Client-Key").Value.ToString();

//    if (!AppClients.Any(x => x.Name == clientName && x.ApiKey == clientKey))
//    {
//        ctx.Response.StatusCode = 400;

//        return;
//    }

//    await next(ctx);
//});
// ========> API Clients Middleware <========

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Files")),
    //OnPrepareResponse = ctx =>
    //{
    //    if (!ctx.Context.User.Identity.IsAuthenticated)
    //    {
    //        // respond HTTP 401 Unauthorized, 
    //        ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    //        // Append following 2 lines to drop body from static files middleware!
    //        ctx.Context.Response.ContentLength = 0;
    //        ctx.Context.Response.Body = Stream.Null;
    //    }
    //},
});

app.MapControllers();

app.Run();
