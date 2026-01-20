using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResidentialExpenseControl.Application.UseCases.Category;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseControl.Infrastructure.Data;
using ResidentialExpenseControl.Infrastructure.Repositories;

namespace ResidentialExpenseControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Person Use Cases
            services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
            services.AddScoped<IGetAllPersonsUseCase, GetAllPersonsUseCase>();
            services.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>();

            // Category Use Cases
            services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
            services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();

            return services;
        }
    }
}
