using Application;
using Infrastructure;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
try
{
    builder.Logging.ClearProviders();
    builder.Services.AddSerilog(config =>
    {
        config.Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:8081/")
                .MinimumLevel.Information();
    });
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressInferBindingSourcesForParameters = true;
    })
                        .AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        });
    ConfigurationAuthentification(builder);

    builder.Services.AddHttpClient()
                    .AddDistributedMemoryCache();
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();
    builder.Services.AddSignalR(e =>
    {
        e.ClientTimeoutInterval = TimeSpan.FromHours(12);
        e.EnableDetailedErrors = true;
        e.KeepAliveInterval = TimeSpan.FromMinutes(1);
    }).AddJsonProtocol();

    builder.Configuration.AddUserSecrets<Program>();
    builder.Services.AddInfrastructure(builder.Configuration["ConnectionString"]!);
    builder.Services.AddApplication(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
    }
    var defaultCulture = new CultureInfo("fr-FR");
    var localizationOptions = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(defaultCulture),
        SupportedCultures = [defaultCulture],
        SupportedUICultures = [defaultCulture]
    };
    app.UseRequestLocalization(localizationOptions);

    app.UseStaticFiles();
    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}

static void ConfigurationAuthentification(WebApplicationBuilder builder)
{
    builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:5122")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    });
}

public partial class Program { }