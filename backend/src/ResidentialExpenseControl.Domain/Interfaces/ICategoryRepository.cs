using ResidentialExpenseControl.Domain.Entities;

namespace ResidentialExpenseControl.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Retorna null se não encontrar 
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task SaveChangesAsync();
    }
}