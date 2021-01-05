using dapperApplication.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;


namespace dapperApplication
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            //services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));


            services.AddDbContextPool<DataContext.AppContext>
                (options => options.UseSqlServer(connectionString, sqloptions =>
                {
                    sqloptions.EnableRetryOnFailure(
                                   maxRetryCount: 5,
                                   maxRetryDelay: TimeSpan.FromSeconds(30),
                                   errorNumbersToAdd: new List<int>() { });
                }));
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IDapper, Dapperr>();
        }
    }
}
