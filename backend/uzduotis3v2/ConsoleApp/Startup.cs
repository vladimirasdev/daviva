using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace ConsoleApp.Models
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<appDbContext>(options => options
                .UseMySql("Server=178.128.202.96;Port=3306;User=TestUser;Password=TestasTesta5;Database=Testas;", mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql))
            ));
        }
    }
}