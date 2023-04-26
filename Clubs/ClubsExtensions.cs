using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clubs.BLL;
using Clubs.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clubs
{
    public static class ClubsExtensions
    {
        public static void CExtensions(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<StarTEDContext>(options); //WestWindContext

            //register each service class
            services.AddTransient<ClubsService>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<StarTEDContext>();
                return new ClubsService(context);
            });
            services.AddTransient<EmployeesService>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<StarTEDContext>();
                return new EmployeesService(context);
            });
        }
    }
}
