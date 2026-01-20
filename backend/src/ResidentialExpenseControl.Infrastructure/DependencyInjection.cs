using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReisdentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseControl.Infrastructure.Data;
using ResidentialExpenseControl.Infrastructure.Repositories;
using ResidentialExpenseCOntrol.Application.UseCases.Person.Interfaces;



namespace ResidentialExpenseControl.Infrastructure
{
    // Classe estática para registrar todas as dependências
    // Mantém o Program.cs limpo e organizado
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Registra o DbContext com SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // Registra os repositórios
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Registra os Use Cases
            services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
            services.AddScoped<IGetAllPersonsUseCase, GetAllPersonsUseCase>();
            services.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>();

            return services;
        }
    }
}