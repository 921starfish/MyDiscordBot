using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.IO;

namespace MyDiscordBot
{
    public class MyFirstBotDbContextFactory : IDesignTimeDbContextFactory<MyFirstBotDbContext>
    {
        public MyFirstBotDbContext CreateDbContext(string[] args)
        {
            using (var services = ConfigureServices())
            {
                var configuration = services.GetRequiredService<IConfiguration>();

                var optionsBuilder = new DbContextOptionsBuilder<MyFirstBotDbContext>();
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:MyFirstBotDb"]);

                return new MyFirstBotDbContext(optionsBuilder.Options);
            }
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IConfiguration>(
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: "appsettings.json").Build()
                )
                .BuildServiceProvider();
        }
    }
}
