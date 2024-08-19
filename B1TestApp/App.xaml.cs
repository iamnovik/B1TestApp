using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Implementations;
using B1TestApp.Repositories.Interfaces;
using B1TestApp.Services;
using B1TestApp.Services.Implementations;
using B1TestApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace B1TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AppDbContext>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ExcelParser>();
                    services.AddScoped<FileService>();
                    services.AddScoped<BankAccountDataService>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        public static T GetService<T>() where T : class
        {
            return (Current as App)._host.Services.GetService(typeof(T)) as T;
        }
    }
}