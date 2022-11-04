using Microsoft.EntityFrameworkCore;
using Training.Data.EntityFrameworkCore;
using Training.Services;
using Training.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CmsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CmsConnectionString"));
});

// add auto mapper to the ioc container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// register app services ...
builder.Services.AddTransient<CategoryService>();
//... 

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

app.UseAuthorization();

app.MapControllers();

app.Run();
