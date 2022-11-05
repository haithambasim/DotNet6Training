using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Training.Data.EntityFrameworkCore;
using Training.Services;

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
builder.Services.AddTransient<ArticleService>();
builder.Services.AddTransient<TagService>();
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
