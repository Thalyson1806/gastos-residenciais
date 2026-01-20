using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResidentialExpenseControl.Application.UseCases.Category;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Reports;
using ResidentialExpenseControl.Application.UseCases.Reports.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Transaction;
using ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseControl.Infrastructure.Data;
using ResidentialExpenseControl.Infrastructure.Repositories;

namespace ResidentialExpenseControl.Infrastructure
{
    // Classe de extensão para registrar todas as dependências
    // Centraliza a configuração de DI e mantém o Program.cs limpo
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Configura o DbContext com SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Person Use Cases
            services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
            services.AddScoped<IGetAllPersonsUseCase, GetAllPersonsUseCase>();
            services.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>();

            // Category Use Cases
            services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
            services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();

            // Transaction Use Cases
            services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
            services.AddScoped<IGetAllTransactionsUseCase, GetAllTransactionsUseCase>();

            // Report Use Cases
            services.AddScoped<IGetTotalsReportUseCase, GetTotalsReportUseCase>();

            return services;
        }
    }
}
