using Microsoft.Extensions.DependencyInjection;
using Training102.BAL.Base;
using Training102.BAL.Interfaces;

namespace Training102.BAL
{
    public static class RegisterServicesFromBAL
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }

}