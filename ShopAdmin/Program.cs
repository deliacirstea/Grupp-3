using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopGeneral.Data;
using ShopGeneral.Services;
using Microsoft.EntityFrameworkCore;

var builder = ConsoleApp.CreateBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    var connectionString = ctx.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddTransient<IAgreementService, AgreementService>();
    // Using Cysharp/ZLogger for logging to file
    //services.AddLogging(logging =>
    //{
    //    logging.AddZLoggerFile("log.txt");
    //});
});

var app = builder.Build();

app.AddAllCommandType();
app.Run();
//generate prices to PriceRunner (JSON file)
//verify all product images exists 
//report categories without products
//report  

