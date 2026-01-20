using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Domain.Entities;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseControl.Infrastructure.Data;

namespace ResidentialExpenseControl.Infrastructure.Repositories
{
    // Implementação do repositório de transações
    // Inclui método para deletar por PersonId (exclusão em cascata)
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            // Include carrega as entidades relacionadas (eager loading)
            // Necessário para exibir nome da pessoa e categoria
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(Guid personId)
        {
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .Where(t => t.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        // Deleta todas as transações de uma pessoa
        // Usado na exclusão em cascata
        public async Task DeleteByPersonIdAsync(Guid personId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.PersonId == personId)
                .ToListAsync();

            _context.Transactions.RemoveRange(transactions);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}