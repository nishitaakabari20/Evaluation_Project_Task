using Microsoft.EntityFrameworkCore;
using Recipe.Service.RepositoryFactory;
using Recipe.Service.UnitOFWork;

namespace Test_api.HelperFolder
{
    public static class UnitOfWorkDServiceCollection
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddTransient<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddTransient<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddTransient<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }
    }
}
