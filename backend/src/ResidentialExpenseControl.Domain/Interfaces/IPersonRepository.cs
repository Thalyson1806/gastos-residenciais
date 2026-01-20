using ResidentialExpenseControl.Domain.Entities;

namespace ResidentialExpenseControl.Domain.Interfaces
{
    // Interface definida no Domain, implementada na Infrastructure
    // Isso é Inversão de Dependência (DIP) - o Domain dita o contrato
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task DeleteAsync(Person person);
        Task SaveChangesAsync();
    }
}