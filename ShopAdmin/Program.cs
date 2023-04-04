﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopGeneral.Data;
using ShopGeneral.Services;
using Microsoft.EntityFrameworkCore;
using ZLogger;

var builder = ConsoleApp.CreateBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    var connectionString = ctx.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddTransient<IAgreementService, AgreementService>();
    services.AddTransient<IPricingService, PricingService>();
    services.AddTransient<IProductService, ProductService>();
    services.AddAutoMapper(typeof(ShopGeneral.Infrastructure.Profiles.ProductProfile));
    services.AddTransient<DataInitializer>();

    services.AddLogging(logging => { logging.AddZLoggerFile("log.txt"); });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataInitializer = scope.ServiceProvider.GetService<DataInitializer>();
    dataInitializer.SeedData();
}

app.AddAllCommandType();
app.Run();
Console.ReadKey(true);



