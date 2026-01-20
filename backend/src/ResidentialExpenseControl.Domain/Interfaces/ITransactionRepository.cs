using ResidentialExpenseControl.Domain.Entities;

namespace ResidentialExpenseControl.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetByPersonIdAsync(Guid personId);
        Task AddAsync(Transaction transaction);
        Task DeleteByPersonIdAsync(Guid personId);
        Task SaveChangesAsync();
    }
}