using FootballCrazeSweepstakes.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.ComponentModel.Design.Serialization;

namespace FootballCrazeSweepstakes
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
                })
                .Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<Dashboard>());
        }
        public static IServiceProvider? ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.Configure<AppSettings>(context.Configuration);
                services.Configure<SmtpSettings>(context.Configuration.GetSection("SmtpSettings"));
                services.AddDbContext<ApplicationDbContext>();

                // add logging
                var serilogLogger = new LoggerConfiguration()
                     .WriteTo.File(path: AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\errorlog.txt")
                     .CreateLogger();
                services.AddLogging(x =>
                {
                    x.SetMinimumLevel(LogLevel.Information);
                    x.AddSerilog(logger: serilogLogger, dispose: true);
                });

                services.AddTransient<IFootballCrazeSweepstakesRepository, FootballCrazeSweepstakesRepository>();
                    services.AddTransient<IFootballCrazeSweepstakesService, FootballCrazeSweepstakesService>();
                    services.AddTransient<Dashboard>();
                });
        }
    }
}